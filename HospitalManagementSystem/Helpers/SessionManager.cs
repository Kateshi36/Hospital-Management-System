using HospitalManagementSystem.Models.Common;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// Remembers who is logged in. Set by the Login screen, read everywhere else
    /// (e.g. "created by" / "received by" columns and role checks).
    /// </summary>
    public static class SessionManager
    {
        public const string RoleAdministrator = "Administrator";
        public const string RoleReceptionist = "Receptionist";
        public const string RoleDoctor = "Doctor";
        public const string RolePharmacist = "Pharmacist";
        public const string RoleLabTechnician = "Lab Technician";
        public const string RoleCashier = "Cashier";

        public static User? CurrentUser { get; private set; }
        public static DateTime? LoggedInAt { get; private set; }

        public static bool IsLoggedIn => CurrentUser != null;

        /// <summary>Current user's id. Throws if nobody is logged in (a programming error).</summary>
        public static int CurrentUserId =>
            CurrentUser?.UserId ?? throw new InvalidOperationException("No user is logged in.");

        public static string CurrentUserName => CurrentUser?.FullName ?? "(not logged in)";

        public static bool IsAdministrator =>
            CurrentUser != null && CurrentUser.Role.Equals(RoleAdministrator, StringComparison.OrdinalIgnoreCase);

        public static void Start(User user)
        {
            CurrentUser = user;
            LoggedInAt = DateTime.Now;
        }

        public static void End()
        {
            CurrentUser = null;
            LoggedInAt = null;
        }

        /// <summary>True when the user holds any of the given roles (Administrator always passes).</summary>
        public static bool HasRole(params string[] roles)
        {
            if (CurrentUser == null) return false;
            if (IsAdministrator) return true;
            return roles.Any(r => r.Equals(CurrentUser.Role, StringComparison.OrdinalIgnoreCase));
        }
    }
}