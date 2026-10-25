namespace HospitalManagementSystem.Models.Common
{
    /// <summary>A system user (staff member who logs in). Never holds the password or hash.</summary>
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        /// <summary>Administrator, Receptionist, Doctor, Pharmacist, Lab Technician or Cashier.</summary>
        public string Role { get; set; } = string.Empty;

        public override string ToString() => FullName;
    }
}