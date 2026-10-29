using System.Data;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP01_PatientRegistration;
using HospitalManagementSystem.Models.MP02_PatientSearchEditor;
using HospitalManagementSystem.Repositories.MP01_PatientRegistration;
using MySqlConnector;

namespace HospitalManagementSystem.Repositories.MP02_PatientSearchEditor
{
    /// <summary>
    /// All SQL for editing a patient profile and reading its audit trail (MP02, Part 5B).
    ///
    /// Rules enforced here (not only in the form):
    ///  * Patient ID, Registration Date, name and gender are never updated.
    ///  * Date of Birth is only written when the caller says an administrator approved it.
    ///  * One patient_audit_log row is written per CHANGED field, in the same transaction as the update.
    ///  * Two people editing the same patient cannot silently overwrite each other (last_updated_at check).
    /// </summary>
    public class PatientProfileRepository
    {
        /// <summary>The "last updated" stamp of the patient row (null if never edited). Used to detect clashing edits.</summary>
        public DateTime? GetLastUpdatedAt(string patientId)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(
                "SELECT last_updated_at FROM patients WHERE patient_id = @id",
                DatabaseHelper.Param("@id", patientId));

            return table.Rows.Count == 0 ? null : DatabaseHelper.GetNullableDate(table.Rows[0], "last_updated_at");
        }

        /// <summary>
        /// True when the username belongs to an active, unlocked Administrator and the password matches.
        /// Used only for the Date of Birth admin override. The caller shows the same message for every failure.
        /// </summary>
        public bool VerifyAdministrator(string username, string password)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(
                "SELECT u.password_hash, u.password_salt, u.hash_iterations " +
                "FROM users u " +
                "WHERE u.username = @username AND u.role = @role AND u.is_active = 1 " +
                "  AND (u.locked_until IS NULL OR u.locked_until <= NOW())",
                DatabaseHelper.Param("@username", (username ?? string.Empty).Trim()),
                DatabaseHelper.Param("@role", SessionManager.RoleAdministrator));

            if (table.Rows.Count == 0) return false;

            DataRow row = table.Rows[0];
            return PasswordHelper.Verify(
                password ?? string.Empty,
                DatabaseHelper.GetString(row, "password_hash"),
                DatabaseHelper.GetString(row, "password_salt"),
                DatabaseHelper.GetInt(row, "hash_iterations"));
        }

        // ------------------------------------------------------------------
        // Change detection (pure logic, no SQL)
        // ------------------------------------------------------------------

        /// <summary>
        /// Compares the stored patient with the edited one and returns one AuditEntry per field that really differs.
        /// Blank and null are the same; phone numbers are compared in canonical form, so the legacy
        /// "0917-123-4567" and "+639171234567" are NOT reported as a change.
        /// </summary>
        /// <param name="dobApprovedBy">Username of the administrator who approved a Date of Birth change (recorded in the log).</param>
        public static List<AuditEntry> DetectChanges(Patient original, Patient edited, string? dobApprovedBy = null)
        {
            var changes = new List<AuditEntry>();

            void Add(string field, string oldValue, string newValue)
            {
                changes.Add(new AuditEntry
                {
                    PatientId = original.PatientId,
                    FieldChanged = field,
                    OldValue = NullIfBlank(oldValue),
                    NewValue = NullIfBlank(newValue)
                });
            }

            if (original.DateOfBirth.Date != edited.DateOfBirth.Date)
            {
                string approval = string.IsNullOrWhiteSpace(dobApprovedBy) ? string.Empty : $"  [approved by {dobApprovedBy}]";
                Add("Date of Birth", original.DateOfBirth.ToString("MMM d, yyyy"), edited.DateOfBirth.ToString("MMM d, yyyy") + approval);
            }

            if (Norm(original.BloodType) != Norm(edited.BloodType)) Add("Blood Type", original.BloodType, edited.BloodType);

            if (PhoneKey(original.PhoneNumber) != PhoneKey(edited.PhoneNumber))
                Add("Phone Number", PhoneHelper.Format(original.PhoneNumber), PhoneHelper.Format(edited.PhoneNumber));

            if (Norm(original.Email) != Norm(edited.Email)) Add("Email", original.Email, edited.Email);
            if (Norm(original.StreetAddress) != Norm(edited.StreetAddress)) Add("Street Address", original.StreetAddress, edited.StreetAddress);
            if (Norm(original.City) != Norm(edited.City)) Add("City", original.City, edited.City);
            if (Norm(original.Province) != Norm(edited.Province)) Add("Province", original.Province, edited.Province);
            if (Norm(original.PostalCode) != Norm(edited.PostalCode)) Add("Postal Code", original.PostalCode, edited.PostalCode);
            if (Norm(original.Allergies) != Norm(edited.Allergies)) Add("Allergies", original.Allergies, edited.Allergies);
            if (Norm(original.MedicalHistory) != Norm(edited.MedicalHistory)) Add("Medical History", original.MedicalHistory, edited.MedicalHistory);

            if (Norm(original.EmergencyContactName) != Norm(edited.EmergencyContactName))
                Add("Emergency Contact Name", original.EmergencyContactName, edited.EmergencyContactName);

            if (Norm(original.EmergencyContactRelation) != Norm(edited.EmergencyContactRelation))
                Add("Emergency Contact Relationship", original.EmergencyContactRelation, edited.EmergencyContactRelation);

            if (PhoneKey(original.EmergencyContactPhone) != PhoneKey(edited.EmergencyContactPhone))
                Add("Emergency Contact Phone", PhoneHelper.Format(original.EmergencyContactPhone), PhoneHelper.Format(edited.EmergencyContactPhone));

            // discount card and coverage (compared in their stored form)
            if (PatientRepository.CardType(original) != PatientRepository.CardType(edited))
                Add("Discount Card", PatientRepository.CardType(original), PatientRepository.CardType(edited));
            if (Norm(PatientRepository.CardNumber(original)) != Norm(PatientRepository.CardNumber(edited)))
                Add("Discount Card No.", PatientRepository.CardNumber(original) ?? string.Empty, PatientRepository.CardNumber(edited) ?? string.Empty);
            if (PatientRepository.CoverageType(original) != PatientRepository.CoverageType(edited))
                Add("Coverage", PatientRepository.CoverageType(original), PatientRepository.CoverageType(edited));
            if (Norm(PatientRepository.CoverageNumber(original)) != Norm(PatientRepository.CoverageNumber(edited)))
                Add("Coverage No.", PatientRepository.CoverageNumber(original) ?? string.Empty, PatientRepository.CoverageNumber(edited) ?? string.Empty);
            if (Norm(PatientRepository.CoverageProvider(original)) != Norm(PatientRepository.CoverageProvider(edited)))
                Add("HMO Provider", PatientRepository.CoverageProvider(original) ?? string.Empty, PatientRepository.CoverageProvider(edited) ?? string.Empty);

            return changes;
        }

        // ------------------------------------------------------------------
        // Saving
        // ------------------------------------------------------------------

        /// <summary>
        /// Writes the edited profile and its audit rows in ONE transaction.
        /// Throws InvalidOperationException (with a plain message) when the patient was changed by someone else meanwhile.
        /// </summary>
        /// <param name="loadedLastUpdatedAt">The value GetLastUpdatedAt returned when the editor was opened.</param>
        /// <param name="allowDobChange">True only after an administrator approved the override.</param>
        public void Save(Patient edited, List<AuditEntry> changes, int changedByUserId,
                         DateTime? loadedLastUpdatedAt, bool allowDobChange)
        {
            if (changes.Count == 0) return;

            DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                // Lock the row and make sure nobody else saved a change after this editor opened.
                DataTable current = DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT last_updated_at FROM patients WHERE patient_id = @id FOR UPDATE",
                    DatabaseHelper.Param("@id", edited.PatientId));

                if (current.Rows.Count == 0)
                    throw new InvalidOperationException("This patient no longer exists.");

                DateTime? storedStamp = DatabaseHelper.GetNullableDate(current.Rows[0], "last_updated_at");
                if (storedStamp != loadedLastUpdatedAt)
                    throw new InvalidOperationException(
                        "This profile was changed by someone else after you opened it. " +
                        "Close this window, open the patient again and repeat your changes.");

                // Date of Birth is written ONLY when the override was approved; otherwise the column is left alone.
                string dobSql = allowDobChange ? ", date_of_birth = @dob" : string.Empty;
                bool contact = edited.HasEmergencyContact;   // the contact is all three parts or none

                var parameters = new List<MySqlParameter>
                {
                    DatabaseHelper.Param("@id", edited.PatientId),
                    DatabaseHelper.Param("@blood", NullIfBlank(edited.BloodType)),
                    DatabaseHelper.Param("@phone", edited.PhoneNumber),
                    DatabaseHelper.Param("@email", NullIfBlank(edited.Email)),
                    DatabaseHelper.Param("@street", NullIfBlank(edited.StreetAddress)),
                    DatabaseHelper.Param("@city", NullIfBlank(edited.City)),
                    DatabaseHelper.Param("@province", NullIfBlank(edited.Province)),
                    DatabaseHelper.Param("@postal", NullIfBlank(edited.PostalCode)),
                    DatabaseHelper.Param("@allergies", NullIfBlank(edited.Allergies)),
                    DatabaseHelper.Param("@history", NullIfBlank(edited.MedicalHistory)),
                    DatabaseHelper.Param("@ecName", contact ? edited.EmergencyContactName.Trim() : null),
                    DatabaseHelper.Param("@ecPhone", contact ? edited.EmergencyContactPhone : null),
                    DatabaseHelper.Param("@ecRelation", contact ? edited.EmergencyContactRelation : null),
                    DatabaseHelper.Param("@cardType", PatientRepository.CardType(edited)),
                    DatabaseHelper.Param("@cardNo", PatientRepository.CardNumber(edited)),
                    DatabaseHelper.Param("@covType", PatientRepository.CoverageType(edited)),
                    DatabaseHelper.Param("@covNo", PatientRepository.CoverageNumber(edited)),
                    DatabaseHelper.Param("@covProvider", PatientRepository.CoverageProvider(edited))
                };
                if (allowDobChange) parameters.Add(DatabaseHelper.Param("@dob", edited.DateOfBirth.Date));

                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "UPDATE patients SET blood_type = @blood, phone_number = @phone, email = @email, " +
                    "       street_address = @street, city = @city, province = @province, postal_code = @postal, " +
                    "       allergies = @allergies, medical_history = @history, " +
                    "       emergency_contact_name = @ecName, emergency_contact_phone = @ecPhone, " +
                    "       emergency_contact_relationship = @ecRelation, " +
                    "       discount_card_type = @cardType, discount_card_number = @cardNo, " +
                    "       coverage_type = @covType, coverage_number = @covNo, coverage_provider = @covProvider, " +
                    "       last_updated_at = NOW()" + dobSql + " " +
                    "WHERE patient_id = @id",
                    parameters.ToArray());

                foreach (AuditEntry entry in changes)
                {
                    DatabaseHelper.ExecuteNonQuery(conn, tx,
                        "INSERT INTO patient_audit_log (patient_id, field_changed, old_value, new_value, changed_by_user_id) " +
                        "VALUES (@patient, @field, @old, @new, @user)",
                        DatabaseHelper.Param("@patient", edited.PatientId),
                        DatabaseHelper.Param("@field", entry.FieldChanged),
                        DatabaseHelper.Param("@old", entry.OldValue),
                        DatabaseHelper.Param("@new", entry.NewValue),
                        DatabaseHelper.Param("@user", changedByUserId));
                }
            });
        }

        // ------------------------------------------------------------------
        // Audit trail
        // ------------------------------------------------------------------

        /// <summary>
        /// The change history of one patient, newest first.
        /// Columns: audit_id, field_changed, old_value, new_value, changed_by, changed_at.
        /// </summary>
        public DataTable GetAuditTrail(string patientId)
        {
            return DatabaseHelper.ExecuteQuery(
                "SELECT a.audit_id, a.field_changed, a.old_value, a.new_value, " +
                "       u.full_name AS changed_by, a.changed_at " +
                "FROM patient_audit_log a JOIN users u ON u.user_id = a.changed_by_user_id " +
                "WHERE a.patient_id = @id " +
                "ORDER BY a.changed_at DESC, a.audit_id DESC",
                DatabaseHelper.Param("@id", patientId));
        }

        // ------------------------------------------------------------------
        // Helpers
        // ------------------------------------------------------------------

        private static string Norm(string? text) => (text ?? string.Empty).Trim();

        /// <summary>Canonical phone for comparing; text that is not a valid number is compared as typed.</summary>
        private static string PhoneKey(string? phone) =>
            PhoneHelper.TryNormalize(phone, out string canonical) ? canonical : Norm(phone);

        private static string? NullIfBlank(string? text) =>
            string.IsNullOrWhiteSpace(text) ? null : text.Trim();
    }
}