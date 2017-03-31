namespace ECOLOGViewerver2
{
    partial class AnnotationDialog
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
            this.EventListdataGridView = new System.Windows.Forms.DataGridView();
            this.Displaybutton = new System.Windows.Forms.Button();
            this.Deletebutton = new System.Windows.Forms.Button();
            this.Cancelbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.EventListdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // EventListdataGridView
            // 
            this.EventListdataGridView.AllowUserToAddRows = false;
            this.EventListdataGridView.AllowUserToDeleteRows = false;
            this.EventListdataGridView.AllowUserToResizeColumns = false;
            this.EventListdataGridView.AllowUserToResizeRows = false;
            this.EventListdataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.EventListdataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.EventListdataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.EventListdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EventListdataGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.EventListdataGridView.Location = new System.Drawing.Point(12, 12);
            this.EventListdataGridView.MultiSelect = false;
            this.EventListdataGridView.Name = "EventListdataGridView";
            this.EventListdataGridView.ReadOnly = true;
            this.EventListdataGridView.RowHeadersVisible = false;
            this.EventListdataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.EventListdataGridView.RowTemplate.Height = 21;
            this.EventListdataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.EventListdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.EventListdataGridView.Size = new System.Drawing.Size(583, 439);
            this.EventListdataGridView.TabIndex = 61;
            // 
            // Displaybutton
            // 
            this.Displaybutton.Location = new System.Drawing.Point(21, 468);
            this.Displaybutton.Name = "Displaybutton";
            this.Displaybutton.Size = new System.Drawing.Size(198, 31);
            this.Displaybutton.TabIndex = 62;
            this.Displaybutton.Text = "Display";
            this.Displaybutton.UseVisualStyleBackColor = true;
            this.Displaybutton.Click += new System.EventHandler(this.Displaybutton_Click);
            // 
            // Deletebutton
            // 
            this.Deletebutton.Location = new System.Drawing.Point(420, 468);
            this.Deletebutton.Name = "Deletebutton";
            this.Deletebutton.Size = new System.Drawing.Size(175, 31);
            this.Deletebutton.TabIndex = 63;
            this.Deletebutton.Text = "Delete";
            this.Deletebutton.UseVisualStyleBackColor = true;
            this.Deletebutton.Click += new System.EventHandler(this.Deletebutton_Click);
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.Location = new System.Drawing.Point(225, 468);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(114, 31);
            this.Cancelbutton.TabIndex = 64;
            this.Cancelbutton.Text = "Cancel";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // AnnotationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 519);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.Displaybutton);
            this.Controls.Add(this.EventListdataGridView);
            this.Controls.Add(this.Deletebutton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AnnotationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AnnotationDialog";
            ((System.ComponentModel.ISupportInitialize)(this.EventListdataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView EventListdataGridView;
        private System.Windows.Forms.Button Displaybutton;
        private System.Windows.Forms.Button Deletebutton;
        private System.Windows.Forms.Button Cancelbutton;
    }
}