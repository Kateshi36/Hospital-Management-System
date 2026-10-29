using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP07_Prescription;
using HospitalManagementSystem.Repositories.MP07_Prescription;
using System.Globalization;

namespace HospitalManagementSystem.Views.MP07_Prescription
{
    /// <summary>
    /// MP07 (Step 11) - Prescription Builder. Two views, switched with the buttons at the top:
    ///
    ///   NEW PRESCRIPTION
    ///     1. pick a consultation of the last 60 days (filter by patient);
    ///     2. set General Instructions (optional) and Valid Until (default today + the setting, editable);
    ///     3. add medicine lines: the medicine is chosen from the catalog (no free typing), then Dosage, Times per day and
    ///        Duration in days; Quantity = times per day x days is calculated and read-only; Edit / Remove a line before saving.
    ///     A line whose quantity is more than the usable stock gets a warning and a confirmation, it is never blocked.
    ///     At least one line is needed. Save generates PRE-YYYYMMDD-XXX and opens a print-style preview.
    ///
    ///   ISSUED PRESCRIPTIONS
    ///     every prescription issued so far (filter by patient) with the same print-style preview.
    ///
    /// Every rule lives in PrescriptionRepository (Validate and Save); the form shows the same rules next to the fields.
    /// All visuals live in PrescriptionForm.Designer.cs; the grid columns are created here (BuildGridColumns).
    /// </summary>
    public partial class PrescriptionForm : Views.Main.PageHost
    {
        private readonly PrescriptionRepository _repository = new PrescriptionRepository();
        private readonly FormValidator _validator;

        private List<ConsultationChoice> _consultations = new List<ConsultationChoice>();
        private List<MedicineChoice> _medicines = new List<MedicineChoice>();
        private List<PrescriptionRecord> _issued = new List<PrescriptionRecord>();
        private readonly List<PrescriptionLine> _lines = new List<PrescriptionLine>();
        private ConsultationChoice? _selected;
        private int _editIndex = -1;            // index of the line being edited, -1 = adding a new line
        private bool _loading;                  // true while the code (not the user) fills lists
        private bool _initialized;
        private bool _issuedMode;

        public PrescriptionForm()
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtInstructions, dtpValid, cboMedicine, txtDosage, txtFrequency, txtDuration, txtLineInstructions);

            BuildGridColumns();
            GridHelper.Apply(dgvConsultations);
            GridHelper.Apply(dgvItems);
            GridHelper.Apply(dgvIssued);

            btnCancelEdit.Visible = false;
            ApplyInputRules();
            WireEvents();
            Load += (s, e) => InitializeScreen();
        }

        private IWin32Window DialogOwner() => (IWin32Window?)TopLevelControl ?? this;

        /// <summary>
        /// Creates the columns of the three grids. They are built here, not in the Designer, because Visual Studio's
        /// [Design] view deletes designer-made grid columns when it re-saves a large designer file.
        /// </summary>
        private void BuildGridColumns()
        {
            foreach (DataGridView grid in new[] { dgvConsultations, dgvItems, dgvIssued }) grid.AutoGenerateColumns = false;

            AddColumn(dgvConsultations, "colCCode", "Consultation ID", nameof(ConsultationChoice.ConsultationId), 20);
            AddColumn(dgvConsultations, "colCDate", "Recorded", nameof(ConsultationChoice.RecordedText), 20);
            AddColumn(dgvConsultations, "colCPatient", "Patient", nameof(ConsultationChoice.PatientName), 24);
            AddColumn(dgvConsultations, "colCDiagnosis", "Diagnosis", nameof(ConsultationChoice.Diagnosis), 28);
            AddColumn(dgvConsultations, "colCRx", "Rx", nameof(ConsultationChoice.PrescriptionCountText), 8);

            AddColumn(dgvItems, "colIMedicine", "Medicine", nameof(PrescriptionLine.MedicineName), 26);
            AddColumn(dgvItems, "colIDosage", "Dosage", nameof(PrescriptionLine.Dosage), 12);
            AddColumn(dgvItems, "colIFreq", "Per day", nameof(PrescriptionLine.FrequencyPerDay), 9);
            AddColumn(dgvItems, "colIDays", "Days", nameof(PrescriptionLine.DurationDays), 8);
            AddColumn(dgvItems, "colIQty", "Qty", nameof(PrescriptionLine.Quantity), 8);
            AddColumn(dgvItems, "colIInstr", "Instructions", nameof(PrescriptionLine.Instructions), 24);
            AddColumn(dgvItems, "colIStock", "Stock check", nameof(PrescriptionLine.StockCheckText), 13);

            AddColumn(dgvIssued, "colRCode", "Prescription ID", nameof(PrescriptionRecord.PrescriptionId), 18);
            AddColumn(dgvIssued, "colRIssued", "Issued", nameof(PrescriptionRecord.IssuedText), 14);
            AddColumn(dgvIssued, "colRValid", "Valid until", nameof(PrescriptionRecord.ValidUntilText), 14);
            AddColumn(dgvIssued, "colRPatient", "Patient", nameof(PrescriptionRecord.PatientName), 22);
            AddColumn(dgvIssued, "colRDoctor", "Doctor", nameof(PrescriptionRecord.DoctorText), 18);
            AddColumn(dgvIssued, "colRLines", "Medicines", nameof(PrescriptionRecord.LineCount), 10);
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

        private void ApplyInputRules()
        {
            InputHelper.Attach(txtDosage, InputKind.Address, PrescriptionRules.MaxDosage);
            InputHelper.Attach(txtFrequency, InputKind.Digits, 2);
            InputHelper.Attach(txtDuration, InputKind.Digits, 3);
            InputHelper.Attach(txtLineInstructions, InputKind.Address, PrescriptionRules.MaxLineInstructions);
            InputHelper.Attach(txtInstructions, InputKind.Address, PrescriptionRules.MaxGeneralInstructions);
            InputHelper.Attach(txtConsultSearch, InputKind.Address, 60);
            InputHelper.Attach(txtIssuedSearch, InputKind.Address, 60);
        }

        private void WireEvents()
        {
            btnModeNew.Click += (s, e) => ShowMode(false);
            btnModeIssued.Click += (s, e) => ShowMode(true);

            btnConsultSearch.Click += (s, e) => LoadConsultations();
            txtConsultSearch.KeyDown += (s, e) => OnEnter(e, LoadConsultations);
            SearchHelper.AttachLive(txtConsultSearch, LoadConsultations);
            dgvConsultations.SelectionChanged += (s, e) => OnConsultationSelected();

            cboMedicine.SelectedIndexChanged += (s, e) => { if (!_loading) UpdateStockHint(); };
            txtFrequency.TextChanged += (s, e) => UpdateQuantity();
            txtDuration.TextChanged += (s, e) => UpdateQuantity();

            btnAddLine.Click += (s, e) => AddOrUpdateLine();
            btnCancelEdit.Click += (s, e) => CancelEdit();
            btnEditLine.Click += (s, e) => StartEdit();
            btnRemoveLine.Click += (s, e) => RemoveLine();
            dgvItems.SelectionChanged += (s, e) => UpdateItemButtons();
            dgvItems.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) StartEdit(); };
            dgvItems.CellFormatting += OnItemCellFormatting;

            btnSave.Click += (s, e) => SavePrescription();
            btnClear.Click += (s, e) => ClearForm();

            btnIssuedSearch.Click += (s, e) => LoadIssued();
            btnIssuedAll.Click += (s, e) => { txtIssuedSearch.Clear(); LoadIssued(); };
            txtIssuedSearch.KeyDown += (s, e) => OnEnter(e, LoadIssued);
            SearchHelper.AttachLive(txtIssuedSearch, LoadIssued);
            dgvIssued.SelectionChanged += (s, e) => btnPreview.Enabled = dgvIssued.SelectedRows.Count > 0;
            dgvIssued.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) PreviewSelected(); };
            btnPreview.Click += (s, e) => PreviewSelected();

            VisibleChanged += (s, e) =>
            {
                if (Visible && _initialized && !_issuedMode) { ReloadMedicines(); LoadConsultations(); }
            };
        }

        private static void OnEnter(KeyEventArgs e, Action action)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            e.Handled = true;
            action();
        }

        // ------------------------------------------------------------------
        // Start-up and view switching
        // ------------------------------------------------------------------

        private void InitializeScreen()
        {
            lblDate.Text = DateTime.Today.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture) +
                          "  ·  Write a prescription for a recent consultation.";
            lblIssuedInfo.Text = "Issue date: today, " + DateTime.Today.ToString("MMM d, yyyy", CultureInfo.InvariantCulture);

            dtpValid.MinDate = DateTime.Today;
            dtpValid.MaxDate = DateTime.Today.AddDays(PrescriptionRules.MaxValidityDays);
            dtpValid.Value = PrescriptionRepository.DefaultValidUntil;

            ReloadMedicines();
            _initialized = true;
            ShowMode(false);
        }

        private void ShowMode(bool issued)
        {
            _issuedMode = issued;
            pnlNew.Visible = !issued;
            pnlIssued.Visible = issued;
            ThemeHelper.StyleButton(btnModeNew, issued ? ButtonKind.Secondary : ButtonKind.Primary);
            ThemeHelper.StyleButton(btnModeIssued, issued ? ButtonKind.Primary : ButtonKind.Secondary);

            if (!_initialized) return;
            if (issued) LoadIssued();
            else LoadConsultations();
        }

        private void SetStatus(string text, Color color)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = color;
        }

        /// <summary>(Re)loads the medicine drop-down with the current stock and keeps the chosen medicine.</summary>
        private void ReloadMedicines()
        {
            int keep = cboMedicine.SelectedValue is int id ? id : 0;
            _loading = true;
            try
            {
                _medicines = _repository.GetMedicines();
                cboMedicine.DataSource = _medicines;
                cboMedicine.DisplayMember = nameof(MedicineChoice.Display);
                cboMedicine.ValueMember = nameof(MedicineChoice.MedicineId);
                cboMedicine.SelectedValue = keep;
                if (cboMedicine.SelectedIndex < 0) cboMedicine.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The medicine catalog could not be loaded.", DialogOwner());
            }
            finally
            {
                _loading = false;
            }
            UpdateStockHint();
        }

        // ------------------------------------------------------------------
        // Consultation picker
        // ------------------------------------------------------------------

        private void LoadConsultations()
        {
            string? keep = _selected?.ConsultationId;
            try
            {
                _consultations = _repository.GetConsultations(txtConsultSearch.Text.Trim());

                _loading = true;
                dgvConsultations.DataSource = null;
                dgvConsultations.DataSource = _consultations;
                dgvConsultations.ClearSelection();
                dgvConsultations.CurrentCell = null;
                _loading = false;

                if (keep != null)
                {
                    foreach (DataGridViewRow row in dgvConsultations.Rows)
                    {
                        if (row.DataBoundItem is ConsultationChoice c && c.ConsultationId == keep) { row.Selected = true; break; }
                    }
                }
                OnConsultationSelected(force: true);

                SetStatus(_consultations.Count == 0 ? "No consultation found." : $"{_consultations.Count} consultation(s) of the last {PrescriptionRules.ConsultationLookbackDays} days.", ThemeHelper.TextMuted);
            }
            catch (Exception ex)
            {
                _loading = false;
                MessageHelper.Error(ex, "The consultations could not be loaded.", DialogOwner());
            }
        }

        private void OnConsultationSelected(bool force = false)
        {
            if (_loading && !force) return;

            if (dgvConsultations.SelectedRows.Count == 0 || dgvConsultations.SelectedRows[0].DataBoundItem is not ConsultationChoice choice)
            {
                _selected = null;
                lblSelected.Text = "No consultation selected yet";
                lblSelected.ForeColor = ThemeHelper.TextMuted;
                btnSave.Enabled = false;
                return;
            }

            _selected = choice;
            _validator.ClearError(dgvConsultations);

            string second = "Diagnosis: " + (string.IsNullOrWhiteSpace(choice.Diagnosis) ? "-" : choice.Diagnosis);
            if (!string.IsNullOrWhiteSpace(choice.Allergies)) second += "   |   Allergies: " + choice.Allergies;
            lblSelected.Text = $"{choice.PatientName}, {choice.Age} y   |   {choice.PatientId}   |   Dr. {choice.DoctorName}\n{second}";
            lblSelected.ForeColor = ThemeHelper.Success;
            btnSave.Enabled = true;
        }

        // ------------------------------------------------------------------
        // Medicine line mini-form
        // ------------------------------------------------------------------

        private static int ParseWhole(string text) => int.TryParse(text.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out int v) ? v : 0;

        /// <summary>Quantity = times per day x days (blank until both numbers are valid).</summary>
        private void UpdateQuantity()
        {
            int freq = ParseWhole(txtFrequency.Text);
            int days = ParseWhole(txtDuration.Text);
            bool valid = freq >= 1 && freq <= PrescriptionRules.FrequencyMax && days >= 1 && days <= PrescriptionRules.DurationMax;
            txtQuantity.Text = valid ? (freq * days).ToString(CultureInfo.InvariantCulture) : string.Empty;
            UpdateStockHint();
        }

        /// <summary>Shows the usable stock of the chosen medicine and warns when the quantity is more than that.</summary>
        private void UpdateStockHint()
        {
            if (cboMedicine.SelectedItem is not MedicineChoice med || med.MedicineId.Length == 0)
            {
                lblStockHint.Text = "Choose a medicine to see its stock.";
                lblStockHint.ForeColor = ThemeHelper.TextMuted;
                return;
            }

            int quantity = ParseWhole(txtQuantity.Text);
            string status = med.StockStatus == "Normal" ? string.Empty : $"   (status: {med.StockStatus})";

            if (quantity > 0 && quantity > med.UsableStock)
            {
                lblStockHint.Text = $"Warning: this line needs {quantity} but only {med.UsableStock} are available (short by {quantity - med.UsableStock}). You can still add it.{status}";
                lblStockHint.ForeColor = ThemeHelper.Warning;
            }
            else
            {
                lblStockHint.Text = quantity > 0
                    ? $"Stock is enough: {med.UsableStock} available for {quantity} needed.{status}"
                    : $"Available stock (not expired): {med.UsableStock}.{status}";
                lblStockHint.ForeColor = quantity > 0 ? ThemeHelper.Success : ThemeHelper.TextMuted;
            }
        }

        private int ReadWhole(TextBox box, string label, int min, int max, string unit)
        {
            string text = box.Text.Trim();
            if (text.Length == 0) { _validator.AddError(box, $"{label} is required."); return 0; }
            if (!int.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out int value))
            {
                _validator.AddError(box, $"{label} must be a whole number.");
                return 0;
            }
            _validator.InRange(box, value, min, max, label, unit);
            return value;
        }

        private void AddOrUpdateLine()
        {
            _validator.Begin();

            MedicineChoice? med = cboMedicine.SelectedItem as MedicineChoice;
            bool medicineChosen = med != null && med.MedicineId.Length > 0;
            _validator.Check(medicineChosen, cboMedicine, "Choose the medicine from the catalog list.");

            string dosage = txtDosage.Text.Trim();
            _validator.Check(dosage.Length > 0, txtDosage, "Enter the dosage, for example 500mg.");

            int frequency = ReadWhole(txtFrequency, "Times per day", 1, PrescriptionRules.FrequencyMax, "times per day");
            int duration = ReadWhole(txtDuration, "Duration", 1, PrescriptionRules.DurationMax, "days");

            if (medicineChosen)
            {
                for (int i = 0; i < _lines.Count; i++)
                {
                    if (i != _editIndex && _lines[i].MedicineId == med!.MedicineId)
                    {
                        _validator.AddError(cboMedicine, $"{med.MedicineName} is already in this prescription. Edit that line instead.");
                        break;
                    }
                }
            }

            if (!_validator.IsValid)
            {
                _validator.ShowErrors(DialogOwner());
                return;
            }

            var line = new PrescriptionLine
            {
                MedicineId = med!.MedicineId,
                MedicineName = med.MedicineName,
                Dosage = dosage,
                FrequencyPerDay = frequency,
                DurationDays = duration,
                Instructions = txtLineInstructions.Text.Trim(),
                AvailableStock = med.UsableStock
            };

            if (line.ShortBy > 0 && !MessageHelper.Confirm(
                    $"{med.MedicineName}: the quantity {line.Quantity} is more than the {med.UsableStock} available in stock (short by {line.ShortBy}).\n\nAdd the line anyway?",
                    "Low stock warning", DialogOwner()))
            {
                return;
            }

            if (_editIndex >= 0 && _editIndex < _lines.Count) _lines[_editIndex] = line;
            else _lines.Add(line);

            CancelEdit();
            RebindItems();
        }

        private void StartEdit()
        {
            if (dgvItems.SelectedRows.Count == 0) return;
            int index = dgvItems.SelectedRows[0].Index;
            if (index < 0 || index >= _lines.Count) return;

            PrescriptionLine line = _lines[index];
            _editIndex = index;
            _validator.Begin();

            cboMedicine.SelectedValue = line.MedicineId;
            txtDosage.Text = line.Dosage;
            txtFrequency.Text = line.FrequencyPerDay.ToString(CultureInfo.InvariantCulture);
            txtDuration.Text = line.DurationDays.ToString(CultureInfo.InvariantCulture);
            txtLineInstructions.Text = line.Instructions;

            btnAddLine.Text = "Update Line";
            btnCancelEdit.Visible = true;
            lblMedicineStep.Text = "3  ·  EDIT THE MEDICINE LINE";
            txtDosage.Focus();
        }

        /// <summary>Leaves edit mode and empties the mini-form.</summary>
        private void CancelEdit()
        {
            _editIndex = -1;
            btnAddLine.Text = "Add Line";
            btnCancelEdit.Visible = false;
            lblMedicineStep.Text = "3  ·  ADD A MEDICINE LINE";

            _validator.Begin();
            cboMedicine.SelectedIndex = 0;
            txtDosage.Clear();
            txtFrequency.Clear();
            txtDuration.Clear();
            txtLineInstructions.Clear();
            UpdateQuantity();
        }

        private void RemoveLine()
        {
            if (dgvItems.SelectedRows.Count == 0) return;
            int index = dgvItems.SelectedRows[0].Index;
            if (index < 0 || index >= _lines.Count) return;

            if (!MessageHelper.Confirm($"Remove {_lines[index].MedicineName} from this prescription?", "Remove line", DialogOwner())) return;

            _lines.RemoveAt(index);
            if (_editIndex >= 0) CancelEdit();
            RebindItems();
        }

        private void RebindItems()
        {
            dgvItems.DataSource = null;
            dgvItems.DataSource = _lines;
            dgvItems.ClearSelection();

            lblItemsTitle.Text = $"Medicines in this prescription  ·  {_lines.Count}";
            lblTotals.Text = _lines.Count == 0
                ? string.Empty
                : $"{_lines.Count} medicine(s), {_lines.Sum(l => l.Quantity)} units in total";
            _validator.ClearError(dgvItems);
            UpdateItemButtons();
        }

        private void UpdateItemButtons()
        {
            bool selected = dgvItems.SelectedRows.Count > 0;
            btnEditLine.Enabled = selected;
            btnRemoveLine.Enabled = selected;
        }

        private void OnItemCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvItems.Rows.Count) return;
            if (dgvItems.Columns[e.ColumnIndex].Name != "colIStock") return;
            if (dgvItems.Rows[e.RowIndex].DataBoundItem is not PrescriptionLine line) return;

            e.CellStyle!.ForeColor = line.ShortBy > 0 ? ThemeHelper.Warning : ThemeHelper.Success;
            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
        }

        // ------------------------------------------------------------------
        // Save
        // ------------------------------------------------------------------

        private void ClearForm()
        {
            _lines.Clear();
            CancelEdit();
            txtInstructions.Clear();
            dtpValid.Value = PrescriptionRepository.DefaultValidUntil;
            RebindItems();
            _validator.Begin();
        }

        private void SavePrescription()
        {
            if (_editIndex >= 0)
            {
                MessageHelper.Warning("Finish the line you are editing (Update Line) or press Cancel Edit before saving.", "Prescription", DialogOwner());
                return;
            }

            _validator.Begin();
            _validator.Check(_selected != null, dgvConsultations, "Select the consultation first.");
            _validator.Check(_lines.Count > 0, dgvItems, "Add at least one medicine line before saving.");
            _validator.Check(dtpValid.Value.Date >= DateTime.Today, dtpValid, "Valid Until cannot be before today.");
            if (!_validator.IsValid)
            {
                _validator.ShowErrors(DialogOwner());
                return;
            }

            var input = new PrescriptionInput
            {
                ConsultationId = _selected!.ConsultationId,
                GeneralInstructions = txtInstructions.Text.Trim(),
                ValidUntil = dtpValid.Value.Date,
                Lines = _lines.Select(l => new PrescriptionLine
                {
                    MedicineId = l.MedicineId,
                    MedicineName = l.MedicineName,
                    Dosage = l.Dosage,
                    FrequencyPerDay = l.FrequencyPerDay,
                    DurationDays = l.DurationDays,
                    Instructions = l.Instructions
                }).ToList()
            };

            try
            {
                PrescriptionRecord saved = _repository.Save(input);

                ClearForm();
                ReloadMedicines();
                LoadConsultations();
                SetStatus($"Saved {saved.PrescriptionId} ({saved.PatientName}).", ThemeHelper.Success);

                using var preview = new PrescriptionPreviewDialog(saved);
                preview.ShowDialog(DialogOwner());
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Prescription", DialogOwner());
                LoadConsultations();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The prescription could not be saved.", DialogOwner());
            }
        }

        // ------------------------------------------------------------------
        // Issued prescriptions
        // ------------------------------------------------------------------

        private void LoadIssued()
        {
            try
            {
                string filter = txtIssuedSearch.Text.Trim();
                _issued = _repository.GetIssued(filter);

                dgvIssued.DataSource = null;
                dgvIssued.DataSource = _issued;
                dgvIssued.ClearSelection();
                btnPreview.Enabled = false;

                lblIssuedCount.Text = _issued.Count == 0
                    ? (filter.Length == 0 ? "No prescription has been issued yet." : "No prescription found for this patient.")
                    : $"{_issued.Count} prescription(s)" + (filter.Length == 0 ? string.Empty : " for this search");
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The issued prescriptions could not be loaded.", DialogOwner());
            }
        }

        private void PreviewSelected()
        {
            if (dgvIssued.SelectedRows.Count == 0 || dgvIssued.SelectedRows[0].DataBoundItem is not PrescriptionRecord header) return;

            try
            {
                PrescriptionRecord? full = _repository.GetPrescription(header.PrescriptionId);
                if (full == null)
                {
                    MessageHelper.Warning("This prescription no longer exists. The list will be refreshed.", "Prescription", DialogOwner());
                    LoadIssued();
                    return;
                }
                AuditHelper.LogView(AuditHelper.Prescription, full.PrescriptionId, full.PatientId, "Opened the prescription");
                using var preview = new PrescriptionPreviewDialog(full);
                preview.ShowDialog(DialogOwner());
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The prescription could not be opened.", DialogOwner());
            }
        }
    }
}