namespace ECOLOGViewerver2
{
    partial class QueryView
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
            this.QuerytextBox = new System.Windows.Forms.TextBox();
            this.OKbutton = new System.Windows.Forms.Button();
            this.CANCELbutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // QuerytextBox
            // 
            this.QuerytextBox.Location = new System.Drawing.Point(12, 24);
            this.QuerytextBox.Multiline = true;
            this.QuerytextBox.Name = "QuerytextBox";
            this.QuerytextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.QuerytextBox.Size = new System.Drawing.Size(869, 565);
            this.QuerytextBox.TabIndex = 140;
            // 
            // OKbutton
            // 
            this.OKbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKbutton.Location = new System.Drawing.Point(169, 596);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(140, 31);
            this.OKbutton.TabIndex = 141;
            this.OKbutton.Text = "OK";
            this.OKbutton.UseVisualStyleBackColor = true;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // CANCELbutton
            // 
            this.CANCELbutton.Location = new System.Drawing.Point(556, 596);
            this.CANCELbutton.Name = "CANCELbutton";
            this.CANCELbutton.Size = new System.Drawing.Size(140, 31);
            this.CANCELbutton.TabIndex = 142;
            this.CANCELbutton.Text = "CANCEL";
            this.CANCELbutton.UseVisualStyleBackColor = true;
            this.CANCELbutton.Click += new System.EventHandler(this.CANCELbutton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label1.Location = new System.Drawing.Point(210, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(403, 16);
            this.label1.TabIndex = 143;
            this.label1.Text = "When you don\'t change the query, please push \'OK\' button.";
            // 
            // QueryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 639);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CANCELbutton);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.QuerytextBox);
            this.Name = "QueryView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QueryView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox QuerytextBox;
        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.Button CANCELbutton;
        private System.Windows.Forms.Label label1;

    }
}