using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP01_PatientRegistration;
using HospitalManagementSystem.Models.MP02_PatientSearchEditor;
using HospitalManagementSystem.Repositories.Common;
using HospitalManagementSystem.Repositories.MP01_PatientRegistration;
using HospitalManagementSystem.Repositories.MP02_PatientSearchEditor;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace HospitalManagementSystem.Views.MP02_PatientSearchEditor
{
    /// <summary>
    /// MP02 - Patient Profile Editor (Part 5B), a full-screen dialog.
    ///
    ///  * Locked (gray): Patient Code, Registration Date, name, gender, age.
    ///  * Editable: contact information, address, emergency contact, blood type, allergies, medical history.
    ///  * Date of Birth is locked until an administrator approves the override (checkbox + admin password).
    ///  * Blood Type and Date of Birth are "critical": saving a change to either asks for confirmation.
    ///  * Every field that really changed gets its own row in the change history (patient_audit_log).
    ///
    /// All visuals live in PatientEditForm.Designer.cs; this file only holds behaviour.
    /// </summary>
    public partial class PatientEditForm : Form
    {
        private const int MaxOverrideAttempts = 3;
        private const string NotSpecified = "Not specified";

        private readonly PatientRepository _patients = new PatientRepository();
        private readonly PatientProfileRepository _profile = new PatientProfileRepository();
        private readonly LookupRepository _lookups = new LookupRepository();
        private readonly FormValidator _validator;
        private readonly string _patientId;

        private Patient _original = new Patient();
        private DateTime? _loadedStamp;          // patients.last_updated_at when the profile was loaded
        private bool _loading;
        private bool _dobUnlocked;
        private string? _dobApprovedBy;          // administrator username that approved a DOB change
        private int _failedOverrideAttempts;

        /// <summary>True once at least one save succeeded, so the search screen knows to refresh its results.</summary>
        public bool ChangesSaved { get; private set; }

        public PatientEditForm(string patientId)
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon
            _patientId = patientId;

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtPhone, txtEmail, txtPostalCode, txtEmergencyName, cboRelationship,
                                 txtEmergencyPhone, dtpDob);

            ApplyInputRules();
            WireEvents();
            Load += (s, e) => InitializeScreen();
            FormClosing += OnFormClosing;
        }

        // ------------------------------------------------------------------
        // Setup
        // ------------------------------------------------------------------

        /// <summary>Each field accepts only characters that fit its data type (typing and pasting).</summary>
        private void ApplyInputRules()
        {
            InputHelper.Attach(txtPhone, InputKind.PhoneSubscriber);            // +63 fixed, then exactly 10 digits
            InputHelper.Attach(txtEmergencyPhone, InputKind.PhoneSubscriber);
            InputHelper.Attach(txtEmergencyName, InputKind.PersonName, 60);     // letters only - digits rejected
            InputHelper.Attach(txtEmail, InputKind.Email, 120);
            InputHelper.Attach(txtStreet, InputKind.Address, 200);
            InputHelper.Attach(txtPostalCode, InputKind.Digits, 4);             // PH postal code = 4 digits
            InputHelper.Attach(txtProvince, InputKind.Address, 80);
            cboCity.MaxLength = 80;                                             // editable: a new city can be typed
            InputHelper.Attach(txtAdminUser, InputKind.Username, 50);
        }

        private void WireEvents()
        {
            // Any edit refreshes the "N fields changed" summary and the Save button.
            foreach (TextBox box in new[] { txtPhone, txtEmail, txtStreet, txtProvince, txtPostalCode, txtAllergies,
                                            txtMedicalHistory, txtEmergencyName, txtEmergencyPhone })
            {
                box.TextChanged += (s, e) => OnFieldEdited();
            }

            foreach (ComboBox combo in new[] { cboBloodType, cboCity, cboRelationship })
            {
                combo.SelectedIndexChanged += (s, e) => OnFieldEdited();
            }

            cboCity.SelectedIndexChanged += (s, e) => UpdateProvince();
            cboCity.TextChanged += (s, e) => OnFieldEdited();   // a new city can be typed in
            dtpDob.ValueChanged += (s, e) =>
            {
                UpdateAge();
                OnFieldEdited();
            };

            chkAdminOverride.CheckedChanged += (s, e) => OnOverrideToggled();
            btnVerifyAdmin.Click += (s, e) => VerifyOverride();
            txtAdminPassword.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    VerifyOverride();
                }
            };

            btnSave.Click += (s, e) => SaveChanges();
            btnReset.Click += (s, e) => UndoChanges();
            btnHistory.Click += (s, e) => ShowHistory();
        }

        private void InitializeScreen()
        {
            try
            {
                BindCombo(cboBloodType, _lookups.GetBloodTypes());
                BindCombo(cboCity, _lookups.GetCities());
                BindCombo(cboRelationship, _lookups.GetRelationships());

                if (!LoadPatient()) BeginInvoke(new Action(Close));
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The profile editor could not load its data.", this);
                BeginInvoke(new Action(Close));
            }
        }

        /// <summary>Drop-down with a first "Not specified" entry (empty Id = no value) so an optional choice can be cleared.</summary>
        private static void BindCombo(ComboBox combo, List<LookupItem> items)
        {
            items.Insert(0, LookupItem.Placeholder(NotSpecified));
            combo.DataSource = items;
            combo.DisplayMember = nameof(LookupItem.Name);
            combo.ValueMember = nameof(LookupItem.Id);
            combo.SelectedIndex = 0;
        }

        // ------------------------------------------------------------------
        // Loading
        // ------------------------------------------------------------------

        /// <summary>(Re)loads the patient from the database and resets every control to the stored values.</summary>
        private bool LoadPatient()
        {
            _loading = true;
            try
            {
                // Read the stamp FIRST: if somebody saves in between, our later save is refused instead of overwriting them.
                DateTime? stamp = _profile.GetLastUpdatedAt(_patientId);
                Patient? patient = _patients.GetById(_patientId);

                if (patient == null)
                {
                    MessageHelper.Warning("This patient no longer exists.", "Patient not found", this);
                    return false;
                }

                _original = patient;
                _loadedStamp = stamp;

                lblPatientInfo.Text = $"{patient.PatientId}   |   {patient.FullName}";

                // ---- locked fields ----
                txtCode.Text = patient.PatientId;
                txtRegistered.Text = patient.RegisteredAt.ToString("MMMM d, yyyy  h:mm tt");
                txtFullName.Text = patient.FullName;
                txtGender.Text = patient.Gender;

                dtpDob.MaxDate = DateTime.Today.AddDays(1).AddSeconds(-1);   // today is the latest pickable date
                dtpDob.Value = patient.DateOfBirth.Date;
                txtAge.Text = patient.Age.ToString();

                // ---- editable fields ----
                SelectById(cboBloodType, patient.BloodType);
                txtPhone.Text = PhoneHelper.ToSubscriberDigits(patient.PhoneNumber);
                txtEmail.Text = patient.Email;
                txtStreet.Text = patient.StreetAddress;
                SelectById(cboCity, patient.City);
                txtProvince.Text = patient.Province;
                txtPostalCode.Text = patient.PostalCode;
                txtAllergies.Text = patient.Allergies;
                txtMedicalHistory.Text = patient.MedicalHistory;

                txtEmergencyName.Text = patient.EmergencyContactName;
                SelectById(cboRelationship, patient.EmergencyContactRelation);
                txtEmergencyPhone.Text = PhoneHelper.ToSubscriberDigits(patient.EmergencyContactPhone);

                // ---- override back to locked ----
                chkAdminOverride.Checked = false;
                ResetOverrideState();
                _failedOverrideAttempts = 0;

                _validator.ClearAll();
            }
            finally
            {
                _loading = false;
            }

            UpdateChangeSummary();
            return true;
        }

        private static void SelectById(ComboBox combo, string? id)
        {
            string wanted = id ?? string.Empty;
            foreach (object item in combo.Items)
            {
                if (item is LookupItem lookup && lookup.Id == wanted)
                {
                    combo.SelectedItem = item;
                    return;
                }
            }
            combo.SelectedIndex = 0;
            // An editable drop-down (city) can hold a value that is not in its list.
            if (wanted.Length > 0 && combo.DropDownStyle == ComboBoxStyle.DropDown) combo.Text = wanted;
        }

        /// <summary>The chosen (or, for the editable city box, typed) value; blank for "Not specified".</summary>
        private static string SelectedValue(ComboBox combo)
        {
            if (combo.SelectedItem is LookupItem item) return item.Id;
            string typed = combo.Text.Trim();
            return typed == NotSpecified ? string.Empty : typed;
        }

        // ------------------------------------------------------------------
        // Live fields
        // ------------------------------------------------------------------

        private void UpdateProvince()
        {
            // Picking a known city fills in its province; a typed new city leaves the province to the clerk.
            if (cboCity.SelectedItem is LookupItem city) txtProvince.Text = city.Extra;
        }

        private void UpdateAge()
        {
            txtAge.Text = Patient.CalculateAge(dtpDob.Value.Date, DateTime.Today).ToString();
        }

        private void OnFieldEdited()
        {
            if (_loading) return;
            UpdateChangeSummary();
        }

        /// <summary>The patient as it would be saved right now (locked fields come from the stored record).</summary>
        private Patient BuildEdited()
        {
            return new Patient
            {
                PatientId = _original.PatientId,
                FirstName = _original.FirstName,
                MiddleName = _original.MiddleName,
                LastName = _original.LastName,
                Gender = _original.Gender,
                RegisteredAt = _original.RegisteredAt,

                DateOfBirth = _dobUnlocked ? dtpDob.Value.Date : _original.DateOfBirth,
                BloodType = SelectedValue(cboBloodType),
                PhoneNumber = ToStoredPhone(txtPhone.Text),
                Email = txtEmail.Text.Trim(),
                StreetAddress = txtStreet.Text.Trim(),
                City = SelectedValue(cboCity),
                Province = txtProvince.Text.Trim(),
                PostalCode = txtPostalCode.Text.Trim(),
                Allergies = txtAllergies.Text.Trim(),
                MedicalHistory = txtMedicalHistory.Text.Trim(),
                EmergencyContactName = txtEmergencyName.Text.Trim(),
                EmergencyContactPhone = ToStoredPhone(txtEmergencyPhone.Text),
                EmergencyContactRelation = SelectedValue(cboRelationship)
            };
        }

        /// <summary>"9171234567" typed after the fixed +63 becomes "+639171234567" (the stored form). Blank or invalid stays blank.</summary>
        private static string ToStoredPhone(string typed)
        {
            return PhoneHelper.TryNormalize(typed, out string canonical) ? canonical : string.Empty;
        }

        private List<AuditEntry> CurrentChanges()
        {
            return PatientProfileRepository.DetectChanges(_original, BuildEdited(), _dobApprovedBy);
        }

        private bool HasUnsavedChanges() => CurrentChanges().Count > 0;

        /// <summary>Shows how many fields differ from the saved record and enables Save only when something changed.</summary>
        private void UpdateChangeSummary()
        {
            List<AuditEntry> changes = CurrentChanges();
            btnSave.Enabled = changes.Count > 0;
            btnReset.Enabled = changes.Count > 0;

            if (changes.Count == 0)
            {
                lblChangeSummary.ForeColor = ThemeHelper.TextMuted;
                lblChangeSummary.Text = "No changes yet. Gray fields are locked and cannot be edited.";
                return;
            }

            lblChangeSummary.ForeColor = ThemeHelper.Warning;
            lblChangeSummary.Text = (changes.Count == 1 ? "1 field changed: " : $"{changes.Count} fields changed: ")
                                    + string.Join(", ", changes.Select(c => c.FieldChanged));
        }

        // ------------------------------------------------------------------
        // Administrator override for Date of Birth
        // ------------------------------------------------------------------

        private void OnOverrideToggled()
        {
            if (_loading) return;

            if (chkAdminOverride.Checked)
            {
                pnlAdmin.Visible = true;
                if (SessionManager.IsAdministrator && SessionManager.CurrentUser != null)
                    txtAdminUser.Text = SessionManager.CurrentUser.Username;

                SetOverrideStatus("Enter an administrator's username and password to unlock Date of Birth.", false);
                (txtAdminUser.Text.Length > 0 ? txtAdminPassword : txtAdminUser).Focus();
            }
            else
            {
                ResetOverrideState();
                UpdateChangeSummary();
            }
        }

        private void VerifyOverride()
        {
            if (_dobUnlocked) return;

            if (_failedOverrideAttempts >= MaxOverrideAttempts)
            {
                SetOverrideStatus("Too many wrong attempts. Close this window and open the profile again to retry.", true);
                return;
            }

            string user = txtAdminUser.Text.Trim();
            if (user.Length == 0 || txtAdminPassword.Text.Length == 0)
            {
                SetOverrideStatus("Enter both the administrator username and password.", true);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                bool ok = _profile.VerifyAdministrator(user, txtAdminPassword.Text);
                Cursor = Cursors.Default;

                if (!ok)
                {
                    _failedOverrideAttempts++;
                    txtAdminPassword.Clear();
                    int left = MaxOverrideAttempts - _failedOverrideAttempts;
                    SetOverrideStatus(left > 0
                        ? $"Administrator username or password is incorrect. {left} attempt(s) left."
                        : "Too many wrong attempts. Close this window and open the profile again to retry.", true);
                    return;
                }

                _dobUnlocked = true;
                _dobApprovedBy = user;
                dtpDob.Enabled = true;
                txtAdminPassword.Clear();
                ThemeHelper.SetReadOnlyLook(txtAdminUser, true);
                txtAdminPassword.Enabled = false;
                btnVerifyAdmin.Enabled = false;
                SetOverrideStatus($"Approved by {user}. Date of Birth is now unlocked.", false);
                dtpDob.Focus();
                UpdateChangeSummary();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageHelper.Error(ex, "The administrator could not be verified.", this);
            }
        }

        /// <summary>Locks Date of Birth again (restoring the stored date) and clears every override input.</summary>
        private void ResetOverrideState()
        {
            _dobUnlocked = false;
            _dobApprovedBy = null;

            bool wasLoading = _loading;
            _loading = true;
            dtpDob.Value = _original.DateOfBirth == default ? DateTime.Today : _original.DateOfBirth.Date;
            dtpDob.Enabled = false;
            UpdateAge();
            _loading = wasLoading;

            pnlAdmin.Visible = false;
            txtAdminUser.Clear();
            txtAdminPassword.Clear();
            ThemeHelper.SetReadOnlyLook(txtAdminUser, false);
            txtAdminPassword.Enabled = true;
            btnVerifyAdmin.Enabled = true;
            lblOverrideStatus.Text = string.Empty;
        }

        private void SetOverrideStatus(string text, bool isError)
        {
            lblOverrideStatus.Text = text;
            lblOverrideStatus.ForeColor = isError ? ThemeHelper.Warning : ThemeHelper.Success;
        }

        // ------------------------------------------------------------------
        // Validation and saving
        // ------------------------------------------------------------------

        private bool ValidateForm()
        {
            _validator.Begin();

            _validator.Phone(txtPhone, "Mobile Number");
            _validator.Email(txtEmail, "Email");
            _validator.PostalCode(txtPostalCode, "Postal Code");

            // Emergency contact: all three parts together, or none (clear all three to remove the contact).
            bool anyEmergency = !ValidationHelper.IsBlank(txtEmergencyName.Text)
                                || !ValidationHelper.IsBlank(txtEmergencyPhone.Text)
                                || SelectedValue(cboRelationship).Length > 0;
            if (anyEmergency)
            {
                _validator.PersonName(txtEmergencyName, "Emergency Contact Name");
                _validator.Check(SelectedValue(cboRelationship).Length > 0, cboRelationship,
                                 "Please select Emergency Contact Relationship.");
                _validator.Phone(txtEmergencyPhone, "Emergency Contact Mobile Number");
            }

            if (_dobUnlocked) _validator.NotFutureDate(dtpDob, "Date of Birth");

            if (_validator.IsValid) return true;

            _validator.ShowErrors(this);
            return false;
        }

        private void SaveChanges()
        {
            if (!ValidateForm()) return;

            Patient edited = BuildEdited();
            List<AuditEntry> changes = PatientProfileRepository.DetectChanges(_original, edited, _dobApprovedBy);

            if (changes.Count == 0)
            {
                MessageHelper.Info("There are no changes to save.", "Nothing changed", this);
                return;
            }

            // Critical fields need an explicit confirmation that shows exactly what is changing.
            List<AuditEntry> critical = changes.Where(c => c.FieldChanged == "Blood Type" || c.FieldChanged == "Date of Birth").ToList();
            if (critical.Count > 0)
            {
                string lines = string.Join("\n", critical.Select(c =>
                    $"  \u2022 {c.FieldChanged}:  {c.OldValue ?? "(blank)"}   \u2192   {c.NewValue ?? "(blank)"}"));

                bool confirmed = MessageHelper.ConfirmDestructive(
                    "You are changing critical patient information:\n\n" + lines +
                    "\n\nThis change is recorded in the patient's change history. Save it?",
                    "Confirm critical change", this);
                if (!confirmed) return;
            }

            bool dobChanged = changes.Any(c => c.FieldChanged == "Date of Birth");

            try
            {
                Cursor = Cursors.WaitCursor;
                _profile.Save(edited, changes, SessionManager.CurrentUserId, _loadedStamp, _dobUnlocked && dobChanged);
                Cursor = Cursors.Default;
            }
            catch (InvalidOperationException ex)   // changed by someone else / patient removed
            {
                Cursor = Cursors.Default;
                MessageHelper.Warning(ex.Message, "Cannot save", this);
                return;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageHelper.Error(ex, "The changes could not be saved.", this);
                return;
            }

            ChangesSaved = true;
            MessageHelper.Success(
                (changes.Count == 1 ? "1 field was updated:" : $"{changes.Count} fields were updated:") + "\n\n" +
                string.Join("\n", changes.Select(c => "  \u2022 " + c.FieldChanged)) +
                "\n\nEach change was added to the patient's change history.",
                "Profile saved", this);

            if (!LoadPatient()) Close();   // reload so the screen shows exactly what is stored
        }

        /// <summary>Throws away the edits and reloads the stored record (also picks up other people's saves).</summary>
        private void UndoChanges()
        {
            if (HasUnsavedChanges() &&
                !MessageHelper.Confirm("Discard your changes and reload the saved profile?", "Undo changes", this))
            {
                return;
            }

            try
            {
                if (!LoadPatient()) Close();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The profile could not be reloaded.", this);
            }
        }

        private void ShowHistory()
        {
            using var history = new AuditHistoryForm(_original.PatientId, _original.FullName);
            history.ShowDialog(this);
        }

        // ------------------------------------------------------------------
        // Closing
        // ------------------------------------------------------------------

        private void OnFormClosing(object? sender, FormClosingEventArgs e)
        {
            // Never block Windows shutdown or the application exiting.
            if (e.CloseReason != CloseReason.UserClosing) return;

            if (HasUnsavedChanges() &&
                !MessageHelper.Confirm("You have unsaved changes. Close without saving them?", "Unsaved changes", this))
            {
                e.Cancel = true;
            }
        }
    }
}