using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP06_ConsultationVitals;
using HospitalManagementSystem.Repositories.MP06_ConsultationVitals;
using System.Globalization;

namespace HospitalManagementSystem.Views.MP06_ConsultationVitals
{
    /// <summary>
    /// MP06 (Step 10) - Consultation &amp; Vital Signs recorder. Two views, switched with the buttons at the top:
    ///
    ///   NEW CONSULTATION
    ///     1. pick one of today's appointments that has no consultation yet (optionally filtered by doctor);
    ///     2. enter the vital signs: systolic BP 50-250, diastolic BP 30-150 (lower than systolic), temperature 30-45 C,
    ///        pulse 20-250 bpm, weight above 0 (all required) and an optional height;
    ///     3. write the chief complaint (required), findings, diagnosis (required), treatment plan, special instructions
    ///        and an optional next follow-up date, then Save.
    ///     The database generates the Consultation ID (CON-YYYYMMDD-XXX) and the time stamp. An appointment can be
    ///     consulted only once: after saving it disappears from the picker.
    ///
    ///   HISTORY &amp; CHART NOTES
    ///     every consultation recorded so far (newest first), filterable by patient name or code; selecting a row shows
    ///     a read-only, formatted chart note.
    ///
    /// Every rule lives in ConsultationRepository (Validate and Save); the form shows the same rules next to the fields.
    /// All visuals live in ConsultationForm.Designer.cs; this file only holds behaviour.
    /// </summary>
    public partial class ConsultationForm : Views.Main.PageHost
    {
        private readonly ConsultationRepository _repository = new ConsultationRepository();
        private readonly FormValidator _validator;

        private List<AppointmentChoice> _pending = new List<AppointmentChoice>();
        private List<ConsultationRecord> _history = new List<ConsultationRecord>();
        private AppointmentChoice? _selected;
        private bool _loading;                  // true while the code (not the user) fills lists
        private bool _initialized;
        private bool _historyMode;

        public ConsultationForm()
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtSystolic, txtDiastolic, txtTemperature, txtPulse, txtWeight, txtHeight,
                                 txtComplaint, txtFindings, txtDiagnosis, txtPlan, txtInstructions, dtpFollowUp);

            BuildGridColumns();                     // columns are created in code, see the note on BuildGridColumns
            GridHelper.Apply(dgvAppointments);      // read-only guards
            GridHelper.Apply(dgvHistory);
            GridHelper.EnableStatusColoring(dgvAppointments, "colAStatus");
            GridHelper.EnableStatusColoring(dgvAppointments, "colAQueue");

            ApplyInputRules();
            WireEvents();
            Load += (s, e) => InitializeScreen();
            AppEvents.OnDoctorsChanged(this, ReloadDoctorList);   // a doctor was added / edited in Doctor Directory
        }

        /// <summary>
        /// Creates the columns of both grids. They are built here, not in the Designer, because Visual Studio's [Design]
        /// view silently deletes grid columns from InitializeComponent when it re-saves a large designer file, and the
        /// grids then show every property of the model. Code in the constructor cannot be removed by the Designer.
        /// </summary>
        private void BuildGridColumns()
        {
            dgvAppointments.AutoGenerateColumns = false;
            AddColumn(dgvAppointments, "colATime", "Time", nameof(AppointmentChoice.TimeText), 12);
            AddColumn(dgvAppointments, "colAToken", "Token", nameof(AppointmentChoice.TokenNumber), 9);
            AddColumn(dgvAppointments, "colAPatient", "Patient", nameof(AppointmentChoice.PatientName), 25);
            AddColumn(dgvAppointments, "colAReason", "Reason for visit", nameof(AppointmentChoice.Reason), 26);
            AddColumn(dgvAppointments, "colAStatus", "Status", nameof(AppointmentChoice.StatusName), 14);
            AddColumn(dgvAppointments, "colAQueue", "Queue", nameof(AppointmentChoice.QueueText), 16);

            dgvHistory.AutoGenerateColumns = false;
            AddColumn(dgvHistory, "colHCode", "Consultation ID", nameof(ConsultationRecord.ConsultationId), 20);
            AddColumn(dgvHistory, "colHDate", "Recorded", nameof(ConsultationRecord.RecordedText), 22);
            AddColumn(dgvHistory, "colHPatient", "Patient", nameof(ConsultationRecord.PatientName), 22);
            AddColumn(dgvHistory, "colHDoctor", "Doctor", nameof(ConsultationRecord.DoctorText), 18);
            AddColumn(dgvHistory, "colHDiagnosis", "Diagnosis", nameof(ConsultationRecord.Diagnosis), 30);
        }

        private static void AddColumn(DataGridView grid, string name, string header, string property, float weight)
        {
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                DataPropertyName = property,
                FillWeight = weight,
                MinimumWidth = 6,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
        }

        private IWin32Window DialogOwner() => (IWin32Window?)TopLevelControl ?? this;

        private void ApplyInputRules()
        {
            InputHelper.Attach(txtSystolic, InputKind.Digits, 3);
            InputHelper.Attach(txtDiastolic, InputKind.Digits, 3);
            InputHelper.Attach(txtPulse, InputKind.Digits, 3);
            InputHelper.Attach(txtTemperature, InputKind.Decimal2, 4);
            InputHelper.Attach(txtWeight, InputKind.Decimal2, 5);
            InputHelper.Attach(txtHeight, InputKind.Decimal2, 5);

            // Letters, digits, space and , . - # / ' ( ) : the same characters every free-text box accepts.
            InputHelper.Attach(txtComplaint, InputKind.Address, ConsultationRules.MaxComplaint);
            InputHelper.Attach(txtFindings, InputKind.Address, ConsultationRules.MaxFindings);
            InputHelper.Attach(txtDiagnosis, InputKind.Address, ConsultationRules.MaxDiagnosis);
            InputHelper.Attach(txtPlan, InputKind.Address, ConsultationRules.MaxPlan);
            InputHelper.Attach(txtInstructions, InputKind.Address, ConsultationRules.MaxInstructions);
            InputHelper.Attach(txtHistorySearch, InputKind.Address, 60);
        }

        private void WireEvents()
        {
            btnModeEntry.Click += (s, e) => ShowMode(false);
            btnModeHistory.Click += (s, e) => ShowMode(true);

            cboDoctor.SelectedIndexChanged += (s, e) => { if (!_loading) LoadPending(); };
            btnRefresh.Click += (s, e) => LoadPending();
            dgvAppointments.SelectionChanged += (s, e) => OnAppointmentSelected();

            chkFollowUp.CheckedChanged += (s, e) => dtpFollowUp.Enabled = chkFollowUp.Checked;
            btnSave.Click += (s, e) => SaveConsultation();
            btnClear.Click += (s, e) => ClearFields();

            btnHistorySearch.Click += (s, e) => LoadHistory();
            btnHistoryAll.Click += (s, e) => { txtHistorySearch.Clear(); LoadHistory(); };
            txtHistorySearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                e.Handled = true;
                LoadHistory();
            };
            SearchHelper.AttachLive(txtHistorySearch, LoadHistory);
            dgvHistory.SelectionChanged += (s, e) => ShowSelectedNote();
            // a note the user clicks is a view of the patient's medical record (audit trail, RA 10173)
            dgvHistory.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0 && dgvHistory.Rows[e.RowIndex].DataBoundItem is ConsultationRecord r)
                    AuditHelper.LogView(AuditHelper.Consultation, r.ConsultationId, r.PatientId, "Opened the consultation note");
            };

            VisibleChanged += (s, e) =>
            {
                if (Visible && _initialized && !_historyMode) LoadPending();
            };
        }

        // ------------------------------------------------------------------
        // Start-up and view switching
        // ------------------------------------------------------------------

        /// <summary>Reloads the doctor filter after a doctor changed in MP03, keeping the selection.</summary>
        private void ReloadDoctorList()
        {
            if (cboDoctor.DataSource == null) return;
            string? keep = cboDoctor.SelectedItem is LookupItem { HasValue: true } item ? item.Id : null;
            _loading = true;
            try
            {
                var doctors = new List<LookupItem> { LookupItem.Placeholder("All doctors") };
                doctors.AddRange(_repository.GetDoctors().Select(d => new LookupItem { Id = d.DoctorId, Name = d.Display }));
                cboDoctor.DataSource = doctors;
                int index = keep == null ? 0 : doctors.FindIndex(d => d.Id == keep);
                cboDoctor.SelectedIndex = index < 0 ? 0 : index;
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The doctor list could not be refreshed.", DialogOwner());
            }
            finally
            {
                _loading = false;
            }
            LoadPending();
        }

        private void InitializeScreen()
        {
            _loading = true;
            try
            {
                lblDate.Text = DateTime.Today.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture) +
                              "  ·  Record vital signs and clinical notes for today's appointments.";

                var doctors = new List<LookupItem> { LookupItem.Placeholder("All doctors") };
                doctors.AddRange(_repository.GetDoctors().Select(d => new LookupItem { Id = d.DoctorId, Name = d.Display }));
                cboDoctor.DataSource = doctors;
                cboDoctor.DisplayMember = nameof(LookupItem.Name);
                cboDoctor.ValueMember = nameof(LookupItem.Id);
                cboDoctor.SelectedIndex = 0;

                // Follow-up: tomorrow at the earliest, two years at the latest.
                dtpFollowUp.MinDate = DateTime.Today.AddDays(1);
                dtpFollowUp.MaxDate = DateTime.Today.AddYears(2);
                dtpFollowUp.Value = DateTime.Today.AddDays(7);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The screen could not be prepared.", DialogOwner());
            }
            finally
            {
                _loading = false;
            }

            _initialized = true;
            ShowMode(false);
            LoadPending();
        }

        private void ShowMode(bool history)
        {
            _historyMode = history;
            pnlEntry.Visible = !history;
            pnlHistory.Visible = history;
            ThemeHelper.StyleButton(btnModeEntry, history ? ButtonKind.Secondary : ButtonKind.Primary);
            ThemeHelper.StyleButton(btnModeHistory, history ? ButtonKind.Primary : ButtonKind.Secondary);

            if (history) LoadHistory();
            else if (_initialized) LoadPending();
        }

        private void SetStatus(string text, Color color)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = color;
        }

        // ------------------------------------------------------------------
        // New consultation
        // ------------------------------------------------------------------

        private void LoadPending()
        {
            string? keepAppointment = _selected?.AppointmentId;
            string? doctorId = cboDoctor.SelectedItem is LookupItem { HasValue: true } doctor ? doctor.Id : null;

            try
            {
                _pending = _repository.GetPendingAppointments(doctorId);

                _loading = true;
                dgvAppointments.DataSource = null;
                dgvAppointments.DataSource = _pending;
                dgvAppointments.ClearSelection();
                dgvAppointments.CurrentCell = null;
                _loading = false;

                _selected = null;
                if (keepAppointment != null)
                {
                    foreach (DataGridViewRow row in dgvAppointments.Rows)
                    {
                        if (row.DataBoundItem is AppointmentChoice choice && choice.AppointmentId == keepAppointment)
                        {
                            row.Selected = true;
                            break;
                        }
                    }
                }
                OnAppointmentSelected();

                SetStatus(_pending.Count == 0
                    ? "No appointment is waiting for a consultation."
                    : $"{_pending.Count} appointment(s) waiting for a consultation.", ThemeHelper.TextMuted);
            }
            catch (Exception ex)
            {
                _loading = false;
                MessageHelper.Error(ex, "Today's appointments could not be loaded.", DialogOwner());
            }
        }

        private void OnAppointmentSelected()
        {
            if (_loading) return;

            if (dgvAppointments.SelectedRows.Count == 0 || dgvAppointments.SelectedRows[0].DataBoundItem is not AppointmentChoice choice)
            {
                _selected = null;
                lblSelected.Text = "No appointment selected yet";
                lblSelected.ForeColor = ThemeHelper.TextMuted;
                btnSave.Enabled = false;
                return;
            }

            _selected = choice;
            _validator.ClearError(dgvAppointments);

            string second = "Reason: " + (string.IsNullOrWhiteSpace(choice.Reason) ? "-" : choice.Reason);
            if (!string.IsNullOrWhiteSpace(choice.Allergies)) second += "   |   Allergies: " + choice.Allergies;

            lblSelected.Text = $"{choice.PatientName}, {choice.Age} y   |   {choice.PatientId}   |   Dr. {choice.DoctorName}\n{second}";
            lblSelected.ForeColor = ThemeHelper.Success;
            btnSave.Enabled = true;
        }

        /// <summary>Resets every field of the form (the selected appointment stays selected).</summary>
        private void ClearFields()
        {
            _validator.Begin();
            foreach (TextBox box in new[] { txtSystolic, txtDiastolic, txtTemperature, txtPulse, txtWeight, txtHeight,
                                            txtComplaint, txtFindings, txtDiagnosis, txtPlan, txtInstructions })
            {
                box.Clear();
            }
            chkFollowUp.Checked = false;
            dtpFollowUp.Value = DateTime.Today.AddDays(7);
            _validator.Begin();
        }

        private void SaveConsultation()
        {
            ConsultationInput? input = ReadAndValidate();
            if (input == null) return;

            AppointmentChoice? chosen = _selected;
            try
            {
                ConsultationRecord saved = _repository.Save(input);

                MessageHelper.Success(
                    $"Consultation {saved.ConsultationId} was saved for {saved.PatientName}.\nRecorded {saved.RecordedText} by {saved.RecordedBy}.",
                    "Consultation saved", DialogOwner());

                ClearFields();
                _selected = null;
                LoadPending();
                SetStatus($"Saved {saved.ConsultationId} ({saved.PatientName}).", ThemeHelper.Success);
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Consultation", DialogOwner());
                LoadPending();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, chosen == null ? "The consultation could not be saved." : $"The consultation for {chosen.PatientName} could not be saved.", DialogOwner());
            }
        }

        /// <summary>Checks every field, shows the problems next to the fields and returns the input (null when something is wrong).</summary>
        private ConsultationInput? ReadAndValidate()
        {
            _validator.Begin();

            _validator.Check(_selected != null, dgvAppointments, "Select the appointment to consult first.");

            int systolic = ReadWhole(txtSystolic, "Systolic BP", ConsultationRules.SystolicMin, ConsultationRules.SystolicMax, "mmHg");
            int diastolic = ReadWhole(txtDiastolic, "Diastolic BP", ConsultationRules.DiastolicMin, ConsultationRules.DiastolicMax, "mmHg");
            if (systolic > 0 && diastolic > 0 && diastolic >= systolic)
                _validator.AddError(txtDiastolic, "Diastolic BP must be lower than systolic BP.");

            decimal temperature = ReadDecimal(txtTemperature, "Temperature", required: true);
            if (temperature != 0) _validator.InRange(txtTemperature, temperature, ConsultationRules.TemperatureMin, ConsultationRules.TemperatureMax, "Temperature", "\u00B0C");

            int pulse = ReadWhole(txtPulse, "Pulse", ConsultationRules.PulseMin, ConsultationRules.PulseMax, "bpm");

            decimal weight = ReadDecimal(txtWeight, "Weight", required: true);
            if (weight != 0 && (weight <= 0 || weight > ConsultationRules.WeightMax))
                _validator.AddError(txtWeight, $"Weight must be greater than 0 and at most {ConsultationRules.WeightMax:0} kg.");

            decimal height = ReadDecimal(txtHeight, "Height", required: false);
            decimal? heightValue = null;
            if (txtHeight.Text.Trim().Length > 0 && height != 0)
            {
                _validator.InRange(txtHeight, height, ConsultationRules.HeightMin, ConsultationRules.HeightMax, "Height", "cm");
                heightValue = height;
            }

            _validator.Check(txtComplaint.Text.Trim().Length > 0, txtComplaint, "Enter the chief complaint.");
            _validator.Check(txtDiagnosis.Text.Trim().Length > 0, txtDiagnosis, "Enter at least one diagnosis before saving.");

            if (!_validator.IsValid)
            {
                _validator.ShowErrors(DialogOwner());
                return null;
            }

            return new ConsultationInput
            {
                AppointmentId = _selected!.AppointmentId,
                SystolicBp = systolic,
                DiastolicBp = diastolic,
                TemperatureC = temperature,
                PulseRateBpm = pulse,
                WeightKg = weight,
                HeightCm = heightValue,
                ChiefComplaint = txtComplaint.Text.Trim(),
                ExaminationFindings = txtFindings.Text.Trim(),
                Diagnosis = txtDiagnosis.Text.Trim(),
                TreatmentPlan = txtPlan.Text.Trim(),
                SpecialInstructions = txtInstructions.Text.Trim(),
                NextFollowUpDate = chkFollowUp.Checked ? dtpFollowUp.Value.Date : null
            };
        }

        /// <summary>Whole-number vital sign: required, digits only, inside min..max. Returns 0 when invalid.</summary>
        private int ReadWhole(TextBox box, string label, int min, int max, string unit)
        {
            string text = box.Text.Trim();
            if (text.Length == 0)
            {
                _validator.AddError(box, $"{label} is required.");
                return 0;
            }
            if (!int.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out int value))
            {
                _validator.AddError(box, $"{label} must be a whole number.");
                return 0;
            }
            _validator.InRange(box, value, min, max, label, unit);
            return value;
        }

        /// <summary>Decimal vital sign with at most one decimal place. Returns 0 when blank or invalid (errors are added).</summary>
        private decimal ReadDecimal(TextBox box, string label, bool required)
        {
            string text = box.Text.Trim();
            if (text.Length == 0)
            {
                if (required) _validator.AddError(box, $"{label} is required.");
                return 0;
            }
            if (!ValidationHelper.TryParseDecimal(text, out decimal value))
            {
                _validator.AddError(box, $"{label} must be a number.");
                return 0;
            }
            if (Math.Round(value, 1) != value)
            {
                _validator.AddError(box, $"{label} can have at most 1 decimal place.");
                return 0;
            }
            return value;
        }

        // ------------------------------------------------------------------
        // History and chart note
        // ------------------------------------------------------------------

        private void LoadHistory()
        {
            try
            {
                string filter = txtHistorySearch.Text.Trim();
                _history = _repository.GetHistory(filter);

                dgvHistory.DataSource = null;
                dgvHistory.DataSource = _history;
                dgvHistory.ClearSelection();

                lblHistoryCount.Text = _history.Count == 0
                    ? (filter.Length == 0 ? "No consultation has been recorded yet." : "No consultation found for this patient.")
                    : $"{_history.Count} consultation(s)" + (filter.Length == 0 ? string.Empty : " for this search");

                if (_history.Count > 0) dgvHistory.Rows[0].Selected = true;
                ShowSelectedNote();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The consultation history could not be loaded.", DialogOwner());
            }
        }

        private void ShowSelectedNote()
        {
            ConsultationRecord? record = dgvHistory.SelectedRows.Count > 0
                ? dgvHistory.SelectedRows[0].DataBoundItem as ConsultationRecord
                : null;
            ShowNote(record);
        }

        /// <summary>Writes the read-only chart note of one consultation.</summary>
        private void ShowNote(ConsultationRecord? r)
        {
            rtbNote.Clear();
            if (r == null)
            {
                Append("Select a consultation in the list to see its chart note.", false, ThemeHelper.TextMuted, 10f);
                return;
            }

            Append(SettingsHelper.GetString(SettingsHelper.HospitalName, "Hospital Management System").ToUpperInvariant() + "\n", true, ThemeHelper.TextMuted, 10f);
            Append("CHART NOTE  -  " + r.ConsultationId + "\n", true, ThemeHelper.Accent, 15f);
            Append("Recorded " + r.RecordedText + (string.IsNullOrEmpty(r.RecordedBy) ? string.Empty : "  by " + r.RecordedBy) + "\n\n", false, ThemeHelper.TextMuted, 10f);

            Section("PATIENT");
            Line("Name", r.PatientName);
            Line("Patient ID", r.PatientId + "   |   Age at visit: " + r.AgeAtVisit);
            Line("Allergies", r.Allergies);

            Section("VISIT");
            Line("Appointment", r.AppointmentId + "   |   " + r.AppointmentDate.ToString("ddd, MMM d, yyyy", CultureInfo.InvariantCulture));
            Line("Doctor", r.DoctorText);

            Section("VITAL SIGNS");
            Line("Blood pressure", r.BloodPressureText);
            Line("Temperature", r.TemperatureC.ToString("0.0", CultureInfo.InvariantCulture) + " \u00B0C");
            Line("Pulse", r.PulseRateBpm + " bpm");
            Line("Weight", r.WeightKg.ToString("0.0", CultureInfo.InvariantCulture) + " kg");
            Line("Height", r.HeightCm.HasValue ? r.HeightCm.Value.ToString("0.0", CultureInfo.InvariantCulture) + " cm" : string.Empty);
            if (r.BmiText.Length > 0) Line("BMI", r.BmiText + " kg/m\u00B2");

            Section("CHIEF COMPLAINT");
            Body(r.ChiefComplaint);
            Section("EXAMINATION FINDINGS");
            Body(r.ExaminationFindings);
            Section("DIAGNOSIS");
            Body(r.Diagnosis);
            Section("TREATMENT PLAN");
            Body(r.TreatmentPlan);
            Section("SPECIAL INSTRUCTIONS");
            Body(r.SpecialInstructions);
            Section("NEXT FOLLOW-UP");
            Body(r.NextFollowUpDate.HasValue ? r.NextFollowUpDate.Value.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture) : string.Empty);

            rtbNote.SelectAll();
            rtbNote.SelectionTabs = new[] { 150 };      // aligns the values after "Caption:" in every line
            rtbNote.SelectionStart = 0;
            rtbNote.SelectionLength = 0;
            rtbNote.ScrollToCaret();
        }

        private void Section(string title) => Append("\n" + title + "\n", true, ThemeHelper.Accent, 10f);

        private void Line(string caption, string value) =>
            Append(caption + ":\t", false, ThemeHelper.TextMuted, 10f, follow: (string.IsNullOrWhiteSpace(value) ? "-" : value) + "\n");

        private void Body(string text) => Append((string.IsNullOrWhiteSpace(text) ? "-" : text.Trim()) + "\n", false, ThemeHelper.TextPrimary, 10f);

        private void Append(string text, bool bold, Color color, float size, string? follow = null)
        {
            rtbNote.SelectionStart = rtbNote.TextLength;
            rtbNote.SelectionLength = 0;
            rtbNote.SelectionFont = new Font("Segoe UI", size, bold ? FontStyle.Bold : FontStyle.Regular, GraphicsUnit.Point);
            rtbNote.SelectionColor = color;
            rtbNote.AppendText(text);

            if (follow != null)
            {
                rtbNote.SelectionStart = rtbNote.TextLength;
                rtbNote.SelectionLength = 0;
                rtbNote.SelectionFont = new Font("Segoe UI", 10f, FontStyle.Regular, GraphicsUnit.Point);
                rtbNote.SelectionColor = ThemeHelper.TextPrimary;
                rtbNote.AppendText(follow);
            }
        }
    }
}