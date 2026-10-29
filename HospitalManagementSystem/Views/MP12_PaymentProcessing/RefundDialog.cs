using System.Globalization;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP12_Payment;
using HospitalManagementSystem.Repositories.MP12_Payment;

namespace HospitalManagementSystem.Views.MP12_Payment
{
    /// <summary>
    /// MP12 - money given back from one payment. The cashier enters the amount (at most what is left of the payment)
    /// and the reason; the refund is made with the same method as the payment. The original payment is never changed:
    /// the caller records a separate reversing entry with PaymentRepository.RecordRefund. All visuals live in RefundDialog.Designer.cs.
    /// </summary>
    public partial class RefundDialog : Form
    {
        private readonly FormValidator _validator;
        private readonly PaymentRecord _payment;

        /// <summary>What was entered (valid only when the dialog result is OK).</summary>
        public RefundInput Input { get; private set; } = new RefundInput();

        public RefundDialog(PaymentRecord payment, string billId, string patientName)
        {
            InitializeComponent();
            FormHelper.Prepare(this);
            _payment = payment;

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtAmount, txtReason);
            InputHelper.Attach(txtAmount, InputKind.Decimal2, 12);
            InputHelper.Attach(txtReason, InputKind.Address, PaymentRules.MaxRemarks);
            ThemeHelper.SetReadOnlyLook(txtMethod, true);

            lblTitle.Text = "Refund payment " + payment.PaymentId;
            lblInfo.Text =
                $"Bill {billId}  ·  {patientName}\n" +
                $"Paid {SettingsHelper.FormatMoney(payment.Amount)} by {payment.MethodName} on {payment.PaidAtText}" +
                (payment.Refunded > 0 ? $"\nAlready refunded: {SettingsHelper.FormatMoney(payment.Refunded)}" : string.Empty);
            txtMethod.Text = payment.MethodName;
            lblNote.Text = $"At most {SettingsHelper.FormatMoney(payment.Refundable)} can be refunded. The payment itself stays on record; " +
                           "the refund is saved as a separate entry with its own receipt, and the bill's balance goes up by the same amount.";

            btnFull.Click += (s, e) => txtAmount.Text = payment.Refundable.ToString("0.00", CultureInfo.InvariantCulture);
            btnConfirm.Click += (s, e) => Confirm();
            btnCancelDialog.Click += (s, e) => DialogResult = DialogResult.Cancel;
            CancelButton = btnCancelDialog;
            Shown += (s, e) => txtAmount.Focus();
        }

        private void Confirm()
        {
            _validator.Begin();
            var input = new RefundInput { PaymentId = _payment.PaymentId, Reason = txtReason.Text.Trim() };

            string amountText = txtAmount.Text.Trim();
            bool parsed = ValidationHelper.TryParseDecimal(amountText, out decimal amount);
            if (amountText.Length == 0) _validator.AddError(txtAmount, "Enter the amount to refund.");
            else if (!parsed) _validator.AddError(txtAmount, "The amount must be a number.");
            else input.Amount = amount;

            foreach (string error in PaymentRepository.ValidateRefund(input, _payment))
            {
                bool aboutAmount = error.Contains("amount", StringComparison.OrdinalIgnoreCase) || error.Contains("At most", StringComparison.Ordinal);
                if (aboutAmount && !parsed) continue;
                _validator.AddError(error.Contains("reason", StringComparison.OrdinalIgnoreCase) ? txtReason : txtAmount, error);
            }

            if (!_validator.IsValid)
            {
                _validator.ShowErrors(this);
                return;
            }

            Input = input;
            DialogResult = DialogResult.OK;
        }
    }
}
