namespace HospitalManagementSystem.Models.MP03_DoctorScheduling
{
    /// <summary>A doctor in the directory.</summary>
    public class Doctor
    {
        /// <summary>DOC-001 style id, generated on first save and never changed.</summary>
        public string DoctorId { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;

        public string Specialization { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string EmploymentType { get; set; } = string.Empty;

        /// <summary>Stored form: +639XXXXXXXXX (older seed rows may still hold 0917-555-0001).</summary>
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal ConsultationFee { get; set; }
        public DateTime CreatedAt { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();
        public string DisplayName => "Dr. " + FullName;
    }

    /// <summary>
    /// What deleting a doctor would affect. Schedules and leave dates are removed together with the doctor;
    /// appointments, queue entries and lab orders block the delete (they are patient history).
    /// </summary>
    public class DoctorDeleteImpact
    {
        public int ScheduleBlocks { get; set; }
        public int LeaveDates { get; set; }
        public int Appointments { get; set; }
        public int QueueEntries { get; set; }
        public int LabOrders { get; set; }

        public bool IsBlocked => Appointments > 0 || QueueEntries > 0 || LabOrders > 0;
    }
}