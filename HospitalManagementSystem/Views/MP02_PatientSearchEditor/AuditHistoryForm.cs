using System.Data;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Repositories.MP02_PatientSearchEditor;

namespace HospitalManagementSystem.Views.MP02_PatientSearchEditor
{
    /// <summary>
    /// MP02 - one patient's change history (Field, Old Value, New Value, Changed By, Date), newest first.
    /// Opened from the search screen and from the profile editor. Read-only.
    ///
    /// All visuals live in AuditHistoryForm.Designer.cs; this file only holds behaviour.
    /// </summary>
    public partial class AuditHistoryForm : Form
    {
        private readonly PatientProfileRepository _repository = new PatientProfileRepository();
        private readonly string _patientId;

        public AuditHistoryForm(string patientId, string patientName)
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _patientId = patientId;
            lblPatientInfo.Text = $"{patientId}   |   {patientName}";

            GridHelper.Apply(dgvHistory);   // read-only guards; columns come from the Designer
            Load += (s, e) => LoadHistory();
        }

        private void LoadHistory()
        {
            try
            {
                DataTable trail = _repository.GetAuditTrail(_patientId);

                bool hasRows = trail.Rows.Count > 0;
                dgvHistory.Visible = hasRows;
                lblEmpty.Visible = !hasRows;

                dgvHistory.DataSource = trail;
                dgvHistory.ClearSelection();

                lblHistoryTitle.Text = hasRows ? $"Recorded changes  ·  {trail.Rows.Count}" : "Recorded changes";
                lblCount.Text = hasRows
                    ? (trail.Rows.Count == 1 ? "1 field change recorded." : $"{trail.Rows.Count} field changes recorded.")
                    : string.Empty;
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The change history could not be loaded.", this);
            }
        }
    }
}