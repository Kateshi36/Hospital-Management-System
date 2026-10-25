using System.Globalization;
using System.Text;

namespace HospitalManagementSystem.Models.MP06_ConsultationVitals
{
    /// <summary>The numeric limits of Step 10. The form and the repository both use these constants.</summary>
    public static class ConsultationRules
    {
        public const int SystolicMin = 50, SystolicMax = 250;
        public const int DiastolicMin = 30, DiastolicMax = 150;
        public const decimal TemperatureMin = 30m, TemperatureMax = 45m;
        public const int PulseMin = 20, PulseMax = 250;

        /// <summary>Weight must be greater than 0; the upper limit only catches typing mistakes.</summary>
        public const decimal WeightMax = 500m;

        /// <summary>Height is optional; when entered it must be in this range (typing-mistake guard).</summary>
        public const decimal HeightMin = 30m, HeightMax = 250m;

        public const int MaxComplaint = 500;
        public const int MaxFindings = 1000;
        public const int MaxDiagnosis = 500;
        public const int MaxPlan = 1000;
        public const int MaxInstructions = 500;
    }

    /// <summary>An appointment of today that has no consultation yet (the picker list).</summary>
    public class AppointmentChoice
    {
        public string AppointmentId { get; set; } = string.Empty;
        public int TokenNumber { get; set; }
        public TimeSpan TimeSlot { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;

        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Allergies { get; set; } = string.Empty;

        public string DoctorId { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;

        /// <summary>Queue board state of the visit: Waiting, In Progress, Completed or empty (not arrived).</summary>
        public string QueueStatus { get; set; } = string.Empty;

        public string TimeText => DateTime.Today.Add(TimeSlot).ToString("hh:mm tt", CultureInfo.InvariantCulture);
        public string QueueText => string.IsNullOrEmpty(QueueStatus) ? "Not arrived" : QueueStatus;
    }

    /// <summary>What the doctor typed (the repository adds the consultation id and the time stamp).</summary>
    public class ConsultationInput
    {
        public string AppointmentId { get; set; } = string.Empty;
        public int SystolicBp { get; set; }
        public int DiastolicBp { get; set; }
        public decimal TemperatureC { get; set; }
        public int PulseRateBpm { get; set; }
        public decimal WeightKg { get; set; }
        public decimal? HeightCm { get; set; }
        public string ChiefComplaint { get; set; } = string.Empty;
        public string ExaminationFindings { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public string TreatmentPlan { get; set; } = string.Empty;
        public string SpecialInstructions { get; set; } = string.Empty;
        public DateTime? NextFollowUpDate { get; set; }
    }

    /// <summary>A saved consultation with the names needed for the history grid and the chart note.</summary>
    public class ConsultationRecord
    {
        /// <summary>CON-YYYYMMDD-XXX</summary>
        public string ConsultationId { get; set; } = string.Empty;
        public string AppointmentId { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }

        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public int AgeAtVisit { get; set; }
        public string Allergies { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;

        public int SystolicBp { get; set; }
        public int DiastolicBp { get; set; }
        public decimal TemperatureC { get; set; }
        public int PulseRateBpm { get; set; }
        public decimal WeightKg { get; set; }
        public decimal? HeightCm { get; set; }

        public string ChiefComplaint { get; set; } = string.Empty;
        public string ExaminationFindings { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public string TreatmentPlan { get; set; } = string.Empty;
        public string SpecialInstructions { get; set; } = string.Empty;
        public DateTime? NextFollowUpDate { get; set; }
        public DateTime RecordedAt { get; set; }
        public string RecordedBy { get; set; } = string.Empty;

        public string RecordedText => RecordedAt.ToString("MMM d, yyyy  hh:mm tt", CultureInfo.InvariantCulture);
        public string DoctorText => "Dr. " + DoctorName;
        public string BloodPressureText => $"{SystolicBp}/{DiastolicBp} mmHg";

        /// <summary>Body-mass index when the height is known (weight / height in metres squared), otherwise empty.</summary>
        public string BmiText
        {
            get
            {
                if (!HeightCm.HasValue || HeightCm.Value <= 0) return string.Empty;
                decimal metres = HeightCm.Value / 100m;
                return (WeightKg / (metres * metres)).ToString("0.0", CultureInfo.InvariantCulture);
            }
        }
    }
}