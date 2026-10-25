using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP07_Prescription;
using System.Globalization;

namespace HospitalManagementSystem.Views.MP07_Prescription
{
    /// <summary>
    /// MP07 - print-style, read-only preview of one prescription (white "paper" layout: hospital, patient, medicine lines,
    /// instructions, validity and the doctor's signature line). Nothing is printed; the layout is what matters.
    /// All visuals live in PrescriptionPreviewDialog.Designer.cs; this file only writes the text.
    /// </summary>
    public partial class PrescriptionPreviewDialog : Form
    {
        public PrescriptionPreviewDialog(PrescriptionRecord record)
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon
            btnClose.Click += (s, e) => DialogResult = DialogResult.OK;
            Text = "Prescription " + record.PrescriptionId;
            Build(record);
        }

        private void Build(PrescriptionRecord r)
        {
            rtbPreview.Clear();

            Write(SettingsHelper.GetString(SettingsHelper.HospitalName, "Hospital Management System").ToUpperInvariant() + "\n", 16, true, false, HorizontalAlignment.Center);
            Write("PRESCRIPTION\n", 12, true, false, HorizontalAlignment.Center);
            Write(r.PrescriptionId + "\n\n", 11, false, false, HorizontalAlignment.Center);

            Field("Patient", r.PatientName + "   (" + r.Age + " y)");
            Field("Patient ID", r.PatientId);
            Field("Allergies", r.Allergies);
            Field("Doctor", r.DoctorText);
            Field("Consultation", r.ConsultationId);
            Field("Diagnosis", r.Diagnosis);
            Field("Issued", r.IssuedText);
            Field("Valid until", r.ValidUntilText);

            Write("\n\u211E\n", 26, true, false, HorizontalAlignment.Left);

            int number = 1;
            foreach (PrescriptionLine line in r.Lines)
            {
                Write($"{number}.  {line.MedicineName}\n", 11, true, false, HorizontalAlignment.Left);
                Write($"      {line.Dosage}   -   {line.FrequencyPerDay} time(s) a day for {line.DurationDays} day(s)   -   Quantity: {line.Quantity}\n", 10, false, false, HorizontalAlignment.Left);
                if (!string.IsNullOrWhiteSpace(line.Instructions))
                    Write($"      {line.Instructions}\n", 10, false, true, HorizontalAlignment.Left);
                Write("\n", 6, false, false, HorizontalAlignment.Left);
                number++;
            }

            if (!string.IsNullOrWhiteSpace(r.GeneralInstructions))
            {
                Write("General instructions\n", 10, true, false, HorizontalAlignment.Left);
                Write(r.GeneralInstructions.Trim() + "\n\n", 10, false, false, HorizontalAlignment.Left);
            }

            Write("\n\n______________________________\n", 10, false, false, HorizontalAlignment.Right);
            Write(r.DoctorText + "\n", 10, true, false, HorizontalAlignment.Right);
            Write("Prepared by " + (string.IsNullOrEmpty(r.CreatedBy) ? "-" : r.CreatedBy) + "\n", 9, false, true, HorizontalAlignment.Right);

            rtbPreview.SelectionStart = 0;
            rtbPreview.SelectionLength = 0;
            rtbPreview.ScrollToCaret();
        }

        private void Field(string caption, string value)
        {
            Write(caption + ":\t", 10, false, false, HorizontalAlignment.Left, Color.FromArgb(90, 90, 90));
            Write((string.IsNullOrWhiteSpace(value) ? "-" : value) + "\n", 10, true, false, HorizontalAlignment.Left);
        }

        private void Write(string text, float size, bool bold, bool italic, HorizontalAlignment alignment, Color? color = null)
        {
            rtbPreview.SelectionStart = rtbPreview.TextLength;
            rtbPreview.SelectionLength = 0;
            rtbPreview.SelectionAlignment = alignment;
            rtbPreview.SelectionTabs = new[] { 130 };
            FontStyle style = (bold ? FontStyle.Bold : FontStyle.Regular) | (italic ? FontStyle.Italic : FontStyle.Regular);
            rtbPreview.SelectionFont = new Font("Segoe UI", size, style, GraphicsUnit.Point);
            rtbPreview.SelectionColor = color ?? Color.FromArgb(20, 20, 20);
            rtbPreview.AppendText(text);
        }
    }
}