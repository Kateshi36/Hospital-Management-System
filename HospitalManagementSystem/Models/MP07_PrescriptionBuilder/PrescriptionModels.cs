using System.Globalization;

namespace HospitalManagementSystem.Models.MP07_Prescription
{
    /// <summary>The limits of Step 11. The form and the repository both use these constants.</summary>
    public static class PrescriptionRules
    {
        public const int FrequencyMax = 24;         // times per day
        public const int DurationMax = 365;         // days
        public const int MaxDosage = 50;
        public const int MaxLineInstructions = 250;
        public const int MaxGeneralInstructions = 1000;
        public const int MaxValidityDays = 365;     // Valid Until can be at most one year after the issue date
        public const int ConsultationLookbackDays = 60;
    }

    /// <summary>A recent consultation a prescription can be written for (the picker list).</summary>
    public class ConsultationChoice
    {
        public string ConsultationId { get; set; } = string.Empty;
        public DateTime RecordedAt { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Allergies { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public int PrescriptionCount { get; set; }

        public string RecordedText => RecordedAt.ToString("MMM d, hh:mm tt", CultureInfo.InvariantCulture);
        public string PrescriptionCountText => PrescriptionCount == 0 ? "-" : PrescriptionCount.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>A medicine of the catalog with the stock that can still be used (batches that have not expired).</summary>
    public class MedicineChoice
    {
        /// <summary>MED-XXX; empty for the "select" placeholder.</summary>
        public string MedicineId { get; set; } = string.Empty;
        public string MedicineName { get; set; } = string.Empty;
        public int UsableStock { get; set; }
        public string StockStatus { get; set; } = string.Empty;

        public string Display => MedicineId.Length == 0 ? MedicineName : $"{MedicineName}   (stock: {UsableStock})";
    }

    /// <summary>One medicine line of a prescription. Quantity is always frequency x duration.</summary>
    public class PrescriptionLine
    {
        public string MedicineId { get; set; } = string.Empty;
        public string MedicineName { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public int FrequencyPerDay { get; set; }
        public int DurationDays { get; set; }
        public string Instructions { get; set; } = string.Empty;

        /// <summary>Usable stock when the line was added (used only for the warning, never blocks saving).</summary>
        public int AvailableStock { get; set; }

        public int Quantity => FrequencyPerDay * DurationDays;
        public int ShortBy => Math.Max(0, Quantity - AvailableStock);
        public string StockCheckText => ShortBy > 0 ? $"Short by {ShortBy}" : "In stock";
    }

    /// <summary>What the doctor entered (the repository adds the prescription id and the issue date).</summary>
    public class PrescriptionInput
    {
        public string ConsultationId { get; set; } = string.Empty;
        public string GeneralInstructions { get; set; } = string.Empty;
        public DateTime ValidUntil { get; set; }
        public List<PrescriptionLine> Lines { get; set; } = new List<PrescriptionLine>();
    }

    /// <summary>A saved prescription with everything the print-style preview needs.</summary>
    public class PrescriptionRecord
    {
        /// <summary>PRE-YYYYMMDD-XXX</summary>
        public string PrescriptionId { get; set; } = string.Empty;
        public string ConsultationId { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Allergies { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public string GeneralInstructions { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public DateTime ValidUntil { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public int LineCount { get; set; }
        public List<PrescriptionLine> Lines { get; set; } = new List<PrescriptionLine>();

        public string IssuedText => IssuedDate.ToString("MMM d, yyyy", CultureInfo.InvariantCulture);
        public string ValidUntilText => ValidUntil.ToString("MMM d, yyyy", CultureInfo.InvariantCulture);
        public string DoctorText => "Dr. " + DoctorName;
    }
}