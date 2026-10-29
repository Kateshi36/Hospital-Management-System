namespace HospitalManagementSystem.Views.MP11_Billing
{
    partial class VoidBillDialog
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

        // Void bill: title, bill line, a card with the reason and the corrected-copy option; Keep / Void underneath.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            lblTitle = new Label();
            lblInfo = new Label();
            pnlCard = new Panel();
            tlpReason = new TableLayoutPanel();
            lblReasonCaption = new Label();
            txtReason = new TextBox();
            chkCorrection = new CheckBox();
            tlpButtons = new TableLayoutPanel();
            btnKeep = new Button();
            btnConfirm = new Button();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            pnlCard.SuspendLayout();
            tlpReason.SuspendLayout();
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
            pnlCard.Controls.Add(tlpReason);
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.Margin = new Padding(0, 0, 0, 16);
            pnlCard.Name = "pnlCard";
            pnlCard.Padding = new Padding(20, 18, 20, 4);
            pnlCard.TabIndex = 2;
            // 
            // tlpReason
            // 
            tlpReason.BackColor = Color.Transparent;
            tlpReason.ColumnCount = 1;
            tlpReason.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpReason.Controls.Add(lblReasonCaption, 0, 0);
            tlpReason.Controls.Add(txtReason, 0, 1);
            tlpReason.Controls.Add(chkCorrection, 0, 2);
            tlpReason.Dock = DockStyle.Fill;
            tlpReason.Margin = new Padding(0);
            tlpReason.Name = "tlpReason";
            tlpReason.RowCount = 3;
            tlpReason.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpReason.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpReason.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpReason.TabIndex = 0;
            // 
            // lblReasonCaption
            // 
            lblReasonCaption.AutoSize = true;
            lblReasonCaption.BackColor = Color.Transparent;
            lblReasonCaption.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblReasonCaption.ForeColor = Color.FromArgb(20, 34, 51);
            lblReasonCaption.Margin = new Padding(0, 0, 0, 6);
            lblReasonCaption.Name = "lblReasonCaption";
            lblReasonCaption.TabIndex = 0;
            lblReasonCaption.Text = "Reason for voiding *";
            // 
            // txtReason
            // 
            txtReason.AutoSize = false;
            txtReason.BackColor = Color.White;
            txtReason.BorderStyle = BorderStyle.FixedSingle;
            txtReason.Dock = DockStyle.Fill;
            txtReason.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtReason.ForeColor = Color.FromArgb(20, 34, 51);
            txtReason.Margin = new Padding(0, 0, 0, 12);
            txtReason.Multiline = true;
            txtReason.Name = "txtReason";
            txtReason.ScrollBars = ScrollBars.Vertical;
            txtReason.Size = new Size(200, 90);
            txtReason.TabIndex = 1;
            // 
            // chkCorrection
            // 
            chkCorrection.AutoSize = true;
            chkCorrection.BackColor = Color.Transparent;
            chkCorrection.Checked = true;
            chkCorrection.CheckState = CheckState.Checked;
            chkCorrection.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkCorrection.ForeColor = Color.FromArgb(20, 34, 51);
            chkCorrection.Margin = new Padding(0, 0, 0, 14);
            chkCorrection.Name = "chkCorrection";
            chkCorrection.TabIndex = 2;
            chkCorrection.Text = "Create a corrected copy as a new draft (same patient, discount and items)";
            chkCorrection.UseVisualStyleBackColor = false;
            // 
            // tlpButtons
            // 
            tlpButtons.BackColor = Color.Transparent;
            tlpButtons.ColumnCount = 3;
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpButtons.Controls.Add(btnKeep, 1, 0);
            tlpButtons.Controls.Add(btnConfirm, 2, 0);
            tlpButtons.Dock = DockStyle.Fill;
            tlpButtons.Margin = new Padding(0);
            tlpButtons.Name = "tlpButtons";
            tlpButtons.RowCount = 1;
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpButtons.TabIndex = 3;
            // 
            // btnKeep
            // 
            btnKeep.BackColor = Color.White;
            btnKeep.FlatStyle = FlatStyle.Flat;
            btnKeep.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnKeep.ForeColor = Color.FromArgb(20, 34, 51);
            btnKeep.Margin = new Padding(10, 0, 0, 0);
            btnKeep.Name = "btnKeep";
            btnKeep.Size = new Size(130, 40);
            btnKeep.TabIndex = 0;
            btnKeep.Text = "Keep Bill";
            btnKeep.UseVisualStyleBackColor = false;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.FromArgb(180, 35, 24);
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Margin = new Padding(10, 0, 0, 0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(150, 40);
            btnConfirm.TabIndex = 1;
            btnConfirm.Text = "Void Bill";
            btnConfirm.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            // 
            // VoidBillDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(600, 440);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(24, 20, 24, 20);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Void Bill";
            Controls.Add(tlpRoot);
            Name = "VoidBillDialog";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            tlpReason.ResumeLayout(false);
            tlpReason.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel tlpReason;
        private System.Windows.Forms.Label lblReasonCaption;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.CheckBox chkCorrection;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnKeep;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
