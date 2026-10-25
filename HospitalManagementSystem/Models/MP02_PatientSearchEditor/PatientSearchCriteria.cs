namespace HospitalManagementSystem.Models.MP02_PatientSearchEditor
{
    /// <summary>What the user typed in the search panel. Every field is optional; all filled ones must match (AND).</summary>
    public class PatientSearchCriteria
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }

        /// <summary>True when at least one criterion was entered (an empty search is never run).</summary>
        public bool HasAny =>
            !string.IsNullOrWhiteSpace(Code) ||
            !string.IsNullOrWhiteSpace(Name) ||
            !string.IsNullOrWhiteSpace(Phone) ||
            DateOfBirth.HasValue;
    }
}