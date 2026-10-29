using System.Globalization;

namespace HospitalManagementSystem.Models.Common
{
    /// <summary>One line of the Audit Trail screen (a row of audit_log, or a field change from patient_audit_log).</summary>
    public class AuditTrailRow
    {
        public DateTime LoggedAt { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;

        public string WhenText => LoggedAt.ToString("MMM dd, yyyy  hh:mm:ss tt", CultureInfo.InvariantCulture);
        public string UserText => UserName.Length == 0 ? "-" : Role.Length == 0 ? UserName : $"{UserName} ({Role})";
        public string RecordText => $"{EntityType} {EntityId}";
        public string PatientText => PatientId.Length == 0 ? "-" : PatientName.Length == 0 ? PatientId : $"{PatientName} ({PatientId})";
    }

    /// <summary>The filters of the Audit Trail screen.</summary>
    public class AuditTrailFilter
    {
        public string Text { get; set; } = string.Empty;        // record ID, patient, user or details (every word must match)
        public string Action { get; set; } = string.Empty;      // "" = all
        public string EntityType { get; set; } = string.Empty;  // "" = all
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int Limit { get; set; } = 500;
    }
}
