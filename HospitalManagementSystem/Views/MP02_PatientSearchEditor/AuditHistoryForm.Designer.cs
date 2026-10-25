namespace HospitalManagementSystem.Views.MP02_PatientSearchEditor
{
    partial class AuditHistoryForm
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

        // Redesign: page title + patient line and one table card with the count and Close in its footer.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblPatientInfo = new Label();
            lblTitle = new Label();
            pnlHistoryCard = new Panel();
            dgvHistory = new DataGridView();
            lblEmpty = new Label();
            pnlFooter = new Panel();
            lblCount = new Label();
            flowFooter = new FlowLayoutPanel();
            btnClose = new Button();
            pnlHistoryHeader = new Panel();
            lblHistoryTitle = new Label();
            colField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colOld = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colNew = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            tlpRoot.SuspendLayout();
            pnlTitle.SuspendLayout();
            pnlHistoryCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            pnlFooter.SuspendLayout();
            flowFooter.SuspendLayout();
            pnlHistoryHeader.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.BackColor = Color.FromArgb(243, 246, 249);
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(pnlTitle, 0, 0);
            tlpRoot.Controls.Add(pnlHistoryCard, 0, 1);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 2;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.TabIndex = 0;
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.Transparent;
            pnlTitle.Controls.Add(lblPatientInfo);
            pnlTitle.Controls.Add(lblTitle);
            pnlTitle.Dock = DockStyle.Fill;
            pnlTitle.Margin = new Padding(0, 0, 0, 18);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Padding = new Padding(0);
            pnlTitle.TabIndex = 0;
            // 
            // lblPatientInfo
            // 
            lblPatientInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPatientInfo.AutoEllipsis = true;
            lblPatientInfo.BackColor = Color.Transparent;
            lblPatientInfo.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPatientInfo.ForeColor = Color.FromArgb(91, 107, 124);
            lblPatientInfo.Location = new Point(-2, 34);
            lblPatientInfo.Name = "lblPatientInfo";
            lblPatientInfo.Size = new Size(900, 22);
            lblPatientInfo.TabIndex = 0;
            lblPatientInfo.Text = "";
            lblPatientInfo.UseMnemonic = false;
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
            lblTitle.Text = "Change History";
            lblTitle.UseMnemonic = false;
            // 
            // pnlHistoryCard
            // 
            pnlHistoryCard.BackColor = Color.White;
            pnlHistoryCard.Controls.Add(dgvHistory);
            pnlHistoryCard.Controls.Add(lblEmpty);
            pnlHistoryCard.Controls.Add(pnlFooter);
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
            dgvHistory.AllowUserToOrderColumns = false;
            dgvHistory.AllowUserToResizeRows = false;
            dgvHistory.AutoGenerateColumns = false;
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            dgvHistory.Margin = new Padding(0);
            dgvHistory.MultiSelect = false;
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.TabIndex = 0;
            dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colField, colOld, colNew, colBy, colDate });
            // 
            // lblEmpty
            // 
            lblEmpty.BackColor = Color.Transparent;
            lblEmpty.Dock = DockStyle.Fill;
            lblEmpty.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmpty.ForeColor = Color.FromArgb(91, 107, 124);
            lblEmpty.Margin = new Padding(0);
            lblEmpty.Name = "lblEmpty";
            lblEmpty.TabIndex = 1;
            lblEmpty.Text = "No changes have been recorded for this patient.";
            lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
            lblEmpty.Visible = false;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(lblCount);
            pnlFooter.Controls.Add(flowFooter);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Margin = new Padding(0);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(0);
            pnlFooter.Size = new Size(600, 56);
            pnlFooter.TabIndex = 2;
            // 
            // lblCount
            // 
            lblCount.AutoEllipsis = true;
            lblCount.BackColor = Color.Transparent;
            lblCount.Dock = DockStyle.Fill;
            lblCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCount.ForeColor = Color.FromArgb(91, 107, 124);
            lblCount.Name = "lblCount";
            lblCount.Padding = new Padding(18, 0, 8, 0);
            lblCount.TabIndex = 0;
            lblCount.Text = "";
            lblCount.TextAlign = ContentAlignment.MiddleLeft;
            lblCount.UseMnemonic = false;
            // 
            // flowFooter
            // 
            flowFooter.AutoSize = true;
            flowFooter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowFooter.BackColor = Color.Transparent;
            flowFooter.Controls.Add(btnClose);
            flowFooter.Dock = DockStyle.Right;
            flowFooter.Margin = new Padding(0);
            flowFooter.Name = "flowFooter";
            flowFooter.Padding = new Padding(0, 9, 18, 0);
            flowFooter.TabIndex = 1;
            flowFooter.WrapContents = false;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClose.ForeColor = Color.FromArgb(20, 34, 51);
            btnClose.Margin = new Padding(10, 0, 0, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 38);
            btnClose.TabIndex = 0;
            btnClose.Text = "Close";
            btnClose.UseMnemonic = false;
            btnClose.UseVisualStyleBackColor = false;
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
            pnlHistoryHeader.TabIndex = 3;
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
            lblHistoryTitle.Text = "Recorded changes";
            lblHistoryTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblHistoryTitle.UseMnemonic = false;
            // 
            // colField
            // 
            colField.DataPropertyName = "field_changed";
            colField.FillWeight = 17F;
            colField.HeaderText = "Field";
            colField.MinimumWidth = 6;
            colField.Name = "colField";
            colField.ReadOnly = true;
            // 
            // colOld
            // 
            colOld.DataPropertyName = "old_value";
            colOld.FillWeight = 27F;
            colOld.HeaderText = "Old Value";
            colOld.MinimumWidth = 6;
            colOld.Name = "colOld";
            colOld.ReadOnly = true;
            // 
            // colNew
            // 
            colNew.DataPropertyName = "new_value";
            colNew.FillWeight = 27F;
            colNew.HeaderText = "New Value";
            colNew.MinimumWidth = 6;
            colNew.Name = "colNew";
            colNew.ReadOnly = true;
            // 
            // colBy
            // 
            colBy.DataPropertyName = "changed_by";
            colBy.FillWeight = 14F;
            colBy.HeaderText = "Changed By";
            colBy.MinimumWidth = 6;
            colBy.Name = "colBy";
            colBy.ReadOnly = true;
            // 
            // colDate
            // 
            colDate.DataPropertyName = "changed_at";
            colDate.FillWeight = 15F;
            colDate.HeaderText = "Date";
            colDate.MinimumWidth = 6;
            colDate.Name = "colDate";
            colDate.ReadOnly = true;
            // 
            // AuditHistoryForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1280, 800);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            MinimumSize = new Size(1000, 640);
            Padding = new Padding(28, 24, 28, 20);
            CancelButton = btnClose;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Change History";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Controls.Add(tlpRoot);
            Name = "AuditHistoryForm";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            pnlHistoryCard.ResumeLayout(false);
            pnlHistoryCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            flowFooter.ResumeLayout(false);
            flowFooter.PerformLayout();
            pnlHistoryHeader.ResumeLayout(false);
            pnlHistoryHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblPatientInfo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHistoryCard;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Label lblEmpty;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.FlowLayoutPanel flowFooter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlHistoryHeader;
        private System.Windows.Forms.Label lblHistoryTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colField;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOld;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
    }
}
