using System.Data;
using System.Text;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP03_DoctorScheduling;
using HospitalManagementSystem.Models.MP04_AppointmentBooking;
using HospitalManagementSystem.Models.MP06_ConsultationVitals;
using HospitalManagementSystem.Repositories.MP04_AppointmentBooking;
using MySqlConnector;

namespace HospitalManagementSystem.Repositories.MP06_ConsultationVitals
{
    /// <summary>
    /// All SQL for the Consultation &amp; Vital Signs recorder (MP06, Step 10).
    ///
    /// WHICH APPOINTMENTS CAN BE CONSULTED: today's appointments that are Booked or Completed (the queue board completes
    /// the appointment when the doctor finishes, and the note may be written a moment later) and that have NO consultation
    /// yet. Cancelled and No-show appointments, other dates and appointments that already have a consultation are never listed.
    ///
    /// ONE CONSULTATION PER APPOINTMENT: the picker hides appointments that already have one, Save checks again while the
    /// appointment row is locked (FOR UPDATE), and the database has a unique key on consultations.appointment_id.
    ///
    /// SAVE: the Consultation ID (CON-YYYYMMDD-XXX) comes from GeneratorHelper inside the save transaction and the time
    /// stamp (recorded_at) from the database clock. The numeric limits live in ConsultationRules and are repeated by
    /// the database's CHECK constraints.
    /// Saving a consultation does not change the appointment status (that is done by the queue board or Mark Completed).
    /// </summary>
    public class ConsultationRepository
    {
        private const int MaxHistoryRows = 300;

        private const string RecordSelect =
            "SELECT c.consultation_id, c.appointment_id, a.appointment_date, " +
            "       p.patient_id, CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " +
            "       TIMESTAMPDIFF(YEAR, p.date_of_birth, DATE(c.recorded_at)) AS age_at_visit, p.allergies, " +
            "       CONCAT(d.first_name, ' ', d.last_name) AS doctor_name, " +
            "       c.systolic_bp, c.diastolic_bp, c.temperature_c, c.pulse_rate_bpm, c.weight_kg, c.height_cm, " +
            "       c.chief_complaint, c.examination_findings, c.diagnosis, c.treatment_plan, c.special_instructions, " +
            "       c.next_follow_up_date, c.recorded_at, u.full_name AS recorded_by " +
            "FROM consultations c " +
            "JOIN appointments a ON a.appointment_id = c.appointment_id " +
            "JOIN patients p ON p.patient_id = a.patient_id " +
            "JOIN doctors d ON d.doctor_id = a.doctor_id " +
            "LEFT JOIN users u ON u.user_id = c.recorded_by_user_id ";

        // ------------------------------------------------------------------
        // Picker
        // ------------------------------------------------------------------

        public List<DoctorOption> GetDoctors() => new AppointmentRepository().GetDoctors(null);

        /// <summary>Today's Booked / Completed appointments without a consultation (patients being seen are listed first).</summary>
        public List<AppointmentChoice> GetPendingAppointments(string? doctorId)
        {
            var sql = new StringBuilder(
                "SELECT a.appointment_id, a.token_number, a.time_slot, a.reason_for_visit, a.status AS status_name, " +
                "       p.patient_id, CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " +
                "       TIMESTAMPDIFF(YEAR, p.date_of_birth, CURDATE()) AS age, p.allergies, " +
                "       a.doctor_id, CONCAT(d.first_name, ' ', d.last_name) AS doctor_name, " +
                "       (SELECT q.queue_status FROM queue_entries q " +
                "        WHERE q.appointment_id = a.appointment_id) AS queue_status, " +
                "       (SELECT COALESCE(MAX(q.queue_status = 'In Progress'), 0) FROM queue_entries q " +
                "        WHERE q.appointment_id = a.appointment_id) AS in_progress " +
                "FROM appointments a " +
                "JOIN patients p ON p.patient_id = a.patient_id " +
                "JOIN doctors d ON d.doctor_id = a.doctor_id " +
                "WHERE a.appointment_date = CURDATE() AND a.status IN ('Booked', 'Completed') " +
                "  AND NOT EXISTS (SELECT 1 FROM consultations c WHERE c.appointment_id = a.appointment_id) ");

            var parameters = new List<MySqlParameter>();
            if (!string.IsNullOrEmpty(doctorId))
            {
                sql.Append("AND a.doctor_id = @doctor ");
                parameters.Add(DatabaseHelper.Param("@doctor", doctorId));
            }
            sql.Append("ORDER BY in_progress DESC, a.time_slot, a.token_number");

            DataTable table = DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());
            return table.Rows.Cast<DataRow>().Select(row => new AppointmentChoice
            {
                AppointmentId = DatabaseHelper.GetString(row, "appointment_id"),
                TokenNumber = DatabaseHelper.GetInt(row, "token_number"),
                TimeSlot = ReadTime(row["time_slot"]),
                Reason = DatabaseHelper.GetString(row, "reason_for_visit"),
                StatusName = DatabaseHelper.GetString(row, "status_name"),
                PatientId = DatabaseHelper.GetString(row, "patient_id"),
                PatientName = DatabaseHelper.GetString(row, "patient_name"),
                Age = DatabaseHelper.GetInt(row, "age"),
                Allergies = DatabaseHelper.GetString(row, "allergies"),
                DoctorId = DatabaseHelper.GetString(row, "doctor_id"),
                DoctorName = DatabaseHelper.GetString(row, "doctor_name"),
                QueueStatus = DatabaseHelper.GetString(row, "queue_status")
            }).ToList();
        }

        // ------------------------------------------------------------------
        // Save
        // ------------------------------------------------------------------

        /// <summary>
        /// Every rule of the input (ranges, mandatory fields, lengths). Returns the problems in plain words (empty = valid).
        /// The form shows the same rules next to the fields; this repeats them for the database.
        /// </summary>
        public static List<string> Validate(ConsultationInput input)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(input.AppointmentId)) errors.Add("Select an appointment first.");

            if (input.SystolicBp < ConsultationRules.SystolicMin || input.SystolicBp > ConsultationRules.SystolicMax)
                errors.Add($"Systolic BP must be between {ConsultationRules.SystolicMin} and {ConsultationRules.SystolicMax} mmHg.");
            if (input.DiastolicBp < ConsultationRules.DiastolicMin || input.DiastolicBp > ConsultationRules.DiastolicMax)
                errors.Add($"Diastolic BP must be between {ConsultationRules.DiastolicMin} and {ConsultationRules.DiastolicMax} mmHg.");
            if (input.DiastolicBp >= input.SystolicBp)
                errors.Add("Diastolic BP must be lower than systolic BP.");
            if (input.TemperatureC < ConsultationRules.TemperatureMin || input.TemperatureC > ConsultationRules.TemperatureMax)
                errors.Add($"Temperature must be between {ConsultationRules.TemperatureMin:0} and {ConsultationRules.TemperatureMax:0} \u00B0C.");
            if (input.PulseRateBpm < ConsultationRules.PulseMin || input.PulseRateBpm > ConsultationRules.PulseMax)
                errors.Add($"Pulse must be between {ConsultationRules.PulseMin} and {ConsultationRules.PulseMax} bpm.");
            if (input.WeightKg <= 0 || input.WeightKg > ConsultationRules.WeightMax)
                errors.Add($"Weight must be greater than 0 and at most {ConsultationRules.WeightMax:0} kg.");
            if (input.HeightCm.HasValue && (input.HeightCm.Value < ConsultationRules.HeightMin || input.HeightCm.Value > ConsultationRules.HeightMax))
                errors.Add($"Height must be between {ConsultationRules.HeightMin:0} and {ConsultationRules.HeightMax:0} cm (or left empty).");

            if (string.IsNullOrWhiteSpace(input.ChiefComplaint)) errors.Add("Enter the chief complaint.");
            if (string.IsNullOrWhiteSpace(input.Diagnosis)) errors.Add("Enter at least one diagnosis before saving.");

            if (input.ChiefComplaint.Trim().Length > ConsultationRules.MaxComplaint) errors.Add($"Chief complaint can have at most {ConsultationRules.MaxComplaint} characters.");
            if (input.ExaminationFindings.Trim().Length > ConsultationRules.MaxFindings) errors.Add($"Examination findings can have at most {ConsultationRules.MaxFindings} characters.");
            if (input.Diagnosis.Trim().Length > ConsultationRules.MaxDiagnosis) errors.Add($"Diagnosis can have at most {ConsultationRules.MaxDiagnosis} characters.");
            if (input.TreatmentPlan.Trim().Length > ConsultationRules.MaxPlan) errors.Add($"Treatment plan can have at most {ConsultationRules.MaxPlan} characters.");
            if (input.SpecialInstructions.Trim().Length > ConsultationRules.MaxInstructions) errors.Add($"Special instructions can have at most {ConsultationRules.MaxInstructions} characters.");

            if (input.NextFollowUpDate.HasValue && input.NextFollowUpDate.Value.Date <= DateTime.Today)
                errors.Add("The next follow-up date must be after today.");

            return errors;
        }

        /// <summary>Saves the consultation, generating its id and time stamp, and returns the stored record.</summary>
        public ConsultationRecord Save(ConsultationInput input)
        {
            List<string> errors = Validate(input);
            if (errors.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, errors));

            return DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                DataTable found = DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT appointment_date, status FROM appointments WHERE appointment_id = @id FOR UPDATE",
                    DatabaseHelper.Param("@id", input.AppointmentId));
                if (found.Rows.Count == 0)
                    throw new InvalidOperationException("This appointment no longer exists. The list will be refreshed.");

                string appointmentCode = input.AppointmentId;
                string status = DatabaseHelper.GetString(found.Rows[0], "status");
                DateTime date = DatabaseHelper.GetNullableDate(found.Rows[0], "appointment_date") ?? DateTime.MinValue;

                if (date.Date != DateTime.Today)
                    throw new InvalidOperationException($"{appointmentCode} is not an appointment of today, so it cannot be consulted here.");
                if (status != AppointmentStatus.Booked && status != AppointmentStatus.Completed)
                    throw new InvalidOperationException(
                        $"{appointmentCode} is {status}, so no consultation can be recorded for it.");

                object? existing = DatabaseHelper.ExecuteScalar(conn, tx,
                    "SELECT consultation_id FROM consultations WHERE appointment_id = @id",
                    DatabaseHelper.Param("@id", input.AppointmentId));
                if (existing != null && existing != DBNull.Value)
                    throw new InvalidOperationException(
                        $"{appointmentCode} already has consultation {existing}. An appointment can only be consulted once. The list will be refreshed.");

                string consultationId = GeneratorHelper.Generate(GeneratorHelper.Prefix.Consultation, conn, tx);

                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "INSERT INTO consultations (consultation_id, appointment_id, systolic_bp, diastolic_bp, temperature_c, " +
                    "  pulse_rate_bpm, weight_kg, height_cm, chief_complaint, examination_findings, diagnosis, treatment_plan, " +
                    "  special_instructions, next_follow_up_date, recorded_at, recorded_by_user_id) " +
                    "VALUES (@id, @appointment, @sys, @dia, @temp, @pulse, @weight, @height, @complaint, @findings, @diagnosis, " +
                    "  @plan, @instructions, @followup, NOW(), @user)",
                    DatabaseHelper.Param("@id", consultationId),
                    DatabaseHelper.Param("@appointment", input.AppointmentId),
                    DatabaseHelper.Param("@sys", input.SystolicBp),
                    DatabaseHelper.Param("@dia", input.DiastolicBp),
                    DatabaseHelper.Param("@temp", Math.Round(input.TemperatureC, 1)),
                    DatabaseHelper.Param("@pulse", input.PulseRateBpm),
                    DatabaseHelper.Param("@weight", Math.Round(input.WeightKg, 1)),
                    DatabaseHelper.Param("@height", input.HeightCm.HasValue ? Math.Round(input.HeightCm.Value, 1) : (decimal?)null),
                    DatabaseHelper.Param("@complaint", input.ChiefComplaint.Trim()),
                    DatabaseHelper.Param("@findings", NullIfBlank(input.ExaminationFindings)),
                    DatabaseHelper.Param("@diagnosis", input.Diagnosis.Trim()),
                    DatabaseHelper.Param("@plan", NullIfBlank(input.TreatmentPlan)),
                    DatabaseHelper.Param("@instructions", NullIfBlank(input.SpecialInstructions)),
                    DatabaseHelper.Param("@followup", input.NextFollowUpDate?.Date),
                    DatabaseHelper.Param("@user", SessionManager.CurrentUserId > 0 ? SessionManager.CurrentUserId : (int?)null));

                DataTable saved = DatabaseHelper.ExecuteQuery(conn, tx,
                    RecordSelect + "WHERE c.consultation_id = @id",
                    DatabaseHelper.Param("@id", consultationId));
                return ReadRecord(saved.Rows[0]);
            });
        }

        // ------------------------------------------------------------------
        // History
        // ------------------------------------------------------------------

        /// <summary>Consultations recorded so far, newest first, optionally filtered by patient name or ID (every word must match).</summary>
        public List<ConsultationRecord> GetHistory(string? patientText)
        {
            var sql = new StringBuilder(RecordSelect + "WHERE 1 = 1");
            var parameters = new List<MySqlParameter>();

            string[] words = (patientText ?? string.Empty).Split(new[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                sql.Append($" AND (p.first_name LIKE @w{i} OR p.middle_name LIKE @w{i} OR p.last_name LIKE @w{i} OR p.patient_id LIKE @w{i})");
                parameters.Add(DatabaseHelper.Param($"@w{i}", "%" + EscapeLike(words[i]) + "%"));
            }

            sql.Append($" ORDER BY c.recorded_at DESC, c.consultation_id DESC LIMIT {MaxHistoryRows}");
            DataTable table = DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());
            return table.Rows.Cast<DataRow>().Select(ReadRecord).ToList();
        }

        // ------------------------------------------------------------------
        // Helpers
        // ------------------------------------------------------------------

        private static ConsultationRecord ReadRecord(DataRow row)
        {
            return new ConsultationRecord
            {
                ConsultationId = DatabaseHelper.GetString(row, "consultation_id"),
                AppointmentId = DatabaseHelper.GetString(row, "appointment_id"),
                AppointmentDate = DatabaseHelper.GetNullableDate(row, "appointment_date") ?? DateTime.MinValue,
                PatientId = DatabaseHelper.GetString(row, "patient_id"),
                PatientName = DatabaseHelper.GetString(row, "patient_name"),
                AgeAtVisit = DatabaseHelper.GetInt(row, "age_at_visit"),
                Allergies = DatabaseHelper.GetString(row, "allergies"),
                DoctorName = DatabaseHelper.GetString(row, "doctor_name"),
                SystolicBp = DatabaseHelper.GetInt(row, "systolic_bp"),
                DiastolicBp = DatabaseHelper.GetInt(row, "diastolic_bp"),
                TemperatureC = DatabaseHelper.GetDecimal(row, "temperature_c"),
                PulseRateBpm = DatabaseHelper.GetInt(row, "pulse_rate_bpm"),
                WeightKg = DatabaseHelper.GetDecimal(row, "weight_kg"),
                HeightCm = row["height_cm"] == DBNull.Value ? null : DatabaseHelper.GetDecimal(row, "height_cm"),
                ChiefComplaint = DatabaseHelper.GetString(row, "chief_complaint"),
                ExaminationFindings = DatabaseHelper.GetString(row, "examination_findings"),
                Diagnosis = DatabaseHelper.GetString(row, "diagnosis"),
                TreatmentPlan = DatabaseHelper.GetString(row, "treatment_plan"),
                SpecialInstructions = DatabaseHelper.GetString(row, "special_instructions"),
                NextFollowUpDate = DatabaseHelper.GetNullableDate(row, "next_follow_up_date"),
                RecordedAt = DatabaseHelper.GetNullableDate(row, "recorded_at") ?? DateTime.Now,
                RecordedBy = DatabaseHelper.GetString(row, "recorded_by")
            };
        }

        private static string? NullIfBlank(string text) => string.IsNullOrWhiteSpace(text) ? null : text.Trim();

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