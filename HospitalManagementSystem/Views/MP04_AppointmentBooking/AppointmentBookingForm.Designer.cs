namespace HospitalManagementSystem.Views.MP04_AppointmentBooking
{
    partial class AppointmentBookingForm
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

        // Layout = redesign mockup, artboard "3 · MP04 Appointment Booking" (px at 96 DPI):
        //   page padding 24 / 28, title + one-line hint, then a 440 px booking card beside the appointments card.
        //   Booking card (20 px padding, 14 px gaps): 1 · PATIENT (search, result list when several match, selected
        //   patient chip), 2 · DOCTOR, DATE AND TIME (specialization + date, doctor, time-slot buttons, visit type +
        //   reason), Book Appointment + Clear at the bottom.
        //   Appointments card: title and filters, the list, the selected appointment and the four actions.
        // cboSlot stays (hidden): the slot buttons write into it, so the booking logic is unchanged.

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblTitle = new Label();
            lblHint = new Label();
            tlpMain = new TableLayoutPanel();
            pnlBookingCard = new Panel();
            tlpLeft = new TableLayoutPanel();
            lblStep1 = new Label();
            tlpPatientSearch = new TableLayoutPanel();
            txtPatientSearch = new TextBox();
            btnPatientSearch = new Button();
            dgvPatients = new DataGridView();
            lblSelectedPatient = new Label();
            pnlPatientChip = new Panel();
            lblPatientAvatar = new Label();
            lblPatientName = new Label();
            lblPatientDetail = new Label();
            lblStep2 = new Label();
            tlpSpecDate = new TableLayoutPanel();
            lblSpec = new Label();
            lblDate = new Label();
            cboSpecialization = new ComboBox();
            dtpDate = new DateTimePicker();
            lblDoctor = new Label();
            cboDoctor = new ComboBox();
            flpSlotHeader = new FlowLayoutPanel();
            lblSlot = new Label();
            lblSlotCount = new Label();
            flpSlots = new FlowLayoutPanel();
            lblSlotInfo = new Label();
            tlpTypeReason = new TableLayoutPanel();
            lblType = new Label();
            lblReason = new Label();
            cboType = new ComboBox();
            txtReason = new TextBox();
            tlpBookButtons = new TableLayoutPanel();
            btnBook = new Button();
            btnClearBooking = new Button();
            cboSlot = new ComboBox();
            pnlListCard = new Panel();
            dgvAppointments = new DataGridView();
            pnlListFooter = new Panel();
            lblSummary = new Label();
            flpActions = new FlowLayoutPanel();
            btnReschedule = new Button();
            btnNoShow = new Button();
            btnComplete = new Button();
            btnCancelAppt = new Button();
            pnlListHeader = new Panel();
            lblListTitle = new Label();
            flpFilters = new FlowLayoutPanel();
            dtpFilterDate = new DateTimePicker();
            cboFilterDoctor = new ComboBox();
            cboFilterStatus = new ComboBox();
            txtFilterPatient = new TextBox();
            btnClearFilters = new Button();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            pnlTitle.SuspendLayout();
            tlpMain.SuspendLayout();
            pnlBookingCard.SuspendLayout();
            tlpLeft.SuspendLayout();
            tlpPatientSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPatients).BeginInit();
            pnlPatientChip.SuspendLayout();
            tlpSpecDate.SuspendLayout();
            flpSlotHeader.SuspendLayout();
            tlpTypeReason.SuspendLayout();
            tlpBookButtons.SuspendLayout();
            pnlListCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).BeginInit();
            pnlListFooter.SuspendLayout();
            flpActions.SuspendLayout();
            pnlListHeader.SuspendLayout();
            flpFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            //
            // tlpRoot
            //
            tlpRoot.BackColor = Color.FromArgb(243, 246, 249);
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(pnlTitle, 0, 0);
            tlpRoot.Controls.Add(tlpMain, 0, 1);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 2;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.Size = new Size(1120, 800);
            tlpRoot.TabIndex = 0;
            //
            // pnlTitle
            //
            pnlTitle.BackColor = Color.Transparent;
            pnlTitle.Controls.Add(lblHint);
            pnlTitle.Controls.Add(lblTitle);
            pnlTitle.Dock = DockStyle.Fill;
            pnlTitle.Margin = new Padding(0, 0, 0, 18);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Size = new Size(1120, 56);
            pnlTitle.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblTitle.Location = new Point(-4, -2);
            lblTitle.Name = "lblTitle";
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Appointment Booking";
            //
            // lblHint
            //
            lblHint.AutoSize = true;
            lblHint.BackColor = Color.Transparent;
            lblHint.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHint.ForeColor = Color.FromArgb(91, 107, 124);
            lblHint.Location = new Point(-2, 34);
            lblHint.Name = "lblHint";
            lblHint.TabIndex = 1;
            lblHint.Text = "Book a patient with a doctor, then manage the day's appointments on the right.";
            //
            // tlpMain
            //
            tlpMain.BackColor = Color.Transparent;
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 460F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(pnlBookingCard, 0, 0);
            tlpMain.Controls.Add(pnlListCard, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Margin = new Padding(0);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(1120, 726);
            tlpMain.TabIndex = 1;
            //
            // pnlBookingCard
            //
            pnlBookingCard.BackColor = Color.White;
            pnlBookingCard.Controls.Add(tlpLeft);
            pnlBookingCard.Controls.Add(cboSlot);
            pnlBookingCard.Dock = DockStyle.Fill;
            pnlBookingCard.Margin = new Padding(0, 0, 20, 0);
            pnlBookingCard.Name = "pnlBookingCard";
            pnlBookingCard.Padding = new Padding(20);
            pnlBookingCard.Size = new Size(440, 726);
            pnlBookingCard.TabIndex = 0;
            //
            // tlpLeft
            //
            tlpLeft.BackColor = Color.White;
            tlpLeft.ColumnCount = 1;
            tlpLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpLeft.Controls.Add(lblStep1, 0, 0);
            tlpLeft.Controls.Add(tlpPatientSearch, 0, 1);
            tlpLeft.Controls.Add(dgvPatients, 0, 2);
            tlpLeft.Controls.Add(lblSelectedPatient, 0, 3);
            tlpLeft.Controls.Add(pnlPatientChip, 0, 4);
            tlpLeft.Controls.Add(lblStep2, 0, 5);
            tlpLeft.Controls.Add(tlpSpecDate, 0, 6);
            tlpLeft.Controls.Add(lblDoctor, 0, 7);
            tlpLeft.Controls.Add(cboDoctor, 0, 8);
            tlpLeft.Controls.Add(flpSlotHeader, 0, 9);
            tlpLeft.Controls.Add(flpSlots, 0, 10);
            tlpLeft.Controls.Add(lblSlotInfo, 0, 11);
            tlpLeft.Controls.Add(tlpTypeReason, 0, 12);
            tlpLeft.Controls.Add(tlpBookButtons, 0, 14);
            tlpLeft.Dock = DockStyle.Fill;
            tlpLeft.Margin = new Padding(0);
            tlpLeft.Name = "tlpLeft";
            tlpLeft.RowCount = 15;
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.Size = new Size(400, 686);
            tlpLeft.TabIndex = 0;
            //
            // lblStep1
            //
            lblStep1.AutoSize = true;
            lblStep1.BackColor = Color.Transparent;
            lblStep1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStep1.ForeColor = Color.FromArgb(18, 122, 140);
            lblStep1.Margin = new Padding(0, 0, 0, 14);
            lblStep1.Name = "lblStep1";
            lblStep1.TabIndex = 20;
            lblStep1.Text = "1  ·  PATIENT";
            //
            // tlpPatientSearch
            //
            tlpPatientSearch.BackColor = Color.White;
            tlpPatientSearch.ColumnCount = 2;
            tlpPatientSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPatientSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 92F));
            tlpPatientSearch.Controls.Add(txtPatientSearch, 0, 0);
            tlpPatientSearch.Controls.Add(btnPatientSearch, 1, 0);
            tlpPatientSearch.Dock = DockStyle.Fill;
            tlpPatientSearch.Margin = new Padding(0, 0, 0, 14);
            tlpPatientSearch.Name = "tlpPatientSearch";
            tlpPatientSearch.RowCount = 1;
            tlpPatientSearch.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpPatientSearch.Size = new Size(400, 40);
            tlpPatientSearch.TabIndex = 0;
            //
            // txtPatientSearch
            //
            txtPatientSearch.AutoSize = false;
            txtPatientSearch.BackColor = Color.White;
            txtPatientSearch.BorderStyle = BorderStyle.FixedSingle;
            txtPatientSearch.Dock = DockStyle.Fill;
            txtPatientSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPatientSearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtPatientSearch.Margin = new Padding(0);
            txtPatientSearch.MaxLength = 60;
            txtPatientSearch.Name = "txtPatientSearch";
            txtPatientSearch.PlaceholderText = "Name, patient ID or phone number";
            txtPatientSearch.Size = new Size(308, 40);
            txtPatientSearch.TabIndex = 0;
            //
            // btnPatientSearch
            //
            btnPatientSearch.Dock = DockStyle.Fill;
            btnPatientSearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPatientSearch.Margin = new Padding(8, 0, 0, 0);
            btnPatientSearch.Name = "btnPatientSearch";
            btnPatientSearch.Size = new Size(84, 40);
            btnPatientSearch.TabIndex = 1;
            btnPatientSearch.Text = "Search";
            //
            // dgvPatients
            //
            dgvPatients.BackgroundColor = Color.White;
            dgvPatients.BorderStyle = BorderStyle.None;
            dgvPatients.ColumnHeadersVisible = false;
            dgvPatients.Dock = DockStyle.Fill;
            dgvPatients.Margin = new Padding(0, 0, 0, 14);
            dgvPatients.Name = "dgvPatients";
            dgvPatients.RowHeadersVisible = false;
            dgvPatients.Size = new Size(400, 132);
            dgvPatients.TabIndex = 2;
            dgvPatients.Visible = false;
            //
            // lblSelectedPatient
            //
            lblSelectedPatient.AutoSize = true;
            lblSelectedPatient.BackColor = Color.Transparent;
            lblSelectedPatient.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSelectedPatient.ForeColor = Color.FromArgb(91, 107, 124);
            lblSelectedPatient.Margin = new Padding(0, 0, 0, 14);
            lblSelectedPatient.MaximumSize = new Size(400, 0);
            lblSelectedPatient.Name = "lblSelectedPatient";
            lblSelectedPatient.TabIndex = 21;
            lblSelectedPatient.Text = "No patient selected yet";
            //
            // pnlPatientChip
            //
            pnlPatientChip.BackColor = Color.FromArgb(243, 250, 251);
            pnlPatientChip.Controls.Add(lblPatientDetail);
            pnlPatientChip.Controls.Add(lblPatientName);
            pnlPatientChip.Controls.Add(lblPatientAvatar);
            pnlPatientChip.Dock = DockStyle.Fill;
            pnlPatientChip.Margin = new Padding(0, 0, 0, 14);
            pnlPatientChip.Name = "pnlPatientChip";
            pnlPatientChip.Size = new Size(400, 56);
            pnlPatientChip.TabIndex = 22;
            pnlPatientChip.Visible = false;
            //
            // lblPatientAvatar
            //
            lblPatientAvatar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPatientAvatar.ForeColor = Color.White;
            lblPatientAvatar.Location = new Point(12, 11);
            lblPatientAvatar.Name = "lblPatientAvatar";
            lblPatientAvatar.Size = new Size(34, 34);
            lblPatientAvatar.TabIndex = 0;
            lblPatientAvatar.Text = "LA";
            //
            // lblPatientName
            //
            lblPatientName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPatientName.AutoEllipsis = true;
            lblPatientName.BackColor = Color.Transparent;
            lblPatientName.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPatientName.ForeColor = Color.FromArgb(20, 34, 51);
            lblPatientName.Location = new Point(58, 8);
            lblPatientName.Name = "lblPatientName";
            lblPatientName.Size = new Size(330, 21);
            lblPatientName.TabIndex = 1;
            lblPatientName.UseMnemonic = false;
            //
            // lblPatientDetail
            //
            lblPatientDetail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPatientDetail.AutoEllipsis = true;
            lblPatientDetail.BackColor = Color.Transparent;
            lblPatientDetail.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPatientDetail.ForeColor = Color.FromArgb(91, 107, 124);
            lblPatientDetail.Location = new Point(58, 29);
            lblPatientDetail.Name = "lblPatientDetail";
            lblPatientDetail.Size = new Size(330, 18);
            lblPatientDetail.TabIndex = 2;
            //
            // lblStep2
            //
            lblStep2.AutoSize = true;
            lblStep2.BackColor = Color.Transparent;
            lblStep2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStep2.ForeColor = Color.FromArgb(18, 122, 140);
            lblStep2.Margin = new Padding(0, 4, 0, 14);
            lblStep2.Name = "lblStep2";
            lblStep2.TabIndex = 23;
            lblStep2.Text = "2  ·  DOCTOR, DATE AND TIME";
            //
            // tlpSpecDate
            //
            tlpSpecDate.BackColor = Color.White;
            tlpSpecDate.AutoSize = true;
            tlpSpecDate.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpSpecDate.ColumnCount = 2;
            tlpSpecDate.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpSpecDate.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpSpecDate.Controls.Add(lblSpec, 0, 0);
            tlpSpecDate.Controls.Add(lblDate, 1, 0);
            tlpSpecDate.Controls.Add(cboSpecialization, 0, 1);
            tlpSpecDate.Controls.Add(dtpDate, 1, 1);
            tlpSpecDate.Dock = DockStyle.Fill;
            tlpSpecDate.Margin = new Padding(0, 0, 0, 14);
            tlpSpecDate.Name = "tlpSpecDate";
            tlpSpecDate.RowCount = 2;
            tlpSpecDate.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tlpSpecDate.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpSpecDate.Size = new Size(400, 70);
            tlpSpecDate.TabIndex = 3;
            //
            // lblSpec
            //
            lblSpec.AutoSize = true;
            lblSpec.BackColor = Color.Transparent;
            lblSpec.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSpec.ForeColor = Color.FromArgb(20, 34, 51);
            lblSpec.Margin = new Padding(0);
            lblSpec.Name = "lblSpec";
            lblSpec.TabIndex = 0;
            lblSpec.Text = "Specialization";
            //
            // lblDate
            //
            lblDate.AutoSize = true;
            lblDate.BackColor = Color.Transparent;
            lblDate.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDate.ForeColor = Color.FromArgb(20, 34, 51);
            lblDate.Margin = new Padding(6, 0, 0, 0);
            lblDate.Name = "lblDate";
            lblDate.TabIndex = 1;
            lblDate.Text = "Date";
            //
            // cboSpecialization
            //
            cboSpecialization.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboSpecialization.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSpecialization.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboSpecialization.Margin = new Padding(0, 0, 6, 0);
            cboSpecialization.Name = "cboSpecialization";
            cboSpecialization.Size = new Size(194, 32);
            cboSpecialization.TabIndex = 2;
            //
            // dtpDate
            //
            dtpDate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpDate.CustomFormat = "ddd, MMM d, yyyy";
            dtpDate.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.Margin = new Padding(6, 4, 0, 0);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(194, 31);
            dtpDate.TabIndex = 3;
            //
            // lblDoctor
            //
            lblDoctor.AutoSize = true;
            lblDoctor.BackColor = Color.Transparent;
            lblDoctor.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDoctor.ForeColor = Color.FromArgb(20, 34, 51);
            lblDoctor.Margin = new Padding(0, 0, 0, 6);
            lblDoctor.Name = "lblDoctor";
            lblDoctor.TabIndex = 24;
            lblDoctor.Text = "Doctor";
            //
            // cboDoctor
            //
            cboDoctor.Dock = DockStyle.Fill;
            cboDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDoctor.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboDoctor.Margin = new Padding(0, 0, 0, 14);
            cboDoctor.Name = "cboDoctor";
            cboDoctor.Size = new Size(400, 32);
            cboDoctor.TabIndex = 4;
            //
            // flpSlotHeader
            //
            flpSlotHeader.AutoSize = true;
            flpSlotHeader.BackColor = Color.White;
            flpSlotHeader.Controls.Add(lblSlot);
            flpSlotHeader.Controls.Add(lblSlotCount);
            flpSlotHeader.Margin = new Padding(0, 0, 0, 6);
            flpSlotHeader.Name = "flpSlotHeader";
            flpSlotHeader.Size = new Size(260, 20);
            flpSlotHeader.TabIndex = 25;
            flpSlotHeader.WrapContents = false;
            //
            // lblSlot
            //
            lblSlot.AutoSize = true;
            lblSlot.BackColor = Color.Transparent;
            lblSlot.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSlot.ForeColor = Color.FromArgb(20, 34, 51);
            lblSlot.Margin = new Padding(0);
            lblSlot.Name = "lblSlot";
            lblSlot.TabIndex = 0;
            lblSlot.Text = "Time slot";
            //
            // lblSlotCount
            //
            lblSlotCount.AutoSize = true;
            lblSlotCount.BackColor = Color.Transparent;
            lblSlotCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSlotCount.ForeColor = Color.FromArgb(91, 107, 124);
            lblSlotCount.Margin = new Padding(4, 0, 0, 0);
            lblSlotCount.Name = "lblSlotCount";
            lblSlotCount.TabIndex = 1;
            //
            // flpSlots
            //
            flpSlots.AutoScroll = true;
            flpSlots.BackColor = Color.White;
            flpSlots.Dock = DockStyle.Fill;
            flpSlots.Margin = new Padding(0, 0, 0, 6);
            flpSlots.Name = "flpSlots";
            flpSlots.Size = new Size(400, 80);
            flpSlots.TabIndex = 5;
            //
            // lblSlotInfo
            //
            lblSlotInfo.AutoSize = true;
            lblSlotInfo.BackColor = Color.Transparent;
            lblSlotInfo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSlotInfo.ForeColor = Color.FromArgb(91, 107, 124);
            lblSlotInfo.Margin = new Padding(0, 0, 0, 14);
            lblSlotInfo.MaximumSize = new Size(400, 0);
            lblSlotInfo.Name = "lblSlotInfo";
            lblSlotInfo.TabIndex = 26;
            //
            // tlpTypeReason
            //
            tlpTypeReason.BackColor = Color.White;
            tlpTypeReason.AutoSize = true;
            tlpTypeReason.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpTypeReason.ColumnCount = 2;
            tlpTypeReason.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTypeReason.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTypeReason.Controls.Add(lblType, 0, 0);
            tlpTypeReason.Controls.Add(lblReason, 1, 0);
            tlpTypeReason.Controls.Add(cboType, 0, 1);
            tlpTypeReason.Controls.Add(txtReason, 1, 1);
            tlpTypeReason.Dock = DockStyle.Fill;
            tlpTypeReason.Margin = new Padding(0, 0, 0, 14);
            tlpTypeReason.Name = "tlpTypeReason";
            tlpTypeReason.RowCount = 2;
            tlpTypeReason.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tlpTypeReason.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpTypeReason.Size = new Size(400, 70);
            tlpTypeReason.TabIndex = 6;
            //
            // lblType
            //
            lblType.AutoSize = true;
            lblType.BackColor = Color.Transparent;
            lblType.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblType.ForeColor = Color.FromArgb(20, 34, 51);
            lblType.Margin = new Padding(0);
            lblType.Name = "lblType";
            lblType.TabIndex = 0;
            lblType.Text = "Visit type";
            //
            // lblReason
            //
            lblReason.AutoSize = true;
            lblReason.BackColor = Color.Transparent;
            lblReason.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblReason.ForeColor = Color.FromArgb(20, 34, 51);
            lblReason.Margin = new Padding(6, 0, 0, 0);
            lblReason.Name = "lblReason";
            lblReason.TabIndex = 1;
            lblReason.Text = "Reason";
            //
            // cboType
            //
            cboType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboType.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboType.Margin = new Padding(0, 0, 6, 0);
            cboType.Name = "cboType";
            cboType.Size = new Size(194, 32);
            cboType.TabIndex = 2;
            //
            // txtReason
            //
            txtReason.AutoSize = false;
            txtReason.BackColor = Color.White;
            txtReason.BorderStyle = BorderStyle.FixedSingle;
            txtReason.Dock = DockStyle.Top;
            txtReason.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtReason.ForeColor = Color.FromArgb(20, 34, 51);
            txtReason.Margin = new Padding(6, 0, 0, 0);
            txtReason.MaxLength = 250;
            txtReason.Name = "txtReason";
            txtReason.PlaceholderText = "Optional";
            txtReason.Size = new Size(194, 40);
            txtReason.TabIndex = 3;
            //
            // tlpBookButtons
            //
            tlpBookButtons.BackColor = Color.White;
            tlpBookButtons.ColumnCount = 2;
            tlpBookButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBookButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 92F));
            tlpBookButtons.Controls.Add(btnBook, 0, 0);
            tlpBookButtons.Controls.Add(btnClearBooking, 1, 0);
            tlpBookButtons.Dock = DockStyle.Fill;
            tlpBookButtons.Margin = new Padding(0);
            tlpBookButtons.Name = "tlpBookButtons";
            tlpBookButtons.RowCount = 1;
            tlpBookButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpBookButtons.Size = new Size(400, 44);
            tlpBookButtons.TabIndex = 7;
            //
            // btnBook
            //
            btnBook.Dock = DockStyle.Fill;
            btnBook.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBook.Margin = new Padding(0);
            btnBook.Name = "btnBook";
            btnBook.Size = new Size(308, 44);
            btnBook.TabIndex = 0;
            btnBook.Text = "Book Appointment";
            //
            // btnClearBooking
            //
            btnClearBooking.Dock = DockStyle.Fill;
            btnClearBooking.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClearBooking.Margin = new Padding(10, 0, 0, 0);
            btnClearBooking.Name = "btnClearBooking";
            btnClearBooking.Size = new Size(82, 44);
            btnClearBooking.TabIndex = 1;
            btnClearBooking.Text = "Clear";
            //
            // cboSlot
            //
            cboSlot.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSlot.Location = new Point(20, 20);
            cboSlot.Name = "cboSlot";
            cboSlot.Size = new Size(120, 28);
            cboSlot.TabIndex = 99;
            cboSlot.TabStop = false;
            cboSlot.Visible = false;
            //
            // pnlListCard
            //
            pnlListCard.BackColor = Color.White;
            pnlListCard.Controls.Add(dgvAppointments);
            pnlListCard.Controls.Add(pnlListFooter);
            pnlListCard.Controls.Add(pnlListHeader);
            pnlListCard.Dock = DockStyle.Fill;
            pnlListCard.Margin = new Padding(0);
            pnlListCard.Name = "pnlListCard";
            pnlListCard.Padding = new Padding(1, 10, 1, 10);
            pnlListCard.Size = new Size(660, 726);
            pnlListCard.TabIndex = 1;
            //
            // dgvAppointments
            //
            dgvAppointments.BackgroundColor = Color.White;
            dgvAppointments.BorderStyle = BorderStyle.None;
            dgvAppointments.Dock = DockStyle.Fill;
            dgvAppointments.Name = "dgvAppointments";
            dgvAppointments.RowHeadersVisible = false;
            dgvAppointments.TabIndex = 1;
            //
            // pnlListFooter
            //
            pnlListFooter.BackColor = Color.White;
            pnlListFooter.Controls.Add(lblSummary);
            pnlListFooter.Controls.Add(flpActions);
            pnlListFooter.Dock = DockStyle.Bottom;
            pnlListFooter.Name = "pnlListFooter";
            pnlListFooter.Size = new Size(658, 56);
            pnlListFooter.TabIndex = 2;
            //
            // lblSummary
            //
            lblSummary.AutoEllipsis = true;
            lblSummary.BackColor = Color.Transparent;
            lblSummary.Dock = DockStyle.Fill;
            lblSummary.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSummary.ForeColor = Color.FromArgb(91, 107, 124);
            lblSummary.Name = "lblSummary";
            lblSummary.Padding = new Padding(18, 0, 8, 0);
            lblSummary.TabIndex = 0;
            lblSummary.TextAlign = ContentAlignment.MiddleLeft;
            lblSummary.UseMnemonic = false;
            //
            // flpActions
            //
            flpActions.AutoSize = true;
            flpActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpActions.BackColor = Color.White;
            flpActions.Controls.Add(btnReschedule);
            flpActions.Controls.Add(btnNoShow);
            flpActions.Controls.Add(btnComplete);
            flpActions.Controls.Add(btnCancelAppt);
            flpActions.Dock = DockStyle.Right;
            flpActions.Name = "flpActions";
            flpActions.Padding = new Padding(0, 9, 18, 0);
            flpActions.TabIndex = 1;
            flpActions.WrapContents = false;
            //
            // btnReschedule
            //
            btnReschedule.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnReschedule.Margin = new Padding(10, 0, 0, 0);
            btnReschedule.Name = "btnReschedule";
            btnReschedule.Size = new Size(104, 38);
            btnReschedule.TabIndex = 0;
            btnReschedule.Text = "Reschedule";
            //
            // btnNoShow
            //
            btnNoShow.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNoShow.Margin = new Padding(10, 0, 0, 0);
            btnNoShow.Name = "btnNoShow";
            btnNoShow.Size = new Size(124, 38);
            btnNoShow.TabIndex = 1;
            btnNoShow.Text = "Mark No-show";
            //
            // btnComplete
            //
            btnComplete.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnComplete.Margin = new Padding(10, 0, 0, 0);
            btnComplete.Name = "btnComplete";
            btnComplete.Size = new Size(136, 38);
            btnComplete.TabIndex = 2;
            btnComplete.Text = "Mark Completed";
            //
            // btnCancelAppt
            //
            btnCancelAppt.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancelAppt.Margin = new Padding(10, 0, 0, 0);
            btnCancelAppt.Name = "btnCancelAppt";
            btnCancelAppt.Size = new Size(158, 38);
            btnCancelAppt.TabIndex = 3;
            btnCancelAppt.Text = "Cancel Appointment";
            //
            // pnlListHeader
            //
            pnlListHeader.BackColor = Color.White;
            pnlListHeader.Controls.Add(lblListTitle);
            pnlListHeader.Controls.Add(flpFilters);
            pnlListHeader.Dock = DockStyle.Top;
            pnlListHeader.Name = "pnlListHeader";
            pnlListHeader.Size = new Size(658, 54);
            pnlListHeader.TabIndex = 0;
            //
            // lblListTitle
            //
            lblListTitle.AutoEllipsis = true;
            lblListTitle.BackColor = Color.Transparent;
            lblListTitle.Dock = DockStyle.Fill;
            lblListTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblListTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblListTitle.Name = "lblListTitle";
            lblListTitle.Padding = new Padding(16, 0, 0, 0);
            lblListTitle.TabIndex = 0;
            lblListTitle.Text = "Appointments";
            lblListTitle.TextAlign = ContentAlignment.MiddleLeft;
            //
            // flpFilters
            //
            flpFilters.AutoSize = true;
            flpFilters.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpFilters.BackColor = Color.White;
            flpFilters.Controls.Add(dtpFilterDate);
            flpFilters.Controls.Add(cboFilterDoctor);
            flpFilters.Controls.Add(cboFilterStatus);
            flpFilters.Controls.Add(txtFilterPatient);
            flpFilters.Controls.Add(btnClearFilters);
            flpFilters.Dock = DockStyle.Right;
            flpFilters.Name = "flpFilters";
            flpFilters.Padding = new Padding(0, 9, 18, 0);
            flpFilters.TabIndex = 1;
            flpFilters.WrapContents = false;
            //
            // dtpFilterDate
            //
            dtpFilterDate.CustomFormat = "MMM d, yyyy";
            dtpFilterDate.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFilterDate.Format = DateTimePickerFormat.Custom;
            dtpFilterDate.Margin = new Padding(10, 3, 0, 0);
            dtpFilterDate.Name = "dtpFilterDate";
            dtpFilterDate.ShowCheckBox = true;
            dtpFilterDate.Size = new Size(150, 29);
            dtpFilterDate.TabIndex = 0;
            //
            // cboFilterDoctor
            //
            cboFilterDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterDoctor.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboFilterDoctor.Margin = new Padding(10, 0, 0, 0);
            cboFilterDoctor.Name = "cboFilterDoctor";
            cboFilterDoctor.Size = new Size(170, 30);
            cboFilterDoctor.TabIndex = 1;
            //
            // cboFilterStatus
            //
            cboFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterStatus.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboFilterStatus.Margin = new Padding(10, 0, 0, 0);
            cboFilterStatus.Name = "cboFilterStatus";
            cboFilterStatus.Size = new Size(130, 30);
            cboFilterStatus.TabIndex = 2;
            //
            // txtFilterPatient
            //
            txtFilterPatient.AutoSize = false;
            txtFilterPatient.BackColor = Color.White;
            txtFilterPatient.BorderStyle = BorderStyle.FixedSingle;
            txtFilterPatient.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFilterPatient.ForeColor = Color.FromArgb(20, 34, 51);
            txtFilterPatient.Margin = new Padding(10, 0, 0, 0);
            txtFilterPatient.MaxLength = 60;
            txtFilterPatient.Name = "txtFilterPatient";
            txtFilterPatient.PlaceholderText = "Patient";
            txtFilterPatient.Size = new Size(130, 36);
            txtFilterPatient.TabIndex = 3;
            //
            // btnClearFilters
            //
            btnClearFilters.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClearFilters.Margin = new Padding(10, 0, 0, 0);
            btnClearFilters.Name = "btnClearFilters";
            btnClearFilters.Size = new Size(86, 36);
            btnClearFilters.TabIndex = 4;
            btnClearFilters.Text = "Show all";
            //
            // errorProvider
            //
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            //
            // AppointmentBookingForm
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1176, 848);
            Controls.Add(tlpRoot);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AppointmentBookingForm";
            Padding = new Padding(28, 24, 28, 24);
            Text = "Appointment Booking";
            tlpRoot.ResumeLayout(false);
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            tlpMain.ResumeLayout(false);
            pnlBookingCard.ResumeLayout(false);
            tlpLeft.ResumeLayout(false);
            tlpLeft.PerformLayout();
            tlpPatientSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPatients).EndInit();
            pnlPatientChip.ResumeLayout(false);
            tlpSpecDate.ResumeLayout(false);
            tlpSpecDate.PerformLayout();
            flpSlotHeader.ResumeLayout(false);
            flpSlotHeader.PerformLayout();
            tlpTypeReason.ResumeLayout(false);
            tlpTypeReason.PerformLayout();
            tlpBookButtons.ResumeLayout(false);
            pnlListCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).EndInit();
            pnlListFooter.ResumeLayout(false);
            pnlListFooter.PerformLayout();
            flpActions.ResumeLayout(false);
            pnlListHeader.ResumeLayout(false);
            pnlListHeader.PerformLayout();
            flpFilters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlBookingCard;
        private System.Windows.Forms.TableLayoutPanel tlpLeft;
        private System.Windows.Forms.Label lblStep1;
        private System.Windows.Forms.TableLayoutPanel tlpPatientSearch;
        private System.Windows.Forms.TextBox txtPatientSearch;
        private System.Windows.Forms.Button btnPatientSearch;
        private System.Windows.Forms.DataGridView dgvPatients;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPCode = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPName = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPAge = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPPhone = null!;
        private System.Windows.Forms.Label lblSelectedPatient;
        private System.Windows.Forms.Panel pnlPatientChip;
        private System.Windows.Forms.Label lblPatientAvatar;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblPatientDetail;
        private System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.TableLayoutPanel tlpSpecDate;
        private System.Windows.Forms.Label lblSpec;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.ComboBox cboSpecialization;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.ComboBox cboDoctor;
        private System.Windows.Forms.FlowLayoutPanel flpSlotHeader;
        private System.Windows.Forms.Label lblSlot;
        private System.Windows.Forms.Label lblSlotCount;
        private System.Windows.Forms.FlowLayoutPanel flpSlots;
        private System.Windows.Forms.Label lblSlotInfo;
        private System.Windows.Forms.TableLayoutPanel tlpTypeReason;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.TableLayoutPanel tlpBookButtons;
        private System.Windows.Forms.Button btnBook;
        private System.Windows.Forms.Button btnClearBooking;
        private System.Windows.Forms.ComboBox cboSlot;
        private System.Windows.Forms.Panel pnlListCard;
        private System.Windows.Forms.DataGridView dgvAppointments;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colToken = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPatient = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDoctor = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReason = null!;
        private System.Windows.Forms.Panel pnlListFooter;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.FlowLayoutPanel flpActions;
        private System.Windows.Forms.Button btnReschedule;
        private System.Windows.Forms.Button btnNoShow;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnCancelAppt;
        private System.Windows.Forms.Panel pnlListHeader;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.FlowLayoutPanel flpFilters;
        private System.Windows.Forms.DateTimePicker dtpFilterDate;
        private System.Windows.Forms.ComboBox cboFilterDoctor;
        private System.Windows.Forms.ComboBox cboFilterStatus;
        private System.Windows.Forms.TextBox txtFilterPatient;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
