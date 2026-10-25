using System.Data;
using System.Globalization;
using System.Text;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP07_Prescription;
using MySqlConnector;

namespace HospitalManagementSystem.Repositories.MP07_Prescription
{
    /// <summary>
    /// All SQL for the Prescription Builder (MP07, Step 11).
    ///
    /// QUANTITY: always frequency per day x duration in days. The database computes it (generated column
    /// prescription_items.quantity); the form shows the same product while the doctor types.
    ///
    /// STOCK WARNING (never blocks): the stock of a medicine is the view v_medicine_stock.usable_stock, i.e. the remaining
    /// quantity of all batches that have not expired (batch received + stock movements). If a line asks for more than that,
    /// the form warns and asks for confirmation. Quantities of prescriptions that were issued but not dispensed yet are
    /// not reserved; the pharmacist's dispensing screen (MP09) is the real stock check.
    ///
    /// SAVE: the Prescription ID (PRE-YYYYMMDD-XXX) comes from GeneratorHelper inside the save transaction; the header
    /// and all lines are saved together or not at all. The issue date is the database date.
    /// A consultation may have more than one prescription (the schema allows it, e.g. a follow-up prescription).
    /// </summary>
    public class PrescriptionRepository
    {
        private const string RecordSelect =
            "SELECT r.prescription_id, r.consultation_id, p.patient_id, " +
            "       CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " +
            "       TIMESTAMPDIFF(YEAR, p.date_of_birth, r.issued_date) AS age, p.allergies, " +
            "       CONCAT(d.first_name, ' ', d.last_name) AS doctor_name, c.diagnosis, r.general_instructions, " +
            "       r.issued_date, r.valid_until, u.full_name AS created_by, " +
            "       (SELECT COUNT(*) FROM prescription_items i WHERE i.prescription_id = r.prescription_id) AS line_count " +
            "FROM prescriptions r " +
            "JOIN consultations c ON c.consultation_id = r.consultation_id " +
            "JOIN appointments a ON a.appointment_id = c.appointment_id " +
            "JOIN patients p ON p.patient_id = a.patient_id " +
            "JOIN doctors d ON d.doctor_id = a.doctor_id " +
            "LEFT JOIN users u ON u.user_id = r.created_by_user_id ";

        /// <summary>Default Valid Until = today + the setting prescription_validity_days (30).</summary>
        public static DateTime DefaultValidUntil =>
            DateTime.Today.AddDays(Math.Max(1, SettingsHelper.GetInt(SettingsHelper.PrescriptionValidityDays, 30)));

        // ------------------------------------------------------------------
        // Lists
        // ------------------------------------------------------------------

        /// <summary>Consultations of the last 60 days, newest first, optionally filtered by patient name or ID.</summary>
        public List<ConsultationChoice> GetConsultations(string? patientText)
        {
            var sql = new StringBuilder(
                "SELECT c.consultation_id, c.recorded_at, p.patient_id, " +
                "       CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " +
                "       TIMESTAMPDIFF(YEAR, p.date_of_birth, CURDATE()) AS age, p.allergies, " +
                "       CONCAT(d.first_name, ' ', d.last_name) AS doctor_name, c.diagnosis, " +
                "       (SELECT COUNT(*) FROM prescriptions r WHERE r.consultation_id = c.consultation_id) AS rx_count " +
                "FROM consultations c " +
                "JOIN appointments a ON a.appointment_id = c.appointment_id " +
                "JOIN patients p ON p.patient_id = a.patient_id " +
                "JOIN doctors d ON d.doctor_id = a.doctor_id " +
                $"WHERE c.recorded_at >= DATE_SUB(CURDATE(), INTERVAL {PrescriptionRules.ConsultationLookbackDays} DAY)");
            var parameters = new List<MySqlParameter>();
            AppendPatientFilter(sql, parameters, patientText);
            sql.Append(" ORDER BY c.recorded_at DESC, c.consultation_id DESC LIMIT 200");

            DataTable table = DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());
            return table.Rows.Cast<DataRow>().Select(row => new ConsultationChoice
            {
                ConsultationId = DatabaseHelper.GetString(row, "consultation_id"),
                RecordedAt = DatabaseHelper.GetNullableDate(row, "recorded_at") ?? DateTime.Now,
                PatientId = DatabaseHelper.GetString(row, "patient_id"),
                PatientName = DatabaseHelper.GetString(row, "patient_name"),
                Age = DatabaseHelper.GetInt(row, "age"),
                Allergies = DatabaseHelper.GetString(row, "allergies"),
                DoctorName = DatabaseHelper.GetString(row, "doctor_name"),
                Diagnosis = DatabaseHelper.GetString(row, "diagnosis"),
                PrescriptionCount = DatabaseHelper.GetInt(row, "rx_count")
            }).ToList();
        }

        /// <summary>The medicine catalog with usable stock (the first item is the "select" placeholder with an empty id).</summary>
        public List<MedicineChoice> GetMedicines()
        {
            DataTable table = DatabaseHelper.ExecuteQuery(
                "SELECT medicine_id, medicine_name, usable_stock, stock_status " +
                "FROM v_medicine_stock_status ORDER BY medicine_name");

            var list = new List<MedicineChoice> { new MedicineChoice { MedicineName = "Select a medicine..." } };
            list.AddRange(table.Rows.Cast<DataRow>().Select(row => new MedicineChoice
            {
                MedicineId = DatabaseHelper.GetString(row, "medicine_id"),
                MedicineName = DatabaseHelper.GetString(row, "medicine_name"),
                UsableStock = Convert.ToInt32(row["usable_stock"], CultureInfo.InvariantCulture),
                StockStatus = DatabaseHelper.GetString(row, "stock_status")
            }));
            return list;
        }

        /// <summary>Issued prescriptions (newest first), optionally filtered by patient name or ID; lines are not loaded.</summary>
        public List<PrescriptionRecord> GetIssued(string? patientText)
        {
            var sql = new StringBuilder(RecordSelect + "WHERE 1 = 1");
            var parameters = new List<MySqlParameter>();
            AppendPatientFilter(sql, parameters, patientText);
            sql.Append(" ORDER BY r.created_at DESC, r.prescription_id DESC LIMIT 300");

            DataTable table = DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());
            return table.Rows.Cast<DataRow>().Select(ReadHeader).ToList();
        }

        /// <summary>One prescription with all its lines (for the preview).</summary>
        public PrescriptionRecord? GetPrescription(string prescriptionId)
        {
            DataTable header = DatabaseHelper.ExecuteQuery(RecordSelect + "WHERE r.prescription_id = @id",
                DatabaseHelper.Param("@id", prescriptionId));
            if (header.Rows.Count == 0) return null;

            PrescriptionRecord record = ReadHeader(header.Rows[0]);
            record.Lines = ReadLines(DatabaseHelper.ExecuteQuery(LineSelect, DatabaseHelper.Param("@id", prescriptionId)));
            return record;
        }

        // ------------------------------------------------------------------
        // Save
        // ------------------------------------------------------------------

        /// <summary>Every rule of the input, in plain words (empty list = valid). The form shows the same rules next to the fields.</summary>
        public static List<string> Validate(PrescriptionInput input)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(input.ConsultationId)) errors.Add("Select the consultation first.");
            if (input.Lines.Count == 0) errors.Add("Add at least one medicine line before saving.");
            if (input.GeneralInstructions.Trim().Length > PrescriptionRules.MaxGeneralInstructions)
                errors.Add($"General instructions can have at most {PrescriptionRules.MaxGeneralInstructions} characters.");
            if (input.ValidUntil.Date < DateTime.Today) errors.Add("Valid Until cannot be before today.");
            if (input.ValidUntil.Date > DateTime.Today.AddDays(PrescriptionRules.MaxValidityDays))
                errors.Add($"Valid Until can be at most {PrescriptionRules.MaxValidityDays} days after today.");

            var seen = new HashSet<string>();
            foreach (PrescriptionLine line in input.Lines)
            {
                string who = string.IsNullOrEmpty(line.MedicineName) ? "A medicine line" : line.MedicineName;
                if (string.IsNullOrEmpty(line.MedicineId)) errors.Add($"{who}: choose the medicine from the catalog.");
                else if (!seen.Add(line.MedicineId)) errors.Add($"{who} appears twice. Keep one line per medicine.");
                if (string.IsNullOrWhiteSpace(line.Dosage)) errors.Add($"{who}: enter the dosage.");
                else if (line.Dosage.Trim().Length > PrescriptionRules.MaxDosage) errors.Add($"{who}: the dosage can have at most {PrescriptionRules.MaxDosage} characters.");
                if (line.FrequencyPerDay < 1 || line.FrequencyPerDay > PrescriptionRules.FrequencyMax)
                    errors.Add($"{who}: frequency must be a whole number from 1 to {PrescriptionRules.FrequencyMax} times per day.");
                if (line.DurationDays < 1 || line.DurationDays > PrescriptionRules.DurationMax)
                    errors.Add($"{who}: duration must be a whole number from 1 to {PrescriptionRules.DurationMax} days.");
                if (line.Instructions.Trim().Length > PrescriptionRules.MaxLineInstructions)
                    errors.Add($"{who}: the instructions can have at most {PrescriptionRules.MaxLineInstructions} characters.");
            }
            return errors;
        }

        /// <summary>Saves the header and all lines in one transaction and returns the stored prescription.</summary>
        public PrescriptionRecord Save(PrescriptionInput input)
        {
            List<string> errors = Validate(input);
            if (errors.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, errors));

            return DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                object? consultation = DatabaseHelper.ExecuteScalar(conn, tx,
                    "SELECT consultation_id FROM consultations WHERE consultation_id = @id FOR UPDATE",
                    DatabaseHelper.Param("@id", input.ConsultationId));
                if (consultation == null || consultation == DBNull.Value)
                    throw new InvalidOperationException("This consultation no longer exists. The list will be refreshed.");

                foreach (PrescriptionLine line in input.Lines)
                {
                    object? medicine = DatabaseHelper.ExecuteScalar(conn, tx,
                        "SELECT medicine_id FROM medicines WHERE medicine_id = @id",
                        DatabaseHelper.Param("@id", line.MedicineId));
                    if (medicine == null || medicine == DBNull.Value)
                        throw new InvalidOperationException($"{line.MedicineName} is no longer in the medicine catalog. Remove the line and choose another medicine.");
                }

                string id = GeneratorHelper.Generate(GeneratorHelper.Prefix.Prescription, conn, tx);

                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "INSERT INTO prescriptions (prescription_id, consultation_id, general_instructions, issued_date, valid_until, created_by_user_id) " +
                    "VALUES (@id, @consultation, @instructions, CURDATE(), @valid, @user)",
                    DatabaseHelper.Param("@id", id),
                    DatabaseHelper.Param("@consultation", input.ConsultationId),
                    DatabaseHelper.Param("@instructions", string.IsNullOrWhiteSpace(input.GeneralInstructions) ? null : input.GeneralInstructions.Trim()),
                    DatabaseHelper.Param("@valid", input.ValidUntil.Date),
                    DatabaseHelper.Param("@user", SessionManager.CurrentUserId > 0 ? SessionManager.CurrentUserId : (int?)null));

                foreach (PrescriptionLine line in input.Lines)
                {
                    DatabaseHelper.ExecuteInsert(conn, tx,
                        "INSERT INTO prescription_items (prescription_id, medicine_id, dosage, frequency_per_day, duration_days, instructions) " +
                        "VALUES (@rx, @medicine, @dosage, @freq, @days, @instructions)",
                        DatabaseHelper.Param("@rx", id),
                        DatabaseHelper.Param("@medicine", line.MedicineId),
                        DatabaseHelper.Param("@dosage", line.Dosage.Trim()),
                        DatabaseHelper.Param("@freq", line.FrequencyPerDay),
                        DatabaseHelper.Param("@days", line.DurationDays),
                        DatabaseHelper.Param("@instructions", string.IsNullOrWhiteSpace(line.Instructions) ? null : line.Instructions.Trim()));
                }

                DataTable header = DatabaseHelper.ExecuteQuery(conn, tx, RecordSelect + "WHERE r.prescription_id = @id",
                    DatabaseHelper.Param("@id", id));
                PrescriptionRecord record = ReadHeader(header.Rows[0]);
                record.Lines = ReadLines(DatabaseHelper.ExecuteQuery(conn, tx, LineSelect, DatabaseHelper.Param("@id", id)));
                return record;
            });
        }

        // ------------------------------------------------------------------
        // Helpers
        // ------------------------------------------------------------------

        private const string LineSelect =
            "SELECT i.medicine_id, m.medicine_name, i.dosage, i.frequency_per_day, i.duration_days, i.instructions " +
            "FROM prescription_items i JOIN medicines m ON m.medicine_id = i.medicine_id " +
            "WHERE i.prescription_id = @id ORDER BY i.prescription_item_id";

        private static List<PrescriptionLine> ReadLines(DataTable table)
        {
            return table.Rows.Cast<DataRow>().Select(row => new PrescriptionLine
            {
                MedicineId = DatabaseHelper.GetString(row, "medicine_id"),
                MedicineName = DatabaseHelper.GetString(row, "medicine_name"),
                Dosage = DatabaseHelper.GetString(row, "dosage"),
                FrequencyPerDay = DatabaseHelper.GetInt(row, "frequency_per_day"),
                DurationDays = DatabaseHelper.GetInt(row, "duration_days"),
                Instructions = DatabaseHelper.GetString(row, "instructions")
            }).ToList();
        }

        private static PrescriptionRecord ReadHeader(DataRow row)
        {
            return new PrescriptionRecord
            {
                PrescriptionId = DatabaseHelper.GetString(row, "prescription_id"),
                ConsultationId = DatabaseHelper.GetString(row, "consultation_id"),
                PatientId = DatabaseHelper.GetString(row, "patient_id"),
                PatientName = DatabaseHelper.GetString(row, "patient_name"),
                Age = DatabaseHelper.GetInt(row, "age"),
                Allergies = DatabaseHelper.GetString(row, "allergies"),
                DoctorName = DatabaseHelper.GetString(row, "doctor_name"),
                Diagnosis = DatabaseHelper.GetString(row, "diagnosis"),
                GeneralInstructions = DatabaseHelper.GetString(row, "general_instructions"),
                IssuedDate = DatabaseHelper.GetNullableDate(row, "issued_date") ?? DateTime.Today,
                ValidUntil = DatabaseHelper.GetNullableDate(row, "valid_until") ?? DateTime.Today,
                CreatedBy = DatabaseHelper.GetString(row, "created_by"),
                LineCount = DatabaseHelper.GetInt(row, "line_count")
            };
        }

        /// <summary>Every typed word must match the first, middle or last name or the patient ID.</summary>
        private static void AppendPatientFilter(StringBuilder sql, List<MySqlParameter> parameters, string? patientText)
        {
            string[] words = (patientText ?? string.Empty).Split(new[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                sql.Append($" AND (p.first_name LIKE @w{i} OR p.middle_name LIKE @w{i} OR p.last_name LIKE @w{i} OR p.patient_id LIKE @w{i})");
                parameters.Add(DatabaseHelper.Param($"@w{i}", "%" + words[i].Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_") + "%"));
            }
        }
    }
}