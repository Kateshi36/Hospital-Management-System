namespace HospitalManagementSystem.Views.MP12_Payment
{
    partial class PaymentForm
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null!;

        /// <summary>Clean up any resources being used.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // Redesign: page title + date line, Record Payment / Records toggle with the status on the right; (pay)
        // payable bills and payments-made table cards on the left, selected-bill and new-payment cards on the
        // right; (records) the records table card with filters in its header above the payments card.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblDate = new Label();
            lblTitle = new Label();
            tlpMode = new TableLayoutPanel();
            btnModePay = new Button();
            btnModeRecords = new Button();
            lblStatus = new Label();
            pnlBody = new Panel();
            pnlRecords = new Panel();
            tlpRecords = new TableLayoutPanel();
            pnlRecordsCard = new Panel();
            dgvRecords = new DataGridView();
            pnlRecordsFooter = new Panel();
            lblRecCount = new Label();
            flowRecordsFooter = new FlowLayoutPanel();
            pnlRecordsHeader = new Panel();
            lblRecordsTitle = new Label();
            flowRecFilter = new FlowLayoutPanel();
            txtRecSearch = new TextBox();
            lblRecFrom = new Label();
            dtpRecFrom = new DateTimePicker();
            lblRecTo = new Label();
            dtpRecTo = new DateTimePicker();
            cboRecStatus = new ComboBox();
            btnRecSearch = new Button();
            btnRecAll = new Button();
            pnlRecHistoryCard = new Panel();
            dgvRecHistory = new DataGridView();
            pnlRecHistoryFooter = new Panel();
            lblRecHistoryInfo = new Label();
            flowRecHistoryActions = new FlowLayoutPanel();
            btnReprint = new Button();
            pnlRecHistoryHeader = new Panel();
            lblRecHistoryTitle = new Label();
            pnlPay = new Panel();
            tlpPayMain = new TableLayoutPanel();
            tlpPayLeft = new TableLayoutPanel();
            pnlBillsCard = new Panel();
            dgvPayable = new DataGridView();
            pnlBillsFooter = new Panel();
            lblPayableCount = new Label();
            flowBillsFooter = new FlowLayoutPanel();
            pnlBillsHeader = new Panel();
            lblBillsTitle = new Label();
            flowBillFilter = new FlowLayoutPanel();
            txtBillSearch = new TextBox();
            btnBillSearch = new Button();
            btnBillAll = new Button();
            pnlHistoryCard = new Panel();
            dgvHistory = new DataGridView();
            pnlHistoryFooter = new Panel();
            lblHistoryInfo = new Label();
            flowHistoryActions = new FlowLayoutPanel();
            btnViewReceipt = new Button();
            pnlHistoryHeader = new Panel();
            lblHistoryTitle = new Label();
            tlpPayRight = new TableLayoutPanel();
            pnlBillInfoCard = new Panel();
            tlpBillInfoCard = new TableLayoutPanel();
            lblBillInfoTitle = new Label();
            tlpInfo = new TableLayoutPanel();
            lblInfoBillCap = new Label();
            lblInfoPatientCap = new Label();
            lblInfoBill = new Label();
            lblInfoPatient = new Label();
            lblInfoTotalCap = new Label();
            lblInfoPaidCap = new Label();
            lblInfoTotal = new Label();
            lblInfoPaid = new Label();
            lblInfoBalanceCap = new Label();
            lblInfoStatusCap = new Label();
            lblInfoBalance = new Label();
            lblInfoStatus = new Label();
            pnlPayCard = new Panel();
            tlpPayCard = new TableLayoutPanel();
            lblPayTitle = new Label();
            pnlPayScroll = new Panel();
            tlpPayFields = new TableLayoutPanel();
            tlpPayForm = new TableLayoutPanel();
            lblAmount = new Label();
            tlpAmount = new TableLayoutPanel();
            txtAmount = new TextBox();
            btnPayFull = new Button();
            lblMethod = new Label();
            cboMethod = new ComboBox();
            lblReference = new Label();
            txtReference = new TextBox();
            lblTendered = new Label();
            txtTendered = new TextBox();
            lblChangeCap = new Label();
            lblChange = new Label();
            lblReceivedByCap = new Label();
            txtReceivedBy = new TextBox();
            lblPayNote = new Label();
            tlpPayButtons = new TableLayoutPanel();
            btnRecord = new Button();
            btnClearPay = new Button();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            pnlTitle.SuspendLayout();
            tlpMode.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlRecords.SuspendLayout();
            tlpRecords.SuspendLayout();
            pnlRecordsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecords).BeginInit();
            pnlRecordsFooter.SuspendLayout();
            flowRecordsFooter.SuspendLayout();
            pnlRecordsHeader.SuspendLayout();
            flowRecFilter.SuspendLayout();
            pnlRecHistoryCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecHistory).BeginInit();
            pnlRecHistoryFooter.SuspendLayout();
            flowRecHistoryActions.SuspendLayout();
            pnlRecHistoryHeader.SuspendLayout();
            pnlPay.SuspendLayout();
            tlpPayMain.SuspendLayout();
            tlpPayLeft.SuspendLayout();
            pnlBillsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayable).BeginInit();
            pnlBillsFooter.SuspendLayout();
            flowBillsFooter.SuspendLayout();
            pnlBillsHeader.SuspendLayout();
            flowBillFilter.SuspendLayout();
            pnlHistoryCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            pnlHistoryFooter.SuspendLayout();
            flowHistoryActions.SuspendLayout();
            pnlHistoryHeader.SuspendLayout();
            tlpPayRight.SuspendLayout();
            pnlBillInfoCard.SuspendLayout();
            tlpBillInfoCard.SuspendLayout();
            tlpInfo.SuspendLayout();
            pnlPayCard.SuspendLayout();
            tlpPayCard.SuspendLayout();
            pnlPayScroll.SuspendLayout();
            tlpPayFields.SuspendLayout();
            tlpPayForm.SuspendLayout();
            tlpAmount.SuspendLayout();
            tlpPayButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.BackColor = Color.FromArgb(243, 246, 249);
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(pnlTitle, 0, 0);
            tlpRoot.Controls.Add(tlpMode, 0, 1);
            tlpRoot.Controls.Add(pnlBody, 0, 2);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 3;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.TabIndex = 0;
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.Transparent;
            pnlTitle.Controls.Add(lblDate);
            pnlTitle.Controls.Add(lblTitle);
            pnlTitle.Dock = DockStyle.Fill;
            pnlTitle.Margin = new Padding(0, 0, 0, 18);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Padding = new Padding(0);
            pnlTitle.TabIndex = 0;
            // 
            // lblDate
            // 
            lblDate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblDate.AutoEllipsis = true;
            lblDate.BackColor = Color.Transparent;
            lblDate.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDate.ForeColor = Color.FromArgb(91, 107, 124);
            lblDate.Location = new Point(-2, 34);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(900, 22);
            lblDate.TabIndex = 0;
            lblDate.Text = "Record payments on finalized bills and print receipts.";
            lblDate.UseMnemonic = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblTitle.Location = new Point(-4, -2);
            lblTitle.Name = "lblTitle";
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Payments & Receipts";
            lblTitle.UseMnemonic = false;
            // 
            // tlpMode
            // 
            tlpMode.BackColor = Color.Transparent;
            tlpMode.ColumnCount = 3;
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMode.Controls.Add(btnModePay, 0, 0);
            tlpMode.Controls.Add(btnModeRecords, 1, 0);
            tlpMode.Controls.Add(lblStatus, 2, 0);
            tlpMode.Dock = DockStyle.Fill;
            tlpMode.Margin = new Padding(0, 0, 0, 18);
            tlpMode.Name = "tlpMode";
            tlpMode.RowCount = 1;
            tlpMode.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMode.TabIndex = 1;
            // 
            // btnModePay
            // 
            btnModePay.BackColor = Color.FromArgb(18, 122, 140);
            btnModePay.FlatStyle = FlatStyle.Flat;
            btnModePay.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModePay.ForeColor = Color.White;
            btnModePay.Margin = new Padding(0, 0, 8, 0);
            btnModePay.Name = "btnModePay";
            btnModePay.Size = new Size(170, 38);
            btnModePay.TabIndex = 0;
            btnModePay.Text = "Record Payment";
            btnModePay.UseMnemonic = false;
            btnModePay.UseVisualStyleBackColor = false;
            // 
            // btnModeRecords
            // 
            btnModeRecords.BackColor = Color.White;
            btnModeRecords.FlatStyle = FlatStyle.Flat;
            btnModeRecords.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModeRecords.ForeColor = Color.FromArgb(20, 34, 51);
            btnModeRecords.Margin = new Padding(0);
            btnModeRecords.Name = "btnModeRecords";
            btnModeRecords.Size = new Size(170, 38);
            btnModeRecords.TabIndex = 1;
            btnModeRecords.Text = "Billing Records";
            btnModeRecords.UseMnemonic = false;
            btnModeRecords.UseVisualStyleBackColor = false;
            // 
            // lblStatus
            // 
            lblStatus.AutoEllipsis = true;
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.FromArgb(91, 107, 124);
            lblStatus.Margin = new Padding(0);
            lblStatus.Name = "lblStatus";
            lblStatus.TabIndex = 2;
            lblStatus.Text = "";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            lblStatus.UseMnemonic = false;
            // 
            // pnlBody
            // 
            pnlBody.BackColor = Color.Transparent;
            pnlBody.Controls.Add(pnlRecords);
            pnlBody.Controls.Add(pnlPay);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Margin = new Padding(0);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(0);
            pnlBody.TabIndex = 2;
            // 
            // pnlRecords
            // 
            pnlRecords.BackColor = Color.Transparent;
            pnlRecords.Controls.Add(tlpRecords);
            pnlRecords.Dock = DockStyle.Fill;
            pnlRecords.Margin = new Padding(0);
            pnlRecords.Name = "pnlRecords";
            pnlRecords.Padding = new Padding(0);
            pnlRecords.TabIndex = 0;
            pnlRecords.Visible = false;
            // 
            // tlpRecords
            // 
            tlpRecords.BackColor = Color.Transparent;
            tlpRecords.ColumnCount = 1;
            tlpRecords.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRecords.Controls.Add(pnlRecordsCard, 0, 0);
            tlpRecords.Controls.Add(pnlRecHistoryCard, 0, 1);
            tlpRecords.Dock = DockStyle.Fill;
            tlpRecords.Margin = new Padding(0);
            tlpRecords.Name = "tlpRecords";
            tlpRecords.RowCount = 2;
            tlpRecords.RowStyles.Add(new RowStyle(SizeType.Percent, 58F));
            tlpRecords.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
            tlpRecords.TabIndex = 0;
            // 
            // pnlRecordsCard
            // 
            pnlRecordsCard.BackColor = Color.White;
            pnlRecordsCard.Controls.Add(dgvRecords);
            pnlRecordsCard.Controls.Add(pnlRecordsFooter);
            pnlRecordsCard.Controls.Add(pnlRecordsHeader);
            pnlRecordsCard.Dock = DockStyle.Fill;
            pnlRecordsCard.Margin = new Padding(0, 0, 0, 20);
            pnlRecordsCard.Name = "pnlRecordsCard";
            pnlRecordsCard.Padding = new Padding(1, 6, 1, 1);
            pnlRecordsCard.TabIndex = 0;
            // 
            // dgvRecords
            // 
            dgvRecords.AllowUserToAddRows = false;
            dgvRecords.AllowUserToDeleteRows = false;
            dgvRecords.AllowUserToResizeRows = false;
            dgvRecords.BackgroundColor = Color.White;
            dgvRecords.BorderStyle = BorderStyle.None;
            dgvRecords.Dock = DockStyle.Fill;
            dgvRecords.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvRecords.Margin = new Padding(0);
            dgvRecords.MultiSelect = false;
            dgvRecords.Name = "dgvRecords";
            dgvRecords.ReadOnly = true;
            dgvRecords.RowHeadersVisible = false;
            dgvRecords.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecords.TabIndex = 0;
            // 
            // pnlRecordsFooter
            // 
            pnlRecordsFooter.BackColor = Color.White;
            pnlRecordsFooter.Controls.Add(lblRecCount);
            pnlRecordsFooter.Controls.Add(flowRecordsFooter);
            pnlRecordsFooter.Dock = DockStyle.Bottom;
            pnlRecordsFooter.Margin = new Padding(0);
            pnlRecordsFooter.Name = "pnlRecordsFooter";
            pnlRecordsFooter.Padding = new Padding(0);
            pnlRecordsFooter.Size = new Size(600, 44);
            pnlRecordsFooter.TabIndex = 1;
            // 
            // lblRecCount
            // 
            lblRecCount.AutoEllipsis = true;
            lblRecCount.BackColor = Color.Transparent;
            lblRecCount.Dock = DockStyle.Fill;
            lblRecCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRecCount.ForeColor = Color.FromArgb(91, 107, 124);
            lblRecCount.Name = "lblRecCount";
            lblRecCount.Padding = new Padding(18, 0, 8, 0);
            lblRecCount.TabIndex = 0;
            lblRecCount.TextAlign = ContentAlignment.MiddleLeft;
            lblRecCount.UseMnemonic = false;
            // 
            // flowRecordsFooter
            // 
            flowRecordsFooter.AutoSize = true;
            flowRecordsFooter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowRecordsFooter.BackColor = Color.Transparent;
            flowRecordsFooter.Dock = DockStyle.Right;
            flowRecordsFooter.Margin = new Padding(0);
            flowRecordsFooter.Name = "flowRecordsFooter";
            flowRecordsFooter.Padding = new Padding(0);
            flowRecordsFooter.TabIndex = 1;
            flowRecordsFooter.WrapContents = false;
            // 
            // pnlRecordsHeader
            // 
            pnlRecordsHeader.BackColor = Color.White;
            pnlRecordsHeader.Controls.Add(lblRecordsTitle);
            pnlRecordsHeader.Controls.Add(flowRecFilter);
            pnlRecordsHeader.Dock = DockStyle.Top;
            pnlRecordsHeader.Margin = new Padding(0);
            pnlRecordsHeader.Name = "pnlRecordsHeader";
            pnlRecordsHeader.Padding = new Padding(0);
            pnlRecordsHeader.Size = new Size(600, 46);
            pnlRecordsHeader.TabIndex = 2;
            // 
            // lblRecordsTitle
            // 
            lblRecordsTitle.AutoEllipsis = true;
            lblRecordsTitle.BackColor = Color.Transparent;
            lblRecordsTitle.Dock = DockStyle.Fill;
            lblRecordsTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRecordsTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblRecordsTitle.Margin = new Padding(0);
            lblRecordsTitle.Name = "lblRecordsTitle";
            lblRecordsTitle.Padding = new Padding(17, 0, 0, 0);
            lblRecordsTitle.TabIndex = 0;
            lblRecordsTitle.Text = "Billing records";
            lblRecordsTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblRecordsTitle.UseMnemonic = false;
            // 
            // flowRecFilter
            // 
            flowRecFilter.AutoSize = true;
            flowRecFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowRecFilter.BackColor = Color.White;
            flowRecFilter.Controls.Add(txtRecSearch);
            flowRecFilter.Controls.Add(lblRecFrom);
            flowRecFilter.Controls.Add(dtpRecFrom);
            flowRecFilter.Controls.Add(lblRecTo);
            flowRecFilter.Controls.Add(dtpRecTo);
            flowRecFilter.Controls.Add(cboRecStatus);
            flowRecFilter.Controls.Add(btnRecSearch);
            flowRecFilter.Controls.Add(btnRecAll);
            flowRecFilter.Dock = DockStyle.Right;
            flowRecFilter.Margin = new Padding(0);
            flowRecFilter.Name = "flowRecFilter";
            flowRecFilter.Padding = new Padding(0, 5, 18, 0);
            flowRecFilter.TabIndex = 1;
            flowRecFilter.WrapContents = false;
            // 
            // txtRecSearch
            // 
            txtRecSearch.AutoSize = false;
            txtRecSearch.BackColor = Color.White;
            txtRecSearch.BorderStyle = BorderStyle.FixedSingle;
            txtRecSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRecSearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtRecSearch.Margin = new Padding(0);
            txtRecSearch.MaxLength = 60;
            txtRecSearch.Name = "txtRecSearch";
            txtRecSearch.PlaceholderText = "Bill code, patient name or PAT- code";
            txtRecSearch.Size = new Size(200, 36);
            txtRecSearch.TabIndex = 0;
            // 
            // lblRecFrom
            // 
            lblRecFrom.AutoSize = true;
            lblRecFrom.BackColor = Color.Transparent;
            lblRecFrom.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRecFrom.ForeColor = Color.FromArgb(91, 107, 124);
            lblRecFrom.Margin = new Padding(12, 9, 6, 0);
            lblRecFrom.Name = "lblRecFrom";
            lblRecFrom.TabIndex = 1;
            lblRecFrom.Text = "Bill date";
            lblRecFrom.UseMnemonic = false;
            // 
            // dtpRecFrom
            // 
            dtpRecFrom.Anchor = AnchorStyles.Left;
            dtpRecFrom.Checked = false;
            dtpRecFrom.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpRecFrom.Format = DateTimePickerFormat.Short;
            dtpRecFrom.Margin = new Padding(0, 3, 0, 0);
            dtpRecFrom.Name = "dtpRecFrom";
            dtpRecFrom.ShowCheckBox = true;
            dtpRecFrom.Size = new Size(140, 31);
            dtpRecFrom.TabIndex = 2;
            // 
            // lblRecTo
            // 
            lblRecTo.AutoSize = true;
            lblRecTo.BackColor = Color.Transparent;
            lblRecTo.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRecTo.ForeColor = Color.FromArgb(91, 107, 124);
            lblRecTo.Margin = new Padding(6, 9, 6, 0);
            lblRecTo.Name = "lblRecTo";
            lblRecTo.TabIndex = 3;
            lblRecTo.Text = "to";
            lblRecTo.UseMnemonic = false;
            // 
            // dtpRecTo
            // 
            dtpRecTo.Anchor = AnchorStyles.Left;
            dtpRecTo.Checked = false;
            dtpRecTo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpRecTo.Format = DateTimePickerFormat.Short;
            dtpRecTo.Margin = new Padding(0, 3, 0, 0);
            dtpRecTo.Name = "dtpRecTo";
            dtpRecTo.ShowCheckBox = true;
            dtpRecTo.Size = new Size(140, 31);
            dtpRecTo.TabIndex = 4;
            // 
            // cboRecStatus
            // 
            cboRecStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRecStatus.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboRecStatus.FormattingEnabled = true;
            cboRecStatus.Margin = new Padding(10, 0, 0, 0);
            cboRecStatus.Name = "cboRecStatus";
            cboRecStatus.Size = new Size(140, 32);
            cboRecStatus.TabIndex = 5;
            // 
            // btnRecSearch
            // 
            btnRecSearch.BackColor = Color.FromArgb(226, 242, 246);
            btnRecSearch.FlatStyle = FlatStyle.Flat;
            btnRecSearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRecSearch.ForeColor = Color.FromArgb(20, 34, 51);
            btnRecSearch.Margin = new Padding(10, 0, 0, 0);
            btnRecSearch.Name = "btnRecSearch";
            btnRecSearch.Size = new Size(80, 36);
            btnRecSearch.TabIndex = 6;
            btnRecSearch.Text = "Search";
            btnRecSearch.UseMnemonic = false;
            btnRecSearch.UseVisualStyleBackColor = false;
            // 
            // btnRecAll
            // 
            btnRecAll.BackColor = Color.White;
            btnRecAll.FlatStyle = FlatStyle.Flat;
            btnRecAll.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRecAll.ForeColor = Color.FromArgb(20, 34, 51);
            btnRecAll.Margin = new Padding(10, 0, 0, 0);
            btnRecAll.Name = "btnRecAll";
            btnRecAll.Size = new Size(90, 36);
            btnRecAll.TabIndex = 7;
            btnRecAll.Text = "Show All";
            btnRecAll.UseMnemonic = false;
            btnRecAll.UseVisualStyleBackColor = false;
            // 
            // pnlRecHistoryCard
            // 
            pnlRecHistoryCard.BackColor = Color.White;
            pnlRecHistoryCard.Controls.Add(dgvRecHistory);
            pnlRecHistoryCard.Controls.Add(pnlRecHistoryFooter);
            pnlRecHistoryCard.Controls.Add(pnlRecHistoryHeader);
            pnlRecHistoryCard.Dock = DockStyle.Fill;
            pnlRecHistoryCard.Margin = new Padding(0);
            pnlRecHistoryCard.Name = "pnlRecHistoryCard";
            pnlRecHistoryCard.Padding = new Padding(1, 6, 1, 1);
            pnlRecHistoryCard.TabIndex = 1;
            // 
            // dgvRecHistory
            // 
            dgvRecHistory.AllowUserToAddRows = false;
            dgvRecHistory.AllowUserToDeleteRows = false;
            dgvRecHistory.AllowUserToResizeRows = false;
            dgvRecHistory.BackgroundColor = Color.White;
            dgvRecHistory.BorderStyle = BorderStyle.None;
            dgvRecHistory.Dock = DockStyle.Fill;
            dgvRecHistory.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvRecHistory.Margin = new Padding(0);
            dgvRecHistory.MultiSelect = false;
            dgvRecHistory.Name = "dgvRecHistory";
            dgvRecHistory.ReadOnly = true;
            dgvRecHistory.RowHeadersVisible = false;
            dgvRecHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecHistory.TabIndex = 0;
            // 
            // pnlRecHistoryFooter
            // 
            pnlRecHistoryFooter.BackColor = Color.White;
            pnlRecHistoryFooter.Controls.Add(lblRecHistoryInfo);
            pnlRecHistoryFooter.Controls.Add(flowRecHistoryActions);
            pnlRecHistoryFooter.Dock = DockStyle.Bottom;
            pnlRecHistoryFooter.Margin = new Padding(0);
            pnlRecHistoryFooter.Name = "pnlRecHistoryFooter";
            pnlRecHistoryFooter.Padding = new Padding(0);
            pnlRecHistoryFooter.Size = new Size(600, 56);
            pnlRecHistoryFooter.TabIndex = 1;
            // 
            // lblRecHistoryInfo
            // 
            lblRecHistoryInfo.AutoEllipsis = true;
            lblRecHistoryInfo.BackColor = Color.Transparent;
            lblRecHistoryInfo.Dock = DockStyle.Fill;
            lblRecHistoryInfo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRecHistoryInfo.ForeColor = Color.FromArgb(91, 107, 124);
            lblRecHistoryInfo.Name = "lblRecHistoryInfo";
            lblRecHistoryInfo.Padding = new Padding(18, 0, 8, 0);
            lblRecHistoryInfo.TabIndex = 0;
            lblRecHistoryInfo.TextAlign = ContentAlignment.MiddleLeft;
            lblRecHistoryInfo.UseMnemonic = false;
            // 
            // flowRecHistoryActions
            // 
            flowRecHistoryActions.AutoSize = true;
            flowRecHistoryActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowRecHistoryActions.BackColor = Color.Transparent;
            flowRecHistoryActions.Controls.Add(btnReprint);
            flowRecHistoryActions.Dock = DockStyle.Right;
            flowRecHistoryActions.Margin = new Padding(0);
            flowRecHistoryActions.Name = "flowRecHistoryActions";
            flowRecHistoryActions.Padding = new Padding(0, 10, 18, 0);
            flowRecHistoryActions.TabIndex = 1;
            flowRecHistoryActions.WrapContents = false;
            // 
            // btnReprint
            // 
            btnReprint.BackColor = Color.White;
            btnReprint.Enabled = false;
            btnReprint.FlatStyle = FlatStyle.Flat;
            btnReprint.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnReprint.ForeColor = Color.FromArgb(20, 34, 51);
            btnReprint.Margin = new Padding(10, 0, 0, 0);
            btnReprint.Name = "btnReprint";
            btnReprint.Size = new Size(150, 36);
            btnReprint.TabIndex = 0;
            btnReprint.Text = "Reprint Receipt";
            btnReprint.UseMnemonic = false;
            btnReprint.UseVisualStyleBackColor = false;
            // 
            // pnlRecHistoryHeader
            // 
            pnlRecHistoryHeader.BackColor = Color.White;
            pnlRecHistoryHeader.Controls.Add(lblRecHistoryTitle);
            pnlRecHistoryHeader.Dock = DockStyle.Top;
            pnlRecHistoryHeader.Margin = new Padding(0);
            pnlRecHistoryHeader.Name = "pnlRecHistoryHeader";
            pnlRecHistoryHeader.Padding = new Padding(0);
            pnlRecHistoryHeader.Size = new Size(600, 46);
            pnlRecHistoryHeader.TabIndex = 2;
            // 
            // lblRecHistoryTitle
            // 
            lblRecHistoryTitle.AutoEllipsis = true;
            lblRecHistoryTitle.BackColor = Color.Transparent;
            lblRecHistoryTitle.Dock = DockStyle.Fill;
            lblRecHistoryTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRecHistoryTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblRecHistoryTitle.Margin = new Padding(0);
            lblRecHistoryTitle.Name = "lblRecHistoryTitle";
            lblRecHistoryTitle.Padding = new Padding(17, 0, 0, 0);
            lblRecHistoryTitle.TabIndex = 0;
            lblRecHistoryTitle.Text = "Payments of the selected bill";
            lblRecHistoryTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblRecHistoryTitle.UseMnemonic = false;
            // 
            // pnlPay
            // 
            pnlPay.BackColor = Color.Transparent;
            pnlPay.Controls.Add(tlpPayMain);
            pnlPay.Dock = DockStyle.Fill;
            pnlPay.Margin = new Padding(0);
            pnlPay.Name = "pnlPay";
            pnlPay.Padding = new Padding(0);
            pnlPay.TabIndex = 1;
            // 
            // tlpPayMain
            // 
            tlpPayMain.BackColor = Color.Transparent;
            tlpPayMain.ColumnCount = 2;
            tlpPayMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPayMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 460F));
            tlpPayMain.Controls.Add(tlpPayLeft, 0, 0);
            tlpPayMain.Controls.Add(tlpPayRight, 1, 0);
            tlpPayMain.Dock = DockStyle.Fill;
            tlpPayMain.Margin = new Padding(0);
            tlpPayMain.Name = "tlpPayMain";
            tlpPayMain.RowCount = 1;
            tlpPayMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPayMain.TabIndex = 0;
            // 
            // tlpPayLeft
            // 
            tlpPayLeft.BackColor = Color.Transparent;
            tlpPayLeft.ColumnCount = 1;
            tlpPayLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPayLeft.Controls.Add(pnlBillsCard, 0, 0);
            tlpPayLeft.Controls.Add(pnlHistoryCard, 0, 1);
            tlpPayLeft.Dock = DockStyle.Fill;
            tlpPayLeft.Margin = new Padding(0, 0, 20, 0);
            tlpPayLeft.Name = "tlpPayLeft";
            tlpPayLeft.RowCount = 2;
            tlpPayLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 58F));
            tlpPayLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
            tlpPayLeft.TabIndex = 0;
            // 
            // pnlBillsCard
            // 
            pnlBillsCard.BackColor = Color.White;
            pnlBillsCard.Controls.Add(dgvPayable);
            pnlBillsCard.Controls.Add(pnlBillsFooter);
            pnlBillsCard.Controls.Add(pnlBillsHeader);
            pnlBillsCard.Dock = DockStyle.Fill;
            pnlBillsCard.Margin = new Padding(0, 0, 0, 20);
            pnlBillsCard.Name = "pnlBillsCard";
            pnlBillsCard.Padding = new Padding(1, 6, 1, 1);
            pnlBillsCard.TabIndex = 0;
            // 
            // dgvPayable
            // 
            dgvPayable.AllowUserToAddRows = false;
            dgvPayable.AllowUserToDeleteRows = false;
            dgvPayable.AllowUserToResizeRows = false;
            dgvPayable.BackgroundColor = Color.White;
            dgvPayable.BorderStyle = BorderStyle.None;
            dgvPayable.Dock = DockStyle.Fill;
            dgvPayable.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvPayable.Margin = new Padding(0);
            dgvPayable.MultiSelect = false;
            dgvPayable.Name = "dgvPayable";
            dgvPayable.ReadOnly = true;
            dgvPayable.RowHeadersVisible = false;
            dgvPayable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPayable.TabIndex = 0;
            // 
            // pnlBillsFooter
            // 
            pnlBillsFooter.BackColor = Color.White;
            pnlBillsFooter.Controls.Add(lblPayableCount);
            pnlBillsFooter.Controls.Add(flowBillsFooter);
            pnlBillsFooter.Dock = DockStyle.Bottom;
            pnlBillsFooter.Margin = new Padding(0);
            pnlBillsFooter.Name = "pnlBillsFooter";
            pnlBillsFooter.Padding = new Padding(0);
            pnlBillsFooter.Size = new Size(600, 44);
            pnlBillsFooter.TabIndex = 1;
            // 
            // lblPayableCount
            // 
            lblPayableCount.AutoEllipsis = true;
            lblPayableCount.BackColor = Color.Transparent;
            lblPayableCount.Dock = DockStyle.Fill;
            lblPayableCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPayableCount.ForeColor = Color.FromArgb(91, 107, 124);
            lblPayableCount.Name = "lblPayableCount";
            lblPayableCount.Padding = new Padding(18, 0, 8, 0);
            lblPayableCount.TabIndex = 0;
            lblPayableCount.TextAlign = ContentAlignment.MiddleLeft;
            lblPayableCount.UseMnemonic = false;
            // 
            // flowBillsFooter
            // 
            flowBillsFooter.AutoSize = true;
            flowBillsFooter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowBillsFooter.BackColor = Color.Transparent;
            flowBillsFooter.Dock = DockStyle.Right;
            flowBillsFooter.Margin = new Padding(0);
            flowBillsFooter.Name = "flowBillsFooter";
            flowBillsFooter.Padding = new Padding(0);
            flowBillsFooter.TabIndex = 1;
            flowBillsFooter.WrapContents = false;
            // 
            // pnlBillsHeader
            // 
            pnlBillsHeader.BackColor = Color.White;
            pnlBillsHeader.Controls.Add(lblBillsTitle);
            pnlBillsHeader.Controls.Add(flowBillFilter);
            pnlBillsHeader.Dock = DockStyle.Top;
            pnlBillsHeader.Margin = new Padding(0);
            pnlBillsHeader.Name = "pnlBillsHeader";
            pnlBillsHeader.Padding = new Padding(0);
            pnlBillsHeader.Size = new Size(600, 46);
            pnlBillsHeader.TabIndex = 2;
            // 
            // lblBillsTitle
            // 
            lblBillsTitle.AutoEllipsis = true;
            lblBillsTitle.BackColor = Color.Transparent;
            lblBillsTitle.Dock = DockStyle.Fill;
            lblBillsTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBillsTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblBillsTitle.Margin = new Padding(0);
            lblBillsTitle.Name = "lblBillsTitle";
            lblBillsTitle.Padding = new Padding(17, 0, 0, 0);
            lblBillsTitle.TabIndex = 0;
            lblBillsTitle.Text = "Bills waiting for payment";
            lblBillsTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblBillsTitle.UseMnemonic = false;
            // 
            // flowBillFilter
            // 
            flowBillFilter.AutoSize = true;
            flowBillFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowBillFilter.BackColor = Color.White;
            flowBillFilter.Controls.Add(txtBillSearch);
            flowBillFilter.Controls.Add(btnBillSearch);
            flowBillFilter.Controls.Add(btnBillAll);
            flowBillFilter.Dock = DockStyle.Right;
            flowBillFilter.Margin = new Padding(0);
            flowBillFilter.Name = "flowBillFilter";
            flowBillFilter.Padding = new Padding(0, 5, 18, 0);
            flowBillFilter.TabIndex = 1;
            flowBillFilter.WrapContents = false;
            // 
            // txtBillSearch
            // 
            txtBillSearch.AutoSize = false;
            txtBillSearch.BackColor = Color.White;
            txtBillSearch.BorderStyle = BorderStyle.FixedSingle;
            txtBillSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBillSearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtBillSearch.Margin = new Padding(0);
            txtBillSearch.MaxLength = 60;
            txtBillSearch.Name = "txtBillSearch";
            txtBillSearch.PlaceholderText = "Bill code, patient name or PAT- code";
            txtBillSearch.Size = new Size(220, 36);
            txtBillSearch.TabIndex = 0;
            // 
            // btnBillSearch
            // 
            btnBillSearch.BackColor = Color.FromArgb(226, 242, 246);
            btnBillSearch.FlatStyle = FlatStyle.Flat;
            btnBillSearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBillSearch.ForeColor = Color.FromArgb(20, 34, 51);
            btnBillSearch.Margin = new Padding(10, 0, 0, 0);
            btnBillSearch.Name = "btnBillSearch";
            btnBillSearch.Size = new Size(80, 36);
            btnBillSearch.TabIndex = 1;
            btnBillSearch.Text = "Find";
            btnBillSearch.UseMnemonic = false;
            btnBillSearch.UseVisualStyleBackColor = false;
            // 
            // btnBillAll
            // 
            btnBillAll.BackColor = Color.White;
            btnBillAll.FlatStyle = FlatStyle.Flat;
            btnBillAll.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBillAll.ForeColor = Color.FromArgb(20, 34, 51);
            btnBillAll.Margin = new Padding(10, 0, 0, 0);
            btnBillAll.Name = "btnBillAll";
            btnBillAll.Size = new Size(96, 36);
            btnBillAll.TabIndex = 2;
            btnBillAll.Text = "Show All";
            btnBillAll.UseMnemonic = false;
            btnBillAll.UseVisualStyleBackColor = false;
            // 
            // pnlHistoryCard
            // 
            pnlHistoryCard.BackColor = Color.White;
            pnlHistoryCard.Controls.Add(dgvHistory);
            pnlHistoryCard.Controls.Add(pnlHistoryFooter);
            pnlHistoryCard.Controls.Add(pnlHistoryHeader);
            pnlHistoryCard.Dock = DockStyle.Fill;
            pnlHistoryCard.Margin = new Padding(0);
            pnlHistoryCard.Name = "pnlHistoryCard";
            pnlHistoryCard.Padding = new Padding(1, 6, 1, 1);
            pnlHistoryCard.TabIndex = 1;
            // 
            // dgvHistory
            // 
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.AllowUserToResizeRows = false;
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvHistory.Margin = new Padding(0);
            dgvHistory.MultiSelect = false;
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.TabIndex = 0;
            // 
            // pnlHistoryFooter
            // 
            pnlHistoryFooter.BackColor = Color.White;
            pnlHistoryFooter.Controls.Add(lblHistoryInfo);
            pnlHistoryFooter.Controls.Add(flowHistoryActions);
            pnlHistoryFooter.Dock = DockStyle.Bottom;
            pnlHistoryFooter.Margin = new Padding(0);
            pnlHistoryFooter.Name = "pnlHistoryFooter";
            pnlHistoryFooter.Padding = new Padding(0);
            pnlHistoryFooter.Size = new Size(600, 56);
            pnlHistoryFooter.TabIndex = 1;
            // 
            // lblHistoryInfo
            // 
            lblHistoryInfo.AutoEllipsis = true;
            lblHistoryInfo.BackColor = Color.Transparent;
            lblHistoryInfo.Dock = DockStyle.Fill;
            lblHistoryInfo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHistoryInfo.ForeColor = Color.FromArgb(91, 107, 124);
            lblHistoryInfo.Name = "lblHistoryInfo";
            lblHistoryInfo.Padding = new Padding(18, 0, 8, 0);
            lblHistoryInfo.TabIndex = 0;
            lblHistoryInfo.TextAlign = ContentAlignment.MiddleLeft;
            lblHistoryInfo.UseMnemonic = false;
            // 
            // flowHistoryActions
            // 
            flowHistoryActions.AutoSize = true;
            flowHistoryActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowHistoryActions.BackColor = Color.Transparent;
            flowHistoryActions.Controls.Add(btnViewReceipt);
            flowHistoryActions.Dock = DockStyle.Right;
            flowHistoryActions.Margin = new Padding(0);
            flowHistoryActions.Name = "flowHistoryActions";
            flowHistoryActions.Padding = new Padding(0, 10, 18, 0);
            flowHistoryActions.TabIndex = 1;
            flowHistoryActions.WrapContents = false;
            // 
            // btnViewReceipt
            // 
            btnViewReceipt.BackColor = Color.White;
            btnViewReceipt.Enabled = false;
            btnViewReceipt.FlatStyle = FlatStyle.Flat;
            btnViewReceipt.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnViewReceipt.ForeColor = Color.FromArgb(20, 34, 51);
            btnViewReceipt.Margin = new Padding(10, 0, 0, 0);
            btnViewReceipt.Name = "btnViewReceipt";
            btnViewReceipt.Size = new Size(130, 36);
            btnViewReceipt.TabIndex = 0;
            btnViewReceipt.Text = "View Receipt";
            btnViewReceipt.UseMnemonic = false;
            btnViewReceipt.UseVisualStyleBackColor = false;
            // 
            // pnlHistoryHeader
            // 
            pnlHistoryHeader.BackColor = Color.White;
            pnlHistoryHeader.Controls.Add(lblHistoryTitle);
            pnlHistoryHeader.Dock = DockStyle.Top;
            pnlHistoryHeader.Margin = new Padding(0);
            pnlHistoryHeader.Name = "pnlHistoryHeader";
            pnlHistoryHeader.Padding = new Padding(0);
            pnlHistoryHeader.Size = new Size(600, 46);
            pnlHistoryHeader.TabIndex = 2;
            // 
            // lblHistoryTitle
            // 
            lblHistoryTitle.AutoEllipsis = true;
            lblHistoryTitle.BackColor = Color.Transparent;
            lblHistoryTitle.Dock = DockStyle.Fill;
            lblHistoryTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHistoryTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblHistoryTitle.Margin = new Padding(0);
            lblHistoryTitle.Name = "lblHistoryTitle";
            lblHistoryTitle.Padding = new Padding(17, 0, 0, 0);
            lblHistoryTitle.TabIndex = 0;
            lblHistoryTitle.Text = "Payments already made on this bill";
            lblHistoryTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblHistoryTitle.UseMnemonic = false;
            // 
            // tlpPayRight
            // 
            tlpPayRight.BackColor = Color.Transparent;
            tlpPayRight.ColumnCount = 1;
            tlpPayRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPayRight.Controls.Add(pnlBillInfoCard, 0, 0);
            tlpPayRight.Controls.Add(pnlPayCard, 0, 1);
            tlpPayRight.Dock = DockStyle.Fill;
            tlpPayRight.Margin = new Padding(0);
            tlpPayRight.Name = "tlpPayRight";
            tlpPayRight.RowCount = 2;
            tlpPayRight.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPayRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPayRight.TabIndex = 1;
            // 
            // pnlBillInfoCard
            // 
            pnlBillInfoCard.AutoSize = true;
            pnlBillInfoCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlBillInfoCard.BackColor = Color.White;
            pnlBillInfoCard.Controls.Add(tlpBillInfoCard);
            pnlBillInfoCard.Dock = DockStyle.Fill;
            pnlBillInfoCard.Margin = new Padding(0, 0, 0, 20);
            pnlBillInfoCard.Name = "pnlBillInfoCard";
            pnlBillInfoCard.Padding = new Padding(20, 18, 20, 6);
            pnlBillInfoCard.TabIndex = 0;
            // 
            // tlpBillInfoCard
            // 
            tlpBillInfoCard.AutoSize = true;
            tlpBillInfoCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpBillInfoCard.BackColor = Color.Transparent;
            tlpBillInfoCard.ColumnCount = 1;
            tlpBillInfoCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBillInfoCard.Controls.Add(lblBillInfoTitle, 0, 0);
            tlpBillInfoCard.Controls.Add(tlpInfo, 0, 1);
            tlpBillInfoCard.Dock = DockStyle.Top;
            tlpBillInfoCard.Margin = new Padding(0);
            tlpBillInfoCard.Name = "tlpBillInfoCard";
            tlpBillInfoCard.RowCount = 2;
            tlpBillInfoCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBillInfoCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBillInfoCard.TabIndex = 0;
            // 
            // lblBillInfoTitle
            // 
            lblBillInfoTitle.AutoSize = true;
            lblBillInfoTitle.BackColor = Color.Transparent;
            lblBillInfoTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBillInfoTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblBillInfoTitle.Margin = new Padding(0, 0, 0, 14);
            lblBillInfoTitle.Name = "lblBillInfoTitle";
            lblBillInfoTitle.TabIndex = 0;
            lblBillInfoTitle.Text = "Selected bill";
            // 
            // tlpInfo
            // 
            tlpInfo.AutoSize = true;
            tlpInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpInfo.BackColor = Color.Transparent;
            tlpInfo.ColumnCount = 2;
            tlpInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpInfo.Controls.Add(lblInfoBillCap, 0, 0);
            tlpInfo.Controls.Add(lblInfoPatientCap, 1, 0);
            tlpInfo.Controls.Add(lblInfoBill, 0, 1);
            tlpInfo.Controls.Add(lblInfoPatient, 1, 1);
            tlpInfo.Controls.Add(lblInfoTotalCap, 0, 2);
            tlpInfo.Controls.Add(lblInfoPaidCap, 1, 2);
            tlpInfo.Controls.Add(lblInfoTotal, 0, 3);
            tlpInfo.Controls.Add(lblInfoPaid, 1, 3);
            tlpInfo.Controls.Add(lblInfoBalanceCap, 0, 4);
            tlpInfo.Controls.Add(lblInfoStatusCap, 1, 4);
            tlpInfo.Controls.Add(lblInfoBalance, 0, 5);
            tlpInfo.Controls.Add(lblInfoStatus, 1, 5);
            tlpInfo.Dock = DockStyle.Top;
            tlpInfo.Margin = new Padding(0);
            tlpInfo.Name = "tlpInfo";
            tlpInfo.RowCount = 6;
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpInfo.TabIndex = 1;
            // 
            // lblInfoBillCap
            // 
            lblInfoBillCap.AutoSize = true;
            lblInfoBillCap.BackColor = Color.Transparent;
            lblInfoBillCap.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInfoBillCap.ForeColor = Color.FromArgb(91, 107, 124);
            lblInfoBillCap.Margin = new Padding(0, 0, 8, 2);
            lblInfoBillCap.Name = "lblInfoBillCap";
            lblInfoBillCap.TabIndex = 0;
            lblInfoBillCap.Text = "Bill";
            lblInfoBillCap.UseMnemonic = false;
            // 
            // lblInfoPatientCap
            // 
            lblInfoPatientCap.AutoSize = true;
            lblInfoPatientCap.BackColor = Color.Transparent;
            lblInfoPatientCap.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInfoPatientCap.ForeColor = Color.FromArgb(91, 107, 124);
            lblInfoPatientCap.Margin = new Padding(0, 0, 8, 2);
            lblInfoPatientCap.Name = "lblInfoPatientCap";
            lblInfoPatientCap.TabIndex = 1;
            lblInfoPatientCap.Text = "Patient";
            lblInfoPatientCap.UseMnemonic = false;
            // 
            // lblInfoBill
            // 
            lblInfoBill.AutoSize = true;
            lblInfoBill.BackColor = Color.Transparent;
            lblInfoBill.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInfoBill.ForeColor = Color.FromArgb(20, 34, 51);
            lblInfoBill.Margin = new Padding(0, 0, 8, 12);
            lblInfoBill.MaximumSize = new Size(200, 0);
            lblInfoBill.Name = "lblInfoBill";
            lblInfoBill.TabIndex = 2;
            lblInfoBill.Text = "-";
            lblInfoBill.UseMnemonic = false;
            // 
            // lblInfoPatient
            // 
            lblInfoPatient.AutoSize = true;
            lblInfoPatient.BackColor = Color.Transparent;
            lblInfoPatient.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInfoPatient.ForeColor = Color.FromArgb(20, 34, 51);
            lblInfoPatient.Margin = new Padding(0, 0, 8, 12);
            lblInfoPatient.MaximumSize = new Size(200, 0);
            lblInfoPatient.Name = "lblInfoPatient";
            lblInfoPatient.TabIndex = 3;
            lblInfoPatient.Text = "-";
            lblInfoPatient.UseMnemonic = false;
            // 
            // lblInfoTotalCap
            // 
            lblInfoTotalCap.AutoSize = true;
            lblInfoTotalCap.BackColor = Color.Transparent;
            lblInfoTotalCap.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInfoTotalCap.ForeColor = Color.FromArgb(91, 107, 124);
            lblInfoTotalCap.Margin = new Padding(0, 0, 8, 2);
            lblInfoTotalCap.Name = "lblInfoTotalCap";
            lblInfoTotalCap.TabIndex = 4;
            lblInfoTotalCap.Text = "Bill total";
            lblInfoTotalCap.UseMnemonic = false;
            // 
            // lblInfoPaidCap
            // 
            lblInfoPaidCap.AutoSize = true;
            lblInfoPaidCap.BackColor = Color.Transparent;
            lblInfoPaidCap.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInfoPaidCap.ForeColor = Color.FromArgb(91, 107, 124);
            lblInfoPaidCap.Margin = new Padding(0, 0, 8, 2);
            lblInfoPaidCap.Name = "lblInfoPaidCap";
            lblInfoPaidCap.TabIndex = 5;
            lblInfoPaidCap.Text = "Paid so far";
            lblInfoPaidCap.UseMnemonic = false;
            // 
            // lblInfoTotal
            // 
            lblInfoTotal.AutoSize = true;
            lblInfoTotal.BackColor = Color.Transparent;
            lblInfoTotal.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInfoTotal.ForeColor = Color.FromArgb(20, 34, 51);
            lblInfoTotal.Margin = new Padding(0, 0, 8, 12);
            lblInfoTotal.MaximumSize = new Size(200, 0);
            lblInfoTotal.Name = "lblInfoTotal";
            lblInfoTotal.TabIndex = 6;
            lblInfoTotal.Text = "-";
            lblInfoTotal.UseMnemonic = false;
            // 
            // lblInfoPaid
            // 
            lblInfoPaid.AutoSize = true;
            lblInfoPaid.BackColor = Color.Transparent;
            lblInfoPaid.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInfoPaid.ForeColor = Color.FromArgb(20, 34, 51);
            lblInfoPaid.Margin = new Padding(0, 0, 8, 12);
            lblInfoPaid.MaximumSize = new Size(200, 0);
            lblInfoPaid.Name = "lblInfoPaid";
            lblInfoPaid.TabIndex = 7;
            lblInfoPaid.Text = "-";
            lblInfoPaid.UseMnemonic = false;
            // 
            // lblInfoBalanceCap
            // 
            lblInfoBalanceCap.AutoSize = true;
            lblInfoBalanceCap.BackColor = Color.Transparent;
            lblInfoBalanceCap.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInfoBalanceCap.ForeColor = Color.FromArgb(91, 107, 124);
            lblInfoBalanceCap.Margin = new Padding(0, 0, 8, 2);
            lblInfoBalanceCap.Name = "lblInfoBalanceCap";
            lblInfoBalanceCap.TabIndex = 8;
            lblInfoBalanceCap.Text = "BALANCE";
            lblInfoBalanceCap.UseMnemonic = false;
            // 
            // lblInfoStatusCap
            // 
            lblInfoStatusCap.AutoSize = true;
            lblInfoStatusCap.BackColor = Color.Transparent;
            lblInfoStatusCap.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInfoStatusCap.ForeColor = Color.FromArgb(91, 107, 124);
            lblInfoStatusCap.Margin = new Padding(0, 0, 8, 2);
            lblInfoStatusCap.Name = "lblInfoStatusCap";
            lblInfoStatusCap.TabIndex = 9;
            lblInfoStatusCap.Text = "Status";
            lblInfoStatusCap.UseMnemonic = false;
            // 
            // lblInfoBalance
            // 
            lblInfoBalance.AutoSize = true;
            lblInfoBalance.BackColor = Color.Transparent;
            lblInfoBalance.Font = new Font("Segoe UI", 21F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInfoBalance.ForeColor = Color.FromArgb(20, 34, 51);
            lblInfoBalance.Margin = new Padding(0, 0, 8, 12);
            lblInfoBalance.MaximumSize = new Size(200, 0);
            lblInfoBalance.Name = "lblInfoBalance";
            lblInfoBalance.TabIndex = 10;
            lblInfoBalance.Text = "-";
            lblInfoBalance.UseMnemonic = false;
            // 
            // lblInfoStatus
            // 
            lblInfoStatus.AutoSize = true;
            lblInfoStatus.BackColor = Color.Transparent;
            lblInfoStatus.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInfoStatus.ForeColor = Color.FromArgb(20, 34, 51);
            lblInfoStatus.Margin = new Padding(0, 0, 8, 12);
            lblInfoStatus.MaximumSize = new Size(200, 0);
            lblInfoStatus.Name = "lblInfoStatus";
            lblInfoStatus.TabIndex = 11;
            lblInfoStatus.Text = "-";
            lblInfoStatus.UseMnemonic = false;
            // 
            // pnlPayCard
            // 
            pnlPayCard.BackColor = Color.White;
            pnlPayCard.Controls.Add(tlpPayCard);
            pnlPayCard.Dock = DockStyle.Fill;
            pnlPayCard.Margin = new Padding(0);
            pnlPayCard.Name = "pnlPayCard";
            pnlPayCard.Padding = new Padding(20);
            pnlPayCard.TabIndex = 1;
            // 
            // tlpPayCard
            // 
            tlpPayCard.BackColor = Color.Transparent;
            tlpPayCard.ColumnCount = 1;
            tlpPayCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPayCard.Controls.Add(lblPayTitle, 0, 0);
            tlpPayCard.Controls.Add(pnlPayScroll, 0, 1);
            tlpPayCard.Controls.Add(tlpPayButtons, 0, 2);
            tlpPayCard.Dock = DockStyle.Fill;
            tlpPayCard.Margin = new Padding(0);
            tlpPayCard.Name = "tlpPayCard";
            tlpPayCard.RowCount = 3;
            tlpPayCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPayCard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPayCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpPayCard.TabIndex = 0;
            // 
            // lblPayTitle
            // 
            lblPayTitle.AutoSize = true;
            lblPayTitle.BackColor = Color.Transparent;
            lblPayTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPayTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblPayTitle.Margin = new Padding(0, 0, 0, 14);
            lblPayTitle.Name = "lblPayTitle";
            lblPayTitle.TabIndex = 0;
            lblPayTitle.Text = "New payment";
            // 
            // pnlPayScroll
            // 
            pnlPayScroll.AutoScroll = true;
            pnlPayScroll.BackColor = Color.Transparent;
            pnlPayScroll.Controls.Add(tlpPayFields);
            pnlPayScroll.Dock = DockStyle.Fill;
            pnlPayScroll.Margin = new Padding(0, 0, 0, 14);
            pnlPayScroll.Name = "pnlPayScroll";
            pnlPayScroll.Padding = new Padding(0, 0, 6, 0);
            pnlPayScroll.TabIndex = 1;
            // 
            // tlpPayFields
            // 
            tlpPayFields.AutoSize = true;
            tlpPayFields.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpPayFields.BackColor = Color.Transparent;
            tlpPayFields.ColumnCount = 1;
            tlpPayFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPayFields.Controls.Add(tlpPayForm, 0, 0);
            tlpPayFields.Controls.Add(lblPayNote, 0, 1);
            tlpPayFields.Dock = DockStyle.Top;
            tlpPayFields.Margin = new Padding(0);
            tlpPayFields.Name = "tlpPayFields";
            tlpPayFields.RowCount = 2;
            tlpPayFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPayFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPayFields.TabIndex = 0;
            // 
            // tlpPayForm
            // 
            tlpPayForm.AutoSize = true;
            tlpPayForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpPayForm.BackColor = Color.Transparent;
            tlpPayForm.ColumnCount = 2;
            tlpPayForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpPayForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpPayForm.Controls.Add(lblAmount, 0, 0);
            tlpPayForm.SetColumnSpan(lblAmount, 2);
            tlpPayForm.Controls.Add(tlpAmount, 0, 1);
            tlpPayForm.SetColumnSpan(tlpAmount, 2);
            tlpPayForm.Controls.Add(lblMethod, 0, 2);
            tlpPayForm.Controls.Add(cboMethod, 0, 3);
            tlpPayForm.Controls.Add(lblReference, 1, 2);
            tlpPayForm.Controls.Add(txtReference, 1, 3);
            tlpPayForm.Controls.Add(lblTendered, 0, 4);
            tlpPayForm.Controls.Add(txtTendered, 0, 5);
            tlpPayForm.Controls.Add(lblChangeCap, 1, 4);
            tlpPayForm.Controls.Add(lblChange, 1, 5);
            tlpPayForm.Controls.Add(lblReceivedByCap, 0, 6);
            tlpPayForm.SetColumnSpan(lblReceivedByCap, 2);
            tlpPayForm.Controls.Add(txtReceivedBy, 0, 7);
            tlpPayForm.SetColumnSpan(txtReceivedBy, 2);
            tlpPayForm.Dock = DockStyle.Top;
            tlpPayForm.Margin = new Padding(0);
            tlpPayForm.Name = "tlpPayForm";
            tlpPayForm.RowCount = 8;
            tlpPayForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPayForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpPayForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPayForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpPayForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPayForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpPayForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPayForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpPayForm.TabIndex = 0;
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.BackColor = Color.Transparent;
            lblAmount.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAmount.ForeColor = Color.FromArgb(20, 34, 51);
            lblAmount.Margin = new Padding(0, 0, 0, 6);
            lblAmount.Name = "lblAmount";
            lblAmount.TabIndex = 0;
            lblAmount.Text = "Amount to pay *";
            lblAmount.UseMnemonic = false;
            // 
            // tlpAmount
            // 
            tlpAmount.BackColor = Color.Transparent;
            tlpAmount.ColumnCount = 2;
            tlpAmount.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpAmount.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tlpAmount.Controls.Add(txtAmount, 0, 0);
            tlpAmount.Controls.Add(btnPayFull, 1, 0);
            tlpAmount.Dock = DockStyle.Fill;
            tlpAmount.Margin = new Padding(0, 0, 0, 14);
            tlpAmount.Name = "tlpAmount";
            tlpAmount.RowCount = 1;
            tlpAmount.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpAmount.TabIndex = 1;
            // 
            // txtAmount
            // 
            txtAmount.AutoSize = false;
            txtAmount.BackColor = Color.White;
            txtAmount.BorderStyle = BorderStyle.FixedSingle;
            txtAmount.Dock = DockStyle.Fill;
            txtAmount.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAmount.ForeColor = Color.FromArgb(20, 34, 51);
            txtAmount.Margin = new Padding(0);
            txtAmount.MaxLength = 12;
            txtAmount.Name = "txtAmount";
            txtAmount.PlaceholderText = "0.00";
            txtAmount.Size = new Size(200, 40);
            txtAmount.TabIndex = 0;
            // 
            // btnPayFull
            // 
            btnPayFull.BackColor = Color.FromArgb(226, 242, 246);
            btnPayFull.Dock = DockStyle.Fill;
            btnPayFull.FlatStyle = FlatStyle.Flat;
            btnPayFull.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPayFull.ForeColor = Color.FromArgb(20, 34, 51);
            btnPayFull.Margin = new Padding(8, 0, 0, 0);
            btnPayFull.Name = "btnPayFull";
            btnPayFull.Size = new Size(120, 40);
            btnPayFull.TabIndex = 1;
            btnPayFull.Text = "Pay full balance";
            btnPayFull.UseMnemonic = false;
            btnPayFull.UseVisualStyleBackColor = false;
            // 
            // lblMethod
            // 
            lblMethod.AutoSize = true;
            lblMethod.BackColor = Color.Transparent;
            lblMethod.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMethod.ForeColor = Color.FromArgb(20, 34, 51);
            lblMethod.Margin = new Padding(0, 0, 6, 6);
            lblMethod.Name = "lblMethod";
            lblMethod.TabIndex = 2;
            lblMethod.Text = "Payment method *";
            lblMethod.UseMnemonic = false;
            // 
            // cboMethod
            // 
            cboMethod.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMethod.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboMethod.FormattingEnabled = true;
            cboMethod.Margin = new Padding(0, 0, 6, 14);
            cboMethod.Name = "cboMethod";
            cboMethod.Size = new Size(200, 32);
            cboMethod.TabIndex = 3;
            // 
            // lblReference
            // 
            lblReference.AutoSize = true;
            lblReference.BackColor = Color.Transparent;
            lblReference.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblReference.ForeColor = Color.FromArgb(20, 34, 51);
            lblReference.Margin = new Padding(6, 0, 0, 6);
            lblReference.Name = "lblReference";
            lblReference.TabIndex = 4;
            lblReference.Text = "Reference no.";
            lblReference.UseMnemonic = false;
            // 
            // txtReference
            // 
            txtReference.AutoSize = false;
            txtReference.BackColor = Color.White;
            txtReference.BorderStyle = BorderStyle.FixedSingle;
            txtReference.Dock = DockStyle.Fill;
            txtReference.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtReference.ForeColor = Color.FromArgb(20, 34, 51);
            txtReference.Margin = new Padding(6, 0, 0, 14);
            txtReference.MaxLength = 60;
            txtReference.Name = "txtReference";
            txtReference.PlaceholderText = "Card / transaction / cheque number";
            txtReference.Size = new Size(200, 40);
            txtReference.TabIndex = 5;
            // 
            // lblTendered
            // 
            lblTendered.AutoSize = true;
            lblTendered.BackColor = Color.Transparent;
            lblTendered.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTendered.ForeColor = Color.FromArgb(20, 34, 51);
            lblTendered.Margin = new Padding(0, 0, 6, 6);
            lblTendered.Name = "lblTendered";
            lblTendered.TabIndex = 6;
            lblTendered.Text = "Cash tendered";
            lblTendered.UseMnemonic = false;
            // 
            // txtTendered
            // 
            txtTendered.AutoSize = false;
            txtTendered.BackColor = Color.White;
            txtTendered.BorderStyle = BorderStyle.FixedSingle;
            txtTendered.Dock = DockStyle.Fill;
            txtTendered.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTendered.ForeColor = Color.FromArgb(20, 34, 51);
            txtTendered.Margin = new Padding(0, 0, 6, 14);
            txtTendered.MaxLength = 12;
            txtTendered.Name = "txtTendered";
            txtTendered.PlaceholderText = "Cash only (optional)";
            txtTendered.Size = new Size(200, 40);
            txtTendered.TabIndex = 7;
            // 
            // lblChangeCap
            // 
            lblChangeCap.AutoSize = true;
            lblChangeCap.BackColor = Color.Transparent;
            lblChangeCap.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblChangeCap.ForeColor = Color.FromArgb(20, 34, 51);
            lblChangeCap.Margin = new Padding(6, 0, 0, 6);
            lblChangeCap.Name = "lblChangeCap";
            lblChangeCap.TabIndex = 8;
            lblChangeCap.Text = "Change";
            lblChangeCap.UseMnemonic = false;
            // 
            // lblChange
            // 
            lblChange.AutoSize = true;
            lblChange.BackColor = Color.Transparent;
            lblChange.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblChange.ForeColor = Color.FromArgb(20, 34, 51);
            lblChange.Margin = new Padding(6, 8, 0, 14);
            lblChange.Name = "lblChange";
            lblChange.TabIndex = 9;
            lblChange.Text = "-";
            lblChange.UseMnemonic = false;
            // 
            // lblReceivedByCap
            // 
            lblReceivedByCap.AutoSize = true;
            lblReceivedByCap.BackColor = Color.Transparent;
            lblReceivedByCap.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblReceivedByCap.ForeColor = Color.FromArgb(20, 34, 51);
            lblReceivedByCap.Margin = new Padding(0, 0, 0, 6);
            lblReceivedByCap.Name = "lblReceivedByCap";
            lblReceivedByCap.TabIndex = 10;
            lblReceivedByCap.Text = "Received by";
            lblReceivedByCap.UseMnemonic = false;
            // 
            // txtReceivedBy
            // 
            txtReceivedBy.AutoSize = false;
            txtReceivedBy.BackColor = Color.White;
            txtReceivedBy.BorderStyle = BorderStyle.FixedSingle;
            txtReceivedBy.Dock = DockStyle.Fill;
            txtReceivedBy.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtReceivedBy.ForeColor = Color.FromArgb(20, 34, 51);
            txtReceivedBy.Margin = new Padding(0, 0, 0, 14);
            txtReceivedBy.MaxLength = 100;
            txtReceivedBy.Name = "txtReceivedBy";
            txtReceivedBy.ReadOnly = true;
            txtReceivedBy.Size = new Size(200, 40);
            txtReceivedBy.TabIndex = 11;
            // 
            // lblPayNote
            // 
            lblPayNote.AutoSize = true;
            lblPayNote.BackColor = Color.Transparent;
            lblPayNote.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPayNote.ForeColor = Color.FromArgb(91, 107, 124);
            lblPayNote.Margin = new Padding(0, 0, 0, 4);
            lblPayNote.MaximumSize = new Size(420, 0);
            lblPayNote.Name = "lblPayNote";
            lblPayNote.TabIndex = 1;
            lblPayNote.UseMnemonic = false;
            // 
            // tlpPayButtons
            // 
            tlpPayButtons.BackColor = Color.Transparent;
            tlpPayButtons.ColumnCount = 2;
            tlpPayButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPayButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlpPayButtons.Controls.Add(btnRecord, 0, 0);
            tlpPayButtons.Controls.Add(btnClearPay, 1, 0);
            tlpPayButtons.Dock = DockStyle.Fill;
            tlpPayButtons.Margin = new Padding(0);
            tlpPayButtons.Name = "tlpPayButtons";
            tlpPayButtons.RowCount = 1;
            tlpPayButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPayButtons.TabIndex = 2;
            // 
            // btnRecord
            // 
            btnRecord.BackColor = Color.FromArgb(18, 122, 140);
            btnRecord.Dock = DockStyle.Fill;
            btnRecord.Enabled = false;
            btnRecord.FlatStyle = FlatStyle.Flat;
            btnRecord.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRecord.ForeColor = Color.White;
            btnRecord.Margin = new Padding(0);
            btnRecord.Name = "btnRecord";
            btnRecord.Size = new Size(120, 40);
            btnRecord.TabIndex = 0;
            btnRecord.Text = "Record Payment";
            btnRecord.UseMnemonic = false;
            btnRecord.UseVisualStyleBackColor = false;
            // 
            // btnClearPay
            // 
            btnClearPay.BackColor = Color.White;
            btnClearPay.Dock = DockStyle.Fill;
            btnClearPay.FlatStyle = FlatStyle.Flat;
            btnClearPay.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClearPay.ForeColor = Color.FromArgb(20, 34, 51);
            btnClearPay.Margin = new Padding(10, 0, 0, 0);
            btnClearPay.Name = "btnClearPay";
            btnClearPay.Size = new Size(120, 40);
            btnClearPay.TabIndex = 1;
            btnClearPay.Text = "Clear";
            btnClearPay.UseMnemonic = false;
            btnClearPay.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // PaymentForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1176, 848);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(28, 24, 28, 24);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Payment Processing and Receipts";
            Controls.Add(tlpRoot);
            Name = "PaymentForm";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            tlpMode.ResumeLayout(false);
            tlpMode.PerformLayout();
            pnlBody.ResumeLayout(false);
            pnlBody.PerformLayout();
            pnlRecords.ResumeLayout(false);
            pnlRecords.PerformLayout();
            tlpRecords.ResumeLayout(false);
            tlpRecords.PerformLayout();
            pnlRecordsCard.ResumeLayout(false);
            pnlRecordsCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecords).EndInit();
            pnlRecordsFooter.ResumeLayout(false);
            pnlRecordsFooter.PerformLayout();
            flowRecordsFooter.ResumeLayout(false);
            flowRecordsFooter.PerformLayout();
            pnlRecordsHeader.ResumeLayout(false);
            pnlRecordsHeader.PerformLayout();
            flowRecFilter.ResumeLayout(false);
            flowRecFilter.PerformLayout();
            pnlRecHistoryCard.ResumeLayout(false);
            pnlRecHistoryCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecHistory).EndInit();
            pnlRecHistoryFooter.ResumeLayout(false);
            pnlRecHistoryFooter.PerformLayout();
            flowRecHistoryActions.ResumeLayout(false);
            flowRecHistoryActions.PerformLayout();
            pnlRecHistoryHeader.ResumeLayout(false);
            pnlRecHistoryHeader.PerformLayout();
            pnlPay.ResumeLayout(false);
            pnlPay.PerformLayout();
            tlpPayMain.ResumeLayout(false);
            tlpPayMain.PerformLayout();
            tlpPayLeft.ResumeLayout(false);
            tlpPayLeft.PerformLayout();
            pnlBillsCard.ResumeLayout(false);
            pnlBillsCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayable).EndInit();
            pnlBillsFooter.ResumeLayout(false);
            pnlBillsFooter.PerformLayout();
            flowBillsFooter.ResumeLayout(false);
            flowBillsFooter.PerformLayout();
            pnlBillsHeader.ResumeLayout(false);
            pnlBillsHeader.PerformLayout();
            flowBillFilter.ResumeLayout(false);
            flowBillFilter.PerformLayout();
            pnlHistoryCard.ResumeLayout(false);
            pnlHistoryCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            pnlHistoryFooter.ResumeLayout(false);
            pnlHistoryFooter.PerformLayout();
            flowHistoryActions.ResumeLayout(false);
            flowHistoryActions.PerformLayout();
            pnlHistoryHeader.ResumeLayout(false);
            pnlHistoryHeader.PerformLayout();
            tlpPayRight.ResumeLayout(false);
            tlpPayRight.PerformLayout();
            pnlBillInfoCard.ResumeLayout(false);
            pnlBillInfoCard.PerformLayout();
            tlpBillInfoCard.ResumeLayout(false);
            tlpBillInfoCard.PerformLayout();
            tlpInfo.ResumeLayout(false);
            tlpInfo.PerformLayout();
            pnlPayCard.ResumeLayout(false);
            pnlPayCard.PerformLayout();
            tlpPayCard.ResumeLayout(false);
            tlpPayCard.PerformLayout();
            pnlPayScroll.ResumeLayout(false);
            pnlPayScroll.PerformLayout();
            tlpPayFields.ResumeLayout(false);
            tlpPayFields.PerformLayout();
            tlpPayForm.ResumeLayout(false);
            tlpPayForm.PerformLayout();
            tlpAmount.ResumeLayout(false);
            tlpAmount.PerformLayout();
            tlpPayButtons.ResumeLayout(false);
            tlpPayButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tlpMode;
        private System.Windows.Forms.Button btnModePay;
        private System.Windows.Forms.Button btnModeRecords;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlRecords;
        private System.Windows.Forms.TableLayoutPanel tlpRecords;
        private System.Windows.Forms.Panel pnlRecordsCard;
        private System.Windows.Forms.DataGridView dgvRecords;
        private System.Windows.Forms.Panel pnlRecordsFooter;
        private System.Windows.Forms.Label lblRecCount;
        private System.Windows.Forms.FlowLayoutPanel flowRecordsFooter;
        private System.Windows.Forms.Panel pnlRecordsHeader;
        private System.Windows.Forms.Label lblRecordsTitle;
        private System.Windows.Forms.FlowLayoutPanel flowRecFilter;
        private System.Windows.Forms.TextBox txtRecSearch;
        private System.Windows.Forms.Label lblRecFrom;
        private System.Windows.Forms.DateTimePicker dtpRecFrom;
        private System.Windows.Forms.Label lblRecTo;
        private System.Windows.Forms.DateTimePicker dtpRecTo;
        private System.Windows.Forms.ComboBox cboRecStatus;
        private System.Windows.Forms.Button btnRecSearch;
        private System.Windows.Forms.Button btnRecAll;
        private System.Windows.Forms.Panel pnlRecHistoryCard;
        private System.Windows.Forms.DataGridView dgvRecHistory;
        private System.Windows.Forms.Panel pnlRecHistoryFooter;
        private System.Windows.Forms.Label lblRecHistoryInfo;
        private System.Windows.Forms.FlowLayoutPanel flowRecHistoryActions;
        private System.Windows.Forms.Button btnReprint;
        private System.Windows.Forms.Panel pnlRecHistoryHeader;
        private System.Windows.Forms.Label lblRecHistoryTitle;
        private System.Windows.Forms.Panel pnlPay;
        private System.Windows.Forms.TableLayoutPanel tlpPayMain;
        private System.Windows.Forms.TableLayoutPanel tlpPayLeft;
        private System.Windows.Forms.Panel pnlBillsCard;
        private System.Windows.Forms.DataGridView dgvPayable;
        private System.Windows.Forms.Panel pnlBillsFooter;
        private System.Windows.Forms.Label lblPayableCount;
        private System.Windows.Forms.FlowLayoutPanel flowBillsFooter;
        private System.Windows.Forms.Panel pnlBillsHeader;
        private System.Windows.Forms.Label lblBillsTitle;
        private System.Windows.Forms.FlowLayoutPanel flowBillFilter;
        private System.Windows.Forms.TextBox txtBillSearch;
        private System.Windows.Forms.Button btnBillSearch;
        private System.Windows.Forms.Button btnBillAll;
        private System.Windows.Forms.Panel pnlHistoryCard;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Panel pnlHistoryFooter;
        private System.Windows.Forms.Label lblHistoryInfo;
        private System.Windows.Forms.FlowLayoutPanel flowHistoryActions;
        private System.Windows.Forms.Button btnViewReceipt;
        private System.Windows.Forms.Panel pnlHistoryHeader;
        private System.Windows.Forms.Label lblHistoryTitle;
        private System.Windows.Forms.TableLayoutPanel tlpPayRight;
        private System.Windows.Forms.Panel pnlBillInfoCard;
        private System.Windows.Forms.TableLayoutPanel tlpBillInfoCard;
        private System.Windows.Forms.Label lblBillInfoTitle;
        private System.Windows.Forms.TableLayoutPanel tlpInfo;
        private System.Windows.Forms.Label lblInfoBillCap;
        private System.Windows.Forms.Label lblInfoPatientCap;
        private System.Windows.Forms.Label lblInfoBill;
        private System.Windows.Forms.Label lblInfoPatient;
        private System.Windows.Forms.Label lblInfoTotalCap;
        private System.Windows.Forms.Label lblInfoPaidCap;
        private System.Windows.Forms.Label lblInfoTotal;
        private System.Windows.Forms.Label lblInfoPaid;
        private System.Windows.Forms.Label lblInfoBalanceCap;
        private System.Windows.Forms.Label lblInfoStatusCap;
        private System.Windows.Forms.Label lblInfoBalance;
        private System.Windows.Forms.Label lblInfoStatus;
        private System.Windows.Forms.Panel pnlPayCard;
        private System.Windows.Forms.TableLayoutPanel tlpPayCard;
        private System.Windows.Forms.Label lblPayTitle;
        private System.Windows.Forms.Panel pnlPayScroll;
        private System.Windows.Forms.TableLayoutPanel tlpPayFields;
        private System.Windows.Forms.TableLayoutPanel tlpPayForm;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TableLayoutPanel tlpAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Button btnPayFull;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.ComboBox cboMethod;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Label lblTendered;
        private System.Windows.Forms.TextBox txtTendered;
        private System.Windows.Forms.Label lblChangeCap;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Label lblReceivedByCap;
        private System.Windows.Forms.TextBox txtReceivedBy;
        private System.Windows.Forms.Label lblPayNote;
        private System.Windows.Forms.TableLayoutPanel tlpPayButtons;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnClearPay;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
