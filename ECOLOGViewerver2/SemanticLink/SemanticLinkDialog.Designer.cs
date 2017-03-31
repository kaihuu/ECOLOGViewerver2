namespace ECOLOGViewerver2
{
    partial class SemanticLinkDialog
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
            this.SemanticLinksdataGridView = new System.Windows.Forms.DataGridView();
            this.DirectioncomboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.driver_label = new System.Windows.Forms.Label();
            this.Displaybutton = new System.Windows.Forms.Button();
            this.car_label = new System.Windows.Forms.Label();
            this.DrivercomboBox = new System.Windows.Forms.ComboBox();
            this.CarcomboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.Editbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SemanticLinksdataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SemanticLinksdataGridView
            // 
            this.SemanticLinksdataGridView.AllowUserToAddRows = false;
            this.SemanticLinksdataGridView.AllowUserToDeleteRows = false;
            this.SemanticLinksdataGridView.AllowUserToResizeColumns = false;
            this.SemanticLinksdataGridView.AllowUserToResizeRows = false;
            this.SemanticLinksdataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SemanticLinksdataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.SemanticLinksdataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.SemanticLinksdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SemanticLinksdataGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.SemanticLinksdataGridView.Location = new System.Drawing.Point(9, 12);
            this.SemanticLinksdataGridView.MultiSelect = false;
            this.SemanticLinksdataGridView.Name = "SemanticLinksdataGridView";
            this.SemanticLinksdataGridView.ReadOnly = true;
            this.SemanticLinksdataGridView.RowHeadersVisible = false;
            this.SemanticLinksdataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.SemanticLinksdataGridView.RowTemplate.Height = 21;
            this.SemanticLinksdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SemanticLinksdataGridView.Size = new System.Drawing.Size(467, 489);
            this.SemanticLinksdataGridView.TabIndex = 2;
            // 
            // DirectioncomboBox
            // 
            this.DirectioncomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DirectioncomboBox.FormattingEnabled = true;
            this.DirectioncomboBox.Items.AddRange(new object[] {
            "All",
            "outward",
            "homeward"});
            this.DirectioncomboBox.Location = new System.Drawing.Point(268, 41);
            this.DirectioncomboBox.Name = "DirectioncomboBox";
            this.DirectioncomboBox.Size = new System.Drawing.Size(127, 20);
            this.DirectioncomboBox.TabIndex = 137;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(266, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 138;
            this.label3.Text = "DIRECTION";
            // 
            // driver_label
            // 
            this.driver_label.AutoSize = true;
            this.driver_label.Location = new System.Drawing.Point(16, 21);
            this.driver_label.Name = "driver_label";
            this.driver_label.Size = new System.Drawing.Size(54, 12);
            this.driver_label.TabIndex = 134;
            this.driver_label.Text = "DRIVERS";
            // 
            // Displaybutton
            // 
            this.Displaybutton.Location = new System.Drawing.Point(58, 519);
            this.Displaybutton.Name = "Displaybutton";
            this.Displaybutton.Size = new System.Drawing.Size(109, 26);
            this.Displaybutton.TabIndex = 136;
            this.Displaybutton.Text = "Display";
            this.Displaybutton.UseVisualStyleBackColor = true;
            this.Displaybutton.Click += new System.EventHandler(this.Displaybutton_Click);
            // 
            // car_label
            // 
            this.car_label.AutoSize = true;
            this.car_label.Location = new System.Drawing.Point(148, 21);
            this.car_label.Name = "car_label";
            this.car_label.Size = new System.Drawing.Size(29, 12);
            this.car_label.TabIndex = 135;
            this.car_label.Text = "CAR";
            // 
            // DrivercomboBox
            // 
            this.DrivercomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DrivercomboBox.FormattingEnabled = true;
            this.DrivercomboBox.Location = new System.Drawing.Point(12, 41);
            this.DrivercomboBox.Name = "DrivercomboBox";
            this.DrivercomboBox.Size = new System.Drawing.Size(117, 20);
            this.DrivercomboBox.TabIndex = 132;
            // 
            // CarcomboBox
            // 
            this.CarcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CarcomboBox.FormattingEnabled = true;
            this.CarcomboBox.Location = new System.Drawing.Point(146, 41);
            this.CarcomboBox.Name = "CarcomboBox";
            this.CarcomboBox.Size = new System.Drawing.Size(96, 20);
            this.CarcomboBox.TabIndex = 133;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DirectioncomboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.driver_label);
            this.groupBox1.Controls.Add(this.CarcomboBox);
            this.groupBox1.Controls.Add(this.car_label);
            this.groupBox1.Controls.Add(this.DrivercomboBox);
            this.groupBox1.Location = new System.Drawing.Point(482, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 76);
            this.groupBox1.TabIndex = 140;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selection";
            this.groupBox1.Visible = false;
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.Location = new System.Drawing.Point(199, 519);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(109, 26);
            this.Cancelbutton.TabIndex = 141;
            this.Cancelbutton.Text = "Cancel";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // Editbutton
            // 
            this.Editbutton.Location = new System.Drawing.Point(343, 519);
            this.Editbutton.Name = "Editbutton";
            this.Editbutton.Size = new System.Drawing.Size(109, 26);
            this.Editbutton.TabIndex = 142;
            this.Editbutton.Text = "Edit";
            this.Editbutton.UseVisualStyleBackColor = true;
            this.Editbutton.Click += new System.EventHandler(this.Editbutton_Click);
            // 
            // SemanticLinkDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 557);
            this.Controls.Add(this.Editbutton);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.Displaybutton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SemanticLinksdataGridView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SemanticLinkDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SemanticLinkDialog";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SemanticLinkDialog_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.SemanticLinksdataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView SemanticLinksdataGridView;
        private System.Windows.Forms.ComboBox DirectioncomboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label driver_label;
        private System.Windows.Forms.Button Displaybutton;
        private System.Windows.Forms.Label car_label;
        private System.Windows.Forms.ComboBox DrivercomboBox;
        private System.Windows.Forms.ComboBox CarcomboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Cancelbutton;
        private System.Windows.Forms.Button Editbutton;
    }
}