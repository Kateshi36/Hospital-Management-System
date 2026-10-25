namespace HospitalManagementSystem.Views.MP05_QueueBoard
{
    partial class QueueWalkInDialog
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

        // Redesign: title, doctor line, a card with patient search, results, emergency flag; Cancel / Add underneath.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            lblTitle = new Label();
            lblInfo = new Label();
            pnlCard = new Panel();
            tlpCardBody = new TableLayoutPanel();
            tlpSearch = new TableLayoutPanel();
            txtSearch = new TextBox();
            btnSearch = new Button();
            dgvPatients = new DataGridView();
            lblSelected = new Label();
            chkEmergency = new CheckBox();
            tlpButtons = new TableLayoutPanel();
            btnCancelDialog = new Button();
            btnAdd = new Button();
            errorProvider = new ErrorProvider(components);
            colPCode = new DataGridViewTextBoxColumn();
            colPName = new DataGridViewTextBoxColumn();
            colPAge = new DataGridViewTextBoxColumn();
            colPPhone = new DataGridViewTextBoxColumn();
            tlpRoot.SuspendLayout();
            pnlCard.SuspendLayout();
            tlpCardBody.SuspendLayout();
            tlpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPatients).BeginInit();
            tlpButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.BackColor = Color.FromArgb(243, 246, 249);
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(lblTitle, 0, 0);
            tlpRoot.Controls.Add(lblInfo, 0, 1);
            tlpRoot.Controls.Add(pnlCard, 0, 2);
            tlpRoot.Controls.Add(tlpButtons, 0, 3);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 4;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpRoot.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblTitle.Margin = new Padding(0, 0, 0, 6);
            lblTitle.Name = "lblTitle";
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Walk-in / Emergency patient";
            lblTitle.UseMnemonic = false;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.BackColor = Color.Transparent;
            lblInfo.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInfo.ForeColor = Color.FromArgb(91, 107, 124);
            lblInfo.Margin = new Padding(0, 0, 0, 16);
            lblInfo.MaximumSize = new Size(640, 0);
            lblInfo.Name = "lblInfo";
            lblInfo.TabIndex = 1;
            lblInfo.Text = "";
            lblInfo.UseMnemonic = false;
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.White;
            pnlCard.Controls.Add(tlpCardBody);
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.Margin = new Padding(0, 0, 0, 16);
            pnlCard.Name = "pnlCard";
            pnlCard.Padding = new Padding(20, 18, 20, 4);
            pnlCard.TabIndex = 2;
            // 
            // tlpCardBody
            // 
            tlpCardBody.BackColor = Color.Transparent;
            tlpCardBody.ColumnCount = 1;
            tlpCardBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCardBody.Controls.Add(tlpSearch, 0, 0);
            tlpCardBody.Controls.Add(dgvPatients, 0, 1);
            tlpCardBody.Controls.Add(lblSelected, 0, 2);
            tlpCardBody.Controls.Add(chkEmergency, 0, 3);
            tlpCardBody.Dock = DockStyle.Fill;
            tlpCardBody.Margin = new Padding(0);
            tlpCardBody.Name = "tlpCardBody";
            tlpCardBody.RowCount = 4;
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCardBody.TabIndex = 0;
            // 
            // tlpSearch
            // 
            tlpSearch.BackColor = Color.Transparent;
            tlpSearch.ColumnCount = 2;
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tlpSearch.Controls.Add(txtSearch, 0, 0);
            tlpSearch.Controls.Add(btnSearch, 1, 0);
            tlpSearch.Dock = DockStyle.Fill;
            tlpSearch.Margin = new Padding(0, 0, 0, 14);
            tlpSearch.Name = "tlpSearch";
            tlpSearch.RowCount = 1;
            tlpSearch.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpSearch.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.AutoSize = false;
            txtSearch.BackColor = Color.White;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Dock = DockStyle.Fill;
            txtSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtSearch.Margin = new Padding(0);
            txtSearch.MaxLength = 60;
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Name, patient code or phone number";
            txtSearch.Size = new Size(200, 40);
            txtSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(226, 242, 246);
            btnSearch.Dock = DockStyle.Fill;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSearch.ForeColor = Color.FromArgb(20, 34, 51);
            btnSearch.Margin = new Padding(8, 0, 0, 0);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(120, 40);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseMnemonic = false;
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // dgvPatients
            // 
            dgvPatients.AllowUserToAddRows = false;
            dgvPatients.AllowUserToDeleteRows = false;
            dgvPatients.AllowUserToResizeRows = false;
            dgvPatients.AutoGenerateColumns = false;
            dgvPatients.BackgroundColor = Color.White;
            dgvPatients.BorderStyle = BorderStyle.None;
            dgvPatients.Dock = DockStyle.Fill;
            dgvPatients.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvPatients.Margin = new Padding(0, 0, 0, 12);
            dgvPatients.MultiSelect = false;
            dgvPatients.Name = "dgvPatients";
            dgvPatients.ReadOnly = true;
            dgvPatients.RowHeadersVisible = false;
            dgvPatients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPatients.TabIndex = 1;
            dgvPatients.Columns.AddRange(new DataGridViewColumn[] { colPCode, colPName, colPAge, colPPhone });
            // 
            // lblSelected
            // 
            lblSelected.AutoSize = true;
            lblSelected.BackColor = Color.Transparent;
            lblSelected.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSelected.ForeColor = Color.FromArgb(91, 107, 124);
            lblSelected.Margin = new Padding(0, 0, 0, 10);
            lblSelected.Name = "lblSelected";
            lblSelected.TabIndex = 2;
            lblSelected.Text = "No patient selected yet";
            lblSelected.UseMnemonic = false;
            // 
            // chkEmergency
            // 
            chkEmergency.AutoSize = true;
            chkEmergency.BackColor = Color.Transparent;
            chkEmergency.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkEmergency.ForeColor = Color.FromArgb(20, 34, 51);
            chkEmergency.Margin = new Padding(0, 0, 0, 14);
            chkEmergency.Name = "chkEmergency";
            chkEmergency.TabIndex = 3;
            chkEmergency.Text = "Emergency - call this patient before everybody else";
            chkEmergency.UseMnemonic = false;
            chkEmergency.UseVisualStyleBackColor = false;
            // 
            // tlpButtons
            // 
            tlpButtons.BackColor = Color.Transparent;
            tlpButtons.ColumnCount = 3;
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpButtons.Controls.Add(btnCancelDialog, 1, 0);
            tlpButtons.Controls.Add(btnAdd, 2, 0);
            tlpButtons.Dock = DockStyle.Fill;
            tlpButtons.Margin = new Padding(0);
            tlpButtons.Name = "tlpButtons";
            tlpButtons.RowCount = 1;
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpButtons.TabIndex = 3;
            // 
            // btnCancelDialog
            // 
            btnCancelDialog.BackColor = Color.White;
            btnCancelDialog.FlatStyle = FlatStyle.Flat;
            btnCancelDialog.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancelDialog.ForeColor = Color.FromArgb(20, 34, 51);
            btnCancelDialog.Margin = new Padding(10, 0, 0, 0);
            btnCancelDialog.Name = "btnCancelDialog";
            btnCancelDialog.Size = new Size(110, 40);
            btnCancelDialog.TabIndex = 0;
            btnCancelDialog.Text = "Cancel";
            btnCancelDialog.UseMnemonic = false;
            btnCancelDialog.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(18, 122, 140);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAdd.ForeColor = Color.White;
            btnAdd.Margin = new Padding(10, 0, 0, 0);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(150, 40);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add to Queue";
            btnAdd.UseMnemonic = false;
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // colPCode
            // 
            colPCode.DataPropertyName = "patient_id";
            colPCode.FillWeight = 24F;
            colPCode.HeaderText = "Patient ID";
            colPCode.MinimumWidth = 6;
            colPCode.Name = "colPCode";
            colPCode.ReadOnly = true;
            colPCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colPName
            // 
            colPName.DataPropertyName = "full_name";
            colPName.FillWeight = 44F;
            colPName.HeaderText = "Patient";
            colPName.MinimumWidth = 6;
            colPName.Name = "colPName";
            colPName.ReadOnly = true;
            colPName.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colPAge
            // 
            colPAge.DataPropertyName = "age";
            colPAge.FillWeight = 10F;
            colPAge.HeaderText = "Age";
            colPAge.MinimumWidth = 6;
            colPAge.Name = "colPAge";
            colPAge.ReadOnly = true;
            colPAge.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colPPhone
            // 
            colPPhone.DataPropertyName = "phone_text";
            colPPhone.FillWeight = 26F;
            colPPhone.HeaderText = "Phone";
            colPPhone.MinimumWidth = 6;
            colPPhone.Name = "colPPhone";
            colPPhone.ReadOnly = true;
            colPPhone.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // QueueWalkInDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(720, 620);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(24, 20, 24, 20);
            CancelButton = btnCancelDialog;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Walk-in / Emergency";
            Controls.Add(tlpRoot);
            Name = "QueueWalkInDialog";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            tlpCardBody.ResumeLayout(false);
            tlpCardBody.PerformLayout();
            tlpSearch.ResumeLayout(false);
            tlpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPatients).EndInit();
            tlpButtons.ResumeLayout(false);
            tlpButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.TableLayoutPanel tlpCardBody;
        private System.Windows.Forms.TableLayoutPanel tlpSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvPatients;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.CheckBox chkEmergency;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnCancelDialog;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPPhone;
    }
}
