using System.Data;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP03_DoctorScheduling;
using MySqlConnector;

namespace HospitalManagementSystem.Repositories.MP03_DoctorScheduling
{
    /// <summary>
    /// All SQL for the weekly schedule builder (MP03, Part 7B): availability blocks and leave dates.
    /// Every rule (end after start, minimum minutes, no overlap, daily hour limit) is checked here as well as
    /// in the form: the form only gives early feedback, this class is the authority and re-checks inside the
    /// save transaction while the doctor row is locked, so two users can never save conflicting blocks.
    /// </summary>
    public class DoctorScheduleRepository
    {
        private const string StatusBooked = "Booked";   // appointments.status

        /// <summary>Smallest allowed consultation length (app_settings: min_consultation_minutes, default 10).</summary>
        public int MinConsultationMinutes => SettingsHelper.GetInt(SettingsHelper.MinConsultationMinutes, 10);

        /// <summary>Most hours one doctor may be scheduled per day (app_settings: max_daily_working_hours, default 12).</summary>
        public decimal MaxDailyHours => SettingsHelper.GetDecimal(SettingsHelper.MaxDailyWorkingHours, 12m);

        /// <summary>The consultation length pre-filled for a new block (app_settings: default_consultation_minutes).</summary>
        public int DefaultConsultationMinutes => SettingsHelper.GetInt(SettingsHelper.DefaultConsultationMinutes, 15);

        // ------------------------------------------------------------------
        // Availability blocks
        // ------------------------------------------------------------------

        /// <summary>All blocks of one doctor, Monday first, then by start time.</summary>
        public List<ScheduleBlock> GetBlocks(string doctorId)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(
                "SELECT schedule_id, doctor_id, day_of_week, start_time, end_time, consultation_minutes, notes " +
                "FROM doctor_schedules WHERE doctor_id = @id " +
                "ORDER BY (day_of_week + 6) % 7, start_time",
                DatabaseHelper.Param("@id", doctorId));

            return table.Rows.Cast<DataRow>().Select(MapBlock).ToList();
        }

        public ScheduleBlock? GetBlock(int scheduleId)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(
                "SELECT schedule_id, doctor_id, day_of_week, start_time, end_time, consultation_minutes, notes " +
                "FROM doctor_schedules WHERE schedule_id = @id",
                DatabaseHelper.Param("@id", scheduleId));

            return table.Rows.Count == 0 ? null : MapBlock(table.Rows[0]);
        }

        /// <summary>
        /// Early feedback for the form: every rule problem of this block (empty list = the block is acceptable).
        /// The block's own ScheduleId is ignored while looking for overlaps, so editing a block never clashes with itself.
        /// </summary>
        public List<ScheduleProblem> CheckBlock(ScheduleBlock block)
        {
            return DatabaseHelper.ExecuteInTransaction((conn, tx) => FindProblems(conn, tx, block));
        }

        /// <summary>Saves a new block. Returns the new schedule id.</summary>
        public int AddBlock(ScheduleBlock block)
        {
            long newId = DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                LockDoctor(conn, tx, block.DoctorId);
                ThrowIfProblems(FindProblems(conn, tx, block));

                return DatabaseHelper.ExecuteInsert(conn, tx,
                    "INSERT INTO doctor_schedules (doctor_id, day_of_week, start_time, end_time, consultation_minutes, notes) " +
                    "VALUES (@doc, @dow, @start, @end, @mins, @notes)",
                    DatabaseHelper.Param("@doc", block.DoctorId),
                    DatabaseHelper.Param("@dow", (int)block.Day),
                    DatabaseHelper.Param("@start", block.StartTime),
                    DatabaseHelper.Param("@end", block.EndTime),
                    DatabaseHelper.Param("@mins", block.ConsultationMinutes),
                    DatabaseHelper.Param("@notes", NullIfBlank(block.Notes)));
            });

            return (int)newId;
        }

        /// <summary>
        /// Changes an existing block. Refused when the change would leave booked future appointments
        /// outside the doctor's working hours.
        /// </summary>
        public void UpdateBlock(ScheduleBlock block)
        {
            DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                LockDoctor(conn, tx, block.DoctorId);

                ScheduleBlock existing = ReadBlock(conn, tx, block.ScheduleId)
                    ?? throw new InvalidOperationException("This schedule block no longer exists. Another user may have deleted it.");

                ThrowIfProblems(FindProblems(conn, tx, block));

                int stranded = CountUncoveredBookings(conn, tx, existing, block);
                if (stranded > 0)
                {
                    throw new InvalidOperationException(DescribeStranded(stranded, existing, "changing"));
                }

                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "UPDATE doctor_schedules SET day_of_week = @dow, start_time = @start, end_time = @end, " +
                    "       consultation_minutes = @mins, notes = @notes " +
                    "WHERE schedule_id = @id AND doctor_id = @doc",
                    DatabaseHelper.Param("@dow", (int)block.Day),
                    DatabaseHelper.Param("@start", block.StartTime),
                    DatabaseHelper.Param("@end", block.EndTime),
                    DatabaseHelper.Param("@mins", block.ConsultationMinutes),
                    DatabaseHelper.Param("@notes", NullIfBlank(block.Notes)),
                    DatabaseHelper.Param("@id", block.ScheduleId),
                    DatabaseHelper.Param("@doc", block.DoctorId));
            });
        }

        /// <summary>Deletes a block. Refused while booked future appointments fall inside it.</summary>
        public void DeleteBlock(int scheduleId)
        {
            DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                ScheduleBlock existing = ReadBlock(conn, tx, scheduleId)
                    ?? throw new InvalidOperationException("This schedule block no longer exists. Another user may have deleted it.");

                LockDoctor(conn, tx, existing.DoctorId);

                int stranded = CountUncoveredBookings(conn, tx, existing, null);
                if (stranded > 0)
                {
                    throw new InvalidOperationException(DescribeStranded(stranded, existing, "removing"));
                }

                DatabaseHelper.ExecuteNonQuery(conn, tx, "DELETE FROM doctor_schedules WHERE schedule_id = @id",
                    DatabaseHelper.Param("@id", scheduleId));
            });
        }

        // ------------------------------------------------------------------
        // Leave dates
        // ------------------------------------------------------------------

        /// <summary>Leave dates of one doctor, soonest first, with a Past / Today / Upcoming status word.</summary>
        public DataTable GetLeaves(string doctorId)
        {
            return DatabaseHelper.ExecuteQuery(
                "SELECT leave_id, leave_date, DATE_FORMAT(leave_date, '%W') AS day_name, reason, " +
                "       CASE WHEN leave_date < CURDATE() THEN 'Past' " +
                "            WHEN leave_date = CURDATE() THEN 'Today' ELSE 'Upcoming' END AS leave_status " +
                "FROM doctor_leaves WHERE doctor_id = @id ORDER BY leave_date",
                DatabaseHelper.Param("@id", doctorId));
        }

        /// <summary>How many Booked appointments the doctor already has on a date (shown as a warning before marking leave).</summary>
        public int CountBookedAppointmentsOn(string doctorId, DateTime date)
        {
            long count = DatabaseHelper.ExecuteScalar<long>(
                "SELECT COUNT(*) FROM appointments WHERE doctor_id = @doc AND appointment_date = @d AND status = @booked",
                DatabaseHelper.Param("@doc", doctorId),
                DatabaseHelper.Param("@d", date.Date),
                DatabaseHelper.Param("@booked", StatusBooked));
            return (int)count;
        }

        /// <summary>True when the doctor already has a leave entry on this date.</summary>
        public bool LeaveExists(string doctorId, DateTime date)
        {
            long count = DatabaseHelper.ExecuteScalar<long>(
                "SELECT COUNT(*) FROM doctor_leaves WHERE doctor_id = @doc AND leave_date = @d",
                DatabaseHelper.Param("@doc", doctorId),
                DatabaseHelper.Param("@d", date.Date));
            return count > 0;
        }

        /// <summary>Marks a date as unavailable. One entry per doctor and date; past dates are refused.</summary>
        public void AddLeave(LeaveDate leave)
        {
            DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                LockDoctor(conn, tx, leave.DoctorId);

                if (leave.Date.Date < DateTime.Today)
                {
                    throw new InvalidOperationException("A leave date cannot be in the past.");
                }

                DataTable existing = DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT reason FROM doctor_leaves WHERE doctor_id = @doc AND leave_date = @d",
                    DatabaseHelper.Param("@doc", leave.DoctorId),
                    DatabaseHelper.Param("@d", leave.Date.Date));

                if (existing.Rows.Count > 0)
                {
                    throw new InvalidOperationException(
                        $"{leave.Date:MMM d, yyyy} is already marked as leave ({DatabaseHelper.GetString(existing.Rows[0], "reason")}). " +
                        "Each date can be entered only once per doctor.");
                }

                DatabaseHelper.ExecuteInsert(conn, tx,
                    "INSERT INTO doctor_leaves (doctor_id, leave_date, reason) VALUES (@doc, @d, @reason)",
                    DatabaseHelper.Param("@doc", leave.DoctorId),
                    DatabaseHelper.Param("@d", leave.Date.Date),
                    DatabaseHelper.Param("@reason", leave.Reason.Trim()));
            });
        }

        public void RemoveLeave(int leaveId)
        {
            int rows = DatabaseHelper.ExecuteNonQuery("DELETE FROM doctor_leaves WHERE leave_id = @id",
                DatabaseHelper.Param("@id", leaveId));

            if (rows == 0)
            {
                throw new InvalidOperationException("This leave date no longer exists. Another user may have removed it.");
            }
        }

        // ------------------------------------------------------------------
        // Rule checks
        // ------------------------------------------------------------------

        private List<ScheduleProblem> FindProblems(MySqlConnection conn, MySqlTransaction tx, ScheduleBlock b)
        {
            var problems = new List<ScheduleProblem>();

            bool timesOk = b.EndTime > b.StartTime;
            if (!timesOk)
            {
                problems.Add(new ScheduleProblem(ScheduleField.EndTime, "End Time must be after Start Time."));
            }

            int minMinutes = MinConsultationMinutes;
            if (b.ConsultationMinutes < minMinutes)
            {
                problems.Add(new ScheduleProblem(ScheduleField.Minutes,
                    $"Consultation Duration must be at least {minMinutes} minutes."));
            }
            else if (timesOk && b.ConsultationMinutes > b.DurationMinutes)
            {
                problems.Add(new ScheduleProblem(ScheduleField.Minutes,
                    $"Consultation Duration ({b.ConsultationMinutes} min) is longer than the block itself ({b.DurationMinutes} min)."));
            }

            if (!timesOk) return problems;   // overlap and hour totals mean nothing without a valid range

            DataTable others = DatabaseHelper.ExecuteQuery(conn, tx,
                "SELECT schedule_id, start_time, end_time FROM doctor_schedules " +
                "WHERE doctor_id = @doc AND day_of_week = @dow AND schedule_id <> @id ORDER BY start_time",
                DatabaseHelper.Param("@doc", b.DoctorId),
                DatabaseHelper.Param("@dow", (int)b.Day),
                DatabaseHelper.Param("@id", b.ScheduleId));

            int otherMinutes = 0;
            foreach (DataRow row in others.Rows)
            {
                TimeSpan otherStart = ReadTime(row["start_time"]);
                TimeSpan otherEnd = ReadTime(row["end_time"]);
                otherMinutes += (int)(otherEnd - otherStart).TotalMinutes;

                // Two ranges overlap when each starts before the other ends. Touching ends (12:00 and 12:00) are fine.
                if (b.StartTime < otherEnd && b.EndTime > otherStart)
                {
                    problems.Add(new ScheduleProblem(ScheduleField.StartTime,
                        $"{b.DayName} {b.TimeRangeText} overlaps the existing block " +
                        $"{ScheduleBlock.FormatTime(otherStart)} - {ScheduleBlock.FormatTime(otherEnd)}. " +
                        "Blocks for the same doctor and day cannot overlap."));
                }
            }

            decimal maxMinutes = MaxDailyHours * 60m;
            int totalMinutes = otherMinutes + b.DurationMinutes;
            if (totalMinutes > maxMinutes)
            {
                problems.Add(new ScheduleProblem(ScheduleField.EndTime,
                    $"Total scheduled time on {b.DayName} would be {totalMinutes / 60m:0.##} hours; " +
                    $"the maximum is {MaxDailyHours:0.##} hours per doctor per day."));
            }

            return problems;
        }

        private static void ThrowIfProblems(List<ScheduleProblem> problems)
        {
            if (problems.Count == 0) return;
            throw new InvalidOperationException(string.Join("\n", problems.Select(p => p.Message)));
        }

        /// <summary>
        /// Booked appointments (today or later) that sit inside the old block but would no longer be inside
        /// the new one (newBlock = null means the block is being removed).
        /// </summary>
        private static int CountUncoveredBookings(MySqlConnection conn, MySqlTransaction tx, ScheduleBlock oldBlock, ScheduleBlock? newBlock)
        {
            long count = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                "SELECT COUNT(*) FROM appointments " +
                "WHERE doctor_id = @doc AND status = @booked AND appointment_date >= CURDATE() " +
                "  AND DAYOFWEEK(appointment_date) - 1 = @odow " +
                "  AND time_slot >= @ostart AND time_slot < @oend " +
                "  AND NOT (@hasNew = 1 AND @ndow = @odow AND time_slot >= @nstart AND time_slot < @nend)",
                DatabaseHelper.Param("@doc", oldBlock.DoctorId),
                DatabaseHelper.Param("@booked", StatusBooked),
                DatabaseHelper.Param("@odow", (int)oldBlock.Day),
                DatabaseHelper.Param("@ostart", oldBlock.StartTime),
                DatabaseHelper.Param("@oend", oldBlock.EndTime),
                DatabaseHelper.Param("@hasNew", newBlock == null ? 0 : 1),
                DatabaseHelper.Param("@ndow", newBlock == null ? 0 : (int)newBlock.Day),
                DatabaseHelper.Param("@nstart", newBlock?.StartTime ?? TimeSpan.Zero),
                DatabaseHelper.Param("@nend", newBlock?.EndTime ?? TimeSpan.Zero));

            return (int)count;
        }

        private static string DescribeStranded(int count, ScheduleBlock oldBlock, string verb)
        {
            return $"{count} booked appointment(s) on or after today fall inside the {oldBlock.DayName} block " +
                   $"{oldBlock.TimeRangeText}, so {verb} it would leave them outside the doctor's working hours.\n\n" +
                   "Cancel or reschedule those appointments first (Appointment Booking), then try again.";
        }

        // ------------------------------------------------------------------
        // Helpers
        // ------------------------------------------------------------------

        /// <summary>Locks the doctor row for the rest of the transaction so schedule saves for one doctor happen one at a time.</summary>
        private static void LockDoctor(MySqlConnection conn, MySqlTransaction tx, string doctorId)
        {
            DataTable row = DatabaseHelper.ExecuteQuery(conn, tx,
                "SELECT doctor_id FROM doctors WHERE doctor_id = @id FOR UPDATE",
                DatabaseHelper.Param("@id", doctorId));

            if (row.Rows.Count == 0)
            {
                throw new InvalidOperationException("This doctor no longer exists. Another user may have deleted the record.");
            }
        }

        private static ScheduleBlock? ReadBlock(MySqlConnection conn, MySqlTransaction tx, int scheduleId)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(conn, tx,
                "SELECT schedule_id, doctor_id, day_of_week, start_time, end_time, consultation_minutes, notes " +
                "FROM doctor_schedules WHERE schedule_id = @id",
                DatabaseHelper.Param("@id", scheduleId));

            return table.Rows.Count == 0 ? null : MapBlock(table.Rows[0]);
        }

        private static ScheduleBlock MapBlock(DataRow row)
        {
            return new ScheduleBlock
            {
                ScheduleId = DatabaseHelper.GetInt(row, "schedule_id"),
                DoctorId = DatabaseHelper.GetString(row, "doctor_id"),
                Day = (DayOfWeek)DatabaseHelper.GetInt(row, "day_of_week"),
                StartTime = ReadTime(row["start_time"]),
                EndTime = ReadTime(row["end_time"]),
                ConsultationMinutes = DatabaseHelper.GetInt(row, "consultation_minutes"),
                Notes = DatabaseHelper.GetString(row, "notes")
            };
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

        private static string? NullIfBlank(string? text) => string.IsNullOrWhiteSpace(text) ? null : text.Trim();
    }
}