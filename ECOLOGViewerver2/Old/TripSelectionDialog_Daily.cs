using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECOLOGViewerver2
{
    partial class TripSelectionDialog_Daily
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
            this.TripsExtra_checkBox = new System.Windows.Forms.CheckBox();
            this.DirectioncomboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CarcomboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Displaybutton = new System.Windows.Forms.Button();
            this.TripDataGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.SensorcomboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DrivercomboBox = new System.Windows.Forms.ComboBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TripDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.Location = new System.Drawing.Point(503, 416);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(94, 34);
            this.Cancelbutton.TabIndex = 53;
            this.Cancelbutton.Text = "Cancel";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.SensorcomboBox);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.DrivercomboBox);
            this.groupBox3.Controls.Add(this.TripsExtra_checkBox);
            this.groupBox3.Controls.Add(this.DirectioncomboBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.CarcomboBox);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(803, 59);
            this.groupBox3.TabIndex = 62;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Trip Search";
            // 
            // TripsExtra_checkBox
            // 
            this.TripsExtra_checkBox.AutoSize = true;
            this.TripsExtra_checkBox.Location = new System.Drawing.Point(498, 32);
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
            this.DirectioncomboBox.Location = new System.Drawing.Point(411, 30);
            this.DirectioncomboBox.Name = "DirectioncomboBox";
            this.DirectioncomboBox.Size = new System.Drawing.Size(81, 20);
            this.DirectioncomboBox.TabIndex = 113;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(409, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 114;
            this.label3.Text = "DIRECTION";
            // 
            // CarcomboBox
            // 
            this.CarcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CarcomboBox.FormattingEnabled = true;
            this.CarcomboBox.Items.AddRange(new object[] {
            "All"});
            this.CarcomboBox.Location = new System.Drawing.Point(125, 30);
            this.CarcomboBox.Name = "CarcomboBox";
            this.CarcomboBox.Size = new System.Drawing.Size(92, 20);
            this.CarcomboBox.TabIndex = 110;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(131, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 112;
            this.label11.Text = "CAR";
            // 
            // Displaybutton
            // 
            this.Displaybutton.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.Displaybutton.Location = new System.Drawing.Point(145, 413);
            this.Displaybutton.Name = "Displaybutton";
            this.Displaybutton.Size = new System.Drawing.Size(99, 37);
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
            this.TripDataGrid.Location = new System.Drawing.Point(10, 75);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 12);
            this.label1.TabIndex = 122;
            this.label1.Text = "SENSOR";
            // 
            // SensorcomboBox
            // 
            this.SensorcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SensorcomboBox.FormattingEnabled = true;
            this.SensorcomboBox.Items.AddRange(new object[] {
            "All"});
            this.SensorcomboBox.Location = new System.Drawing.Point(223, 30);
            this.SensorcomboBox.Name = "SensorcomboBox";
            this.SensorcomboBox.Size = new System.Drawing.Size(182, 20);
            this.SensorcomboBox.TabIndex = 121;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 120;
            this.label5.Text = "DRIVER";
            // 
            // DrivercomboBox
            // 
            this.DrivercomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DrivercomboBox.FormattingEnabled = true;
            this.DrivercomboBox.Items.AddRange(new object[] {
            "All"});
            this.DrivercomboBox.Location = new System.Drawing.Point(16, 30);
            this.DrivercomboBox.Name = "DrivercomboBox";
            this.DrivercomboBox.Size = new System.Drawing.Size(103, 20);
            this.DrivercomboBox.TabIndex = 119;
            // 
            // TripSelectionDialog_Daily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 466);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.Displaybutton);
            this.Controls.Add(this.TripDataGrid);
            this.Controls.Add(this.Cancelbutton);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TripSelectionDialog_Daily";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trip Selection";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TripSelectionDialog_KeyDown);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TripDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Cancelbutton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox DirectioncomboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CarcomboBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button Displaybutton;
        private System.Windows.Forms.DataGridView TripDataGrid;
        private System.Windows.Forms.CheckBox TripsExtra_checkBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SensorcomboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox DrivercomboBox;
    }
}
