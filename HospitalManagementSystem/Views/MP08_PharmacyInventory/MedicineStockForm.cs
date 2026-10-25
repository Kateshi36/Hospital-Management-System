using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP08_MedicineStock;
using HospitalManagementSystem.Repositories.MP08_MedicineStock;
using System.Globalization;

namespace HospitalManagementSystem.Views.MP08_MedicineStock
{
    /// <summary>
    /// MP08 (Step 12) - Medicine Catalog &amp; Stock Manager. Two views, switched with the buttons at the top:
    ///
    ///   MEDICINE CATALOG
    ///     add / edit / delete medicines (name unique, category, manufacturer, unit type, unit price above 0 with 2 decimals,
    ///     reorder level, storage instructions); the code MED-XXX is generated when a new medicine is saved.
    ///     A medicine that has stock batches or was prescribed cannot be deleted.
    ///
    ///   STOCK AND ALERTS
    ///     stock per medicine with a colour-coded status (Expired / Expiring Soon / Low Stock / Normal) and a filter for
    ///     alerts only; select a medicine to see its batches in FIFO order; Add Stock records a new batch (future expiry
    ///     date required) and Adjust Stock records a correction against a batch (reason required, never below zero).
    ///
    /// Every rule lives in MedicineStockRepository; the form shows the same rules next to the fields.
    /// All visuals live in MedicineStockForm.Designer.cs; the grid columns are created here (BuildGridColumns).
    /// </summary>
    public partial class MedicineStockForm : Form
    {
        private static readonly Color ExpiredBack = ThemeHelper.DangerSoft;
        private static readonly Color SoonBack = ThemeHelper.WarningSoft;
        private static readonly Color LowBack = ThemeHelper.ReadOnlyBack;

        private readonly MedicineStockRepository _repository = new MedicineStockRepository();
        private readonly FormValidator _validator;

        private List<MedicineRecord> _medicines = new List<MedicineRecord>();
        private List<StockSummaryRow> _summary = new List<StockSummaryRow>();
        private List<BatchRecord> _batches = new List<BatchRecord>();
        private MedicineRecord? _editing;       // null = a new medicine is being entered
        private StockSummaryRow? _selectedStock;
        private bool _loading;                  // true while the code (not the user) fills lists
        private bool _initialized;
        private bool _stockMode;

        public MedicineStockForm()
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtName, txtGeneric, cboCategory, cboManufacturer, cboUnitType, txtPrice, txtReorder, txtStorage);

            BuildGridColumns();
            GridHelper.Apply(dgvMedicines);
            GridHelper.Apply(dgvStock);
            GridHelper.Apply(dgvBatches);

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
            foreach (DataGridView grid in new[] { dgvMedicines, dgvStock, dgvBatches }) grid.AutoGenerateColumns = false;

            AddColumn(dgvMedicines, "colMCode", "Medicine ID", nameof(MedicineRecord.MedicineId), 12);
            AddColumn(dgvMedicines, "colMName", "Medicine", nameof(MedicineRecord.Name), 28);
            AddColumn(dgvMedicines, "colMGeneric", "Generic name", nameof(MedicineRecord.GenericName), 20);
            AddColumn(dgvMedicines, "colMCategory", "Category", nameof(MedicineRecord.Category), 16);
            AddColumn(dgvMedicines, "colMUnit", "Unit", nameof(MedicineRecord.UnitType), 10);
            AddColumn(dgvMedicines, "colMPrice", "Price", nameof(MedicineRecord.UnitPriceText), 12);
            AddColumn(dgvMedicines, "colMReorder", "Reorder", nameof(MedicineRecord.ReorderLevel), 10);

            AddColumn(dgvStock, "colSCode", "Medicine ID", nameof(StockSummaryRow.MedicineId), 11);
            AddColumn(dgvStock, "colSName", "Medicine", nameof(StockSummaryRow.MedicineName), 26);
            AddColumn(dgvStock, "colSStock", "Stock", nameof(StockSummaryRow.TotalStock), 9);
            AddColumn(dgvStock, "colSUsable", "Usable", nameof(StockSummaryRow.UsableStock), 9);
            AddColumn(dgvStock, "colSReorder", "Reorder", nameof(StockSummaryRow.ReorderLevel), 9);
            AddColumn(dgvStock, "colSExpiry", "Nearest expiry", nameof(StockSummaryRow.NearestExpiryText), 15);
            AddColumn(dgvStock, "colSStatus", "Status", nameof(StockSummaryRow.StatusName), 14);
            AddColumn(dgvStock, "colSAlerts", "All alerts", nameof(StockSummaryRow.AlertsText), 18);

            AddColumn(dgvBatches, "colBOrder", "Use order", nameof(BatchRecord.OrderText), 9);
            AddColumn(dgvBatches, "colBNumber", "Batch", nameof(BatchRecord.BatchNumber), 17);
            AddColumn(dgvBatches, "colBExpiry", "Expires", nameof(BatchRecord.ExpiryText), 16);
            AddColumn(dgvBatches, "colBLeft", "Left", nameof(BatchRecord.QuantityRemaining), 9);
            AddColumn(dgvBatches, "colBReceived", "Received", nameof(BatchRecord.QuantityReceived), 11);
            AddColumn(dgvBatches, "colBSupplier", "Supplier", nameof(BatchRecord.SupplierName), 20);
            AddColumn(dgvBatches, "colBStatus", "Status", nameof(BatchRecord.StatusText), 14);
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
            InputHelper.Attach(txtName, InputKind.Address, MedicineStockRules.MaxName);
            InputHelper.Attach(txtGeneric, InputKind.Address, MedicineStockRules.MaxGenericName);
            InputHelper.Attach(txtPrice, InputKind.Decimal2, 10);
            InputHelper.Attach(txtReorder, InputKind.Digits, 7);
            InputHelper.Attach(txtStorage, InputKind.Address, MedicineStockRules.MaxStorage);
            cboCategory.MaxLength = MedicineStockRules.MaxCategory;          // editable: a new value can be typed
            cboManufacturer.MaxLength = MedicineStockRules.MaxManufacturer;
            cboUnitType.MaxLength = MedicineStockRules.MaxUnitType;
            InputHelper.Attach(txtCatSearch, InputKind.Address, 60);
            InputHelper.Attach(txtStockSearch, InputKind.Address, 60);
        }

        private void WireEvents()
        {
            btnModeCatalog.Click += (s, e) => ShowMode(false);
            btnModeStock.Click += (s, e) => ShowMode(true);

            btnCatSearch.Click += (s, e) => LoadMedicines();
            btnCatAll.Click += (s, e) => { txtCatSearch.Clear(); LoadMedicines(); };
            txtCatSearch.KeyDown += (s, e) => OnEnter(e, () => LoadMedicines());
            dgvMedicines.SelectionChanged += (s, e) => OnMedicineSelected();
            btnMedNew.Click += (s, e) => NewMedicine();
            btnMedSave.Click += (s, e) => SaveMedicine();
            btnMedDelete.Click += (s, e) => DeleteMedicine();

            btnStockSearch.Click += (s, e) => LoadStock();
            btnStockAll.Click += (s, e) => { txtStockSearch.Clear(); chkAlertsOnly.Checked = false; LoadStock(); };
            txtStockSearch.KeyDown += (s, e) => OnEnter(e, () => LoadStock());
            chkAlertsOnly.CheckedChanged += (s, e) => { if (!_loading) LoadStock(); };
            dgvStock.SelectionChanged += (s, e) => OnStockSelected();
            dgvStock.CellFormatting += OnStockCellFormatting;
            dgvBatches.CellFormatting += OnBatchCellFormatting;
            btnAddStock.Click += (s, e) => AddStock();
            btnAdjustStock.Click += (s, e) => AdjustStock();

            VisibleChanged += (s, e) =>
            {
                if (Visible && _initialized) { if (_stockMode) LoadStock(); }
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
                          "  ·  Keep the medicine catalog and stock up to date.";

            _loading = true;
            try
            {
                BindLookup(cboCategory, _repository.GetCategories());
                BindLookup(cboManufacturer, _repository.GetManufacturers());
                BindLookup(cboUnitType, _repository.GetUnitTypes());
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The lists could not be loaded.", DialogOwner());
            }
            finally
            {
                _loading = false;
            }

            _initialized = true;
            ShowMode(false);
        }

        /// <summary>Editable drop-down: pick a value already in use or type a new one.</summary>
        private static void BindLookup(ComboBox box, List<LookupItem> items)
        {
            box.DataSource = items;
            box.DisplayMember = nameof(LookupItem.Name);
            box.ValueMember = nameof(LookupItem.Id);
            box.SelectedIndex = -1;
            box.Text = string.Empty;
        }

        private void ShowMode(bool stock)
        {
            _stockMode = stock;
            pnlCatalog.Visible = !stock;
            pnlStock.Visible = stock;
            ThemeHelper.StyleButton(btnModeCatalog, stock ? ButtonKind.Secondary : ButtonKind.Primary);
            ThemeHelper.StyleButton(btnModeStock, stock ? ButtonKind.Primary : ButtonKind.Secondary);

            if (!_initialized) return;
            if (stock) LoadStock();
            else LoadMedicines();
        }

        private void SetStatus(string text, Color color)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = color;
        }

        // ------------------------------------------------------------------
        // Catalog
        // ------------------------------------------------------------------

        private void LoadMedicines(string? selectId = null)
        {
            string? keep = selectId ?? _editing?.MedicineId;
            try
            {
                _medicines = _repository.GetMedicines(txtCatSearch.Text.Trim());

                _loading = true;
                dgvMedicines.DataSource = null;
                dgvMedicines.DataSource = _medicines;
                dgvMedicines.ClearSelection();
                dgvMedicines.CurrentCell = null;
                _loading = false;

                lblCatCount.Text = _medicines.Count == 0 ? "No medicine found." : $"{_medicines.Count} medicine(s)";

                bool found = false;
                if (keep != null)
                {
                    foreach (DataGridViewRow row in dgvMedicines.Rows)
                    {
                        if (row.DataBoundItem is MedicineRecord m && m.MedicineId == keep) { row.Selected = true; found = true; break; }
                    }
                }
                if (found) OnMedicineSelected(force: true);
                else if (_editing != null) NewMedicine();
            }
            catch (Exception ex)
            {
                _loading = false;
                MessageHelper.Error(ex, "The medicine catalog could not be loaded.", DialogOwner());
            }
        }

        private void OnMedicineSelected(bool force = false)
        {
            if (_loading && !force) return;
            if (dgvMedicines.SelectedRows.Count == 0 || dgvMedicines.SelectedRows[0].DataBoundItem is not MedicineRecord m) return;

            _editing = m;
            _validator.Begin();

            txtCode.Text = m.MedicineId;
            txtName.Text = m.Name;
            txtGeneric.Text = m.GenericName;
            cboCategory.Text = m.Category;
            cboManufacturer.Text = m.Manufacturer;
            cboUnitType.Text = m.UnitType;
            txtPrice.Text = m.UnitPrice.ToString("0.00", CultureInfo.InvariantCulture);
            txtReorder.Text = m.ReorderLevel.ToString(CultureInfo.InvariantCulture);
            txtStorage.Text = m.StorageInstructions;

            lblMedicineTitle.Text = "Medicine details  ·  editing " + m.MedicineId;
            btnMedSave.Text = "Update Medicine";
            btnMedDelete.Enabled = true;
            _validator.Begin();
        }

        private void NewMedicine()
        {
            _editing = null;
            _loading = true;
            dgvMedicines.ClearSelection();
            dgvMedicines.CurrentCell = null;
            _loading = false;

            _validator.Begin();
            txtCode.Clear();
            txtName.Clear();
            txtGeneric.Clear();
            foreach (ComboBox box in new[] { cboCategory, cboManufacturer, cboUnitType })
            {
                box.SelectedIndex = -1;
                box.Text = string.Empty;
            }
            txtPrice.Clear();
            txtReorder.Clear();
            txtStorage.Clear();

            lblMedicineTitle.Text = "Medicine details  ·  new medicine";
            btnMedSave.Text = "Save Medicine";
            btnMedDelete.Enabled = false;
            txtName.Focus();
        }

        private void SaveMedicine()
        {
            _validator.Begin();

            string name = txtName.Text.Trim();
            _validator.Check(name.Length > 0, txtName, "Enter the medicine name.");
            _validator.Check(cboCategory.Text.Trim().Length > 0, cboCategory, "Choose or type the category.");
            _validator.Check(cboUnitType.Text.Trim().Length > 0, cboUnitType, "Choose or type the unit type.");

            decimal price = 0;
            string priceText = txtPrice.Text.Trim();
            if (priceText.Length == 0) _validator.AddError(txtPrice, "Enter the unit price.");
            else if (!ValidationHelper.TryParseDecimal(priceText, out price)) _validator.AddError(txtPrice, "The unit price must be a number.");
            else if (price <= 0 || price > MedicineStockRules.PriceMax) _validator.AddError(txtPrice, $"The unit price must be greater than 0 and at most {MedicineStockRules.PriceMax:N0}.");
            else if (Math.Round(price, 2) != price) _validator.AddError(txtPrice, "The unit price can have at most 2 decimal places.");

            int reorder = 0;
            string reorderText = txtReorder.Text.Trim();
            if (reorderText.Length == 0) _validator.AddError(txtReorder, "Enter the reorder level (0 or more).");
            else if (!int.TryParse(reorderText, NumberStyles.None, CultureInfo.InvariantCulture, out reorder) || reorder > MedicineStockRules.ReorderMax)
                _validator.AddError(txtReorder, $"The reorder level must be a whole number from 0 to {MedicineStockRules.ReorderMax:N0}.");

            if (!_validator.IsValid)
            {
                _validator.ShowErrors(DialogOwner());
                return;
            }

            var input = new MedicineInput
            {
                MedicineId = _editing?.MedicineId ?? string.Empty,
                Name = name,
                GenericName = txtGeneric.Text.Trim(),
                Category = cboCategory.Text.Trim(),
                Manufacturer = cboManufacturer.Text.Trim(),
                UnitType = cboUnitType.Text.Trim(),
                UnitPrice = price,
                ReorderLevel = reorder,
                StorageInstructions = txtStorage.Text.Trim()
            };

            try
            {
                bool isNew = input.MedicineId.Length == 0;
                MedicineRecord saved = _repository.SaveMedicine(input);

                MessageHelper.Success(isNew ? $"{saved.Name} was added as {saved.MedicineId}." : $"{saved.Name} ({saved.MedicineId}) was updated.",
                    "Medicine saved", DialogOwner());

                txtCatSearch.Clear();
                _editing = saved;
                LoadMedicines(saved.MedicineId);
                SetStatus($"Saved {saved.MedicineId} - {saved.Name}.", ThemeHelper.Success);
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Medicine Catalog", DialogOwner());
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The medicine could not be saved.", DialogOwner());
            }
        }

        private void DeleteMedicine()
        {
            if (_editing == null) return;
            MedicineRecord medicine = _editing;

            if (!MessageHelper.ConfirmDestructive($"Delete {medicine.Name} ({medicine.MedicineId}) from the catalog?", "Delete medicine", DialogOwner())) return;

            try
            {
                _repository.DeleteMedicine(medicine.MedicineId);
                _editing = null;
                LoadMedicines();
                NewMedicine();
                SetStatus($"Deleted {medicine.MedicineId} - {medicine.Name}.", ThemeHelper.Success);
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Medicine Catalog", DialogOwner());
                LoadMedicines();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The medicine could not be deleted.", DialogOwner());
            }
        }

        // ------------------------------------------------------------------
        // Stock and alerts
        // ------------------------------------------------------------------

        private void LoadStock(string? selectId = null)
        {
            string? keep = selectId ?? _selectedStock?.MedicineId;
            try
            {
                _summary = _repository.GetStockSummary(txtStockSearch.Text.Trim(), chkAlertsOnly.Checked);

                _loading = true;
                dgvStock.DataSource = null;
                dgvStock.DataSource = _summary;
                dgvStock.ClearSelection();
                dgvStock.CurrentCell = null;
                _loading = false;

                int alerts = _summary.Count(r => r.IsAlert);
                lblStockCount.Text = _summary.Count == 0
                    ? (chkAlertsOnly.Checked ? "No medicine is in an alert state." : "No medicine found.")
                    : $"{_summary.Count} medicine(s), {alerts} in an alert state (expiring soon means within {MedicineStockRepository.ExpiringSoonDays} days)";

                _selectedStock = null;
                if (keep != null)
                {
                    foreach (DataGridViewRow row in dgvStock.Rows)
                    {
                        if (row.DataBoundItem is StockSummaryRow s && s.MedicineId == keep) { row.Selected = true; break; }
                    }
                }
                OnStockSelected(force: true);
            }
            catch (Exception ex)
            {
                _loading = false;
                MessageHelper.Error(ex, "The stock list could not be loaded.", DialogOwner());
            }
        }

        private void OnStockSelected(bool force = false)
        {
            if (_loading && !force) return;

            if (dgvStock.SelectedRows.Count == 0 || dgvStock.SelectedRows[0].DataBoundItem is not StockSummaryRow row)
            {
                _selectedStock = null;
                _batches = new List<BatchRecord>();
                dgvBatches.DataSource = null;
                lblBatchInfo.Text = "Select a medicine to see its batches";
                lblBatchInfo.ForeColor = ThemeHelper.TextMuted;
                btnAddStock.Enabled = false;
                btnAdjustStock.Enabled = false;
                return;
            }

            _selectedStock = row;
            try
            {
                _batches = _repository.GetBatches(row.MedicineId);
                dgvBatches.DataSource = null;
                dgvBatches.DataSource = _batches;
                dgvBatches.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The batches could not be loaded.", DialogOwner());
                return;
            }

            lblBatchInfo.Text = $"{row.MedicineName} ({row.MedicineId})\nStock {row.TotalStock}  |  usable {row.UsableStock}  |  reorder level {row.ReorderLevel}  |  {row.StatusName}";
            lblBatchInfo.ForeColor = ThemeHelper.TextPrimary;
            btnAddStock.Enabled = true;
            btnAdjustStock.Enabled = _batches.Count > 0;
        }

        private void OnStockCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvStock.Rows.Count) return;
            if (dgvStock.Columns[e.ColumnIndex].Name != "colSStatus") return;
            if (dgvStock.Rows[e.RowIndex].DataBoundItem is not StockSummaryRow row) return;
            Colour(e, row.StatusName);
        }

        private void OnBatchCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvBatches.Rows.Count) return;
            if (dgvBatches.Columns[e.ColumnIndex].Name != "colBStatus") return;
            if (dgvBatches.Rows[e.RowIndex].DataBoundItem is not BatchRecord batch) return;
            Colour(e, batch.StatusText);
        }

        /// <summary>Red = Expired, orange = Expiring Soon, yellow = Low Stock, green text = Normal / OK, muted = Empty.</summary>
        private static void Colour(DataGridViewCellFormattingEventArgs e, string status)
        {
            DataGridViewCellStyle style = e.CellStyle!;
            switch (status)
            {
                case "Expired":
                    style.BackColor = ExpiredBack; style.ForeColor = ThemeHelper.DangerInk;
                    style.SelectionBackColor = ExpiredBack; style.SelectionForeColor = ThemeHelper.DangerInk;
                    break;
                case "Expiring Soon":
                    style.BackColor = SoonBack; style.ForeColor = ThemeHelper.Warning;
                    style.SelectionBackColor = SoonBack; style.SelectionForeColor = ThemeHelper.Warning;
                    break;
                case "Low Stock":
                    style.BackColor = LowBack; style.ForeColor = ThemeHelper.TextSecondary;
                    style.SelectionBackColor = LowBack; style.SelectionForeColor = ThemeHelper.TextSecondary;
                    break;
                case "Empty":
                    style.ForeColor = ThemeHelper.TextMuted; style.SelectionForeColor = ThemeHelper.TextMuted;
                    break;
                default:
                    style.ForeColor = ThemeHelper.Success; style.SelectionForeColor = ThemeHelper.Success;
                    break;
            }
        }

        private void AddStock()
        {
            StockSummaryRow? row = _selectedStock;
            if (row == null) return;

            try
            {
                using var dialog = new StockAddDialog(row.MedicineName, _repository.GetSuppliers());
                if (dialog.ShowDialog(DialogOwner()) != DialogResult.OK) return;

                _repository.AddStock(new StockBatchInput
                {
                    MedicineId = row.MedicineId,
                    BatchNumber = dialog.BatchNumber,
                    Quantity = dialog.Quantity,
                    ExpiryDate = dialog.ExpiryDate,
                    Supplier = dialog.Supplier,
                    PurchasePrice = dialog.PurchasePrice
                });

                LoadStock(row.MedicineId);
                SetStatus($"Added {dialog.Quantity} x {row.MedicineName} (batch {dialog.BatchNumber}).", ThemeHelper.Success);
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Add Stock", DialogOwner());
                LoadStock(row.MedicineId);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The stock could not be added.", DialogOwner());
            }
        }

        private void AdjustStock()
        {
            StockSummaryRow? row = _selectedStock;
            if (row == null || _batches.Count == 0) return;

            try
            {
                using var dialog = new StockAdjustDialog(row.MedicineName, _batches);
                if (dialog.ShowDialog(DialogOwner()) != DialogResult.OK) return;

                int left = _repository.AdjustStock(new StockAdjustInput
                {
                    BatchId = dialog.BatchId,
                    QuantityChange = dialog.QuantityChange,
                    Reason = dialog.Reason
                });

                LoadStock(row.MedicineId);
                SetStatus($"Adjusted {row.MedicineName} by {dialog.QuantityChange:+#;-#}. The batch now has {left}.", ThemeHelper.Success);
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Adjust Stock", DialogOwner());
                LoadStock(row.MedicineId);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The stock could not be adjusted.", DialogOwner());
            }
        }
    }
}