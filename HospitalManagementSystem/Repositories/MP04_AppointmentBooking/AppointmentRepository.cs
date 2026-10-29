using System.Data;
using System.Text;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP03_DoctorScheduling;
using HospitalManagementSystem.Models.MP04_AppointmentBooking;
using MySqlConnector;

namespace HospitalManagementSystem.Repositories.MP04_AppointmentBooking
{
    /// <summary>
    /// All SQL for appointment booking (MP04, Part 8A): patient picker search, doctor list, slot availability,
    /// saving a new appointment and the appointments list.
    ///
    /// Slot availability for (doctor, date):
    ///   1. take the doctor's weekly blocks whose day_of_week matches the date (0 = Sunday ... 6 = Saturday);
    ///   2. cut each block into slots of its consultation_minutes (a slot must END inside the block);
    ///   3. a doctor leave on that date removes every slot;
    ///   4. a slot is taken when a Booked, Completed or No-show appointment already uses that time
    ///      (Cancelled appointments free their slot - the same rule as the database's unique key);
    ///   5. for today, slots whose start time has already passed are not offered.
    ///
    /// The form calls GetAvailability for early feedback; Book() repeats every rule inside the save
    /// transaction while the doctor row is locked, so two receptionists can never double-book a slot.
    ///
    /// Part 8B adds Cancel, Reschedule, MarkCompleted and MarkNoShow. All four share ONE rule function
    /// (CheckAction): the form asks GetBlockReason before it opens a dialog, and the save methods ask the same
    /// function again inside their transaction, so the screen and the database can never disagree.
    /// A cancelled appointment gets status 'Cancelled'; the generated column active_slot_flag then becomes NULL and the
    /// slot is free again for other patients (a rescheduled appointment simply moves, which frees its old slot).
    /// </summary>
    public class AppointmentRepository
    {
        private const int MaxPatientRows = 100;
        private const int MaxAppointmentRows = 500;

        /// <summary>How far ahead an appointment may be booked.</summary>
        public const int MaxAdvanceDays = 365;

        // ------------------------------------------------------------------
        // Reference lists
        // ------------------------------------------------------------------

        public List<LookupItem> GetAppointmentTypes() => AppointmentType.All.Select(LookupItem.Of).ToList();

        public List<LookupItem> GetStatuses() => AppointmentStatus.All.Select(LookupItem.Of).ToList();

        /// <summary>Doctors for the picker, optionally limited to one specialization (null / blank = all).</summary>
        public List<DoctorOption> GetDoctors(string? specialization)
        {
            var sql = new StringBuilder(
                "SELECT d.doctor_id, CONCAT(d.first_name, ' ', d.last_name) AS full_name, " +
                "       d.specialization, d.consultation_fee " +
                "FROM doctors d ");

            var parameters = new List<MySqlParameter>();
            if (!string.IsNullOrEmpty(specialization))
            {
                sql.Append("WHERE d.specialization = @spec ");
                parameters.Add(DatabaseHelper.Param("@spec", specialization));
            }
            sql.Append("ORDER BY d.last_name, d.first_name");

            DataTable table = DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());
            return table.Rows.Cast<DataRow>().Select(row => new DoctorOption
            {
                DoctorId = DatabaseHelper.GetString(row, "doctor_id"),
                FullName = DatabaseHelper.GetString(row, "full_name"),
                Specialization = DatabaseHelper.GetString(row, "specialization"),
                ConsultationFee = DatabaseHelper.GetDecimal(row, "consultation_fee")
            }).ToList();
        }

        // ------------------------------------------------------------------
        // Patient picker
        // ------------------------------------------------------------------

        /// <summary>
        /// "Search then select": every word typed must appear in the first, middle or last name, in the patient
        /// ID, or in the phone number (digits). Case-insensitive, partial matches, at most 100 rows.
        /// </summary>
        public DataTable SearchPatients(string text)
        {
            var sql = new StringBuilder(
                "SELECT p.patient_id, " +
                "       CONCAT(p.last_name, ', ', p.first_name, " +
                "              IF(p.middle_name IS NULL OR p.middle_name = '', '', CONCAT(' ', LEFT(p.middle_name, 1), '.'))) AS full_name, " +
                "       TIMESTAMPDIFF(YEAR, p.date_of_birth, CURDATE()) AS age, " +
                "       p.phone_number " +
                "FROM patients p WHERE 1 = 1");

            var parameters = new List<MySqlParameter>();
            string[] words = (text ?? string.Empty).Split(new[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                string like = "%" + EscapeLike(words[i]) + "%";
                string digits = new string(words[i].Where(char.IsDigit).ToArray());

                sql.Append($" AND (p.first_name LIKE @w{i} OR p.middle_name LIKE @w{i} OR p.last_name LIKE @w{i} OR p.patient_id LIKE @w{i}");
                parameters.Add(DatabaseHelper.Param($"@w{i}", like));

                if (digits.Length >= 3)
                {
                    // "0917..." must also find a number stored as +63917...: ignore one leading zero.
                    string phoneDigits = digits.StartsWith('0') ? digits.Substring(1) : digits;
                    sql.Append($" OR REGEXP_REPLACE(p.phone_number, '[^0-9]', '') LIKE @d{i}");
                    parameters.Add(DatabaseHelper.Param($"@d{i}", "%" + phoneDigits + "%"));
                }
                sql.Append(')');
            }

            sql.Append($" ORDER BY p.last_name, p.first_name, p.patient_id LIMIT {MaxPatientRows}");
            DataTable table = DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());

            // Show the phone number the same way everywhere (+63 917 ...), whatever form it was stored in.
            table.Columns.Add("phone_text", typeof(string));
            foreach (DataRow row in table.Rows)
            {
                row["phone_text"] = PhoneHelper.Format(DatabaseHelper.GetString(row, "phone_number"));
            }
            return table;
        }

        // ------------------------------------------------------------------
        // Availability
        // ------------------------------------------------------------------

        /// <summary>The slots of one doctor on one date (see the class description for the rules).</summary>
        public DayAvailability GetAvailability(string doctorId, DateTime date)
        {
            return DatabaseHelper.ExecuteInTransaction((conn, tx) => LoadAvailability(conn, tx, doctorId, date));
        }

        /// <summary>
        /// The doctor's calendar hint for the booking screen: the next leave dates (from today) and the first date on or
        /// after <paramref name="from"/> on which the doctor holds clinic and is not on leave (null when none in 90 days).
        /// </summary>
        public (List<(DateTime Date, string Reason)> Leaves, DateTime? NextClinicDay) GetDoctorCalendar(string doctorId, DateTime from)
        {
            DataTable leaves = DatabaseHelper.ExecuteQuery(
                "SELECT leave_date, reason FROM doctor_leaves WHERE doctor_id = @id AND leave_date >= CURDATE() " +
                "ORDER BY leave_date LIMIT 60",
                DatabaseHelper.Param("@id", doctorId));
            var list = leaves.Rows.Cast<DataRow>()
                .Select(r => ((DatabaseHelper.GetNullableDate(r, "leave_date") ?? DateTime.Today).Date, DatabaseHelper.GetString(r, "reason")))
                .ToList();

            DataTable days = DatabaseHelper.ExecuteQuery(
                "SELECT DISTINCT day_of_week FROM doctor_schedules WHERE doctor_id = @id", DatabaseHelper.Param("@id", doctorId));
            var working = new HashSet<int>(days.Rows.Cast<DataRow>().Select(r => DatabaseHelper.GetInt(r, "day_of_week")));
            var leaveDates = new HashSet<DateTime>(list.Select(l => l.Item1));

            DateTime? next = null;
            if (working.Count > 0)
            {
                for (DateTime d = (from.Date < DateTime.Today ? DateTime.Today : from.Date); d <= DateTime.Today.AddDays(90); d = d.AddDays(1))
                {
                    if (working.Contains((int)d.DayOfWeek) && !leaveDates.Contains(d)) { next = d; break; }
                }
            }
            return (list.Take(4).ToList(), next);
        }

        /// <summary>True when the patient already has a non-cancelled appointment with this doctor on this date.</summary>
        public bool PatientHasActiveAppointment(string patientId, string doctorId, DateTime date)
        {
            long count = DatabaseHelper.ExecuteScalar<long>(
                "SELECT COUNT(*) FROM appointments " +
                "WHERE patient_id = @patient AND doctor_id = @doctor AND appointment_date = @date AND status <> @cancelled",
                DatabaseHelper.Param("@patient", patientId),
                DatabaseHelper.Param("@doctor", doctorId),
                DatabaseHelper.Param("@date", date.Date),
                DatabaseHelper.Param("@cancelled", AppointmentStatus.Cancelled));
            return count > 0;
        }

        // ------------------------------------------------------------------
        // Booking
        // ------------------------------------------------------------------

        /// <summary>
        /// Saves a new appointment. Re-checks every booking rule inside the transaction with the doctor row locked,
        /// generates the APT id and the next token number of that doctor on that date, then inserts.
        /// Rule failures throw InvalidOperationException with a message that is safe to show to staff.
        /// </summary>
        public BookingResult Book(BookingRequest request)
        {
            return DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                DateTime date = request.Date.Date;

                if (date < DateTime.Today)
                    throw new InvalidOperationException("An appointment cannot be booked for a date in the past.");
                if (date > DateTime.Today.AddDays(MaxAdvanceDays))
                    throw new InvalidOperationException($"Appointments can only be booked up to {MaxAdvanceDays} days ahead.");

                // Lock the doctor: bookings for the same doctor are saved one after the other.
                DataTable doctor = DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT doctor_id FROM doctors WHERE doctor_id = @id FOR UPDATE",
                    DatabaseHelper.Param("@id", request.DoctorId));
                if (doctor.Rows.Count == 0)
                    throw new InvalidOperationException("This doctor no longer exists. Please pick the doctor again.");

                long patients = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                    "SELECT COUNT(*) FROM patients WHERE patient_id = @id",
                    DatabaseHelper.Param("@id", request.PatientId));
                if (patients == 0)
                    throw new InvalidOperationException("This patient no longer exists. Please search for the patient again.");

                DayAvailability day = LoadAvailability(conn, tx, request.DoctorId, date);

                if (day.OnLeave)
                {
                    string leaveDay = date.ToString("dddd, MMMM d, yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    throw new InvalidOperationException($"The doctor is on leave on {leaveDay} ({day.LeaveReason}). Please choose another date.");
                }

                if (!day.WorksThisDay)
                    throw new InvalidOperationException(
                        $"The doctor does not see patients on {date.DayOfWeek}s. Please choose another date.");

                SlotInfo? slot = day.Slots.FirstOrDefault(s => s.Time == request.TimeSlot);
                if (slot == null)
                    throw new InvalidOperationException("That time is not one of the doctor's consultation slots for this date.");
                if (slot.IsPast)
                    throw new InvalidOperationException($"{slot.Text} has already passed. Please choose a later time.");
                if (slot.IsTaken)
                    throw new InvalidOperationException($"The {slot.Text} slot was just taken by another appointment. Please choose another time.");

                long duplicates = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                    "SELECT COUNT(*) FROM appointments " +
                    "WHERE patient_id = @patient AND doctor_id = @doctor AND appointment_date = @date AND status <> @cancelled",
                    DatabaseHelper.Param("@patient", request.PatientId),
                    DatabaseHelper.Param("@doctor", request.DoctorId),
                    DatabaseHelper.Param("@date", date),
                    DatabaseHelper.Param("@cancelled", AppointmentStatus.Cancelled));
                if (duplicates > 0)
                    throw new InvalidOperationException("This patient already has an appointment with this doctor on that day.");

                // Id and token are generated INSIDE the transaction: if the insert fails, both are released.
                string appointmentId = GeneratorHelper.Generate(GeneratorHelper.Prefix.Appointment, conn, tx);

                // Cancelled appointments keep their token (the database key covers every status), so tokens are never reused.
                long lastToken = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                    "SELECT COALESCE(MAX(token_number), 0) FROM appointments WHERE doctor_id = @doctor AND appointment_date = @date",
                    DatabaseHelper.Param("@doctor", request.DoctorId),
                    DatabaseHelper.Param("@date", date));
                int token = (int)lastToken + 1;

                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "INSERT INTO appointments (appointment_id, token_number, patient_id, doctor_id, appointment_date, time_slot, " +
                    "                          appointment_type, reason_for_visit, status, created_by_user_id) " +
                    "VALUES (@id, @token, @patient, @doctor, @date, @slot, @type, @reason, @booked, @user)",
                    DatabaseHelper.Param("@id", appointmentId),
                    DatabaseHelper.Param("@token", token),
                    DatabaseHelper.Param("@patient", request.PatientId),
                    DatabaseHelper.Param("@doctor", request.DoctorId),
                    DatabaseHelper.Param("@date", date),
                    DatabaseHelper.Param("@slot", request.TimeSlot),
                    DatabaseHelper.Param("@type", request.AppointmentType),
                    DatabaseHelper.Param("@reason", string.IsNullOrWhiteSpace(request.Reason) ? null : request.Reason.Trim()),
                    DatabaseHelper.Param("@booked", AppointmentStatus.Booked),
                    DatabaseHelper.Param("@user", SessionManager.CurrentUserId));

                return new BookingResult { AppointmentId = appointmentId, TokenNumber = token };
            });
        }

        // ------------------------------------------------------------------
        // Appointments list
        // ------------------------------------------------------------------

        /// <summary>Appointments matching ALL filled filters, newest date first then by time (at most 500 rows).</summary>
        public DataTable GetAppointments(AppointmentFilter filter)
        {
            var sql = new StringBuilder(
                "SELECT a.appointment_id, a.token_number, " +
                "       DATE_FORMAT(a.appointment_date, '%a, %b %e, %Y') AS date_text, " +
                "       TIME_FORMAT(a.time_slot, '%l:%i %p') AS time_text, " +
                "       CONCAT(p.last_name, ', ', p.first_name) AS patient_name, p.patient_id, " +
                "       CONCAT('Dr. ', d.first_name, ' ', d.last_name) AS doctor_name, " +
                "       a.appointment_type AS type_name, a.status AS status_name, " +
                "       COALESCE(a.reason_for_visit, '') AS reason " +
                "FROM appointments a " +
                "JOIN patients p ON p.patient_id = a.patient_id " +
                "JOIN doctors d ON d.doctor_id = a.doctor_id " +
                "WHERE 1 = 1");

            var parameters = new List<MySqlParameter>();

            if (filter.Date.HasValue)
            {
                sql.Append(" AND a.appointment_date = @date");
                parameters.Add(DatabaseHelper.Param("@date", filter.Date.Value.Date));
            }
            if (!string.IsNullOrEmpty(filter.DoctorId))
            {
                sql.Append(" AND a.doctor_id = @doctor");
                parameters.Add(DatabaseHelper.Param("@doctor", filter.DoctorId));
            }
            if (!string.IsNullOrEmpty(filter.Status))
            {
                sql.Append(" AND a.status = @status");
                parameters.Add(DatabaseHelper.Param("@status", filter.Status));
            }

            string[] words = (filter.PatientText ?? string.Empty).Split(new[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                sql.Append($" AND (p.first_name LIKE @w{i} OR p.middle_name LIKE @w{i} OR p.last_name LIKE @w{i} OR p.patient_id LIKE @w{i})");
                parameters.Add(DatabaseHelper.Param($"@w{i}", "%" + EscapeLike(words[i]) + "%"));
            }

            sql.Append($" ORDER BY a.appointment_date DESC, a.time_slot, a.token_number LIMIT {MaxAppointmentRows}");
            return DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());
        }

        // ------------------------------------------------------------------
        // Appointment management (Part 8B): Cancel, Reschedule, Mark Completed, Mark No-show
        // ------------------------------------------------------------------

        /// <summary>Used when the setting cancellation_window_hours is missing or not a number.</summary>
        public const int DefaultCancellationWindowHours = 2;

        /// <summary>Longest text the status_remarks column accepts.</summary>
        public const int MaxRemarksLength = 250;

        /// <summary>
        /// An appointment can no longer be cancelled when it starts within this many hours
        /// (setting cancellation_window_hours, 2 by default).
        /// </summary>
        public static int CancellationWindowHours =>
            Math.Max(0, SettingsHelper.GetInt(SettingsHelper.CancellationWindowHours, DefaultCancellationWindowHours));

        /// <summary>One saved appointment with patient and doctor names, or null when it no longer exists.</summary>
        public AppointmentRecord? GetAppointment(string appointmentId)
        {
            return DatabaseHelper.ExecuteInTransaction((conn, tx) => LoadAppointment(conn, tx, appointmentId, lockRow: false));
        }

        /// <summary>
        /// Early feedback for the form: the reason why the action is not allowed right now, or null when it is allowed.
        /// Uses the same rule function as the save methods.
        /// </summary>
        public string? GetBlockReason(string appointmentId, AppointmentAction action)
        {
            return DatabaseHelper.ExecuteInTransaction<string?>((conn, tx) =>
            {
                AppointmentRecord? appointment = LoadAppointment(conn, tx, appointmentId, lockRow: false);
                return appointment == null
                    ? "This appointment no longer exists. The list will be refreshed."
                    : CheckAction(conn, tx, appointment, action);
            });
        }

        /// <summary>
        /// Cancels a Booked appointment. A reason is required and is stored in status_remarks.
        /// Blocked when the appointment starts within CancellationWindowHours (or has already started).
        /// The slot is free again immediately (active_slot_flag becomes NULL).
        /// </summary>
        public void Cancel(string appointmentId, string reason)
        {
            reason = (reason ?? string.Empty).Trim();
            if (reason.Length == 0)
                throw new InvalidOperationException("Enter the reason for the cancellation.");
            if (reason.Length > MaxRemarksLength)
                throw new InvalidOperationException($"The reason can have at most {MaxRemarksLength} characters.");

            DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                AppointmentRecord appointment = LoadForUpdate(conn, tx, appointmentId);

                string? blocked = CheckAction(conn, tx, appointment, AppointmentAction.Cancel);
                if (blocked != null) throw new InvalidOperationException(blocked);

                UpdateStatus(conn, tx, appointmentId, AppointmentStatus.Cancelled, reason);
            });
        }

        /// <summary>Marks a Booked appointment as Completed (final: it can no longer be cancelled, moved or marked No-show).</summary>
        public void MarkCompleted(string appointmentId)
        {
            DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                AppointmentRecord appointment = LoadForUpdate(conn, tx, appointmentId);

                string? blocked = CheckAction(conn, tx, appointment, AppointmentAction.Complete);
                if (blocked != null) throw new InvalidOperationException(blocked);

                UpdateStatus(conn, tx, appointmentId, AppointmentStatus.Completed, null);
            });
        }

        /// <summary>Marks a Booked appointment whose time has passed as No-show. The slot stays used (like the database key).</summary>
        public void MarkNoShow(string appointmentId)
        {
            DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                AppointmentRecord appointment = LoadForUpdate(conn, tx, appointmentId);

                string? blocked = CheckAction(conn, tx, appointment, AppointmentAction.NoShow);
                if (blocked != null) throw new InvalidOperationException(blocked);

                UpdateStatus(conn, tx, appointmentId, AppointmentStatus.NoShow, null);
            });
        }

        /// <summary>
        /// Moves a Booked appointment to another open slot of the SAME doctor. The Appointment ID stays the same.
        /// The old slot becomes free because the appointment leaves it. When the date changes, the appointment gets the
        /// next token of that doctor on the new date; when only the time changes, the token stays.
        /// Every booking rule is checked again inside the transaction with the doctor row locked.
        /// </summary>
        public RescheduleResult Reschedule(string appointmentId, DateTime newDate, TimeSpan newTimeSlot)
        {
            return DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                newDate = newDate.Date;

                if (newDate < DateTime.Today)
                    throw new InvalidOperationException("An appointment cannot be moved to a date in the past.");
                if (newDate > DateTime.Today.AddDays(MaxAdvanceDays))
                    throw new InvalidOperationException($"Appointments can only be booked up to {MaxAdvanceDays} days ahead.");

                // Same lock order as Book(): the doctor first, then the appointment.
                AppointmentRecord? preview = LoadAppointment(conn, tx, appointmentId, lockRow: false);
                if (preview == null)
                    throw new InvalidOperationException("This appointment no longer exists. The list will be refreshed.");

                DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT doctor_id FROM doctors WHERE doctor_id = @id FOR UPDATE",
                    DatabaseHelper.Param("@id", preview.DoctorId));

                AppointmentRecord appointment = LoadForUpdate(conn, tx, appointmentId);

                string? blocked = CheckAction(conn, tx, appointment, AppointmentAction.Reschedule);
                if (blocked != null) throw new InvalidOperationException(blocked);

                if (newDate == appointment.Date && newTimeSlot == appointment.TimeSlot)
                    throw new InvalidOperationException("That is the current date and time. Choose a different date or time.");

                DayAvailability day = LoadAvailability(conn, tx, appointment.DoctorId, newDate);

                if (day.OnLeave)
                {
                    string leaveDay = newDate.ToString("dddd, MMMM d, yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    throw new InvalidOperationException($"The doctor is on leave on {leaveDay} ({day.LeaveReason}). Please choose another date.");
                }
                if (!day.WorksThisDay)
                    throw new InvalidOperationException($"The doctor does not see patients on {newDate.DayOfWeek}s. Please choose another date.");

                SlotInfo? slot = day.Slots.FirstOrDefault(s => s.Time == newTimeSlot);
                if (slot == null)
                    throw new InvalidOperationException("That time is not one of the doctor's consultation slots for this date.");
                if (slot.IsPast)
                    throw new InvalidOperationException($"{slot.Text} has already passed. Please choose a later time.");
                if (slot.IsTaken)
                    throw new InvalidOperationException($"The {slot.Text} slot was just taken by another appointment. Please choose another time.");

                bool dateChanged = newDate != appointment.Date;

                if (dateChanged)
                {
                    long duplicates = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                        "SELECT COUNT(*) FROM appointments " +
                        "WHERE patient_id = @patient AND doctor_id = @doctor AND appointment_date = @date " +
                        "  AND status <> @cancelled AND appointment_id <> @self",
                        DatabaseHelper.Param("@patient", appointment.PatientId),
                        DatabaseHelper.Param("@doctor", appointment.DoctorId),
                        DatabaseHelper.Param("@date", newDate),
                        DatabaseHelper.Param("@cancelled", AppointmentStatus.Cancelled),
                        DatabaseHelper.Param("@self", appointmentId));
                    if (duplicates > 0)
                        throw new InvalidOperationException("This patient already has another appointment with this doctor on that day.");
                }

                int token = appointment.TokenNumber;
                if (dateChanged)
                {
                    // Tokens are never reused, so the new date gets the highest token of that doctor + 1.
                    long lastToken = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                        "SELECT COALESCE(MAX(token_number), 0) FROM appointments WHERE doctor_id = @doctor AND appointment_date = @date",
                        DatabaseHelper.Param("@doctor", appointment.DoctorId),
                        DatabaseHelper.Param("@date", newDate));
                    token = (int)lastToken + 1;
                }

                string remarks = ("Rescheduled from " + appointment.WhenText).Trim();
                if (remarks.Length > MaxRemarksLength) remarks = remarks.Substring(0, MaxRemarksLength);

                int rows = DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "UPDATE appointments SET appointment_date = @date, time_slot = @slot, token_number = @token, " +
                    "       status_remarks = @remarks, status_updated_at = NOW() " +
                    "WHERE appointment_id = @id AND status = @booked",
                    DatabaseHelper.Param("@date", newDate),
                    DatabaseHelper.Param("@slot", newTimeSlot),
                    DatabaseHelper.Param("@token", token),
                    DatabaseHelper.Param("@remarks", remarks),
                    DatabaseHelper.Param("@id", appointmentId),
                    DatabaseHelper.Param("@booked", AppointmentStatus.Booked));
                if (rows == 0)
                    throw new InvalidOperationException("The appointment was changed by someone else. The list will be refreshed.");

                return new RescheduleResult
                {
                    AppointmentId = appointment.AppointmentId,
                    OldWhenText = appointment.WhenText,
                    NewWhenText = AppointmentRecord.FormatWhen(newDate, newTimeSlot),
                    TokenChanged = dateChanged,
                    NewToken = token
                };
            });
        }

        // ------------------------------------------------------------------
        // Private helpers
        // ------------------------------------------------------------------

        private static DayAvailability LoadAvailability(MySqlConnection conn, MySqlTransaction tx, string doctorId, DateTime date)
        {
            date = date.Date;
            var result = new DayAvailability { DoctorId = doctorId, Date = date };

            // 1. Weekly blocks: all of them (for the "works on Mon, Wed" hint) and the ones of this weekday (for the slots).
            DataTable blocks = DatabaseHelper.ExecuteQuery(conn, tx,
                "SELECT day_of_week, start_time, end_time, consultation_minutes " +
                "FROM doctor_schedules WHERE doctor_id = @doctor ORDER BY (day_of_week + 6) % 7, start_time",
                DatabaseHelper.Param("@doctor", doctorId));

            var workingDays = new List<DayOfWeek>();
            var todaysBlocks = new List<(TimeSpan Start, TimeSpan End, int Minutes)>();

            foreach (DataRow row in blocks.Rows)
            {
                var day = (DayOfWeek)DatabaseHelper.GetInt(row, "day_of_week");
                if (!workingDays.Contains(day)) workingDays.Add(day);

                if (day == date.DayOfWeek)
                {
                    todaysBlocks.Add((ReadTime(row["start_time"]), ReadTime(row["end_time"]),
                                      DatabaseHelper.GetInt(row, "consultation_minutes")));
                }
            }

            result.WorkingDaysText = string.Join(", ", workingDays.Select(ScheduleBlock.ShortDayLabel));
            result.WorksThisDay = todaysBlocks.Count > 0;

            if (todaysBlocks.Count > 0)
            {
                result.Minutes = todaysBlocks[0].Minutes;
                result.WorkingHoursText = string.Join(", ",
                    todaysBlocks.Select(b => $"{ScheduleBlock.FormatTime(b.Start)} - {ScheduleBlock.FormatTime(b.End)}"));
            }

            // 2. Leave on this date removes every slot.
            DataTable leave = DatabaseHelper.ExecuteQuery(conn, tx,
                "SELECT reason FROM doctor_leaves WHERE doctor_id = @doctor AND leave_date = @date",
                DatabaseHelper.Param("@doctor", doctorId),
                DatabaseHelper.Param("@date", date));
            if (leave.Rows.Count > 0)
            {
                result.OnLeave = true;
                result.LeaveReason = DatabaseHelper.GetString(leave.Rows[0], "reason");
                return result;
            }

            // 3. Times already used (every status except Cancelled).
            DataTable used = DatabaseHelper.ExecuteQuery(conn, tx,
                "SELECT time_slot FROM appointments " +
                "WHERE doctor_id = @doctor AND appointment_date = @date AND status <> @cancelled",
                DatabaseHelper.Param("@doctor", doctorId),
                DatabaseHelper.Param("@date", date),
                DatabaseHelper.Param("@cancelled", AppointmentStatus.Cancelled));
            var taken = new HashSet<TimeSpan>(used.Rows.Cast<DataRow>().Select(r => ReadTime(r["time_slot"])));

            // 4. Cut every block into slots. A slot must finish inside its block.
            TimeSpan now = DateTime.Now.TimeOfDay;
            bool isToday = date == DateTime.Today;

            foreach (var block in todaysBlocks)
            {
                if (block.Minutes <= 0) continue;
                var length = TimeSpan.FromMinutes(block.Minutes);

                for (TimeSpan start = block.Start; start + length <= block.End; start += length)
                {
                    result.Slots.Add(new SlotInfo
                    {
                        Time = start,
                        Minutes = block.Minutes,
                        IsTaken = taken.Contains(start),
                        IsPast = isToday && start <= now
                    });
                }
            }

            result.Slots = result.Slots.OrderBy(s => s.Time).ToList();
            return result;
        }

        // ---- Part 8B helpers ----

        /// <summary>Reads one appointment (optionally locking its row) with the patient and doctor names; null when missing.</summary>
        private static AppointmentRecord? LoadAppointment(MySqlConnection conn, MySqlTransaction tx, string appointmentId, bool lockRow)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(conn, tx,
                "SELECT appointment_id, token_number, patient_id, doctor_id, appointment_date, " +
                "       time_slot, status, status_remarks " +
                "FROM appointments WHERE appointment_id = @id" + (lockRow ? " FOR UPDATE" : string.Empty),
                DatabaseHelper.Param("@id", appointmentId));
            if (table.Rows.Count == 0) return null;

            DataRow row = table.Rows[0];
            var record = new AppointmentRecord
            {
                AppointmentId = DatabaseHelper.GetString(row, "appointment_id"),
                TokenNumber = DatabaseHelper.GetInt(row, "token_number"),
                PatientId = DatabaseHelper.GetString(row, "patient_id"),
                DoctorId = DatabaseHelper.GetString(row, "doctor_id"),
                Date = Convert.ToDateTime(row["appointment_date"]).Date,
                TimeSlot = ReadTime(row["time_slot"]),
                Status = DatabaseHelper.GetString(row, "status"),
                Remarks = DatabaseHelper.GetString(row, "status_remarks")
            };

            // Names come from a second query so that FOR UPDATE locks only the appointment row.
            DataTable names = DatabaseHelper.ExecuteQuery(conn, tx,
                "SELECT CONCAT(p.last_name, ', ', p.first_name) AS patient_name, " +
                "       CONCAT('Dr. ', d.first_name, ' ', d.last_name) AS doctor_name " +
                "FROM patients p, doctors d WHERE p.patient_id = @patient AND d.doctor_id = @doctor",
                DatabaseHelper.Param("@patient", record.PatientId),
                DatabaseHelper.Param("@doctor", record.DoctorId));
            if (names.Rows.Count > 0)
            {
                record.PatientName = DatabaseHelper.GetString(names.Rows[0], "patient_name");
                record.DoctorName = DatabaseHelper.GetString(names.Rows[0], "doctor_name");
            }
            return record;
        }

        private static AppointmentRecord LoadForUpdate(MySqlConnection conn, MySqlTransaction tx, string appointmentId)
        {
            return LoadAppointment(conn, tx, appointmentId, lockRow: true)
                   ?? throw new InvalidOperationException("This appointment no longer exists. The list will be refreshed.");
        }

        /// <summary>Changes the status of a Booked appointment; nothing is written when someone else changed it first.</summary>
        private static void UpdateStatus(MySqlConnection conn, MySqlTransaction tx, string appointmentId, string newStatus, string? remarks)
        {
            int rows = DatabaseHelper.ExecuteNonQuery(conn, tx,
                "UPDATE appointments SET status = @status, status_remarks = COALESCE(@remarks, status_remarks), " +
                "       status_updated_at = NOW() " +
                "WHERE appointment_id = @id AND status = @booked",
                DatabaseHelper.Param("@status", newStatus),
                DatabaseHelper.Param("@remarks", remarks),
                DatabaseHelper.Param("@id", appointmentId),
                DatabaseHelper.Param("@booked", AppointmentStatus.Booked));
            if (rows == 0)
                throw new InvalidOperationException("The appointment was changed by someone else. The list will be refreshed.");
        }

        private static string VerbOf(AppointmentAction action) => action switch
        {
            AppointmentAction.Cancel => "cancelled",
            AppointmentAction.Reschedule => "rescheduled",
            AppointmentAction.Complete => "marked as Completed",
            _ => "marked as No-show"
        };

        /// <summary>
        /// THE rules of Part 8B (used by the form for early feedback and again inside every save transaction).
        /// Returns the reason why the action is not allowed, or null when it is allowed.
        ///   - Only a Booked appointment can be cancelled, rescheduled, completed or marked No-show;
        ///     Completed is final, Cancelled and No-show are final as well.
        ///   - Cancel / Reschedule / No-show are refused once the patient has checked in on the queue board.
        ///   - Cancel is refused when the visit starts within CancellationWindowHours or has already started.
        ///   - Completed and No-show are refused for a future date; No-show also before the appointment time.
        /// </summary>
        private static string? CheckAction(MySqlConnection conn, MySqlTransaction tx, AppointmentRecord a, AppointmentAction action)
        {
            if (a.Status != AppointmentStatus.Booked)
            {
                string verb = VerbOf(action);
                return a.Status switch
                {
                    AppointmentStatus.Completed => $"{a.AppointmentId} is already Completed, so it can no longer be {verb}.",
                    AppointmentStatus.Cancelled => $"{a.AppointmentId} is already Cancelled, so it cannot be {verb}.",
                    AppointmentStatus.NoShow => $"{a.AppointmentId} was marked No-show, so it can no longer be {verb}.",
                    _ => $"{a.AppointmentId} cannot be {verb} in its current status."
                };
            }

            DateTime now = DateTime.Now;

            if (action != AppointmentAction.Complete)
            {
                long queued = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                    "SELECT COUNT(*) FROM queue_entries WHERE appointment_id = @id",
                    DatabaseHelper.Param("@id", a.AppointmentId));
                if (queued > 0)
                    return $"{a.PatientName} has already checked in on the Patient Queue Board ({a.AppointmentId}), " +
                           $"so this appointment cannot be {VerbOf(action)}. Finish the visit from the queue board instead.";
            }

            switch (action)
            {
                case AppointmentAction.Cancel:
                    {
                        int window = CancellationWindowHours;
                        TimeSpan untilStart = a.StartsAt - now;

                        if (untilStart <= TimeSpan.Zero)
                            return $"{a.AppointmentId} ({a.WhenText}) has already started or passed, so it can no longer be cancelled. " +
                                   "Mark it Completed or No-show instead.";

                        if (untilStart < TimeSpan.FromHours(window))
                        {
                            int minutes = (int)Math.Ceiling(untilStart.TotalMinutes);
                            string left = minutes < 60 ? $"{minutes} minute(s)" : $"{minutes / 60} h {minutes % 60} min";
                            return $"An appointment cannot be cancelled within {window} hour(s) of its start time. " +
                                   $"{a.AppointmentId} starts at {ScheduleBlock.FormatTime(a.TimeSlot)} (in {left}).";
                        }
                        return null;
                    }

                case AppointmentAction.Complete:
                    if (a.Date > DateTime.Today)
                        return $"{a.AppointmentId} is scheduled for {a.WhenText}. It can only be marked Completed on or after its appointment date.";
                    return null;

                case AppointmentAction.NoShow:
                    if (a.Date > DateTime.Today)
                        return $"{a.AppointmentId} is scheduled for {a.WhenText}. A patient can only be marked No-show once the appointment time has passed.";
                    if (a.StartsAt > now)
                        return $"{a.AppointmentId} has not started yet (it starts at {ScheduleBlock.FormatTime(a.TimeSlot)}). " +
                               "A patient can only be marked No-show once the appointment time has passed.";
                    return null;

                default:        // Reschedule: no extra time rule (a patient who is late may be moved to a later slot)
                    return null;
            }
        }

        /// <summary>Reads a MySQL TIME value however the driver delivers it.</summary>
        private static TimeSpan ReadTime(object value)
        {
            return value switch
            {
                TimeSpan span => span,
                DateTime dateTime => dateTime.TimeOfDay,
                _ => TimeSpan.Parse(Convert.ToString(value) ?? "00:00:00", System.Globalization.CultureInfo.InvariantCulture)
            };
        }

        /// <summary>Makes % and _ typed by the user match literally instead of acting as wildcards.</summary>
        private static string EscapeLike(string text)
        {
            return text.Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_");
        }
    }
}