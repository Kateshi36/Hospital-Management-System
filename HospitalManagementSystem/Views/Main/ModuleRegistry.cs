using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Views.MP01_PatientRegistration;
using HospitalManagementSystem.Views.MP02_PatientSearchEditor;
using HospitalManagementSystem.Views.MP03_DoctorScheduling;
using HospitalManagementSystem.Views.MP04_AppointmentBooking;
using HospitalManagementSystem.Views.MP05_QueueBoard;
using HospitalManagementSystem.Views.MP06_ConsultationVitals;
using HospitalManagementSystem.Views.MP07_Prescription;
using HospitalManagementSystem.Views.MP08_MedicineStock;
using HospitalManagementSystem.Views.MP09_Dispensing;
using HospitalManagementSystem.Views.MP10_Laboratory;
using HospitalManagementSystem.Views.MP11_Billing;
using HospitalManagementSystem.Views.MP12_Payment;
using HospitalManagementSystem.Views.Shared;

namespace HospitalManagementSystem.Views.Main
{
    /// <summary>One screen that can be opened from the sidebar.</summary>
    public sealed class ModuleInfo
    {
        public string Key { get; }
        public string Title { get; }

        /// <summary>Roles allowed to see this module. Empty = everybody. (Administrator always sees everything.)</summary>
        public string[] Roles { get; }

        private readonly Func<Form> _factory;

        public ModuleInfo(string key, string title, string[] roles, Func<Form> factory)
        {
            Key = key;
            Title = title;
            Roles = roles;
            _factory = factory;
        }

        /// <summary>Creates a new instance of the module's screen.</summary>
        public Form CreateForm() => _factory();
    }

    /// <summary>
    /// The list of all modules. MainForm builds tabs from this list.
    /// Each build step replaces one "Planned(...)" line with the real form,
    /// e.g.  new ModuleInfo("MP01", "Patient Registration", Reception, () => new PatientRegistrationForm())
    /// </summary>
    public static class ModuleRegistry
    {
        private static readonly string[] Reception = { SessionManager.RoleReceptionist };
        private static readonly string[] ReceptionAndDoctor = { SessionManager.RoleReceptionist, SessionManager.RoleDoctor };
        private static readonly string[] DoctorOnly = { SessionManager.RoleDoctor };
        private static readonly string[] Pharmacy = { SessionManager.RolePharmacist };
        private static readonly string[] Lab = { SessionManager.RoleLabTechnician, SessionManager.RoleDoctor };
        private static readonly string[] Billing = { SessionManager.RoleCashier };

        private static readonly List<ModuleInfo> Modules = new List<ModuleInfo>
        {
            new ModuleInfo("MP01", "Patient Registration", Reception, () => new PatientRegistrationForm()),
            new ModuleInfo("MP02", "Patient Search & Profile", ReceptionAndDoctor, () => new PatientSearchForm()),
            new ModuleInfo("MP03", "Doctor Directory & Schedule", Reception, () => new DoctorManagementForm()),
            new ModuleInfo("MP04", "Appointment Booking", Reception, () => new AppointmentBookingForm()),
            new ModuleInfo("MP05", "Patient Queue Board", ReceptionAndDoctor, () => new QueueBoardForm()),
            new ModuleInfo("MP06", "Consultations & Vitals", DoctorOnly, () => new ConsultationForm()),
            new ModuleInfo("MP07", "Prescription Builder", DoctorOnly, () => new PrescriptionForm()),
            new ModuleInfo("MP08", "Medicine Catalog & Stock", Pharmacy, () => new MedicineStockForm()),
            new ModuleInfo("MP09", "Medicine Dispensing", Pharmacy, () => new DispensingForm()),
            new ModuleInfo("MP10", "Laboratory", Lab, () => new LaboratoryForm()),
            new ModuleInfo("MP11", "Itemized Billing", Billing, () => new BillingForm()),
            new ModuleInfo("MP12", "Payments & Receipts", Billing, () => new PaymentForm())
        };

        public static IReadOnlyList<ModuleInfo> All => Modules;

        public static ModuleInfo? Find(string key)
        {
            return Modules.FirstOrDefault(m => m.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>A module that is not built yet: opens a friendly "coming soon" page.</summary>
        private static ModuleInfo Planned(string key, string title, string[] roles, int buildStep, string description)
        {
            return new ModuleInfo(key, title, roles, () => new ModulePlaceholderForm(title, key, buildStep, description));
        }
    }
}