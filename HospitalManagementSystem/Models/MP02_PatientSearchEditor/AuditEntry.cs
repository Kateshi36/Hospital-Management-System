namespace HospitalManagementSystem.Models.MP02_PatientSearchEditor
{
    /// <summary>
    /// One changed field of one patient profile (one row of patient_audit_log).
    /// Only fields that really changed are ever logged.
    /// </summary>
    public class AuditEntry
    {
        public string PatientId { get; set; } = string.Empty;

        /// <summary>Readable field name, e.g. "Phone Number" or "Blood Type".</summary>
        public string FieldChanged { get; set; } = string.Empty;

        /// <summary>Value before the change, or null when the field was blank.</summary>
        public string? OldValue { get; set; }

        /// <summary>Value after the change, or null when the field was cleared.</summary>
        public string? NewValue { get; set; }

        public int ChangedByUserId { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}