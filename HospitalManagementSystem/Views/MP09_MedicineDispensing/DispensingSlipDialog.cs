using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP09_Dispensing;

namespace HospitalManagementSystem.Views.MP09_Dispensing
{
    /// <summary>
    /// MP09 - print-style, read-only dispensing slip (white "paper" layout: hospital, patient, medicines with quantities and
    /// prices, total, payment method, date and the pharmacist). Nothing is printed; the layout is what matters.
    /// All visuals live in DispensingSlipDialog.Designer.cs; this file only writes the text.
    /// </summary>
    public partial class DispensingSlipDialog : Form
    {
        private static readonly Color Ink = Color.FromArgb(20, 20, 20);
        private static readonly Color Gray = Color.FromArgb(90, 90, 90);

        public DispensingSlipDialog(DispensingSlip slip)
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon
            btnClose.Click += (s, e) => DialogResult = DialogResult.OK;
            Text = "Dispensing Slip " + slip.DispensingId;
            Build(slip);
        }

        private void Build(DispensingSlip s)
        {
            rtbSlip.Clear();

            Write(SettingsHelper.GetString(SettingsHelper.HospitalName, "Hospital Management System").ToUpperInvariant() + "\n", 16, true, false, HorizontalAlignment.Center, 130);
            Write("PHARMACY - DISPENSING SLIP\n", 12, true, false, HorizontalAlignment.Center, 130);
            Write(s.DispensingId + "\n\n", 11, false, false, HorizontalAlignment.Center, 130);

            Field("Patient", s.PatientName + "   (" + s.Age + " y)");
            Field("Patient ID", s.PatientId);
            Field("Prescription", s.PrescriptionId);
            Field("Prescribed by", s.DoctorText);
            Field("Date", s.DispensedAtText);
            Field("Payment method", s.PaymentMethod);

            Write("\nMEDICINES\n", 11, true, false, HorizontalAlignment.Left, 400);
            Write("______________________________________________________________\n", 9, false, false, HorizontalAlignment.Left, 400, Gray);

            int number = 1;
            foreach (DispensingSlipItem item in s.Items)
            {
                string dosage = string.IsNullOrWhiteSpace(item.Dosage) ? string.Empty : "  " + item.Dosage;
                Write($"{number}.  {item.MedicineName}{dosage}\n", 11, true, false, HorizontalAlignment.Left, 400);
                Write($"      {item.Quantity}  x  {item.UnitPriceText}\t{item.LineTotalText}\n", 10, false, false, HorizontalAlignment.Left, 400);
                number++;
            }

            Write("______________________________________________________________\n", 9, false, false, HorizontalAlignment.Left, 400, Gray);
            Write($"TOTAL\t{s.TotalText}\n\n", 13, true, false, HorizontalAlignment.Left, 400);

            Write(s.PrescriptionStatusText + "\n", 10, false, true, HorizontalAlignment.Left, 400, Gray);

            Write("\n\n______________________________\n", 10, false, false, HorizontalAlignment.Right, 130);
            Write("Dispensed by " + (string.IsNullOrEmpty(s.DispensedBy) ? "-" : s.DispensedBy) + "\n", 10, true, false, HorizontalAlignment.Right, 130);
            Write("Please check the medicines before leaving the counter.\n", 9, false, true, HorizontalAlignment.Right, 130, Gray);

            rtbSlip.SelectionStart = 0;
            rtbSlip.SelectionLength = 0;
            rtbSlip.ScrollToCaret();
        }

        private void Field(string caption, string value)
        {
            Write(caption + ":\t", 10, false, false, HorizontalAlignment.Left, 130, Gray);
            Write((string.IsNullOrWhiteSpace(value) ? "-" : value) + "\n", 10, true, false, HorizontalAlignment.Left, 130);
        }

        private void Write(string text, float size, bool bold, bool italic, HorizontalAlignment alignment, int tabStop, Color? color = null)
        {
            rtbSlip.SelectionStart = rtbSlip.TextLength;
            rtbSlip.SelectionLength = 0;
            rtbSlip.SelectionAlignment = alignment;
            rtbSlip.SelectionTabs = new[] { tabStop };
            FontStyle style = (bold ? FontStyle.Bold : FontStyle.Regular) | (italic ? FontStyle.Italic : FontStyle.Regular);
            rtbSlip.SelectionFont = new System.Drawing.Font("Segoe UI", size, style, GraphicsUnit.Point);
            rtbSlip.SelectionColor = color ?? Ink;
            rtbSlip.AppendText(text);
        }
    }
}