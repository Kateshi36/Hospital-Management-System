using System.Globalization;

namespace HospitalManagementSystem.Models.MP03_DoctorScheduling
{
    /// <summary>
    /// One weekly availability block of a doctor (for example Monday 8:00 AM - 12:00 PM, 15-minute consultations).
    /// The database stores the day as 0 = Sunday ... 6 = Saturday, which is exactly System.DayOfWeek.
    /// </summary>
    public class ScheduleBlock
    {
        /// <summary>Days in the order the weekly grid shows them: Monday first, Sunday last.</summary>
        public static readonly DayOfWeek[] WeekOrder =
        {
            DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday,
            DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday
        };

        public int ScheduleId { get; set; }
        public string DoctorId { get; set; } = string.Empty;
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int ConsultationMinutes { get; set; }
        public string Notes { get; set; } = string.Empty;

        public string DayName => DayLabel(Day);
        public int DurationMinutes => (int)(EndTime - StartTime).TotalMinutes;
        public decimal Hours => Math.Round(DurationMinutes / 60m, 2);

        /// <summary>How many whole consultations fit in the block (used later by appointment booking).</summary>
        public int SlotCount => ConsultationMinutes > 0 ? DurationMinutes / ConsultationMinutes : 0;

        public string TimeRangeText => $"{FormatTime(StartTime)} - {FormatTime(EndTime)}";

        public static string DayLabel(DayOfWeek day) => day.ToString();

        /// <summary>Three-letter day name used as a grid heading (Mon, Tue ...).</summary>
        public static string ShortDayLabel(DayOfWeek day) => day.ToString().Substring(0, 3);

        /// <summary>8:00 AM style text (always English AM/PM, whatever the Windows language is).</summary>
        public static string FormatTime(TimeSpan time) =>
            DateTime.Today.Add(time).ToString("h:mm tt", CultureInfo.InvariantCulture);
    }

    /// <summary>A calendar date on which a doctor is unavailable (leave, conference, holiday ...).</summary>
    public class LeaveDate
    {
        public int LeaveId { get; set; }
        public string DoctorId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Reason { get; set; } = string.Empty;
    }

    /// <summary>Which input of the block form a validation problem belongs to (so it can be marked in red).</summary>
    public enum ScheduleField
    {
        Day,
        StartTime,
        EndTime,
        Minutes
    }

    /// <summary>One problem found while checking a schedule block.</summary>
    public class ScheduleProblem
    {
        public ScheduleProblem(ScheduleField field, string message)
        {
            Field = field;
            Message = message;
        }

        public ScheduleField Field { get; }
        public string Message { get; }
    }
}