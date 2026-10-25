namespace HospitalManagementSystem.Models.MP05_QueueBoard
{
    /// <summary>The values of queue_entries.queue_status (an ENUM, see schema.sql Section 5).</summary>
    public static class QueueStatus
    {
        public const string Waiting = "Waiting";
        public const string InProgress = "In Progress";
        public const string Completed = "Completed";
        public const string Skipped = "Skipped";       // allowed by the ENUM; this app sends a skipped patient back to Waiting
    }

    /// <summary>One patient on a doctor's queue for today.</summary>
    public class QueueEntryRecord
    {
        public int QueueId { get; set; }
        public int TokenNumber { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string DoctorId { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;

        /// <summary>APT-... of the appointment, or null for a walk-in.</summary>
        public string? AppointmentId { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsEmergency { get; set; }
        public DateTime CheckedInAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        /// <summary>Reason written when the appointment was booked (empty for walk-ins); shown in the Now Serving panel.</summary>
        public string VisitReason { get; set; } = string.Empty;

        /// <summary>How many times the patient was skipped (rows in queue_skip_log).</summary>
        public int SkipCount { get; set; }

        /// <summary>1-based place among the waiting patients (0 when the patient is not waiting).</summary>
        public int Position { get; set; }

        /// <summary>Position x the doctor's average consultation minutes (0 when not waiting).</summary>
        public int EstimatedWaitMinutes { get; set; }

        public string TokenText => TokenNumber.ToString("000");

        /// <summary>"Emergency", "Appointment" or "Walk-in".</summary>
        public string TypeText => IsEmergency ? "Emergency" : (AppointmentId != null ? "Appointment" : "Walk-in");

        public string CheckedInText => CheckedInAt.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);

        public string SkipText => SkipCount == 0 ? string.Empty : $"Skipped x{SkipCount}";

        public string EstimatedWaitText => EstimatedWaitMinutes <= 0 ? string.Empty : FormatMinutes(EstimatedWaitMinutes);

        public static string FormatMinutes(int minutes)
        {
            if (minutes < 60) return $"{minutes} min";
            return $"{minutes / 60} h {minutes % 60:00} min";
        }
    }

    /// <summary>Running numbers shown in the stats panel (one doctor, today).</summary>
    public class QueueStats
    {
        public int Completed { get; set; }
        public int Waiting { get; set; }
        public int EmergenciesWaiting { get; set; }

        /// <summary>Average of (completed_at - started_at) in minutes; the default setting while nobody has finished yet.</summary>
        public double AverageMinutes { get; set; }

        /// <summary>True when AverageMinutes is the default (no consultation completed yet today).</summary>
        public bool AverageIsDefault { get; set; }

        public string AverageText => AverageIsDefault
            ? $"{AverageMinutes:0.#} min (default)"
            : $"{AverageMinutes:0.#} min";
    }

    /// <summary>Everything the board needs for one doctor, loaded in one go.</summary>
    public class QueueBoard
    {
        public QueueEntryRecord? NowServing { get; set; }

        /// <summary>Waiting patients in CALL ORDER (index 0 is called next).</summary>
        public List<QueueEntryRecord> Waiting { get; set; } = new List<QueueEntryRecord>();

        public QueueStats Stats { get; set; } = new QueueStats();
    }

    /// <summary>A Booked appointment of today that has not checked in yet.</summary>
    public class CheckInCandidate
    {
        public string AppointmentId { get; set; } = string.Empty;
        public int TokenNumber { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public TimeSpan TimeSlot { get; set; }
        public string TimeText => DateTime.Today.Add(TimeSlot).ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
    }

    /// <summary>Result of a Mark Complete.</summary>
    public class CompleteResult
    {
        public QueueEntryRecord Entry { get; set; } = new QueueEntryRecord();
        public double MinutesTaken { get; set; }
        public double NewAverageMinutes { get; set; }
    }
}