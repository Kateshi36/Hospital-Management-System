using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP11_Billing;

namespace HospitalManagementSystem.Views.MP11_Billing
{
    /// <summary>
    /// MP11 - the patient's charges recorded in other modules that are not on a bill yet: consultation fees (MP06),
    /// medicines dispensed with "Charge to Bill" (MP09) and lab orders (MP10). Every charge starts ticked; click a row
    /// (or press Space) to tick / untick it. "Add Selected" returns the ticked ones in <see cref="Selected"/>;
    /// the bill keeps their link, so the same service can never be billed twice. All visuals live in UnbilledChargesDialog.Designer.cs.
    /// </summary>
    public partial class UnbilledChargesDialog : Form
    {
        private readonly List<UnbilledCharge> _charges;
        private readonly bool[] _ticked;
        private bool _syncing;

        /// <summary>The ticked charges (valid only when the dialog result is OK).</summary>
        public List<UnbilledCharge> Selected { get; private set; } = new List<UnbilledCharge>();

        public UnbilledChargesDialog(string patientName, List<UnbilledCharge> charges)
        {
            InitializeComponent();
            FormHelper.Prepare(this);
            _charges = charges;
            _ticked = Enumerable.Repeat(true, charges.Count).ToArray();

            lblTitle.Text = "Unbilled charges";
            lblInfo.Text = $"{patientName}  ·  consultations, medicines charged to the bill and lab orders that are not on any bill yet. " +
                           "Untick a charge that should not be billed now.";

            BuildColumns();
            GridHelper.Apply(dgvCharges);
            dgvCharges.DataSource = _charges;
            dgvCharges.DataBindingComplete += (s, e) => RefreshTicks();
            dgvCharges.CellClick += (s, e) => { if (e.RowIndex >= 0) Toggle(e.RowIndex); };
            dgvCharges.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Space || dgvCharges.CurrentRow == null) return;
                e.Handled = true;
                Toggle(dgvCharges.CurrentRow.Index);
            };

            chkAll.Checked = true;
            chkAll.CheckedChanged += (s, e) =>
            {
                if (_syncing) return;
                for (int i = 0; i < _ticked.Length; i++) _ticked[i] = chkAll.Checked;
                RefreshTicks();
            };
            btnAdd.Click += (s, e) => Confirm();
            btnCancelDialog.Click += (s, e) => DialogResult = DialogResult.Cancel;
            CancelButton = btnCancelDialog;
        }

        private void BuildColumns()
        {
            dgvCharges.AutoGenerateColumns = false;
            var tick = new DataGridViewCheckBoxColumn
            {
                Name = "colTick",
                HeaderText = "Add",
                FillWeight = 7,
                MinimumWidth = 6,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            dgvCharges.Columns.Add(tick);
            GridHelper.AddTextColumn(dgvCharges, "colDate", "Date", nameof(UnbilledCharge.DateText), 19);
            GridHelper.AddTextColumn(dgvCharges, "colCategory", "Category", nameof(UnbilledCharge.Category), 13);
            GridHelper.AddTextColumn(dgvCharges, "colDescription", "Description", nameof(UnbilledCharge.Description), 37);
            GridHelper.AddTextColumn(dgvCharges, "colQty", "Qty", nameof(UnbilledCharge.QuantityText), 7, alignRight: true);
            GridHelper.AddTextColumn(dgvCharges, "colAmount", "Amount", nameof(UnbilledCharge.AmountText), 13, alignRight: true);
            foreach (DataGridViewColumn column in dgvCharges.Columns) column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void Toggle(int index)
        {
            if (index < 0 || index >= _ticked.Length) return;
            _ticked[index] = !_ticked[index];
            RefreshTicks();
        }

        private void RefreshTicks()
        {
            for (int i = 0; i < dgvCharges.Rows.Count && i < _ticked.Length; i++)
                dgvCharges.Rows[i].Cells["colTick"].Value = _ticked[i];

            int count = _ticked.Count(t => t);
            decimal total = _charges.Where((c, i) => _ticked[i]).Sum(c => c.Amount);
            lblTotal.Text = count == 0
                ? "Nothing selected"
                : $"{count} of {_charges.Count} selected  ·  {SettingsHelper.FormatMoney(total)}";
            btnAdd.Enabled = count > 0;

            _syncing = true;
            chkAll.Checked = count == _charges.Count;
            _syncing = false;
        }

        private void Confirm()
        {
            Selected = _charges.Where((c, i) => _ticked[i]).ToList();
            if (Selected.Count == 0) return;
            DialogResult = DialogResult.OK;
        }
    }
}
