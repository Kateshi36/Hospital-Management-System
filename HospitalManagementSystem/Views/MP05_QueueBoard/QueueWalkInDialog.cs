using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP04_AppointmentBooking;
using HospitalManagementSystem.Repositories.MP05_QueueBoard;
using System.Data;

namespace HospitalManagementSystem.Views.MP05_QueueBoard
{
    /// <summary>
    /// MP05 - picks the patient for a walk-in or emergency check-in ("search then select").
    /// Returns <see cref="PatientId"/> and <see cref="IsEmergency"/> when the user confirms. The caller adds the patient
    /// to the queue (QueueRepository.CheckInWalkIn); the dialog changes no data.
    ///
    /// All visuals live in QueueWalkInDialog.Designer.cs; this file only holds behaviour.
    /// </summary>
    public partial class QueueWalkInDialog : Form
    {
        private readonly QueueRepository _repository;
        private readonly FormValidator _validator;

        public string PatientId { get; private set; } = string.Empty;
        public bool IsEmergency { get; private set; }

        public QueueWalkInDialog(QueueRepository repository, DoctorOption doctor)
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon
            _repository = repository;

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtSearch);

            GridHelper.Apply(dgvPatients);
            InputHelper.Attach(txtSearch, InputKind.Address, 60);

            lblInfo.Text = $"Adds a patient without an appointment to the queue of {doctor.DisplayName}. " +
                           "Tick Emergency to put the patient at the front of the waiting list.";

            btnSearch.Click += (s, e) => Search();
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                e.Handled = true;
                Search();
            };
            dgvPatients.SelectionChanged += (s, e) => ShowSelection();
            dgvPatients.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) Confirm(); };
            btnAdd.Click += (s, e) => Confirm();
            btnCancelDialog.Click += (s, e) => DialogResult = DialogResult.Cancel;
            Shown += (s, e) => txtSearch.Focus();
        }

        private void Search()
        {
            _validator.Begin();

            string text = txtSearch.Text.Trim();
            if (text.Length == 0)
            {
                _validator.AddError(txtSearch, "Type part of the patient's name, patient code or phone number, then press Search.");
                txtSearch.Focus();
                return;
            }

            try
            {
                DataTable table = _repository.SearchPatients(text);
                dgvPatients.DataSource = table;
                dgvPatients.ClearSelection();

                if (table.Rows.Count == 0)
                    SetSelectionLabel("No patient found. Register the patient first under Patient Registration.", ThemeHelper.Warning);
                else if (table.Rows.Count == 1)
                    dgvPatients.Rows[0].Selected = true;
                else
                    SetSelectionLabel($"{table.Rows.Count} patients found - click one to select.", ThemeHelper.TextMuted);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The patient search failed.", this);
            }
        }

        private void ShowSelection()
        {
            if (dgvPatients.SelectedRows.Count == 0 || dgvPatients.SelectedRows[0].DataBoundItem is not DataRowView view)
            {
                SetSelectionLabel("No patient selected yet", ThemeHelper.TextMuted);
                return;
            }

            SetSelectionLabel(
                $"Selected: {DatabaseHelper.GetString(view.Row, "full_name")}  |  {DatabaseHelper.GetString(view.Row, "patient_id")}",
                ThemeHelper.Success);
        }

        private void SetSelectionLabel(string text, Color color)
        {
            lblSelected.Text = text;
            lblSelected.ForeColor = color;
        }

        private void Confirm()
        {
            if (dgvPatients.SelectedRows.Count == 0 || dgvPatients.SelectedRows[0].DataBoundItem is not DataRowView view)
            {
                MessageHelper.ValidationErrors(new[] { "Search for a patient and click the patient to add." }, "Walk-in / Emergency", this);
                return;
            }

            PatientId = DatabaseHelper.GetString(view.Row, "patient_id");
            IsEmergency = chkEmergency.Checked;
            DialogResult = DialogResult.OK;
        }
    }
}