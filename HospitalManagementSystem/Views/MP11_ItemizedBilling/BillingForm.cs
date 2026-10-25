using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP11_Billing;
using HospitalManagementSystem.Repositories.MP11_Billing;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace HospitalManagementSystem.Views.MP11_Billing
{
    /// <summary>
    /// MP11 (Step 15) - Itemized Billing Calculator. Two views, switched with the buttons at the top:
    ///
    ///   BILL BUILDER
    ///     choose the patient, add charges from the price lists (Consultation, Medicine, Lab Test, Room = room type x days x rate,
    ///     Procedure, Miscellaneous - or "Other" with your own description and price), and watch the summary change live:
    ///         Subtotal  ->  Discount (flat or percent)  ->  Tax on the amount AFTER the discount  ->  Grand total.
    ///     A discount above the configured threshold (more than 20 % of the subtotal or more than 5,000.00) makes the
    ///     "Authorized by" prompt appear; the bill cannot be finalized without a name.
    ///     Save Draft keeps the bill editable. Finalize Bill asks for confirmation, generates BILL-YYYYMMDD-XXX and LOCKS the bill:
    ///     every add / edit / remove control is disabled, and the repository refuses changes to a finalized bill as well.
    ///
    ///   ALL BILLS
    ///     every bill with its totals and payment status; open one (a Draft can be edited, a Finalized bill is view-only)
    ///     or delete a Draft. Finalized bills can never be deleted.
    ///
    /// The rules live in BillingRepository.Validate and BillCalculator; this form shows the same rules next to the fields.
    /// All visuals live in BillingForm.Designer.cs; the grid columns are created here (BuildGridColumns).
    /// </summary>
    public partial class BillingForm : Form
    {
        private static readonly Color DraftBack = ThemeHelper.WarningSoft;
        private static readonly Color DraftFore = ThemeHelper.Warning;
        private static readonly Color FinalBack = ThemeHelper.SuccessSoft;
        private static readonly Color ErrorFore = ThemeHelper.Danger;

        private enum ViewMode { Builder, List }

        private readonly BillingRepository _repository = new BillingRepository();
        private readonly FormValidator _validator;

        private readonly List<BillItem> _items = new List<BillItem>();
        private List<PriceListItem> _entries = new List<PriceListItem>();   // what the Item drop-down shows
        private List<BillListRow> _rows = new List<BillListRow>();
        private Bill _bill = new Bill();             // header of the bill on the screen (id, status, code ...)
        private string? _listDescription;            // description of the price-list entry that is picked (not changed by the input filter)
        private int _editingIndex = -1;              // index in _items of the item loaded in the editor; -1 = adding a new one
        private ViewMode _mode = ViewMode.Builder;
        private bool _dirty;                         // the bill on the screen has changes that are not saved
        private bool _loading;                       // true while the code (not the user) fills the controls
        private bool _initialized;

        public BillingForm()
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(cboPatient, cboCategory, cboItem, txtDescription, txtQuantity, txtUnitPrice,
                txtDiscount, txtTaxRate, txtAuthorizedBy);

            BuildGridColumns();
            GridHelper.Apply(dgvItems);
            GridHelper.Apply(dgvBills);
            GridHelper.EnableStatusColoring(dgvBills, "colLStatus");
            GridHelper.EnableStatusColoring(dgvBills, "colLPayment");

            ApplyInputRules();
            WireEvents();
            Load += (s, e) => InitializeScreen();
        }

        private IWin32Window DialogOwner() => (IWin32Window?)TopLevelControl ?? this;

        private static string Money(decimal amount) => SettingsHelper.FormatMoney(amount);

        // ------------------------------------------------------------------
        // Set-up
        // ------------------------------------------------------------------

        /// <summary>
        /// Creates the columns of the two grids. They are built here, not in the Designer, because Visual Studio's
        /// [Design] view deletes designer-made grid columns when it re-saves a large designer file.
        /// </summary>
        private void BuildGridColumns()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvBills.AutoGenerateColumns = false;

            AddColumn(dgvItems, "colICategory", "Category", nameof(BillItem.Category), 17);
            AddColumn(dgvItems, "colIDescription", "Description", nameof(BillItem.Description), 38);
            AddColumn(dgvItems, "colIQuantity", "Qty / days", nameof(BillItem.QuantityText), 11, true);
            AddColumn(dgvItems, "colIPrice", "Unit price", nameof(BillItem.UnitPriceText), 15, true);
            AddColumn(dgvItems, "colIAmount", "Amount", nameof(BillItem.AmountText), 17, true);

            AddColumn(dgvBills, "colLCode", "Bill ID", nameof(BillListRow.BillId), 15);
            AddColumn(dgvBills, "colLPatient", "Patient", nameof(BillListRow.PatientName), 16);
            AddColumn(dgvBills, "colLItems", "Items", nameof(BillListRow.ItemCountText), 6, true);
            AddColumn(dgvBills, "colLSubtotal", "Subtotal", nameof(BillListRow.SubtotalText), 10, true);
            AddColumn(dgvBills, "colLDiscount", "Discount", nameof(BillListRow.DiscountText), 9, true);
            AddColumn(dgvBills, "colLTax", "Tax", nameof(BillListRow.TaxText), 8, true);
            AddColumn(dgvBills, "colLTotal", "Grand total", nameof(BillListRow.GrandTotalText), 11, true);
            AddColumn(dgvBills, "colLStatus", "Status", nameof(BillListRow.Status), 9);
            AddColumn(dgvBills, "colLPayment", "Payment", nameof(BillListRow.PaymentStatus), 11);
            AddColumn(dgvBills, "colLCreated", "Created", nameof(BillListRow.CreatedText), 15);
        }

        private static void AddColumn(DataGridView grid, string name, string header, string property, float weight, bool right = false)
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
            if (right)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            grid.Columns.Add(column);
        }

        private void ApplyInputRules()
        {
            InputHelper.Attach(txtPatientSearch, InputKind.Address, 60);
            InputHelper.Attach(txtDescription, InputKind.Address, BillingRules.MaxDescription);
            InputHelper.Attach(txtQuantity, InputKind.Digits, 3);
            InputHelper.Attach(txtUnitPrice, InputKind.Decimal2, 9);
            InputHelper.Attach(txtDiscount, InputKind.Decimal2, 11);
            InputHelper.Attach(txtTaxRate, InputKind.Decimal2, 6);
            InputHelper.Attach(txtAuthorizedBy, InputKind.PersonName, BillingRules.MaxAuthorizedBy);
            InputHelper.Attach(txtListSearch, InputKind.Address, 60);
        }

        private void WireEvents()
        {
            btnModeBuilder.Click += (s, e) => ShowMode(ViewMode.Builder);
            btnModeList.Click += (s, e) => ShowMode(ViewMode.List);

            // bill + patient
            btnPatientFind.Click += (s, e) => FindPatients();
            txtPatientSearch.KeyDown += (s, e) => OnEnter(e, () => FindPatients());

            // add a charge
            cboCategory.SelectedIndexChanged += (s, e) => OnCategoryChanged();
            cboItem.SelectedIndexChanged += (s, e) => OnItemPicked();
            txtQuantity.TextChanged += (s, e) => UpdateLineAmount();
            txtUnitPrice.TextChanged += (s, e) => UpdateLineAmount();
            btnAddItem.Click += (s, e) => AddOrUpdateItem();
            btnRemoveItem.Click += (s, e) => RemoveItem();
            btnClearItem.Click += (s, e) => ResetItemEditor(clearGridSelection: true);
            dgvItems.SelectionChanged += (s, e) => OnItemSelected();

            // summary
            cboDiscountType.SelectedIndexChanged += (s, e) => OnDiscountTypeChanged();
            txtDiscount.TextChanged += (s, e) => OnSummaryInputChanged();
            txtTaxRate.TextChanged += (s, e) => OnSummaryInputChanged();
            txtAuthorizedBy.TextChanged += (s, e) => OnSummaryInputChanged();
            btnSaveDraft.Click += (s, e) => Persist(finalize: false);
            btnFinalize.Click += (s, e) => Persist(finalize: true);
            btnNewBill.Click += (s, e) => NewBillClicked();

            // all bills
            btnListSearch.Click += (s, e) => LoadBills();
            btnListAll.Click += (s, e) => { _loading = true; txtListSearch.Clear(); cboListStatus.SelectedIndex = 0; _loading = false; LoadBills(); };
            txtListSearch.KeyDown += (s, e) => OnEnter(e, () => LoadBills());
            cboListStatus.SelectedIndexChanged += (s, e) => { if (!_loading && _initialized) LoadBills(); };
            dgvBills.SelectionChanged += (s, e) => UpdateListButtons();
            dgvBills.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) OpenSelectedBill(); };
            btnOpenBill.Click += (s, e) => OpenSelectedBill();
            btnDeleteDraft.Click += (s, e) => DeleteSelectedDraft();

            VisibleChanged += (s, e) =>
            {
                if (Visible && _initialized && _mode == ViewMode.List) LoadBills();
            };
        }

        private static void OnEnter(KeyEventArgs e, Action action)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            e.Handled = true;
            action();
        }

        private void InitializeScreen()
        {
            lblDate.Text = DateTime.Today.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture) +
                          "  ·  Build a patient's bill item by item, then save it as a draft or finalize it.";

            _loading = true;
            try
            {
                BindLookup(cboCategory, _repository.GetCategories(), "Select the category...");
                BindLookup(cboDiscountType, _repository.GetDiscountTypes(), null);
                BindLookup(cboListStatus, new List<LookupItem>
                {
                    LookupItem.Placeholder("All bills"),
                    LookupItem.Of(BillingRules.StatusDraft),
                    LookupItem.Of(BillingRules.StatusFinalized)
                }, null);
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
            StartNewBill();
            ShowMode(ViewMode.Builder);
        }

        private static void BindLookup(ComboBox box, List<LookupItem> items, string? placeholder)
        {
            var list = new List<LookupItem>();
            if (placeholder != null) list.Add(LookupItem.Placeholder(placeholder));
            list.AddRange(items);
            box.DataSource = list;
            box.DisplayMember = nameof(LookupItem.Name);
            box.ValueMember = nameof(LookupItem.Id);
            if (list.Count > 0) box.SelectedIndex = 0;
        }

        /// <summary>The value of the chosen item, or an empty text for a placeholder.</summary>
        private static string SelectedId(ComboBox box) =>
            box.SelectedItem is LookupItem { HasValue: true } item ? item.Id : string.Empty;

        private void SetStatus(string text, Color color)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = color;
        }

        private void ShowMode(ViewMode mode)
        {
            _mode = mode;
            pnlBuilder.Visible = mode == ViewMode.Builder;
            pnlList.Visible = mode == ViewMode.List;
            ThemeHelper.StyleButton(btnModeBuilder, mode == ViewMode.Builder ? ButtonKind.Primary : ButtonKind.Secondary);
            ThemeHelper.StyleButton(btnModeList, mode == ViewMode.List ? ButtonKind.Primary : ButtonKind.Secondary);

            if (_initialized && mode == ViewMode.List) LoadBills();
        }

        // ------------------------------------------------------------------
        // The bill on the screen: new / opened / state (Draft or Finalized)
        // ------------------------------------------------------------------

        private void MarkDirty()
        {
            if (_loading || _bill.IsFinalized) return;
            _dirty = true;
            UpdateBanner();
        }

        /// <summary>True when it is safe to replace the bill on the screen (nothing unsaved, or the user agrees to discard it).</summary>
        private bool ConfirmDiscardChanges()
        {
            if (!_dirty || _bill.IsFinalized) return true;
            return MessageHelper.Confirm("The bill on the Bill Builder screen has changes that are not saved.\n\nDiscard them?",
                "Unsaved changes", DialogOwner());
        }

        private static string FormatPercent(decimal rate) => (rate * 100m).ToString("0.##", CultureInfo.InvariantCulture);

        private void StartNewBill()
        {
            _loading = true;
            try
            {
                _bill = new Bill { TaxRate = SettingsHelper.GetDecimal(SettingsHelper.DefaultTaxRate, 0.12m) };
                _items.Clear();

                txtPatientSearch.Clear();
                BindPatients(_repository.GetPatients(null), null);

                if (cboDiscountType.Items.Count > 0) cboDiscountType.SelectedIndex = 0;
                txtDiscount.Text = "0.00";
                txtTaxRate.Text = FormatPercent(_bill.TaxRate);
                txtAuthorizedBy.Clear();

                cboCategory.SelectedIndex = 0;
                LoadPriceList(string.Empty);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The new bill could not be prepared.", DialogOwner());
            }
            finally
            {
                _loading = false;
            }

            _validator.Begin();
            RefreshItemsGrid();
            ResetItemEditor(clearGridSelection: false);
            _dirty = false;
            UpdateDiscountCaption();
            ApplyState();
            Recalculate();
        }

        private void LoadBillIntoScreen(Bill bill)
        {
            _loading = true;
            try
            {
                _bill = bill;
                _items.Clear();
                _items.AddRange(bill.Items);

                txtPatientSearch.Clear();
                LookupItem patient = _repository.GetPatient(bill.PatientId)
                    ?? new LookupItem { Id = bill.PatientId, Name = bill.PatientId + "  -  " + bill.PatientName };
                BindPatients(new List<LookupItem> { patient }, bill.PatientId);

                cboDiscountType.SelectedValue = bill.DiscountType;
                txtDiscount.Text = bill.DiscountValue.ToString("0.00", CultureInfo.InvariantCulture);
                txtTaxRate.Text = FormatPercent(bill.TaxRate);
                txtAuthorizedBy.Text = bill.AuthorizedBy;

                cboCategory.SelectedIndex = 0;
                LoadPriceList(string.Empty);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The bill could not be shown.", DialogOwner());
            }
            finally
            {
                _loading = false;
            }

            _validator.Begin();
            RefreshItemsGrid();
            ResetItemEditor(clearGridSelection: false);
            _dirty = false;
            UpdateDiscountCaption();
            ApplyState();
            Recalculate();
        }

        /// <summary>Draft versus Finalized: a finalized bill disables every control that could change it.</summary>
        private void ApplyState()
        {
            bool locked = _bill.IsFinalized;

            foreach (Control control in new Control[]
            {
                cboCategory, cboItem, txtDescription, txtQuantity, txtUnitPrice, btnAddItem, btnClearItem,
                cboDiscountType, txtDiscount, txtTaxRate, txtAuthorizedBy, btnSaveDraft, btnFinalize
            })
            {
                control.Enabled = !locked;
            }
            btnRemoveItem.Enabled = !locked && _editingIndex >= 0;

            // the patient can be chosen until the bill is saved for the first time
            bool patientEditable = !locked && !_bill.IsSaved;
            txtPatientSearch.Enabled = patientEditable;
            btnPatientFind.Enabled = patientEditable;
            cboPatient.Enabled = patientEditable;

            txtBillCode.Text = _bill.BillId;
            UpdateBanner();
        }

        private void UpdateBanner()
        {
            if (_bill.IsFinalized)
            {
                lblBillState.Text = "FINALIZED  -  " + _bill.BillId + "  -  locked (view only)";
                lblBillState.BackColor = FinalBack;
                lblBillState.ForeColor = ThemeHelper.SuccessInk;
            }
            else
            {
                string text = !_bill.IsSaved ? "NEW BILL  -  not saved yet" : $"DRAFT  -  {_bill.BillId}  -  saved, still editable";
                if (_dirty && _bill.IsSaved) text += "  (unsaved changes)";
                lblBillState.Text = text;
                lblBillState.BackColor = DraftBack;
                lblBillState.ForeColor = DraftFore;
            }
        }

        private void NewBillClicked()
        {
            if (!ConfirmDiscardChanges()) return;
            StartNewBill();
            SetStatus("New bill started.", ThemeHelper.TextMuted);
        }

        // ------------------------------------------------------------------
        // Patient
        // ------------------------------------------------------------------

        private void BindPatients(List<LookupItem> patients, string? selectId)
        {
            bool was = _loading;
            _loading = true;
            try
            {
                var list = new List<LookupItem> { LookupItem.Placeholder("Select the patient...") };
                list.AddRange(patients);
                cboPatient.DataSource = list;
                cboPatient.DisplayMember = nameof(LookupItem.Name);
                cboPatient.ValueMember = nameof(LookupItem.Id);
                cboPatient.SelectedIndex = 0;
                if (selectId != null) cboPatient.SelectedValue = selectId;
            }
            finally
            {
                _loading = was;
            }
        }

        private void FindPatients()
        {
            if (_bill.IsFinalized || _bill.IsSaved) return;
            try
            {
                List<LookupItem> patients = _repository.GetPatients(txtPatientSearch.Text.Trim());
                BindPatients(patients, null);

                if (patients.Count == 0)
                    SetStatus("No patient matches that search.", ThemeHelper.Warning);
                else if (patients.Count == 1)
                {
                    cboPatient.SelectedIndex = 1;
                    SetStatus("1 patient found and selected.", ThemeHelper.TextMuted);
                }
                else
                    SetStatus($"{patients.Count} patients found - choose one from the list.", ThemeHelper.TextMuted);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The patients could not be searched.", DialogOwner());
            }
        }

        // ------------------------------------------------------------------
        // Add a charge (category -> price list -> item)
        // ------------------------------------------------------------------

        private void OnCategoryChanged()
        {
            if (_loading) return;
            LoadPriceList(SelectedId(cboCategory));
        }

        /// <summary>Fills the Item drop-down from the price list of the category (services, medicines, lab tests or room types).</summary>
        private void LoadPriceList(string category)
        {
            bool isRoom = category == BillingRules.CategoryRoom;
            lblQuantity.Text = isRoom ? "Days *" : "Quantity *";

            var entries = new List<PriceListItem> { new PriceListItem { Id = PriceListItem.SelectId, DisplayText = "Select the item..." } };
            if (category.Length > 0)
            {
                entries.Add(new PriceListItem { Id = PriceListItem.OtherId, DisplayText = "(Other - type the description and the price)" });
                try
                {
                    entries.AddRange(_repository.GetPriceList(category));
                }
                catch (Exception ex)
                {
                    MessageHelper.Error(ex, "The price list could not be loaded.", DialogOwner());
                }
            }

            bool was = _loading;
            _loading = true;
            try
            {
                _entries = entries;
                cboItem.DataSource = entries;
                cboItem.DisplayMember = nameof(PriceListItem.DisplayText);
                cboItem.ValueMember = nameof(PriceListItem.Id);
                cboItem.SelectedIndex = 0;

                _listDescription = null;
                txtDescription.Clear();
                txtUnitPrice.Clear();
                SetItemFieldsReadOnly(true);
            }
            finally
            {
                _loading = was;
            }
            UpdateLineAmount();
        }

        private void SetItemFieldsReadOnly(bool readOnly)
        {
            ThemeHelper.SetReadOnlyLook(txtDescription, readOnly);
            ThemeHelper.SetReadOnlyLook(txtUnitPrice, readOnly);
        }

        private void OnItemPicked()
        {
            if (_loading) return;

            var picked = cboItem.SelectedItem as PriceListItem;
            _listDescription = null;

            if (picked == null || picked.IsPlaceholder)
            {
                txtDescription.Clear();
                txtUnitPrice.Clear();
                SetItemFieldsReadOnly(true);
            }
            else if (picked.IsOther)
            {
                // "Other": the cashier types the description and the price
                txtDescription.Clear();
                txtUnitPrice.Clear();
                SetItemFieldsReadOnly(false);
                txtDescription.Focus();
            }
            else
            {
                _listDescription = picked.Description;
                txtDescription.Text = picked.Description;
                txtUnitPrice.Text = picked.UnitPrice.ToString("0.00", CultureInfo.InvariantCulture);
                SetItemFieldsReadOnly(true);
                if (txtQuantity.Text.Trim().Length == 0) txtQuantity.Text = "1";
            }
            UpdateLineAmount();
        }

        private void UpdateLineAmount()
        {
            if (ValidationHelper.TryParsePositiveInt(txtQuantity.Text, out int quantity) &&
                ValidationHelper.TryParseDecimal(txtUnitPrice.Text, out decimal price) && price > 0)
                lblLineAmount.Text = Money(BillCalculator.Round2(quantity * price));
            else
                lblLineAmount.Text = Money(0m);
        }

        /// <summary>Clears the "Add a charge" fields and returns to adding a new item (the Add button reads "Add Item" again).</summary>
        private void ResetItemEditor(bool clearGridSelection)
        {
            bool was = _loading;
            _loading = true;
            try
            {
                _editingIndex = -1;
                _listDescription = null;
                txtDescription.Clear();
                txtUnitPrice.Clear();
                txtQuantity.Text = "1";
                if (cboItem.Items.Count > 0) cboItem.SelectedIndex = 0;
                SetItemFieldsReadOnly(true);
                btnAddItem.Text = "Add Item";
                btnRemoveItem.Enabled = false;
                if (clearGridSelection)
                {
                    dgvItems.ClearSelection();
                    dgvItems.CurrentCell = null;
                }
            }
            finally
            {
                _loading = was;
            }
            UpdateLineAmount();
        }

        private void AddOrUpdateItem()
        {
            if (_bill.IsFinalized) return;
            _validator.Begin();

            string category = SelectedId(cboCategory);
            var picked = cboItem.SelectedItem as PriceListItem;
            bool isRoom = category == BillingRules.CategoryRoom;

            _validator.Check(category.Length > 0, cboCategory, "Choose the category.");
            _validator.Check(picked != null && !picked.IsPlaceholder, cboItem,
                "Choose an item from the price list, or choose \"Other\" to type your own.");

            bool fromList = picked != null && picked.IsListed && _listDescription != null;
            string description = fromList ? _listDescription! : txtDescription.Text.Trim();
            if (picked != null && picked.IsOther && description.Length == 0)
                _validator.AddError(txtDescription, "Enter the description of the item.");

            int maxQuantity = isRoom ? BillingRules.MaxRoomDays : BillingRules.MaxQuantity;
            bool quantityOk = ValidationHelper.TryParsePositiveInt(txtQuantity.Text, out int quantity) && quantity <= maxQuantity;
            if (!quantityOk)
                _validator.AddError(txtQuantity,
                    $"{(isRoom ? "The number of days" : "The quantity")} must be a whole number from 1 to {maxQuantity}.");

            // a room charge reads "Semi-Private Room (3 days)": room type x days x daily rate
            if (isRoom && fromList && quantityOk)
                description += quantity == 1 ? " (1 day)" : $" ({quantity} days)";

            decimal price = 0m;
            string priceText = txtUnitPrice.Text.Trim();
            if (priceText.Length == 0) _validator.AddError(txtUnitPrice, "Enter the unit price.");
            else if (!ValidationHelper.TryParseDecimal(priceText, out price)) _validator.AddError(txtUnitPrice, "The unit price must be a number.");
            else if (price <= 0 || price > BillingRules.MaxUnitPrice)
                _validator.AddError(txtUnitPrice, $"The unit price must be greater than 0 and at most {BillingRules.MaxUnitPrice:N2}.");

            if (_editingIndex < 0 && _items.Count >= BillingRules.MaxItems)
                _validator.AddError(dgvItems, $"A bill can have at most {BillingRules.MaxItems} items.");

            if (!_validator.IsValid)
            {
                _validator.ShowErrors(DialogOwner());
                return;
            }

            var item = new BillItem
            {
                Category = category,
                Description = description,
                Quantity = quantity,
                UnitPrice = price
            };

            if (_editingIndex >= 0 && _editingIndex < _items.Count)
            {
                item.BillItemId = _items[_editingIndex].BillItemId;
                _items[_editingIndex] = item;
                SetStatus($"Updated: {description}.", ThemeHelper.Success);
            }
            else
            {
                _items.Add(item);
                SetStatus($"Added: {description}  ({Money(item.Amount)}).", ThemeHelper.Success);
            }

            MarkDirty();
            RefreshItemsGrid();
            ResetItemEditor(clearGridSelection: true);
            Recalculate();
        }

        private void RemoveItem()
        {
            if (_bill.IsFinalized || _editingIndex < 0 || _editingIndex >= _items.Count) return;

            BillItem item = _items[_editingIndex];
            if (!MessageHelper.ConfirmDestructive($"Remove \"{item.Description}\" ({Money(item.Amount)}) from this bill?", "Remove item", DialogOwner()))
                return;

            _items.RemoveAt(_editingIndex);
            MarkDirty();
            RefreshItemsGrid();
            ResetItemEditor(clearGridSelection: true);
            Recalculate();
            SetStatus($"Removed: {item.Description}.", ThemeHelper.TextMuted);
        }

        /// <summary>Shows the items in the grid again (the grid keeps its columns; the selection is cleared or set to selectIndex).</summary>
        private void RefreshItemsGrid(int selectIndex = -1)
        {
            bool was = _loading;
            _loading = true;
            try
            {
                dgvItems.DataSource = null;
                dgvItems.DataSource = _items;
                dgvItems.ClearSelection();
                dgvItems.CurrentCell = null;
                if (selectIndex >= 0 && selectIndex < dgvItems.Rows.Count) dgvItems.Rows[selectIndex].Selected = true;
            }
            finally
            {
                _loading = was;
            }
        }

        /// <summary>Clicking an item in the grid loads it into the editor, so it can be changed (Update Item) or removed.</summary>
        private void OnItemSelected()
        {
            if (_loading || _bill.IsFinalized) return;
            if (dgvItems.SelectedRows.Count == 0) return;

            int index = dgvItems.SelectedRows[0].Index;
            if (index < 0 || index >= _items.Count) return;

            BillItem item = _items[index];
            bool was = _loading;
            _loading = true;
            try
            {
                _validator.Begin();
                _editingIndex = index;
                cboCategory.SelectedValue = item.Category;
                LoadPriceList(item.Category);

                // use the price-list entry only when the saved item still matches it exactly; otherwise edit it as an "Other" item
                string baseDescription = item.Category == BillingRules.CategoryRoom
                    ? Regex.Replace(item.Description, @" \(\d+ days?\)$", string.Empty)
                    : item.Description;
                PriceListItem? match = _entries.FirstOrDefault(p => p.IsListed && p.Description == baseDescription && p.UnitPrice == item.UnitPrice);
                PriceListItem? other = _entries.FirstOrDefault(p => p.IsOther);
                if (match != null)
                {
                    cboItem.SelectedItem = match;
                    _listDescription = match.Description;
                }
                else if (other != null)
                {
                    cboItem.SelectedItem = other;
                    _listDescription = null;
                }

                txtDescription.Text = match != null ? baseDescription : item.Description;
                txtQuantity.Text = item.Quantity.ToString(CultureInfo.InvariantCulture);
                txtUnitPrice.Text = item.UnitPrice.ToString("0.00", CultureInfo.InvariantCulture);
                SetItemFieldsReadOnly(match != null);

                btnAddItem.Text = "Update Item";
                btnRemoveItem.Enabled = true;
            }
            finally
            {
                _loading = was;
            }
            UpdateLineAmount();
        }

        // ------------------------------------------------------------------
        // Summary: Subtotal -> Discount -> Tax -> Grand total
        // ------------------------------------------------------------------

        private void OnDiscountTypeChanged()
        {
            if (_loading) return;
            UpdateDiscountCaption();
            MarkDirty();
            Recalculate();
        }

        private void UpdateDiscountCaption()
        {
            lblDiscountCap.Text = SelectedId(cboDiscountType) == BillingRules.DiscountPercent ? "Discount (%)" : "Discount (amount)";
        }

        private void OnSummaryInputChanged()
        {
            if (_loading) return;
            MarkDirty();
            Recalculate();
        }

        /// <summary>
        /// Builds a Bill from what is on the screen. Text that is not a number becomes a BillingIssue
        /// (the input filter already blocks letters, so this only happens with a lone "." for example).
        /// </summary>
        private (Bill Bill, List<BillingIssue> Issues) CollectBill()
        {
            var issues = new List<BillingIssue>();
            var bill = new Bill
            {
                BillId = _bill.BillId,
                Status = _bill.Status,
                CreatedAt = _bill.CreatedAt,
                FinalizedAt = _bill.FinalizedAt,
                CreatedBy = _bill.CreatedBy,
                PatientId = SelectedId(cboPatient),
                DiscountType = SelectedId(cboDiscountType),
                AuthorizedBy = txtAuthorizedBy.Text.Trim(),
                Items = _items.Select(i => i.Clone()).ToList()
            };

            string discount = txtDiscount.Text.Trim();
            if (discount.Length == 0) bill.DiscountValue = 0m;
            else if (ValidationHelper.TryParseDecimal(discount, out decimal discountValue)) bill.DiscountValue = discountValue;
            else issues.Add(new BillingIssue(BillingField.Discount, "The discount must be a number (for example 250.00)."));

            string tax = txtTaxRate.Text.Trim();
            if (tax.Length == 0) issues.Add(new BillingIssue(BillingField.Tax, "Enter the tax rate in percent (type 0 for no tax)."));
            else if (ValidationHelper.TryParseDecimal(tax, out decimal taxPercent)) bill.TaxRate = taxPercent / 100m;
            else issues.Add(new BillingIssue(BillingField.Tax, "The tax rate must be a number (for example 12)."));

            return (bill, issues);
        }

        /// <summary>Recalculates every amount from the items, the discount and the tax rate, and shows or hides the authorization prompt.</summary>
        private void Recalculate()
        {
            var (bill, parseIssues) = CollectBill();
            BillTotals totals = BillCalculator.Calculate(bill);

            lblSubtotal.Text = Money(totals.Subtotal);
            lblDiscountAmount.Text = totals.DiscountAmount > 0 ? "- " + Money(totals.DiscountAmount) : Money(0m);
            lblTaxable.Text = Money(totals.TaxableAmount);
            lblTaxAmount.Text = Money(totals.TaxAmount);
            lblGrandTotal.Text = Money(totals.GrandTotal);

            lblItemsInfo.Text = _items.Count == 0
                ? "No items yet - add a charge on the left."
                : $"{_items.Count} item(s)   -   subtotal {Money(totals.Subtotal)}" +
                  (_bill.IsFinalized ? "" : "   -   click an item to change or remove it");

            // problems that can be shown while typing: the discount and the tax rate
            List<BillingIssue> live = parseIssues
                .Concat(BillingRepository.Validate(bill, finalize: false))
                .Where(i => i.Field == BillingField.Discount || i.Field == BillingField.Tax)
                .ToList();
            bool discountInvalid = live.Any(i => i.Field == BillingField.Discount);

            lblSummaryNote.Text = live.Count > 0 ? live[0].Message : string.Empty;
            lblGrandTotal.ForeColor = live.Count > 0 ? ErrorFore : ThemeHelper.Accent;

            // authorization prompt: only when the discount is above the threshold
            bool needsAuthorization = !discountInvalid && BillingRepository.NeedsAuthorization(bill);
            tlpAuth.Visible = needsAuthorization;
            if (needsAuthorization)
            {
                decimal percentThreshold = SettingsHelper.GetDecimal(SettingsHelper.DiscountThresholdPercent, 20m);
                decimal amountThreshold = SettingsHelper.GetDecimal(SettingsHelper.DiscountThresholdAmount, 5000m);
                decimal percent = BillCalculator.DiscountPercentOf(totals.Subtotal, bill.DiscountType, bill.DiscountValue, totals.DiscountAmount);
                lblAuthMessage.Text =
                    $"Authorization required: a discount of {Money(totals.DiscountAmount)} ({percent:0.##}% of the subtotal) is above the limit " +
                    $"of {percentThreshold:0.##}% or {Money(amountThreshold)}. Enter who authorized it before the bill can be finalized.";
            }
        }

        // ------------------------------------------------------------------
        // Save Draft / Finalize
        // ------------------------------------------------------------------

        private Control ControlFor(BillingField field)
        {
            switch (field)
            {
                case BillingField.Patient: return cboPatient;
                case BillingField.Items: return dgvItems;
                case BillingField.Discount: return txtDiscount;
                case BillingField.Tax: return txtTaxRate;
                default: return txtAuthorizedBy;
            }
        }

        private void Persist(bool finalize)
        {
            if (_bill.IsFinalized) return;
            _validator.Begin();

            var (bill, parseIssues) = CollectBill();
            var issues = new List<BillingIssue>(parseIssues);
            issues.AddRange(BillingRepository.Validate(bill, finalize));
            foreach (BillingIssue issue in issues) _validator.AddError(ControlFor(issue.Field), issue.Message);

            if (!_validator.IsValid)
            {
                _validator.ShowErrors(DialogOwner());
                return;
            }

            if (finalize)
            {
                BillTotals totals = BillCalculator.Calculate(bill);
                string message =
                    $"Finalize this bill for {cboPatient.Text.Trim()}?\n\n" +
                    $"Items:        {bill.Items.Count}\n" +
                    $"Subtotal:   {Money(totals.Subtotal)}\n" +
                    $"Discount:   {Money(totals.DiscountAmount)}\n" +
                    $"Tax:           {Money(totals.TaxAmount)}\n" +
                    $"GRAND TOTAL:  {Money(totals.GrandTotal)}\n\n" +
                    "The bill code will be generated and the bill will be LOCKED: items can no longer be added, edited or removed.";
                if (!MessageHelper.Confirm(message, "Finalize bill", DialogOwner())) return;
            }

            try
            {
                Bill saved = finalize ? _repository.Finalize(bill) : _repository.SaveDraft(bill);
                LoadBillIntoScreen(saved);

                if (finalize)
                {
                    SetStatus($"Finalized {saved.BillId}.", ThemeHelper.Success);
                    BillTotals totals = BillCalculator.Calculate(saved);
                    MessageHelper.Success($"Bill {saved.BillId} was finalized for {saved.PatientName}.\n\nGrand total: {Money(totals.GrandTotal)}\n\n" +
                                          "The bill is now locked. Payments are recorded in Payment Processing.", "Bill finalized", DialogOwner());
                }
                else
                {
                    SetStatus($"Draft {saved.BillId} saved.", ThemeHelper.Success);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Itemized Billing", DialogOwner());
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The bill could not be saved.", DialogOwner());
            }
        }

        // ------------------------------------------------------------------
        // All bills
        // ------------------------------------------------------------------

        private void LoadBills()
        {
            string? keep = (dgvBills.SelectedRows.Count > 0 ? dgvBills.SelectedRows[0].DataBoundItem as BillListRow : null)?.BillId;
            try
            {
                _rows = _repository.GetBills(txtListSearch.Text.Trim(), SelectedId(cboListStatus));

                bool was = _loading;
                _loading = true;
                dgvBills.DataSource = null;
                dgvBills.DataSource = _rows;
                dgvBills.ClearSelection();
                dgvBills.CurrentCell = null;
                _loading = was;

                if (keep != null)
                {
                    foreach (DataGridViewRow row in dgvBills.Rows)
                    {
                        if (row.DataBoundItem is BillListRow r && r.BillId == keep) { row.Selected = true; break; }
                    }
                }

                lblListCount.Text = _rows.Count == 0
                    ? "No bill found."
                    : $"{_rows.Count} bill(s)" + (_rows.Count >= BillingRules.ListLimit ? $"  (showing the newest {BillingRules.ListLimit})" : "");
                UpdateListButtons();
            }
            catch (Exception ex)
            {
                _loading = false;
                MessageHelper.Error(ex, "The bills could not be loaded.", DialogOwner());
            }
        }

        private BillListRow? SelectedRow() =>
            dgvBills.SelectedRows.Count > 0 ? dgvBills.SelectedRows[0].DataBoundItem as BillListRow : null;

        private void UpdateListButtons()
        {
            BillListRow? row = SelectedRow();
            btnOpenBill.Enabled = row != null;
            btnDeleteDraft.Enabled = row != null && row.Status == BillingRules.StatusDraft;
        }

        private void OpenSelectedBill()
        {
            BillListRow? row = SelectedRow();
            if (row == null) return;
            if (!ConfirmDiscardChanges()) return;

            try
            {
                Bill? bill = _repository.GetBill(row.BillId);
                if (bill == null)
                {
                    MessageHelper.Warning("This bill no longer exists. The list will be refreshed.", "Itemized Billing", DialogOwner());
                    LoadBills();
                    return;
                }

                LoadBillIntoScreen(bill);
                ShowMode(ViewMode.Builder);
                SetStatus(bill.IsFinalized ? $"Opened {bill.BillId} (locked, view only)." : $"Opened draft {bill.BillId} - you can keep editing it.",
                    ThemeHelper.TextMuted);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The bill could not be opened.", DialogOwner());
            }
        }

        private void DeleteSelectedDraft()
        {
            BillListRow? row = SelectedRow();
            if (row == null || row.Status != BillingRules.StatusDraft) return;

            if (!MessageHelper.ConfirmDestructive(
                    $"Delete the draft bill of {row.PatientName} with {row.ItemCount} item(s)?\n\nThis cannot be undone.", "Delete draft", DialogOwner()))
                return;

            try
            {
                _repository.DeleteDraft(row.BillId);
                if (_bill.BillId == row.BillId) StartNewBill();
                SetStatus($"Deleted the draft bill of {row.PatientName}.", ThemeHelper.Success);
                LoadBills();
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Itemized Billing", DialogOwner());
                LoadBills();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The draft could not be deleted.", DialogOwner());
            }
        }
    }
}