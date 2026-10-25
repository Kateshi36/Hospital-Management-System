using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP03_DoctorScheduling;
using HospitalManagementSystem.Models.MP04_AppointmentBooking;
using HospitalManagementSystem.Repositories.MP04_AppointmentBooking;
using System.Globalization;
using System.Text;

namespace HospitalManagementSystem.Views.MP04_AppointmentBooking
{
    /// <summary>
    /// MP04 (Part 8B) - picks the new date and time slot for a Booked appointment. The doctor stays the same.
    /// The slot list shows only OPEN slots (same computation as booking: weekly blocks of that weekday, minus leave,
    /// minus taken slots, minus times already passed today). The appointment's own current slot is never offered.
    /// The dialog only chooses; the caller saves with AppointmentRepository.Reschedule, which checks every rule again.
    ///
    /// All visuals live in AppointmentRescheduleDialog.Designer.cs; this file only holds behaviour.
    /// </summary>
    public partial class AppointmentRescheduleDialog : Form
    {
        private readonly AppointmentRepository _repository;
        private readonly AppointmentRecord _appointment;
        private readonly FormValidator _validator;
        private bool _loading;

        /// <summary>The chosen date (valid only when the dialog result is OK).</summary>
        public DateTime NewDate { get; private set; }

        /// <summary>The chosen time slot (valid only when the dialog result is OK).</summary>
        public TimeSpan NewTimeSlot { get; private set; }

        public AppointmentRescheduleDialog(AppointmentRepository repository, AppointmentRecord appointment)
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _repository = repository;
            _appointment = appointment;
            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(dtpNewDate, cboNewSlot);

            lblInfo.Text =
                $"Appointment:  {appointment.AppointmentId}   (token {appointment.TokenNumber})\n" +
                $"Patient:  {appointment.PatientName}   |   {appointment.PatientId}\n" +
                $"Doctor:  {appointment.DoctorName}\n" +
                $"Now scheduled:  {appointment.WhenText}";

            dtpNewDate.ValueChanged += (s, e) => RefreshSlots();
            btnConfirm.Click += (s, e) => Confirm();
            btnKeep.Click += (s, e) => DialogResult = DialogResult.Cancel;
            Load += (s, e) => InitializeScreen();
        }

        private void InitializeScreen()
        {
            _loading = true;
            try
            {
                // No past dates and not more than a year ahead (same limits as booking).
                dtpNewDate.MinDate = DateTime.Today;
                dtpNewDate.MaxDate = DateTime.Today.AddDays(AppointmentRepository.MaxAdvanceDays);
                dtpNewDate.Value = _appointment.Date < DateTime.Today ? DateTime.Today : _appointment.Date;
            }
            finally
            {
                _loading = false;
            }
            RefreshSlots();
        }

        /// <summary>Recomputes the open slots of the appointment's doctor on the chosen date.</summary>
        private void RefreshSlots()
        {
            if (_loading) return;

            DateTime date = dtpNewDate.Value.Date;
            var open = new List<SlotInfo>();
            var hint = new StringBuilder();
            bool warning = false;

            try
            {
                DayAvailability day = _repository.GetAvailability(_appointment.DoctorId, date);
                string dayText = date.ToString("dddd, MMM d, yyyy", CultureInfo.InvariantCulture);

                if (day.OnLeave)
                {
                    hint.Append($"{_appointment.DoctorName} is on leave on {dayText} ({day.LeaveReason}). Choose another date.");
                    warning = true;
                }
                else if (!day.WorksThisDay)
                {
                    hint.Append(day.WorkingDaysText.Length == 0
                        ? $"{_appointment.DoctorName} has no weekly schedule."
                        : $"{_appointment.DoctorName} does not see patients on {date.DayOfWeek}s. Working days: {day.WorkingDaysText}.");
                    warning = true;
                }
                else
                {
                    open = day.Slots.Where(s => s.IsBookable).ToList();

                    hint.Append($"Working hours: {day.WorkingHoursText}  ({day.Minutes}-minute visits)\n");
                    if (open.Count == 0)
                    {
                        hint.Append("Fully booked - no open slot left on this date.");
                        warning = true;
                    }
                    else
                    {
                        hint.Append($"{day.OpenCount} open, {day.TakenCount} taken");
                        if (day.PastCount > 0) hint.Append($", {day.PastCount} already passed");
                        hint.Append('.');
                    }

                    if (date == _appointment.Date)
                    {
                        hint.Append($"\nThe current slot ({ScheduleBlock.FormatTime(_appointment.TimeSlot)}) is not offered - choose a different time.");
                    }
                    else if (_repository.PatientHasActiveAppointment(_appointment.PatientId, _appointment.DoctorId, date))
                    {
                        hint.Append("\nThis patient already has an appointment with this doctor on that date.");
                        warning = true;
                    }
                    else
                    {
                        hint.Append("\nThe appointment keeps its code and gets the next token of that date.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The time slots could not be loaded.", this);
                hint.Clear();
            }

            cboNewSlot.DataSource = open;
            cboNewSlot.DisplayMember = nameof(SlotInfo.Text);
            cboNewSlot.SelectedIndex = -1;            // the clerk chooses; nothing is pre-selected

            lblSlotInfo.Text = hint.ToString();
            lblSlotInfo.ForeColor = warning ? ThemeHelper.Warning : ThemeHelper.TextMuted;
        }

        private void Confirm()
        {
            _validator.Begin();

            DateTime date = dtpNewDate.Value.Date;
            var slot = cboNewSlot.SelectedItem as SlotInfo;

            _validator.Check(date >= DateTime.Today, dtpNewDate, "The new date cannot be in the past.");
            _validator.Check(slot != null, cboNewSlot,
                cboNewSlot.Items.Count == 0
                    ? "There is no open time slot on this date. Choose another date."
                    : "Choose the new time slot.");

            if (_validator.IsValid && slot != null && date == _appointment.Date && slot.Time == _appointment.TimeSlot)
            {
                _validator.AddError(cboNewSlot, "That is the current time. Choose a different time.");
            }

            if (!_validator.IsValid)
            {
                _validator.ShowErrors(this);
                return;
            }

            NewDate = date;
            NewTimeSlot = slot!.Time;
            DialogResult = DialogResult.OK;
        }
    }
}