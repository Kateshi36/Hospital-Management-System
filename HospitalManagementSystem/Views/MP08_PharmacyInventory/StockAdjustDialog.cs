using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP08_MedicineStock;
using System.Globalization;

namespace HospitalManagementSystem.Views.MP08_MedicineStock
{
    /// <summary>
    /// MP08 - collects a stock correction against one batch (damage, loss, expired-stock disposal, count correction).
    /// It saves nothing itself: the caller reads the properties and calls MedicineStockRepository.AdjustStock.
    /// Rules: batch chosen, quantity 1 or more, reason required, and a decrease can never be more than what the batch still has.
    /// All visuals live in StockAdjustDialog.Designer.cs; this file only holds behaviour.
    /// </summary>
    public partial class StockAdjustDialog : Form
    {
        private const string Decrease = "Decrease";
        private const string Increase = "Increase";

        private readonly FormValidator _validator;
        private readonly List<BatchRecord> _batches;

        public int BatchId { get; private set; }

        /// <summary>Negative for a decrease, positive for an increase.</summary>
        public int QuantityChange { get; private set; }

        public string Reason { get; private set; } = string.Empty;

        public StockAdjustDialog(string medicineName, List<BatchRecord> batches)
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon
            _batches = batches;

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(cboBatch, cboDirection, txtQuantity, txtReason);

            InputHelper.Attach(txtQuantity, InputKind.Digits, 7);
            InputHelper.Attach(txtReason, InputKind.Address, MedicineStockRules.MaxReason);

            lblInfo.Text = "Medicine: " + medicineName + "\nA decrease can never take a batch below zero. Every adjustment is saved with its reason.";

            cboBatch.DataSource = batches;
            cboBatch.DisplayMember = nameof(BatchRecord.Display);
            cboBatch.ValueMember = nameof(BatchRecord.BatchId);

            // Start on the batch that would be used first (FIFO); otherwise the first batch.
            int first = batches.FindIndex(b => b.DispenseOrder == 1);
            cboBatch.SelectedIndex = first >= 0 ? first : 0;

            cboDirection.DataSource = new List<LookupItem>
            {
                new LookupItem { Id = Decrease, Name = "Decrease - damage, loss or expired stock disposal" },
                new LookupItem { Id = Increase, Name = "Increase - count correction" }
            };
            cboDirection.DisplayMember = nameof(LookupItem.Name);
            cboDirection.ValueMember = nameof(LookupItem.Id);
            cboDirection.SelectedIndex = 0;

            btnConfirm.Click += (s, e) => Confirm();
            btnCancelDialog.Click += (s, e) => DialogResult = DialogResult.Cancel;
            Shown += (s, e) => txtQuantity.Focus();
        }

        private void Confirm()
        {
            _validator.Begin();

            BatchRecord? batch = cboBatch.SelectedItem as BatchRecord;
            _validator.Check(batch != null, cboBatch, "Choose the batch to adjust.");

            bool decrease = cboDirection.SelectedValue is string d && d == Decrease;

            int quantity = 0;
            string quantityText = txtQuantity.Text.Trim();
            if (quantityText.Length == 0) _validator.AddError(txtQuantity, "Enter the quantity.");
            else if (!int.TryParse(quantityText, NumberStyles.None, CultureInfo.InvariantCulture, out quantity) || quantity < 1 || quantity > MedicineStockRules.QuantityMax)
                _validator.AddError(txtQuantity, $"The quantity must be a whole number from 1 to {MedicineStockRules.QuantityMax:N0}.");
            else if (decrease && batch != null && quantity > batch.QuantityRemaining)
                _validator.AddError(txtQuantity, $"Batch {batch.BatchNumber} has only {batch.QuantityRemaining} left. Stock can never go below zero.");

            string reason = txtReason.Text.Trim();
            _validator.Check(reason.Length > 0, txtReason, "Enter the reason for the adjustment.");

            if (!_validator.IsValid)
            {
                _validator.ShowErrors(this);
                return;
            }

            BatchId = batch!.BatchId;
            QuantityChange = decrease ? -quantity : quantity;
            Reason = reason;
            DialogResult = DialogResult.OK;
        }
    }
}