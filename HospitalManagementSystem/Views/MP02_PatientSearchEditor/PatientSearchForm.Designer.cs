namespace HospitalManagementSystem.Views.MP02_PatientSearchEditor
{
    partial class PatientSearchForm
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

        // Redesign: page title + hint, a search card (fields in one row, Search / Clear on the right),
        // then the results table card with the profile / history actions in its footer.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblSearchHint = new Label();
            lblTitle = new Label();
            pnlSearchCard = new Panel();
            tlpCriteria = new TableLayoutPanel();
            lblSearchCode = new Label();
            lblSearchName = new Label();
            lblSearchPhone = new Label();
            lblSearchDob = new Label();
            txtSearchCode = new TextBox();
            txtSearchName = new TextBox();
            txtSearchPhone = new TextBox();
            dtpSearchDob = new DateTimePicker();
            flowSearchButtons = new FlowLayoutPanel();
            btnSearch = new Button();
            btnClearSearch = new Button();
            lblSearchMessage = new Label();
            pnlResultsCard = new Panel();
            dgvResults = new DataGridView();
            tlpResultState = new TableLayoutPanel();
            lblStateTitle = new Label();
            lblStateHint = new Label();
            pnlResultsFooter = new Panel();
            lblResultsHint = new Label();
            flowActions = new FlowLayoutPanel();
            btnViewHistory = new Button();
            btnEditProfile = new Button();
            pnlResultsHeader = new Panel();
            lblResultsTitle = new Label();
            colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colDob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            tlpRoot.SuspendLayout();
            pnlTitle.SuspendLayout();
            pnlSearchCard.SuspendLayout();
            tlpCriteria.SuspendLayout();
            flowSearchButtons.SuspendLayout();
            pnlResultsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            tlpResultState.SuspendLayout();
            pnlResultsFooter.SuspendLayout();
            flowActions.SuspendLayout();
            pnlResultsHeader.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.BackColor = Color.FromArgb(243, 246, 249);
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(pnlTitle, 0, 0);
            tlpRoot.Controls.Add(pnlSearchCard, 0, 1);
            tlpRoot.Controls.Add(pnlResultsCard, 0, 2);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 3;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.TabIndex = 0;
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.Transparent;
            pnlTitle.Controls.Add(lblSearchHint);
            pnlTitle.Controls.Add(lblTitle);
            pnlTitle.Dock = DockStyle.Fill;
            pnlTitle.Margin = new Padding(0, 0, 0, 18);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Padding = new Padding(0);
            pnlTitle.TabIndex = 0;
            // 
            // lblSearchHint
            // 
            lblSearchHint.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSearchHint.AutoEllipsis = true;
            lblSearchHint.BackColor = Color.Transparent;
            lblSearchHint.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSearchHint.ForeColor = Color.FromArgb(91, 107, 124);
            lblSearchHint.Location = new Point(-2, 34);
            lblSearchHint.Name = "lblSearchHint";
            lblSearchHint.Size = new Size(900, 22);
            lblSearchHint.TabIndex = 0;
            lblSearchHint.Text = "Fill in any combination of the fields below, then open a patient's profile or change history.";
            lblSearchHint.UseMnemonic = false;
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
            lblTitle.Text = "Patient Search & Profile";
            lblTitle.UseMnemonic = false;
            // 
            // pnlSearchCard
            // 
            pnlSearchCard.AutoSize = true;
            pnlSearchCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlSearchCard.BackColor = Color.White;
            pnlSearchCard.Controls.Add(tlpCriteria);
            pnlSearchCard.Dock = DockStyle.Fill;
            pnlSearchCard.Margin = new Padding(0, 0, 0, 20);
            pnlSearchCard.Name = "pnlSearchCard";
            pnlSearchCard.Padding = new Padding(20, 18, 20, 18);
            pnlSearchCard.TabIndex = 1;
            // 
            // tlpCriteria
            // 
            tlpCriteria.AutoSize = true;
            tlpCriteria.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpCriteria.BackColor = Color.Transparent;
            tlpCriteria.ColumnCount = 5;
            tlpCriteria.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tlpCriteria.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tlpCriteria.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpCriteria.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpCriteria.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 216F));
            tlpCriteria.Controls.Add(lblSearchCode, 0, 0);
            tlpCriteria.Controls.Add(lblSearchName, 1, 0);
            tlpCriteria.Controls.Add(lblSearchPhone, 2, 0);
            tlpCriteria.Controls.Add(lblSearchDob, 3, 0);
            tlpCriteria.Controls.Add(txtSearchCode, 0, 1);
            tlpCriteria.Controls.Add(txtSearchName, 1, 1);
            tlpCriteria.Controls.Add(txtSearchPhone, 2, 1);
            tlpCriteria.Controls.Add(dtpSearchDob, 3, 1);
            tlpCriteria.Controls.Add(flowSearchButtons, 4, 1);
            tlpCriteria.Controls.Add(lblSearchMessage, 0, 2);
            tlpCriteria.SetColumnSpan(lblSearchMessage, 5);
            tlpCriteria.Dock = DockStyle.Top;
            tlpCriteria.Margin = new Padding(0);
            tlpCriteria.Name = "tlpCriteria";
            tlpCriteria.RowCount = 3;
            tlpCriteria.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCriteria.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpCriteria.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCriteria.TabIndex = 0;
            // 
            // lblSearchCode
            // 
            lblSearchCode.AutoSize = true;
            lblSearchCode.BackColor = Color.Transparent;
            lblSearchCode.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSearchCode.ForeColor = Color.FromArgb(20, 34, 51);
            lblSearchCode.Margin = new Padding(0, 0, 12, 6);
            lblSearchCode.Name = "lblSearchCode";
            lblSearchCode.TabIndex = 0;
            lblSearchCode.Text = "Patient ID";
            // 
            // lblSearchName
            // 
            lblSearchName.AutoSize = true;
            lblSearchName.BackColor = Color.Transparent;
            lblSearchName.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSearchName.ForeColor = Color.FromArgb(20, 34, 51);
            lblSearchName.Margin = new Padding(0, 0, 12, 6);
            lblSearchName.Name = "lblSearchName";
            lblSearchName.TabIndex = 1;
            lblSearchName.Text = "Name (first, middle or last - partial is fine)";
            // 
            // lblSearchPhone
            // 
            lblSearchPhone.AutoSize = true;
            lblSearchPhone.BackColor = Color.Transparent;
            lblSearchPhone.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSearchPhone.ForeColor = Color.FromArgb(20, 34, 51);
            lblSearchPhone.Margin = new Padding(0, 0, 12, 6);
            lblSearchPhone.Name = "lblSearchPhone";
            lblSearchPhone.TabIndex = 2;
            lblSearchPhone.Text = "Phone number";
            // 
            // lblSearchDob
            // 
            lblSearchDob.AutoSize = true;
            lblSearchDob.BackColor = Color.Transparent;
            lblSearchDob.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSearchDob.ForeColor = Color.FromArgb(20, 34, 51);
            lblSearchDob.Margin = new Padding(0, 0, 12, 6);
            lblSearchDob.Name = "lblSearchDob";
            lblSearchDob.TabIndex = 3;
            lblSearchDob.Text = "Date of birth";
            // 
            // txtSearchCode
            // 
            txtSearchCode.AutoSize = false;
            txtSearchCode.BackColor = Color.White;
            txtSearchCode.BorderStyle = BorderStyle.FixedSingle;
            txtSearchCode.Dock = DockStyle.Fill;
            txtSearchCode.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearchCode.ForeColor = Color.FromArgb(20, 34, 51);
            txtSearchCode.Margin = new Padding(0, 0, 12, 0);
            txtSearchCode.MaxLength = 20;
            txtSearchCode.Name = "txtSearchCode";
            txtSearchCode.Size = new Size(200, 40);
            txtSearchCode.TabIndex = 4;
            // 
            // txtSearchName
            // 
            txtSearchName.AutoSize = false;
            txtSearchName.BackColor = Color.White;
            txtSearchName.BorderStyle = BorderStyle.FixedSingle;
            txtSearchName.Dock = DockStyle.Fill;
            txtSearchName.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearchName.ForeColor = Color.FromArgb(20, 34, 51);
            txtSearchName.Margin = new Padding(0, 0, 12, 0);
            txtSearchName.MaxLength = 80;
            txtSearchName.Name = "txtSearchName";
            txtSearchName.Size = new Size(200, 40);
            txtSearchName.TabIndex = 5;
            // 
            // txtSearchPhone
            // 
            txtSearchPhone.AutoSize = false;
            txtSearchPhone.BackColor = Color.White;
            txtSearchPhone.BorderStyle = BorderStyle.FixedSingle;
            txtSearchPhone.Dock = DockStyle.Fill;
            txtSearchPhone.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearchPhone.ForeColor = Color.FromArgb(20, 34, 51);
            txtSearchPhone.Margin = new Padding(0, 0, 12, 0);
            txtSearchPhone.MaxLength = 25;
            txtSearchPhone.Name = "txtSearchPhone";
            txtSearchPhone.Size = new Size(200, 40);
            txtSearchPhone.TabIndex = 6;
            // 
            // dtpSearchDob
            // 
            dtpSearchDob.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpSearchDob.Checked = false;
            dtpSearchDob.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpSearchDob.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpSearchDob.Margin = new Padding(0, 0, 12, 0);
            dtpSearchDob.Name = "dtpSearchDob";
            dtpSearchDob.ShowCheckBox = true;
            dtpSearchDob.Size = new Size(200, 31);
            dtpSearchDob.TabIndex = 7;
            // 
            // flowSearchButtons
            // 
            flowSearchButtons.Anchor = AnchorStyles.Left;
            flowSearchButtons.AutoSize = true;
            flowSearchButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowSearchButtons.BackColor = Color.Transparent;
            flowSearchButtons.Controls.Add(btnSearch);
            flowSearchButtons.Controls.Add(btnClearSearch);
            flowSearchButtons.Margin = new Padding(0);
            flowSearchButtons.Name = "flowSearchButtons";
            flowSearchButtons.Padding = new Padding(0);
            flowSearchButtons.TabIndex = 8;
            flowSearchButtons.WrapContents = false;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(18, 122, 140);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSearch.ForeColor = Color.White;
            btnSearch.Margin = new Padding(0);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 40);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Search";
            btnSearch.UseMnemonic = false;
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnClearSearch
            // 
            btnClearSearch.BackColor = Color.White;
            btnClearSearch.FlatStyle = FlatStyle.Flat;
            btnClearSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClearSearch.ForeColor = Color.FromArgb(20, 34, 51);
            btnClearSearch.Margin = new Padding(10, 0, 0, 0);
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Size = new Size(90, 40);
            btnClearSearch.TabIndex = 1;
            btnClearSearch.Text = "Clear";
            btnClearSearch.UseMnemonic = false;
            btnClearSearch.UseVisualStyleBackColor = false;
            // 
            // lblSearchMessage
            // 
            lblSearchMessage.AutoSize = true;
            lblSearchMessage.BackColor = Color.Transparent;
            lblSearchMessage.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSearchMessage.ForeColor = Color.FromArgb(91, 107, 124);
            lblSearchMessage.Margin = new Padding(0, 10, 0, 0);
            lblSearchMessage.Name = "lblSearchMessage";
            lblSearchMessage.TabIndex = 9;
            lblSearchMessage.Text = "";
            // 
            // pnlResultsCard
            // 
            pnlResultsCard.BackColor = Color.White;
            pnlResultsCard.Controls.Add(dgvResults);
            pnlResultsCard.Controls.Add(tlpResultState);
            pnlResultsCard.Controls.Add(pnlResultsFooter);
            pnlResultsCard.Controls.Add(pnlResultsHeader);
            pnlResultsCard.Dock = DockStyle.Fill;
            pnlResultsCard.Margin = new Padding(0);
            pnlResultsCard.Name = "pnlResultsCard";
            pnlResultsCard.Padding = new Padding(1, 6, 1, 1);
            pnlResultsCard.TabIndex = 2;
            // 
            // dgvResults
            // 
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.AllowUserToOrderColumns = false;
            dgvResults.AllowUserToResizeRows = false;
            dgvResults.AutoGenerateColumns = false;
            dgvResults.BackgroundColor = Color.White;
            dgvResults.BorderStyle = BorderStyle.None;
            dgvResults.Dock = DockStyle.Fill;
            dgvResults.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            dgvResults.Margin = new Padding(0);
            dgvResults.MultiSelect = false;
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.RowHeadersVisible = false;
            dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvResults.TabIndex = 0;
            dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colCode, colName, colPhone, colDob, colAge, colGender, colCity });
            // 
            // tlpResultState
            // 
            tlpResultState.BackColor = Color.White;
            tlpResultState.ColumnCount = 1;
            tlpResultState.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpResultState.Controls.Add(lblStateTitle, 0, 1);
            tlpResultState.Controls.Add(lblStateHint, 0, 2);
            tlpResultState.Dock = DockStyle.Fill;
            tlpResultState.Margin = new Padding(0);
            tlpResultState.Name = "tlpResultState";
            tlpResultState.RowCount = 4;
            tlpResultState.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpResultState.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpResultState.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpResultState.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpResultState.TabIndex = 1;
            tlpResultState.Visible = false;
            // 
            // lblStateTitle
            // 
            lblStateTitle.BackColor = Color.Transparent;
            lblStateTitle.Dock = DockStyle.Fill;
            lblStateTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStateTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblStateTitle.Margin = new Padding(0);
            lblStateTitle.Name = "lblStateTitle";
            lblStateTitle.TabIndex = 0;
            lblStateTitle.Text = "No matching patients found";
            lblStateTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStateHint
            // 
            lblStateHint.BackColor = Color.Transparent;
            lblStateHint.Dock = DockStyle.Fill;
            lblStateHint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStateHint.ForeColor = Color.FromArgb(91, 107, 124);
            lblStateHint.Margin = new Padding(0);
            lblStateHint.Name = "lblStateHint";
            lblStateHint.TabIndex = 1;
            lblStateHint.Text = "Check the spelling, or try fewer criteria (for example just part of the name).";
            lblStateHint.TextAlign = ContentAlignment.TopCenter;
            // 
            // pnlResultsFooter
            // 
            pnlResultsFooter.BackColor = Color.White;
            pnlResultsFooter.Controls.Add(lblResultsHint);
            pnlResultsFooter.Controls.Add(flowActions);
            pnlResultsFooter.Dock = DockStyle.Bottom;
            pnlResultsFooter.Margin = new Padding(0);
            pnlResultsFooter.Name = "pnlResultsFooter";
            pnlResultsFooter.Padding = new Padding(0);
            pnlResultsFooter.Size = new Size(600, 56);
            pnlResultsFooter.TabIndex = 2;
            // 
            // lblResultsHint
            // 
            lblResultsHint.AutoEllipsis = true;
            lblResultsHint.BackColor = Color.Transparent;
            lblResultsHint.Dock = DockStyle.Fill;
            lblResultsHint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblResultsHint.ForeColor = Color.FromArgb(91, 107, 124);
            lblResultsHint.Name = "lblResultsHint";
            lblResultsHint.Padding = new Padding(18, 0, 8, 0);
            lblResultsHint.TabIndex = 0;
            lblResultsHint.Text = "Select a patient, then open the profile or its change history.";
            lblResultsHint.TextAlign = ContentAlignment.MiddleLeft;
            lblResultsHint.UseMnemonic = false;
            // 
            // flowActions
            // 
            flowActions.AutoSize = true;
            flowActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowActions.BackColor = Color.Transparent;
            flowActions.Controls.Add(btnViewHistory);
            flowActions.Controls.Add(btnEditProfile);
            flowActions.Dock = DockStyle.Right;
            flowActions.Margin = new Padding(0);
            flowActions.Name = "flowActions";
            flowActions.Padding = new Padding(0, 9, 18, 0);
            flowActions.TabIndex = 1;
            flowActions.WrapContents = false;
            // 
            // btnViewHistory
            // 
            btnViewHistory.BackColor = Color.White;
            btnViewHistory.FlatStyle = FlatStyle.Flat;
            btnViewHistory.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnViewHistory.ForeColor = Color.FromArgb(20, 34, 51);
            btnViewHistory.Margin = new Padding(10, 0, 0, 0);
            btnViewHistory.Name = "btnViewHistory";
            btnViewHistory.Size = new Size(170, 38);
            btnViewHistory.TabIndex = 0;
            btnViewHistory.Text = "View Change History";
            btnViewHistory.UseMnemonic = false;
            btnViewHistory.UseVisualStyleBackColor = false;
            // 
            // btnEditProfile
            // 
            btnEditProfile.BackColor = Color.FromArgb(18, 122, 140);
            btnEditProfile.FlatStyle = FlatStyle.Flat;
            btnEditProfile.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEditProfile.ForeColor = Color.White;
            btnEditProfile.Margin = new Padding(10, 0, 0, 0);
            btnEditProfile.Name = "btnEditProfile";
            btnEditProfile.Size = new Size(160, 38);
            btnEditProfile.TabIndex = 1;
            btnEditProfile.Text = "Open / Edit Profile";
            btnEditProfile.UseMnemonic = false;
            btnEditProfile.UseVisualStyleBackColor = false;
            // 
            // pnlResultsHeader
            // 
            pnlResultsHeader.BackColor = Color.White;
            pnlResultsHeader.Controls.Add(lblResultsTitle);
            pnlResultsHeader.Dock = DockStyle.Top;
            pnlResultsHeader.Margin = new Padding(0);
            pnlResultsHeader.Name = "pnlResultsHeader";
            pnlResultsHeader.Padding = new Padding(0);
            pnlResultsHeader.Size = new Size(600, 46);
            pnlResultsHeader.TabIndex = 3;
            // 
            // lblResultsTitle
            // 
            lblResultsTitle.AutoEllipsis = true;
            lblResultsTitle.BackColor = Color.Transparent;
            lblResultsTitle.Dock = DockStyle.Fill;
            lblResultsTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblResultsTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblResultsTitle.Margin = new Padding(0);
            lblResultsTitle.Name = "lblResultsTitle";
            lblResultsTitle.Padding = new Padding(17, 0, 0, 0);
            lblResultsTitle.TabIndex = 0;
            lblResultsTitle.Text = "Results";
            lblResultsTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblResultsTitle.UseMnemonic = false;
            // 
            // colCode
            // 
            colCode.DataPropertyName = "patient_id";
            colCode.FillWeight = 17F;
            colCode.HeaderText = "Patient ID";
            colCode.MinimumWidth = 6;
            colCode.Name = "colCode";
            colCode.ReadOnly = true;
            // 
            // colName
            // 
            colName.DataPropertyName = "full_name";
            colName.FillWeight = 26F;
            colName.HeaderText = "Name";
            colName.MinimumWidth = 6;
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colPhone
            // 
            colPhone.DataPropertyName = "phone_number";
            colPhone.FillWeight = 15F;
            colPhone.HeaderText = "Phone";
            colPhone.MinimumWidth = 6;
            colPhone.Name = "colPhone";
            colPhone.ReadOnly = true;
            // 
            // colDob
            // 
            colDob.DataPropertyName = "birth_date";
            colDob.FillWeight = 15F;
            colDob.HeaderText = "Date of Birth";
            colDob.MinimumWidth = 6;
            colDob.Name = "colDob";
            colDob.ReadOnly = true;
            // 
            // colAge
            // 
            colAge.DataPropertyName = "age";
            colAge.FillWeight = 6F;
            colAge.HeaderText = "Age";
            colAge.MinimumWidth = 6;
            colAge.Name = "colAge";
            colAge.ReadOnly = true;
            // 
            // colGender
            // 
            colGender.DataPropertyName = "gender";
            colGender.FillWeight = 9F;
            colGender.HeaderText = "Gender";
            colGender.MinimumWidth = 6;
            colGender.Name = "colGender";
            colGender.ReadOnly = true;
            // 
            // colCity
            // 
            colCity.DataPropertyName = "city";
            colCity.FillWeight = 12F;
            colCity.HeaderText = "City";
            colCity.MinimumWidth = 6;
            colCity.Name = "colCity";
            colCity.ReadOnly = true;
            // 
            // PatientSearchForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1176, 848);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(28, 24, 28, 24);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Patient Search & Profile";
            Controls.Add(tlpRoot);
            Name = "PatientSearchForm";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            pnlSearchCard.ResumeLayout(false);
            pnlSearchCard.PerformLayout();
            tlpCriteria.ResumeLayout(false);
            tlpCriteria.PerformLayout();
            flowSearchButtons.ResumeLayout(false);
            flowSearchButtons.PerformLayout();
            pnlResultsCard.ResumeLayout(false);
            pnlResultsCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            tlpResultState.ResumeLayout(false);
            tlpResultState.PerformLayout();
            pnlResultsFooter.ResumeLayout(false);
            pnlResultsFooter.PerformLayout();
            flowActions.ResumeLayout(false);
            flowActions.PerformLayout();
            pnlResultsHeader.ResumeLayout(false);
            pnlResultsHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblSearchHint;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlSearchCard;
        private System.Windows.Forms.TableLayoutPanel tlpCriteria;
        private System.Windows.Forms.Label lblSearchCode;
        private System.Windows.Forms.Label lblSearchName;
        private System.Windows.Forms.Label lblSearchPhone;
        private System.Windows.Forms.Label lblSearchDob;
        private System.Windows.Forms.TextBox txtSearchCode;
        private System.Windows.Forms.TextBox txtSearchName;
        private System.Windows.Forms.TextBox txtSearchPhone;
        private System.Windows.Forms.DateTimePicker dtpSearchDob;
        private System.Windows.Forms.FlowLayoutPanel flowSearchButtons;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.Label lblSearchMessage;
        private System.Windows.Forms.Panel pnlResultsCard;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.TableLayoutPanel tlpResultState;
        private System.Windows.Forms.Label lblStateTitle;
        private System.Windows.Forms.Label lblStateHint;
        private System.Windows.Forms.Panel pnlResultsFooter;
        private System.Windows.Forms.Label lblResultsHint;
        private System.Windows.Forms.FlowLayoutPanel flowActions;
        private System.Windows.Forms.Button btnViewHistory;
        private System.Windows.Forms.Button btnEditProfile;
        private System.Windows.Forms.Panel pnlResultsHeader;
        private System.Windows.Forms.Label lblResultsTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDob;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCity;
    }
}
