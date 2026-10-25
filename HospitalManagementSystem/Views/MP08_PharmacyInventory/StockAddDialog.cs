using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP08_MedicineStock;
using System.Globalization;

namespace HospitalManagementSystem.Views.MP08_MedicineStock
{
    /// <summary>
    /// MP08 - collects the data of a new stock batch. It saves nothing itself: the caller reads the properties and calls
    /// MedicineStockRepository.AddStock. Rules: batch number required, quantity 1 or more, expiry date mandatory and in
    /// the future, supplier chosen, purchase price above 0 with at most 2 decimals.
    /// All visuals live in StockAddDialog.Designer.cs; this file only holds behaviour.
    /// </summary>
    public partial class StockAddDialog : Form
    {
        private readonly FormValidator _validator;

        public string BatchNumber { get; private set; } = string.Empty;
        public int Quantity { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public string Supplier { get; private set; } = string.Empty;
        public decimal PurchasePrice { get; private set; }

        public StockAddDialog(string medicineName, List<LookupItem> suppliers)
        {
            InitializeComponent();
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtBatch, txtQuantity, dtpExpiry, cboSupplier, txtPrice);

            InputHelper.Attach(txtBatch, InputKind.Code, MedicineStockRules.MaxBatchNumber);
            InputHelper.Attach(txtQuantity, InputKind.Digits, 7);
            InputHelper.Attach(txtPrice, InputKind.Decimal2, 10);

            lblInfo.Text = "Medicine: " + medicineName + "\nThe quantity is added to the stock of this medicine as a new batch.";

            // Pick a supplier used before, or type a new one.
            cboSupplier.DataSource = suppliers;
            cboSupplier.DisplayMember = nameof(LookupItem.Name);
            cboSupplier.ValueMember = nameof(LookupItem.Id);
            cboSupplier.SelectedIndex = -1;
            cboSupplier.MaxLength = MedicineStockRules.MaxSupplier;

            // The expiry date must be in the future: tomorrow is the earliest choice.
            dtpExpiry.MinDate = DateTime.Today.AddDays(1);
            dtpExpiry.MaxDate = DateTime.Today.AddYears(MedicineStockRules.ExpiryMaxYears);
            dtpExpiry.Value = DateTime.Today.AddYears(1);

            btnConfirm.Click += (s, e) => Confirm();
            btnCancelDialog.Click += (s, e) => DialogResult = DialogResult.Cancel;
            Shown += (s, e) => txtBatch.Focus();
        }

        private void Confirm()
        {
            _validator.Begin();

            string batch = txtBatch.Text.Trim();
            _validator.Check(batch.Length > 0, txtBatch, "Enter the batch number.");

            int quantity = 0;
            string quantityText = txtQuantity.Text.Trim();
            if (quantityText.Length == 0) _validator.AddError(txtQuantity, "Enter the quantity received.");
            else if (!int.TryParse(quantityText, NumberStyles.None, CultureInfo.InvariantCulture, out quantity) || quantity < 1 || quantity > MedicineStockRules.QuantityMax)
                _validator.AddError(txtQuantity, $"The quantity must be a whole number from 1 to {MedicineStockRules.QuantityMax:N0}.");

            _validator.Check(dtpExpiry.Value.Date > DateTime.Today, dtpExpiry, "The expiry date must be a future date.");
            _validator.Check(cboSupplier.Text.Trim().Length > 0, cboSupplier, "Choose or type the supplier.");

            decimal price = 0;
            string priceText = txtPrice.Text.Trim();
            if (priceText.Length == 0) _validator.AddError(txtPrice, "Enter the purchase price.");
            else if (!ValidationHelper.TryParseDecimal(priceText, out price)) _validator.AddError(txtPrice, "The purchase price must be a number.");
            else if (price <= 0 || price > MedicineStockRules.PriceMax) _validator.AddError(txtPrice, $"The purchase price must be greater than 0 and at most {MedicineStockRules.PriceMax:N0}.");
            else if (Math.Round(price, 2) != price) _validator.AddError(txtPrice, "The purchase price can have at most 2 decimal places.");

            if (!_validator.IsValid)
            {
                _validator.ShowErrors(this);
                return;
            }

            BatchNumber = batch;
            Quantity = quantity;
            ExpiryDate = dtpExpiry.Value.Date;
            Supplier = cboSupplier.Text.Trim();
            PurchasePrice = price;
            DialogResult = DialogResult.OK;
        }
    }
}