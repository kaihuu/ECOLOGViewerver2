namespace ECOLOGViewerver2
{
    partial class TripBrowser
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label12 = new System.Windows.Forms.Label();
            this.labelLoss = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelConsumedEnergy = new System.Windows.Forms.Label();
            this.labelLongitudinal_ACC = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.LabelCurrent = new System.Windows.Forms.Label();
            this.LabelMax = new System.Windows.Forms.Label();
            this.Slider = new System.Windows.Forms.TrackBar();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.Altitudelabel = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.SemanticLinkidlabel = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.linkIDTextBox = new System.Windows.Forms.TextBox();
            this.Linkidlabel = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.TimetextBox = new System.Windows.Forms.TextBox();
            this.Controllerbutton = new System.Windows.Forms.Button();
            this.ClickedcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DisplayImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetStartTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetEndTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Timechart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.Slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.ClickedcontextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Timechart)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(7, 5);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(480, 360);
            this.webBrowser1.TabIndex = 5;
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(100, 21);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 14);
            this.label12.TabIndex = 103;
            this.label12.Text = "[kW]";
            // 
            // labelLoss
            // 
            this.labelLoss.AutoSize = true;
            this.labelLoss.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.labelLoss.ForeColor = System.Drawing.Color.Black;
            this.labelLoss.Location = new System.Drawing.Point(10, 21);
            this.labelLoss.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLoss.Name = "labelLoss";
            this.labelLoss.Size = new System.Drawing.Size(87, 14);
            this.labelLoss.TabIndex = 102;
            this.labelLoss.Text = "0.0000000000";
            this.labelLoss.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(95, 19);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 14);
            this.label11.TabIndex = 100;
            this.label11.Text = "[kW]";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label8.Location = new System.Drawing.Point(76, 21);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 14);
            this.label8.TabIndex = 97;
            this.label8.Text = "[m/s^2]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label5.Location = new System.Drawing.Point(50, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 14);
            this.label5.TabIndex = 96;
            this.label5.Text = "[km/h]";
            // 
            // labelConsumedEnergy
            // 
            this.labelConsumedEnergy.AutoSize = true;
            this.labelConsumedEnergy.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.labelConsumedEnergy.ForeColor = System.Drawing.Color.Black;
            this.labelConsumedEnergy.Location = new System.Drawing.Point(4, 19);
            this.labelConsumedEnergy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelConsumedEnergy.Name = "labelConsumedEnergy";
            this.labelConsumedEnergy.Size = new System.Drawing.Size(87, 14);
            this.labelConsumedEnergy.TabIndex = 95;
            this.labelConsumedEnergy.Text = "0.0000000000";
            this.labelConsumedEnergy.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelLongitudinal_ACC
            // 
            this.labelLongitudinal_ACC.AutoSize = true;
            this.labelLongitudinal_ACC.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.labelLongitudinal_ACC.Location = new System.Drawing.Point(10, 21);
            this.labelLongitudinal_ACC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLongitudinal_ACC.Name = "labelLongitudinal_ACC";
            this.labelLongitudinal_ACC.Size = new System.Drawing.Size(59, 14);
            this.labelLongitudinal_ACC.TabIndex = 91;
            this.labelLongitudinal_ACC.Text = "0.000000";
            this.labelLongitudinal_ACC.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.labelSpeed.Location = new System.Drawing.Point(5, 20);
            this.labelSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(45, 14);
            this.labelSpeed.TabIndex = 87;
            this.labelSpeed.Text = "0.0000";
            this.labelSpeed.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LabelCurrent
            // 
            this.LabelCurrent.AutoSize = true;
            this.LabelCurrent.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.LabelCurrent.Location = new System.Drawing.Point(407, 25);
            this.LabelCurrent.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelCurrent.Name = "LabelCurrent";
            this.LabelCurrent.Size = new System.Drawing.Size(40, 16);
            this.LabelCurrent.TabIndex = 84;
            this.LabelCurrent.Text = "1000";
            this.LabelCurrent.Visible = false;
            // 
            // LabelMax
            // 
            this.LabelMax.AutoSize = true;
            this.LabelMax.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.LabelMax.Location = new System.Drawing.Point(419, 47);
            this.LabelMax.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelMax.Name = "LabelMax";
            this.LabelMax.Size = new System.Drawing.Size(53, 16);
            this.LabelMax.TabIndex = 83;
            this.LabelMax.Text = "/ 1000";
            this.LabelMax.Visible = false;
            // 
            // Slider
            // 
            this.Slider.Location = new System.Drawing.Point(6, 18);
            this.Slider.Name = "Slider";
            this.Slider.Size = new System.Drawing.Size(396, 45);
            this.Slider.TabIndex = 82;
            this.Slider.Scroll += new System.EventHandler(this.Slider_Scroll);
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxImage.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxImage.Location = new System.Drawing.Point(530, 5);
            this.pictureBoxImage.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(400, 300);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImage.TabIndex = 81;
            this.pictureBoxImage.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelSpeed);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(10, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(104, 46);
            this.groupBox1.TabIndex = 105;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Speed";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelLongitudinal_ACC);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(120, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(133, 47);
            this.groupBox2.TabIndex = 106;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Longitudinal_Acc";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.labelLoss);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Location = new System.Drawing.Point(303, 17);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(138, 47);
            this.groupBox6.TabIndex = 106;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Lost_Energy";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.labelConsumedEnergy);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Location = new System.Drawing.Point(154, 17);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(136, 45);
            this.groupBox7.TabIndex = 106;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Consumed_Energy";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.groupBox10);
            this.groupBox3.Controls.Add(this.groupBox9);
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Controls.Add(this.groupBox8);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(6, 383);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(481, 128);
            this.groupBox3.TabIndex = 107;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Property";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.Altitudelabel);
            this.groupBox10.Location = new System.Drawing.Point(407, 74);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(64, 47);
            this.groupBox10.TabIndex = 108;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Altitude";
            // 
            // Altitudelabel
            // 
            this.Altitudelabel.AutoSize = true;
            this.Altitudelabel.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.Altitudelabel.ForeColor = System.Drawing.Color.Black;
            this.Altitudelabel.Location = new System.Drawing.Point(16, 21);
            this.Altitudelabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Altitudelabel.Name = "Altitudelabel";
            this.Altitudelabel.Size = new System.Drawing.Size(31, 14);
            this.Altitudelabel.TabIndex = 102;
            this.Altitudelabel.Text = "00.0";
            this.Altitudelabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.SemanticLinkidlabel);
            this.groupBox9.Location = new System.Drawing.Point(10, 127);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(120, 47);
            this.groupBox9.TabIndex = 108;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "SEMANTIC_LINK_ID";
            // 
            // SemanticLinkidlabel
            // 
            this.SemanticLinkidlabel.AutoSize = true;
            this.SemanticLinkidlabel.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.SemanticLinkidlabel.ForeColor = System.Drawing.Color.Black;
            this.SemanticLinkidlabel.Location = new System.Drawing.Point(10, 21);
            this.SemanticLinkidlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SemanticLinkidlabel.Name = "SemanticLinkidlabel";
            this.SemanticLinkidlabel.Size = new System.Drawing.Size(14, 14);
            this.SemanticLinkidlabel.TabIndex = 102;
            this.SemanticLinkidlabel.Text = "0";
            this.SemanticLinkidlabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.linkIDTextBox);
            this.groupBox8.Controls.Add(this.Linkidlabel);
            this.groupBox8.Location = new System.Drawing.Point(262, 75);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(141, 47);
            this.groupBox8.TabIndex = 107;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "LINK_ID";
            // 
            // linkIDTextBox
            // 
            this.linkIDTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.linkIDTextBox.Location = new System.Drawing.Point(6, 23);
            this.linkIDTextBox.Name = "linkIDTextBox";
            this.linkIDTextBox.ReadOnly = true;
            this.linkIDTextBox.Size = new System.Drawing.Size(129, 12);
            this.linkIDTextBox.TabIndex = 103;
            this.linkIDTextBox.Text = "RB................";
            // 
            // Linkidlabel
            // 
            this.Linkidlabel.AutoSize = true;
            this.Linkidlabel.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.Linkidlabel.ForeColor = System.Drawing.Color.Black;
            this.Linkidlabel.Location = new System.Drawing.Point(10, 21);
            this.Linkidlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Linkidlabel.Name = "Linkidlabel";
            this.Linkidlabel.Size = new System.Drawing.Size(103, 14);
            this.Linkidlabel.TabIndex = 102;
            this.Linkidlabel.Text = "RBxxxxxxxxxxxxx";
            this.Linkidlabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.TimetextBox);
            this.groupBox5.Location = new System.Drawing.Point(10, 17);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(138, 46);
            this.groupBox5.TabIndex = 106;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Datetime";
            // 
            // TimetextBox
            // 
            this.TimetextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TimetextBox.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.TimetextBox.Location = new System.Drawing.Point(8, 17);
            this.TimetextBox.Name = "TimetextBox";
            this.TimetextBox.ReadOnly = true;
            this.TimetextBox.Size = new System.Drawing.Size(128, 14);
            this.TimetextBox.TabIndex = 86;
            this.TimetextBox.Text = "00:00:00";
            this.TimetextBox.Click += new System.EventHandler(this.TimetextBox_Enter);
            this.TimetextBox.Enter += new System.EventHandler(this.TimetextBox_Enter);
            // 
            // Controllerbutton
            // 
            this.Controllerbutton.Location = new System.Drawing.Point(334, 69);
            this.Controllerbutton.Name = "Controllerbutton";
            this.Controllerbutton.Size = new System.Drawing.Size(138, 23);
            this.Controllerbutton.TabIndex = 107;
            this.Controllerbutton.Text = "Show Chart Controller";
            this.Controllerbutton.UseVisualStyleBackColor = true;
            this.Controllerbutton.Click += new System.EventHandler(this.Controllerbutton_Click);
            // 
            // ClickedcontextMenuStrip
            // 
            this.ClickedcontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DisplayImageToolStripMenuItem,
            this.SetStartTimeToolStripMenuItem,
            this.SetEndTimeToolStripMenuItem});
            this.ClickedcontextMenuStrip.Name = "ClickedcontextMenuStrip";
            this.ClickedcontextMenuStrip.Size = new System.Drawing.Size(182, 70);
            // 
            // DisplayImageToolStripMenuItem
            // 
            this.DisplayImageToolStripMenuItem.Name = "DisplayImageToolStripMenuItem";
            this.DisplayImageToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.DisplayImageToolStripMenuItem.Text = "Display Picture";
            this.DisplayImageToolStripMenuItem.Click += new System.EventHandler(this.DisplayImageToolStripMenuItem_Click);
            // 
            // SetStartTimeToolStripMenuItem
            // 
            this.SetStartTimeToolStripMenuItem.Name = "SetStartTimeToolStripMenuItem";
            this.SetStartTimeToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.SetStartTimeToolStripMenuItem.Text = "Set JST on Start_time";
            this.SetStartTimeToolStripMenuItem.Click += new System.EventHandler(this.SetStartTimeToolStripMenuItem_Click);
            // 
            // SetEndTimeToolStripMenuItem
            // 
            this.SetEndTimeToolStripMenuItem.Name = "SetEndTimeToolStripMenuItem";
            this.SetEndTimeToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.SetEndTimeToolStripMenuItem.Text = "Set JST on End_time";
            this.SetEndTimeToolStripMenuItem.Click += new System.EventHandler(this.SetEndTimeToolStripMenuItem_Click);
            // 
            // Timechart
            // 
            legend5.Enabled = false;
            legend5.Name = "Legend1";
            this.Timechart.Legends.Add(legend5);
            this.Timechart.Location = new System.Drawing.Point(505, 320);
            this.Timechart.Name = "Timechart";
            this.Timechart.Size = new System.Drawing.Size(442, 307);
            this.Timechart.TabIndex = 111;
            this.Timechart.Text = "chart1";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.Slider);
            this.groupBox4.Controls.Add(this.LabelMax);
            this.groupBox4.Controls.Add(this.LabelCurrent);
            this.groupBox4.Controls.Add(this.Controllerbutton);
            this.groupBox4.Location = new System.Drawing.Point(7, 521);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(480, 106);
            this.groupBox4.TabIndex = 112;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Control";
            // 
            // TripBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 642);
            this.Controls.Add(this.Timechart);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.groupBox3);
            this.MaximizeBox = false;
            this.Name = "TripBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ECOLOGViewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Browser_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.Slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ClickedcontextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Timechart)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelLoss;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelConsumedEnergy;
        private System.Windows.Forms.Label labelLongitudinal_ACC;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label LabelCurrent;
        private System.Windows.Forms.Label LabelMax;
        private System.Windows.Forms.TrackBar Slider;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ContextMenuStrip ClickedcontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SetStartTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetEndTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisplayImageToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart Timechart;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox TimetextBox;
        private System.Windows.Forms.Button Controllerbutton;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label Linkidlabel;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label SemanticLinkidlabel;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label Altitudelabel;
        private System.Windows.Forms.TextBox linkIDTextBox; 
    }
}