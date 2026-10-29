namespace HospitalManagementSystem.Views.MP11_Billing
{
    partial class UnbilledChargesDialog
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

        // Unbilled charges: title, patient line, a card with the charges (tick the ones to add) and the total; Cancel / Add underneath.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            lblTitle = new Label();
            lblInfo = new Label();
            pnlCard = new Panel();
            tlpCardBody = new TableLayoutPanel();
            dgvCharges = new DataGridView();
            tlpCardFooter = new TableLayoutPanel();
            chkAll = new CheckBox();
            lblTotal = new Label();
            tlpButtons = new TableLayoutPanel();
            btnCancelDialog = new Button();
            btnAdd = new Button();
            tlpRoot.SuspendLayout();
            pnlCard.SuspendLayout();
            tlpCardBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCharges).BeginInit();
            tlpCardFooter.SuspendLayout();
            tlpButtons.SuspendLayout();
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
            lblInfo.UseMnemonic = false;
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.White;
            pnlCard.Controls.Add(tlpCardBody);
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.Margin = new Padding(0, 0, 0, 16);
            pnlCard.Name = "pnlCard";
            pnlCard.Padding = new Padding(1, 6, 18, 6);
            pnlCard.TabIndex = 2;
            // 
            // tlpCardBody
            // 
            tlpCardBody.BackColor = Color.Transparent;
            tlpCardBody.ColumnCount = 1;
            tlpCardBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCardBody.Controls.Add(dgvCharges, 0, 0);
            tlpCardBody.Controls.Add(tlpCardFooter, 0, 1);
            tlpCardBody.Dock = DockStyle.Fill;
            tlpCardBody.Margin = new Padding(0);
            tlpCardBody.Name = "tlpCardBody";
            tlpCardBody.RowCount = 2;
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpCardBody.TabIndex = 0;
            // 
            // dgvCharges
            // 
            dgvCharges.BackgroundColor = Color.White;
            dgvCharges.BorderStyle = BorderStyle.None;
            dgvCharges.Dock = DockStyle.Fill;
            dgvCharges.Margin = new Padding(0, 0, 0, 8);
            dgvCharges.Name = "dgvCharges";
            dgvCharges.RowHeadersVisible = false;
            dgvCharges.TabIndex = 0;
            // 
            // tlpCardFooter
            // 
            tlpCardFooter.BackColor = Color.Transparent;
            tlpCardFooter.ColumnCount = 2;
            tlpCardFooter.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpCardFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCardFooter.Controls.Add(chkAll, 0, 0);
            tlpCardFooter.Controls.Add(lblTotal, 1, 0);
            tlpCardFooter.Dock = DockStyle.Fill;
            tlpCardFooter.Margin = new Padding(0);
            tlpCardFooter.Name = "tlpCardFooter";
            tlpCardFooter.RowCount = 1;
            tlpCardFooter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCardFooter.TabIndex = 1;
            // 
            // chkAll
            // 
            chkAll.AutoSize = true;
            chkAll.BackColor = Color.Transparent;
            chkAll.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkAll.ForeColor = Color.FromArgb(20, 34, 51);
            chkAll.Margin = new Padding(0, 10, 0, 0);
            chkAll.Name = "chkAll";
            chkAll.TabIndex = 0;
            chkAll.Text = "Select all";
            chkAll.UseVisualStyleBackColor = false;
            // 
            // lblTotal
            // 
            lblTotal.BackColor = Color.Transparent;
            lblTotal.Dock = DockStyle.Fill;
            lblTotal.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotal.ForeColor = Color.FromArgb(20, 34, 51);
            lblTotal.Margin = new Padding(0);
            lblTotal.Name = "lblTotal";
            lblTotal.TabIndex = 1;
            lblTotal.Text = "";
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            lblTotal.UseMnemonic = false;
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
            btnAdd.Size = new Size(160, 40);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add Selected";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // UnbilledChargesDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(820, 560);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(24, 20, 24, 20);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Unbilled Charges";
            Controls.Add(tlpRoot);
            Name = "UnbilledChargesDialog";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            tlpCardBody.ResumeLayout(false);
            tlpCardBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCharges).EndInit();
            tlpCardFooter.ResumeLayout(false);
            tlpCardFooter.PerformLayout();
            tlpButtons.ResumeLayout(false);
            tlpButtons.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.TableLayoutPanel tlpCardBody;
        private System.Windows.Forms.DataGridView dgvCharges;
        private System.Windows.Forms.TableLayoutPanel tlpCardFooter;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnCancelDialog;
        private System.Windows.Forms.Button btnAdd;
    }
}
