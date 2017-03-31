namespace ECOLOGViewerver2
{
    partial class SemanticLinkBrowser
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
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.CarcomboBox = new System.Windows.Forms.ComboBox();
            this.CarIDlabel = new System.Windows.Forms.Label();
            this.d = new System.Windows.Forms.GroupBox();
            this.DirectioncomboBox = new System.Windows.Forms.ComboBox();
            this.Directionlabel = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.DrivercomboBox = new System.Windows.Forms.ComboBox();
            this.DriverIDlabel = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Semanticslabel = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.SemanticLinkIDlabel = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ECOLOGTable_textBox = new System.Windows.Forms.TextBox();
            this.LeadSpy_checkBox = new System.Windows.Forms.CheckBox();
            this.TORQUEData_checkBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.AxisYcomboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MaxofXtextBox = new System.Windows.Forms.TextBox();
            this.AxisXcomboBox = new System.Windows.Forms.ComboBox();
            this.QueryEdit_checkBox = new System.Windows.Forms.CheckBox();
            this.ChartcomboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MaxofYtextBox = new System.Windows.Forms.TextBox();
            this.Legend_checkBox = new System.Windows.Forms.CheckBox();
            this.Refreshbutton = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.d.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(650, 3);
            this.webBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScrollBarsEnabled = false;
            this.webBrowser.Size = new System.Drawing.Size(300, 300);
            this.webBrowser.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox9);
            this.groupBox3.Controls.Add(this.d);
            this.groupBox3.Controls.Add(this.groupBox8);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(623, 77);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Property";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.CarcomboBox);
            this.groupBox9.Controls.Add(this.CarIDlabel);
            this.groupBox9.Location = new System.Drawing.Point(414, 18);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(104, 45);
            this.groupBox9.TabIndex = 11;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Car";
            // 
            // CarcomboBox
            // 
            this.CarcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CarcomboBox.FormattingEnabled = true;
            this.CarcomboBox.Location = new System.Drawing.Point(2, 17);
            this.CarcomboBox.Name = "CarcomboBox";
            this.CarcomboBox.Size = new System.Drawing.Size(96, 20);
            this.CarcomboBox.TabIndex = 140;
            // 
            // CarIDlabel
            // 
            this.CarIDlabel.AutoSize = true;
            this.CarIDlabel.Location = new System.Drawing.Point(15, 20);
            this.CarIDlabel.Name = "CarIDlabel";
            this.CarIDlabel.Size = new System.Drawing.Size(14, 12);
            this.CarIDlabel.TabIndex = 10;
            this.CarIDlabel.Text = "id";
            // 
            // d
            // 
            this.d.Controls.Add(this.DirectioncomboBox);
            this.d.Controls.Add(this.Directionlabel);
            this.d.Location = new System.Drawing.Point(518, 18);
            this.d.Name = "d";
            this.d.Size = new System.Drawing.Size(99, 45);
            this.d.TabIndex = 11;
            this.d.TabStop = false;
            this.d.Text = "Direction";
            // 
            // DirectioncomboBox
            // 
            this.DirectioncomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DirectioncomboBox.FormattingEnabled = true;
            this.DirectioncomboBox.Items.AddRange(new object[] {
            "All",
            "outward",
            "homeward"});
            this.DirectioncomboBox.Location = new System.Drawing.Point(7, 17);
            this.DirectioncomboBox.Name = "DirectioncomboBox";
            this.DirectioncomboBox.Size = new System.Drawing.Size(87, 20);
            this.DirectioncomboBox.TabIndex = 143;
            // 
            // Directionlabel
            // 
            this.Directionlabel.AutoSize = true;
            this.Directionlabel.Location = new System.Drawing.Point(15, 20);
            this.Directionlabel.Name = "Directionlabel";
            this.Directionlabel.Size = new System.Drawing.Size(49, 12);
            this.Directionlabel.TabIndex = 10;
            this.Directionlabel.Text = "direction";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.DrivercomboBox);
            this.groupBox8.Controls.Add(this.DriverIDlabel);
            this.groupBox8.Location = new System.Drawing.Point(282, 18);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(128, 45);
            this.groupBox8.TabIndex = 10;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Driver";
            // 
            // DrivercomboBox
            // 
            this.DrivercomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DrivercomboBox.FormattingEnabled = true;
            this.DrivercomboBox.Location = new System.Drawing.Point(6, 17);
            this.DrivercomboBox.Name = "DrivercomboBox";
            this.DrivercomboBox.Size = new System.Drawing.Size(117, 20);
            this.DrivercomboBox.TabIndex = 139;
            // 
            // DriverIDlabel
            // 
            this.DriverIDlabel.AutoSize = true;
            this.DriverIDlabel.Location = new System.Drawing.Point(50, 20);
            this.DriverIDlabel.Name = "DriverIDlabel";
            this.DriverIDlabel.Size = new System.Drawing.Size(14, 12);
            this.DriverIDlabel.TabIndex = 10;
            this.DriverIDlabel.Text = "id";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Semanticslabel);
            this.groupBox5.Location = new System.Drawing.Point(112, 18);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(164, 45);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Semantics";
            // 
            // Semanticslabel
            // 
            this.Semanticslabel.Location = new System.Drawing.Point(17, 15);
            this.Semanticslabel.Name = "Semanticslabel";
            this.Semanticslabel.Size = new System.Drawing.Size(138, 24);
            this.Semanticslabel.TabIndex = 11;
            this.Semanticslabel.Text = "semantics\r\nsemantics";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.SemanticLinkIDlabel);
            this.groupBox4.Location = new System.Drawing.Point(10, 18);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(98, 45);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SemanticLinkID";
            // 
            // SemanticLinkIDlabel
            // 
            this.SemanticLinkIDlabel.AutoSize = true;
            this.SemanticLinkIDlabel.Location = new System.Drawing.Point(15, 20);
            this.SemanticLinkIDlabel.Name = "SemanticLinkIDlabel";
            this.SemanticLinkIDlabel.Size = new System.Drawing.Size(14, 12);
            this.SemanticLinkIDlabel.TabIndex = 10;
            this.SemanticLinkIDlabel.Text = "id";
            // 
            // chart
            // 
            this.chart.Location = new System.Drawing.Point(12, 95);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(623, 535);
            this.chart.TabIndex = 8;
            this.chart.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ECOLOGTable_textBox);
            this.groupBox1.Controls.Add(this.LeadSpy_checkBox);
            this.groupBox1.Controls.Add(this.TORQUEData_checkBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.AxisYcomboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.MaxofXtextBox);
            this.groupBox1.Controls.Add(this.AxisXcomboBox);
            this.groupBox1.Controls.Add(this.QueryEdit_checkBox);
            this.groupBox1.Controls.Add(this.ChartcomboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.MaxofYtextBox);
            this.groupBox1.Controls.Add(this.Legend_checkBox);
            this.groupBox1.Controls.Add(this.Refreshbutton);
            this.groupBox1.Location = new System.Drawing.Point(650, 301);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 329);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ChartController";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 248);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 12);
            this.label6.TabIndex = 117;
            this.label6.Text = "Lookup Table";
            // 
            // ECOLOGTable_textBox
            // 
            this.ECOLOGTable_textBox.Location = new System.Drawing.Point(92, 245);
            this.ECOLOGTable_textBox.Name = "ECOLOGTable_textBox";
            this.ECOLOGTable_textBox.Size = new System.Drawing.Size(199, 19);
            this.ECOLOGTable_textBox.TabIndex = 116;
            this.ECOLOGTable_textBox.Text = "ECOLOG_ALTITUDE_FROM_LINKS";
            // 
            // LeadSpy_checkBox
            // 
            this.LeadSpy_checkBox.AutoSize = true;
            this.LeadSpy_checkBox.Location = new System.Drawing.Point(128, 201);
            this.LeadSpy_checkBox.Name = "LeadSpy_checkBox";
            this.LeadSpy_checkBox.Size = new System.Drawing.Size(163, 16);
            this.LeadSpy_checkBox.TabIndex = 115;
            this.LeadSpy_checkBox.Text = "ECOLOG・CANの相関を見る";
            this.LeadSpy_checkBox.UseVisualStyleBackColor = true;
            this.LeadSpy_checkBox.Visible = false;
            // 
            // TORQUEData_checkBox
            // 
            this.TORQUEData_checkBox.AutoSize = true;
            this.TORQUEData_checkBox.Location = new System.Drawing.Point(17, 223);
            this.TORQUEData_checkBox.Name = "TORQUEData_checkBox";
            this.TORQUEData_checkBox.Size = new System.Drawing.Size(150, 16);
            this.TORQUEData_checkBox.TabIndex = 114;
            this.TORQUEData_checkBox.Text = "Data with TORQUE Only";
            this.TORQUEData_checkBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 12);
            this.label5.TabIndex = 113;
            this.label5.Text = "AxisY : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 12);
            this.label4.TabIndex = 112;
            this.label4.Text = "AxisX : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "Chart : ";
            // 
            // AxisYcomboBox
            // 
            this.AxisYcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AxisYcomboBox.FormattingEnabled = true;
            this.AxisYcomboBox.Location = new System.Drawing.Point(67, 105);
            this.AxisYcomboBox.Name = "AxisYcomboBox";
            this.AxisYcomboBox.Size = new System.Drawing.Size(224, 20);
            this.AxisYcomboBox.TabIndex = 112;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "Max of X : ";
            // 
            // MaxofXtextBox
            // 
            this.MaxofXtextBox.Location = new System.Drawing.Point(79, 148);
            this.MaxofXtextBox.Name = "MaxofXtextBox";
            this.MaxofXtextBox.Size = new System.Drawing.Size(27, 19);
            this.MaxofXtextBox.TabIndex = 11;
            // 
            // AxisXcomboBox
            // 
            this.AxisXcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AxisXcomboBox.FormattingEnabled = true;
            this.AxisXcomboBox.Location = new System.Drawing.Point(67, 70);
            this.AxisXcomboBox.Name = "AxisXcomboBox";
            this.AxisXcomboBox.Size = new System.Drawing.Size(224, 20);
            this.AxisXcomboBox.TabIndex = 111;
            // 
            // QueryEdit_checkBox
            // 
            this.QueryEdit_checkBox.AutoSize = true;
            this.QueryEdit_checkBox.Location = new System.Drawing.Point(17, 201);
            this.QueryEdit_checkBox.Name = "QueryEdit_checkBox";
            this.QueryEdit_checkBox.Size = new System.Drawing.Size(78, 16);
            this.QueryEdit_checkBox.TabIndex = 10;
            this.QueryEdit_checkBox.Text = "Query Edit";
            this.QueryEdit_checkBox.UseVisualStyleBackColor = true;
            // 
            // ChartcomboBox
            // 
            this.ChartcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChartcomboBox.FormattingEnabled = true;
            this.ChartcomboBox.Location = new System.Drawing.Point(67, 31);
            this.ChartcomboBox.Name = "ChartcomboBox";
            this.ChartcomboBox.Size = new System.Drawing.Size(224, 20);
            this.ChartcomboBox.TabIndex = 110;
            this.ChartcomboBox.SelectedIndexChanged += new System.EventHandler(this.ChartcomboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "Max of Y : ";
            // 
            // MaxofYtextBox
            // 
            this.MaxofYtextBox.Location = new System.Drawing.Point(205, 146);
            this.MaxofYtextBox.Name = "MaxofYtextBox";
            this.MaxofYtextBox.Size = new System.Drawing.Size(26, 19);
            this.MaxofYtextBox.TabIndex = 8;
            // 
            // Legend_checkBox
            // 
            this.Legend_checkBox.AutoSize = true;
            this.Legend_checkBox.Checked = true;
            this.Legend_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Legend_checkBox.Location = new System.Drawing.Point(17, 179);
            this.Legend_checkBox.Name = "Legend_checkBox";
            this.Legend_checkBox.Size = new System.Drawing.Size(102, 16);
            this.Legend_checkBox.TabIndex = 7;
            this.Legend_checkBox.Text = "Display Legend";
            this.Legend_checkBox.UseVisualStyleBackColor = true;
            // 
            // Refreshbutton
            // 
            this.Refreshbutton.Font = new System.Drawing.Font("MS UI Gothic", 16F);
            this.Refreshbutton.Location = new System.Drawing.Point(6, 272);
            this.Refreshbutton.Name = "Refreshbutton";
            this.Refreshbutton.Size = new System.Drawing.Size(285, 51);
            this.Refreshbutton.TabIndex = 0;
            this.Refreshbutton.Text = "Refresh";
            this.Refreshbutton.UseVisualStyleBackColor = true;
            this.Refreshbutton.Click += new System.EventHandler(this.Refreshbutton_Click);
            // 
            // SemanticLinkBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 642);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.webBrowser);
            this.Name = "SemanticLinkBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SemanticLink Browser";
            this.groupBox3.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.d.ResumeLayout(false);
            this.d.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label Semanticslabel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label SemanticLinkIDlabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Refreshbutton;
        private System.Windows.Forms.CheckBox Legend_checkBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MaxofYtextBox;
        private System.Windows.Forms.GroupBox d;
        private System.Windows.Forms.Label Directionlabel;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label DriverIDlabel;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label CarIDlabel;
        private System.Windows.Forms.CheckBox QueryEdit_checkBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MaxofXtextBox;
        private System.Windows.Forms.ComboBox ChartcomboBox;
        private System.Windows.Forms.ComboBox AxisXcomboBox;
        private System.Windows.Forms.ComboBox AxisYcomboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox TORQUEData_checkBox;
        private System.Windows.Forms.CheckBox LeadSpy_checkBox;
        private System.Windows.Forms.ComboBox DirectioncomboBox;
        private System.Windows.Forms.ComboBox CarcomboBox;
        private System.Windows.Forms.ComboBox DrivercomboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ECOLOGTable_textBox;
    }
}