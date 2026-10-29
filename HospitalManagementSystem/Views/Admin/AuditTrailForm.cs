using System.Globalization;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Repositories.Common;

namespace HospitalManagementSystem.Views.Admin
{
    /// <summary>
    /// Audit Trail (Administrator only) - the answer to "who did what, and who looked at this patient's record?"
    /// required by the Data Privacy Act (RA 10173). It lists, newest first:
    ///   * creates, finalizations, voids, deletions (patients, doctors, consultations, prescriptions, lab results, bills),
    ///   * payments and refunds, dispensing, stock received and stock adjustments,
    ///   * every time a patient's profile, consultation, prescription or lab result was OPENED,
    ///   * the field-by-field patient profile edits (old value -> new value).
    /// Filter by text (record ID, patient, user, details - results appear while typing), action, record type and dates.
    /// Read-only: nothing on this screen can change or remove an audit row. All visuals live in AuditTrailForm.Designer.cs.
    /// </summary>
    public partial class AuditTrailForm : Views.Main.PageHost
    {
        private readonly AuditRepository _repository = new AuditRepository();
        private List<AuditTrailRow> _rows = new List<AuditTrailRow>();
        private bool _loading;
        private bool _initialized;

        public AuditTrailForm()
        {
            InitializeComponent();
            FormHelper.Prepare(this);

            BuildColumns();
            GridHelper.Apply(dgvAudit);
            GridHelper.EnableStatusColoring(dgvAudit, "colAction");
            InputHelper.Attach(txtSearch, InputKind.Address, 60);
            cboAction.DropDownStyle = ComboBoxStyle.DropDownList;
            cboType.DropDownStyle = ComboBoxStyle.DropDownList;

            btnSearch.Click += (s, e) => LoadRows();
            btnShowAll.Click += (s, e) => ResetFilters();
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = e.Handled = true;
                LoadRows();
            };
            SearchHelper.AttachLive(txtSearch, LoadRows);
            cboAction.SelectedIndexChanged += (s, e) => { if (!_loading && _initialized) LoadRows(); };
            cboType.SelectedIndexChanged += (s, e) => { if (!_loading && _initialized) LoadRows(); };
            dtpFrom.ValueChanged += (s, e) => { if (!_loading && _initialized) LoadRows(); };
            dtpTo.ValueChanged += (s, e) => { if (!_loading && _initialized) LoadRows(); };
            dgvAudit.SelectionChanged += (s, e) => ShowSelected();
            VisibleChanged += (s, e) => { if (Visible && _initialized) LoadRows(); };
            Load += (s, e) => InitializeScreen();
        }

        private void BuildColumns()
        {
            dgvAudit.AutoGenerateColumns = false;
            GridHelper.AddTextColumn(dgvAudit, "colWhen", "When", nameof(AuditTrailRow.WhenText), 15);
            GridHelper.AddTextColumn(dgvAudit, "colUser", "User", nameof(AuditTrailRow.UserText), 16);
            GridHelper.AddTextColumn(dgvAudit, "colAction", "Action", nameof(AuditTrailRow.Action), 8);
            GridHelper.AddTextColumn(dgvAudit, "colRecord", "Record", nameof(AuditTrailRow.RecordText), 17);
            GridHelper.AddTextColumn(dgvAudit, "colPatient", "Patient", nameof(AuditTrailRow.PatientText), 17);
            GridHelper.AddTextColumn(dgvAudit, "colDetails", "Details", nameof(AuditTrailRow.Details), 27);
            foreach (DataGridViewColumn column in dgvAudit.Columns) column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void InitializeScreen()
        {
            lblDate.Text = DateTime.Today.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture) +
                           "  ·  Who created, changed, voided, refunded or opened which record (Data Privacy Act, RA 10173).";
            _loading = true;
            try
            {
                cboAction.Items.Clear();
                cboAction.Items.Add("All actions");
                cboAction.Items.AddRange(AuditRepository.Actions.Cast<object>().ToArray());
                cboAction.SelectedIndex = 0;

                cboType.Items.Clear();
                cboType.Items.Add("All records");
                cboType.Items.AddRange(AuditRepository.EntityTypes.Cast<object>().ToArray());
                cboType.SelectedIndex = 0;

                dtpFrom.Value = DateTime.Today.AddDays(-30);
                dtpFrom.Checked = false;
                dtpTo.Value = DateTime.Today;
                dtpTo.Checked = false;
            }
            finally
            {
                _loading = false;
            }
            _initialized = true;
            LoadRows();
        }

        private void ResetFilters()
        {
            _loading = true;
            txtSearch.Clear();
            cboAction.SelectedIndex = 0;
            cboType.SelectedIndex = 0;
            dtpFrom.Checked = false;
            dtpTo.Checked = false;
            _loading = false;
            LoadRows();
        }

        private void LoadRows()
        {
            var filter = new AuditTrailFilter
            {
                Text = txtSearch.Text.Trim(),
                Action = cboAction.SelectedIndex > 0 ? cboAction.SelectedItem?.ToString() ?? string.Empty : string.Empty,
                EntityType = cboType.SelectedIndex > 0 ? cboType.SelectedItem?.ToString() ?? string.Empty : string.Empty,
                From = dtpFrom.Checked ? dtpFrom.Value.Date : null,
                To = dtpTo.Checked ? dtpTo.Value.Date : null
            };
            if (filter.From.HasValue && filter.To.HasValue && filter.From > filter.To)
            {
                lblCount.Text = "The \"From\" date is after the \"To\" date.";
                return;
            }

            try
            {
                _rows = _repository.Search(filter);
                dgvAudit.DataSource = null;
                dgvAudit.DataSource = _rows;
                dgvAudit.ClearSelection();
                lblCount.Text = _rows.Count == 0
                    ? "No activity matches the filters."
                    : $"{_rows.Count} entr{(_rows.Count == 1 ? "y" : "ies")}" + (_rows.Count >= filter.Limit ? $" (showing the newest {filter.Limit})" : string.Empty) +
                      $"  ·  {_rows.Count(r => r.Action == AuditHelper.View)} record view(s)";
                ShowSelected();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The audit trail could not be loaded.", (IWin32Window?)TopLevelControl ?? this);
            }
        }

        private void ShowSelected()
        {
            if (dgvAudit.SelectedRows.Count == 0 || dgvAudit.SelectedRows[0].DataBoundItem is not AuditTrailRow row)
            {
                txtDetail.Text = "Select an entry to see all of its details.";
                return;
            }
            txtDetail.Text =
                $"{row.WhenText}   ·   {row.UserText}\r\n" +
                $"{row.Action} {row.EntityType} {row.EntityId}" + (row.PatientId.Length > 0 ? $"   ·   patient {row.PatientText}" : string.Empty) + "\r\n\r\n" +
                (row.Details.Length > 0 ? row.Details : "(no details)");
        }
    }
}
