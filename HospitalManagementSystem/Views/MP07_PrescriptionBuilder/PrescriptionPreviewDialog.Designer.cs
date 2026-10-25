namespace HospitalManagementSystem.Views.MP07_Prescription
{
    partial class PrescriptionPreviewDialog
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

        // Redesign: the prescription on a white sheet card, Close underneath.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlPaper = new Panel();
            rtbPreview = new RichTextBox();
            tlpButtons = new TableLayoutPanel();
            btnClose = new Button();
            tlpRoot.SuspendLayout();
            pnlPaper.SuspendLayout();
            tlpButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.BackColor = Color.FromArgb(243, 246, 249);
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(pnlPaper, 0, 0);
            tlpRoot.Controls.Add(tlpButtons, 0, 1);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 2;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpRoot.TabIndex = 0;
            // 
            // pnlPaper
            // 
            pnlPaper.BackColor = Color.White;
            pnlPaper.Controls.Add(rtbPreview);
            pnlPaper.Dock = DockStyle.Fill;
            pnlPaper.Margin = new Padding(0, 0, 0, 16);
            pnlPaper.Name = "pnlPaper";
            pnlPaper.Padding = new Padding(24, 20, 16, 16);
            pnlPaper.TabIndex = 0;
            // 
            // rtbPreview
            // 
            rtbPreview.BackColor = Color.White;
            rtbPreview.BorderStyle = BorderStyle.None;
            rtbPreview.DetectUrls = false;
            rtbPreview.Dock = DockStyle.Fill;
            rtbPreview.Margin = new Padding(0);
            rtbPreview.Name = "rtbPreview";
            rtbPreview.ReadOnly = true;
            rtbPreview.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbPreview.TabIndex = 0;
            // 
            // tlpButtons
            // 
            tlpButtons.BackColor = Color.Transparent;
            tlpButtons.ColumnCount = 2;
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpButtons.Controls.Add(btnClose, 1, 0);
            tlpButtons.Dock = DockStyle.Fill;
            tlpButtons.Margin = new Padding(0);
            tlpButtons.Name = "tlpButtons";
            tlpButtons.RowCount = 1;
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpButtons.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClose.ForeColor = Color.FromArgb(20, 34, 51);
            btnClose.Margin = new Padding(0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(120, 40);
            btnClose.TabIndex = 0;
            btnClose.Text = "Close";
            btnClose.UseMnemonic = false;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // PrescriptionPreviewDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(640, 760);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(24, 20, 24, 20);
            AcceptButton = btnClose;
            CancelButton = btnClose;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Prescription";
            Controls.Add(tlpRoot);
            Name = "PrescriptionPreviewDialog";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlPaper.ResumeLayout(false);
            pnlPaper.PerformLayout();
            tlpButtons.ResumeLayout(false);
            tlpButtons.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlPaper;
        private System.Windows.Forms.RichTextBox rtbPreview;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnClose;
    }
}
