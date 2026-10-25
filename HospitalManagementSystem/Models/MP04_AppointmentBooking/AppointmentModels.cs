using HospitalManagementSystem.Models.MP03_DoctorScheduling;

namespace HospitalManagementSystem.Models.MP04_AppointmentBooking
{
    /// <summary>The values of appointments.status (an ENUM in the database).</summary>
    public static class AppointmentStatus
    {
        public const string Booked = "Booked";
        public const string Completed = "Completed";
        public const string Cancelled = "Cancelled";
        public const string NoShow = "No-show";

        public static readonly string[] All = { Booked, Completed, Cancelled, NoShow };
    }

    /// <summary>The values of appointments.appointment_type (an ENUM in the database).</summary>
    public static class AppointmentType
    {
        public const string NewConsultation = "New Consultation";
        public const string FollowUp = "Follow-up";

        public static readonly string[] All = { NewConsultation, FollowUp };
    }

    /// <summary>A doctor as shown in the booking drop-down.</summary>
    public class DoctorOption
    {
        public string DoctorId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public decimal ConsultationFee { get; set; }

        public string DisplayName => "Dr. " + FullName;

        /// <summary>Text shown in the drop-down: "Dr. Ana Reyes  -  Cardiology".</summary>
        public string Display => $"{DisplayName}  -  {Specialization}";

        public override string ToString() => Display;
    }

    /// <summary>One consultation slot of a doctor on one date.</summary>
    public class SlotInfo
    {
        public TimeSpan Time { get; set; }
        public int Minutes { get; set; }

        /// <summary>Another Booked / Completed / No-show appointment already uses this time.</summary>
        public bool IsTaken { get; set; }

        /// <summary>The date is today and the slot start time has already passed.</summary>
        public bool IsPast { get; set; }

        public bool IsBookable => !IsTaken && !IsPast;

        /// <summary>8:00 AM style text (also what the time drop-down shows).</summary>
        public string Text => ScheduleBlock.FormatTime(Time);

        public override string ToString() => Text;
    }

    /// <summary>
    /// What a doctor offers on one calendar date: the slots computed from the weekly blocks of that
    /// day of the week, minus leave and minus slots that are already booked.
    /// </summary>
    public class DayAvailability
    {
        public string DoctorId { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        /// <summary>The doctor has at least one weekly block on this day of the week.</summary>
        public bool WorksThisDay { get; set; }

        public bool OnLeave { get; set; }
        public string LeaveReason { get; set; } = string.Empty;

        /// <summary>"Mon, Wed, Fri" - every day the doctor has a block (empty text = no weekly schedule at all).</summary>
        public string WorkingDaysText { get; set; } = string.Empty;

        /// <summary>"8:00 AM - 12:00 PM, 1:00 PM - 4:00 PM" for this date.</summary>
        public string WorkingHoursText { get; set; } = string.Empty;

        /// <summary>Consultation length of the first block of the day (for the hint text).</summary>
        public int Minutes { get; set; }

        public List<SlotInfo> Slots { get; set; } = new List<SlotInfo>();

        public int OpenCount => Slots.Count(s => s.IsBookable);
        public int TakenCount => Slots.Count(s => s.IsTaken);
        public int PastCount => Slots.Count(s => s.IsPast && !s.IsTaken);
    }

    /// <summary>Everything needed to save a new appointment.</summary>
    public class BookingRequest
    {
        public string PatientId { get; set; } = string.Empty;
        public string DoctorId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TimeSpan TimeSlot { get; set; }
        public string AppointmentType { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }

    /// <summary>What the database generated while saving.</summary>
    public class BookingResult
    {
        /// <summary>APT-YYYYMMDD-XXX</summary>
        public string AppointmentId { get; set; } = string.Empty;
        public int TokenNumber { get; set; }
    }

    /// <summary>Filters of the appointments grid. A null / blank value means "do not filter by this".</summary>
    public class AppointmentFilter
    {
        public DateTime? Date { get; set; }
        public string? DoctorId { get; set; }
        public string PatientText { get; set; } = string.Empty;
        public string? Status { get; set; }
    }

    /// <summary>The four actions of Part 8B that change an existing appointment.</summary>
    public enum AppointmentAction
    {
        Cancel,
        Reschedule,
        Complete,
        NoShow
    }

    /// <summary>One saved appointment, as needed by the Cancel / Reschedule / Complete / No-show actions.</summary>
    public class AppointmentRecord
    {
        public string AppointmentId { get; set; } = string.Empty;
        public int TokenNumber { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string DoctorId { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TimeSpan TimeSlot { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;

        /// <summary>Date and time the visit starts.</summary>
        public DateTime StartsAt => Date.Date + TimeSlot;

        /// <summary>"Mon, Oct 19, 2026  8:00 AM".</summary>
        public string WhenText => FormatWhen(Date, TimeSlot);

        public static string FormatWhen(DateTime date, TimeSpan time)
        {
            return date.ToString("ddd, MMM d, yyyy", System.Globalization.CultureInfo.InvariantCulture)
                   + "  " + ScheduleBlock.FormatTime(time);
        }
    }

    /// <summary>What a successful reschedule changed.</summary>
    public class RescheduleResult
    {
        public string AppointmentId { get; set; } = string.Empty;
        public string OldWhenText { get; set; } = string.Empty;
        public string NewWhenText { get; set; } = string.Empty;

        /// <summary>The date changed, so the appointment received the next token of that doctor on the new date.</summary>
        public bool TokenChanged { get; set; }
        public int NewToken { get; set; }
    }
}