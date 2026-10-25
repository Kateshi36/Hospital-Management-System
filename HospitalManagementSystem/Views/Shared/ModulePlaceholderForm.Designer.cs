namespace HospitalManagementSystem.Views.Shared
{
    partial class ModulePlaceholderForm
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer? components = null;

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

        // NOTE: every colour, font, size and style is set in this file so the design is visible in
        // Visual Studio's [Design] view without running the application.

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            pnlCard = new System.Windows.Forms.Panel();
            pnlAccentBar = new System.Windows.Forms.Panel();
            lblIcon = new System.Windows.Forms.Label();
            lblModuleTitle = new System.Windows.Forms.Label();
            lblModuleCode = new System.Windows.Forms.Label();
            lblDescription = new System.Windows.Forms.Label();
            tlpRoot.SuspendLayout();
            pnlCard.SuspendLayout();
            pnlAccentBar.SuspendLayout();
            SuspendLayout();
            //
            // tlpRoot
            //
            tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 620F));
            tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 340F));
            tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlpRoot.Controls.Add(pnlCard, 1, 1);
            tlpRoot.BackColor = System.Drawing.Color.FromArgb(20, 32, 46);
            tlpRoot.ColumnCount = 3;
            tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpRoot.Location = new System.Drawing.Point(0, 0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 3;
            tlpRoot.Size = new System.Drawing.Size(1400, 800);
            tlpRoot.TabIndex = 96;
            //
            // pnlCard
            //
            pnlCard.Controls.Add(pnlAccentBar);
            pnlCard.Controls.Add(lblIcon);
            pnlCard.Controls.Add(lblModuleTitle);
            pnlCard.Controls.Add(lblModuleCode);
            pnlCard.Controls.Add(lblDescription);
            pnlCard.BackColor = System.Drawing.Color.FromArgb(28, 46, 64);
            pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlCard.Location = new System.Drawing.Point(0, 0);
            pnlCard.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new System.Drawing.Size(620, 340);
            pnlCard.TabIndex = 95;
            //
            // pnlAccentBar
            //
            pnlAccentBar.BackColor = System.Drawing.Color.FromArgb(31, 162, 184);
            pnlAccentBar.Dock = System.Windows.Forms.DockStyle.Top;
            pnlAccentBar.Location = new System.Drawing.Point(0, 0);
            pnlAccentBar.Name = "pnlAccentBar";
            pnlAccentBar.Size = new System.Drawing.Size(620, 6);
            pnlAccentBar.TabIndex = 90;
            //
            // lblIcon
            //
            lblIcon.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblIcon.BackColor = System.Drawing.Color.Transparent;
            lblIcon.Font = new System.Drawing.Font("Segoe UI Symbol", 34F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblIcon.ForeColor = System.Drawing.Color.FromArgb(31, 162, 184);
            lblIcon.Location = new System.Drawing.Point(0, 34);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new System.Drawing.Size(620, 64);
            lblIcon.TabIndex = 91;
            lblIcon.Text = "\u2699";
            lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblModuleTitle
            //
            lblModuleTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblModuleTitle.BackColor = System.Drawing.Color.Transparent;
            lblModuleTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblModuleTitle.ForeColor = System.Drawing.Color.FromArgb(232, 241, 245);
            lblModuleTitle.Location = new System.Drawing.Point(0, 108);
            lblModuleTitle.Name = "lblModuleTitle";
            lblModuleTitle.Size = new System.Drawing.Size(620, 44);
            lblModuleTitle.TabIndex = 92;
            lblModuleTitle.Text = "Module Title";
            lblModuleTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblModuleCode
            //
            lblModuleCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblModuleCode.BackColor = System.Drawing.Color.Transparent;
            lblModuleCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblModuleCode.ForeColor = System.Drawing.Color.FromArgb(31, 162, 184);
            lblModuleCode.Location = new System.Drawing.Point(0, 156);
            lblModuleCode.Name = "lblModuleCode";
            lblModuleCode.Size = new System.Drawing.Size(620, 28);
            lblModuleCode.TabIndex = 93;
            lblModuleCode.Text = "MP00  |  Coming in build Step 0";
            lblModuleCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblDescription
            //
            lblDescription.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblDescription.BackColor = System.Drawing.Color.Transparent;
            lblDescription.ForeColor = System.Drawing.Color.FromArgb(159, 179, 194);
            lblDescription.Location = new System.Drawing.Point(40, 204);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new System.Drawing.Size(540, 90);
            lblDescription.TabIndex = 94;
            lblDescription.Text = "Short description of what this module will do.";
            lblDescription.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // ModulePlaceholderForm
            //
            Controls.Add(tlpRoot);
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.FromArgb(20, 32, 46);
            ClientSize = new System.Drawing.Size(1400, 800);
            Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ForeColor = System.Drawing.Color.FromArgb(232, 241, 245);
            MinimumSize = new System.Drawing.Size(700, 460);
            Name = "ModulePlaceholderForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Module";
            pnlAccentBar.ResumeLayout(false);
            pnlCard.ResumeLayout(false);
            tlpRoot.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Panel pnlAccentBar;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lblModuleTitle;
        private System.Windows.Forms.Label lblModuleCode;
        private System.Windows.Forms.Label lblDescription;
    }
}