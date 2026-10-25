namespace HospitalManagementSystem.Views.MP06_ConsultationVitals
{
    partial class ConsultationForm
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

        // Redesign: page title + date line, the New consultation / History toggle with the status on the right,
        // then (entry) appointments and vital-signs cards beside the clinical-notes card, or (history) the
        // consultation table card beside the chart-note card.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblDate = new Label();
            lblTitle = new Label();
            tlpMode = new TableLayoutPanel();
            btnModeEntry = new Button();
            btnModeHistory = new Button();
            lblStatus = new Label();
            pnlBody = new Panel();
            pnlHistory = new Panel();
            tlpHistory = new TableLayoutPanel();
            pnlHistoryCard = new Panel();
            dgvHistory = new DataGridView();
            pnlHistoryFooter = new Panel();
            lblHistoryCount = new Label();
            flowHistoryFooterSpacer = new FlowLayoutPanel();
            pnlHistoryHeader = new Panel();
            lblHistoryTitle = new Label();
            flowHistoryFilter = new FlowLayoutPanel();
            txtHistorySearch = new TextBox();
            btnHistorySearch = new Button();
            btnHistoryAll = new Button();
            pnlNoteCard = new Panel();
            pnlNoteBody = new Panel();
            rtbNote = new RichTextBox();
            pnlNoteHeader = new Panel();
            lblNoteTitle = new Label();
            pnlEntry = new Panel();
            tlpEntry = new TableLayoutPanel();
            tlpEntryLeft = new TableLayoutPanel();
            pnlAppointmentCard = new Panel();
            dgvAppointments = new DataGridView();
            pnlSelectedBar = new Panel();
            lblSelected = new Label();
            pnlAppointmentHeader = new Panel();
            lblAppointmentTitle = new Label();
            flowApptFilter = new FlowLayoutPanel();
            lblDoctor = new Label();
            cboDoctor = new ComboBox();
            btnRefresh = new Button();
            pnlVitalsCard = new Panel();
            tlpVitalsCard = new TableLayoutPanel();
            lblVitalsStep = new Label();
            tlpVitals = new TableLayoutPanel();
            lblSystolic = new Label();
            txtSystolic = new TextBox();
            lblDiastolic = new Label();
            txtDiastolic = new TextBox();
            lblPulse = new Label();
            txtPulse = new TextBox();
            lblTemperature = new Label();
            txtTemperature = new TextBox();
            lblWeight = new Label();
            txtWeight = new TextBox();
            lblHeight = new Label();
            txtHeight = new TextBox();
            lblVitalsHint = new Label();
            pnlClinicalCard = new Panel();
            tlpClinicalCard = new TableLayoutPanel();
            lblClinicalStep = new Label();
            pnlClinicalScroll = new Panel();
            tlpClinical = new TableLayoutPanel();
            tlpClinicalFields = new TableLayoutPanel();
            lblComplaint = new Label();
            txtComplaint = new TextBox();
            lblFindings = new Label();
            txtFindings = new TextBox();
            lblDiagnosis = new Label();
            txtDiagnosis = new TextBox();
            lblPlan = new Label();
            txtPlan = new TextBox();
            lblInstructions = new Label();
            txtInstructions = new TextBox();
            tlpFollowUp = new TableLayoutPanel();
            chkFollowUp = new CheckBox();
            dtpFollowUp = new DateTimePicker();
            tlpEntryButtons = new TableLayoutPanel();
            btnSave = new Button();
            btnClear = new Button();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            pnlTitle.SuspendLayout();
            tlpMode.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlHistory.SuspendLayout();
            tlpHistory.SuspendLayout();
            pnlHistoryCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            pnlHistoryFooter.SuspendLayout();
            flowHistoryFooterSpacer.SuspendLayout();
            pnlHistoryHeader.SuspendLayout();
            flowHistoryFilter.SuspendLayout();
            pnlNoteCard.SuspendLayout();
            pnlNoteBody.SuspendLayout();
            pnlNoteHeader.SuspendLayout();
            pnlEntry.SuspendLayout();
            tlpEntry.SuspendLayout();
            tlpEntryLeft.SuspendLayout();
            pnlAppointmentCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).BeginInit();
            pnlSelectedBar.SuspendLayout();
            pnlAppointmentHeader.SuspendLayout();
            flowApptFilter.SuspendLayout();
            pnlVitalsCard.SuspendLayout();
            tlpVitalsCard.SuspendLayout();
            tlpVitals.SuspendLayout();
            pnlClinicalCard.SuspendLayout();
            tlpClinicalCard.SuspendLayout();
            pnlClinicalScroll.SuspendLayout();
            tlpClinical.SuspendLayout();
            tlpClinicalFields.SuspendLayout();
            tlpFollowUp.SuspendLayout();
            tlpEntryButtons.SuspendLayout();
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
            lblDate.Text = "Record vital signs and clinical notes for today's appointments.";
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
            lblTitle.Text = "Consultation & Vital Signs";
            lblTitle.UseMnemonic = false;
            // 
            // tlpMode
            // 
            tlpMode.BackColor = Color.Transparent;
            tlpMode.ColumnCount = 3;
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMode.Controls.Add(btnModeEntry, 0, 0);
            tlpMode.Controls.Add(btnModeHistory, 1, 0);
            tlpMode.Controls.Add(lblStatus, 2, 0);
            tlpMode.Dock = DockStyle.Fill;
            tlpMode.Margin = new Padding(0, 0, 0, 18);
            tlpMode.Name = "tlpMode";
            tlpMode.RowCount = 1;
            tlpMode.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMode.TabIndex = 1;
            // 
            // btnModeEntry
            // 
            btnModeEntry.BackColor = Color.FromArgb(18, 122, 140);
            btnModeEntry.FlatStyle = FlatStyle.Flat;
            btnModeEntry.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModeEntry.ForeColor = Color.White;
            btnModeEntry.Margin = new Padding(0, 0, 8, 0);
            btnModeEntry.Name = "btnModeEntry";
            btnModeEntry.Size = new Size(180, 38);
            btnModeEntry.TabIndex = 0;
            btnModeEntry.Text = "New Consultation";
            btnModeEntry.UseMnemonic = false;
            btnModeEntry.UseVisualStyleBackColor = false;
            // 
            // btnModeHistory
            // 
            btnModeHistory.BackColor = Color.White;
            btnModeHistory.FlatStyle = FlatStyle.Flat;
            btnModeHistory.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModeHistory.ForeColor = Color.FromArgb(20, 34, 51);
            btnModeHistory.Margin = new Padding(0);
            btnModeHistory.Name = "btnModeHistory";
            btnModeHistory.Size = new Size(200, 38);
            btnModeHistory.TabIndex = 1;
            btnModeHistory.Text = "History and Chart Notes";
            btnModeHistory.UseMnemonic = false;
            btnModeHistory.UseVisualStyleBackColor = false;
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
            pnlBody.Controls.Add(pnlHistory);
            pnlBody.Controls.Add(pnlEntry);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Margin = new Padding(0);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(0);
            pnlBody.TabIndex = 2;
            // 
            // pnlHistory
            // 
            pnlHistory.BackColor = Color.Transparent;
            pnlHistory.Controls.Add(tlpHistory);
            pnlHistory.Dock = DockStyle.Fill;
            pnlHistory.Margin = new Padding(0);
            pnlHistory.Name = "pnlHistory";
            pnlHistory.Padding = new Padding(0);
            pnlHistory.TabIndex = 0;
            pnlHistory.Visible = false;
            // 
            // tlpHistory
            // 
            tlpHistory.BackColor = Color.Transparent;
            tlpHistory.ColumnCount = 2;
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 440F));
            tlpHistory.Controls.Add(pnlHistoryCard, 0, 0);
            tlpHistory.Controls.Add(pnlNoteCard, 1, 0);
            tlpHistory.Dock = DockStyle.Fill;
            tlpHistory.Margin = new Padding(0);
            tlpHistory.Name = "tlpHistory";
            tlpHistory.RowCount = 1;
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpHistory.TabIndex = 0;
            // 
            // pnlHistoryCard
            // 
            pnlHistoryCard.BackColor = Color.White;
            pnlHistoryCard.Controls.Add(dgvHistory);
            pnlHistoryCard.Controls.Add(pnlHistoryFooter);
            pnlHistoryCard.Controls.Add(pnlHistoryHeader);
            pnlHistoryCard.Dock = DockStyle.Fill;
            pnlHistoryCard.Margin = new Padding(0, 0, 20, 0);
            pnlHistoryCard.Name = "pnlHistoryCard";
            pnlHistoryCard.Padding = new Padding(1, 6, 1, 1);
            pnlHistoryCard.TabIndex = 0;
            // 
            // dgvHistory
            // 
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.AllowUserToResizeRows = false;
            dgvHistory.AutoGenerateColumns = false;
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
            pnlHistoryFooter.Controls.Add(lblHistoryCount);
            pnlHistoryFooter.Controls.Add(flowHistoryFooterSpacer);
            pnlHistoryFooter.Dock = DockStyle.Bottom;
            pnlHistoryFooter.Margin = new Padding(0);
            pnlHistoryFooter.Name = "pnlHistoryFooter";
            pnlHistoryFooter.Padding = new Padding(0);
            pnlHistoryFooter.Size = new Size(600, 56);
            pnlHistoryFooter.TabIndex = 1;
            // 
            // lblHistoryCount
            // 
            lblHistoryCount.AutoEllipsis = true;
            lblHistoryCount.BackColor = Color.Transparent;
            lblHistoryCount.Dock = DockStyle.Fill;
            lblHistoryCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHistoryCount.ForeColor = Color.FromArgb(91, 107, 124);
            lblHistoryCount.Name = "lblHistoryCount";
            lblHistoryCount.Padding = new Padding(18, 0, 8, 0);
            lblHistoryCount.TabIndex = 0;
            lblHistoryCount.Text = "";
            lblHistoryCount.TextAlign = ContentAlignment.MiddleLeft;
            lblHistoryCount.UseMnemonic = false;
            // 
            // flowHistoryFooterSpacer
            // 
            flowHistoryFooterSpacer.AutoSize = true;
            flowHistoryFooterSpacer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowHistoryFooterSpacer.BackColor = Color.Transparent;
            flowHistoryFooterSpacer.Dock = DockStyle.Right;
            flowHistoryFooterSpacer.Margin = new Padding(0);
            flowHistoryFooterSpacer.Name = "flowHistoryFooterSpacer";
            flowHistoryFooterSpacer.Padding = new Padding(0);
            flowHistoryFooterSpacer.TabIndex = 1;
            flowHistoryFooterSpacer.WrapContents = false;
            // 
            // pnlHistoryHeader
            // 
            pnlHistoryHeader.BackColor = Color.White;
            pnlHistoryHeader.Controls.Add(lblHistoryTitle);
            pnlHistoryHeader.Controls.Add(flowHistoryFilter);
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
            lblHistoryTitle.Text = "Consultations recorded so far";
            lblHistoryTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblHistoryTitle.UseMnemonic = false;
            // 
            // flowHistoryFilter
            // 
            flowHistoryFilter.AutoSize = true;
            flowHistoryFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowHistoryFilter.BackColor = Color.White;
            flowHistoryFilter.Controls.Add(txtHistorySearch);
            flowHistoryFilter.Controls.Add(btnHistorySearch);
            flowHistoryFilter.Controls.Add(btnHistoryAll);
            flowHistoryFilter.Dock = DockStyle.Right;
            flowHistoryFilter.Margin = new Padding(0);
            flowHistoryFilter.Name = "flowHistoryFilter";
            flowHistoryFilter.Padding = new Padding(0, 5, 18, 0);
            flowHistoryFilter.TabIndex = 1;
            flowHistoryFilter.WrapContents = false;
            // 
            // txtHistorySearch
            // 
            txtHistorySearch.AutoSize = false;
            txtHistorySearch.BackColor = Color.White;
            txtHistorySearch.BorderStyle = BorderStyle.FixedSingle;
            txtHistorySearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtHistorySearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtHistorySearch.Margin = new Padding(0);
            txtHistorySearch.MaxLength = 60;
            txtHistorySearch.Name = "txtHistorySearch";
            txtHistorySearch.PlaceholderText = "Name or patient code";
            txtHistorySearch.Size = new Size(220, 36);
            txtHistorySearch.TabIndex = 0;
            // 
            // btnHistorySearch
            // 
            btnHistorySearch.BackColor = Color.FromArgb(226, 242, 246);
            btnHistorySearch.FlatStyle = FlatStyle.Flat;
            btnHistorySearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnHistorySearch.ForeColor = Color.FromArgb(20, 34, 51);
            btnHistorySearch.Margin = new Padding(10, 0, 0, 0);
            btnHistorySearch.Name = "btnHistorySearch";
            btnHistorySearch.Size = new Size(86, 36);
            btnHistorySearch.TabIndex = 1;
            btnHistorySearch.Text = "Search";
            btnHistorySearch.UseMnemonic = false;
            btnHistorySearch.UseVisualStyleBackColor = false;
            // 
            // btnHistoryAll
            // 
            btnHistoryAll.BackColor = Color.White;
            btnHistoryAll.FlatStyle = FlatStyle.Flat;
            btnHistoryAll.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnHistoryAll.ForeColor = Color.FromArgb(20, 34, 51);
            btnHistoryAll.Margin = new Padding(10, 0, 0, 0);
            btnHistoryAll.Name = "btnHistoryAll";
            btnHistoryAll.Size = new Size(96, 36);
            btnHistoryAll.TabIndex = 2;
            btnHistoryAll.Text = "Show All";
            btnHistoryAll.UseMnemonic = false;
            btnHistoryAll.UseVisualStyleBackColor = false;
            // 
            // pnlNoteCard
            // 
            pnlNoteCard.BackColor = Color.White;
            pnlNoteCard.Controls.Add(pnlNoteBody);
            pnlNoteCard.Controls.Add(pnlNoteHeader);
            pnlNoteCard.Dock = DockStyle.Fill;
            pnlNoteCard.Margin = new Padding(0);
            pnlNoteCard.Name = "pnlNoteCard";
            pnlNoteCard.Padding = new Padding(1, 6, 1, 1);
            pnlNoteCard.TabIndex = 1;
            // 
            // pnlNoteBody
            // 
            pnlNoteBody.BackColor = Color.White;
            pnlNoteBody.Controls.Add(rtbNote);
            pnlNoteBody.Dock = DockStyle.Fill;
            pnlNoteBody.Margin = new Padding(0);
            pnlNoteBody.Name = "pnlNoteBody";
            pnlNoteBody.Padding = new Padding(18, 8, 12, 12);
            pnlNoteBody.TabIndex = 0;
            // 
            // rtbNote
            // 
            rtbNote.BackColor = Color.White;
            rtbNote.BorderStyle = BorderStyle.None;
            rtbNote.DetectUrls = false;
            rtbNote.Dock = DockStyle.Fill;
            rtbNote.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbNote.ForeColor = Color.FromArgb(20, 34, 51);
            rtbNote.Margin = new Padding(0);
            rtbNote.Name = "rtbNote";
            rtbNote.ReadOnly = true;
            rtbNote.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbNote.TabIndex = 0;
            // 
            // pnlNoteHeader
            // 
            pnlNoteHeader.BackColor = Color.White;
            pnlNoteHeader.Controls.Add(lblNoteTitle);
            pnlNoteHeader.Dock = DockStyle.Top;
            pnlNoteHeader.Margin = new Padding(0);
            pnlNoteHeader.Name = "pnlNoteHeader";
            pnlNoteHeader.Padding = new Padding(0);
            pnlNoteHeader.Size = new Size(600, 46);
            pnlNoteHeader.TabIndex = 1;
            // 
            // lblNoteTitle
            // 
            lblNoteTitle.AutoEllipsis = true;
            lblNoteTitle.BackColor = Color.Transparent;
            lblNoteTitle.Dock = DockStyle.Fill;
            lblNoteTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoteTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblNoteTitle.Margin = new Padding(0);
            lblNoteTitle.Name = "lblNoteTitle";
            lblNoteTitle.Padding = new Padding(17, 0, 0, 0);
            lblNoteTitle.TabIndex = 0;
            lblNoteTitle.Text = "Chart note (read-only)";
            lblNoteTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblNoteTitle.UseMnemonic = false;
            // 
            // pnlEntry
            // 
            pnlEntry.BackColor = Color.Transparent;
            pnlEntry.Controls.Add(tlpEntry);
            pnlEntry.Dock = DockStyle.Fill;
            pnlEntry.Margin = new Padding(0);
            pnlEntry.Name = "pnlEntry";
            pnlEntry.Padding = new Padding(0);
            pnlEntry.TabIndex = 1;
            // 
            // tlpEntry
            // 
            tlpEntry.BackColor = Color.Transparent;
            tlpEntry.ColumnCount = 2;
            tlpEntry.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpEntry.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 440F));
            tlpEntry.Controls.Add(tlpEntryLeft, 0, 0);
            tlpEntry.Controls.Add(pnlClinicalCard, 1, 0);
            tlpEntry.Dock = DockStyle.Fill;
            tlpEntry.Margin = new Padding(0);
            tlpEntry.Name = "tlpEntry";
            tlpEntry.RowCount = 1;
            tlpEntry.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpEntry.TabIndex = 0;
            // 
            // tlpEntryLeft
            // 
            tlpEntryLeft.BackColor = Color.Transparent;
            tlpEntryLeft.ColumnCount = 1;
            tlpEntryLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpEntryLeft.Controls.Add(pnlAppointmentCard, 0, 0);
            tlpEntryLeft.Controls.Add(pnlVitalsCard, 0, 1);
            tlpEntryLeft.Dock = DockStyle.Fill;
            tlpEntryLeft.Margin = new Padding(0, 0, 20, 0);
            tlpEntryLeft.Name = "tlpEntryLeft";
            tlpEntryLeft.RowCount = 2;
            tlpEntryLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpEntryLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEntryLeft.TabIndex = 0;
            // 
            // pnlAppointmentCard
            // 
            pnlAppointmentCard.BackColor = Color.White;
            pnlAppointmentCard.Controls.Add(dgvAppointments);
            pnlAppointmentCard.Controls.Add(pnlSelectedBar);
            pnlAppointmentCard.Controls.Add(pnlAppointmentHeader);
            pnlAppointmentCard.Dock = DockStyle.Fill;
            pnlAppointmentCard.Margin = new Padding(0, 0, 0, 20);
            pnlAppointmentCard.Name = "pnlAppointmentCard";
            pnlAppointmentCard.Padding = new Padding(1, 6, 1, 1);
            pnlAppointmentCard.TabIndex = 0;
            // 
            // dgvAppointments
            // 
            dgvAppointments.AllowUserToAddRows = false;
            dgvAppointments.AllowUserToDeleteRows = false;
            dgvAppointments.AllowUserToResizeRows = false;
            dgvAppointments.AutoGenerateColumns = false;
            dgvAppointments.BackgroundColor = Color.White;
            dgvAppointments.BorderStyle = BorderStyle.None;
            dgvAppointments.Dock = DockStyle.Fill;
            dgvAppointments.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvAppointments.Margin = new Padding(0);
            dgvAppointments.MultiSelect = false;
            dgvAppointments.Name = "dgvAppointments";
            dgvAppointments.ReadOnly = true;
            dgvAppointments.RowHeadersVisible = false;
            dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppointments.TabIndex = 0;
            // 
            // pnlSelectedBar
            // 
            pnlSelectedBar.BackColor = Color.White;
            pnlSelectedBar.Controls.Add(lblSelected);
            pnlSelectedBar.Dock = DockStyle.Bottom;
            pnlSelectedBar.Margin = new Padding(0);
            pnlSelectedBar.Name = "pnlSelectedBar";
            pnlSelectedBar.Padding = new Padding(0);
            pnlSelectedBar.Size = new Size(600, 60);
            pnlSelectedBar.TabIndex = 1;
            // 
            // lblSelected
            // 
            lblSelected.BackColor = Color.Transparent;
            lblSelected.Dock = DockStyle.Fill;
            lblSelected.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSelected.ForeColor = Color.FromArgb(91, 107, 124);
            lblSelected.Margin = new Padding(0);
            lblSelected.Name = "lblSelected";
            lblSelected.Padding = new Padding(18, 0, 18, 0);
            lblSelected.TabIndex = 0;
            lblSelected.Text = "No appointment selected yet";
            lblSelected.TextAlign = ContentAlignment.MiddleLeft;
            lblSelected.UseMnemonic = false;
            // 
            // pnlAppointmentHeader
            // 
            pnlAppointmentHeader.BackColor = Color.White;
            pnlAppointmentHeader.Controls.Add(lblAppointmentTitle);
            pnlAppointmentHeader.Controls.Add(flowApptFilter);
            pnlAppointmentHeader.Dock = DockStyle.Top;
            pnlAppointmentHeader.Margin = new Padding(0);
            pnlAppointmentHeader.Name = "pnlAppointmentHeader";
            pnlAppointmentHeader.Padding = new Padding(0);
            pnlAppointmentHeader.Size = new Size(600, 46);
            pnlAppointmentHeader.TabIndex = 2;
            // 
            // lblAppointmentTitle
            // 
            lblAppointmentTitle.AutoEllipsis = true;
            lblAppointmentTitle.BackColor = Color.Transparent;
            lblAppointmentTitle.Dock = DockStyle.Fill;
            lblAppointmentTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAppointmentTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblAppointmentTitle.Margin = new Padding(0);
            lblAppointmentTitle.Name = "lblAppointmentTitle";
            lblAppointmentTitle.Padding = new Padding(17, 0, 0, 0);
            lblAppointmentTitle.TabIndex = 0;
            lblAppointmentTitle.Text = "1  ·  Today's appointments without a consultation";
            lblAppointmentTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblAppointmentTitle.UseMnemonic = false;
            // 
            // flowApptFilter
            // 
            flowApptFilter.AutoSize = true;
            flowApptFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowApptFilter.BackColor = Color.White;
            flowApptFilter.Controls.Add(lblDoctor);
            flowApptFilter.Controls.Add(cboDoctor);
            flowApptFilter.Controls.Add(btnRefresh);
            flowApptFilter.Dock = DockStyle.Right;
            flowApptFilter.Margin = new Padding(0);
            flowApptFilter.Name = "flowApptFilter";
            flowApptFilter.Padding = new Padding(0, 5, 18, 0);
            flowApptFilter.TabIndex = 1;
            flowApptFilter.WrapContents = false;
            // 
            // lblDoctor
            // 
            lblDoctor.AutoSize = true;
            lblDoctor.BackColor = Color.Transparent;
            lblDoctor.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDoctor.ForeColor = Color.FromArgb(91, 107, 124);
            lblDoctor.Margin = new Padding(0, 9, 8, 0);
            lblDoctor.Name = "lblDoctor";
            lblDoctor.TabIndex = 0;
            lblDoctor.Text = "Doctor";
            lblDoctor.UseMnemonic = false;
            // 
            // cboDoctor
            // 
            cboDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDoctor.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboDoctor.FormattingEnabled = true;
            cboDoctor.Margin = new Padding(0);
            cboDoctor.Name = "cboDoctor";
            cboDoctor.Size = new Size(240, 32);
            cboDoctor.TabIndex = 1;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRefresh.ForeColor = Color.FromArgb(20, 34, 51);
            btnRefresh.Margin = new Padding(10, 0, 0, 0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(90, 36);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseMnemonic = false;
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // pnlVitalsCard
            // 
            pnlVitalsCard.AutoSize = true;
            pnlVitalsCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlVitalsCard.BackColor = Color.White;
            pnlVitalsCard.Controls.Add(tlpVitalsCard);
            pnlVitalsCard.Dock = DockStyle.Fill;
            pnlVitalsCard.Margin = new Padding(0);
            pnlVitalsCard.Name = "pnlVitalsCard";
            pnlVitalsCard.Padding = new Padding(20, 18, 20, 16);
            pnlVitalsCard.TabIndex = 1;
            // 
            // tlpVitalsCard
            // 
            tlpVitalsCard.AutoSize = true;
            tlpVitalsCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpVitalsCard.BackColor = Color.Transparent;
            tlpVitalsCard.ColumnCount = 1;
            tlpVitalsCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpVitalsCard.Controls.Add(lblVitalsStep, 0, 0);
            tlpVitalsCard.Controls.Add(tlpVitals, 0, 1);
            tlpVitalsCard.Dock = DockStyle.Top;
            tlpVitalsCard.Margin = new Padding(0);
            tlpVitalsCard.Name = "tlpVitalsCard";
            tlpVitalsCard.RowCount = 2;
            tlpVitalsCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpVitalsCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpVitalsCard.TabIndex = 0;
            // 
            // lblVitalsStep
            // 
            lblVitalsStep.AutoSize = true;
            lblVitalsStep.BackColor = Color.Transparent;
            lblVitalsStep.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVitalsStep.ForeColor = Color.FromArgb(18, 122, 140);
            lblVitalsStep.Margin = new Padding(0, 0, 0, 14);
            lblVitalsStep.Name = "lblVitalsStep";
            lblVitalsStep.TabIndex = 0;
            lblVitalsStep.Text = "2  ·  VITAL SIGNS  (ALL REQUIRED EXCEPT HEIGHT)";
            // 
            // tlpVitals
            // 
            tlpVitals.AutoSize = true;
            tlpVitals.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpVitals.BackColor = Color.Transparent;
            tlpVitals.ColumnCount = 3;
            tlpVitals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpVitals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpVitals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpVitals.Controls.Add(lblSystolic, 0, 0);
            tlpVitals.Controls.Add(txtSystolic, 0, 1);
            tlpVitals.Controls.Add(lblDiastolic, 1, 0);
            tlpVitals.Controls.Add(txtDiastolic, 1, 1);
            tlpVitals.Controls.Add(lblPulse, 2, 0);
            tlpVitals.Controls.Add(txtPulse, 2, 1);
            tlpVitals.Controls.Add(lblTemperature, 0, 2);
            tlpVitals.Controls.Add(txtTemperature, 0, 3);
            tlpVitals.Controls.Add(lblWeight, 1, 2);
            tlpVitals.Controls.Add(txtWeight, 1, 3);
            tlpVitals.Controls.Add(lblHeight, 2, 2);
            tlpVitals.Controls.Add(txtHeight, 2, 3);
            tlpVitals.Controls.Add(lblVitalsHint, 0, 4);
            tlpVitals.SetColumnSpan(lblVitalsHint, 3);
            tlpVitals.Dock = DockStyle.Top;
            tlpVitals.Margin = new Padding(0);
            tlpVitals.Name = "tlpVitals";
            tlpVitals.RowCount = 5;
            tlpVitals.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpVitals.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpVitals.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpVitals.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpVitals.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tlpVitals.TabIndex = 1;
            // 
            // lblSystolic
            // 
            lblSystolic.AutoSize = true;
            lblSystolic.BackColor = Color.Transparent;
            lblSystolic.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSystolic.ForeColor = Color.FromArgb(20, 34, 51);
            lblSystolic.Margin = new Padding(0, 0, 6, 6);
            lblSystolic.Name = "lblSystolic";
            lblSystolic.TabIndex = 0;
            lblSystolic.Text = "Systolic BP (mmHg) *";
            lblSystolic.UseMnemonic = false;
            // 
            // txtSystolic
            // 
            txtSystolic.AutoSize = false;
            txtSystolic.BackColor = Color.White;
            txtSystolic.BorderStyle = BorderStyle.FixedSingle;
            txtSystolic.Dock = DockStyle.Fill;
            txtSystolic.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSystolic.ForeColor = Color.FromArgb(20, 34, 51);
            txtSystolic.Margin = new Padding(0, 0, 6, 14);
            txtSystolic.MaxLength = 3;
            txtSystolic.Name = "txtSystolic";
            txtSystolic.PlaceholderText = "50 - 250";
            txtSystolic.Size = new Size(200, 40);
            txtSystolic.TabIndex = 1;
            // 
            // lblDiastolic
            // 
            lblDiastolic.AutoSize = true;
            lblDiastolic.BackColor = Color.Transparent;
            lblDiastolic.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDiastolic.ForeColor = Color.FromArgb(20, 34, 51);
            lblDiastolic.Margin = new Padding(6, 0, 6, 6);
            lblDiastolic.Name = "lblDiastolic";
            lblDiastolic.TabIndex = 2;
            lblDiastolic.Text = "Diastolic BP (mmHg) *";
            lblDiastolic.UseMnemonic = false;
            // 
            // txtDiastolic
            // 
            txtDiastolic.AutoSize = false;
            txtDiastolic.BackColor = Color.White;
            txtDiastolic.BorderStyle = BorderStyle.FixedSingle;
            txtDiastolic.Dock = DockStyle.Fill;
            txtDiastolic.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDiastolic.ForeColor = Color.FromArgb(20, 34, 51);
            txtDiastolic.Margin = new Padding(6, 0, 6, 14);
            txtDiastolic.MaxLength = 3;
            txtDiastolic.Name = "txtDiastolic";
            txtDiastolic.PlaceholderText = "30 - 150";
            txtDiastolic.Size = new Size(200, 40);
            txtDiastolic.TabIndex = 3;
            // 
            // lblPulse
            // 
            lblPulse.AutoSize = true;
            lblPulse.BackColor = Color.Transparent;
            lblPulse.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPulse.ForeColor = Color.FromArgb(20, 34, 51);
            lblPulse.Margin = new Padding(6, 0, 0, 6);
            lblPulse.Name = "lblPulse";
            lblPulse.TabIndex = 4;
            lblPulse.Text = "Pulse (bpm) *";
            lblPulse.UseMnemonic = false;
            // 
            // txtPulse
            // 
            txtPulse.AutoSize = false;
            txtPulse.BackColor = Color.White;
            txtPulse.BorderStyle = BorderStyle.FixedSingle;
            txtPulse.Dock = DockStyle.Fill;
            txtPulse.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPulse.ForeColor = Color.FromArgb(20, 34, 51);
            txtPulse.Margin = new Padding(6, 0, 0, 14);
            txtPulse.MaxLength = 3;
            txtPulse.Name = "txtPulse";
            txtPulse.PlaceholderText = "20 - 250";
            txtPulse.Size = new Size(200, 40);
            txtPulse.TabIndex = 5;
            // 
            // lblTemperature
            // 
            lblTemperature.AutoSize = true;
            lblTemperature.BackColor = Color.Transparent;
            lblTemperature.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTemperature.ForeColor = Color.FromArgb(20, 34, 51);
            lblTemperature.Margin = new Padding(0, 0, 6, 6);
            lblTemperature.Name = "lblTemperature";
            lblTemperature.TabIndex = 6;
            lblTemperature.Text = "Temperature (°C) *";
            lblTemperature.UseMnemonic = false;
            // 
            // txtTemperature
            // 
            txtTemperature.AutoSize = false;
            txtTemperature.BackColor = Color.White;
            txtTemperature.BorderStyle = BorderStyle.FixedSingle;
            txtTemperature.Dock = DockStyle.Fill;
            txtTemperature.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTemperature.ForeColor = Color.FromArgb(20, 34, 51);
            txtTemperature.Margin = new Padding(0, 0, 6, 14);
            txtTemperature.MaxLength = 4;
            txtTemperature.Name = "txtTemperature";
            txtTemperature.PlaceholderText = "30 - 45";
            txtTemperature.Size = new Size(200, 40);
            txtTemperature.TabIndex = 7;
            // 
            // lblWeight
            // 
            lblWeight.AutoSize = true;
            lblWeight.BackColor = Color.Transparent;
            lblWeight.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWeight.ForeColor = Color.FromArgb(20, 34, 51);
            lblWeight.Margin = new Padding(6, 0, 6, 6);
            lblWeight.Name = "lblWeight";
            lblWeight.TabIndex = 8;
            lblWeight.Text = "Weight (kg) *";
            lblWeight.UseMnemonic = false;
            // 
            // txtWeight
            // 
            txtWeight.AutoSize = false;
            txtWeight.BackColor = Color.White;
            txtWeight.BorderStyle = BorderStyle.FixedSingle;
            txtWeight.Dock = DockStyle.Fill;
            txtWeight.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtWeight.ForeColor = Color.FromArgb(20, 34, 51);
            txtWeight.Margin = new Padding(6, 0, 6, 14);
            txtWeight.MaxLength = 5;
            txtWeight.Name = "txtWeight";
            txtWeight.PlaceholderText = "above 0";
            txtWeight.Size = new Size(200, 40);
            txtWeight.TabIndex = 9;
            // 
            // lblHeight
            // 
            lblHeight.AutoSize = true;
            lblHeight.BackColor = Color.Transparent;
            lblHeight.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHeight.ForeColor = Color.FromArgb(20, 34, 51);
            lblHeight.Margin = new Padding(6, 0, 0, 6);
            lblHeight.Name = "lblHeight";
            lblHeight.TabIndex = 10;
            lblHeight.Text = "Height (cm)";
            lblHeight.UseMnemonic = false;
            // 
            // txtHeight
            // 
            txtHeight.AutoSize = false;
            txtHeight.BackColor = Color.White;
            txtHeight.BorderStyle = BorderStyle.FixedSingle;
            txtHeight.Dock = DockStyle.Fill;
            txtHeight.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtHeight.ForeColor = Color.FromArgb(20, 34, 51);
            txtHeight.Margin = new Padding(6, 0, 0, 14);
            txtHeight.MaxLength = 5;
            txtHeight.Name = "txtHeight";
            txtHeight.PlaceholderText = "optional";
            txtHeight.Size = new Size(200, 40);
            txtHeight.TabIndex = 11;
            // 
            // lblVitalsHint
            // 
            lblVitalsHint.AutoSize = true;
            lblVitalsHint.BackColor = Color.Transparent;
            lblVitalsHint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVitalsHint.ForeColor = Color.FromArgb(91, 107, 124);
            lblVitalsHint.Margin = new Padding(0, 0, 0, 0);
            lblVitalsHint.Name = "lblVitalsHint";
            lblVitalsHint.TabIndex = 12;
            lblVitalsHint.Text = "Temperature, weight and height take one decimal place. Diastolic BP must be lower than systolic BP.";
            lblVitalsHint.UseMnemonic = false;
            // 
            // pnlClinicalCard
            // 
            pnlClinicalCard.BackColor = Color.White;
            pnlClinicalCard.Controls.Add(tlpClinicalCard);
            pnlClinicalCard.Dock = DockStyle.Fill;
            pnlClinicalCard.Margin = new Padding(0);
            pnlClinicalCard.Name = "pnlClinicalCard";
            pnlClinicalCard.Padding = new Padding(20);
            pnlClinicalCard.TabIndex = 1;
            // 
            // tlpClinicalCard
            // 
            tlpClinicalCard.BackColor = Color.Transparent;
            tlpClinicalCard.ColumnCount = 1;
            tlpClinicalCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpClinicalCard.Controls.Add(lblClinicalStep, 0, 0);
            tlpClinicalCard.Controls.Add(pnlClinicalScroll, 0, 1);
            tlpClinicalCard.Controls.Add(tlpEntryButtons, 0, 2);
            tlpClinicalCard.Dock = DockStyle.Fill;
            tlpClinicalCard.Margin = new Padding(0);
            tlpClinicalCard.Name = "tlpClinicalCard";
            tlpClinicalCard.RowCount = 3;
            tlpClinicalCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpClinicalCard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpClinicalCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpClinicalCard.TabIndex = 0;
            // 
            // lblClinicalStep
            // 
            lblClinicalStep.AutoSize = true;
            lblClinicalStep.BackColor = Color.Transparent;
            lblClinicalStep.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblClinicalStep.ForeColor = Color.FromArgb(18, 122, 140);
            lblClinicalStep.Margin = new Padding(0, 0, 0, 14);
            lblClinicalStep.Name = "lblClinicalStep";
            lblClinicalStep.TabIndex = 0;
            lblClinicalStep.Text = "3  ·  CLINICAL NOTES";
            // 
            // pnlClinicalScroll
            // 
            pnlClinicalScroll.AutoScroll = true;
            pnlClinicalScroll.BackColor = Color.Transparent;
            pnlClinicalScroll.Controls.Add(tlpClinical);
            pnlClinicalScroll.Dock = DockStyle.Fill;
            pnlClinicalScroll.Margin = new Padding(0, 0, 0, 14);
            pnlClinicalScroll.Name = "pnlClinicalScroll";
            pnlClinicalScroll.Padding = new Padding(0, 0, 6, 0);
            pnlClinicalScroll.TabIndex = 1;
            // 
            // tlpClinical
            // 
            tlpClinical.AutoSize = true;
            tlpClinical.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpClinical.BackColor = Color.Transparent;
            tlpClinical.ColumnCount = 1;
            tlpClinical.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpClinical.Controls.Add(tlpClinicalFields, 0, 0);
            tlpClinical.Controls.Add(tlpFollowUp, 0, 1);
            tlpClinical.Dock = DockStyle.Top;
            tlpClinical.Margin = new Padding(0);
            tlpClinical.Name = "tlpClinical";
            tlpClinical.RowCount = 2;
            tlpClinical.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpClinical.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpClinical.TabIndex = 0;
            // 
            // tlpClinicalFields
            // 
            tlpClinicalFields.AutoSize = true;
            tlpClinicalFields.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpClinicalFields.BackColor = Color.Transparent;
            tlpClinicalFields.ColumnCount = 1;
            tlpClinicalFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpClinicalFields.Controls.Add(lblComplaint, 0, 0);
            tlpClinicalFields.Controls.Add(txtComplaint, 0, 1);
            tlpClinicalFields.Controls.Add(lblFindings, 0, 2);
            tlpClinicalFields.Controls.Add(txtFindings, 0, 3);
            tlpClinicalFields.Controls.Add(lblDiagnosis, 0, 4);
            tlpClinicalFields.Controls.Add(txtDiagnosis, 0, 5);
            tlpClinicalFields.Controls.Add(lblPlan, 0, 6);
            tlpClinicalFields.Controls.Add(txtPlan, 0, 7);
            tlpClinicalFields.Controls.Add(lblInstructions, 0, 8);
            tlpClinicalFields.Controls.Add(txtInstructions, 0, 9);
            tlpClinicalFields.Dock = DockStyle.Top;
            tlpClinicalFields.Margin = new Padding(0);
            tlpClinicalFields.Name = "tlpClinicalFields";
            tlpClinicalFields.RowCount = 10;
            tlpClinicalFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpClinicalFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            tlpClinicalFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpClinicalFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            tlpClinicalFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpClinicalFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            tlpClinicalFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpClinicalFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            tlpClinicalFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpClinicalFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            tlpClinicalFields.TabIndex = 0;
            // 
            // lblComplaint
            // 
            lblComplaint.AutoSize = true;
            lblComplaint.BackColor = Color.Transparent;
            lblComplaint.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblComplaint.ForeColor = Color.FromArgb(20, 34, 51);
            lblComplaint.Margin = new Padding(0, 0, 0, 6);
            lblComplaint.Name = "lblComplaint";
            lblComplaint.TabIndex = 0;
            lblComplaint.Text = "Chief complaint *";
            lblComplaint.UseMnemonic = false;
            // 
            // txtComplaint
            // 
            txtComplaint.AutoSize = false;
            txtComplaint.BackColor = Color.White;
            txtComplaint.BorderStyle = BorderStyle.FixedSingle;
            txtComplaint.Dock = DockStyle.Fill;
            txtComplaint.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtComplaint.ForeColor = Color.FromArgb(20, 34, 51);
            txtComplaint.Margin = new Padding(0, 0, 0, 14);
            txtComplaint.MaxLength = 500;
            txtComplaint.Multiline = true;
            txtComplaint.Name = "txtComplaint";
            txtComplaint.PlaceholderText = "Why the patient came - required";
            txtComplaint.ScrollBars = ScrollBars.Vertical;
            txtComplaint.Size = new Size(200, 64);
            txtComplaint.TabIndex = 1;
            // 
            // lblFindings
            // 
            lblFindings.AutoSize = true;
            lblFindings.BackColor = Color.Transparent;
            lblFindings.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFindings.ForeColor = Color.FromArgb(20, 34, 51);
            lblFindings.Margin = new Padding(0, 0, 0, 6);
            lblFindings.Name = "lblFindings";
            lblFindings.TabIndex = 2;
            lblFindings.Text = "Examination findings";
            lblFindings.UseMnemonic = false;
            // 
            // txtFindings
            // 
            txtFindings.AutoSize = false;
            txtFindings.BackColor = Color.White;
            txtFindings.BorderStyle = BorderStyle.FixedSingle;
            txtFindings.Dock = DockStyle.Fill;
            txtFindings.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFindings.ForeColor = Color.FromArgb(20, 34, 51);
            txtFindings.Margin = new Padding(0, 0, 0, 14);
            txtFindings.MaxLength = 1000;
            txtFindings.Multiline = true;
            txtFindings.Name = "txtFindings";
            txtFindings.PlaceholderText = "Optional";
            txtFindings.ScrollBars = ScrollBars.Vertical;
            txtFindings.Size = new Size(200, 64);
            txtFindings.TabIndex = 3;
            // 
            // lblDiagnosis
            // 
            lblDiagnosis.AutoSize = true;
            lblDiagnosis.BackColor = Color.Transparent;
            lblDiagnosis.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDiagnosis.ForeColor = Color.FromArgb(20, 34, 51);
            lblDiagnosis.Margin = new Padding(0, 0, 0, 6);
            lblDiagnosis.Name = "lblDiagnosis";
            lblDiagnosis.TabIndex = 4;
            lblDiagnosis.Text = "Diagnosis * (at least one)";
            lblDiagnosis.UseMnemonic = false;
            // 
            // txtDiagnosis
            // 
            txtDiagnosis.AutoSize = false;
            txtDiagnosis.BackColor = Color.White;
            txtDiagnosis.BorderStyle = BorderStyle.FixedSingle;
            txtDiagnosis.Dock = DockStyle.Fill;
            txtDiagnosis.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDiagnosis.ForeColor = Color.FromArgb(20, 34, 51);
            txtDiagnosis.Margin = new Padding(0, 0, 0, 14);
            txtDiagnosis.MaxLength = 500;
            txtDiagnosis.Multiline = true;
            txtDiagnosis.Name = "txtDiagnosis";
            txtDiagnosis.PlaceholderText = "Required";
            txtDiagnosis.ScrollBars = ScrollBars.Vertical;
            txtDiagnosis.Size = new Size(200, 64);
            txtDiagnosis.TabIndex = 5;
            // 
            // lblPlan
            // 
            lblPlan.AutoSize = true;
            lblPlan.BackColor = Color.Transparent;
            lblPlan.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPlan.ForeColor = Color.FromArgb(20, 34, 51);
            lblPlan.Margin = new Padding(0, 0, 0, 6);
            lblPlan.Name = "lblPlan";
            lblPlan.TabIndex = 6;
            lblPlan.Text = "Treatment plan";
            lblPlan.UseMnemonic = false;
            // 
            // txtPlan
            // 
            txtPlan.AutoSize = false;
            txtPlan.BackColor = Color.White;
            txtPlan.BorderStyle = BorderStyle.FixedSingle;
            txtPlan.Dock = DockStyle.Fill;
            txtPlan.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPlan.ForeColor = Color.FromArgb(20, 34, 51);
            txtPlan.Margin = new Padding(0, 0, 0, 14);
            txtPlan.MaxLength = 1000;
            txtPlan.Multiline = true;
            txtPlan.Name = "txtPlan";
            txtPlan.PlaceholderText = "Optional";
            txtPlan.ScrollBars = ScrollBars.Vertical;
            txtPlan.Size = new Size(200, 64);
            txtPlan.TabIndex = 7;
            // 
            // lblInstructions
            // 
            lblInstructions.AutoSize = true;
            lblInstructions.BackColor = Color.Transparent;
            lblInstructions.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInstructions.ForeColor = Color.FromArgb(20, 34, 51);
            lblInstructions.Margin = new Padding(0, 0, 0, 6);
            lblInstructions.Name = "lblInstructions";
            lblInstructions.TabIndex = 8;
            lblInstructions.Text = "Special instructions";
            lblInstructions.UseMnemonic = false;
            // 
            // txtInstructions
            // 
            txtInstructions.AutoSize = false;
            txtInstructions.BackColor = Color.White;
            txtInstructions.BorderStyle = BorderStyle.FixedSingle;
            txtInstructions.Dock = DockStyle.Fill;
            txtInstructions.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtInstructions.ForeColor = Color.FromArgb(20, 34, 51);
            txtInstructions.Margin = new Padding(0, 0, 0, 14);
            txtInstructions.MaxLength = 500;
            txtInstructions.Multiline = true;
            txtInstructions.Name = "txtInstructions";
            txtInstructions.PlaceholderText = "Optional - advice for the patient";
            txtInstructions.ScrollBars = ScrollBars.Vertical;
            txtInstructions.Size = new Size(200, 64);
            txtInstructions.TabIndex = 9;
            // 
            // tlpFollowUp
            // 
            tlpFollowUp.AutoSize = false;
            tlpFollowUp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpFollowUp.BackColor = Color.Transparent;
            tlpFollowUp.ColumnCount = 2;
            tlpFollowUp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpFollowUp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFollowUp.Controls.Add(chkFollowUp, 0, 0);
            tlpFollowUp.Controls.Add(dtpFollowUp, 1, 0);
            tlpFollowUp.Dock = DockStyle.Top;
            tlpFollowUp.Margin = new Padding(0, 0, 0, 4);
            tlpFollowUp.Name = "tlpFollowUp";
            tlpFollowUp.RowCount = 1;
            tlpFollowUp.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpFollowUp.Size = new Size(400, 40);
            tlpFollowUp.TabIndex = 1;
            // 
            // chkFollowUp
            // 
            chkFollowUp.AutoSize = true;
            chkFollowUp.BackColor = Color.Transparent;
            chkFollowUp.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkFollowUp.ForeColor = Color.FromArgb(20, 34, 51);
            chkFollowUp.Margin = new Padding(0, 9, 12, 0);
            chkFollowUp.Name = "chkFollowUp";
            chkFollowUp.TabIndex = 0;
            chkFollowUp.Text = "Schedule next follow-up";
            chkFollowUp.UseMnemonic = false;
            chkFollowUp.UseVisualStyleBackColor = false;
            // 
            // dtpFollowUp
            // 
            dtpFollowUp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpFollowUp.CustomFormat = "ddd, MMM dd, yyyy";
            dtpFollowUp.Enabled = false;
            dtpFollowUp.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFollowUp.Format = DateTimePickerFormat.Custom;
            dtpFollowUp.Margin = new Padding(0);
            dtpFollowUp.Name = "dtpFollowUp";
            dtpFollowUp.Size = new Size(200, 31);
            dtpFollowUp.TabIndex = 1;
            // 
            // tlpEntryButtons
            // 
            tlpEntryButtons.BackColor = Color.Transparent;
            tlpEntryButtons.ColumnCount = 2;
            tlpEntryButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpEntryButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tlpEntryButtons.Controls.Add(btnSave, 0, 0);
            tlpEntryButtons.Controls.Add(btnClear, 1, 0);
            tlpEntryButtons.Dock = DockStyle.Fill;
            tlpEntryButtons.Margin = new Padding(0);
            tlpEntryButtons.Name = "tlpEntryButtons";
            tlpEntryButtons.RowCount = 1;
            tlpEntryButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpEntryButtons.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(18, 122, 140);
            btnSave.Dock = DockStyle.Fill;
            btnSave.Enabled = false;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.White;
            btnSave.Margin = new Padding(0);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 40);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save Consultation";
            btnSave.UseMnemonic = false;
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            btnClear.Dock = DockStyle.Fill;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClear.ForeColor = Color.FromArgb(20, 34, 51);
            btnClear.Margin = new Padding(10, 0, 0, 0);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(120, 40);
            btnClear.TabIndex = 1;
            btnClear.Text = "Clear Form";
            btnClear.UseMnemonic = false;
            btnClear.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // ConsultationForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1176, 848);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(28, 24, 28, 24);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Consultation & Vital Signs";
            Controls.Add(tlpRoot);
            Name = "ConsultationForm";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            tlpMode.ResumeLayout(false);
            tlpMode.PerformLayout();
            pnlBody.ResumeLayout(false);
            pnlBody.PerformLayout();
            pnlHistory.ResumeLayout(false);
            pnlHistory.PerformLayout();
            tlpHistory.ResumeLayout(false);
            tlpHistory.PerformLayout();
            pnlHistoryCard.ResumeLayout(false);
            pnlHistoryCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            pnlHistoryFooter.ResumeLayout(false);
            pnlHistoryFooter.PerformLayout();
            flowHistoryFooterSpacer.ResumeLayout(false);
            flowHistoryFooterSpacer.PerformLayout();
            pnlHistoryHeader.ResumeLayout(false);
            pnlHistoryHeader.PerformLayout();
            flowHistoryFilter.ResumeLayout(false);
            flowHistoryFilter.PerformLayout();
            pnlNoteCard.ResumeLayout(false);
            pnlNoteCard.PerformLayout();
            pnlNoteBody.ResumeLayout(false);
            pnlNoteBody.PerformLayout();
            pnlNoteHeader.ResumeLayout(false);
            pnlNoteHeader.PerformLayout();
            pnlEntry.ResumeLayout(false);
            pnlEntry.PerformLayout();
            tlpEntry.ResumeLayout(false);
            tlpEntry.PerformLayout();
            tlpEntryLeft.ResumeLayout(false);
            tlpEntryLeft.PerformLayout();
            pnlAppointmentCard.ResumeLayout(false);
            pnlAppointmentCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).EndInit();
            pnlSelectedBar.ResumeLayout(false);
            pnlSelectedBar.PerformLayout();
            pnlAppointmentHeader.ResumeLayout(false);
            pnlAppointmentHeader.PerformLayout();
            flowApptFilter.ResumeLayout(false);
            flowApptFilter.PerformLayout();
            pnlVitalsCard.ResumeLayout(false);
            pnlVitalsCard.PerformLayout();
            tlpVitalsCard.ResumeLayout(false);
            tlpVitalsCard.PerformLayout();
            tlpVitals.ResumeLayout(false);
            tlpVitals.PerformLayout();
            pnlClinicalCard.ResumeLayout(false);
            pnlClinicalCard.PerformLayout();
            tlpClinicalCard.ResumeLayout(false);
            tlpClinicalCard.PerformLayout();
            pnlClinicalScroll.ResumeLayout(false);
            pnlClinicalScroll.PerformLayout();
            tlpClinical.ResumeLayout(false);
            tlpClinical.PerformLayout();
            tlpClinicalFields.ResumeLayout(false);
            tlpClinicalFields.PerformLayout();
            tlpFollowUp.ResumeLayout(false);
            tlpFollowUp.PerformLayout();
            tlpEntryButtons.ResumeLayout(false);
            tlpEntryButtons.PerformLayout();
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
        private System.Windows.Forms.Button btnModeEntry;
        private System.Windows.Forms.Button btnModeHistory;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlHistory;
        private System.Windows.Forms.TableLayoutPanel tlpHistory;
        private System.Windows.Forms.Panel pnlHistoryCard;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Panel pnlHistoryFooter;
        private System.Windows.Forms.Label lblHistoryCount;
        private System.Windows.Forms.FlowLayoutPanel flowHistoryFooterSpacer;
        private System.Windows.Forms.Panel pnlHistoryHeader;
        private System.Windows.Forms.Label lblHistoryTitle;
        private System.Windows.Forms.FlowLayoutPanel flowHistoryFilter;
        private System.Windows.Forms.TextBox txtHistorySearch;
        private System.Windows.Forms.Button btnHistorySearch;
        private System.Windows.Forms.Button btnHistoryAll;
        private System.Windows.Forms.Panel pnlNoteCard;
        private System.Windows.Forms.Panel pnlNoteBody;
        private System.Windows.Forms.RichTextBox rtbNote;
        private System.Windows.Forms.Panel pnlNoteHeader;
        private System.Windows.Forms.Label lblNoteTitle;
        private System.Windows.Forms.Panel pnlEntry;
        private System.Windows.Forms.TableLayoutPanel tlpEntry;
        private System.Windows.Forms.TableLayoutPanel tlpEntryLeft;
        private System.Windows.Forms.Panel pnlAppointmentCard;
        private System.Windows.Forms.DataGridView dgvAppointments;
        private System.Windows.Forms.Panel pnlSelectedBar;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Panel pnlAppointmentHeader;
        private System.Windows.Forms.Label lblAppointmentTitle;
        private System.Windows.Forms.FlowLayoutPanel flowApptFilter;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.ComboBox cboDoctor;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlVitalsCard;
        private System.Windows.Forms.TableLayoutPanel tlpVitalsCard;
        private System.Windows.Forms.Label lblVitalsStep;
        private System.Windows.Forms.TableLayoutPanel tlpVitals;
        private System.Windows.Forms.Label lblSystolic;
        private System.Windows.Forms.TextBox txtSystolic;
        private System.Windows.Forms.Label lblDiastolic;
        private System.Windows.Forms.TextBox txtDiastolic;
        private System.Windows.Forms.Label lblPulse;
        private System.Windows.Forms.TextBox txtPulse;
        private System.Windows.Forms.Label lblTemperature;
        private System.Windows.Forms.TextBox txtTemperature;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label lblVitalsHint;
        private System.Windows.Forms.Panel pnlClinicalCard;
        private System.Windows.Forms.TableLayoutPanel tlpClinicalCard;
        private System.Windows.Forms.Label lblClinicalStep;
        private System.Windows.Forms.Panel pnlClinicalScroll;
        private System.Windows.Forms.TableLayoutPanel tlpClinical;
        private System.Windows.Forms.TableLayoutPanel tlpClinicalFields;
        private System.Windows.Forms.Label lblComplaint;
        private System.Windows.Forms.TextBox txtComplaint;
        private System.Windows.Forms.Label lblFindings;
        private System.Windows.Forms.TextBox txtFindings;
        private System.Windows.Forms.Label lblDiagnosis;
        private System.Windows.Forms.TextBox txtDiagnosis;
        private System.Windows.Forms.Label lblPlan;
        private System.Windows.Forms.TextBox txtPlan;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.TextBox txtInstructions;
        private System.Windows.Forms.TableLayoutPanel tlpFollowUp;
        private System.Windows.Forms.CheckBox chkFollowUp;
        private System.Windows.Forms.DateTimePicker dtpFollowUp;
        private System.Windows.Forms.TableLayoutPanel tlpEntryButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
