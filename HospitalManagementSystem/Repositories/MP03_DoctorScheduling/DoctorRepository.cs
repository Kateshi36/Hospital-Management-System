using System.Data;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP03_DoctorScheduling;

namespace HospitalManagementSystem.Repositories.MP03_DoctorScheduling
{
    /// <summary>All SQL for the doctor directory (MP03, Part 7A): list, search, add, edit and delete.</summary>
    public class DoctorRepository
    {
        private const string DoctorSelect =
            "SELECT d.doctor_id, d.first_name, d.last_name, d.license_number, " +
            "       d.specialization, d.department, d.phone, d.email, d.consultation_fee, d.employment_type, d.created_at " +
            "FROM doctors d ";

        /// <summary>
        /// Doctors for the grid. An empty filter returns everybody; otherwise the text is matched
        /// (partial, case-insensitive) against name, doctor ID, license number and specialization.
        /// </summary>
        public DataTable GetDoctorsTable(string? filter)
        {
            const string sql =
                "SELECT d.doctor_id, CONCAT('Dr. ', d.first_name, ' ', d.last_name) AS full_name, " +
                "       d.license_number, d.specialization AS specialization_name, d.department AS department_name, " +
                "       d.employment_type AS employment_type_name, " +
                "       d.phone, d.consultation_fee, " +
                "       (SELECT COUNT(*) FROM doctor_schedules sc WHERE sc.doctor_id = d.doctor_id) AS schedule_blocks " +
                "FROM doctors d " +
                "WHERE (@f IS NULL OR d.doctor_id LIKE @like OR d.first_name LIKE @like OR d.last_name LIKE @like " +
                "       OR CONCAT(d.first_name, ' ', d.last_name) LIKE @like OR d.license_number LIKE @like " +
                "       OR d.specialization LIKE @like) " +
                "ORDER BY d.last_name, d.first_name";

            string? text = string.IsNullOrWhiteSpace(filter) ? null : filter.Trim();
            string like = text == null ? string.Empty : "%" + EscapeLike(text) + "%";

            return DatabaseHelper.ExecuteQuery(sql,
                DatabaseHelper.Param("@f", text),
                DatabaseHelper.Param("@like", like));
        }

        public Doctor? GetById(string doctorId)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(DoctorSelect + "WHERE d.doctor_id = @id",
                DatabaseHelper.Param("@id", doctorId));
            return table.Rows.Count == 0 ? null : Map(table.Rows[0]);
        }

        /// <summary>True when another doctor already holds this license number (pass the doctor being edited to ignore it).</summary>
        public bool LicenseExists(string licenseNumber, string? excludeDoctorId)
        {
            long count = DatabaseHelper.ExecuteScalar<long>(
                "SELECT COUNT(*) FROM doctors WHERE license_number = @lic AND (@ex IS NULL OR doctor_id <> @ex)",
                DatabaseHelper.Param("@lic", licenseNumber),
                DatabaseHelper.Param("@ex", excludeDoctorId));
            return count > 0;
        }

        /// <summary>
        /// Saves a new doctor. The DOC-XXX id is generated inside the same transaction as the insert,
        /// so a failed save never uses up a number.
        /// </summary>
        public Doctor Add(Doctor doctor)
        {
            EnsureFeeInRange(doctor.ConsultationFee);
            Doctor saved = DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                EnsureLicenseIsUnique(conn, tx, doctor.LicenseNumber, null);
                doctor.Specialization = NormalizeOpenValue(conn, tx, "specialization", doctor.Specialization);
                doctor.Department = NormalizeOpenValue(conn, tx, "department", doctor.Department);

                doctor.DoctorId = GeneratorHelper.Generate(GeneratorHelper.Prefix.Doctor, conn, tx);

                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "INSERT INTO doctors (doctor_id, first_name, last_name, license_number, specialization, " +
                    "                     department, phone, email, consultation_fee, employment_type) " +
                    "VALUES (@id, @first, @last, @lic, @spec, @dept, @phone, @email, @fee, @emp)",
                    DatabaseHelper.Param("@id", doctor.DoctorId),
                    DatabaseHelper.Param("@first", doctor.FirstName),
                    DatabaseHelper.Param("@last", doctor.LastName),
                    DatabaseHelper.Param("@lic", doctor.LicenseNumber),
                    DatabaseHelper.Param("@spec", doctor.Specialization.Trim()),
                    DatabaseHelper.Param("@dept", doctor.Department.Trim()),
                    DatabaseHelper.Param("@phone", doctor.Phone),
                    DatabaseHelper.Param("@email", NullIfBlank(doctor.Email)),
                    DatabaseHelper.Param("@fee", doctor.ConsultationFee),
                    DatabaseHelper.Param("@emp", doctor.EmploymentType));

                AuditHelper.Log(conn, tx, AuditHelper.Create, AuditHelper.Doctor, doctor.DoctorId, null,
                    $"Dr. {doctor.FirstName} {doctor.LastName}, {doctor.Specialization}, fee {doctor.ConsultationFee:N2}");
                return doctor;
            });
            AppEvents.RaiseDoctorsChanged();
            return saved;
        }

        /// <summary>The highest consultation fee a doctor can have (app_settings max_consultation_fee, 20,000.00).</summary>
        public static decimal MaxConsultationFee() => SettingsHelper.GetDecimal(SettingsHelper.MaxConsultationFee, 20000m);

        private static void EnsureFeeInRange(decimal fee)
        {
            decimal max = MaxConsultationFee();
            if (fee < 0 || fee > max)
                throw new InvalidOperationException($"The consultation fee must be from 0.00 to {max:N2}.");
        }

        /// <summary>
        /// Keeps the open lists (specialization, department) consistent: spaces are tidied, a value that already exists
        /// with a different capitalisation ("cardiology" / "CARDIOLOGY") is saved with the existing spelling ("Cardiology"),
        /// and a new value typed all in lower case gets capital initials. So every module shows the same name.
        /// </summary>
        public static string NormalizeOpenValue(MySqlConnector.MySqlConnection conn, MySqlConnector.MySqlTransaction? tx, string column, string value)
        {
            string tidy = string.Join(" ", (value ?? string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries));
            if (tidy.Length == 0) return tidy;
            // column is a code constant (specialization / department), never user input; the collation compares without case
            object? existing = DatabaseHelper.ExecuteScalar(conn, tx,
                $"SELECT {column} FROM doctors WHERE {column} = @v GROUP BY {column} ORDER BY COUNT(*) DESC LIMIT 1",
                DatabaseHelper.Param("@v", tidy));
            if (existing is string found && found.Length > 0) return found;
            if (tidy == tidy.ToLowerInvariant())
                tidy = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tidy);
            return tidy;
        }

        /// <summary>Updates an existing doctor. The doctor ID never changes.</summary>
        public void Update(Doctor doctor)
        {
            EnsureFeeInRange(doctor.ConsultationFee);
            DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                EnsureLicenseIsUnique(conn, tx, doctor.LicenseNumber, doctor.DoctorId);
                doctor.Specialization = NormalizeOpenValue(conn, tx, "specialization", doctor.Specialization);
                doctor.Department = NormalizeOpenValue(conn, tx, "department", doctor.Department);

                int rows = DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "UPDATE doctors SET first_name = @first, last_name = @last, license_number = @lic, " +
                    "       specialization = @spec, department = @dept, phone = @phone, email = @email, " +
                    "       consultation_fee = @fee, employment_type = @emp " +
                    "WHERE doctor_id = @id",
                    DatabaseHelper.Param("@first", doctor.FirstName),
                    DatabaseHelper.Param("@last", doctor.LastName),
                    DatabaseHelper.Param("@lic", doctor.LicenseNumber),
                    DatabaseHelper.Param("@spec", doctor.Specialization.Trim()),
                    DatabaseHelper.Param("@dept", doctor.Department.Trim()),
                    DatabaseHelper.Param("@phone", doctor.Phone),
                    DatabaseHelper.Param("@email", NullIfBlank(doctor.Email)),
                    DatabaseHelper.Param("@fee", doctor.ConsultationFee),
                    DatabaseHelper.Param("@emp", doctor.EmploymentType),
                    DatabaseHelper.Param("@id", doctor.DoctorId));

                // MySQL reports 0 changed rows when nothing was modified, so check the record still exists.
                if (rows == 0 && DatabaseHelper.ExecuteScalar<long>(conn, tx,
                        "SELECT COUNT(*) FROM doctors WHERE doctor_id = @id",
                        DatabaseHelper.Param("@id", doctor.DoctorId)) == 0)
                {
                    throw new InvalidOperationException("This doctor no longer exists. Another user may have deleted the record.");
                }

                AuditHelper.Log(conn, tx, AuditHelper.Update, AuditHelper.Doctor, doctor.DoctorId, null,
                    $"Dr. {doctor.FirstName} {doctor.LastName}, {doctor.Specialization}, fee {doctor.ConsultationFee:N2}");
            });
            AppEvents.RaiseDoctorsChanged();
        }

        /// <summary>Counts what a delete would remove (schedules, leaves) and what would block it (history).</summary>
        public DoctorDeleteImpact GetDeleteImpact(string doctorId)
        {
            return DatabaseHelper.ExecuteInTransaction((conn, tx) => ReadImpact(conn, tx, doctorId));
        }

        /// <summary>
        /// Deletes a doctor together with their schedule blocks and leave dates (database cascade).
        /// Refused when appointments, queue entries or lab orders still point to the doctor.
        /// </summary>
        public void Delete(string doctorId)
        {
            DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                DoctorDeleteImpact impact = ReadImpact(conn, tx, doctorId);
                if (impact.IsBlocked)
                {
                    throw new InvalidOperationException(DescribeBlockers(impact));
                }

                DatabaseHelper.ExecuteNonQuery(conn, tx, "DELETE FROM doctors WHERE doctor_id = @id",
                    DatabaseHelper.Param("@id", doctorId));
                AuditHelper.Log(conn, tx, AuditHelper.Delete, AuditHelper.Doctor, doctorId, null, "Doctor deleted with schedules and leaves");
            });
            AppEvents.RaiseDoctorsChanged();
        }

        /// <summary>Plain-language list of the records that stop a doctor from being deleted.</summary>
        public static string DescribeBlockers(DoctorDeleteImpact impact)
        {
            var parts = new List<string>();
            if (impact.Appointments > 0) parts.Add($"{impact.Appointments} appointment(s)");
            if (impact.QueueEntries > 0) parts.Add($"{impact.QueueEntries} queue entr{(impact.QueueEntries == 1 ? "y" : "ies")}");
            if (impact.LabOrders > 0) parts.Add($"{impact.LabOrders} lab order(s)");

            return "This doctor cannot be deleted because patient records still refer to them: " +
                   string.Join(", ", parts) + ".";
        }

        // ------------------------------------------------------------------
        // Helpers
        // ------------------------------------------------------------------

        private static DoctorDeleteImpact ReadImpact(MySqlConnector.MySqlConnection conn, MySqlConnector.MySqlTransaction tx, string doctorId)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(conn, tx,
                "SELECT " +
                " (SELECT COUNT(*) FROM doctor_schedules WHERE doctor_id = @id) AS schedule_blocks, " +
                " (SELECT COUNT(*) FROM doctor_leaves    WHERE doctor_id = @id) AS leave_dates, " +
                " (SELECT COUNT(*) FROM appointments     WHERE doctor_id = @id) AS appointments, " +
                " (SELECT COUNT(*) FROM queue_entries    WHERE doctor_id = @id) AS queue_entries, " +
                " (SELECT COUNT(*) FROM lab_orders       WHERE ordered_by_doctor_id = @id) AS lab_orders",
                DatabaseHelper.Param("@id", doctorId));

            DataRow row = table.Rows[0];
            return new DoctorDeleteImpact
            {
                ScheduleBlocks = DatabaseHelper.GetInt(row, "schedule_blocks"),
                LeaveDates = DatabaseHelper.GetInt(row, "leave_dates"),
                Appointments = DatabaseHelper.GetInt(row, "appointments"),
                QueueEntries = DatabaseHelper.GetInt(row, "queue_entries"),
                LabOrders = DatabaseHelper.GetInt(row, "lab_orders")
            };
        }

        /// <summary>Authoritative duplicate check, run inside the save transaction (the database key is the last safety net).</summary>
        private static void EnsureLicenseIsUnique(MySqlConnector.MySqlConnection conn, MySqlConnector.MySqlTransaction tx,
                                                  string licenseNumber, string? excludeDoctorId)
        {
            DataTable owner = DatabaseHelper.ExecuteQuery(conn, tx,
                "SELECT doctor_id, first_name, last_name FROM doctors " +
                "WHERE license_number = @lic AND (@ex IS NULL OR doctor_id <> @ex) LIMIT 1",
                DatabaseHelper.Param("@lic", licenseNumber),
                DatabaseHelper.Param("@ex", excludeDoctorId));

            if (owner.Rows.Count > 0)
            {
                DataRow r = owner.Rows[0];
                throw new InvalidOperationException(
                    $"License number {licenseNumber} already belongs to {DatabaseHelper.GetString(r, "doctor_id")} " +
                    $"(Dr. {DatabaseHelper.GetString(r, "first_name")} {DatabaseHelper.GetString(r, "last_name")}). " +
                    "Each doctor must have a unique license number.");
            }
        }

        private static Doctor Map(DataRow row)
        {
            return new Doctor
            {
                DoctorId = DatabaseHelper.GetString(row, "doctor_id"),
                FirstName = DatabaseHelper.GetString(row, "first_name"),
                LastName = DatabaseHelper.GetString(row, "last_name"),
                LicenseNumber = DatabaseHelper.GetString(row, "license_number"),
                Specialization = DatabaseHelper.GetString(row, "specialization"),
                Department = DatabaseHelper.GetString(row, "department"),
                Phone = DatabaseHelper.GetString(row, "phone"),
                Email = DatabaseHelper.GetString(row, "email"),
                ConsultationFee = DatabaseHelper.GetDecimal(row, "consultation_fee"),
                EmploymentType = DatabaseHelper.GetString(row, "employment_type"),
                CreatedAt = DatabaseHelper.GetNullableDate(row, "created_at") ?? DateTime.MinValue
            };
        }

        private static string? NullIfBlank(string? text) => string.IsNullOrWhiteSpace(text) ? null : text.Trim();

        /// <summary>Makes % and _ typed by the user match themselves instead of acting as wildcards.</summary>
        private static string EscapeLike(string text)
        {
            return text.Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_");
        }
    }
}