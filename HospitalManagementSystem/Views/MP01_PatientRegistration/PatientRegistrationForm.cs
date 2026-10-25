using System.Data;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP01_PatientRegistration;
using HospitalManagementSystem.Repositories.Common;
using HospitalManagementSystem.Repositories.MP01_PatientRegistration;

namespace HospitalManagementSystem.Views.MP01_PatientRegistration
{
    /// <summary>
    /// MP01 - Patient Registration. Front-desk staff enter a patient, the system generates the
    /// Patient ID (PAT-YYYYMMDD-XXX), and every registered patient is listed in the grid.
    /// Selecting a grid row shows a read-only "Patient Profile Card" preview.
    ///
    /// All visuals live in PatientRegistrationForm.Designer.cs; this file only holds behaviour.
    /// </summary>
    public partial class PatientRegistrationForm : Form
    {
        private readonly PatientRepository _patients = new PatientRepository();
        private readonly LookupRepository _lookups = new LookupRepository();
        private readonly FormValidator _validator;
        private bool _loadingGrid;

        public PatientRegistrationForm()
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtFirstName, txtLastName, dtpDateOfBirth, cboGender, txtPhone, txtEmail,
                                 txtEmergencyName, cboRelationship, txtEmergencyPhone);

            BuildGridColumns();
            GridHelper.Apply(dgvPatients);               // read-only guards + auto-size / wrap
            dtpDateOfBirth.MaxDate = DateTime.Today.AddDays(1).AddSeconds(-1);   // today is the latest pickable date

            ApplyInputRules();
            WireEvents();
            Load += (s, e) => InitializeScreen();
        }

        /// <summary>
        /// Creates the grid columns (Code, Name, Age, Gender, Phone - the MP01 summary grid). They are built here,
        /// not in the Designer, because Visual Studio's [Design] view drops designer-made grid columns when it
        /// re-saves a large designer file. DataPropertyName = the column names returned by GetRegisteredTable().
        /// </summary>
        private void BuildGridColumns()
        {
            dgvPatients.AutoGenerateColumns = false;
            dgvPatients.Columns.Clear();
            colCode = GridHelper.AddTextColumn(dgvPatients, "colCode", "Patient ID", "patient_id", 22);
            colName = GridHelper.AddTextColumn(dgvPatients, "colName", "Name", "full_name", 34);
            colAge = GridHelper.AddTextColumn(dgvPatients, "colAge", "Age", "age", 8, center: true);
            colGender = GridHelper.AddTextColumn(dgvPatients, "colGender", "Gender", "gender", 12);
            colPhone = GridHelper.AddTextColumn(dgvPatients, "colPhone", "Phone", "phone_number", 20);
        }

        /// <summary>Each field accepts only characters that fit its data type (typing and pasting).</summary>
        private void ApplyInputRules()
        {
            foreach (TextBox name in new[] { txtFirstName, txtMiddleName, txtLastName, txtEmergencyName })
                InputHelper.Attach(name, InputKind.PersonName, 60);          // letters only - digits rejected

            InputHelper.Attach(txtPhone, InputKind.PhoneSubscriber);          // +63 fixed, then exactly 10 digits
            InputHelper.Attach(txtEmergencyPhone, InputKind.PhoneSubscriber);
            InputHelper.Attach(txtEmail, InputKind.Email, 120);
            InputHelper.Attach(txtStreet, InputKind.Address, 200);
            InputHelper.Attach(txtPostalCode, InputKind.Digits, 4);           // PH postal code = 4 digits
            InputHelper.Attach(txtProvince, InputKind.Address, 80);
            cboCity.MaxLength = 80;                                           // editable: a new city can be typed
        }

        // ------------------------------------------------------------------
        // Start-up
        // ------------------------------------------------------------------

        private void WireEvents()
        {
            btnSave.Click += (s, e) => SavePatient();
            btnClear.Click += (s, e) => ClearForm();
            dtpDateOfBirth.ValueChanged += (s, e) => UpdateAge();
            cboCity.SelectedIndexChanged += (s, e) => UpdateProvince();
            dgvPatients.SelectionChanged += (s, e) => UpdateProfileCard();
            dgvPatients.CellFormatting += (s, e) =>
            {
                // Show every stored number the same way: +63 917 123 4567 (also for older 0917-... records).
                if (e.RowIndex >= 0 && dgvPatients.Columns[e.ColumnIndex].Name == "colPhone" && e.Value is string raw)
                {
                    e.Value = PhoneHelper.Format(raw);
                    e.FormattingApplied = true;
                }
            };
        }

        private void InitializeScreen()
        {
            try
            {
                BindCombo(cboGender, _lookups.GetGenders());
                BindCombo(cboBloodType, _lookups.GetBloodTypes());
                BindCombo(cboCity, _lookups.GetCities());
                BindCombo(cboRelationship, _lookups.GetRelationships());
                ReloadGrid(null);
                ShowCardEmptyState();
                txtFirstName.Focus();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The registration screen could not load its data.", this);
            }
        }

        private static void BindCombo(ComboBox combo, List<LookupItem> items)
        {
            combo.DataSource = items;
            combo.DisplayMember = nameof(LookupItem.Name);
            combo.ValueMember = nameof(LookupItem.Id);
            combo.SelectedIndex = -1;   // nothing chosen until the clerk picks one
        }

        /// <summary>Text of a drop-down: the picked item, or what was typed into an editable one.</summary>
        private static string ComboText(ComboBox combo) =>
            combo.SelectedItem is LookupItem item ? item.Id : combo.Text.Trim();

        // ------------------------------------------------------------------
        // Live fields
        // ------------------------------------------------------------------

        /// <summary>Age is calculated from the date of birth and shown read-only.</summary>
        private void UpdateAge()
        {
            txtAge.Text = dtpDateOfBirth.Checked
                ? Patient.CalculateAge(dtpDateOfBirth.Value.Date, DateTime.Today).ToString()
                : string.Empty;
        }

        /// <summary>Picking a known city fills in its province; a newly typed city leaves the province for the clerk.</summary>
        private void UpdateProvince()
        {
            if (cboCity.SelectedItem is LookupItem city) txtProvince.Text = city.Extra;
        }

        // ------------------------------------------------------------------
        // Validation and saving
        // ------------------------------------------------------------------

        private bool ValidateForm()
        {
            _validator.Begin();

            _validator.PersonName(txtFirstName, "First Name");
            _validator.PersonName(txtMiddleName, "Middle Name", required: false);
            _validator.PersonName(txtLastName, "Last Name");

            if (!dtpDateOfBirth.Checked)
                _validator.AddError(dtpDateOfBirth, "Date of Birth is required (tick the box and pick a date).");
            else
                _validator.NotFutureDate(dtpDateOfBirth, "Date of Birth");

            _validator.Required(cboGender, "Gender");
            _validator.Phone(txtPhone, "Mobile Number");
            _validator.Email(txtEmail, "Email");
            _validator.PostalCode(txtPostalCode, "Postal Code");

            // Emergency contact: all three parts together, or none.
            bool anyEmergency = !ValidationHelper.IsBlank(txtEmergencyName.Text)
                                || !ValidationHelper.IsBlank(txtEmergencyPhone.Text)
                                || cboRelationship.SelectedIndex >= 0;
            if (anyEmergency)
            {
                _validator.PersonName(txtEmergencyName, "Emergency Contact Name");
                _validator.Required(cboRelationship, "Emergency Contact Relationship");
                _validator.Phone(txtEmergencyPhone, "Emergency Contact Mobile Number");
            }

            if (_validator.IsValid) return true;

            _validator.ShowErrors(this);
            return false;
        }

        private Patient BuildPatient()
        {
            return new Patient
            {
                FirstName = txtFirstName.Text.Trim(),
                MiddleName = txtMiddleName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                DateOfBirth = dtpDateOfBirth.Value.Date,
                Gender = ((LookupItem)cboGender.SelectedItem!).Id,
                BloodType = ComboText(cboBloodType),
                PhoneNumber = ToStoredPhone(txtPhone.Text),
                Email = txtEmail.Text.Trim(),
                StreetAddress = txtStreet.Text.Trim(),
                City = ComboText(cboCity),
                Province = txtProvince.Text.Trim(),
                PostalCode = txtPostalCode.Text.Trim(),
                Allergies = txtAllergies.Text.Trim(),
                MedicalHistory = txtMedicalHistory.Text.Trim(),
                EmergencyContactName = txtEmergencyName.Text.Trim(),
                EmergencyContactPhone = ToStoredPhone(txtEmergencyPhone.Text),
                EmergencyContactRelation = ComboText(cboRelationship)
            };
        }

        /// <summary>"9171234567" typed after the fixed +63 becomes "+639171234567" (the stored form). Blank stays blank.</summary>
        private static string ToStoredPhone(string typed)
        {
            return PhoneHelper.TryNormalize(typed, out string canonical) ? canonical : string.Empty;
        }

        private void SavePatient()
        {
            if (!ValidateForm()) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                Patient saved = _patients.Register(BuildPatient(), SessionManager.CurrentUserId);
                Cursor = Cursors.Default;

                MessageHelper.Success(
                    $"Patient registered successfully.\n\nPatient ID:  {saved.PatientId}\nName:  {saved.FullName}",
                    "Registration complete", this);

                ClearForm();
                ReloadGrid(saved.PatientId);   // refresh immediately and preview the new patient
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageHelper.Error(ex, "The patient could not be saved.", this);
            }
        }

        /// <summary>Resets every input. Patients that were already saved are not affected.</summary>
        private void ClearForm()
        {
            _validator.ClearAll();

            foreach (TextBox box in new[] { txtFirstName, txtMiddleName, txtLastName, txtPhone, txtEmail, txtStreet,
                                            txtPostalCode, txtAllergies, txtMedicalHistory, txtEmergencyName,
                                            txtEmergencyPhone, txtAge, txtProvince })
            {
                box.Clear();
            }

            foreach (ComboBox combo in new[] { cboGender, cboBloodType, cboCity, cboRelationship })
            {
                combo.SelectedIndex = -1;
            }

            dtpDateOfBirth.Value = DateTime.Today;
            dtpDateOfBirth.Checked = false;
            UpdateAge();
            txtFirstName.Focus();
        }

        // ------------------------------------------------------------------
        // Grid
        // ------------------------------------------------------------------

        /// <summary>Reloads the grid from the database and optionally selects one patient.</summary>
        private void ReloadGrid(string? selectPatientId)
        {
            _loadingGrid = true;
            try
            {
                DataTable table = _patients.GetRegisteredTable();
                dgvPatients.DataSource = table;
                dgvPatients.ClearSelection();
                lblPatientsTitle.Text = $"Registered patients  ·  {table.Rows.Count}";
            }
            finally
            {
                _loadingGrid = false;
            }

            if (selectPatientId != null)
            {
                foreach (DataGridViewRow row in dgvPatients.Rows)
                {
                    if (row.DataBoundItem is DataRowView view && Convert.ToString(view.Row["patient_id"]) == selectPatientId)
                    {
                        dgvPatients.CurrentCell = row.Cells[0];
                        row.Selected = true;
                        break;
                    }
                }
            }

            UpdateProfileCard();
        }

        // ------------------------------------------------------------------
        // Patient Profile Card (read-only preview, laid out like a printed card)
        // ------------------------------------------------------------------

        private void UpdateProfileCard()
        {
            if (_loadingGrid) return;

            if (dgvPatients.SelectedRows.Count == 0 ||
                dgvPatients.SelectedRows[0].DataBoundItem is not DataRowView view)
            {
                ShowCardEmptyState();
                return;
            }

            try
            {
                Patient? patient = _patients.GetById(Convert.ToString(view.Row["patient_id"])!);
                if (patient == null)
                {
                    ShowCardEmptyState();
                    return;
                }
                ShowCard(patient);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The profile card could not be loaded.", this);
            }
        }

        private void ShowCardEmptyState()
        {
            pnlCardHeader.Visible = false;
            tlpCardBody.Visible = false;
            lblCardEmpty.Visible = true;
        }

        private void ShowCard(Patient p)
        {
            lblCardCode.Text = p.PatientId;
            lblCardName.Text = p.FullName;
            lblCardDob.Text = p.DateOfBirth.ToString("MMMM d, yyyy");
            lblCardAge.Text = p.Age + " years";
            lblCardGender.Text = p.Gender;
            lblCardBlood.Text = Dash(p.BloodType);
            lblCardPhone.Text = PhoneHelper.Format(p.PhoneNumber);
            lblCardEmail.Text = Dash(p.Email);
            lblCardAddress.Text = BuildAddress(p);
            lblCardAllergies.Text = p.Allergies.Length > 0 ? p.Allergies : "None recorded";
            lblCardHistory.Text = p.MedicalHistory.Length > 0 ? p.MedicalHistory : "None recorded";
            lblCardEmergency.Text = p.EmergencyContactName.Length > 0
                ? $"{p.EmergencyContactName} ({p.EmergencyContactRelation})  -  {PhoneHelper.Format(p.EmergencyContactPhone)}"
                : "-";
            lblCardRegistered.Text = p.RegisteredAt.ToString("MMMM d, yyyy  h:mm tt");

            lblCardEmpty.Visible = false;
            pnlCardHeader.Visible = true;
            tlpCardBody.Visible = true;
        }

        private static string BuildAddress(Patient p)
        {
            string cityLine = string.Join(", ", new[] { p.City, p.Province }.Where(x => x.Length > 0));
            if (p.PostalCode.Length > 0) cityLine = (cityLine + " " + p.PostalCode).Trim();

            string full = string.Join(", ", new[] { p.StreetAddress, cityLine }.Where(x => x.Length > 0));
            return full.Length > 0 ? full : "-";
        }

        private static string Dash(string text) => text.Length > 0 ? text : "-";
    }
}