using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP04_AppointmentBooking;

namespace HospitalManagementSystem.Views.MP04_AppointmentBooking
{
    /// <summary>
    /// MP04 (Part 8B) - asks for the reason before an appointment is cancelled.
    /// Shows who and when, requires a reason (max 250 characters) and returns it in <see cref="Reason"/>
    /// when the user confirms. The dialog does NOT cancel anything itself: the caller does, so the repository
    /// stays the only place that changes data.
    ///
    /// All visuals live in AppointmentCancelDialog.Designer.cs; this file only holds behaviour.
    /// </summary>
    public partial class AppointmentCancelDialog : Form
    {
        private readonly FormValidator _validator;

        /// <summary>The reason the user typed (valid only when the dialog result is OK).</summary>
        public string Reason { get; private set; } = string.Empty;

        public AppointmentCancelDialog(AppointmentRecord appointment)
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtReason);

            // Letters, digits, space and , . - # / ' ( ) : the same characters every free-text box accepts.
            InputHelper.Attach(txtReason, InputKind.Address, 250);

            lblInfo.Text =
                $"Appointment:  {appointment.AppointmentId}   (token {appointment.TokenNumber})\n" +
                $"Patient:  {appointment.PatientName}   |   {appointment.PatientId}\n" +
                $"Doctor:  {appointment.DoctorName}\n" +
                $"When:  {appointment.WhenText}\n" +
                "The time slot becomes free for other patients.";

            btnConfirm.Click += (s, e) => Confirm();
            btnKeep.Click += (s, e) => DialogResult = DialogResult.Cancel;
            Shown += (s, e) => txtReason.Focus();
        }

        private void Confirm()
        {
            _validator.Begin();

            string reason = txtReason.Text.Trim();
            _validator.Check(reason.Length > 0, txtReason, "Enter the reason for the cancellation.");

            if (!_validator.IsValid)
            {
                _validator.ShowErrors(this);
                return;
            }

            Reason = reason;
            DialogResult = DialogResult.OK;
        }
    }
}