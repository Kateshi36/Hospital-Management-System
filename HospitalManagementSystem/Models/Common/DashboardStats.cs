namespace HospitalManagementSystem.Models.Common
{
    /// <summary>The headline numbers shown on the dashboard cards.</summary>
    public class DashboardStats
    {
        public int TotalPatients { get; set; }
        public int RegistrationsToday { get; set; }
        public int AppointmentsToday { get; set; }
        public int CompletedToday { get; set; }
        public int BookedToday { get; set; }
        public int CancelledToday { get; set; }
        public int NoShowToday { get; set; }
        public int EmergenciesWaiting { get; set; }
        public int PaymentsToday { get; set; }
        public int ActiveQueue { get; set; }
        public int DoctorsOnDuty { get; set; }
        public int LabTestsPending { get; set; }
        public decimal RevenueToday { get; set; }
    }
}