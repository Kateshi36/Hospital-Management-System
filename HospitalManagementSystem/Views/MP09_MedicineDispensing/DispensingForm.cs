using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP09_Dispensing;
using HospitalManagementSystem.Repositories.MP09_Dispensing;
using System.Globalization;

namespace HospitalManagementSystem.Views.MP09_Dispensing
{
    /// <summary>
    /// MP09 (Step 13) - Medicine Dispensing Simulator. Two views, switched with the buttons at the top:
    ///
    ///   DISPENSE PRESCRIPTION  (a three-step wizard in one screen)
    ///     Step 1  pick a prescription. Expired and fully dispensed prescriptions are listed but blocked with a clear message;
    ///             a partly dispensed one only allows the remaining quantities.
    ///     Step 2  for each line see prescribed / already dispensed / remaining / usable stock and set "Quantity to dispense".
    ///             It can never be more than the remaining quantity or the stock. DECISION: when the stock is short the
    ///             pharmacist dispenses what is available (partial dispensing); the rest stays open on the prescription.
    ///     Step 3  choose the payment method, check the total (quantity x unit price) and confirm. The stock is reduced
    ///             oldest-expiry-first (FIFO), MS-YYYYMMDD-XXX is generated and a print-style dispensing slip opens.
    ///
    ///   DISPENSING HISTORY
    ///     every dispensing so far (filter by patient, prescription or dispensing code) with the same slip.
    ///
    /// Every rule lives in DispensingRepository (Validate and Dispense); the form shows the same rules next to the fields.
    /// All visuals live in DispensingForm.Designer.cs; the grid columns are created here (BuildGridColumns).
    /// </summary>
    public partial class DispensingForm : Form
    {
        private const int StepCount = 3;

        private readonly DispensingRepository _repository = new DispensingRepository();
        private readonly FormValidator _validator;

        private List<PrescriptionChoice> _prescriptions = new List<PrescriptionChoice>();
        private List<DispenseLine> _previewLines = new List<DispenseLine>();
        private List<DispenseLine> _lines = new List<DispenseLine>();
        private List<DispenseLine> _summary = new List<DispenseLine>();
        private List<DispensingSlip> _history = new List<DispensingSlip>();
        private List<LookupItem> _payments = new List<LookupItem>();
        private PrescriptionChoice? _selected;
        private int _step = 1;
        private bool _loading;                  // true while the code (not the user) fills lists and controls
        private bool _initialized;
        private bool _historyMode;

        public DispensingForm()
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(cboPayment);

            BuildGridColumns();
            GridHelper.Apply(dgvPrescriptions);
            GridHelper.Apply(dgvRxLines);
            GridHelper.Apply(dgvDispense);
            GridHelper.Apply(dgvSummary);
            GridHelper.Apply(dgvHistory);

            ApplyInputRules();
            WireEvents();
            Load += (s, e) => InitializeScreen();
        }

        private IWin32Window DialogOwner() => (IWin32Window?)TopLevelControl ?? this;

        /// <summary>
        /// Creates the columns of the five grids. They are built here, not in the Designer, because Visual Studio's
        /// [Design] view deletes designer-made grid columns when it re-saves a large designer file.
        /// </summary>
        private void BuildGridColumns()
        {
            foreach (DataGridView grid in new[] { dgvPrescriptions, dgvRxLines, dgvDispense, dgvSummary, dgvHistory })
                grid.AutoGenerateColumns = false;

            AddColumn(dgvPrescriptions, "colPCode", "Prescription", nameof(PrescriptionChoice.PrescriptionId), 17);
            AddColumn(dgvPrescriptions, "colPPatient", "Patient", nameof(PrescriptionChoice.PatientName), 20);
            AddColumn(dgvPrescriptions, "colPDoctor", "Doctor", nameof(PrescriptionChoice.DoctorText), 17);
            AddColumn(dgvPrescriptions, "colPIssued", "Issued", nameof(PrescriptionChoice.IssuedText), 12);
            AddColumn(dgvPrescriptions, "colPValid", "Valid until", nameof(PrescriptionChoice.ValidUntilText), 12);
            AddColumn(dgvPrescriptions, "colPProgress", "Dispensed", nameof(PrescriptionChoice.ProgressText), 10, DataGridViewContentAlignment.MiddleCenter);
            AddColumn(dgvPrescriptions, "colPStatus", "Status", nameof(PrescriptionChoice.StatusText), 16, DataGridViewContentAlignment.MiddleCenter);

            AddColumn(dgvRxLines, "colRMedicine", "Medicine", nameof(DispenseLine.MedicineName), 28);
            AddColumn(dgvRxLines, "colRDosage", "Dosage", nameof(DispenseLine.Dosage), 12);
            AddColumn(dgvRxLines, "colRPrescribed", "Prescribed", nameof(DispenseLine.QuantityPrescribed), 12, DataGridViewContentAlignment.MiddleRight);
            AddColumn(dgvRxLines, "colRDispensed", "Already dispensed", nameof(DispenseLine.QuantityAlreadyDispensed), 16, DataGridViewContentAlignment.MiddleRight);
            AddColumn(dgvRxLines, "colRRemaining", "Remaining", nameof(DispenseLine.QuantityRemaining), 12, DataGridViewContentAlignment.MiddleRight);
            AddColumn(dgvRxLines, "colRStock", "In stock", nameof(DispenseLine.UsableStock), 12, DataGridViewContentAlignment.MiddleRight);

            AddColumn(dgvDispense, "colDMedicine", "Medicine", nameof(DispenseLine.MedicineName), 20);
            AddColumn(dgvDispense, "colDDosage", "Dosage", nameof(DispenseLine.Dosage), 9);
            AddColumn(dgvDispense, "colDPrescribed", "Prescribed", nameof(DispenseLine.QuantityPrescribed), 9, DataGridViewContentAlignment.MiddleRight);
            AddColumn(dgvDispense, "colDDispensed", "Already dispensed", nameof(DispenseLine.QuantityAlreadyDispensed), 11, DataGridViewContentAlignment.MiddleRight);
            AddColumn(dgvDispense, "colDRemaining", "Remaining", nameof(DispenseLine.QuantityRemaining), 9, DataGridViewContentAlignment.MiddleRight);
            AddColumn(dgvDispense, "colDStock", "In stock", nameof(DispenseLine.UsableStock), 8, DataGridViewContentAlignment.MiddleRight);
            AddColumn(dgvDispense, "colDQty", "To dispense", nameof(DispenseLine.QuantityToDispense), 10, DataGridViewContentAlignment.MiddleRight);
            AddColumn(dgvDispense, "colDPrice", "Unit price", nameof(DispenseLine.UnitPriceText), 10, DataGridViewContentAlignment.MiddleRight);
            AddColumn(dgvDispense, "colDTotal", "Line total", nameof(DispenseLine.LineTotalText), 11, DataGridViewContentAlignment.MiddleRight);
            AddColumn(dgvDispense, "colDCheck", "Check", nameof(DispenseLine.CheckText), 17, DataGridViewContentAlignment.MiddleCenter);

            AddColumn(dgvSummary, "colSMedicine", "Medicine", nameof(DispenseLine.MedicineName), 30);
            AddColumn(dgvSummary, "colSDosage", "Dosage", nameof(DispenseLine.Dosage), 12);
            AddColumn(dgvSummary, "colSQty", "Quantity", nameof(DispenseLine.QuantityToDispense), 12, DataGridViewContentAlignment.MiddleRight);
            AddColumn(dgvSummary, "colSPrice", "Unit price", nameof(DispenseLine.UnitPriceText), 14, DataGridViewContentAlignment.MiddleRight);
            AddColumn(dgvSummary, "colSTotal", "Line total", nameof(DispenseLine.LineTotalText), 14, DataGridViewContentAlignment.MiddleRight);
            AddColumn(dgvSummary, "colSCheck", "Check", nameof(DispenseLine.CheckText), 18, DataGridViewContentAlignment.MiddleCenter);

            AddColumn(dgvHistory, "colHCode", "Dispensing", nameof(DispensingSlip.DispensingId), 15);
            AddColumn(dgvHistory, "colHDate", "Date and time", nameof(DispensingSlip.DispensedAtText), 16);
            AddColumn(dgvHistory, "colHPatient", "Patient", nameof(DispensingSlip.PatientName), 18);
            AddColumn(dgvHistory, "colHRx", "Prescription", nameof(DispensingSlip.PrescriptionId), 15);
            AddColumn(dgvHistory, "colHItems", "Medicines", nameof(DispensingSlip.ItemCount), 9, DataGridViewContentAlignment.MiddleCenter);
            AddColumn(dgvHistory, "colHTotal", "Total", nameof(DispensingSlip.TotalText), 11, DataGridViewContentAlignment.MiddleRight);
            AddColumn(dgvHistory, "colHPayment", "Payment", nameof(DispensingSlip.PaymentMethod), 10);
            AddColumn(dgvHistory, "colHBy", "Dispensed by", nameof(DispensingSlip.DispensedBy), 14);
        }

        private static void AddColumn(DataGridView grid, string name, string header, string property, float weight,
                                      DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleLeft)
        {
            var column = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                DataPropertyName = property,
                FillWeight = weight,
                MinimumWidth = 6,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            column.DefaultCellStyle.Alignment = alignment;
            grid.Columns.Add(column);
        }

        private void ApplyInputRules()
        {
            InputHelper.Attach(txtRxSearch, InputKind.Address, 60);
            InputHelper.Attach(txtHistSearch, InputKind.Address, 60);
        }

        private void WireEvents()
        {
            btnModeDispense.Click += (s, e) => ShowMode(false);
            btnModeHistory.Click += (s, e) => ShowMode(true);

            btnRxSearch.Click += (s, e) => LoadPrescriptions();
            btnRxAll.Click += (s, e) => { txtRxSearch.Clear(); chkOpenOnly.Checked = false; LoadPrescriptions(); };
            txtRxSearch.KeyDown += (s, e) => OnEnter(e, () => LoadPrescriptions());
            chkOpenOnly.CheckedChanged += (s, e) => { if (_initialized && !_loading) LoadPrescriptions(); };
            dgvPrescriptions.SelectionChanged += (s, e) => { if (!_loading) OnPrescriptionSelected(); };
            dgvPrescriptions.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) GoNext(); };
            dgvPrescriptions.CellFormatting += OnPrescriptionCellFormatting;

            dgvDispense.SelectionChanged += (s, e) => { if (!_loading) UpdateQuantityEditor(); };
            dgvDispense.CellFormatting += OnDispenseCellFormatting;
            dgvSummary.CellFormatting += OnDispenseCellFormatting;
            nudQty.ValueChanged += (s, e) => OnQuantityChanged();
            btnMax.Click += (s, e) => nudQty.Value = nudQty.Maximum;
            btnNone.Click += (s, e) => nudQty.Value = 0;

            btnStartOver.Click += (s, e) => StartOver();
            btnBack.Click += (s, e) => GoBack();
            btnNext.Click += (s, e) => GoNext();
            btnConfirm.Click += (s, e) => ConfirmDispense();

            btnHistSearch.Click += (s, e) => LoadHistory();
            btnHistAll.Click += (s, e) => { txtHistSearch.Clear(); LoadHistory(); };
            txtHistSearch.KeyDown += (s, e) => OnEnter(e, () => LoadHistory());
            dgvHistory.SelectionChanged += (s, e) => btnViewSlip.Enabled = dgvHistory.SelectedRows.Count > 0;
            dgvHistory.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) ViewSelectedSlip(); };
            btnViewSlip.Click += (s, e) => ViewSelectedSlip();

            VisibleChanged += (s, e) =>
            {
                if (Visible && _initialized && !_historyMode && _step == 1) LoadPrescriptions();
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
                          "  ·  Dispense the medicines of a prescription in three steps.";

            try
            {
                _payments = new List<LookupItem> { LookupItem.Placeholder("- Select a payment method -") };
                _payments.AddRange(_repository.GetPaymentMethods());
                cboPayment.DataSource = null;
                cboPayment.DisplayMember = nameof(LookupItem.Name);
                cboPayment.ValueMember = nameof(LookupItem.Id);
                cboPayment.DataSource = _payments;
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "Could not load the payment methods.", DialogOwner());
            }

            _initialized = true;
            ShowMode(false);
        }

        private void ShowMode(bool history)
        {
            _historyMode = history;
            pnlDispense.Visible = !history;
            pnlHistory.Visible = history;
            ThemeHelper.StyleButton(btnModeDispense, history ? ButtonKind.Secondary : ButtonKind.Primary);
            ThemeHelper.StyleButton(btnModeHistory, history ? ButtonKind.Primary : ButtonKind.Secondary);

            if (!_initialized) return;
            if (history) LoadHistory();
            else { ShowStep(_step); if (_step == 1) LoadPrescriptions(); }
        }

        private void SetStatus(string text, Color color)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = color;
        }

        // ------------------------------------------------------------------
        // Step 1 - prescriptions
        // ------------------------------------------------------------------

        private void LoadPrescriptions()
        {
            if (!_initialized) return;

            string? keep = _selected?.PrescriptionId;
            try
            {
                _prescriptions = _repository.GetPrescriptions(txtRxSearch.Text, chkOpenOnly.Checked);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "Could not load the prescriptions.", DialogOwner());
                return;
            }

            _loading = true;
            dgvPrescriptions.DataSource = null;
            dgvPrescriptions.DataSource = _prescriptions;
            dgvPrescriptions.ClearSelection();
            if (keep != null)
            {
                int index = _prescriptions.FindIndex(p => p.PrescriptionId == keep);
                if (index >= 0)
                {
                    dgvPrescriptions.Rows[index].Selected = true;
                    dgvPrescriptions.CurrentCell = GridHelper.FirstVisibleCell(dgvPrescriptions.Rows[index]);
                }
            }
            _loading = false;

            int ready = _prescriptions.Count(p => p.CanDispense);
            lblRxCount.Text = _prescriptions.Count == 0
                ? "No prescriptions found."
                : $"{_prescriptions.Count} prescription(s) of the last {DispensingRules.PrescriptionLookbackDays} days - {ready} can be dispensed.";
            OnPrescriptionSelected();
        }

        private void OnPrescriptionSelected()
        {
            _selected = dgvPrescriptions.SelectedRows.Count > 0
                ? dgvPrescriptions.SelectedRows[0].DataBoundItem as PrescriptionChoice
                : null;

            if (_selected == null)
            {
                _previewLines = new List<DispenseLine>();
                dgvRxLines.DataSource = null;
                dgvRxLines.DataSource = _previewLines;
                lblSelectedInfo.Text = "Select a prescription from the list above.";
                SetBlocked(string.Empty, ThemeHelper.TextMuted);
                UpdateNavButtons();
                return;
            }

            PrescriptionChoice p = _selected;
            lblSelectedInfo.Text =
                $"Patient: {p.PatientName} ({p.Age} y)   |   Patient ID: {p.PatientId}   |   {p.DoctorText}\n" +
                $"Allergies: {(string.IsNullOrWhiteSpace(p.Allergies) ? "none recorded" : p.Allergies)}   |   Issued {p.IssuedText}   |   Valid until {p.ValidUntilText}";

            try
            {
                _previewLines = _repository.GetLines(p.PrescriptionId);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "Could not load the medicine lines.", DialogOwner());
                _previewLines = new List<DispenseLine>();
            }
            dgvRxLines.DataSource = null;
            dgvRxLines.DataSource = _previewLines;
            dgvRxLines.ClearSelection();

            if (!p.CanDispense) SetBlocked("BLOCKED - " + p.BlockedMessage, ThemeHelper.Danger);
            else if (p.QuantityDispensed > 0)
                SetBlocked($"Partially dispensed before: only the remaining {p.QuantityRemaining} unit(s) can be dispensed. Click Next.", ThemeHelper.Warning);
            else
                SetBlocked("Ready to dispense. Click Next to set the quantities.", ThemeHelper.Success);

            UpdateNavButtons();
        }

        private void SetBlocked(string text, Color color)
        {
            lblBlocked.Text = text;
            lblBlocked.ForeColor = color;
        }

        private void OnPrescriptionCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPrescriptions.Rows.Count) return;
            if (dgvPrescriptions.Columns[e.ColumnIndex].Name != "colPStatus") return;
            if (dgvPrescriptions.Rows[e.RowIndex].DataBoundItem is not PrescriptionChoice choice) return;

            if (!ThemeHelper.TryGetStatusColors(choice.StatusText, out Color back, out Color fore)) return;
            e.CellStyle!.BackColor = back;
            e.CellStyle.ForeColor = fore;
            e.CellStyle.SelectionBackColor = back;
            e.CellStyle.SelectionForeColor = fore;
            e.CellStyle.Font = ThemeHelper.BoldFont;
        }

        // ------------------------------------------------------------------
        // Step 2 - quantities
        // ------------------------------------------------------------------

        private void OnDispenseCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (sender is not DataGridView grid) return;
            if (e.RowIndex < 0 || e.RowIndex >= grid.Rows.Count) return;
            string column = grid.Columns[e.ColumnIndex].Name;
            if (column != "colDCheck" && column != "colSCheck") return;
            if (grid.Rows[e.RowIndex].DataBoundItem is not DispenseLine line) return;

            Color color = line.CheckText switch
            {
                "Full" => ThemeHelper.Success,
                "Already dispensed" => ThemeHelper.TextMuted,
                "Out of stock" => ThemeHelper.Danger,
                "Not dispensing now" => ThemeHelper.Danger,
                _ => ThemeHelper.Warning
            };
            e.CellStyle!.ForeColor = color;
            e.CellStyle.SelectionForeColor = color;
            e.CellStyle.Font = ThemeHelper.BoldFont;
        }

        private DispenseLine? SelectedLine()
        {
            return dgvDispense.SelectedRows.Count > 0 ? dgvDispense.SelectedRows[0].DataBoundItem as DispenseLine : null;
        }

        /// <summary>Fills the quantity box for the selected line: its limit is the smaller of "remaining" and "usable stock".</summary>
        private void UpdateQuantityEditor()
        {
            DispenseLine? line = SelectedLine();
            _loading = true;
            try
            {
                if (line == null)
                {
                    lblSelLine.Text = "Select a medicine in the list.";
                    nudQty.Minimum = 0;
                    nudQty.Maximum = 0;
                    nudQty.Value = 0;
                    nudQty.Enabled = btnMax.Enabled = btnNone.Enabled = false;
                    lblQtyHint.Text = string.Empty;
                    return;
                }

                nudQty.Minimum = 0;
                nudQty.Maximum = Math.Max(0, line.MaxQuantity);
                nudQty.Value = Math.Min(line.QuantityToDispense, nudQty.Maximum);
                bool editable = line.MaxQuantity > 0;
                nudQty.Enabled = btnMax.Enabled = btnNone.Enabled = editable;

                lblSelLine.Text = $"{line.MedicineName}  {line.Dosage}   -   at most {line.MaxQuantity}";
                lblQtyHint.Text = BuildHint(line);
            }
            finally
            {
                _loading = false;
            }
        }

        private static string BuildHint(DispenseLine line)
        {
            if (line.QuantityRemaining <= 0)
                return "This line is already fully dispensed - nothing more can be dispensed for it.";
            if (line.UsableStock <= 0)
                return $"Out of stock (expired batches are not counted). {line.QuantityRemaining} unit(s) remain on the prescription and stay open for a later dispensing.";
            if (line.ShortBy > 0)
                return $"Remaining on the prescription: {line.QuantityRemaining}. Usable stock: {line.UsableStock}. SHORT BY {line.ShortBy}: at most {line.UsableStock} can be dispensed now (partial dispensing); the rest stays open.";
            return $"Remaining on the prescription: {line.QuantityRemaining}. Usable stock: {line.UsableStock}. You may dispense up to {line.QuantityRemaining}.";
        }

        private void OnQuantityChanged()
        {
            if (_loading) return;
            DispenseLine? line = SelectedLine();
            if (line == null) return;

            line.QuantityToDispense = (int)nudQty.Value;
            dgvDispense.InvalidateRow(dgvDispense.SelectedRows[0].Index);
            UpdateNavButtons();
        }

        private int TotalUnits() => _lines.Sum(l => l.QuantityToDispense);
        private decimal TotalAmount() => _lines.Sum(l => l.LineTotal);

        // ------------------------------------------------------------------
        // Wizard navigation
        // ------------------------------------------------------------------

        private void ShowStep(int step)
        {
            _step = Math.Max(1, Math.Min(StepCount, step));
            pnlStep1.Visible = _step == 1;
            pnlStep2.Visible = _step == 2;
            pnlStep3.Visible = _step == 3;

            lblStep.Text = _step switch
            {
                1 => "Step 1 of 3  -  Select a prescription",
                2 => "Step 2 of 3  -  Confirm the quantities to dispense",
                _ => "Step 3 of 3  -  Payment and confirmation"
            };

            btnBack.Visible = _step > 1;
            btnNext.Visible = _step < StepCount;
            btnConfirm.Visible = _step == StepCount;
            btnStartOver.Visible = _step > 1;
            UpdateNavButtons();
        }

        private void UpdateNavButtons()
        {
            switch (_step)
            {
                case 1:
                    btnNext.Text = "Next: Quantities  >";
                    btnNext.Enabled = _selected != null && _selected.CanDispense;
                    lblNavHint.Text = _selected == null
                        ? "Pick a prescription to start."
                        : _selected.CanDispense ? string.Empty : "This prescription cannot be dispensed.";
                    break;

                case 2:
                    btnNext.Text = "Next: Payment  >";
                    btnNext.Enabled = TotalUnits() > 0;
                    lblNavHint.Text = TotalUnits() > 0
                        ? $"Dispensing {TotalUnits()} unit(s)   -   Total {SettingsHelper.FormatMoney(TotalAmount())}"
                        : "Set a quantity of at least 1 for one medicine.";
                    break;

                default:
                    lblNavHint.Text = $"{TotalUnits()} unit(s) of {_summary.Count} medicine(s)";
                    break;
            }
        }

        private void GoBack()
        {
            if (_step > 1) ShowStep(_step - 1);
        }

        private void StartOver()
        {
            _lines = new List<DispenseLine>();
            _summary = new List<DispenseLine>();
            _validator.ClearAll();
            ShowStep(1);
            LoadPrescriptions();
            SetStatus(string.Empty, ThemeHelper.TextMuted);
        }

        private void GoNext()
        {
            if (_step == 1) GoToQuantities();
            else if (_step == 2) GoToPayment();
        }

        private void GoToQuantities()
        {
            if (_step != 1) return;
            if (_selected == null)
            {
                MessageHelper.Warning("Select a prescription first.", "Dispensing", DialogOwner());
                return;
            }
            if (!_selected.CanDispense)
            {
                MessageHelper.Warning(_selected.BlockedMessage, "Prescription cannot be dispensed", DialogOwner());
                return;
            }

            try
            {
                _lines = _repository.GetLines(_selected.PrescriptionId);   // fresh stock figures
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "Could not load the medicine lines.", DialogOwner());
                return;
            }

            lblStep2Info.Text = $"{_selected.PatientName}  -  {_selected.PrescriptionId}  -  {_selected.DoctorText}   |   Allergies: " +
                                (string.IsNullOrWhiteSpace(_selected.Allergies) ? "none recorded" : _selected.Allergies);

            _loading = true;
            dgvDispense.DataSource = null;
            dgvDispense.DataSource = _lines;
            dgvDispense.ClearSelection();
            _loading = false;

            int first = _lines.FindIndex(l => l.MaxQuantity > 0);
            if (first < 0) first = 0;
            if (_lines.Count > 0)
            {
                dgvDispense.Rows[first].Selected = true;
                dgvDispense.CurrentCell = GridHelper.FirstVisibleCell(dgvDispense.Rows[first]);
            }

            ShowStep(2);
            UpdateQuantityEditor();
        }

        private void GoToPayment()
        {
            if (_step != 2) return;
            if (TotalUnits() <= 0)
            {
                MessageHelper.Warning("Nothing to dispense. Enter a quantity of at least 1 for one medicine.", "Dispensing", DialogOwner());
                return;
            }

            _summary = _lines.Where(l => l.QuantityToDispense > 0).ToList();
            dgvSummary.DataSource = null;
            dgvSummary.DataSource = _summary;
            dgvSummary.ClearSelection();

            lblTotal.Text = "Total   " + SettingsHelper.FormatMoney(TotalAmount());
            int open = _lines.Sum(l => l.QuantityRemaining - l.QuantityToDispense);
            lblStep3Info.Text = $"{_selected?.PatientName}  -  {_selected?.PrescriptionId}   |   Medicines to hand over";
            lblStep3Note.Text = open > 0
                ? $"After this dispensing {open} unit(s) stay open on the prescription (partial dispensing); they can be dispensed later, as long as the prescription has not expired."
                : "This dispensing completes the prescription.";

            _validator.ClearAll();
            cboPayment.SelectedIndex = 0;
            ShowStep(3);
        }

        // ------------------------------------------------------------------
        // Step 3 - confirm
        // ------------------------------------------------------------------

        private void ConfirmDispense()
        {
            if (_step != 3 || _selected == null) return;

            _validator.Begin();
            string method = cboPayment.SelectedItem is LookupItem { HasValue: true } m ? m.Id : string.Empty;
            _validator.Check(method.Length > 0, cboPayment, "Choose a payment method.");

            var request = new DispenseRequest
            {
                PrescriptionId = _selected.PrescriptionId,
                PaymentMethod = method,
                Lines = _summary.Select(l => new DispenseRequestLine { PrescriptionItemId = l.PrescriptionItemId, Quantity = l.QuantityToDispense }).ToList()
            };
            foreach (string error in DispensingRepository.Validate(request))
            {
                if (method.Length > 0) _validator.AddError(null, error);
            }
            if (!_validator.IsValid)
            {
                _validator.ShowErrors(DialogOwner());
                return;
            }

            string message =
                $"Dispense {TotalUnits()} unit(s) of {_summary.Count} medicine(s) to {_selected.PatientName}?\n\n" +
                $"Prescription: {_selected.PrescriptionId}\nPayment: {method}\nTotal: {SettingsHelper.FormatMoney(TotalAmount())}\n\n" +
                "The stock will be reduced (oldest expiry first).";
            if (!MessageHelper.Confirm(message, "Confirm dispensing", DialogOwner())) return;

            string dispensingId;
            try
            {
                Cursor = Cursors.WaitCursor;
                dispensingId = _repository.Dispense(request);
            }
            catch (InvalidOperationException ex)
            {
                Cursor = Cursors.Default;
                MessageHelper.Warning(ex.Message, "Cannot dispense", DialogOwner());
                StartOver();                       // stock or prescription changed: start again with fresh figures
                return;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageHelper.Error(ex, "The medicines could not be dispensed. Nothing was saved.", DialogOwner());
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            DispensingSlip? slip = null;
            try { slip = _repository.GetSlip(dispensingId); }
            catch (Exception ex) { MessageHelper.Error(ex, "The dispensing was saved but its slip could not be loaded.", DialogOwner()); }

            string code = slip?.DispensingId ?? "the dispensing";
            StartOver();
            SetStatus($"Dispensed {code}.", ThemeHelper.Success);

            if (slip != null)
            {
                using var dialog = new DispensingSlipDialog(slip);
                dialog.ShowDialog(DialogOwner());
            }
        }

        // ------------------------------------------------------------------
        // History
        // ------------------------------------------------------------------

        private void LoadHistory()
        {
            try
            {
                _history = _repository.GetHistory(txtHistSearch.Text);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "Could not load the dispensing history.", DialogOwner());
                return;
            }

            dgvHistory.DataSource = null;
            dgvHistory.DataSource = _history;
            dgvHistory.ClearSelection();
            btnViewSlip.Enabled = false;
            lblHistCount.Text = _history.Count == 0
                ? "No dispensing records found."
                : $"{_history.Count} dispensing record(s) - total {SettingsHelper.FormatMoney(_history.Sum(h => h.Total))}";
        }

        private void ViewSelectedSlip()
        {
            if (dgvHistory.SelectedRows.Count == 0 || dgvHistory.SelectedRows[0].DataBoundItem is not DispensingSlip row) return;

            try
            {
                DispensingSlip? slip = _repository.GetSlip(row.DispensingId);
                if (slip == null)
                {
                    MessageHelper.Warning("This dispensing record no longer exists.", "Dispensing history", DialogOwner());
                    LoadHistory();
                    return;
                }
                using var dialog = new DispensingSlipDialog(slip);
                dialog.ShowDialog(DialogOwner());
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "Could not open the dispensing slip.", DialogOwner());
            }
        }
    }
}