namespace ECOLOGViewerver2
{
    partial class V2XDialog
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
            this.DisplayAllCalendarbutton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DrivercheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Year_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.Month_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.DisplayHistogrambutton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.minY_textBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.maxY_textBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.minX_textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.maxX_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.QueryEdit_checkBox = new System.Windows.Forms.CheckBox();
            this.DEMO_checkBox = new System.Windows.Forms.CheckBox();
            this.endtime_label = new System.Windows.Forms.Label();
            this.starttime_label = new System.Windows.Forms.Label();
            this.start_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.end_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.AirConcheckBox = new System.Windows.Forms.CheckBox();
            this.EquipmentcheckBox = new System.Windows.Forms.CheckBox();
            this.MaxtextBox = new System.Windows.Forms.TextBox();
            this.LinecheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // DisplayAllCalendarbutton
            // 
            this.DisplayAllCalendarbutton.Location = new System.Drawing.Point(214, 84);
            this.DisplayAllCalendarbutton.Name = "DisplayAllCalendarbutton";
            this.DisplayAllCalendarbutton.Size = new System.Drawing.Size(108, 25);
            this.DisplayAllCalendarbutton.TabIndex = 115;
            this.DisplayAllCalendarbutton.Text = "Display Calendar";
            this.DisplayAllCalendarbutton.UseVisualStyleBackColor = true;
            this.DisplayAllCalendarbutton.Click += new System.EventHandler(this.DisplayCalendar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DrivercheckedListBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(193, 228);
            this.groupBox2.TabIndex = 117;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Driver Select";
            // 
            // DrivercheckedListBox
            // 
            this.DrivercheckedListBox.CheckOnClick = true;
            this.DrivercheckedListBox.FormattingEnabled = true;
            this.DrivercheckedListBox.Location = new System.Drawing.Point(8, 20);
            this.DrivercheckedListBox.Name = "DrivercheckedListBox";
            this.DrivercheckedListBox.Size = new System.Drawing.Size(168, 200);
            this.DrivercheckedListBox.TabIndex = 116;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 12);
            this.label3.TabIndex = 120;
            this.label3.Text = "MONTH";
            // 
            // Year_dateTimePicker
            // 
            this.Year_dateTimePicker.CalendarFont = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Year_dateTimePicker.CustomFormat = "yyyy";
            this.Year_dateTimePicker.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Year_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Year_dateTimePicker.Location = new System.Drawing.Point(20, 44);
            this.Year_dateTimePicker.Name = "Year_dateTimePicker";
            this.Year_dateTimePicker.ShowUpDown = true;
            this.Year_dateTimePicker.Size = new System.Drawing.Size(57, 23);
            this.Year_dateTimePicker.TabIndex = 119;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 118;
            this.label2.Text = "YEAR";
            // 
            // Month_dateTimePicker
            // 
            this.Month_dateTimePicker.CalendarFont = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Month_dateTimePicker.CustomFormat = "MM";
            this.Month_dateTimePicker.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Month_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Month_dateTimePicker.Location = new System.Drawing.Point(106, 42);
            this.Month_dateTimePicker.Name = "Month_dateTimePicker";
            this.Month_dateTimePicker.ShowUpDown = true;
            this.Month_dateTimePicker.Size = new System.Drawing.Size(62, 23);
            this.Month_dateTimePicker.TabIndex = 117;
            // 
            // DisplayHistogrambutton
            // 
            this.DisplayHistogrambutton.Location = new System.Drawing.Point(220, 138);
            this.DisplayHistogrambutton.Name = "DisplayHistogrambutton";
            this.DisplayHistogrambutton.Size = new System.Drawing.Size(108, 25);
            this.DisplayHistogrambutton.TabIndex = 121;
            this.DisplayHistogrambutton.Text = "Display Histogram";
            this.DisplayHistogrambutton.UseVisualStyleBackColor = true;
            this.DisplayHistogrambutton.Click += new System.EventHandler(this.DisplayHistogrambutton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Month_dateTimePicker);
            this.groupBox1.Controls.Add(this.Year_dateTimePicker);
            this.groupBox1.Controls.Add(this.DisplayAllCalendarbutton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(211, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 115);
            this.groupBox1.TabIndex = 118;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "V2X Calendar";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LinecheckBox);
            this.groupBox3.Controls.Add(this.MaxtextBox);
            this.groupBox3.Controls.Add(this.minY_textBox);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.maxY_textBox);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.minX_textBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.maxX_textBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.QueryEdit_checkBox);
            this.groupBox3.Controls.Add(this.DEMO_checkBox);
            this.groupBox3.Controls.Add(this.endtime_label);
            this.groupBox3.Controls.Add(this.starttime_label);
            this.groupBox3.Controls.Add(this.start_dateTimePicker);
            this.groupBox3.Controls.Add(this.end_dateTimePicker);
            this.groupBox3.Controls.Add(this.DisplayHistogrambutton);
            this.groupBox3.Location = new System.Drawing.Point(212, 133);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(334, 165);
            this.groupBox3.TabIndex = 119;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "V2X Histogram";
            // 
            // minY_textBox
            // 
            this.minY_textBox.Location = new System.Drawing.Point(101, 138);
            this.minY_textBox.Name = "minY_textBox";
            this.minY_textBox.Size = new System.Drawing.Size(100, 19);
            this.minY_textBox.TabIndex = 135;
            this.minY_textBox.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 12);
            this.label6.TabIndex = 134;
            this.label6.Text = "Min of Axis Y";
            // 
            // maxY_textBox
            // 
            this.maxY_textBox.Location = new System.Drawing.Point(101, 113);
            this.maxY_textBox.Name = "maxY_textBox";
            this.maxY_textBox.Size = new System.Drawing.Size(100, 19);
            this.maxY_textBox.TabIndex = 133;
            this.maxY_textBox.Text = "30";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 12);
            this.label5.TabIndex = 132;
            this.label5.Text = "Max of Axis Y";
            // 
            // minX_textBox
            // 
            this.minX_textBox.Location = new System.Drawing.Point(101, 88);
            this.minX_textBox.Name = "minX_textBox";
            this.minX_textBox.Size = new System.Drawing.Size(100, 19);
            this.minX_textBox.TabIndex = 131;
            this.minX_textBox.Text = "-1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 12);
            this.label4.TabIndex = 130;
            this.label4.Text = "Min of Axis X";
            // 
            // maxX_textBox
            // 
            this.maxX_textBox.Location = new System.Drawing.Point(101, 65);
            this.maxX_textBox.Name = "maxX_textBox";
            this.maxX_textBox.Size = new System.Drawing.Size(100, 19);
            this.maxX_textBox.TabIndex = 129;
            this.maxX_textBox.Text = "14";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 12);
            this.label1.TabIndex = 128;
            this.label1.Text = "Max of Axis X";
            // 
            // QueryEdit_checkBox
            // 
            this.QueryEdit_checkBox.AutoSize = true;
            this.QueryEdit_checkBox.Location = new System.Drawing.Point(213, 68);
            this.QueryEdit_checkBox.Name = "QueryEdit_checkBox";
            this.QueryEdit_checkBox.Size = new System.Drawing.Size(78, 16);
            this.QueryEdit_checkBox.TabIndex = 127;
            this.QueryEdit_checkBox.Text = "Query Edit";
            this.QueryEdit_checkBox.UseVisualStyleBackColor = true;
            // 
            // DEMO_checkBox
            // 
            this.DEMO_checkBox.AutoSize = true;
            this.DEMO_checkBox.Checked = true;
            this.DEMO_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DEMO_checkBox.Location = new System.Drawing.Point(213, 91);
            this.DEMO_checkBox.Name = "DEMO_checkBox";
            this.DEMO_checkBox.Size = new System.Drawing.Size(53, 16);
            this.DEMO_checkBox.TabIndex = 126;
            this.DEMO_checkBox.Text = "Demo";
            this.DEMO_checkBox.UseVisualStyleBackColor = true;
            // 
            // endtime_label
            // 
            this.endtime_label.AutoSize = true;
            this.endtime_label.Location = new System.Drawing.Point(154, 41);
            this.endtime_label.Name = "endtime_label";
            this.endtime_label.Size = new System.Drawing.Size(17, 12);
            this.endtime_label.TabIndex = 125;
            this.endtime_label.Text = "～";
            // 
            // starttime_label
            // 
            this.starttime_label.AutoSize = true;
            this.starttime_label.Location = new System.Drawing.Point(6, 15);
            this.starttime_label.Name = "starttime_label";
            this.starttime_label.Size = new System.Drawing.Size(37, 12);
            this.starttime_label.TabIndex = 124;
            this.starttime_label.Text = "Period";
            // 
            // start_dateTimePicker
            // 
            this.start_dateTimePicker.Location = new System.Drawing.Point(8, 36);
            this.start_dateTimePicker.Name = "start_dateTimePicker";
            this.start_dateTimePicker.Size = new System.Drawing.Size(140, 19);
            this.start_dateTimePicker.TabIndex = 122;
            this.start_dateTimePicker.Value = new System.DateTime(2011, 7, 12, 0, 0, 0, 0);
            // 
            // end_dateTimePicker
            // 
            this.end_dateTimePicker.Location = new System.Drawing.Point(181, 36);
            this.end_dateTimePicker.Name = "end_dateTimePicker";
            this.end_dateTimePicker.Size = new System.Drawing.Size(140, 19);
            this.end_dateTimePicker.TabIndex = 123;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.AirConcheckBox);
            this.groupBox4.Controls.Add(this.EquipmentcheckBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 246);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(193, 52);
            this.groupBox4.TabIndex = 120;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Setting";
            // 
            // AirConcheckBox
            // 
            this.AirConcheckBox.AutoSize = true;
            this.AirConcheckBox.Checked = true;
            this.AirConcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AirConcheckBox.Location = new System.Drawing.Point(18, 30);
            this.AirConcheckBox.Name = "AirConcheckBox";
            this.AirConcheckBox.Size = new System.Drawing.Size(98, 16);
            this.AirConcheckBox.TabIndex = 1;
            this.AirConcheckBox.Text = "Air Conditoner";
            this.AirConcheckBox.UseVisualStyleBackColor = true;
            // 
            // EquipmentcheckBox
            // 
            this.EquipmentcheckBox.AutoSize = true;
            this.EquipmentcheckBox.Checked = true;
            this.EquipmentcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EquipmentcheckBox.Location = new System.Drawing.Point(18, 13);
            this.EquipmentcheckBox.Name = "EquipmentcheckBox";
            this.EquipmentcheckBox.Size = new System.Drawing.Size(120, 16);
            this.EquipmentcheckBox.TabIndex = 0;
            this.EquipmentcheckBox.Text = "Electric Equipment";
            this.EquipmentcheckBox.UseVisualStyleBackColor = true;
            // 
            // MaxtextBox
            // 
            this.MaxtextBox.Location = new System.Drawing.Point(282, 111);
            this.MaxtextBox.Name = "MaxtextBox";
            this.MaxtextBox.Size = new System.Drawing.Size(37, 19);
            this.MaxtextBox.TabIndex = 137;
            this.MaxtextBox.Text = "20";
            // 
            // LinecheckBox
            // 
            this.LinecheckBox.AutoSize = true;
            this.LinecheckBox.Location = new System.Drawing.Point(213, 113);
            this.LinecheckBox.Name = "LinecheckBox";
            this.LinecheckBox.Size = new System.Drawing.Size(63, 16);
            this.LinecheckBox.TabIndex = 138;
            this.LinecheckBox.Text = "N% Line";
            this.LinecheckBox.UseVisualStyleBackColor = true;
            // 
            // New_V2XDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 305);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "New_V2XDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "V2X Estimation Dialog";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CalendarDialog_KeyDown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DisplayAllCalendarbutton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox DrivercheckedListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker Month_dateTimePicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker Year_dateTimePicker;
        private System.Windows.Forms.Button DisplayHistogrambutton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label endtime_label;
        private System.Windows.Forms.Label starttime_label;
        private System.Windows.Forms.DateTimePicker start_dateTimePicker;
        private System.Windows.Forms.DateTimePicker end_dateTimePicker;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox AirConcheckBox;
        private System.Windows.Forms.CheckBox EquipmentcheckBox;
        private System.Windows.Forms.CheckBox DEMO_checkBox;
        private System.Windows.Forms.CheckBox QueryEdit_checkBox;
        private System.Windows.Forms.TextBox minY_textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox maxY_textBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox minX_textBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox maxX_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MaxtextBox;
        private System.Windows.Forms.CheckBox LinecheckBox;
    }
}