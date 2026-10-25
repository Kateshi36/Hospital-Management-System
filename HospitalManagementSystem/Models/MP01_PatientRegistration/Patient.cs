namespace HospitalManagementSystem.Models.MP01_PatientRegistration
{
    /// <summary>A patient with every detail captured at registration.</summary>
    public class Patient
    {
        /// <summary>PAT-YYYYMMDD-XXX - generated on registration; also the primary key.</summary>
        public string PatientId { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;
        public string BloodType { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string StreetAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        public string Allergies { get; set; } = string.Empty;
        public string MedicalHistory { get; set; } = string.Empty;

        public string EmergencyContactName { get; set; } = string.Empty;
        public string EmergencyContactPhone { get; set; } = string.Empty;
        public string EmergencyContactRelation { get; set; } = string.Empty;

        public DateTime RegisteredAt { get; set; }

        /// <summary>Age in whole years, computed from the date of birth (never typed or stored).</summary>
        public int Age => CalculateAge(DateOfBirth, DateTime.Today);

        /// <summary>"Maria Clara Santos"</summary>
        public string FullName =>
            string.Join(" ", new[] { FirstName, MiddleName, LastName }.Where(part => !string.IsNullOrWhiteSpace(part)));

        /// <summary>True when an emergency contact was entered (all three parts are required together).</summary>
        public bool HasEmergencyContact =>
            !string.IsNullOrWhiteSpace(EmergencyContactName) &&
            !string.IsNullOrWhiteSpace(EmergencyContactPhone) &&
            !string.IsNullOrWhiteSpace(EmergencyContactRelation);

        /// <summary>Whole years between two dates (birthday-aware).</summary>
        public static int CalculateAge(DateTime dateOfBirth, DateTime today)
        {
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return Math.Max(age, 0);
        }
    }
}