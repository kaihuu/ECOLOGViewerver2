namespace ECOLOGViewerver2
{
    partial class AverageQueryDialog
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.AveragetextBox = new System.Windows.Forms.TextBox();
            this.Okbutton = new System.Windows.Forms.Button();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.AveragetextBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(460, 293);
            this.groupBox4.TabIndex = 43;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Average Query";
            // 
            // AveragetextBox
            // 
            this.AveragetextBox.Location = new System.Drawing.Point(14, 21);
            this.AveragetextBox.Multiline = true;
            this.AveragetextBox.Name = "AveragetextBox";
            this.AveragetextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.AveragetextBox.Size = new System.Drawing.Size(426, 259);
            this.AveragetextBox.TabIndex = 0;
            // 
            // Okbutton
            // 
            this.Okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Okbutton.Location = new System.Drawing.Point(58, 320);
            this.Okbutton.Name = "Okbutton";
            this.Okbutton.Size = new System.Drawing.Size(121, 33);
            this.Okbutton.TabIndex = 44;
            this.Okbutton.Text = "OK";
            this.Okbutton.UseVisualStyleBackColor = true;
            this.Okbutton.Click += new System.EventHandler(this.Okbutton_Click);
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.Location = new System.Drawing.Point(287, 320);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(127, 33);
            this.Cancelbutton.TabIndex = 45;
            this.Cancelbutton.Text = "CANCEL";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // AverageQueryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 372);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.Okbutton);
            this.Controls.Add(this.groupBox4);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AverageQueryDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AverageQueryDialog";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AverageQueryDialog_KeyDown);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox AveragetextBox;
        private System.Windows.Forms.Button Okbutton;
        private System.Windows.Forms.Button Cancelbutton;
    }
}