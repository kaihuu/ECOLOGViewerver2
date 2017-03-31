namespace ECOLOGViewerver2
{
    partial class WorstQueryDialog
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
            this.Okbutton = new System.Windows.Forms.Button();
            this.WorsttextBox = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.Location = new System.Drawing.Point(260, 420);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(127, 33);
            this.Cancelbutton.TabIndex = 48;
            this.Cancelbutton.Text = "CANCEL";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // Okbutton
            // 
            this.Okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Okbutton.Location = new System.Drawing.Point(67, 420);
            this.Okbutton.Name = "Okbutton";
            this.Okbutton.Size = new System.Drawing.Size(121, 33);
            this.Okbutton.TabIndex = 47;
            this.Okbutton.Text = "OK";
            this.Okbutton.UseVisualStyleBackColor = true;
            this.Okbutton.Click += new System.EventHandler(this.Okbutton_Click);
            // 
            // WorsttextBox
            // 
            this.WorsttextBox.Location = new System.Drawing.Point(14, 21);
            this.WorsttextBox.Multiline = true;
            this.WorsttextBox.Name = "WorsttextBox";
            this.WorsttextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.WorsttextBox.Size = new System.Drawing.Size(426, 332);
            this.WorsttextBox.TabIndex = 0;
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Location = new System.Drawing.Point(14, 359);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(426, 36);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Notice";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(398, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "※carID, driversensorID, startTime, endTime is replaced Data of SelectedTrip";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Controls.Add(this.WorsttextBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(460, 405);
            this.groupBox4.TabIndex = 46;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Worst Query";
            // 
            // WorstQueryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.Okbutton);
            this.Controls.Add(this.groupBox4);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorstQueryDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WorstQueryDialog";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WorstQueryDialog_KeyDown);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Cancelbutton;
        private System.Windows.Forms.Button Okbutton;
        private System.Windows.Forms.TextBox WorsttextBox;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
    }
}