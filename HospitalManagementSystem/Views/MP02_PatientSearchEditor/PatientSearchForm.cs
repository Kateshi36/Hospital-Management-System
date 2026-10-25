using System.Data;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP02_PatientSearchEditor;
using HospitalManagementSystem.Repositories.MP02_PatientSearchEditor;

namespace HospitalManagementSystem.Views.MP02_PatientSearchEditor
{
    /// <summary>
    /// MP02 - Patient Search and Profile Editor: the multi-criteria search and the results list (Part 5A),
    /// plus the buttons that open the profile editor and the change history (Part 5B).
    ///
    /// All visuals live in PatientSearchForm.Designer.cs; this file only holds behaviour.
    /// </summary>
    public partial class PatientSearchForm : Form
    {
        private static readonly Color OkColor = ThemeHelper.Success;
        private static readonly Color WarnColor = ThemeHelper.Warning;

        private readonly PatientSearchRepository _repository = new PatientSearchRepository();

        /// <summary>The criteria of the last successful search, so the list can be refreshed after an edit.</summary>
        private PatientSearchCriteria? _lastCriteria;

        public PatientSearchForm()
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            GridHelper.Apply(dgvResults);   // read-only guards; columns come from the Designer

            ApplyInputRules();
            WireEvents();
            Load += (s, e) => ShowInitialState();
        }

        // ------------------------------------------------------------------
        // Setup
        // ------------------------------------------------------------------

        /// <summary>Each search box accepts only the characters its data type allows (typing and pasting).</summary>
        private void ApplyInputRules()
        {
            InputHelper.Attach(txtSearchCode, InputKind.Code, 20);               // capital letters, digits, hyphen
            InputHelper.Attach(txtSearchName, InputKind.PersonNameSearch, 80);   // letters only - digits rejected
            InputHelper.Attach(txtSearchPhone, InputKind.PhoneSearch);           // digits only, up to 12
        }

        private void WireEvents()
        {
            dgvResults.CellFormatting += (s, e) =>
            {
                // One display format for every stored number: +63 917 123 4567.
                if (e.RowIndex >= 0 && dgvResults.Columns[e.ColumnIndex].Name == colPhone.Name && e.Value is string raw)
                {
                    e.Value = PhoneHelper.Format(raw);
                    e.FormattingApplied = true;
                }
            };

            btnSearch.Click += (s, e) => RunSearch();
            btnClearSearch.Click += (s, e) => ClearSearch();

            // Enter in any criteria box starts the search.
            foreach (Control box in new Control[] { txtSearchCode, txtSearchName, txtSearchPhone, dtpSearchDob })
            {
                box.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.SuppressKeyPress = true;
                        RunSearch();
                    }
                };
            }

            dgvResults.SelectionChanged += (s, e) => UpdateActionButtons();
            dgvResults.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0) OpenSelectedProfile();
            };
            btnEditProfile.Click += (s, e) => OpenSelectedProfile();
            btnViewHistory.Click += (s, e) => ShowSelectedHistory();
        }

        private void ShowInitialState()
        {
            ShowState("Search for a patient",
                      "Enter a code, name, phone number or date of birth above, then press Search (or Enter).");
            SetMessage(string.Empty, MessageKind.Neutral);
            UpdateActionButtons();
            txtSearchName.Focus();
        }

        // ------------------------------------------------------------------
        // Searching
        // ------------------------------------------------------------------

        private PatientSearchCriteria ReadCriteria()
        {
            return new PatientSearchCriteria
            {
                Code = txtSearchCode.Text.Trim(),
                Name = txtSearchName.Text.Trim(),
                Phone = txtSearchPhone.Text.Trim(),
                DateOfBirth = dtpSearchDob.Checked ? dtpSearchDob.Value.Date : null
            };
        }

        private void RunSearch()
        {
            PatientSearchCriteria criteria = ReadCriteria();

            // Never dump the whole patient list: at least one criterion is required.
            if (!criteria.HasAny)
            {
                ShowState("Nothing to search for yet",
                          "Please enter at least one search field: Patient Code, Name, Phone Number or Date of Birth.");
                SetMessage("Please enter at least one search field.", MessageKind.Warning);
                txtSearchName.Focus();
                return;
            }

            ExecuteSearch(criteria);
        }

        /// <summary>Runs the query and shows the results (or the friendly "no match" state).</summary>
        private void ExecuteSearch(PatientSearchCriteria criteria)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DataTable results = _repository.Search(criteria);
                Cursor = Cursors.Default;

                _lastCriteria = criteria;

                if (results.Rows.Count == 0)
                {
                    ShowState("No matching patients found",
                              "Check the spelling, or try fewer criteria (for example just part of the name).");
                    SetMessage("0 patients found.", MessageKind.Warning);
                    return;
                }

                tlpResultState.Visible = false;
                dgvResults.Visible = true;
                dgvResults.DataSource = results;
                dgvResults.ClearSelection();

                SetMessage(results.Rows.Count == 1 ? "1 patient found." : $"{results.Rows.Count} patients found.",
                           MessageKind.Success);
                lblResultsTitle.Text = $"Results  ·  {results.Rows.Count}";
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageHelper.Error(ex, "The search could not be completed.", this);
            }
            finally
            {
                UpdateActionButtons();
            }
        }

        private void ClearSearch()
        {
            txtSearchCode.Clear();
            txtSearchName.Clear();
            txtSearchPhone.Clear();
            dtpSearchDob.Checked = false;

            _lastCriteria = null;
            dgvResults.DataSource = null;
            lblResultsTitle.Text = "Results";
            ShowInitialState();
        }

        // ------------------------------------------------------------------
        // Result state, message line and buttons
        // ------------------------------------------------------------------

        private enum MessageKind { Neutral, Success, Warning }

        /// <summary>Shows a friendly message panel instead of an empty grid.</summary>
        private void ShowState(string title, string hint)
        {
            lblStateTitle.Text = title;
            lblStateHint.Text = hint;
            dgvResults.Visible = false;
            tlpResultState.Visible = true;
            lblResultsTitle.Text = "Results";
            UpdateActionButtons();
        }

        private void SetMessage(string text, MessageKind kind)
        {
            lblSearchMessage.Text = text;
            lblSearchMessage.ForeColor = kind switch
            {
                MessageKind.Success => OkColor,
                MessageKind.Warning => WarnColor,
                _ => ThemeHelper.TextMuted
            };
        }

        /// <summary>The DataRow behind the selected result row, or null when nothing is selected.</summary>
        private DataRow? SelectedRow()
        {
            if (!dgvResults.Visible || dgvResults.SelectedRows.Count == 0) return null;
            return dgvResults.SelectedRows[0].DataBoundItem is DataRowView view ? view.Row : null;
        }

        private string? SelectedPatientId()
        {
            DataRow? row = SelectedRow();
            return row == null ? null : Convert.ToString(row["patient_id"]);
        }

        private void UpdateActionButtons()
        {
            bool hasSelection = SelectedPatientId() != null;
            btnEditProfile.Enabled = hasSelection;
            btnViewHistory.Enabled = hasSelection;
        }

        // ------------------------------------------------------------------
        // Profile editor and change history (Part 5B)
        // ------------------------------------------------------------------

        /// <summary>
        /// Owner for the full-screen dialogs. This screen lives inside a tab (it is a child window),
        /// so the dialogs are owned by the top-level main window instead.
        /// </summary>
        private IWin32Window DialogOwner() => (IWin32Window?)TopLevelControl ?? this;

        private void OpenSelectedProfile()
        {
            string? patientId = SelectedPatientId();
            if (patientId == null) return;

            using var editor = new PatientEditForm(patientId);
            editor.ShowDialog(DialogOwner());

            // Names, phones and cities may have changed: refresh the list and keep the same patient selected.
            if (editor.ChangesSaved) RefreshAfterEdit(patientId);
        }

        private void ShowSelectedHistory()
        {
            DataRow? row = SelectedRow();
            if (row == null) return;

            using var history = new AuditHistoryForm(
                Convert.ToString(row["patient_id"]) ?? string.Empty,
                Convert.ToString(row["full_name"]) ?? string.Empty);
            history.ShowDialog(DialogOwner());
        }

        private void RefreshAfterEdit(string patientId)
        {
            if (_lastCriteria == null) return;

            ExecuteSearch(_lastCriteria);

            if (!dgvResults.Visible) return;   // the edited patient no longer matches the search

            foreach (DataGridViewRow gridRow in dgvResults.Rows)
            {
                if (gridRow.DataBoundItem is DataRowView view && Convert.ToString(view.Row["patient_id"]) == patientId)
                {
                    dgvResults.CurrentCell = GridHelper.FirstVisibleCell(gridRow);
                    gridRow.Selected = true;
                    break;
                }
            }
            UpdateActionButtons();
        }
    }
}