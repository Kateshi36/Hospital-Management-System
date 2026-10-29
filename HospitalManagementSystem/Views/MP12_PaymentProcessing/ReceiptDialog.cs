using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP12_Payment;

namespace HospitalManagementSystem.Views.MP12_Payment
{
    /// <summary>
    /// MP12 - print-style, read-only Official Receipt (white "paper" layout: hospital, receipt and bill numbers, patient,
    /// amount paid, previous payments, new balance, method, reference, cash tendered and change, cashier, date and status).
    /// Nothing is printed; the layout is what matters. It is also used to reprint any earlier receipt, so every figure comes
    /// from the saved payments (see PaymentRepository.GetReceipt). All visuals live in ReceiptDialog.Designer.cs.
    /// </summary>
    public partial class ReceiptDialog : Form
    {
        private static readonly Color Ink = Color.FromArgb(20, 20, 20);
        private static readonly Color Gray = Color.FromArgb(90, 90, 90);
        private const string Rule = "______________________________________________________\n";

        public ReceiptDialog(Receipt receipt)
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon
            btnClose.Click += (s, e) => DialogResult = DialogResult.OK;
            Text = (receipt.IsRefund ? "Refund Receipt " : "Official Receipt ") + receipt.PaymentId;
            Build(receipt);
        }

        private void Build(Receipt r)
        {
            rtbReceipt.Clear();

            Write(SettingsHelper.GetString(SettingsHelper.HospitalName, "Hospital Management System").ToUpperInvariant() + "\n", 16, true, false, HorizontalAlignment.Center, 140);
            Write(r.IsRefund ? "REFUND RECEIPT\n" : "OFFICIAL RECEIPT\n", 13, true, false, HorizontalAlignment.Center, 140);
            Write(r.PaymentId + "\n\n", 11, false, false, HorizontalAlignment.Center, 140);

            Field("Date", r.PaidAt.ToString("MMM dd, yyyy  hh:mm tt", System.Globalization.CultureInfo.InvariantCulture));
            Field(r.IsRefund ? "Refunded to" : "Received from", r.PatientName);
            Field("Patient ID", r.PatientId);
            Field("Bill", r.BillId);
            if (r.IsRefund) Field("Refund of", r.RefundOfPaymentId);

            Write(r.IsRefund ? "\nREFUND\n" : "\nPAYMENT\n", 11, true, false, HorizontalAlignment.Left, 340);
            Write(Rule, 9, false, false, HorizontalAlignment.Left, 340, Gray);
            Line("Bill total", Money(r.BillTotal), false);
            if (r.IsRefund)
            {
                Line("Paid before this refund", Money(r.PaidBefore), false);
                Line("AMOUNT REFUNDED", Money(r.AmountPaid), true);
                Line("Balance after this refund", Money(r.NewBalance), false);
            }
            else
            {
                Line("Paid before this payment", Money(r.PaidBefore), false);
                Line("AMOUNT PAID NOW", Money(r.AmountPaid), true);
                Line("Balance after this payment", Money(r.NewBalance), false);
            }
            Write(Rule, 9, false, false, HorizontalAlignment.Left, 340, Gray);

            Write("\n", 6, false, false, HorizontalAlignment.Left, 340);
            Field(r.IsRefund ? "Refunded by" : "Payment method", r.MethodName);
            if (r.IsRefund && !string.IsNullOrWhiteSpace(r.Remarks)) Field("Reason", r.Remarks);
            if (!string.IsNullOrWhiteSpace(r.ReferenceNumber)) Field("Reference no.", r.ReferenceNumber);
            if (r.Tendered.HasValue)
            {
                Field("Cash tendered", Money(r.Tendered.Value));
                Field("Change", Money(r.Change ?? 0m));
            }

            Write("\nStatus of the bill:  " + r.StatusAfter + "\n", 11, true, false, HorizontalAlignment.Left, 340);

            Write("\n\n______________________________\n", 10, false, false, HorizontalAlignment.Right, 140);
            Write((r.IsRefund ? "Refunded by " : "Received by ") + (string.IsNullOrEmpty(r.ReceivedBy) ? "-" : r.ReceivedBy) + "\n", 10, true, false, HorizontalAlignment.Right, 140);
            Write(r.IsRefund ? "Patient's signature: ______________________\n"
                             : "Thank you. Please keep this receipt as proof of payment.\n", 9, false, true, HorizontalAlignment.Right, 140, Gray);

            rtbReceipt.SelectionStart = 0;
            rtbReceipt.SelectionLength = 0;
            rtbReceipt.ScrollToCaret();
        }

        private static string Money(decimal value) => SettingsHelper.FormatMoney(value);

        private void Field(string caption, string value)
        {
            Write(caption + ":\t", 10, false, false, HorizontalAlignment.Left, 140, Gray);
            Write((string.IsNullOrWhiteSpace(value) ? "-" : value) + "\n", 10, true, false, HorizontalAlignment.Left, 140);
        }

        private void Line(string caption, string value, bool strong)
        {
            Write(caption + "\t", strong ? 12 : 10, strong, false, HorizontalAlignment.Left, 340);
            Write(value + "\n", strong ? 12 : 10, true, false, HorizontalAlignment.Left, 340);
        }

        private void Write(string text, float size, bool bold, bool italic, HorizontalAlignment alignment, int tabStop, Color? color = null)
        {
            rtbReceipt.SelectionStart = rtbReceipt.TextLength;
            rtbReceipt.SelectionLength = 0;
            rtbReceipt.SelectionAlignment = alignment;
            rtbReceipt.SelectionTabs = new[] { tabStop };
            FontStyle style = (bold ? FontStyle.Bold : FontStyle.Regular) | (italic ? FontStyle.Italic : FontStyle.Regular);
            rtbReceipt.SelectionFont = new System.Drawing.Font("Segoe UI", size, style, GraphicsUnit.Point);
            rtbReceipt.SelectionColor = color ?? Ink;
            rtbReceipt.AppendText(text);
        }
    }
}