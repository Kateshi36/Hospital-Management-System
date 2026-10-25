using System.Globalization;
using HospitalManagementSystem.Helpers;

namespace HospitalManagementSystem.Models.MP09_Dispensing
{
    /// <summary>The limits of Step 13. The form and the repository both use these constants.</summary>
    public static class DispensingRules
    {
        public const int PrescriptionLookbackDays = 90;   // the picker lists prescriptions issued in the last 90 days
        public const int PrescriptionListLimit = 200;
        public const int HistoryLimit = 300;
    }

    /// <summary>A prescription of the picker list, with how much of it has been dispensed so far.</summary>
    public class PrescriptionChoice
    {
        public string PrescriptionId { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public DateTime ValidUntil { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Allergies { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public int LineCount { get; set; }
        public int QuantityPrescribed { get; set; }
        public int QuantityDispensed { get; set; }
        public int QuantityRemaining { get; set; }

        public bool IsFullyDispensed => QuantityRemaining <= 0;
        public bool IsExpired => ValidUntil.Date < DateTime.Today;
        public bool CanDispense => !IsFullyDispensed && !IsExpired;

        /// <summary>Fully Dispensed (checked first), Expired, Partially Dispensed or Ready.</summary>
        public string StatusText =>
            IsFullyDispensed ? "Fully Dispensed" :
            IsExpired ? "Expired" :
            QuantityDispensed > 0 ? "Partially Dispensed" : "Ready";

        /// <summary>The plain-words reason why a prescription cannot be dispensed (empty when it can).</summary>
        public string BlockedMessage =>
            IsFullyDispensed ? $"Prescription {PrescriptionId} has already been fully dispensed and cannot be dispensed again." :
            IsExpired ? $"Prescription {PrescriptionId} EXPIRED on {ValidUntilText}. An expired prescription cannot be dispensed - the patient needs a new prescription." :
            string.Empty;

        public string IssuedText => IssuedDate.ToString("MMM d, yyyy", CultureInfo.InvariantCulture);
        public string ValidUntilText => ValidUntil.ToString("MMM d, yyyy", CultureInfo.InvariantCulture);
        public string DoctorText => "Dr. " + DoctorName;
        public string ProgressText => $"{QuantityDispensed} / {QuantityPrescribed}";
    }

    /// <summary>One medicine line of the prescription being dispensed (the editable value is QuantityToDispense).</summary>
    public class DispenseLine
    {
        public int PrescriptionItemId { get; set; }
        public string MedicineId { get; set; } = string.Empty;
        public string MedicineName { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public int QuantityPrescribed { get; set; }
        public int QuantityAlreadyDispensed { get; set; }
        public int QuantityRemaining { get; set; }

        /// <summary>Stock that can still be used (batches that have not expired).</summary>
        public int UsableStock { get; set; }
        public decimal UnitPrice { get; set; }
        public int QuantityToDispense { get; set; }

        /// <summary>Highest quantity allowed now: never more than the remaining prescribed quantity and never more than the stock.</summary>
        public int MaxQuantity => Math.Max(0, Math.Min(QuantityRemaining, UsableStock));
        public int ShortBy => Math.Max(0, QuantityRemaining - UsableStock);
        public decimal LineTotal => QuantityToDispense * UnitPrice;

        public string CheckText
        {
            get
            {
                if (QuantityRemaining <= 0) return "Already dispensed";
                if (QuantityToDispense <= 0) return UsableStock <= 0 ? "Out of stock" : "Not dispensing now";
                if (ShortBy > 0) return $"Short by {ShortBy} (partial)";
                if (QuantityToDispense < QuantityRemaining) return "Partial (reduced)";
                return "Full";
            }
        }

        public string UnitPriceText => SettingsHelper.FormatMoney(UnitPrice);
        public string LineTotalText => SettingsHelper.FormatMoney(LineTotal);
    }

    /// <summary>One line of the request: which prescription item and how many units.</summary>
    public class DispenseRequestLine
    {
        public int PrescriptionItemId { get; set; }
        public int Quantity { get; set; }
    }

    /// <summary>What the pharmacist confirmed (the repository adds the dispensing id, the price snapshot and the stock movements).</summary>
    public class DispenseRequest
    {
        public string PrescriptionId { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public List<DispenseRequestLine> Lines { get; set; } = new List<DispenseRequestLine>();
    }

    /// <summary>One medicine row of a dispensing slip.</summary>
    public class DispensingSlipItem
    {
        public string MedicineName { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }

        public string UnitPriceText => SettingsHelper.FormatMoney(UnitPrice);
        public string LineTotalText => SettingsHelper.FormatMoney(LineTotal);
    }

    /// <summary>A saved dispensing (history grid row and everything the print-style slip needs).</summary>
    public class DispensingSlip
    {
        /// <summary>MS-YYYYMMDD-XXX</summary>
        public string DispensingId { get; set; } = string.Empty;
        public string PrescriptionId { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string DispensedBy { get; set; } = string.Empty;
        public DateTime DispensedAt { get; set; }
        public decimal Total { get; set; }
        public int ItemCount { get; set; }

        /// <summary>Units of the prescription that are still waiting to be dispensed (0 = fully dispensed).</summary>
        public int RemainingUnits { get; set; }
        public List<DispensingSlipItem> Items { get; set; } = new List<DispensingSlipItem>();

        public string DispensedAtText => DispensedAt.ToString("MMM d, yyyy hh:mm tt", CultureInfo.InvariantCulture);
        public string TotalText => SettingsHelper.FormatMoney(Total);
        public string DoctorText => "Dr. " + DoctorName;
        public string PrescriptionStatusText => RemainingUnits <= 0
            ? "Prescription fully dispensed"
            : $"Partially dispensed - {RemainingUnits} unit(s) still to dispense";
    }
}