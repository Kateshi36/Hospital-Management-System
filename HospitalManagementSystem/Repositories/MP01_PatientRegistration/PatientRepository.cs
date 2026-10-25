using System.Data;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP01_PatientRegistration;

namespace HospitalManagementSystem.Repositories.MP01_PatientRegistration
{
    /// <summary>All SQL for registering patients and listing them (MP01).</summary>
    public class PatientRepository
    {
        // One SELECT used to load a complete patient.
        private const string PatientSelect =
            "SELECT p.patient_id, p.first_name, p.middle_name, p.last_name, p.date_of_birth, " +
            "       p.gender, p.blood_type, p.phone_number, p.email, " +
            "       p.street_address, p.city, p.province, p.postal_code, " +
            "       p.allergies, p.medical_history, p.registered_at, " +
            "       p.emergency_contact_name, p.emergency_contact_phone, p.emergency_contact_relationship " +
            "FROM patients p ";

        /// <summary>
        /// Saves a new patient. The Patient ID (PAT-YYYYMMDD-XXX) is generated INSIDE the same transaction,
        /// so if anything fails nothing is saved and the number is not used up.
        /// </summary>
        public Patient Register(Patient patient, int registeredByUserId)
        {
            return DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                patient.PatientId = GeneratorHelper.Generate(GeneratorHelper.Prefix.Patient, conn, tx);
                bool hasContact = patient.HasEmergencyContact;

                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "INSERT INTO patients (patient_id, first_name, middle_name, last_name, date_of_birth, gender, " +
                    "                      blood_type, phone_number, email, street_address, city, province, postal_code, " +
                    "                      allergies, medical_history, emergency_contact_name, emergency_contact_phone, " +
                    "                      emergency_contact_relationship, registered_by_user_id) " +
                    "VALUES (@id, @first, @middle, @last, @dob, @gender, @blood, @phone, @email, @street, @city, @province, " +
                    "        @postal, @allergies, @history, @ecName, @ecPhone, @ecRelation, @user)",
                    DatabaseHelper.Param("@id", patient.PatientId),
                    DatabaseHelper.Param("@first", patient.FirstName),
                    DatabaseHelper.Param("@middle", NullIfBlank(patient.MiddleName)),
                    DatabaseHelper.Param("@last", patient.LastName),
                    DatabaseHelper.Param("@dob", patient.DateOfBirth.Date),
                    DatabaseHelper.Param("@gender", patient.Gender),
                    DatabaseHelper.Param("@blood", NullIfBlank(patient.BloodType)),
                    DatabaseHelper.Param("@phone", patient.PhoneNumber),
                    DatabaseHelper.Param("@email", NullIfBlank(patient.Email)),
                    DatabaseHelper.Param("@street", NullIfBlank(patient.StreetAddress)),
                    DatabaseHelper.Param("@city", NullIfBlank(patient.City)),
                    DatabaseHelper.Param("@province", NullIfBlank(patient.Province)),
                    DatabaseHelper.Param("@postal", NullIfBlank(patient.PostalCode)),
                    DatabaseHelper.Param("@allergies", NullIfBlank(patient.Allergies)),
                    DatabaseHelper.Param("@history", NullIfBlank(patient.MedicalHistory)),
                    DatabaseHelper.Param("@ecName", hasContact ? patient.EmergencyContactName : null),
                    DatabaseHelper.Param("@ecPhone", hasContact ? patient.EmergencyContactPhone : null),
                    DatabaseHelper.Param("@ecRelation", hasContact ? patient.EmergencyContactRelation : null),
                    DatabaseHelper.Param("@user", registeredByUserId));

                patient.RegisteredAt = DatabaseHelper.ExecuteScalar<DateTime>(conn, tx,
                    "SELECT registered_at FROM patients WHERE patient_id = @id",
                    DatabaseHelper.Param("@id", patient.PatientId));

                return patient;
            });
        }

        /// <summary>Rows for the grid (newest first). Age is calculated by the database from the date of birth.</summary>
        public DataTable GetRegisteredTable()
        {
            return DatabaseHelper.ExecuteQuery(
                "SELECT p.patient_id, " +
                "       CONCAT(p.last_name, ', ', p.first_name, " +
                "              IF(p.middle_name IS NULL OR p.middle_name = '', '', CONCAT(' ', LEFT(p.middle_name, 1), '.'))) AS full_name, " +
                "       TIMESTAMPDIFF(YEAR, p.date_of_birth, CURDATE()) AS age, " +
                "       p.gender, p.phone_number " +
                "FROM patients p " +
                "ORDER BY p.registered_at DESC, p.patient_id DESC");
        }

        /// <summary>One complete patient, or null when the id does not exist.</summary>
        public Patient? GetById(string patientId)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(
                PatientSelect + "WHERE p.patient_id = @id",
                DatabaseHelper.Param("@id", patientId));

            return table.Rows.Count == 0 ? null : Map(table.Rows[0]);
        }

        private static Patient Map(DataRow row)
        {
            return new Patient
            {
                PatientId = DatabaseHelper.GetString(row, "patient_id"),
                FirstName = DatabaseHelper.GetString(row, "first_name"),
                MiddleName = DatabaseHelper.GetString(row, "middle_name"),
                LastName = DatabaseHelper.GetString(row, "last_name"),
                DateOfBirth = Convert.ToDateTime(row["date_of_birth"]),
                Gender = DatabaseHelper.GetString(row, "gender"),
                BloodType = DatabaseHelper.GetString(row, "blood_type"),
                PhoneNumber = DatabaseHelper.GetString(row, "phone_number"),
                Email = DatabaseHelper.GetString(row, "email"),
                StreetAddress = DatabaseHelper.GetString(row, "street_address"),
                City = DatabaseHelper.GetString(row, "city"),
                Province = DatabaseHelper.GetString(row, "province"),
                PostalCode = DatabaseHelper.GetString(row, "postal_code"),
                Allergies = DatabaseHelper.GetString(row, "allergies"),
                MedicalHistory = DatabaseHelper.GetString(row, "medical_history"),
                EmergencyContactName = DatabaseHelper.GetString(row, "emergency_contact_name"),
                EmergencyContactPhone = DatabaseHelper.GetString(row, "emergency_contact_phone"),
                EmergencyContactRelation = DatabaseHelper.GetString(row, "emergency_contact_relationship"),
                RegisteredAt = Convert.ToDateTime(row["registered_at"])
            };
        }

        private static string? NullIfBlank(string? text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : text.Trim();
        }
    }
}
