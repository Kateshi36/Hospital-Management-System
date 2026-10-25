using System.Globalization;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP10_Laboratory;
using HospitalManagementSystem.Repositories.MP10_Laboratory;

namespace HospitalManagementSystem.Views.MP10_Laboratory
{
    /// <summary>
    /// MP10 (Step 14) - Laboratory Test Catalog &amp; Result Entry. Three views, switched with the buttons at the top:
    ///
    ///   TEST CATALOG
    ///     add / edit / delete lab tests (name unique, category, sample type, price above 0, normal range low / high, unit,
    ///     turnaround hours, "requires verification"); the code TST-XXX is generated when a new test is saved.
    ///     A test that already has lab orders cannot be deleted.
    ///
    ///   RESULT ENTRY
    ///     pick a pending lab order (STAT first); the patient and test are filled in read-only. Type the result value:
    ///     the flag under the box turns green (Normal) or red (Abnormal) while you type, before anything is saved.
    ///     Tested By is required; Verified By is required when the test requires verification; the test date and time
    ///     cannot be in the future. Save generates LR-YYYYMMDD-XXX and sets the order to Completed.
    ///
    ///   RESULTS HISTORY
    ///     every result entered, abnormal ones in red, with an "Abnormal only" filter and a detail panel.
    ///
    /// Every rule lives in LaboratoryRepository / RangeEvaluator; the form shows the same rules next to the fields.
    /// All visuals live in LaboratoryForm.Designer.cs; the grid columns are created here (BuildGridColumns).
    /// </summary>
    public partial class LaboratoryForm : Form
    {
        private static readonly Color AbnormalBack = ThemeHelper.DangerTint;
        private static readonly Color AbnormalFore = ThemeHelper.DangerInk;
        private static readonly Color AbnormalSelect = ThemeHelper.DangerSoft;
        private static readonly Color AbnormalStrong = ThemeHelper.Danger;
        private static readonly Color NormalStrong = ThemeHelper.SuccessSoft;
        private static readonly Color WarnStrong = ThemeHelper.WarningSoft;
        private static readonly Color NeutralBack = ThemeHelper.PanelLight;
        private static readonly Color UrgentBack = ThemeHelper.WarningSoft;

        private enum ViewMode { Catalog, Entry, History }

        private readonly LaboratoryRepository _repository = new LaboratoryRepository();
        private readonly FormValidator _validator;

        private List<LabTestRecord> _tests = new List<LabTestRecord>();
        private List<PendingOrder> _orders = new List<PendingOrder>();
        private List<LabResultRecord> _results = new List<LabResultRecord>();
        private LabTestRecord? _editing;          // null = a new test is being entered
        private PendingOrder? _selectedOrder;     // null = no order chosen for result entry
        private ViewMode _mode = ViewMode.Catalog;
        private bool _loading;                    // true while the code (not the user) fills lists
        private bool _initialized;

        public LaboratoryForm()
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtName, cboCategory, cboSample, txtPrice, txtLow, txtHigh, txtUnit, txtTurnaround,
                txtValue, txtFindings, dtpTested, cboTestedBy, cboVerifiedBy);

            BuildGridColumns();
            GridHelper.Apply(dgvTests);
            GridHelper.Apply(dgvOrders);
            GridHelper.Apply(dgvResults);

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
            foreach (DataGridView grid in new[] { dgvTests, dgvOrders, dgvResults }) grid.AutoGenerateColumns = false;

            AddColumn(dgvTests, "colTCode", "Test ID", nameof(LabTestRecord.TestId), 10);
            AddColumn(dgvTests, "colTName", "Test", nameof(LabTestRecord.TestName), 24);
            AddColumn(dgvTests, "colTCategory", "Category", nameof(LabTestRecord.Category), 16);
            AddColumn(dgvTests, "colTSample", "Sample", nameof(LabTestRecord.SampleType), 16);
            AddColumn(dgvTests, "colTPrice", "Price", nameof(LabTestRecord.PriceText), 10);
            AddColumn(dgvTests, "colTRange", "Normal range", nameof(LabTestRecord.RangeText), 18);
            AddColumn(dgvTests, "colTTat", "Turnaround", nameof(LabTestRecord.TurnaroundText), 11);
            AddColumn(dgvTests, "colTVerify", "Verify", nameof(LabTestRecord.VerificationText), 8);

            AddColumn(dgvOrders, "colOCode", "Order", nameof(PendingOrder.OrderId), 20);
            AddColumn(dgvOrders, "colOPatient", "Patient", nameof(PendingOrder.PatientName), 22);
            AddColumn(dgvOrders, "colOTest", "Test", nameof(PendingOrder.TestName), 24);
            AddColumn(dgvOrders, "colOUrgency", "Urgency", nameof(PendingOrder.UrgencyName), 12);
            AddColumn(dgvOrders, "colOVerify", "Verify", nameof(PendingOrder.VerifyText), 8);
            AddColumn(dgvOrders, "colOOrdered", "Ordered", nameof(PendingOrder.OrderedText), 16);

            AddColumn(dgvResults, "colRCode", "Result", nameof(LabResultRecord.ResultId), 15);
            AddColumn(dgvResults, "colRPatient", "Patient", nameof(LabResultRecord.PatientName), 15);
            AddColumn(dgvResults, "colRTest", "Test", nameof(LabResultRecord.TestName), 17);
            AddColumn(dgvResults, "colRValue", "Result", nameof(LabResultRecord.ValueText), 12);
            AddColumn(dgvResults, "colRRange", "Normal range", nameof(LabResultRecord.RangeText), 13);
            AddColumn(dgvResults, "colRFlag", "Flag", nameof(LabResultRecord.FlagText), 9);
            AddColumn(dgvResults, "colRTested", "Tested by", nameof(LabResultRecord.TestedBy), 13);
            AddColumn(dgvResults, "colRVerified", "Verified by", nameof(LabResultRecord.VerifiedText), 13);
            AddColumn(dgvResults, "colRWhen", "Tested on", nameof(LabResultRecord.TestedAtText), 16);
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
            InputHelper.Attach(txtName, InputKind.Address, LaboratoryRules.MaxTestName);
            cboCategory.MaxLength = LaboratoryRules.MaxCategory;     // editable: a new value can be typed
            cboSample.MaxLength = LaboratoryRules.MaxSampleType;
            InputHelper.Attach(txtPrice, InputKind.Decimal2, 10);
            InputHelper.Attach(txtLow, InputKind.Address, LaboratoryRules.MaxRange);
            InputHelper.Attach(txtHigh, InputKind.Address, LaboratoryRules.MaxRange);
            AttachUnitFilter(txtUnit, LaboratoryRules.MaxUnit);
            InputHelper.Attach(txtTurnaround, InputKind.Digits, 3);
            InputHelper.Attach(txtCatSearch, InputKind.Address, 60);

            InputHelper.Attach(txtOrderSearch, InputKind.Address, 60);
            InputHelper.Attach(txtValue, InputKind.Address, LaboratoryRules.MaxResultValue);
            InputHelper.Attach(txtFindings, InputKind.Address, LaboratoryRules.MaxFindings);

            InputHelper.Attach(txtHistSearch, InputKind.Address, 60);
        }

        /// <summary>
        /// A unit such as g/dL, mmol/L or x10^3/uL: letters, digits, space and / ^ . - * only
        /// (InputKind.Address would remove the ^ sign). Typing and pasting are both filtered.
        /// </summary>
        private static void AttachUnitFilter(TextBox box, int maxLength)
        {
            box.MaxLength = maxLength;
            box.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !IsUnitChar(e.KeyChar)) e.Handled = true;
            };
            box.TextChanged += (s, e) =>
            {
                string cleaned = new string(box.Text.Where(IsUnitChar).ToArray());
                if (cleaned == box.Text) return;
                int caret = box.SelectionStart;
                box.Text = cleaned;
                box.SelectionStart = Math.Min(cleaned.Length, caret);
            };
        }

        private static bool IsUnitChar(char c) => char.IsLetterOrDigit(c) || "/^.-* ".IndexOf(c) >= 0;

        private void WireEvents()
        {
            btnModeCatalog.Click += (s, e) => ShowMode(ViewMode.Catalog);
            btnModeEntry.Click += (s, e) => ShowMode(ViewMode.Entry);
            btnModeHistory.Click += (s, e) => ShowMode(ViewMode.History);

            // catalog
            btnCatSearch.Click += (s, e) => LoadTests();
            btnCatAll.Click += (s, e) => { txtCatSearch.Clear(); LoadTests(); };
            txtCatSearch.KeyDown += (s, e) => OnEnter(e, () => LoadTests());
            dgvTests.SelectionChanged += (s, e) => OnTestSelected();
            btnTestNew.Click += (s, e) => NewTest();
            btnTestSave.Click += (s, e) => SaveTest();
            btnTestDelete.Click += (s, e) => DeleteTest();

            // result entry
            btnOrderSearch.Click += (s, e) => LoadOrders();
            btnOrderAll.Click += (s, e) => { txtOrderSearch.Clear(); LoadOrders(); };
            txtOrderSearch.KeyDown += (s, e) => OnEnter(e, () => LoadOrders());
            dgvOrders.SelectionChanged += (s, e) => OnOrderSelected();
            dgvOrders.CellFormatting += OnOrderCellFormatting;
            txtValue.TextChanged += (s, e) => UpdateFlag();
            btnEntryClear.Click += (s, e) => ResetEntryFields();
            btnEntrySave.Click += (s, e) => SaveResult();

            // history
            btnHistSearch.Click += (s, e) => LoadResults();
            btnHistAll.Click += (s, e) => { txtHistSearch.Clear(); chkAbnormalOnly.Checked = false; LoadResults(); };
            txtHistSearch.KeyDown += (s, e) => OnEnter(e, () => LoadResults());
            chkAbnormalOnly.CheckedChanged += (s, e) => { if (!_loading) LoadResults(); };
            dgvResults.SelectionChanged += (s, e) => OnResultSelected();
            dgvResults.CellFormatting += OnResultCellFormatting;

            VisibleChanged += (s, e) =>
            {
                if (!Visible || !_initialized) return;
                if (_mode == ViewMode.Entry) LoadOrders();
                else if (_mode == ViewMode.History) LoadResults();
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
                          "  ·  Manage the test catalog, enter results and look up past results.";

            _loading = true;
            try
            {
                BindEditable(cboCategory, _repository.GetCategories());
                BindEditable(cboSample, _repository.GetSampleTypes());

                List<LookupItem> staff = _repository.GetStaff();
                BindLookup(cboTestedBy, staff, "Select who tested the sample...");
                BindLookup(cboVerifiedBy, staff, "(none)");
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The lists could not be loaded.", DialogOwner());
            }
            finally
            {
                _loading = false;
            }

            ShowNoOrder();
            _initialized = true;

            // A lab technician mostly enters results, so that view opens first.
            ShowMode(SessionManager.HasRole(SessionManager.RoleLabTechnician) ? ViewMode.Entry : ViewMode.Catalog);
        }

        private static void BindLookup(ComboBox box, List<LookupItem> items, string placeholder)
        {
            var list = new List<LookupItem> { LookupItem.Placeholder(placeholder) };
            list.AddRange(items);
            box.DataSource = list;
            box.DisplayMember = nameof(LookupItem.Name);
            box.ValueMember = nameof(LookupItem.Id);
            box.SelectedIndex = 0;
        }

        /// <summary>Editable drop-down: pick a value already in use or type a new one.</summary>
        private static void BindEditable(ComboBox box, List<LookupItem> items)
        {
            box.DataSource = items;
            box.DisplayMember = nameof(LookupItem.Name);
            box.ValueMember = nameof(LookupItem.Id);
            box.SelectedIndex = -1;
            box.Text = string.Empty;
        }

        /// <summary>The user_id of the chosen staff member, or 0 for the placeholder.</summary>
        private static int SelectedId(ComboBox box) =>
            box.SelectedItem is LookupItem { HasValue: true } item ? int.Parse(item.Id, CultureInfo.InvariantCulture) : 0;

        private void ShowMode(ViewMode mode)
        {
            _mode = mode;
            pnlCatalog.Visible = mode == ViewMode.Catalog;
            pnlEntry.Visible = mode == ViewMode.Entry;
            pnlHistory.Visible = mode == ViewMode.History;
            ThemeHelper.StyleButton(btnModeCatalog, mode == ViewMode.Catalog ? ButtonKind.Primary : ButtonKind.Secondary);
            ThemeHelper.StyleButton(btnModeEntry, mode == ViewMode.Entry ? ButtonKind.Primary : ButtonKind.Secondary);
            ThemeHelper.StyleButton(btnModeHistory, mode == ViewMode.History ? ButtonKind.Primary : ButtonKind.Secondary);

            if (!_initialized) return;
            switch (mode)
            {
                case ViewMode.Catalog: LoadTests(); break;
                case ViewMode.Entry: LoadOrders(); break;
                default: LoadResults(); break;
            }
        }

        private void SetStatus(string text, Color color)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = color;
        }

        // ------------------------------------------------------------------
        // Catalog
        // ------------------------------------------------------------------

        private void LoadTests(string? selectId = null)
        {
            string? keep = selectId ?? _editing?.TestId;
            try
            {
                _tests = _repository.GetTests(txtCatSearch.Text.Trim());

                _loading = true;
                dgvTests.DataSource = null;
                dgvTests.DataSource = _tests;
                dgvTests.ClearSelection();
                dgvTests.CurrentCell = null;
                _loading = false;

                lblCatCount.Text = _tests.Count == 0 ? "No test found." : $"{_tests.Count} test(s)";

                bool found = false;
                if (keep != null)
                {
                    foreach (DataGridViewRow row in dgvTests.Rows)
                    {
                        if (row.DataBoundItem is LabTestRecord t && t.TestId == keep) { row.Selected = true; found = true; break; }
                    }
                }
                if (found) OnTestSelected(force: true);
                else if (_editing != null) NewTest();
            }
            catch (Exception ex)
            {
                _loading = false;
                MessageHelper.Error(ex, "The test catalog could not be loaded.", DialogOwner());
            }
        }

        private void OnTestSelected(bool force = false)
        {
            if (_loading && !force) return;
            if (dgvTests.SelectedRows.Count == 0 || dgvTests.SelectedRows[0].DataBoundItem is not LabTestRecord t) return;

            _editing = t;
            _validator.Begin();

            txtCode.Text = t.TestId;
            txtName.Text = t.TestName;
            cboCategory.Text = t.Category;
            cboSample.Text = t.SampleType;
            txtPrice.Text = t.Price.ToString("0.00", CultureInfo.InvariantCulture);
            txtLow.Text = t.NormalRangeLow;
            txtHigh.Text = t.NormalRangeHigh;
            txtUnit.Text = t.Unit;
            txtTurnaround.Text = t.TurnaroundHours.ToString(CultureInfo.InvariantCulture);
            chkVerify.Checked = t.RequiresVerification;

            lblTestTitle.Text = "Test details  ·  editing " + t.TestId;
            btnTestSave.Text = "Update Test";
            btnTestDelete.Enabled = true;
            _validator.Begin();
        }

        private void NewTest()
        {
            _editing = null;
            _loading = true;
            dgvTests.ClearSelection();
            dgvTests.CurrentCell = null;
            _loading = false;

            _validator.Begin();
            txtCode.Clear();
            txtName.Clear();
            foreach (ComboBox box in new[] { cboCategory, cboSample })
            {
                box.SelectedIndex = -1;
                box.Text = string.Empty;
            }
            txtPrice.Clear();
            txtLow.Clear();
            txtHigh.Clear();
            txtUnit.Clear();
            txtTurnaround.Clear();
            chkVerify.Checked = false;

            lblTestTitle.Text = "Test details  ·  new test";
            btnTestSave.Text = "Save Test";
            btnTestDelete.Enabled = false;
            txtName.Focus();
        }

        private void SaveTest()
        {
            _validator.Begin();

            string name = txtName.Text.Trim();
            _validator.Check(name.Length > 0, txtName, "Enter the test name.");
            _validator.Check(cboCategory.Text.Trim().Length > 0, cboCategory, "Choose or type the category.");
            _validator.Check(cboSample.Text.Trim().Length > 0, cboSample, "Choose or type the sample type.");

            decimal price = 0;
            string priceText = txtPrice.Text.Trim();
            if (priceText.Length == 0) _validator.AddError(txtPrice, "Enter the price.");
            else if (!ValidationHelper.TryParseDecimal(priceText, out price)) _validator.AddError(txtPrice, "The price must be a number.");
            else if (price <= 0 || price > LaboratoryRules.PriceMax) _validator.AddError(txtPrice, $"The price must be greater than 0 and at most {LaboratoryRules.PriceMax:N0}.");
            else if (Math.Round(price, 2) != price) _validator.AddError(txtPrice, "The price can have at most 2 decimal places.");

            string low = txtLow.Text.Trim();
            string high = txtHigh.Text.Trim();
            if (low.Length == 0) _validator.AddError(txtLow, "Enter the low normal range (a number such as 12.0, or a word such as Negative).");
            if (high.Length == 0) _validator.AddError(txtHigh, "Enter the high normal range (a number such as 17.5, or the same word such as Negative).");
            if (low.Length > 0 && high.Length > 0)
            {
                bool lowNumeric = RangeEvaluator.TryParseNumber(low, out decimal lowNumber);
                bool highNumeric = RangeEvaluator.TryParseNumber(high, out decimal highNumber);
                if (lowNumeric != highNumeric)
                    _validator.AddError(txtHigh, "The normal range must be two numbers (for example 12.0 and 17.5) or two words (for example Negative and Negative), not one of each.");
                else if (lowNumeric && lowNumber > highNumber)
                    _validator.AddError(txtLow, "The low normal range cannot be greater than the high normal range.");
            }

            int hours = 0;
            string hoursText = txtTurnaround.Text.Trim();
            if (hoursText.Length == 0) _validator.AddError(txtTurnaround, "Enter the turnaround time in hours.");
            else if (!int.TryParse(hoursText, NumberStyles.None, CultureInfo.InvariantCulture, out hours) || hours < 1 || hours > LaboratoryRules.TurnaroundMax)
                _validator.AddError(txtTurnaround, $"The turnaround time must be a whole number of hours from 1 to {LaboratoryRules.TurnaroundMax}.");

            if (!_validator.IsValid)
            {
                _validator.ShowErrors(DialogOwner());
                return;
            }

            var input = new LabTestInput
            {
                TestId = _editing?.TestId ?? string.Empty,
                TestName = name,
                Category = cboCategory.Text.Trim(),
                SampleType = cboSample.Text.Trim(),
                Price = price,
                NormalRangeLow = low,
                NormalRangeHigh = high,
                Unit = txtUnit.Text.Trim(),
                TurnaroundHours = hours,
                RequiresVerification = chkVerify.Checked
            };

            try
            {
                bool isNew = input.TestId.Length == 0;
                LabTestRecord saved = _repository.SaveTest(input);

                MessageHelper.Success(isNew ? $"{saved.TestName} was added as {saved.TestId}." : $"{saved.TestName} ({saved.TestId}) was updated.",
                    "Test saved", DialogOwner());

                txtCatSearch.Clear();
                _editing = saved;
                LoadTests(saved.TestId);
                SetStatus($"Saved {saved.TestId} - {saved.TestName}.", ThemeHelper.Success);
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Test Catalog", DialogOwner());
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The test could not be saved.", DialogOwner());
            }
        }

        private void DeleteTest()
        {
            if (_editing == null) return;
            LabTestRecord test = _editing;

            if (!MessageHelper.ConfirmDestructive($"Delete {test.TestName} ({test.TestId}) from the catalog?", "Delete test", DialogOwner())) return;

            try
            {
                _repository.DeleteTest(test.TestId);
                _editing = null;
                LoadTests();
                NewTest();
                SetStatus($"Deleted {test.TestId} - {test.TestName}.", ThemeHelper.Success);
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Test Catalog", DialogOwner());
                LoadTests();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The test could not be deleted.", DialogOwner());
            }
        }

        // ------------------------------------------------------------------
        // Result entry
        // ------------------------------------------------------------------

        private void LoadOrders()
        {
            string? keep = _selectedOrder?.OrderId;
            try
            {
                _orders = _repository.GetPendingOrders(txtOrderSearch.Text.Trim());

                _loading = true;
                dgvOrders.DataSource = null;
                dgvOrders.DataSource = _orders;
                dgvOrders.ClearSelection();
                dgvOrders.CurrentCell = null;

                bool found = false;
                if (keep != null)
                {
                    foreach (DataGridViewRow row in dgvOrders.Rows)
                    {
                        if (row.DataBoundItem is PendingOrder o && o.OrderId == keep) { row.Selected = true; found = true; break; }
                    }
                }
                _loading = false;

                int stat = _orders.Count(o => o.UrgencyName == "STAT");
                lblOrderCount.Text = _orders.Count == 0
                    ? "No pending lab order."
                    : $"{_orders.Count} pending order(s), {stat} STAT";

                // An order that was just completed (or by another user) is no longer in the list.
                if (!found) ShowNoOrder();
            }
            catch (Exception ex)
            {
                _loading = false;
                MessageHelper.Error(ex, "The pending orders could not be loaded.", DialogOwner());
            }
        }

        private void OnOrderSelected()
        {
            if (_loading) return;
            if (dgvOrders.SelectedRows.Count == 0 || dgvOrders.SelectedRows[0].DataBoundItem is not PendingOrder order) return;
            if (_selectedOrder != null && _selectedOrder.OrderId == order.OrderId) return;   // same order: keep what was typed

            _selectedOrder = order;
            txtOrderCode.Text = $"{order.OrderId}  ({order.UrgencyName})";
            txtPatient.Text = $"{order.PatientName} ({order.PatientId})";
            txtTest.Text = $"{order.TestName} ({order.TestId}) - {order.SampleTypeName}";
            txtRange.Text = order.RangeText;
            lblEntryTitle.Text = "Result entry  ·  " + order.OrderId;
            lblVerifiedBy.Text = order.RequiresVerification ? "Verified by *" : "Verified by (optional)";

            SetEntryEnabled(true);
            ResetEntryFields();
            txtValue.Focus();
        }

        private void ShowNoOrder()
        {
            _selectedOrder = null;
            _validator.Begin();
            txtOrderCode.Clear();
            txtPatient.Clear();
            txtTest.Clear();
            txtRange.Clear();
            txtValue.Clear();
            txtFindings.Clear();
            if (cboTestedBy.Items.Count > 0) cboTestedBy.SelectedIndex = 0;
            if (cboVerifiedBy.Items.Count > 0) cboVerifiedBy.SelectedIndex = 0;
            lblEntryTitle.Text = "Result entry";
            lblVerifiedBy.Text = "Verified by (optional)";
            SetEntryEnabled(false);
            UpdateFlag();
        }

        private void SetEntryEnabled(bool enabled)
        {
            txtValue.Enabled = enabled;
            dtpTested.Enabled = enabled;
            cboTestedBy.Enabled = enabled;
            cboVerifiedBy.Enabled = enabled;
            txtFindings.Enabled = enabled;
            btnEntryClear.Enabled = enabled;
            btnEntrySave.Enabled = enabled;
        }

        /// <summary>Empties the entry fields for the selected order: time = now, Tested By = the logged-in user.</summary>
        private void ResetEntryFields()
        {
            if (_selectedOrder == null) return;

            _validator.Begin();
            txtValue.Clear();
            txtFindings.Clear();
            dtpTested.Value = DateTime.Now;
            cboTestedBy.SelectedValue = SessionManager.CurrentUserId.ToString(CultureInfo.InvariantCulture);
            if (cboTestedBy.SelectedIndex < 0) cboTestedBy.SelectedIndex = 0;
            cboVerifiedBy.SelectedIndex = 0;
            UpdateFlag();
        }

        /// <summary>The live flag: green = Normal, red = Abnormal, amber = the value must be a number, grey = nothing to compare yet.</summary>
        private void UpdateFlag()
        {
            if (_selectedOrder == null)
            {
                SetFlag("Select a pending order to begin", NeutralBack, ThemeHelper.TextMuted);
                return;
            }

            RangeCheck check = RangeEvaluator.Evaluate(txtValue.Text, _selectedOrder.NormalRangeLow, _selectedOrder.NormalRangeHigh);
            switch (check.State)
            {
                case RangeState.Normal:
                    SetFlag("\u25CF  " + check.Message, NormalStrong, ThemeHelper.SuccessInk);
                    break;
                case RangeState.Low:
                case RangeState.High:
                case RangeState.Different:
                    SetFlag("\u25B2  " + check.Message, AbnormalStrong, Color.White);
                    break;
                case RangeState.NotNumeric:
                    SetFlag(check.Message, WarnStrong, ThemeHelper.Warning);
                    break;
                default:
                    SetFlag(check.Message, NeutralBack, ThemeHelper.TextMuted);
                    break;
            }
        }

        private void SetFlag(string text, Color back, Color fore)
        {
            lblFlag.Text = text;
            lblFlag.BackColor = back;
            lblFlag.ForeColor = fore;
        }

        private void SaveResult()
        {
            if (_selectedOrder == null) return;
            PendingOrder order = _selectedOrder;

            _validator.Begin();

            string value = txtValue.Text.Trim();
            if (value.Length == 0)
                _validator.AddError(txtValue, "Enter the result value.");
            else if (RangeEvaluator.IsNumericRange(order.NormalRangeLow, order.NormalRangeHigh) && !RangeEvaluator.TryParseNumber(value, out _))
                _validator.AddError(txtValue, $"{order.TestName} has a numeric normal range ({order.RangeText}), so the result value must be a number (for example 13.5).");

            DateTime testedAt = dtpTested.Value;
            _validator.Check(testedAt <= DateTime.Now, dtpTested, "The test date and time cannot be in the future.");

            int testedBy = SelectedId(cboTestedBy);
            _validator.Check(testedBy > 0, cboTestedBy, "Choose who tested the sample (Tested By).");

            int verifiedBy = SelectedId(cboVerifiedBy);
            if (order.RequiresVerification && verifiedBy <= 0)
                _validator.AddError(cboVerifiedBy, "This test requires verification. Choose who verified the result (Verified By) before it can be marked Completed.");
            else if (verifiedBy > 0 && verifiedBy == testedBy)
                _validator.AddError(cboVerifiedBy, "The verifier must be a different person from the one who tested the sample.");

            if (!_validator.IsValid)
            {
                _validator.ShowErrors(DialogOwner());
                return;
            }

            var input = new LabResultInput
            {
                OrderId = order.OrderId,
                ResultValue = value,
                Findings = txtFindings.Text.Trim(),
                TestedByUserId = testedBy,
                VerifiedByUserId = verifiedBy > 0 ? verifiedBy : null,
                TestDateTime = testedAt
            };

            try
            {
                LabResultSaved saved = _repository.SaveResult(input);

                string text = $"Result {saved.ResultId} was saved for order {saved.OrderId} ({saved.TestName}).\n" +
                              $"The result is flagged {(saved.IsAbnormal ? "ABNORMAL" : "NORMAL")}. The order is now Completed.";
                if (saved.IsAbnormal) MessageHelper.Warning(text, "Result saved - abnormal", DialogOwner());
                else MessageHelper.Success(text, "Result saved", DialogOwner());

                SetStatus($"Saved {saved.ResultId} ({(saved.IsAbnormal ? "abnormal" : "normal")}).", saved.IsAbnormal ? ThemeHelper.Danger : ThemeHelper.Success);

                _selectedOrder = null;
                LoadOrders();
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Result Entry", DialogOwner());
                _selectedOrder = null;
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The result could not be saved.", DialogOwner());
            }
        }

        private void OnOrderCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvOrders.Rows.Count) return;
            if (dgvOrders.Columns[e.ColumnIndex].Name != "colOUrgency") return;
            if (dgvOrders.Rows[e.RowIndex].DataBoundItem is not PendingOrder order) return;

            DataGridViewCellStyle style = e.CellStyle!;
            Color? back = order.UrgencyName == "STAT" ? AbnormalStrong : order.UrgencyName == "Urgent" ? UrgentBack : (Color?)null;
            if (back.HasValue)
            {
                Color fore = order.UrgencyName == "STAT" ? Color.White : ThemeHelper.Warning;
                style.BackColor = back.Value; style.ForeColor = fore;
                style.SelectionBackColor = back.Value; style.SelectionForeColor = fore;
            }
        }

        // ------------------------------------------------------------------
        // Results history
        // ------------------------------------------------------------------

        private void LoadResults()
        {
            string? keep = (dgvResults.SelectedRows.Count > 0 && dgvResults.SelectedRows[0].DataBoundItem is LabResultRecord current) ? current.ResultId : null;
            try
            {
                _results = _repository.GetResults(txtHistSearch.Text.Trim(), chkAbnormalOnly.Checked);

                _loading = true;
                dgvResults.DataSource = null;
                dgvResults.DataSource = _results;
                dgvResults.ClearSelection();
                dgvResults.CurrentCell = null;
                _loading = false;

                int abnormal = _results.Count(r => r.IsAbnormal);
                lblHistCount.Text = _results.Count == 0
                    ? (chkAbnormalOnly.Checked ? "No abnormal result found." : "No result found.")
                    : $"{_results.Count} result(s), {abnormal} abnormal";

                if (keep != null)
                {
                    foreach (DataGridViewRow row in dgvResults.Rows)
                    {
                        if (row.DataBoundItem is LabResultRecord r && r.ResultId == keep) { row.Selected = true; break; }
                    }
                }
                OnResultSelected(force: true);
            }
            catch (Exception ex)
            {
                _loading = false;
                MessageHelper.Error(ex, "The results could not be loaded.", DialogOwner());
            }
        }

        private void OnResultSelected(bool force = false)
        {
            if (_loading && !force) return;

            if (dgvResults.SelectedRows.Count == 0 || dgvResults.SelectedRows[0].DataBoundItem is not LabResultRecord r)
            {
                txtDetail.Text = "Select a result to see its details.";
                txtDetail.ForeColor = ThemeHelper.TextMuted;
                return;
            }

            string findings = string.IsNullOrWhiteSpace(r.Findings) ? "-" : r.Findings;
            txtDetail.Text =
                $"{r.ResultId}   |   Order {r.OrderId}   |   {r.TestedAtText}\r\n" +
                $"Patient: {r.PatientName} ({r.PatientId})\r\n" +
                $"Test: {r.TestName}\r\n" +
                $"Result: {r.ValueText}   (normal range {r.RangeText})   =>   {r.FlagText.ToUpperInvariant()}\r\n" +
                $"Tested by: {r.TestedBy}   |   Verified by: {r.VerifiedText}\r\n" +
                $"Findings: {findings}";
            txtDetail.ForeColor = r.IsAbnormal ? AbnormalFore : ThemeHelper.TextPrimary;
        }

        /// <summary>Abnormal results: red text on a dark red row and a solid red Flag cell. Normal results keep the normal look.</summary>
        private void OnResultCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvResults.Rows.Count) return;
            if (dgvResults.Rows[e.RowIndex].DataBoundItem is not LabResultRecord r) return;

            DataGridViewCellStyle style = e.CellStyle!;
            bool flagColumn = dgvResults.Columns[e.ColumnIndex].Name == "colRFlag";

            if (r.IsAbnormal)
            {
                if (flagColumn)
                {
                    style.BackColor = AbnormalStrong; style.ForeColor = Color.White;
                    style.SelectionBackColor = AbnormalStrong; style.SelectionForeColor = Color.White;
                }
                else
                {
                    style.BackColor = AbnormalBack; style.ForeColor = AbnormalFore;
                    style.SelectionBackColor = AbnormalSelect; style.SelectionForeColor = AbnormalFore;
                }
            }
            else if (flagColumn)
            {
                style.ForeColor = ThemeHelper.Success;
                style.SelectionForeColor = ThemeHelper.Success;
            }
        }
    }
}