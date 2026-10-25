using System.Data;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP04_AppointmentBooking;
using HospitalManagementSystem.Models.MP05_QueueBoard;
using HospitalManagementSystem.Repositories.MP04_AppointmentBooking;
using MySqlConnector;

namespace HospitalManagementSystem.Repositories.MP05_QueueBoard
{
    /// <summary>
    /// All SQL for the Patient Queue Board (MP05, Step 9). One board = one doctor, today.
    ///
    /// CALL ORDER of the waiting patients (the same ORDER BY is used for the list on screen and for Call Next):
    ///   1. Emergency patients first (is_emergency = 1);
    ///   2. then patients who have NOT been skipped before the ones who were (fewer rows in queue_skip_log first);
    ///   3. then the patient skipped longest ago first, so the most recently skipped patient sits at the very back;
    ///   4. then the lowest token number (FIFO), then the check-in order.
    ///
    /// SKIP BEHAVIOUR (documented choice): the current patient gets one row in queue_skip_log (with the reason) and goes
    /// back to Waiting behind everybody who has not been skipped. Nobody is removed from the queue.
    /// An emergency patient who is skipped stays ahead of normal patients (emergency always wins) but behind
    /// emergency patients who have not been skipped.
    ///
    /// ESTIMATED WAIT = position in the waiting list x the doctor's average consultation minutes of today
    /// (average of completed_at - started_at over today's completed visits; the setting default_consultation_minutes
    /// until the first visit is completed).
    ///
    /// Concurrency: every change locks the doctor row first (SELECT ... FOR UPDATE), so two clerks can never call two
    /// patients for the same doctor at once. The unique key uq_queue_one_in_progress is the second safety net.
    /// Time stamps come from the database (NOW(), CURDATE()), the same clock the seed data uses.
    /// </summary>
    public class QueueRepository
    {
        private const int MaxReasonLength = 250;

        private const string EntrySelect =
            "SELECT q.queue_id, q.token_number, q.patient_id, " +
            "       CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " +
            "       q.doctor_id, CONCAT(d.first_name, ' ', d.last_name) AS doctor_name, " +
            "       q.appointment_id, q.queue_status, q.is_emergency, " +
            "       q.checked_in_at, q.started_at, q.completed_at, " +
            "       (SELECT COUNT(*) FROM queue_skip_log s WHERE s.queue_id = q.queue_id) AS skip_count, " +
            "       (SELECT MAX(s.skipped_at) FROM queue_skip_log s WHERE s.queue_id = q.queue_id) AS last_skip, " +
            "       (SELECT a.reason_for_visit FROM appointments a WHERE a.appointment_id = q.appointment_id) AS visit_reason " +
            "FROM queue_entries q " +
            "JOIN patients p ON p.patient_id = q.patient_id " +
            "JOIN doctors d ON d.doctor_id = q.doctor_id ";

        private const string CallOrder =
            "ORDER BY q.is_emergency DESC, skip_count ASC, last_skip ASC, q.token_number ASC, q.queue_id ASC";

        /// <summary>Average consultation time used before any visit has been completed today.</summary>
        public static int DefaultAverageMinutes =>
            Math.Max(1, SettingsHelper.GetInt(SettingsHelper.DefaultConsultationMinutes, 15));

        // ------------------------------------------------------------------
        // Board
        // ------------------------------------------------------------------

        public List<DoctorOption> GetDoctors() => new AppointmentRepository().GetDoctors(null);

        /// <summary>The doctor with the most active (Waiting / In Progress) patients today, or null when nobody is queued.</summary>
        public string? GetBusiestDoctorId()
        {
            object? value = DatabaseHelper.ExecuteScalar(
                "SELECT doctor_id FROM queue_entries " +
                "WHERE queue_date = CURDATE() AND queue_status IN ('Waiting', 'In Progress') " +
                "GROUP BY doctor_id ORDER BY COUNT(*) DESC, doctor_id LIMIT 1");
            return value == null || value == DBNull.Value ? null : Convert.ToString(value);
        }

        /// <summary>Loads today's board of one doctor: now serving, waiting list in call order and the stats.</summary>
        public QueueBoard LoadBoard(string doctorId)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(
                EntrySelect + "WHERE q.queue_date = CURDATE() AND q.doctor_id = @doctor " + CallOrder,
                DatabaseHelper.Param("@doctor", doctorId));

            var board = new QueueBoard();
            int completed = 0;

            foreach (DataRow row in table.Rows)
            {
                QueueEntryRecord entry = ReadEntry(row);
                switch (entry.Status)
                {
                    case QueueStatus.InProgress:
                        board.NowServing = entry;
                        break;
                    case QueueStatus.Completed:
                        completed++;
                        break;
                    default:                                   // Waiting (a skipped patient is Waiting again)
                        board.Waiting.Add(entry);
                        break;
                }
            }

            (double average, bool isDefault) = LoadAverage(null, null, doctorId);

            for (int i = 0; i < board.Waiting.Count; i++)
            {
                QueueEntryRecord entry = board.Waiting[i];
                entry.Position = i + 1;
                entry.EstimatedWaitMinutes = Math.Max(1, (int)Math.Round(entry.Position * average, MidpointRounding.AwayFromZero));
            }

            board.Stats = new QueueStats
            {
                Completed = completed,
                Waiting = board.Waiting.Count,
                EmergenciesWaiting = board.Waiting.Count(w => w.IsEmergency),
                AverageMinutes = average,
                AverageIsDefault = isDefault
            };
            return board;
        }

        /// <summary>Today's Booked appointments of the doctor that have not checked in yet.</summary>
        public List<CheckInCandidate> GetCheckInCandidates(string doctorId)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(
                "SELECT a.appointment_id, a.token_number, a.time_slot, a.patient_id, " +
                "       CONCAT(p.first_name, ' ', p.last_name) AS patient_name " +
                "FROM appointments a JOIN patients p ON p.patient_id = a.patient_id " +
                "WHERE a.doctor_id = @doctor AND a.appointment_date = CURDATE() AND a.status = 'Booked' " +
                "  AND NOT EXISTS (SELECT 1 FROM queue_entries q WHERE q.appointment_id = a.appointment_id) " +
                "ORDER BY a.time_slot, a.token_number",
                DatabaseHelper.Param("@doctor", doctorId));

            return table.Rows.Cast<DataRow>().Select(row => new CheckInCandidate
            {
                AppointmentId = DatabaseHelper.GetString(row, "appointment_id"),
                TokenNumber = DatabaseHelper.GetInt(row, "token_number"),
                PatientId = DatabaseHelper.GetString(row, "patient_id"),
                PatientName = DatabaseHelper.GetString(row, "patient_name"),
                TimeSlot = ReadTime(row["time_slot"])
            }).ToList();
        }

        /// <summary>Patient search for the walk-in dialog (same search as the appointment screen).</summary>
        public DataTable SearchPatients(string text) => new AppointmentRepository().SearchPatients(text);

        // ------------------------------------------------------------------
        // Check-in
        // ------------------------------------------------------------------

        /// <summary>
        /// Puts today's Booked appointment on the queue. The queue token is the appointment's token when it is still
        /// free for this doctor today, otherwise the next free number. An emergency flag moves the patient to the front.
        /// </summary>
        public QueueEntryRecord CheckInAppointment(string appointmentId, bool isEmergency)
        {
            return DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                DataTable found = DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT doctor_id, patient_id, appointment_date, status, token_number " +
                    "FROM appointments WHERE appointment_id = @id",
                    DatabaseHelper.Param("@id", appointmentId));
                if (found.Rows.Count == 0)
                    throw new InvalidOperationException("This appointment no longer exists. The list will be refreshed.");

                string doctorId = DatabaseHelper.GetString(found.Rows[0], "doctor_id");
                LockDoctor(conn, tx, doctorId);

                // Re-read under the doctor lock (the status may have changed a moment ago).
                found = DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT patient_id, appointment_date, status, token_number " +
                    "FROM appointments WHERE appointment_id = @id FOR UPDATE",
                    DatabaseHelper.Param("@id", appointmentId));
                if (found.Rows.Count == 0)
                    throw new InvalidOperationException("This appointment no longer exists. The list will be refreshed.");

                DataRow row = found.Rows[0];
                string code = appointmentId;
                string patientId = DatabaseHelper.GetString(row, "patient_id");
                string status = DatabaseHelper.GetString(row, "status");
                int appointmentToken = DatabaseHelper.GetInt(row, "token_number");
                DateTime date = DatabaseHelper.GetNullableDate(row, "appointment_date") ?? DateTime.MinValue;

                if (date.Date != DateTime.Today)
                    throw new InvalidOperationException($"{code} is not for today, so the patient cannot check in on today's queue.");
                if (status != AppointmentStatus.Booked)
                    throw new InvalidOperationException($"{code} is no longer Booked, so the patient cannot check in.");

                long already = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                    "SELECT COUNT(*) FROM queue_entries WHERE appointment_id = @id",
                    DatabaseHelper.Param("@id", appointmentId));
                if (already > 0)
                    throw new InvalidOperationException($"{code} has already checked in. The list will be refreshed.");

                EnsureNotActiveInQueue(conn, tx, doctorId, patientId);

                bool tokenFree = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                    "SELECT COUNT(*) FROM queue_entries WHERE doctor_id = @doctor AND queue_date = CURDATE() AND token_number = @token",
                    DatabaseHelper.Param("@doctor", doctorId), DatabaseHelper.Param("@token", appointmentToken)) == 0;
                int token = tokenFree ? appointmentToken : NextFreeToken(conn, tx, doctorId);

                long queueId = InsertEntry(conn, tx, doctorId, patientId, appointmentId, token, isEmergency);
                return LoadEntry(conn, tx, (int)queueId)!;
            });
        }

        /// <summary>Adds a patient without an appointment (walk-in or emergency) to the doctor's queue with the next free token.</summary>
        public QueueEntryRecord CheckInWalkIn(string patientId, string doctorId, bool isEmergency)
        {
            return DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                LockDoctor(conn, tx, doctorId);

                long patientExists = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                    "SELECT COUNT(*) FROM patients WHERE patient_id = @id",
                    DatabaseHelper.Param("@id", patientId));
                if (patientExists == 0)
                    throw new InvalidOperationException("This patient no longer exists. Please search for the patient again.");

                EnsureNotActiveInQueue(conn, tx, doctorId, patientId);

                // A patient who still holds a Booked appointment today must check in through it (keeps one visit = one record).
                DataTable open = DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT a.appointment_id FROM appointments a " +
                    "WHERE a.patient_id = @patient AND a.doctor_id = @doctor AND a.appointment_date = CURDATE() AND a.status = 'Booked' " +
                    "  AND NOT EXISTS (SELECT 1 FROM queue_entries q WHERE q.appointment_id = a.appointment_id) LIMIT 1",
                    DatabaseHelper.Param("@patient", patientId), DatabaseHelper.Param("@doctor", doctorId));
                if (open.Rows.Count > 0)
                    throw new InvalidOperationException(
                        $"This patient has a Booked appointment today ({DatabaseHelper.GetString(open.Rows[0], "appointment_id")}). " +
                        "Check in that appointment from the list instead of adding a walk-in.");

                int token = NextFreeToken(conn, tx, doctorId);
                long queueId = InsertEntry(conn, tx, doctorId, patientId, null, token, isEmergency);
                return LoadEntry(conn, tx, (int)queueId)!;
            });
        }

        // ------------------------------------------------------------------
        // Call Next / Skip / Mark Complete
        // ------------------------------------------------------------------

        /// <summary>
        /// Calls the next waiting patient (emergency first, otherwise the call order above) and starts the consultation clock.
        /// Refused while the doctor is still seeing somebody (only one In Progress per doctor).
        /// </summary>
        public QueueEntryRecord CallNext(string doctorId)
        {
            return DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                LockDoctor(conn, tx, doctorId);

                DataTable current = DatabaseHelper.ExecuteQuery(conn, tx,
                    EntrySelect + "WHERE q.queue_date = CURDATE() AND q.doctor_id = @doctor AND q.queue_status = 'In Progress'",
                    DatabaseHelper.Param("@doctor", doctorId));
                if (current.Rows.Count > 0)
                {
                    QueueEntryRecord busy = ReadEntry(current.Rows[0]);
                    throw new InvalidOperationException(
                        $"Dr. {busy.DoctorName} is already seeing token {busy.TokenText} ({busy.PatientName}). " +
                        "Mark that consultation Complete or Skip the patient first.");
                }

                DataTable next = DatabaseHelper.ExecuteQuery(conn, tx,
                    EntrySelect + "WHERE q.queue_date = CURDATE() AND q.doctor_id = @doctor AND q.queue_status = 'Waiting' " + CallOrder + " LIMIT 1",
                    DatabaseHelper.Param("@doctor", doctorId));
                if (next.Rows.Count == 0)
                    throw new InvalidOperationException("Nobody is waiting for this doctor.");

                int queueId = DatabaseHelper.GetInt(next.Rows[0], "queue_id");
                int changed = DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "UPDATE queue_entries SET queue_status = 'In Progress', started_at = NOW() WHERE queue_id = @id AND queue_status = 'Waiting'",
                    DatabaseHelper.Param("@id", queueId));
                if (changed == 0)
                    throw new InvalidOperationException("That patient was just called by someone else. The board will be refreshed.");

                return LoadEntry(conn, tx, queueId)!;
            });
        }

        /// <summary>
        /// Skips the patient who is In Progress: the reason is logged, the patient returns to Waiting behind everybody who
        /// has not been skipped, and the consultation clock is cleared.
        /// </summary>
        public QueueEntryRecord Skip(int queueId, string reason)
        {
            reason = (reason ?? string.Empty).Trim();
            if (reason.Length == 0)
                throw new InvalidOperationException("Enter the reason for skipping the patient.");
            if (reason.Length > MaxReasonLength)
                throw new InvalidOperationException($"The reason can have at most {MaxReasonLength} characters.");

            return DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                QueueEntryRecord entry = LoadInProgressUnderLock(conn, tx, queueId, "skipped");

                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "INSERT INTO queue_skip_log (queue_id, reason, skipped_at, skipped_by_user_id) VALUES (@id, @reason, NOW(), @user)",
                    DatabaseHelper.Param("@id", entry.QueueId),
                    DatabaseHelper.Param("@reason", reason),
                    DatabaseHelper.Param("@user", SessionManager.CurrentUserId > 0 ? SessionManager.CurrentUserId : (int?)null));

                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "UPDATE queue_entries SET queue_status = 'Waiting', started_at = NULL WHERE queue_id = @id AND queue_status = 'In Progress'",
                    DatabaseHelper.Param("@id", entry.QueueId));

                return LoadEntry(conn, tx, entry.QueueId)!;
            });
        }

        /// <summary>
        /// Finishes the consultation of the patient who is In Progress: stores the completion time (the actual time taken is
        /// completed_at - started_at) and marks the linked appointment Completed.
        /// </summary>
        public CompleteResult Complete(int queueId)
        {
            return DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                QueueEntryRecord entry = LoadInProgressUnderLock(conn, tx, queueId, "completed");

                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "UPDATE queue_entries SET queue_status = 'Completed', completed_at = NOW() WHERE queue_id = @id AND queue_status = 'In Progress'",
                    DatabaseHelper.Param("@id", entry.QueueId));

                if (entry.AppointmentId != null)
                {
                    DatabaseHelper.ExecuteNonQuery(conn, tx,
                        "UPDATE appointments SET status = 'Completed', status_updated_at = NOW() WHERE appointment_id = @id AND status = 'Booked'",
                        DatabaseHelper.Param("@id", entry.AppointmentId));
                }

                QueueEntryRecord done = LoadEntry(conn, tx, entry.QueueId)!;
                double minutes = done.StartedAt.HasValue && done.CompletedAt.HasValue
                    ? Math.Max(0, (done.CompletedAt.Value - done.StartedAt.Value).TotalMinutes)
                    : 0;

                (double average, _) = LoadAverage(conn, tx, done.DoctorId);
                return new CompleteResult { Entry = done, MinutesTaken = minutes, NewAverageMinutes = average };
            });
        }

        // ------------------------------------------------------------------
        // Helpers
        // ------------------------------------------------------------------

        /// <summary>Locks the doctor row so that only one clerk changes this doctor's queue at a time.</summary>
        private static void LockDoctor(MySqlConnection conn, MySqlTransaction tx, string doctorId)
        {
            DataTable doctor = DatabaseHelper.ExecuteQuery(conn, tx,
                "SELECT doctor_id FROM doctors WHERE doctor_id = @id FOR UPDATE",
                DatabaseHelper.Param("@id", doctorId));
            if (doctor.Rows.Count == 0)
                throw new InvalidOperationException("This doctor no longer exists. Please pick the doctor again.");
        }

        private static QueueEntryRecord LoadInProgressUnderLock(MySqlConnection conn, MySqlTransaction tx, int queueId, string verb)
        {
            QueueEntryRecord? preview = LoadEntry(conn, tx, queueId);
            if (preview == null)
                throw new InvalidOperationException("This queue entry no longer exists. The board will be refreshed.");

            LockDoctor(conn, tx, preview.DoctorId);

            QueueEntryRecord? entry = LoadEntry(conn, tx, queueId);
            if (entry == null || entry.Status != QueueStatus.InProgress)
                throw new InvalidOperationException(
                    $"Token {preview.TokenText} ({preview.PatientName}) is not being seen right now, so it cannot be {verb}. The board will be refreshed.");
            return entry;
        }

        private static void EnsureNotActiveInQueue(MySqlConnection conn, MySqlTransaction tx, string doctorId, string patientId)
        {
            DataTable active = DatabaseHelper.ExecuteQuery(conn, tx,
                EntrySelect + "WHERE q.queue_date = CURDATE() AND q.doctor_id = @doctor AND q.patient_id = @patient AND q.queue_status IN ('Waiting', 'In Progress') LIMIT 1",
                DatabaseHelper.Param("@doctor", doctorId), DatabaseHelper.Param("@patient", patientId));
            if (active.Rows.Count > 0)
            {
                QueueEntryRecord e = ReadEntry(active.Rows[0]);
                throw new InvalidOperationException($"{e.PatientName} is already on this doctor's queue today (token {e.TokenText}).");
            }
        }

        /// <summary>Next token number that no queue entry and no appointment of this doctor uses today.</summary>
        private static int NextFreeToken(MySqlConnection conn, MySqlTransaction tx, string doctorId)
        {
            DataTable max = DatabaseHelper.ExecuteQuery(conn, tx,
                "SELECT GREATEST(" +
                "  COALESCE((SELECT MAX(token_number) FROM queue_entries WHERE doctor_id = @doctor AND queue_date = CURDATE()), 0), " +
                "  COALESCE((SELECT MAX(token_number) FROM appointments WHERE doctor_id = @doctor AND appointment_date = CURDATE()), 0)" +
                ") AS max_token",
                DatabaseHelper.Param("@doctor", doctorId));
            return DatabaseHelper.GetInt(max.Rows[0], "max_token") + 1;
        }

        private static long InsertEntry(MySqlConnection conn, MySqlTransaction tx, string doctorId, string patientId,
                                        string? appointmentId, int token, bool isEmergency)
        {
            return DatabaseHelper.ExecuteInsert(conn, tx,
                "INSERT INTO queue_entries (queue_date, token_number, patient_id, doctor_id, appointment_id, queue_status, is_emergency, checked_in_at) " +
                "VALUES (CURDATE(), @token, @patient, @doctor, @appointment, 'Waiting', @emergency, NOW())",
                DatabaseHelper.Param("@token", token),
                DatabaseHelper.Param("@patient", patientId),
                DatabaseHelper.Param("@doctor", doctorId),
                DatabaseHelper.Param("@appointment", appointmentId),
                DatabaseHelper.Param("@emergency", isEmergency ? 1 : 0));
        }

        private static QueueEntryRecord? LoadEntry(MySqlConnection conn, MySqlTransaction tx, int queueId)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(conn, tx,
                EntrySelect + "WHERE q.queue_id = @id",
                DatabaseHelper.Param("@id", queueId));
            return table.Rows.Count == 0 ? null : ReadEntry(table.Rows[0]);
        }

        /// <summary>Average minutes of today's completed visits of the doctor; (default, true) when there are none yet.</summary>
        private static (double Minutes, bool IsDefault) LoadAverage(MySqlConnection? conn, MySqlTransaction? tx, string doctorId)
        {
            const string sql =
                "SELECT AVG(TIMESTAMPDIFF(SECOND, started_at, completed_at)) AS avg_seconds " +
                "FROM queue_entries " +
                "WHERE queue_date = CURDATE() AND doctor_id = @doctor AND queue_status = 'Completed' " +
                "  AND started_at IS NOT NULL AND completed_at IS NOT NULL";

            DataTable table = conn == null
                ? DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.Param("@doctor", doctorId))
                : DatabaseHelper.ExecuteQuery(conn, tx, sql, DatabaseHelper.Param("@doctor", doctorId));

            object value = table.Rows[0]["avg_seconds"];
            if (value == DBNull.Value) return (DefaultAverageMinutes, true);

            double minutes = Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture) / 60.0;
            return minutes <= 0 ? (DefaultAverageMinutes, true) : (minutes, false);
        }

        private static QueueEntryRecord ReadEntry(DataRow row)
        {
            return new QueueEntryRecord
            {
                QueueId = DatabaseHelper.GetInt(row, "queue_id"),
                TokenNumber = DatabaseHelper.GetInt(row, "token_number"),
                PatientId = DatabaseHelper.GetString(row, "patient_id"),
                PatientName = DatabaseHelper.GetString(row, "patient_name"),
                DoctorId = DatabaseHelper.GetString(row, "doctor_id"),
                DoctorName = DatabaseHelper.GetString(row, "doctor_name"),
                AppointmentId = row["appointment_id"] == DBNull.Value ? null : DatabaseHelper.GetString(row, "appointment_id"),
                Status = DatabaseHelper.GetString(row, "queue_status"),
                IsEmergency = DatabaseHelper.GetBool(row, "is_emergency"),
                CheckedInAt = DatabaseHelper.GetNullableDate(row, "checked_in_at") ?? DateTime.Now,
                StartedAt = DatabaseHelper.GetNullableDate(row, "started_at"),
                CompletedAt = DatabaseHelper.GetNullableDate(row, "completed_at"),
                SkipCount = DatabaseHelper.GetInt(row, "skip_count"),
                VisitReason = row.Table.Columns.Contains("visit_reason") ? DatabaseHelper.GetString(row, "visit_reason") : string.Empty
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
    }
}