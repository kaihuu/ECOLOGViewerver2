namespace ECOLOGViewerver2
{
    partial class HistoryGraphDialog
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
            this.search_button = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.InfocomboBox = new System.Windows.Forms.ComboBox();
            this.endtime_label = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.AggregationcomboBox = new System.Windows.Forms.ComboBox();
            this.starttime_label = new System.Windows.Forms.Label();
            this.start_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.end_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.Driver_comboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ChartcomboBox = new System.Windows.Forms.ComboBox();
            this.EndTime_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.StartTime_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // search_button
            // 
            this.search_button.Location = new System.Drawing.Point(282, 112);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(79, 25);
            this.search_button.TabIndex = 120;
            this.search_button.Text = "Display";
            this.search_button.UseVisualStyleBackColor = true;
            this.search_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(138, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 12);
            this.label15.TabIndex = 119;
            this.label15.Text = "Content";
            // 
            // InfocomboBox
            // 
            this.InfocomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InfocomboBox.FormattingEnabled = true;
            this.InfocomboBox.Items.AddRange(new object[] {
            "ConsumedEnergy"});
            this.InfocomboBox.Location = new System.Drawing.Point(140, 25);
            this.InfocomboBox.Name = "InfocomboBox";
            this.InfocomboBox.Size = new System.Drawing.Size(114, 20);
            this.InfocomboBox.TabIndex = 118;
            // 
            // endtime_label
            // 
            this.endtime_label.AutoSize = true;
            this.endtime_label.Location = new System.Drawing.Point(104, 81);
            this.endtime_label.Name = "endtime_label";
            this.endtime_label.Size = new System.Drawing.Size(17, 12);
            this.endtime_label.TabIndex = 117;
            this.endtime_label.Text = "～";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(259, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 12);
            this.label9.TabIndex = 116;
            this.label9.Text = "Aggregation";
            // 
            // AggregationcomboBox
            // 
            this.AggregationcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AggregationcomboBox.FormattingEnabled = true;
            this.AggregationcomboBox.Items.AddRange(new object[] {
            "Month"});
            this.AggregationcomboBox.Location = new System.Drawing.Point(261, 25);
            this.AggregationcomboBox.Name = "AggregationcomboBox";
            this.AggregationcomboBox.Size = new System.Drawing.Size(99, 20);
            this.AggregationcomboBox.TabIndex = 115;
            // 
            // starttime_label
            // 
            this.starttime_label.AutoSize = true;
            this.starttime_label.Location = new System.Drawing.Point(15, 55);
            this.starttime_label.Name = "starttime_label";
            this.starttime_label.Size = new System.Drawing.Size(37, 12);
            this.starttime_label.TabIndex = 111;
            this.starttime_label.Text = "Period";
            // 
            // start_dateTimePicker
            // 
            this.start_dateTimePicker.Location = new System.Drawing.Point(43, 113);
            this.start_dateTimePicker.Name = "start_dateTimePicker";
            this.start_dateTimePicker.Size = new System.Drawing.Size(140, 19);
            this.start_dateTimePicker.TabIndex = 109;
            this.start_dateTimePicker.Value = new System.DateTime(2011, 7, 12, 0, 0, 0, 0);
            this.start_dateTimePicker.Visible = false;
            // 
            // end_dateTimePicker
            // 
            this.end_dateTimePicker.Location = new System.Drawing.Point(140, 131);
            this.end_dateTimePicker.Name = "end_dateTimePicker";
            this.end_dateTimePicker.Size = new System.Drawing.Size(140, 19);
            this.end_dateTimePicker.TabIndex = 110;
            this.end_dateTimePicker.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 12);
            this.label1.TabIndex = 124;
            this.label1.Text = "DRIVERS";
            // 
            // Driver_comboBox
            // 
            this.Driver_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Driver_comboBox.FormattingEnabled = true;
            this.Driver_comboBox.Location = new System.Drawing.Point(14, 25);
            this.Driver_comboBox.Name = "Driver_comboBox";
            this.Driver_comboBox.Size = new System.Drawing.Size(114, 20);
            this.Driver_comboBox.TabIndex = 123;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 12);
            this.label2.TabIndex = 126;
            this.label2.Text = "Chart";
            // 
            // ChartcomboBox
            // 
            this.ChartcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChartcomboBox.FormattingEnabled = true;
            this.ChartcomboBox.Items.AddRange(new object[] {
            "Colmun Chart",
            "Box Plot Chart"});
            this.ChartcomboBox.Location = new System.Drawing.Point(220, 78);
            this.ChartcomboBox.Name = "ChartcomboBox";
            this.ChartcomboBox.Size = new System.Drawing.Size(140, 20);
            this.ChartcomboBox.TabIndex = 125;
            // 
            // EndTime_dateTimePicker
            // 
            this.EndTime_dateTimePicker.CalendarFont = new System.Drawing.Font("MS UI Gothic", 12F);
            this.EndTime_dateTimePicker.CustomFormat = "yyy/MM";
            this.EndTime_dateTimePicker.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.EndTime_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndTime_dateTimePicker.Location = new System.Drawing.Point(127, 75);
            this.EndTime_dateTimePicker.Name = "EndTime_dateTimePicker";
            this.EndTime_dateTimePicker.ShowUpDown = true;
            this.EndTime_dateTimePicker.Size = new System.Drawing.Size(83, 23);
            this.EndTime_dateTimePicker.TabIndex = 127;
            this.EndTime_dateTimePicker.Value = new System.DateTime(2013, 10, 16, 0, 0, 0, 0);
            // 
            // StartTime_dateTimePicker
            // 
            this.StartTime_dateTimePicker.CalendarFont = new System.Drawing.Font("MS UI Gothic", 12F);
            this.StartTime_dateTimePicker.CustomFormat = "yyy/MM";
            this.StartTime_dateTimePicker.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.StartTime_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartTime_dateTimePicker.Location = new System.Drawing.Point(17, 75);
            this.StartTime_dateTimePicker.Name = "StartTime_dateTimePicker";
            this.StartTime_dateTimePicker.ShowUpDown = true;
            this.StartTime_dateTimePicker.Size = new System.Drawing.Size(81, 23);
            this.StartTime_dateTimePicker.TabIndex = 129;
            this.StartTime_dateTimePicker.Value = new System.DateTime(2013, 10, 16, 0, 0, 0, 0);
            // 
            // ColmunGraphDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 147);
            this.Controls.Add(this.EndTime_dateTimePicker);
            this.Controls.Add(this.StartTime_dateTimePicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ChartcomboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Driver_comboBox);
            this.Controls.Add(this.search_button);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.endtime_label);
            this.Controls.Add(this.AggregationcomboBox);
            this.Controls.Add(this.starttime_label);
            this.Controls.Add(this.start_dateTimePicker);
            this.Controls.Add(this.end_dateTimePicker);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.InfocomboBox);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColmunGraphDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ColmunGraphDialog";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ColmunGraphDialog_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button search_button;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox InfocomboBox;
        private System.Windows.Forms.Label endtime_label;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox AggregationcomboBox;
        private System.Windows.Forms.Label starttime_label;
        private System.Windows.Forms.DateTimePicker start_dateTimePicker;
        private System.Windows.Forms.DateTimePicker end_dateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Driver_comboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ChartcomboBox;
        private System.Windows.Forms.DateTimePicker EndTime_dateTimePicker;
        private System.Windows.Forms.DateTimePicker StartTime_dateTimePicker;
    }
}