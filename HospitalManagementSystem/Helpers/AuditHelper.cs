using MySqlConnector;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// Writes the system-wide audit trail (table audit_log) - who did what to which record and who OPENED a patient's
    /// medical record (Data Privacy Act, RA 10173). Rows are only ever inserted.
    ///
    ///   * Inside a save: call Log(conn, tx, ...) in the SAME transaction, so the record and its audit row are saved
    ///     (or rolled back) together.
    ///   * For a view (opening a record): call LogView(...). It never stops the screen from opening: if the audit row
    ///     cannot be written the error is ignored, because the record itself is unchanged.
    /// </summary>
    public static class AuditHelper
    {
        // actions
        public const string View = "View";
        public const string Create = "Create";
        public const string Update = "Update";
        public const string Delete = "Delete";
        public const string Finalize = "Finalize";
        public const string Void = "Void";
        public const string Payment = "Payment";
        public const string Refund = "Refund";
        public const string Dispense = "Dispense";
        public const string StockIn = "Stock In";
        public const string Adjust = "Adjust";

        // entity types
        public const string Patient = "Patient";
        public const string Doctor = "Doctor";
        public const string Consultation = "Consultation";
        public const string Prescription = "Prescription";
        public const string Dispensing = "Dispensing";
        public const string MedicineStock = "Medicine Stock";
        public const string LabResult = "Lab Result";
        public const string Bill = "Bill";
        public const string PaymentRecord = "Payment";

        private const int MaxDetails = 500;

        private const string InsertSql =
            "INSERT INTO audit_log (user_id, action, entity_type, entity_id, patient_id, details) " +
            "VALUES (@user, @action, @type, @id, @patient, @details)";

        private static MySqlParameter[] Params(string action, string entityType, string entityId, string? patientId, string? details)
        {
            string? text = string.IsNullOrWhiteSpace(details) ? null : details.Trim();
            if (text != null && text.Length > MaxDetails) text = text.Substring(0, MaxDetails - 3) + "...";
            return new[]
            {
                DatabaseHelper.Param("@user", SessionManager.CurrentUser?.UserId),
                DatabaseHelper.Param("@action", action),
                DatabaseHelper.Param("@type", entityType),
                DatabaseHelper.Param("@id", entityId),
                DatabaseHelper.Param("@patient", string.IsNullOrWhiteSpace(patientId) ? null : patientId),
                DatabaseHelper.Param("@details", text)
            };
        }

        /// <summary>Writes one audit row inside the caller's transaction.</summary>
        public static void Log(MySqlConnection conn, MySqlTransaction? tx, string action, string entityType, string entityId,
                               string? patientId = null, string? details = null)
        {
            DatabaseHelper.ExecuteNonQuery(conn, tx, InsertSql, Params(action, entityType, entityId, patientId, details));
        }

        /// <summary>Writes one audit row on its own connection (for actions that are not part of a save transaction).</summary>
        public static void Log(string action, string entityType, string entityId, string? patientId = null, string? details = null)
        {
            DatabaseHelper.ExecuteNonQuery(InsertSql, Params(action, entityType, entityId, patientId, details));
        }

        /// <summary>Records that the current user opened a record. Never throws.</summary>
        public static void LogView(string entityType, string entityId, string? patientId = null, string? details = null)
        {
            if (string.IsNullOrWhiteSpace(entityId)) return;
            try
            {
                Log(View, entityType, entityId, patientId, details);
            }
            catch
            {
                // a failed audit row must never stop a clinician from reading the record
            }
        }
    }
}
