using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP01_PatientRegistration;
using HospitalManagementSystem.Models.MP11_Billing;
using HospitalManagementSystem.Repositories.MP01_PatientRegistration;
using HospitalManagementSystem.Repositories.MP11_Billing;
using System.Globalization;
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
    ///     or delete a Draft. Finalized bills can never be deleted or edited: a mistake is corrected with VOID BILL
    ///     (reason required, only when nothing is left paid on it - refund first), optionally creating a corrected draft copy.
    ///
    ///   LINKED CHARGES ("Unbilled Charges" button): the patient's consultation fees, medicines dispensed with
    ///     "Charge to Bill" and lab orders that are not on a bill yet can be added in one step. Such an item keeps its link
    ///     (column "From"), can be removed but not changed, and can never be billed twice.
    ///
    ///   DISCOUNTS: Flat amount, Percent, or the statutory Senior Citizen / PWD discount (20 %, VAT-exempt, ID number required).
    ///   COVERAGE: PhilHealth or HMO pays part of the bill; the covered amount is deducted and the patient pays the rest.
    ///   Both are filled in AUTOMATICALLY from the patient's record (discount card / coverage saved at registration)
    ///   when the patient of a new bill is chosen; the cashier only enters the amount PhilHealth / the HMO covers.
    ///
    /// The rules live in BillingRepository.Validate and BillCalculator; this form shows the same rules next to the fields.
    /// All visuals live in BillingForm.Designer.cs; the grid columns are created here (BuildGridColumns).
    /// </summary>
    public partial class BillingForm : Views.Main.PageHost, IPageMinimumSize
    {
        /// <summary>The bill and add-a-charge cards are stacked on the left, so this page needs a little more height.</summary>
        public Size PageMinimumSize => new Size(1150, 920);

        private static readonly Color DraftBack = ThemeHelper.WarningSoft;
        private static readonly Color DraftFore = ThemeHelper.Warning;
        private static readonly Color FinalBack = ThemeHelper.SuccessSoft;
        private static readonly Color VoidBack = ThemeHelper.DangerSoft;
        private static readonly Color ErrorFore = ThemeHelper.Danger;

        private enum ViewMode { Builder, List }

        private readonly BillingRepository _repository = new BillingRepository();
        private readonly PatientRepository _patients = new PatientRepository();
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
                txtDiscount, txtTaxRate, txtAuthorizedBy, txtDiscountId, cboBillCoverage, txtBillCoverageNo, txtCoverageAmount,
                cboBillHmoProvider);

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

            AddColumn(dgvItems, "colICategory", "Category", nameof(BillItem.Category), 14);
            AddColumn(dgvItems, "colIDescription", "Description", nameof(BillItem.Description), 34);
            AddColumn(dgvItems, "colIFrom", "From", nameof(BillItem.SourceText), 15);
            AddColumn(dgvItems, "colIQuantity", "Qty / days", nameof(BillItem.QuantityText), 9, true);
            AddColumn(dgvItems, "colIPrice", "Unit price", nameof(BillItem.UnitPriceText), 13, true);
            AddColumn(dgvItems, "colIAmount", "Amount", nameof(BillItem.AmountText), 15, true);

            AddColumn(dgvBills, "colLCode", "Bill ID", nameof(BillListRow.BillId), 15);
            AddColumn(dgvBills, "colLPatient", "Patient", nameof(BillListRow.PatientName), 16);
            AddColumn(dgvBills, "colLItems", "Items", nameof(BillListRow.ItemCountText), 6, true);
            AddColumn(dgvBills, "colLSubtotal", "Subtotal", nameof(BillListRow.SubtotalText), 10, true);
            AddColumn(dgvBills, "colLDiscount", "Discount", nameof(BillListRow.DiscountText), 9, true);
            AddColumn(dgvBills, "colLTax", "Tax", nameof(BillListRow.TaxText), 8, true);
            AddColumn(dgvBills, "colLTotal", "Patient pays", nameof(BillListRow.GrandTotalText), 11, true);
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
            InputHelper.Attach(txtDiscountId, InputKind.Address, BillingRules.MaxDiscountId);
            InputHelper.Attach(txtListSearch, InputKind.Address, 60);
            InputHelper.Attach(txtBillCoverageNo, InputKind.Address, BenefitRules.MaxCardNumber);
            InputHelper.Attach(txtCoverageAmount, InputKind.Decimal2, 11);
            cboBillHmoProvider.MaxLength = BenefitRules.MaxProvider;
        }

        private void WireEvents()
        {
            btnModeBuilder.Click += (s, e) => ShowMode(ViewMode.Builder);
            btnModeList.Click += (s, e) => ShowMode(ViewMode.List);

            // bill + patient
            btnPatientFind.Click += (s, e) => FindPatients();
            cboPatient.SelectedIndexChanged += (s, e) => { if (!_loading) { MarkDirty(); UpdateUnbilledButton(); ApplyPatientBenefits(); } };
            btnUnbilled.Click += (s, e) => AddUnbilledCharges();
            txtPatientSearch.KeyDown += (s, e) => OnEnter(e, () => FindPatients());
            SearchHelper.AttachLive(txtPatientSearch, () => FindPatients(), minLength: 2);

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
            txtDiscountId.TextChanged += (s, e) => OnSummaryInputChanged();
            cboBillCoverage.SelectedIndexChanged += (s, e) => OnCoverageChanged();
            txtBillCoverageNo.TextChanged += (s, e) => OnSummaryInputChanged();
            txtCoverageAmount.TextChanged += (s, e) => OnSummaryInputChanged();
            cboBillHmoProvider.TextChanged += (s, e) => OnSummaryInputChanged();
            btnSaveDraft.Click += (s, e) => Persist(finalize: false);
            btnFinalize.Click += (s, e) => Persist(finalize: true);
            btnNewBill.Click += (s, e) => NewBillClicked();

            // all bills
            btnListSearch.Click += (s, e) => LoadBills();
            btnListAll.Click += (s, e) => { _loading = true; txtListSearch.Clear(); cboListStatus.SelectedIndex = 0; _loading = false; LoadBills(); };
            txtListSearch.KeyDown += (s, e) => OnEnter(e, () => LoadBills());
            SearchHelper.AttachLive(txtListSearch, () => LoadBills());
            cboListStatus.SelectedIndexChanged += (s, e) => { if (!_loading && _initialized) LoadBills(); };
            dgvBills.SelectionChanged += (s, e) => UpdateListButtons();
            dgvBills.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) OpenSelectedBill(); };
            btnOpenBill.Click += (s, e) => OpenSelectedBill();
            btnDeleteDraft.Click += (s, e) => DeleteSelectedDraft();
            btnVoidBill.Click += (s, e) => VoidSelectedBill();

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
                BindLookup(cboBillCoverage, new List<LookupItem>
                {
                    new LookupItem { Id = BillingRules.CoverageNone, Name = "None" },
                    new LookupItem { Id = BillingRules.CoveragePhilHealth, Name = "PhilHealth" },
                    new LookupItem { Id = BillingRules.CoverageHmo, Name = "HMO" }
                }, null);
                cboBillHmoProvider.Items.Clear();
                cboBillHmoProvider.Items.AddRange(BenefitRules.HmoProviders.Cast<object>().ToArray());
                BindLookup(cboListStatus, new List<LookupItem>
                {
                    LookupItem.Placeholder("All bills"),
                    LookupItem.Of(BillingRules.StatusDraft),
                    LookupItem.Of(BillingRules.StatusFinalized),
                    LookupItem.Of(BillingRules.StatusVoided)
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
            if (_loading || _bill.IsLocked) return;
            _dirty = true;
            UpdateBanner();
        }

        /// <summary>True when it is safe to replace the bill on the screen (nothing unsaved, or the user agrees to discard it).</summary>
        private bool ConfirmDiscardChanges()
        {
            if (!_dirty || _bill.IsLocked) return true;
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
                txtDiscountId.Clear();
                SetCoverageInputs(BillingRules.CoverageNone, string.Empty, string.Empty, 0m);
                lblCoverageMessage.Text = string.Empty;

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
                txtDiscountId.Text = bill.DiscountIdNumber;
                SetCoverageInputs(bill.CoverageType, bill.CoverageNumber, bill.CoverageProvider, bill.CoverageAmount);
                lblCoverageMessage.Text = string.Empty;

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

        /// <summary>Draft versus Finalized / Voided: a locked bill disables every control that could change it.</summary>
        private void ApplyState()
        {
            bool locked = _bill.IsLocked;

            foreach (Control control in new Control[]
            {
                cboCategory, cboItem, txtDescription, txtQuantity, txtUnitPrice, btnAddItem, btnClearItem,
                cboDiscountType, txtDiscount, txtTaxRate, txtAuthorizedBy, txtDiscountId, btnSaveDraft, btnFinalize,
                cboBillCoverage, txtBillCoverageNo, txtCoverageAmount, cboBillHmoProvider
            })
            {
                control.Enabled = !locked;
            }
            btnRemoveItem.Enabled = !locked && _editingIndex >= 0;
            ApplyDiscountLook();
            ApplyCoverageLook();
            UpdateUnbilledButton();

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
            if (_bill.IsVoided)
            {
                string when = _bill.VoidedAt?.ToString("MMM d, yyyy h:mm tt", CultureInfo.InvariantCulture) ?? string.Empty;
                lblBillState.Text = $"VOIDED  -  {_bill.BillId}  -  {when} by {(_bill.VoidedBy.Length > 0 ? _bill.VoidedBy : "-")}: {_bill.VoidReason}";
                lblBillState.BackColor = VoidBack;
                lblBillState.ForeColor = ThemeHelper.DangerInk;
            }
            else if (_bill.IsFinalized)
            {
                lblBillState.Text = "FINALIZED  -  " + _bill.BillId + "  -  locked (view only)";
                lblBillState.BackColor = FinalBack;
                lblBillState.ForeColor = ThemeHelper.SuccessInk;
            }
            else
            {
                string text = !_bill.IsSaved ? "NEW BILL  -  not saved yet" : $"DRAFT  -  {_bill.BillId}  -  saved, still editable";
                if (_bill.CorrectsBillId.Length > 0) text += $"  -  corrects voided {_bill.CorrectsBillId}";
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
            if (_bill.IsLocked || _bill.IsSaved) return;
            try
            {
                List<LookupItem> patients = _repository.GetPatients(txtPatientSearch.Text.Trim());
                BindPatients(patients, null);

                if (patients.Count == 0)
                    SetStatus("No patient matches that search.", ThemeHelper.Warning);
                else if (patients.Count == 1)
                {
                    cboPatient.SelectedIndex = 1;
                    MarkDirty();
                    SetStatus("1 patient found and selected.", ThemeHelper.TextMuted);
                }
                else
                    SetStatus($"{patients.Count} patients found - choose one from the list.", ThemeHelper.TextMuted);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The patients could not be searched.", DialogOwner());
            }
            UpdateUnbilledButton();
        }

        // ------------------------------------------------------------------
        // Charges from Consultations, Dispensing and Laboratory
        // ------------------------------------------------------------------

        /// <summary>Unbilled charges of the chosen patient that are not on the screen yet.</summary>
        private List<UnbilledCharge> PendingCharges(string patientId)
        {
            var onBill = new HashSet<string>(_items.Where(i => i.IsLinked).Select(i => i.SourceType + "|" + i.SourceId));
            return _repository.GetUnbilledCharges(patientId).Where(c => !onBill.Contains(c.SourceType + "|" + c.SourceId)).ToList();
        }

        /// <summary>"Unbilled Charges (3)": enabled while the bill is editable and a patient is chosen.</summary>
        private void UpdateUnbilledButton()
        {
            string patientId = SelectedId(cboPatient);
            if (_bill.IsLocked || patientId.Length == 0)
            {
                btnUnbilled.Enabled = false;
                btnUnbilled.Text = _bill.IsLocked ? "Unbilled Charges" : "Unbilled Charges  -  choose the patient first";
                return;
            }
            try
            {
                int count = PendingCharges(patientId).Count;
                btnUnbilled.Enabled = count > 0;
                btnUnbilled.Text = count == 0 ? "No unbilled charges for this patient" : $"Add Unbilled Charges ({count})";
            }
            catch (Exception)
            {
                btnUnbilled.Enabled = true;
                btnUnbilled.Text = "Unbilled Charges";
            }
        }

        private void AddUnbilledCharges()
        {
            if (_bill.IsLocked) return;
            string patientId = SelectedId(cboPatient);
            if (patientId.Length == 0)
            {
                MessageHelper.Warning("Choose the patient first.", "Unbilled charges", DialogOwner());
                return;
            }

            try
            {
                List<UnbilledCharge> charges = PendingCharges(patientId);
                if (charges.Count == 0)
                {
                    MessageHelper.Info("This patient has no unbilled consultation, medicine or lab charges.", "Unbilled charges", DialogOwner());
                    UpdateUnbilledButton();
                    return;
                }
                if (_items.Count + charges.Count > BillingRules.MaxItems)
                    charges = charges.Take(Math.Max(0, BillingRules.MaxItems - _items.Count)).ToList();

                using var dialog = new UnbilledChargesDialog(cboPatient.Text.Trim(), charges);
                if (dialog.ShowDialog(DialogOwner()) != DialogResult.OK) return;

                foreach (UnbilledCharge charge in dialog.Selected) _items.Add(charge.ToBillItem());
                MarkDirty();
                RefreshItemsGrid();
                ResetItemEditor(clearGridSelection: true);
                Recalculate();
                UpdateUnbilledButton();
                SetStatus($"Added {dialog.Selected.Count} linked charge(s) ({Money(dialog.Selected.Sum(c => c.Amount))}). Save or finalize the bill to keep them.",
                    ThemeHelper.Success);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The unbilled charges could not be loaded.", DialogOwner());
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
                ThemeHelper.SetReadOnlyLook(txtQuantity, false);
                btnAddItem.Enabled = !_bill.IsLocked;
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
            if (_bill.IsLocked) return;
            if (_editingIndex >= 0 && _editingIndex < _items.Count && _items[_editingIndex].IsLinked)
            {
                MessageHelper.Warning("This charge comes from another module (see the \"From\" column), so its amount cannot be changed here. " +
                                      "Remove it if it should not be billed.", "Linked charge", DialogOwner());
                return;
            }
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
            if (_bill.IsLocked || _editingIndex < 0 || _editingIndex >= _items.Count) return;

            BillItem item = _items[_editingIndex];
            if (!MessageHelper.ConfirmDestructive($"Remove \"{item.Description}\" ({Money(item.Amount)}) from this bill?", "Remove item", DialogOwner()))
                return;

            _items.RemoveAt(_editingIndex);
            MarkDirty();
            RefreshItemsGrid();
            ResetItemEditor(clearGridSelection: true);
            Recalculate();
            UpdateUnbilledButton();
            SetStatus($"Removed: {item.Description}." + (item.IsLinked ? " It is unbilled again." : string.Empty), ThemeHelper.TextMuted);
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
            if (_loading || _bill.IsLocked) return;
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

                if (item.IsLinked)
                {
                    // a linked charge keeps the amount of its source record: it can only be removed
                    SetItemFieldsReadOnly(true);
                    ThemeHelper.SetReadOnlyLook(txtQuantity, true);
                    btnAddItem.Enabled = false;
                    SetStatus($"\"{item.Description}\" comes from {item.SourceType} {item.SourceText} - it can be removed but not changed.", ThemeHelper.TextMuted);
                }
                else
                {
                    ThemeHelper.SetReadOnlyLook(txtQuantity, false);
                    btnAddItem.Enabled = true;
                }
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

        private bool _wasStatutory;

        private void OnDiscountTypeChanged()
        {
            if (_loading) return;
            string type = SelectedId(cboDiscountType);
            bool statutory = BillingRules.IsStatutory(type);

            _loading = true;
            try
            {
                if (statutory)
                {
                    // Senior Citizen / PWD: fixed 20 % and VAT-exempt
                    txtDiscount.Text = BillingRepository.StatutoryPercent().ToString("0.00", CultureInfo.InvariantCulture);
                    txtTaxRate.Text = "0";
                }
                else if (_wasStatutory)
                {
                    txtDiscount.Text = "0.00";
                    txtTaxRate.Text = FormatPercent(SettingsHelper.GetDecimal(SettingsHelper.DefaultTaxRate, 0.12m));
                    txtDiscountId.Clear();
                }
            }
            finally
            {
                _loading = false;
            }

            UpdateDiscountCaption();
            ApplyDiscountLook();
            MarkDirty();
            Recalculate();
            if (statutory && txtDiscountId.Enabled) txtDiscountId.Focus();
        }

        private void UpdateDiscountCaption()
        {
            lblDiscountCap.Text = BillingRules.IsPercentType(SelectedId(cboDiscountType)) ? "Discount (%)" : "Discount (amount)";
        }

        /// <summary>Senior Citizen / PWD: discount and tax are fixed (gray), and the ID number row appears.</summary>
        private void ApplyDiscountLook()
        {
            string type = SelectedId(cboDiscountType);
            bool statutory = BillingRules.IsStatutory(type);
            _wasStatutory = statutory;

            ThemeHelper.SetReadOnlyLook(txtDiscount, statutory || _bill.IsLocked);
            ThemeHelper.SetReadOnlyLook(txtTaxRate, statutory || _bill.IsLocked);
            tlpDiscountId.Visible = statutory;
            if (statutory)
            {
                decimal percent = BillingRepository.StatutoryPercent();
                lblDiscountId.Text = type == BillingRules.DiscountSenior ? "Senior ID no. *" : "PWD ID no. *";
                lblDiscountIdMessage.Text = type == BillingRules.DiscountSenior
                    ? $"Senior Citizen discount (RA 9994): {percent:0.##}% off and VAT-exempt. Check the OSCA ID card and enter its number."
                    : $"PWD discount (RA 10754): {percent:0.##}% off and VAT-exempt. Check the PWD ID card and enter its number.";
            }
        }

        // ------------------------------------------------------------------
        // PhilHealth / HMO coverage and the patient's discount card (filled in automatically)
        // ------------------------------------------------------------------

        private void SetCoverageInputs(string type, string number, string provider, decimal amount)
        {
            bool was = _loading;
            _loading = true;
            try
            {
                cboBillCoverage.SelectedValue = string.IsNullOrEmpty(type) ? BillingRules.CoverageNone : type;
                if (cboBillCoverage.SelectedIndex < 0 && cboBillCoverage.Items.Count > 0) cboBillCoverage.SelectedIndex = 0;
                txtBillCoverageNo.Text = number;
                cboBillHmoProvider.Text = provider;
                txtCoverageAmount.Text = amount == 0m ? string.Empty : amount.ToString("0.00", CultureInfo.InvariantCulture);   // blank = 0.00 (placeholder)
            }
            finally
            {
                _loading = was;
            }
            ApplyCoverageLook();
        }

        private void OnCoverageChanged()
        {
            if (_loading) return;
            if (SelectedId(cboBillCoverage) == BillingRules.CoverageNone)
            {
                _loading = true;
                txtCoverageAmount.Clear();
                _loading = false;
            }
            ApplyCoverageLook();
            MarkDirty();
            Recalculate();
            if (SelectedId(cboBillCoverage) != BillingRules.CoverageNone && txtBillCoverageNo.Enabled)
                (txtBillCoverageNo.Text.Trim().Length == 0 ? txtBillCoverageNo : txtCoverageAmount).Focus();
        }

        /// <summary>No coverage: number and amount are gray; the HMO company appears only for an HMO.</summary>
        private void ApplyCoverageLook()
        {
            string type = SelectedId(cboBillCoverage);
            bool covered = type.Length > 0 && type != BillingRules.CoverageNone;
            ThemeHelper.SetReadOnlyLook(txtBillCoverageNo, !covered || _bill.IsLocked);
            ThemeHelper.SetReadOnlyLook(txtCoverageAmount, !covered || _bill.IsLocked);
            lblBillCoverageNoCap.Text = BenefitRules.CoverageNumberLabel(type) + (covered ? " *" : string.Empty);
            lblCoverageAmountCap.Text = covered ? $"{type} covers (amount) *" : "Covered amount";
            bool hmo = type == BillingRules.CoverageHmo;
            lblBillHmoProviderCap.Visible = hmo;
            cboBillHmoProvider.Visible = hmo;
        }

        /// <summary>
        /// A new bill: when the patient is chosen, the discount card and the PhilHealth / HMO coverage saved in the
        /// patient's record are put on the bill automatically (Senior Citizen / PWD = 20 % and VAT-exempt with the ID number;
        /// PhilHealth / HMO with the member number). The cashier only enters the amount PhilHealth / the HMO covers.
        /// Choosing another patient replaces what was filled in for the previous one.
        /// </summary>
        private void ApplyPatientBenefits()
        {
            if (_bill.IsLocked || _bill.IsSaved) return;

            Patient? patient = null;
            string patientId = SelectedId(cboPatient);
            if (patientId.Length > 0)
            {
                try
                {
                    patient = _patients.GetById(patientId);
                }
                catch (Exception ex)
                {
                    MessageHelper.Error(ex, "The patient's discount card and coverage could not be read.", DialogOwner());
                    return;
                }
            }

            var applied = new List<string>();
            _loading = true;
            try
            {
                // ---- discount card ----
                if (patient != null && patient.HasDiscountCard && BillingRules.IsStatutory(patient.DiscountCardType))
                {
                    cboDiscountType.SelectedValue = patient.DiscountCardType;
                    txtDiscount.Text = BillingRepository.StatutoryPercent().ToString("0.00", CultureInfo.InvariantCulture);
                    txtTaxRate.Text = "0";
                    txtDiscountId.Text = patient.DiscountCardNumber;
                    applied.Add($"{patient.DiscountCardType} discount {BillingRepository.StatutoryPercent():0.##}%, VAT-exempt (ID {patient.DiscountCardNumber})");
                }
                else if (BillingRules.IsStatutory(SelectedId(cboDiscountType)))
                {
                    // the previous patient's Senior / PWD discount does not carry over
                    cboDiscountType.SelectedValue = BillingRules.DiscountFlat;
                    txtDiscount.Text = "0.00";
                    txtTaxRate.Text = FormatPercent(SettingsHelper.GetDecimal(SettingsHelper.DefaultTaxRate, 0.12m));
                    txtDiscountId.Clear();
                }
            }
            finally
            {
                _loading = false;
            }

            // ---- coverage ----
            if (patient != null && patient.HasCoverage)
            {
                SetCoverageInputs(patient.CoverageType, patient.CoverageNumber, patient.CoverageProvider, 0m);
                applied.Add(patient.CoverageType == BillingRules.CoverageHmo
                    ? $"HMO {patient.CoverageProvider} (card {patient.CoverageNumber})"
                    : $"PhilHealth (PIN {patient.CoverageNumber})");
            }
            else
            {
                SetCoverageInputs(BillingRules.CoverageNone, string.Empty, string.Empty, 0m);
            }

            UpdateDiscountCaption();
            ApplyDiscountLook();
            Recalculate();

            if (patient == null)
                lblCoverageMessage.Text = string.Empty;
            else if (applied.Count == 0)
            {
                lblCoverageMessage.ForeColor = ThemeHelper.TextMuted;
                lblCoverageMessage.Text = "No discount card or PhilHealth / HMO on file for this patient. " +
                                          "Add them in Patient Search > Edit Profile if the patient has one.";
            }
            else
            {
                lblCoverageMessage.ForeColor = ThemeHelper.SuccessInk;
                lblCoverageMessage.Text = "Applied from the patient's record: " + string.Join("; ", applied) + "." +
                    (patient.HasCoverage ? $" Enter the amount {patient.CoverageType} covers (approved claim / LOA)." : string.Empty);
            }
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
                DiscountIdNumber = txtDiscountId.Text.Trim(),
                AuthorizedBy = txtAuthorizedBy.Text.Trim(),
                CorrectsBillId = _bill.CorrectsBillId,
                CoverageType = SelectedId(cboBillCoverage) is { Length: > 0 } coverage ? coverage : BillingRules.CoverageNone,
                CoverageNumber = txtBillCoverageNo.Text.Trim(),
                CoverageProvider = cboBillHmoProvider.Text.Trim(),
                Items = _items.Select(i => i.Clone()).ToList()
            };

            string covered = txtCoverageAmount.Text.Trim();
            if (covered.Length == 0 || !bill.HasCoverage) bill.CoverageAmount = 0m;
            else if (ValidationHelper.TryParseDecimal(covered, out decimal coveredValue)) bill.CoverageAmount = coveredValue;
            else issues.Add(new BillingIssue(BillingField.CoverageAmount, "The covered amount must be a number (for example 1500.00)."));

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
            lblGrandTotal.Text = Money(totals.GrossTotal);
            bool showCoverage = bill.HasCoverage;
            lblCoverageDeductCap.Visible = showCoverage;
            lblCoverageDeduct.Visible = showCoverage;
            lblPatientPaysCap.Visible = showCoverage;
            lblPatientPays.Visible = showCoverage;
            lblCoverageDeductCap.Text = showCoverage ? $"Less {bill.CoverageType}" : "Less PhilHealth / HMO";
            lblCoverageDeduct.Text = "- " + Money(totals.CoverageAmount);
            lblPatientPays.Text = Money(totals.GrandTotal);

            lblItemsInfo.Text = _items.Count == 0
                ? "No items yet - add a charge on the left, or use \"Unbilled Charges\"."
                : $"{_items.Count} item(s)   -   subtotal {Money(totals.Subtotal)}" +
                  (_bill.IsLocked ? "" : "   -   click an item to change or remove it");

            // problems that can be shown while typing: the discount and the tax rate
            List<BillingIssue> live = parseIssues
                .Concat(BillingRepository.Validate(bill, finalize: false))
                .Where(i => i.Field == BillingField.Discount || i.Field == BillingField.Tax || i.Field == BillingField.CoverageAmount)
                .ToList();
            bool discountInvalid = live.Any(i => i.Field == BillingField.Discount);

            lblSummaryNote.Text = live.Count > 0 ? live[0].Message : string.Empty;
            lblGrandTotal.ForeColor = live.Count > 0 ? ErrorFore : ThemeHelper.Accent;
            lblPatientPays.ForeColor = live.Count > 0 ? ErrorFore : ThemeHelper.Accent;

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
                case BillingField.DiscountId: return txtDiscountId;
                case BillingField.Coverage: return cboBillCoverage;
                case BillingField.CoverageNumber: return txtBillCoverageNo;
                case BillingField.CoverageProvider: return cboBillHmoProvider;
                case BillingField.CoverageAmount: return txtCoverageAmount;
                default: return txtAuthorizedBy;
            }
        }

        private void Persist(bool finalize)
        {
            if (_bill.IsLocked) return;
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
                    $"Discount:   {Money(totals.DiscountAmount)}" +
                    (BillingRules.IsStatutory(bill.DiscountType) ? $"  ({bill.DiscountType}, ID {bill.DiscountIdNumber}, VAT-exempt)" : string.Empty) + "\n" +
                    $"Tax:           {Money(totals.TaxAmount)}\n" +
                    (bill.HasCoverage
                        ? $"Total:          {Money(totals.GrossTotal)}\n" +
                          $"Less {bill.CoverageType}:  {Money(totals.CoverageAmount)}  ({bill.CoverageNumber})\n" +
                          $"PATIENT PAYS:  {Money(totals.GrandTotal)}\n\n"
                        : $"GRAND TOTAL:  {Money(totals.GrandTotal)}\n\n") +
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
                    MessageHelper.Success($"Bill {saved.BillId} was finalized for {saved.PatientName}.\n\n" +
                                          (saved.HasCoverage ? $"Total: {Money(totals.GrossTotal)}\nLess {saved.CoverageType}: {Money(totals.CoverageAmount)}\nPatient pays: {Money(totals.GrandTotal)}\n\n"
                                                             : $"Grand total: {Money(totals.GrandTotal)}\n\n") +
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
            btnVoidBill.Enabled = row != null && row.Status == BillingRules.StatusFinalized;
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
                SetStatus(bill.IsVoided ? $"Opened voided bill {bill.BillId} (view only)."
                          : bill.IsFinalized ? $"Opened {bill.BillId} (locked, view only)."
                          : $"Opened draft {bill.BillId} - you can keep editing it.",
                    ThemeHelper.TextMuted);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The bill could not be opened.", DialogOwner());
            }
        }

        /// <summary>
        /// Voids the selected finalized bill: blocked while money is still paid on it (refund first), asks for the reason,
        /// and can open a corrected draft copy right away.
        /// </summary>
        private void VoidSelectedBill()
        {
            BillListRow? row = SelectedRow();
            if (row == null || row.Status != BillingRules.StatusFinalized) return;

            string? blocker = BillingRepository.VoidBlocker(row.Status, row.AmountPaid);
            if (blocker != null)
            {
                MessageHelper.Warning(blocker, "The bill cannot be voided yet", DialogOwner());
                return;
            }
            if (!ConfirmDiscardChanges()) return;

            using var dialog = new VoidBillDialog(row.BillId, row.PatientName, row.GrandTotal);
            if (dialog.ShowDialog(DialogOwner()) != DialogResult.OK) return;

            try
            {
                string? draftId = _repository.VoidBill(row.BillId, dialog.Reason, dialog.CreateCorrection);
                if (_bill.BillId == row.BillId) StartNewBill();
                LoadBills();

                if (draftId != null)
                {
                    Bill? draft = _repository.GetBill(draftId);
                    if (draft != null)
                    {
                        LoadBillIntoScreen(draft);
                        ShowMode(ViewMode.Builder);
                    }
                    SetStatus($"Voided {row.BillId}. Corrected copy {draftId} is open as a draft - fix it, then finalize it.", ThemeHelper.Success);
                }
                else
                {
                    SetStatus($"Voided {row.BillId}. Its linked charges are unbilled again.", ThemeHelper.Success);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Itemized Billing", DialogOwner());
                LoadBills();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The bill could not be voided.", DialogOwner());
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