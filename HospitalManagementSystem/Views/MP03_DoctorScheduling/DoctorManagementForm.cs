using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP03_DoctorScheduling;
using HospitalManagementSystem.Repositories.Common;
using HospitalManagementSystem.Repositories.MP03_DoctorScheduling;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HospitalManagementSystem.Views.MP03_DoctorScheduling
{
    /// <summary>
    /// MP03 (Part 7A) - Doctor Directory. HR / front-desk staff add, edit, search and delete doctors.
    /// The Doctor ID (DOC-XXX) is generated on first save; the License Number must be unique.
    /// Deleting a doctor also removes their weekly schedule blocks and leave dates, but is refused
    /// while appointments, queue entries or lab orders still refer to the doctor.
    ///
    /// All visuals live in DoctorManagementForm.Designer.cs; this file only holds behaviour.
    /// The weekly schedule builder (Part 7B, ScheduleBuilderForm) is opened from the "Weekly Schedule..." button.
    /// </summary>
    public partial class DoctorManagementForm : Form
    {
        // 4-30 characters: capital letters, digits, hyphens (the License box already forces upper case).
        private static readonly Regex LicensePattern = new Regex(@"^[A-Z0-9][A-Z0-9\-]{3,29}$", RegexOptions.Compiled);

        private const decimal MaxFee = 99999999.99m;   // DECIMAL(10,2) in the database

        private readonly DoctorRepository _doctors = new DoctorRepository();
        private readonly LookupRepository _lookups = new LookupRepository();
        private readonly FormValidator _validator;
        private readonly System.Windows.Forms.Timer _filterTimer = new System.Windows.Forms.Timer { Interval = 300 };

        private string? _editingDoctorId;     // null = adding a new doctor
        private string _editingName = string.Empty;
        private bool _suppressSelection;      // true while the code (not the user) changes the grid selection

        public DoctorManagementForm()
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtFirstName, txtLastName, txtLicense, cboSpecialization, cboDepartment,
                                 cboEmployment, txtPhone, txtEmail, txtFee);

            BuildGridColumns();
            GridHelper.Apply(dgvDoctors);   // read-only guards + auto-size / wrap

            ApplyInputRules();
            WireEvents();
            Load += (s, e) => InitializeScreen();
            Disposed += (s, e) => _filterTimer.Dispose();
        }

        /// <summary>
        /// Creates the Doctor Directory columns in code (the Designer drops designer-made grid columns when it
        /// re-saves a large file). DataPropertyName = the column names returned by DoctorRepository.GetDoctorsTable().
        /// </summary>
        private void BuildGridColumns()
        {
            dgvDoctors.AutoGenerateColumns = false;
            dgvDoctors.Columns.Clear();
            colCode = GridHelper.AddTextColumn(dgvDoctors, "colCode", "Doctor ID", "doctor_id", 9);
            colName = GridHelper.AddTextColumn(dgvDoctors, "colName", "Name", "full_name", 17);
            colLicense = GridHelper.AddTextColumn(dgvDoctors, "colLicense", "License No.", "license_number", 12);
            colSpecialization = GridHelper.AddTextColumn(dgvDoctors, "colSpecialization", "Specialization", "specialization_name", 14);
            colDepartment = GridHelper.AddTextColumn(dgvDoctors, "colDepartment", "Department", "department_name", 14);
            colEmployment = GridHelper.AddTextColumn(dgvDoctors, "colEmployment", "Employment", "employment_type_name", 10);
            colPhone = GridHelper.AddTextColumn(dgvDoctors, "colPhone", "Phone", "phone", 13);
            colFee = GridHelper.AddTextColumn(dgvDoctors, "colFee", "Fee (\u20b1)", "consultation_fee", 9, alignRight: true, format: "N2");
            colBlocks = GridHelper.AddTextColumn(dgvDoctors, "colBlocks", "Schedule Blocks", "schedule_blocks", 8, center: true);
        }

        /// <summary>Each field accepts only characters that fit its data type (typing and pasting).</summary>
        private void ApplyInputRules()
        {
            InputHelper.Attach(txtFirstName, InputKind.PersonName, 60);     // letters only - digits rejected
            InputHelper.Attach(txtLastName, InputKind.PersonName, 60);
            InputHelper.Attach(txtLicense, InputKind.Code, 30);             // capitals, digits, hyphen
            cboSpecialization.MaxLength = 60;                               // editable: a new specialization can be typed
            cboDepartment.MaxLength = 60;
            InputHelper.Attach(txtPhone, InputKind.PhoneSubscriber);        // +63 fixed, then exactly 10 digits
            InputHelper.Attach(txtEmail, InputKind.Email, 120);
            InputHelper.Attach(txtFee, InputKind.Decimal2, 11);             // digits and one point, 2 decimals
        }

        private void WireEvents()
        {
            btnSave.Click += (s, e) => SaveDoctor();
            btnNew.Click += (s, e) => StartNewDoctor();
            btnDelete.Click += (s, e) => DeleteDoctor();
            btnSchedule.Click += (s, e) => OpenSchedule();
            btnClearFilter.Click += (s, e) => txtFilter.Clear();

            _filterTimer.Tick += (s, e) =>
            {
                _filterTimer.Stop();
                ReloadGrid(_editingDoctorId);
            };
            txtFilter.TextChanged += (s, e) =>
            {
                _filterTimer.Stop();
                _filterTimer.Start();      // wait until the user pauses typing
            };

            dgvDoctors.SelectionChanged += (s, e) => OnGridSelectionChanged();
            dgvDoctors.CellFormatting += (s, e) =>
            {
                // Show every stored number the same way: +63 917 123 4567 (also for older 0917-... records).
                if (e.RowIndex >= 0 && dgvDoctors.Columns[e.ColumnIndex].Name == "colPhone" && e.Value is string raw)
                {
                    e.Value = PhoneHelper.Format(raw);
                    e.FormattingApplied = true;
                }
            };
        }

        // ------------------------------------------------------------------
        // Start-up
        // ------------------------------------------------------------------

        private void InitializeScreen()
        {
            try
            {
                BindCombo(cboSpecialization, _lookups.GetSpecializations());
                BindCombo(cboDepartment, _lookups.GetDepartments());
                BindCombo(cboEmployment, _lookups.GetEmploymentTypes());
                ReloadGrid(null);
                SetMode();
                txtFirstName.Focus();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The doctor directory could not load its data.", this);
            }
        }

        private static void BindCombo(ComboBox combo, List<LookupItem> items)
        {
            combo.DataSource = items;
            combo.DisplayMember = nameof(LookupItem.Name);
            combo.ValueMember = nameof(LookupItem.Id);
            combo.SelectedIndex = -1;   // nothing chosen until the user picks one
        }

        // ------------------------------------------------------------------
        // Grid
        // ------------------------------------------------------------------

        /// <summary>Reloads the grid (honouring the search box) and optionally re-selects one doctor.</summary>
        private void ReloadGrid(string? selectDoctorId)
        {
            _suppressSelection = true;
            try
            {
                DataTable table = _doctors.GetDoctorsTable(txtFilter.Text);
                dgvDoctors.DataSource = table;
                dgvDoctors.ClearSelection();

                bool filtered = !string.IsNullOrWhiteSpace(txtFilter.Text);
                lblDoctorsTitle.Text = filtered
                    ? $"Doctor directory  ·  {table.Rows.Count} found"
                    : $"Doctor directory  ·  {table.Rows.Count}";

                if (selectDoctorId != null)
                {
                    foreach (DataGridViewRow row in dgvDoctors.Rows)
                    {
                        if (row.DataBoundItem is DataRowView view && Convert.ToString(view.Row["doctor_id"]) == selectDoctorId)
                        {
                            dgvDoctors.CurrentCell = GridHelper.FirstVisibleCell(row);
                            row.Selected = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The doctor list could not be loaded.", this);
            }
            finally
            {
                _suppressSelection = false;
            }
        }

        private void OnGridSelectionChanged()
        {
            if (_suppressSelection) return;
            if (dgvDoctors.SelectedRows.Count == 0 || dgvDoctors.SelectedRows[0].DataBoundItem is not DataRowView view) return;

            LoadDoctor(Convert.ToString(view.Row["doctor_id"])!);
        }

        // ------------------------------------------------------------------
        // Add / edit mode
        // ------------------------------------------------------------------

        /// <summary>Clears the form and switches to "new doctor" mode.</summary>
        private void StartNewDoctor()
        {
            _validator.ClearAll();

            foreach (TextBox box in new[] { txtCode, txtFirstName, txtLastName, txtLicense, txtPhone, txtEmail, txtFee })
                box.Clear();

            foreach (ComboBox combo in new[] { cboSpecialization, cboDepartment, cboEmployment })
                combo.SelectedIndex = -1;

            _editingDoctorId = null;
            _editingName = string.Empty;

            _suppressSelection = true;
            dgvDoctors.ClearSelection();
            _suppressSelection = false;

            SetMode();
            txtFirstName.Focus();
        }

        /// <summary>Loads one doctor into the form for editing.</summary>
        private void LoadDoctor(string doctorId)
        {
            try
            {
                Doctor? doctor = _doctors.GetById(doctorId);
                if (doctor == null)
                {
                    MessageHelper.Warning("This doctor no longer exists. The list will be refreshed.", "Doctor not found", this);
                    ReloadGrid(null);
                    StartNewDoctor();
                    return;
                }

                _validator.ClearAll();

                txtCode.Text = doctor.DoctorId;
                txtFirstName.Text = doctor.FirstName;
                txtLastName.Text = doctor.LastName;
                txtLicense.Text = doctor.LicenseNumber;
                cboSpecialization.Text = doctor.Specialization;   // editable: also shows a value not in the list
                cboDepartment.Text = doctor.Department;
                cboEmployment.SelectedValue = doctor.EmploymentType;
                txtPhone.Text = PhoneHelper.ToSubscriberDigits(doctor.Phone);   // empty if an old number is not a valid mobile
                txtEmail.Text = doctor.Email;
                txtFee.Text = doctor.ConsultationFee.ToString("0.00", CultureInfo.InvariantCulture);

                _editingDoctorId = doctor.DoctorId;
                _editingName = doctor.DisplayName;
                SetMode();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The doctor could not be loaded.", this);
            }
        }

        /// <summary>Updates the banner, button captions and which buttons are usable.</summary>
        private void SetMode()
        {
            bool editing = _editingDoctorId != null;

            lblMode.Text = editing
                ? $"Editing {_editingDoctorId} - {_editingName}"
                : "New doctor - fill in the details, then click Save Doctor";
            btnSave.Text = editing ? "Save Changes" : "Save Doctor";
            btnDelete.Enabled = editing;
            btnSchedule.Enabled = editing;
        }

        // ------------------------------------------------------------------
        // Validation and saving
        // ------------------------------------------------------------------

        private bool ValidateForm()
        {
            _validator.Begin();

            _validator.PersonName(txtFirstName, "First Name");
            _validator.PersonName(txtLastName, "Last Name");

            string license = txtLicense.Text.Trim();
            _validator.Required(txtLicense, "License Number");
            if (license.Length > 0)
            {
                if (!LicensePattern.IsMatch(license))
                {
                    _validator.AddError(txtLicense,
                        "License Number must be 4 to 30 characters: capital letters, digits and hyphens (example: PRC-0012345).");
                }
                else if (_doctors.LicenseExists(license, _editingDoctorId))
                {
                    _validator.AddError(txtLicense, $"License Number {license} already belongs to another doctor. It must be unique.");
                }
            }

            _validator.Required(cboSpecialization, "Specialization");
            _validator.Required(cboDepartment, "Department");
            _validator.Required(cboEmployment, "Employment Type");

            _validator.Phone(txtPhone, "Mobile Number");
            _validator.Email(txtEmail, "Email");

            if (ValidationHelper.IsBlank(txtFee.Text))
            {
                _validator.AddError(txtFee, "Consultation Fee is required.");
            }
            else if (!ValidationHelper.TryParseDecimal(txtFee.Text, out decimal fee))
            {
                _validator.AddError(txtFee, "Consultation Fee must be a number with at most 2 decimals (example: 500.00).");
            }
            else
            {
                _validator.InRange(txtFee, fee, 0.01m, MaxFee, "Consultation Fee");
            }

            if (_validator.IsValid) return true;

            _validator.ShowErrors(this);
            return false;
        }

        private Doctor BuildDoctor()
        {
            ValidationHelper.TryParseDecimal(txtFee.Text, out decimal fee);

            return new Doctor
            {
                DoctorId = _editingDoctorId ?? string.Empty,
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                LicenseNumber = txtLicense.Text.Trim(),
                Specialization = cboSpecialization.Text.Trim(),   // picked from the list or typed in as a new one
                Department = cboDepartment.Text.Trim(),
                EmploymentType = ((LookupItem)cboEmployment.SelectedItem!).Id,
                Phone = PhoneHelper.TryNormalize(txtPhone.Text, out string canonical) ? canonical : string.Empty,
                Email = txtEmail.Text.Trim(),
                ConsultationFee = decimal.Round(fee, 2)
            };
        }

        private void SaveDoctor()
        {
            try
            {
                if (!ValidateForm()) return;

                Cursor = Cursors.WaitCursor;
                Doctor doctor = BuildDoctor();
                bool isNew = _editingDoctorId == null;

                if (isNew)
                {
                    doctor = _doctors.Add(doctor);
                }
                else
                {
                    _doctors.Update(doctor);
                }
                Cursor = Cursors.Default;

                MessageHelper.Success(
                    isNew
                        ? $"Doctor registered successfully.\n\nDoctor ID:  {doctor.DoctorId}\nName:  {doctor.DisplayName}"
                        : $"Changes saved for {doctor.DoctorId} - {doctor.DisplayName}.",
                    isNew ? "Doctor registered" : "Doctor updated", this);

                ReloadGrid(doctor.DoctorId);
                LoadDoctor(doctor.DoctorId);   // stay in edit mode on the saved doctor
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageHelper.Error(ex, "The doctor could not be saved.", this);
            }
        }

        // ------------------------------------------------------------------
        // Delete
        // ------------------------------------------------------------------

        private void DeleteDoctor()
        {
            if (_editingDoctorId is not string doctorId) return;

            try
            {
                DoctorDeleteImpact impact = _doctors.GetDeleteImpact(doctorId);

                if (impact.IsBlocked)
                {
                    MessageHelper.Warning(
                        DoctorRepository.DescribeBlockers(impact) +
                        "\n\nThese records are patient history, so the doctor is kept. " +
                        "Remove the doctor's availability in the Weekly Schedule instead if they no longer see patients.",
                        "Doctor cannot be deleted", this);
                    return;
                }

                string question =
                    $"Delete {_editingName} ({doctorId})?\n\n" +
                    $"This also permanently removes {impact.ScheduleBlocks} weekly schedule block(s) " +
                    $"and {impact.LeaveDates} leave date(s).\n\nThis cannot be undone.";

                if (!MessageHelper.ConfirmDestructive(question, "Delete doctor", this)) return;

                Cursor = Cursors.WaitCursor;
                _doctors.Delete(doctorId);
                Cursor = Cursors.Default;

                MessageHelper.Success($"{_editingName} ({doctorId}) was deleted.", "Doctor deleted", this);

                StartNewDoctor();
                ReloadGrid(null);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageHelper.Error(ex, "The doctor could not be deleted.", this);
            }
        }

        // ------------------------------------------------------------------
        // Weekly schedule (Part 7B)
        // ------------------------------------------------------------------

        /// <summary>Opens the weekly schedule builder for the selected doctor as a maximized dialog.</summary>
        private void OpenSchedule()
        {
            if (_editingDoctorId is not string doctorId) return;

            try
            {
                using var schedule = new ScheduleBuilderForm(doctorId, _editingName);
                schedule.ShowDialog((IWin32Window?)TopLevelControl ?? this);   // owned by the main window, not this embedded tab

                ReloadGrid(doctorId);   // the schedule-block count in the list may have changed
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The weekly schedule could not be opened.", this);
            }
        }
    }
}