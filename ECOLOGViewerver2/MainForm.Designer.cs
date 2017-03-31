namespace ECOLOGViewerver2
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tRIPBROWSERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.semanticLinkBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dailyEnergyGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.v2XEstimationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registeredAnnotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateECOLOGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Mainpanel = new System.Windows.Forms.Panel();
            this.WindowTogglecheckBox = new System.Windows.Forms.CheckBox();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tRIPBROWSERToolStripMenuItem,
            this.semanticLinkBrowserToolStripMenuItem,
            this.dailyEnergyGraphToolStripMenuItem,
            this.historyGraphToolStripMenuItem,
            this.v2XEstimationToolStripMenuItem,
            this.registeredAnnotationToolStripMenuItem,
            this.updateECOLOGToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(984, 24);
            this.menuStrip.TabIndex = 54;
            this.menuStrip.Text = "menuStrip1";
            // 
            // tRIPBROWSERToolStripMenuItem
            // 
            this.tRIPBROWSERToolStripMenuItem.Name = "tRIPBROWSERToolStripMenuItem";
            this.tRIPBROWSERToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.tRIPBROWSERToolStripMenuItem.Text = "Trip Browser";
            this.tRIPBROWSERToolStripMenuItem.Click += new System.EventHandler(this.tripBrowserToolStripMenuItem_Click);
            // 
            // semanticLinkBrowserToolStripMenuItem
            // 
            this.semanticLinkBrowserToolStripMenuItem.Name = "semanticLinkBrowserToolStripMenuItem";
            this.semanticLinkBrowserToolStripMenuItem.Size = new System.Drawing.Size(150, 20);
            this.semanticLinkBrowserToolStripMenuItem.Text = "SemanticLink Browser";
            this.semanticLinkBrowserToolStripMenuItem.Click += new System.EventHandler(this.semanticLinkBrowserToolStripMenuItem_Click);
            // 
            // dailyEnergyGraphToolStripMenuItem
            // 
            this.dailyEnergyGraphToolStripMenuItem.Name = "dailyEnergyGraphToolStripMenuItem";
            this.dailyEnergyGraphToolStripMenuItem.Size = new System.Drawing.Size(131, 20);
            this.dailyEnergyGraphToolStripMenuItem.Text = "Daily Energy Graph";
            this.dailyEnergyGraphToolStripMenuItem.Click += new System.EventHandler(this.dailyEnergyGraphToolStripMenuItem_Click);
            // 
            // historyGraphToolStripMenuItem
            // 
            this.historyGraphToolStripMenuItem.Name = "historyGraphToolStripMenuItem";
            this.historyGraphToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.historyGraphToolStripMenuItem.Text = "History Graph";
            this.historyGraphToolStripMenuItem.Click += new System.EventHandler(this.historyGraphToolStripMenuItem_Click);
            // 
            // v2XEstimationToolStripMenuItem
            // 
            this.v2XEstimationToolStripMenuItem.Name = "v2XEstimationToolStripMenuItem";
            this.v2XEstimationToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.v2XEstimationToolStripMenuItem.Text = "V2X Estimation";
            this.v2XEstimationToolStripMenuItem.Click += new System.EventHandler(this.v2XEstimationToolStripMenuItem_Click);
            // 
            // registeredAnnotationToolStripMenuItem
            // 
            this.registeredAnnotationToolStripMenuItem.Name = "registeredAnnotationToolStripMenuItem";
            this.registeredAnnotationToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.registeredAnnotationToolStripMenuItem.Text = "Annotations";
            this.registeredAnnotationToolStripMenuItem.Click += new System.EventHandler(this.registeredAnnotationToolStripMenuItem_Click);
            // 
            // updateECOLOGToolStripMenuItem
            // 
            this.updateECOLOGToolStripMenuItem.Name = "updateECOLOGToolStripMenuItem";
            this.updateECOLOGToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.updateECOLOGToolStripMenuItem.Text = "Update ECOLOG";
            this.updateECOLOGToolStripMenuItem.Click += new System.EventHandler(this.updateECOLOGToolStripMenuItem_Click);
            // 
            // Mainpanel
            // 
            this.Mainpanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Mainpanel.BackColor = System.Drawing.Color.SlateGray;
            this.Mainpanel.Location = new System.Drawing.Point(0, 25);
            this.Mainpanel.Name = "Mainpanel";
            this.Mainpanel.Size = new System.Drawing.Size(984, 679);
            this.Mainpanel.TabIndex = 55;
            // 
            // WindowTogglecheckBox
            // 
            this.WindowTogglecheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.WindowTogglecheckBox.AutoSize = true;
            this.WindowTogglecheckBox.Location = new System.Drawing.Point(790, 1);
            this.WindowTogglecheckBox.Name = "WindowTogglecheckBox";
            this.WindowTogglecheckBox.Size = new System.Drawing.Size(193, 22);
            this.WindowTogglecheckBox.TabIndex = 0;
            this.WindowTogglecheckBox.Text = "Open Windows within Main Window";
            this.WindowTogglecheckBox.UseVisualStyleBackColor = true;
            this.WindowTogglecheckBox.CheckedChanged += new System.EventHandler(this.WindowTogglecheckBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 702);
            this.Controls.Add(this.WindowTogglecheckBox);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.Mainpanel);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ECOLOGViewer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        internal System.Windows.Forms.Panel Mainpanel;
        private System.Windows.Forms.ToolStripMenuItem tRIPBROWSERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem semanticLinkBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem v2XEstimationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registeredAnnotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dailyEnergyGraphToolStripMenuItem;
        private System.Windows.Forms.CheckBox WindowTogglecheckBox;
        private System.Windows.Forms.ToolStripMenuItem updateECOLOGToolStripMenuItem;
    }
}

