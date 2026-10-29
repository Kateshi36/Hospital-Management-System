using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP11_Billing;

namespace HospitalManagementSystem.Views.MP11_Billing
{
    /// <summary>
    /// MP11 - asks for the reason before a finalized bill is voided, and whether a corrected copy should be created
    /// as a new draft. The dialog does not void anything itself: the caller calls BillingRepository.VoidBill, so the
    /// repository stays the only place that changes data. All visuals live in VoidBillDialog.Designer.cs.
    /// </summary>
    public partial class VoidBillDialog : Form
    {
        private readonly FormValidator _validator;

        /// <summary>The reason typed (valid only when the dialog result is OK).</summary>
        public string Reason { get; private set; } = string.Empty;

        /// <summary>True when a corrected copy should be created as a new draft.</summary>
        public bool CreateCorrection { get; private set; }

        public VoidBillDialog(string billId, string patientName, decimal grandTotal)
        {
            InitializeComponent();
            FormHelper.Prepare(this);

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtReason);
            InputHelper.Attach(txtReason, InputKind.Address, BillingRules.MaxVoidReason);

            lblTitle.Text = "Void bill " + billId;
            lblInfo.Text =
                $"Patient:  {patientName}\nGrand total:  {SettingsHelper.FormatMoney(grandTotal)}\n\n" +
                "A voided bill is kept as a record (with your reason, your name and the time) but no longer counts as owed. " +
                "Its consultation, medicine and lab charges become unbilled again, so they can go on a new bill.";

            btnConfirm.Click += (s, e) => Confirm();
            btnKeep.Click += (s, e) => DialogResult = DialogResult.Cancel;
            CancelButton = btnKeep;
            Shown += (s, e) => txtReason.Focus();
        }

        private void Confirm()
        {
            _validator.Begin();
            string reason = txtReason.Text.Trim();
            _validator.Check(reason.Length >= BillingRules.MinVoidReason, txtReason,
                $"Enter the reason for voiding the bill (at least {BillingRules.MinVoidReason} characters), for example \"Wrong patient selected\".");
            if (!_validator.IsValid)
            {
                _validator.ShowErrors(this);
                return;
            }

            Reason = reason;
            CreateCorrection = chkCorrection.Checked;
            DialogResult = DialogResult.OK;
        }
    }
}
