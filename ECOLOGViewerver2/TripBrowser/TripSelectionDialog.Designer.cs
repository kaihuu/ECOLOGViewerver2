namespace ECOLOGViewerver2
{
    partial class TripSelectionDialog
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
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.demoCheckBox = new System.Windows.Forms.CheckBox();
            this.LeafSpy_checkBox = new System.Windows.Forms.CheckBox();
            this.ConsumedEnergycheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GasolinecheckBox = new System.Windows.Forms.CheckBox();
            this.TripsExtra_checkBox = new System.Windows.Forms.CheckBox();
            this.DirectioncomboBox = new System.Windows.Forms.ComboBox();
            this.SensorcomboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.DrivercomboBox = new System.Windows.Forms.ComboBox();
            this.CarcomboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ECOLOGTable_comboBox = new System.Windows.Forms.ComboBox();
            this.LeafSpycheckBox = new System.Windows.Forms.CheckBox();
            this.EditQuerycheckBox = new System.Windows.Forms.CheckBox();
            this.Marker_checkBox = new System.Windows.Forms.CheckBox();
            this.ECOLOGTable_textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.WorstQuerybutton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.AverageQuerybutton = new System.Windows.Forms.Button();
            this.pdtextBox = new System.Windows.Forms.TextBox();
            this.InfocomboBox = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.AggregationcomboBox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.DatacomboBox = new System.Windows.Forms.ComboBox();
            this.useFixed_checkBox = new System.Windows.Forms.CheckBox();
            this.Displaybutton = new System.Windows.Forms.Button();
            this.TripDataGrid = new System.Windows.Forms.DataGridView();
            this.useNexus7CameraCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TripDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.Location = new System.Drawing.Point(469, 511);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(200, 34);
            this.Cancelbutton.TabIndex = 53;
            this.Cancelbutton.Text = "Cancel";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.demoCheckBox);
            this.groupBox3.Controls.Add(this.LeafSpy_checkBox);
            this.groupBox3.Controls.Add(this.ConsumedEnergycheckBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.GasolinecheckBox);
            this.groupBox3.Controls.Add(this.TripsExtra_checkBox);
            this.groupBox3.Controls.Add(this.DirectioncomboBox);
            this.groupBox3.Controls.Add(this.SensorcomboBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.DrivercomboBox);
            this.groupBox3.Controls.Add(this.CarcomboBox);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(808, 59);
            this.groupBox3.TabIndex = 62;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Trip Search";
            // 
            // demoCheckBox
            // 
            this.demoCheckBox.AutoSize = true;
            this.demoCheckBox.Location = new System.Drawing.Point(737, 21);
            this.demoCheckBox.Name = "demoCheckBox";
            this.demoCheckBox.Size = new System.Drawing.Size(53, 16);
            this.demoCheckBox.TabIndex = 122;
            this.demoCheckBox.Text = "Demo";
            this.demoCheckBox.UseVisualStyleBackColor = true;
            this.demoCheckBox.CheckedChanged += new System.EventHandler(this.demoCheckBox_CheckedChanged);
            // 
            // LeafSpy_checkBox
            // 
            this.LeafSpy_checkBox.AutoSize = true;
            this.LeafSpy_checkBox.Location = new System.Drawing.Point(526, 41);
            this.LeafSpy_checkBox.Name = "LeafSpy_checkBox";
            this.LeafSpy_checkBox.Size = new System.Drawing.Size(105, 16);
            this.LeafSpy_checkBox.TabIndex = 119;
            this.LeafSpy_checkBox.Text = "LEAF SPY Data";
            this.LeafSpy_checkBox.UseVisualStyleBackColor = true;
            this.LeafSpy_checkBox.CheckedChanged += new System.EventHandler(this.LeafSpy_checkBox_CheckedChanged);
            // 
            // ConsumedEnergycheckBox
            // 
            this.ConsumedEnergycheckBox.AutoSize = true;
            this.ConsumedEnergycheckBox.Location = new System.Drawing.Point(634, 41);
            this.ConsumedEnergycheckBox.Name = "ConsumedEnergycheckBox";
            this.ConsumedEnergycheckBox.Size = new System.Drawing.Size(158, 16);
            this.ConsumedEnergycheckBox.TabIndex = 117;
            this.ConsumedEnergycheckBox.Text = "Correct Consumed Energy";
            this.ConsumedEnergycheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(209, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 12);
            this.label1.TabIndex = 118;
            this.label1.Text = "SENSOR";
            // 
            // GasolinecheckBox
            // 
            this.GasolinecheckBox.AutoSize = true;
            this.GasolinecheckBox.Location = new System.Drawing.Point(634, 21);
            this.GasolinecheckBox.Name = "GasolinecheckBox";
            this.GasolinecheckBox.Size = new System.Drawing.Size(87, 16);
            this.GasolinecheckBox.TabIndex = 116;
            this.GasolinecheckBox.Text = "Torque Data";
            this.GasolinecheckBox.UseVisualStyleBackColor = true;
            this.GasolinecheckBox.CheckedChanged += new System.EventHandler(this.GasolinecheckBox_CheckedChanged);
            // 
            // TripsExtra_checkBox
            // 
            this.TripsExtra_checkBox.AutoSize = true;
            this.TripsExtra_checkBox.Location = new System.Drawing.Point(526, 21);
            this.TripsExtra_checkBox.Name = "TripsExtra_checkBox";
            this.TripsExtra_checkBox.Size = new System.Drawing.Size(97, 16);
            this.TripsExtra_checkBox.TabIndex = 115;
            this.TripsExtra_checkBox.Text = "TRIPS_EXTRA";
            this.TripsExtra_checkBox.UseVisualStyleBackColor = true;
            this.TripsExtra_checkBox.CheckedChanged += new System.EventHandler(this.TripsExtra_checkBox_CheckedChanged);
            // 
            // DirectioncomboBox
            // 
            this.DirectioncomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DirectioncomboBox.FormattingEnabled = true;
            this.DirectioncomboBox.Items.AddRange(new object[] {
            "All",
            "outward",
            "homeward"});
            this.DirectioncomboBox.Location = new System.Drawing.Point(387, 30);
            this.DirectioncomboBox.Name = "DirectioncomboBox";
            this.DirectioncomboBox.Size = new System.Drawing.Size(130, 20);
            this.DirectioncomboBox.TabIndex = 113;
            // 
            // SensorcomboBox
            // 
            this.SensorcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SensorcomboBox.FormattingEnabled = true;
            this.SensorcomboBox.Items.AddRange(new object[] {
            "All"});
            this.SensorcomboBox.Location = new System.Drawing.Point(200, 30);
            this.SensorcomboBox.Name = "SensorcomboBox";
            this.SensorcomboBox.Size = new System.Drawing.Size(182, 20);
            this.SensorcomboBox.TabIndex = 117;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(389, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 114;
            this.label3.Text = "DIRECTION";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 111;
            this.label5.Text = "DRIVER";
            // 
            // DrivercomboBox
            // 
            this.DrivercomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DrivercomboBox.FormattingEnabled = true;
            this.DrivercomboBox.Items.AddRange(new object[] {
            "All"});
            this.DrivercomboBox.Location = new System.Drawing.Point(21, 30);
            this.DrivercomboBox.Name = "DrivercomboBox";
            this.DrivercomboBox.Size = new System.Drawing.Size(103, 20);
            this.DrivercomboBox.TabIndex = 109;
            // 
            // CarcomboBox
            // 
            this.CarcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CarcomboBox.FormattingEnabled = true;
            this.CarcomboBox.Items.AddRange(new object[] {
            "All"});
            this.CarcomboBox.Location = new System.Drawing.Point(130, 30);
            this.CarcomboBox.Name = "CarcomboBox";
            this.CarcomboBox.Size = new System.Drawing.Size(64, 20);
            this.CarcomboBox.TabIndex = 110;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(130, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 112;
            this.label11.Text = "CAR";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ECOLOGTable_comboBox);
            this.groupBox4.Controls.Add(this.LeafSpycheckBox);
            this.groupBox4.Controls.Add(this.EditQuerycheckBox);
            this.groupBox4.Controls.Add(this.Marker_checkBox);
            this.groupBox4.Controls.Add(this.ECOLOGTable_textBox);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.WorstQuerybutton);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.AverageQuerybutton);
            this.groupBox4.Controls.Add(this.pdtextBox);
            this.groupBox4.Controls.Add(this.InfocomboBox);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.AggregationcomboBox);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.DatacomboBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 407);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(808, 97);
            this.groupBox4.TabIndex = 61;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Settings";
            // 
            // ECOLOGTable_comboBox
            // 
            this.ECOLOGTable_comboBox.FormattingEnabled = true;
            this.ECOLOGTable_comboBox.Items.AddRange(new object[] {
            "ECOLOG_LINKS_LOOKUP",
            "ECOLOG_MM_LINKS_LOOKUP",
            "[ECOLOG_SPEEDLPF0.05_MM_LINKS_LOOKUP]"});
            this.ECOLOGTable_comboBox.Location = new System.Drawing.Point(563, 59);
            this.ECOLOGTable_comboBox.Name = "ECOLOGTable_comboBox";
            this.ECOLOGTable_comboBox.Size = new System.Drawing.Size(147, 20);
            this.ECOLOGTable_comboBox.TabIndex = 63;
            // 
            // LeafSpycheckBox
            // 
            this.LeafSpycheckBox.AutoSize = true;
            this.LeafSpycheckBox.Location = new System.Drawing.Point(656, 18);
            this.LeafSpycheckBox.Name = "LeafSpycheckBox";
            this.LeafSpycheckBox.Size = new System.Drawing.Size(143, 16);
            this.LeafSpycheckBox.TabIndex = 121;
            this.LeafSpycheckBox.Text = "LEAF Spyのデータを見る";
            this.LeafSpycheckBox.UseVisualStyleBackColor = true;
            // 
            // EditQuerycheckBox
            // 
            this.EditQuerycheckBox.AutoSize = true;
            this.EditQuerycheckBox.Location = new System.Drawing.Point(656, 40);
            this.EditQuerycheckBox.Name = "EditQuerycheckBox";
            this.EditQuerycheckBox.Size = new System.Drawing.Size(78, 16);
            this.EditQuerycheckBox.TabIndex = 63;
            this.EditQuerycheckBox.Text = "Edit Query";
            this.EditQuerycheckBox.UseVisualStyleBackColor = true;
            // 
            // Marker_checkBox
            // 
            this.Marker_checkBox.AutoSize = true;
            this.Marker_checkBox.Location = new System.Drawing.Point(133, 62);
            this.Marker_checkBox.Name = "Marker_checkBox";
            this.Marker_checkBox.Size = new System.Drawing.Size(80, 16);
            this.Marker_checkBox.TabIndex = 60;
            this.Marker_checkBox.Text = "試験的なの";
            this.Marker_checkBox.UseVisualStyleBackColor = true;
            // 
            // ECOLOGTable_textBox
            // 
            this.ECOLOGTable_textBox.Location = new System.Drawing.Point(563, 59);
            this.ECOLOGTable_textBox.Name = "ECOLOGTable_textBox";
            this.ECOLOGTable_textBox.Size = new System.Drawing.Size(147, 19);
            this.ECOLOGTable_textBox.TabIndex = 59;
            this.ECOLOGTable_textBox.Text = "ECOLOG";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(524, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 12);
            this.label4.TabIndex = 58;
            this.label4.Text = "Table";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 12);
            this.label10.TabIndex = 53;
            this.label10.Text = "Data on Map";
            // 
            // WorstQuerybutton
            // 
            this.WorstQuerybutton.Enabled = false;
            this.WorstQuerybutton.Location = new System.Drawing.Point(387, 53);
            this.WorstQuerybutton.Name = "WorstQuerybutton";
            this.WorstQuerybutton.Size = new System.Drawing.Size(136, 30);
            this.WorstQuerybutton.TabIndex = 51;
            this.WorstQuerybutton.Text = "Edit Worst Query";
            this.WorstQuerybutton.UseVisualStyleBackColor = true;
            this.WorstQuerybutton.Click += new System.EventHandler(this.WorstQuerybutton_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 63);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 12);
            this.label14.TabIndex = 56;
            this.label14.Text = "Marker Interval";
            // 
            // AverageQuerybutton
            // 
            this.AverageQuerybutton.Enabled = false;
            this.AverageQuerybutton.Location = new System.Drawing.Point(243, 53);
            this.AverageQuerybutton.Name = "AverageQuerybutton";
            this.AverageQuerybutton.Size = new System.Drawing.Size(136, 30);
            this.AverageQuerybutton.TabIndex = 50;
            this.AverageQuerybutton.Text = "Edit Average Query";
            this.AverageQuerybutton.UseVisualStyleBackColor = true;
            this.AverageQuerybutton.Click += new System.EventHandler(this.AverageQuerybutton_Click);
            // 
            // pdtextBox
            // 
            this.pdtextBox.Location = new System.Drawing.Point(94, 59);
            this.pdtextBox.Name = "pdtextBox";
            this.pdtextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pdtextBox.Size = new System.Drawing.Size(21, 19);
            this.pdtextBox.TabIndex = 52;
            this.pdtextBox.Text = "1";
            // 
            // InfocomboBox
            // 
            this.InfocomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InfocomboBox.FormattingEnabled = true;
            this.InfocomboBox.Items.AddRange(new object[] {
            "Trajectory",
            "Trajectory+Content",
            "Trajectory+Average",
            "Trajectory+WosrtLink"});
            this.InfocomboBox.Location = new System.Drawing.Point(78, 23);
            this.InfocomboBox.Name = "InfocomboBox";
            this.InfocomboBox.Size = new System.Drawing.Size(138, 20);
            this.InfocomboBox.TabIndex = 30;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(429, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 12);
            this.label13.TabIndex = 55;
            this.label13.Text = "Aggregation";
            // 
            // AggregationcomboBox
            // 
            this.AggregationcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AggregationcomboBox.FormattingEnabled = true;
            this.AggregationcomboBox.Items.AddRange(new object[] {
            "Link"});
            this.AggregationcomboBox.Location = new System.Drawing.Point(501, 23);
            this.AggregationcomboBox.Name = "AggregationcomboBox";
            this.AggregationcomboBox.Size = new System.Drawing.Size(138, 20);
            this.AggregationcomboBox.TabIndex = 31;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(236, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 12);
            this.label12.TabIndex = 54;
            this.label12.Text = "Content";
            // 
            // DatacomboBox
            // 
            this.DatacomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DatacomboBox.FormattingEnabled = true;
            this.DatacomboBox.Items.AddRange(new object[] {
            "ConsumedEnergy",
            "LostEnergy",
            "UsedFuel",
            "Speed",
            "LongitudinalAcc",
            "LateralAcc"});
            this.DatacomboBox.Location = new System.Drawing.Point(287, 22);
            this.DatacomboBox.Name = "DatacomboBox";
            this.DatacomboBox.Size = new System.Drawing.Size(117, 20);
            this.DatacomboBox.TabIndex = 32;
            // 
            // useFixed_checkBox
            // 
            this.useFixed_checkBox.AutoSize = true;
            this.useFixed_checkBox.Location = new System.Drawing.Point(682, 510);
            this.useFixed_checkBox.Name = "useFixed_checkBox";
            this.useFixed_checkBox.Size = new System.Drawing.Size(129, 16);
            this.useFixed_checkBox.TabIndex = 57;
            this.useFixed_checkBox.Text = "Use ECOLOG_FIXED";
            this.useFixed_checkBox.UseVisualStyleBackColor = true;
            this.useFixed_checkBox.Visible = false;
            this.useFixed_checkBox.CheckedChanged += new System.EventHandler(this.useFixed_checkBox_CheckedChanged);
            // 
            // Displaybutton
            // 
            this.Displaybutton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.Displaybutton.Location = new System.Drawing.Point(137, 510);
            this.Displaybutton.Name = "Displaybutton";
            this.Displaybutton.Size = new System.Drawing.Size(230, 37);
            this.Displaybutton.TabIndex = 60;
            this.Displaybutton.Text = "Display";
            this.Displaybutton.UseVisualStyleBackColor = true;
            this.Displaybutton.Click += new System.EventHandler(this.Display_button_Click);
            // 
            // TripDataGrid
            // 
            this.TripDataGrid.AllowUserToAddRows = false;
            this.TripDataGrid.AllowUserToDeleteRows = false;
            this.TripDataGrid.AllowUserToResizeColumns = false;
            this.TripDataGrid.AllowUserToResizeRows = false;
            this.TripDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TripDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.TripDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.TripDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TripDataGrid.Cursor = System.Windows.Forms.Cursors.Default;
            this.TripDataGrid.Location = new System.Drawing.Point(12, 75);
            this.TripDataGrid.MultiSelect = false;
            this.TripDataGrid.Name = "TripDataGrid";
            this.TripDataGrid.RowHeadersVisible = false;
            this.TripDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.TripDataGrid.RowTemplate.Height = 21;
            this.TripDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TripDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TripDataGrid.Size = new System.Drawing.Size(808, 324);
            this.TripDataGrid.TabIndex = 59;
            // 
            // useNexus7CameraCheckBox
            // 
            this.useNexus7CameraCheckBox.AutoSize = true;
            this.useNexus7CameraCheckBox.Location = new System.Drawing.Point(682, 533);
            this.useNexus7CameraCheckBox.Name = "useNexus7CameraCheckBox";
            this.useNexus7CameraCheckBox.Size = new System.Drawing.Size(129, 16);
            this.useNexus7CameraCheckBox.TabIndex = 63;
            this.useNexus7CameraCheckBox.Text = "Nexus7の画像を優先";
            this.useNexus7CameraCheckBox.UseVisualStyleBackColor = true;
            // 
            // TripSelectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 566);
            this.Controls.Add(this.useNexus7CameraCheckBox);
            this.Controls.Add(this.useFixed_checkBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.Displaybutton);
            this.Controls.Add(this.TripDataGrid);
            this.Controls.Add(this.Cancelbutton);
            this.KeyPreview = true;
            this.Name = "TripSelectionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trip Selection";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TripSelectionDialog_KeyDown);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TripDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Cancelbutton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox DirectioncomboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox DrivercomboBox;
        private System.Windows.Forms.ComboBox CarcomboBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button WorstQuerybutton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button AverageQuerybutton;
        private System.Windows.Forms.TextBox pdtextBox;
        private System.Windows.Forms.ComboBox InfocomboBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox AggregationcomboBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox DatacomboBox;
        private System.Windows.Forms.Button Displaybutton;
        private System.Windows.Forms.DataGridView TripDataGrid;
        private System.Windows.Forms.CheckBox useFixed_checkBox;
        private System.Windows.Forms.CheckBox TripsExtra_checkBox;
        private System.Windows.Forms.CheckBox EditQuerycheckBox;
        private System.Windows.Forms.CheckBox GasolinecheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SensorcomboBox;
        private System.Windows.Forms.CheckBox ConsumedEnergycheckBox;
        private System.Windows.Forms.TextBox ECOLOGTable_textBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox Marker_checkBox;
        private System.Windows.Forms.CheckBox LeafSpycheckBox;
        private System.Windows.Forms.CheckBox LeafSpy_checkBox;
        private System.Windows.Forms.ComboBox ECOLOGTable_comboBox;
        private System.Windows.Forms.CheckBox useNexus7CameraCheckBox;
        private System.Windows.Forms.CheckBox demoCheckBox;
    }
}