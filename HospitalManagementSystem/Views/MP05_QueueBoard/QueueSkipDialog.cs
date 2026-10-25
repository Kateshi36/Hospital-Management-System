using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP05_QueueBoard;

namespace HospitalManagementSystem.Views.MP05_QueueBoard
{
    /// <summary>
    /// MP05 - asks for the reason before the patient who is being seen is skipped.
    /// Returns the reason in <see cref="Reason"/> when the user confirms. The dialog does NOT skip anything itself:
    /// the caller does, so the repository stays the only place that changes data.
    ///
    /// All visuals live in QueueSkipDialog.Designer.cs; this file only holds behaviour.
    /// </summary>
    public partial class QueueSkipDialog : Form
    {
        private readonly FormValidator _validator;

        /// <summary>The reason the user typed (valid only when the dialog result is OK).</summary>
        public string Reason { get; private set; } = string.Empty;

        public QueueSkipDialog(QueueEntryRecord entry)
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtReason);

            InputHelper.Attach(txtReason, InputKind.Address, 250);

            lblInfo.Text =
                $"Token:  {entry.TokenText}   ({entry.TypeText})\n" +
                $"Patient:  {entry.PatientName}   |   {entry.PatientId}\n" +
                $"Doctor:  Dr. {entry.DoctorName}\n" +
                "The patient returns to the waiting list, behind everybody who has not been skipped.\n" +
                (entry.SkipCount > 0 ? $"Already skipped {entry.SkipCount} time(s) today." : string.Empty);

            btnConfirm.Click += (s, e) => Confirm();
            btnKeep.Click += (s, e) => DialogResult = DialogResult.Cancel;
            Shown += (s, e) => txtReason.Focus();
        }

        private void Confirm()
        {
            _validator.Begin();

            string reason = txtReason.Text.Trim();
            _validator.Check(reason.Length > 0, txtReason, "Enter the reason for skipping this patient.");

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