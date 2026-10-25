using System.Globalization;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP12_Payment;
using HospitalManagementSystem.Repositories.MP12_Payment;

namespace HospitalManagementSystem.Views.MP12_Payment
{
    /// <summary>
    /// MP12 (Step 16) - Payment Processing and Receipts. Two views, switched with the buttons at the top:
    ///
    ///   RECORD PAYMENT
    ///     left:  the finalized bills that still have a balance (search by bill code, patient name or PAT- code) and,
    ///            under it, the payments already made on the selected bill;
    ///     right: the selected bill (total, paid so far, BALANCE, status) and the payment form:
    ///            amount (or "Pay full balance"), method, reference number (required for Debit / Credit / Online),
    ///            cash tendered (cash only) with the change shown live, and the cashier who receives the payment.
    ///     A payment can be made in parts. The amount can never be more than the balance. After saving, the Official Receipt
    ///     opens (payment no., bill, patient, amount, method, previous payments, new balance, cashier, date).
    ///     STATUS TRANSITION (derived, never stored): Outstanding -> Partially Paid -> Fully Paid.
    ///
    ///   BILLING RECORDS
    ///     every finalized bill with its totals and payment status, filtered by search text, bill date range and status;
    ///     select a bill to see its payments and reprint any receipt.
    ///
    /// The rules live in PaymentRepository.Validate (and are checked again inside the save transaction); this form shows the
    /// same messages next to the fields. All visuals live in PaymentForm.Designer.cs; the grid columns are created here.
    /// </summary>
    public partial class PaymentForm : Form
    {
        private static readonly Color ErrorFore = ThemeHelper.Danger;
        private static readonly Color MutedFore = ThemeHelper.TextMuted;
        private static readonly Color AccentFore = ThemeHelper.Accent;
        private static readonly Color OkFore = ThemeHelper.Success;

        private enum ViewMode { Pay, Records }

        private readonly PaymentRepository _repository = new PaymentRepository();
        private readonly FormValidator _validator;

        private List<PaymentMethod> _methods = new List<PaymentMethod>();
        private List<PayableBill> _payable = new List<PayableBill>();
        private List<PaymentRecord> _history = new List<PaymentRecord>();
        private List<PayableBill> _records = new List<PayableBill>();
        private List<PaymentRecord> _recHistory = new List<PaymentRecord>();
        private PayableBill? _selected;
        private ViewMode _mode = ViewMode.Pay;
        private bool _loading;
        private bool _initialized;

        public PaymentForm()
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(dgvPayable, txtAmount, cboMethod, txtReference, txtTendered);

            BuildGridColumns();
            GridHelper.Apply(dgvPayable);
            GridHelper.Apply(dgvHistory);
            GridHelper.Apply(dgvRecords);
            GridHelper.Apply(dgvRecHistory);
            GridHelper.EnableStatusColoring(dgvPayable, "colPStatus");
            GridHelper.EnableStatusColoring(dgvRecords, "colRStatus");

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
        /// Creates the columns of the four grids. They are built here, not in the Designer, because Visual Studio's
        /// [Design] view deletes designer-made grid columns when it re-saves a large designer file.
        /// </summary>
        private void BuildGridColumns()
        {
            dgvPayable.AutoGenerateColumns = false;
            dgvHistory.AutoGenerateColumns = false;
            dgvRecords.AutoGenerateColumns = false;
            dgvRecHistory.AutoGenerateColumns = false;

            AddColumn(dgvPayable, "colPCode", "Bill ID", nameof(PayableBill.BillId), 19);
            AddColumn(dgvPayable, "colPPatient", "Patient", nameof(PayableBill.PatientName), 25);
            AddColumn(dgvPayable, "colPTotal", "Total", nameof(PayableBill.TotalText), 14, true);
            AddColumn(dgvPayable, "colPPaid", "Paid", nameof(PayableBill.PaidText), 14, true);
            AddColumn(dgvPayable, "colPBalance", "Balance", nameof(PayableBill.BalanceText), 14, true);
            AddColumn(dgvPayable, "colPStatus", "Status", nameof(PayableBill.PaymentStatus), 16);

            AddColumn(dgvHistory, "colHCode", "Receipt no.", nameof(PaymentRecord.PaymentId), 26);
            AddColumn(dgvHistory, "colHDate", "Paid on", nameof(PaymentRecord.PaidAtText), 28);
            AddColumn(dgvHistory, "colHMethod", "Method", nameof(PaymentRecord.MethodName), 14);
            AddColumn(dgvHistory, "colHReference", "Reference", nameof(PaymentRecord.ReferenceText), 16);
            AddColumn(dgvHistory, "colHAmount", "Amount", nameof(PaymentRecord.AmountText), 16, true);

            AddColumn(dgvRecords, "colRCode", "Bill ID", nameof(PayableBill.BillId), 17);
            AddColumn(dgvRecords, "colRPatient", "Patient", nameof(PayableBill.PatientName), 24);
            AddColumn(dgvRecords, "colRDate", "Bill date", nameof(PayableBill.DateText), 14);
            AddColumn(dgvRecords, "colRTotal", "Grand total", nameof(PayableBill.TotalText), 13, true);
            AddColumn(dgvRecords, "colRPaid", "Amount paid", nameof(PayableBill.PaidText), 13, true);
            AddColumn(dgvRecords, "colRBalance", "Balance", nameof(PayableBill.BalanceText), 13, true);
            AddColumn(dgvRecords, "colRStatus", "Payment status", nameof(PayableBill.PaymentStatus), 14);

            AddColumn(dgvRecHistory, "colQCode", "Receipt no.", nameof(PaymentRecord.PaymentId), 18);
            AddColumn(dgvRecHistory, "colQDate", "Paid on", nameof(PaymentRecord.PaidAtText), 20);
            AddColumn(dgvRecHistory, "colQMethod", "Method", nameof(PaymentRecord.MethodName), 11);
            AddColumn(dgvRecHistory, "colQReference", "Reference", nameof(PaymentRecord.ReferenceText), 15);
            AddColumn(dgvRecHistory, "colQAmount", "Amount", nameof(PaymentRecord.AmountText), 12, true);
            AddColumn(dgvRecHistory, "colQTendered", "Tendered", nameof(PaymentRecord.TenderedText), 12, true);
            AddColumn(dgvRecHistory, "colQReceivedBy", "Received by", nameof(PaymentRecord.ReceivedBy), 18);
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
            InputHelper.Attach(txtBillSearch, InputKind.Address, 60);
            InputHelper.Attach(txtAmount, InputKind.Decimal2, 12);
            InputHelper.Attach(txtReference, InputKind.Address, PaymentRules.MaxReference);
            InputHelper.Attach(txtTendered, InputKind.Decimal2, 12);
            InputHelper.Attach(txtRecSearch, InputKind.Address, 60);
        }

        private void WireEvents()
        {
            btnModePay.Click += (s, e) => ShowMode(ViewMode.Pay);
            btnModeRecords.Click += (s, e) => ShowMode(ViewMode.Records);

            // bills waiting for payment
            btnBillSearch.Click += (s, e) => LoadPayable(_selected?.BillId);
            btnBillAll.Click += (s, e) => { txtBillSearch.Clear(); LoadPayable(_selected?.BillId); };
            txtBillSearch.KeyDown += (s, e) => OnEnter(e, () => LoadPayable(_selected?.BillId));
            dgvPayable.SelectionChanged += (s, e) => { if (!_loading) OnPayableSelected(); };
            dgvHistory.SelectionChanged += (s, e) => btnViewReceipt.Enabled = dgvHistory.CurrentRow?.DataBoundItem is PaymentRecord;
            dgvHistory.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) ViewReceipt(dgvHistory); };
            btnViewReceipt.Click += (s, e) => ViewReceipt(dgvHistory);

            // the payment
            btnPayFull.Click += (s, e) => PayFullBalance();
            cboMethod.SelectedIndexChanged += (s, e) => { if (!_loading) { ApplyMethodState(); UpdatePreview(); } };
            txtAmount.TextChanged += (s, e) => { if (!_loading) UpdatePreview(); };
            txtTendered.TextChanged += (s, e) => { if (!_loading) UpdatePreview(); };
            btnRecord.Click += (s, e) => Record();
            btnClearPay.Click += (s, e) => ClearPayInputs();

            // billing records
            btnRecSearch.Click += (s, e) => LoadRecords();
            btnRecAll.Click += (s, e) => ResetRecordFilters();
            txtRecSearch.KeyDown += (s, e) => OnEnter(e, () => LoadRecords());
            cboRecStatus.SelectedIndexChanged += (s, e) => { if (!_loading && _initialized) LoadRecords(); };
            dgvRecords.SelectionChanged += (s, e) => { if (!_loading) OnRecordSelected(); };
            dgvRecHistory.SelectionChanged += (s, e) => btnReprint.Enabled = dgvRecHistory.CurrentRow?.DataBoundItem is PaymentRecord;
            dgvRecHistory.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) ViewReceipt(dgvRecHistory); };
            btnReprint.Click += (s, e) => ViewReceipt(dgvRecHistory);

            VisibleChanged += (s, e) =>
            {
                if (!Visible || !_initialized) return;
                if (_mode == ViewMode.Pay) LoadPayable(_selected?.BillId);
                else LoadRecords();
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
                          "  ·  Record payments on finalized bills and print receipts.";
            txtReceivedBy.Text = SessionManager.CurrentUserName;

            _loading = true;
            try
            {
                _methods = _repository.GetMethods();
                var list = new List<PaymentMethod> { new PaymentMethod { Name = "Select the method..." } };
                list.AddRange(_methods);
                cboMethod.DataSource = list;
                cboMethod.DisplayMember = nameof(PaymentMethod.Name);
                cboMethod.ValueMember = nameof(PaymentMethod.Id);
                cboMethod.SelectedIndex = 0;

                cboRecStatus.Items.Clear();
                cboRecStatus.Items.AddRange(new object[] { "All statuses", "Outstanding (unpaid)", "Partially Paid", "Fully Paid" });
                cboRecStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The payment methods could not be loaded.", DialogOwner());
            }
            finally
            {
                _loading = false;
            }

            _initialized = true;
            ClearPayInputs();
            ShowMode(ViewMode.Pay);
        }

        private void SetStatus(string text, Color color)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = color;
        }

        private void ShowMode(ViewMode mode)
        {
            _mode = mode;
            pnlPay.Visible = mode == ViewMode.Pay;
            pnlRecords.Visible = mode == ViewMode.Records;
            ThemeHelper.StyleButton(btnModePay, mode == ViewMode.Pay ? ButtonKind.Primary : ButtonKind.Secondary);
            ThemeHelper.StyleButton(btnModeRecords, mode == ViewMode.Records ? ButtonKind.Primary : ButtonKind.Secondary);

            if (!_initialized) return;
            if (mode == ViewMode.Pay) LoadPayable(_selected?.BillId);
            else LoadRecords();
        }

        // ------------------------------------------------------------------
        // Record payment: the bill list and the selected bill
        // ------------------------------------------------------------------

        private void LoadPayable(string? keepBillId)
        {
            try
            {
                _payable = _repository.GetPayableBills(txtBillSearch.Text.Trim());

                _loading = true;
                dgvPayable.DataSource = null;
                dgvPayable.DataSource = _payable;
                dgvPayable.ClearSelection();

                int index = keepBillId != null ? _payable.FindIndex(b => b.BillId == keepBillId) : -1;
                if (index >= 0)
                {
                    dgvPayable.Rows[index].Selected = true;
                    dgvPayable.CurrentCell = GridHelper.FirstVisibleCell(dgvPayable.Rows[index]);
                }
                else
                {
                    dgvPayable.CurrentCell = null;
                }
                _loading = false;

                lblPayableCount.Text = _payable.Count == 0
                    ? "No finalized bill is waiting for payment."
                    : $"{_payable.Count} bill(s) with a balance" + (_payable.Count >= PaymentRules.PayableLimit ? " (showing the first " + PaymentRules.PayableLimit + ")" : string.Empty) +
                      "  -  total balance " + Money(_payable.Sum(b => b.Balance));

                OnPayableSelected();
            }
            catch (Exception ex)
            {
                _loading = false;
                MessageHelper.Error(ex, "The bills could not be loaded.", DialogOwner());
            }
        }

        private void OnPayableSelected()
        {
            var bill = dgvPayable.CurrentRow?.DataBoundItem as PayableBill;
            bool sameBill = bill != null && _selected != null && bill.BillId == _selected.BillId;
            _selected = bill;
            ShowBillInfo(bill);
            if (!sameBill) ClearPayInputs();
            btnRecord.Enabled = bill != null;
            btnPayFull.Enabled = bill != null;
            LoadHistory();
            UpdatePreview();
        }

        private void ShowBillInfo(PayableBill? bill)
        {
            if (bill == null)
            {
                lblInfoBill.Text = "-";
                lblInfoPatient.Text = "-";
                lblInfoTotal.Text = "-";
                lblInfoPaid.Text = "-";
                lblInfoBalance.Text = "-";
                lblInfoStatus.Text = "-";
                lblInfoStatus.ForeColor = ThemeHelper.TextPrimary;
                return;
            }

            lblInfoBill.Text = bill.BillId;
            lblInfoPatient.Text = bill.PatientName + "  (" + bill.PatientId + ")";
            lblInfoTotal.Text = Money(bill.GrandTotal);
            lblInfoPaid.Text = Money(bill.AmountPaid);
            lblInfoBalance.Text = Money(bill.Balance);
            lblInfoStatus.Text = bill.PaymentStatus;
            lblInfoStatus.ForeColor = ThemeHelper.TryGetStatusColors(bill.PaymentStatus, out Color _, out Color fore) ? fore : ThemeHelper.TextPrimary;
        }

        private void LoadHistory()
        {
            try
            {
                _history = _selected == null ? new List<PaymentRecord>() : _repository.GetPayments(_selected.BillId);
                dgvHistory.DataSource = null;
                dgvHistory.DataSource = _history;
                dgvHistory.ClearSelection();
                if (_history.Count > 0) dgvHistory.CurrentCell = GridHelper.FirstVisibleCell(dgvHistory.Rows[_history.Count - 1]);
                btnViewReceipt.Enabled = _history.Count > 0;

                lblHistoryInfo.Text = _selected == null
                    ? "Select a bill on the list."
                    : _history.Count == 0
                        ? "No payment has been made on this bill yet."
                        : $"{_history.Count} payment(s)  -  paid {Money(_history.Sum(p => p.Amount))}";
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The payments of the bill could not be loaded.", DialogOwner());
            }
        }

        // ------------------------------------------------------------------
        // The payment form
        // ------------------------------------------------------------------

        private PaymentMethod? SelectedMethod() => cboMethod.SelectedItem is PaymentMethod m && m.Id.Length > 0 ? m : null;

        private static bool TryAmount(string text, out decimal value)
        {
            value = 0;
            return !string.IsNullOrWhiteSpace(text) && ValidationHelper.TryParseDecimal(text.Trim(), out value);
        }

        private void ClearPayInputs()
        {
            _loading = true;
            try
            {
                txtAmount.Clear();
                if (cboMethod.Items.Count > 0) cboMethod.SelectedIndex = 0;
                txtReference.Clear();
                txtTendered.Clear();
            }
            finally
            {
                _loading = false;
            }
            _validator.Begin();
            ApplyMethodState();
            UpdatePreview();
        }

        /// <summary>Reference number: needed for Debit / Credit / Online, optional for Cheque, off for Cash. Tendered: cash only.</summary>
        private void ApplyMethodState()
        {
            PaymentMethod? method = SelectedMethod();
            bool cash = method != null && method.Id == PaymentRules.MethodCash;
            bool referenceOn = method != null && !cash;

            txtReference.Enabled = referenceOn;
            if (!referenceOn) txtReference.Clear();
            lblReference.Text = method != null && method.RequiresReference ? "Reference no. *" : "Reference no.";
            txtReference.PlaceholderText = method == null ? "Choose the method first"
                : cash ? "Not used for cash"
                : method.RequiresReference ? "Required for " + method.Name
                : "Optional";

            txtTendered.Enabled = cash;
            if (!cash) txtTendered.Clear();
            txtTendered.PlaceholderText = cash ? "Cash given by the patient (optional)" : "Cash payments only";
        }

        private void PayFullBalance()
        {
            if (_selected == null) return;
            txtAmount.Text = _selected.Balance.ToString("0.00", CultureInfo.InvariantCulture);
            txtAmount.Focus();
        }

        /// <summary>Live preview: the balance left after this payment and the change to give back.</summary>
        private void UpdatePreview()
        {
            bool amountOk = TryAmount(txtAmount.Text, out decimal amount) && amount > 0;

            if (_selected == null)
            {
                lblPayNote.Text = "Select a bill to pay.";
                lblPayNote.ForeColor = MutedFore;
            }
            else if (!amountOk)
            {
                lblPayNote.Text = "Balance to pay: " + Money(_selected.Balance);
                lblPayNote.ForeColor = MutedFore;
            }
            else if (amount > _selected.Balance)
            {
                lblPayNote.Text = "The amount is more than the balance (" + Money(_selected.Balance) + ").";
                lblPayNote.ForeColor = ErrorFore;
            }
            else if (amount == _selected.Balance)
            {
                lblPayNote.Text = "This payment settles the bill (Fully Paid).";
                lblPayNote.ForeColor = OkFore;
            }
            else
            {
                lblPayNote.Text = "Balance after this payment: " + Money(_selected.Balance - amount) + " (Partially Paid).";
                lblPayNote.ForeColor = MutedFore;
            }

            if (txtTendered.Enabled && amountOk && TryAmount(txtTendered.Text, out decimal tendered) && tendered >= amount)
                lblChange.Text = Money(tendered - amount);
            else
                lblChange.Text = "-";
        }

        private Control ControlFor(PaymentField field) => field switch
        {
            PaymentField.Bill => dgvPayable,
            PaymentField.Amount => txtAmount,
            PaymentField.Method => cboMethod,
            PaymentField.Reference => txtReference,
            _ => txtTendered
        };

        private void Record()
        {
            if (_selected == null)
            {
                MessageHelper.Warning("Select the bill to pay on the list first.", "Record payment", DialogOwner());
                return;
            }

            _validator.Begin();
            PaymentMethod? method = SelectedMethod();
            var input = new PaymentInput
            {
                BillId = _selected.BillId,
                Method = method?.Id ?? string.Empty,
                ReferenceNumber = txtReference.Text.Trim()
            };

            bool amountParsed = false;
            string amountText = txtAmount.Text.Trim();
            if (amountText.Length == 0) _validator.AddError(txtAmount, "Enter the amount to pay.");
            else if (!TryAmount(amountText, out decimal amount)) _validator.AddError(txtAmount, "The amount must be a number.");
            else
            {
                amountParsed = true;
                input.Amount = amount;
            }

            string tenderedText = txtTendered.Text.Trim();
            bool tenderedParsed = true;
            if (method != null && method.Id == PaymentRules.MethodCash && tenderedText.Length > 0)
            {
                if (TryAmount(tenderedText, out decimal tendered)) input.Tendered = tendered;
                else
                {
                    tenderedParsed = false;
                    _validator.AddError(txtTendered, "The cash tendered must be a number.");
                }
            }

            foreach (PaymentIssue issue in PaymentRepository.Validate(input, _selected, method))
            {
                if (issue.Field == PaymentField.Amount && !amountParsed) continue;
                if (issue.Field == PaymentField.Tendered && !tenderedParsed) continue;
                _validator.AddError(ControlFor(issue.Field), issue.Message);
            }

            if (!_validator.IsValid)
            {
                _validator.ShowErrors(DialogOwner());
                return;
            }

            string change = input.Tendered.HasValue ? "\nChange to give back: " + Money(input.Tendered.Value - input.Amount) : string.Empty;
            string question = $"Record a payment of {Money(input.Amount)} by {method!.Name}\n" +
                              $"for bill {_selected.BillId} ({_selected.PatientName})?\n\n" +
                              $"Balance after this payment: {Money(_selected.Balance - input.Amount)}" + change;
            if (!MessageHelper.Confirm(question, "Confirm payment", DialogOwner())) return;

            string billId = _selected.BillId;
            Receipt receipt;
            try
            {
                Cursor = Cursors.WaitCursor;
                receipt = _repository.RecordPayment(input);
            }
            catch (InvalidOperationException ex)
            {
                Cursor = Cursors.Default;
                MessageHelper.Warning(ex.Message, "The payment was not recorded", DialogOwner());
                LoadPayable(billId);
                return;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageHelper.Error(ex, "The payment could not be saved.", DialogOwner());
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            ClearPayInputs();
            LoadPayable(billId);
            SetStatus($"Payment {receipt.PaymentId} of {Money(receipt.AmountPaid)} recorded for {receipt.BillId}  -  {receipt.StatusAfter}.", OkFore);
            ShowReceipt(receipt);
        }

        private void ShowReceipt(Receipt receipt)
        {
            using var dialog = new ReceiptDialog(receipt);
            dialog.ShowDialog(DialogOwner());
        }

        private void ViewReceipt(DataGridView grid)
        {
            if (grid.CurrentRow?.DataBoundItem is not PaymentRecord payment) return;
            try
            {
                Receipt? receipt = _repository.GetReceipt(payment.PaymentId);
                if (receipt == null)
                {
                    MessageHelper.Warning("This receipt could not be found.", "Receipt", DialogOwner());
                    return;
                }
                ShowReceipt(receipt);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The receipt could not be loaded.", DialogOwner());
            }
        }

        // ------------------------------------------------------------------
        // Billing records
        // ------------------------------------------------------------------

        private string? StatusFilter() => cboRecStatus.SelectedIndex switch
        {
            1 => PaymentRules.Outstanding,
            2 => PaymentRules.PartiallyPaid,
            3 => PaymentRules.FullyPaid,
            _ => null
        };

        private void ResetRecordFilters()
        {
            _loading = true;
            txtRecSearch.Clear();
            dtpRecFrom.Checked = false;
            dtpRecTo.Checked = false;
            cboRecStatus.SelectedIndex = 0;
            _loading = false;
            LoadRecords();
        }

        private void LoadRecords()
        {
            DateTime? from = dtpRecFrom.Checked ? dtpRecFrom.Value.Date : null;
            DateTime? to = dtpRecTo.Checked ? dtpRecTo.Value.Date : null;
            if (from.HasValue && to.HasValue && from.Value > to.Value)
            {
                MessageHelper.Warning("The \"from\" date cannot be after the \"to\" date.", "Billing records", DialogOwner());
                return;
            }

            string? keep = (dgvRecords.CurrentRow?.DataBoundItem as PayableBill)?.BillId;
            try
            {
                _records = _repository.GetRecords(txtRecSearch.Text.Trim(), from, to, StatusFilter());

                _loading = true;
                dgvRecords.DataSource = null;
                dgvRecords.DataSource = _records;
                dgvRecords.ClearSelection();
                int index = keep != null ? _records.FindIndex(b => b.BillId == keep) : -1;
                if (index >= 0)
                {
                    dgvRecords.Rows[index].Selected = true;
                    dgvRecords.CurrentCell = GridHelper.FirstVisibleCell(dgvRecords.Rows[index]);
                }
                else
                {
                    dgvRecords.CurrentCell = null;
                }
                _loading = false;

                lblRecCount.Text = _records.Count == 0
                    ? "No finalized bill matches the filters."
                    : $"{_records.Count} bill(s)" + (_records.Count >= PaymentRules.ListLimit ? " (showing the first " + PaymentRules.ListLimit + ")" : string.Empty) +
                      $"  -  billed {Money(_records.Sum(b => b.GrandTotal))}  -  paid {Money(_records.Sum(b => b.AmountPaid))}  -  balance {Money(_records.Sum(b => b.Balance))}";

                OnRecordSelected();
            }
            catch (Exception ex)
            {
                _loading = false;
                MessageHelper.Error(ex, "The billing records could not be loaded.", DialogOwner());
            }
        }

        private void OnRecordSelected()
        {
            var bill = dgvRecords.CurrentRow?.DataBoundItem as PayableBill;
            try
            {
                _recHistory = bill == null ? new List<PaymentRecord>() : _repository.GetPayments(bill.BillId);
                dgvRecHistory.DataSource = null;
                dgvRecHistory.DataSource = _recHistory;
                dgvRecHistory.ClearSelection();
                if (_recHistory.Count > 0) dgvRecHistory.CurrentCell = GridHelper.FirstVisibleCell(dgvRecHistory.Rows[_recHistory.Count - 1]);
                btnReprint.Enabled = _recHistory.Count > 0;

                lblRecHistoryInfo.Text = bill == null
                    ? "Select a bill to see its payments."
                    : _recHistory.Count == 0
                        ? bill.BillId + ": no payment has been made yet."
                        : $"{bill.BillId}: {_recHistory.Count} payment(s), paid {Money(bill.AmountPaid)} of {Money(bill.GrandTotal)}.";
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The payments of the bill could not be loaded.", DialogOwner());
            }
        }
    }
}