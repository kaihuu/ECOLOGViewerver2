namespace ECOLOGViewerver2
{
    partial class SemanticLinkEditor
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
            this.components = new System.ComponentModel.Container();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.linkidtextBox = new System.Windows.Forms.TextBox();
            this.deletebutton = new System.Windows.Forms.Button();
            this.ClickedcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addThisLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addThisNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.node1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.node2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Addbutton = new System.Windows.Forms.Button();
            this.LinktextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SearchLinkbutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.EndNodetextBox = new System.Windows.Forms.TextBox();
            this.StartNodetextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.Driverlabel = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Semanticslabel = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.SemanticLinkIDlabel = new System.Windows.Forms.Label();
            this.Reloadbutton = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.AddAllLinks_button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Node_textBox = new System.Windows.Forms.TextBox();
            this.searchLinksWithNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClickedcontextMenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(550, 550);
            this.webBrowser1.TabIndex = 1;
            // 
            // linkidtextBox
            // 
            this.linkidtextBox.Location = new System.Drawing.Point(18, 18);
            this.linkidtextBox.Name = "linkidtextBox";
            this.linkidtextBox.Size = new System.Drawing.Size(181, 19);
            this.linkidtextBox.TabIndex = 2;
            // 
            // deletebutton
            // 
            this.deletebutton.Location = new System.Drawing.Point(213, 14);
            this.deletebutton.Name = "deletebutton";
            this.deletebutton.Size = new System.Drawing.Size(61, 24);
            this.deletebutton.TabIndex = 1;
            this.deletebutton.Text = "Remove";
            this.deletebutton.UseVisualStyleBackColor = true;
            this.deletebutton.Click += new System.EventHandler(this.deletebutton_Click);
            // 
            // ClickedcontextMenuStrip
            // 
            this.ClickedcontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addThisLinkToolStripMenuItem,
            this.addThisNodeToolStripMenuItem});
            this.ClickedcontextMenuStrip.Name = "ClickedcontextMenuStrip";
            this.ClickedcontextMenuStrip.Size = new System.Drawing.Size(159, 70);
            // 
            // addThisLinkToolStripMenuItem
            // 
            this.addThisLinkToolStripMenuItem.Name = "addThisLinkToolStripMenuItem";
            this.addThisLinkToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.addThisLinkToolStripMenuItem.Text = "Add This Link";
            this.addThisLinkToolStripMenuItem.Click += new System.EventHandler(this.addThisLinkToolStripMenuItem_Click);
            // 
            // addThisNodeToolStripMenuItem
            // 
            this.addThisNodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.node1ToolStripMenuItem,
            this.node2ToolStripMenuItem,
            this.searchLinksWithNodeToolStripMenuItem});
            this.addThisNodeToolStripMenuItem.Name = "addThisNodeToolStripMenuItem";
            this.addThisNodeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.addThisNodeToolStripMenuItem.Text = "Add This Node";
            // 
            // node1ToolStripMenuItem
            // 
            this.node1ToolStripMenuItem.Name = "node1ToolStripMenuItem";
            this.node1ToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.node1ToolStripMenuItem.Text = "Node 1";
            this.node1ToolStripMenuItem.Click += new System.EventHandler(this.node1ToolStripMenuItem_Click);
            // 
            // node2ToolStripMenuItem
            // 
            this.node2ToolStripMenuItem.Name = "node2ToolStripMenuItem";
            this.node2ToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.node2ToolStripMenuItem.Text = "Node 2";
            this.node2ToolStripMenuItem.Click += new System.EventHandler(this.node2ToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkidtextBox);
            this.groupBox1.Controls.Add(this.deletebutton);
            this.groupBox1.Location = new System.Drawing.Point(562, 433);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 53);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Remove the Links";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Addbutton);
            this.groupBox2.Controls.Add(this.LinktextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.SearchLinkbutton);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.EndNodetextBox);
            this.groupBox2.Controls.Add(this.StartNodetextBox);
            this.groupBox2.Location = new System.Drawing.Point(562, 142);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 197);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search the Link";
            // 
            // Addbutton
            // 
            this.Addbutton.Location = new System.Drawing.Point(213, 158);
            this.Addbutton.Name = "Addbutton";
            this.Addbutton.Size = new System.Drawing.Size(61, 24);
            this.Addbutton.TabIndex = 9;
            this.Addbutton.Text = "Add";
            this.Addbutton.UseVisualStyleBackColor = true;
            this.Addbutton.Click += new System.EventHandler(this.Addbutton_Click);
            // 
            // LinktextBox
            // 
            this.LinktextBox.Location = new System.Drawing.Point(18, 161);
            this.LinktextBox.Name = "LinktextBox";
            this.LinktextBox.Size = new System.Drawing.Size(181, 19);
            this.LinktextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Link";
            // 
            // SearchLinkbutton
            // 
            this.SearchLinkbutton.Location = new System.Drawing.Point(213, 85);
            this.SearchLinkbutton.Name = "SearchLinkbutton";
            this.SearchLinkbutton.Size = new System.Drawing.Size(61, 24);
            this.SearchLinkbutton.TabIndex = 3;
            this.SearchLinkbutton.Text = "Search";
            this.SearchLinkbutton.UseVisualStyleBackColor = true;
            this.SearchLinkbutton.Click += new System.EventHandler(this.SearchLinkbutton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Node 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Node 1";
            // 
            // EndNodetextBox
            // 
            this.EndNodetextBox.Location = new System.Drawing.Point(18, 88);
            this.EndNodetextBox.Name = "EndNodetextBox";
            this.EndNodetextBox.Size = new System.Drawing.Size(181, 19);
            this.EndNodetextBox.TabIndex = 3;
            // 
            // StartNodetextBox
            // 
            this.StartNodetextBox.Location = new System.Drawing.Point(18, 39);
            this.StartNodetextBox.Name = "StartNodetextBox";
            this.StartNodetextBox.Size = new System.Drawing.Size(181, 19);
            this.StartNodetextBox.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new System.Drawing.Point(562, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(294, 125);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Property";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.Driverlabel);
            this.groupBox6.Location = new System.Drawing.Point(18, 130);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(240, 45);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "DriverID";
            // 
            // Driverlabel
            // 
            this.Driverlabel.AutoSize = true;
            this.Driverlabel.Location = new System.Drawing.Point(15, 20);
            this.Driverlabel.Name = "Driverlabel";
            this.Driverlabel.Size = new System.Drawing.Size(43, 12);
            this.Driverlabel.TabIndex = 12;
            this.Driverlabel.Text = "driverid";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Semanticslabel);
            this.groupBox5.Location = new System.Drawing.Point(18, 69);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(240, 45);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Semantics";
            // 
            // Semanticslabel
            // 
            this.Semanticslabel.AutoSize = true;
            this.Semanticslabel.Location = new System.Drawing.Point(15, 19);
            this.Semanticslabel.Name = "Semanticslabel";
            this.Semanticslabel.Size = new System.Drawing.Size(57, 12);
            this.Semanticslabel.TabIndex = 11;
            this.Semanticslabel.Text = "semantics";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.SemanticLinkIDlabel);
            this.groupBox4.Location = new System.Drawing.Point(18, 18);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(240, 45);
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
            // Reloadbutton
            // 
            this.Reloadbutton.Location = new System.Drawing.Point(562, 492);
            this.Reloadbutton.Name = "Reloadbutton";
            this.Reloadbutton.Size = new System.Drawing.Size(294, 48);
            this.Reloadbutton.TabIndex = 10;
            this.Reloadbutton.Text = "Reload HTML File";
            this.Reloadbutton.UseVisualStyleBackColor = true;
            this.Reloadbutton.Click += new System.EventHandler(this.Reloadbutton_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.AddAllLinks_button);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.Node_textBox);
            this.groupBox7.Location = new System.Drawing.Point(562, 351);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(294, 70);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Add the All Links with this Node";
            // 
            // AddAllLinks_button
            // 
            this.AddAllLinks_button.Location = new System.Drawing.Point(213, 36);
            this.AddAllLinks_button.Name = "AddAllLinks_button";
            this.AddAllLinks_button.Size = new System.Drawing.Size(61, 24);
            this.AddAllLinks_button.TabIndex = 9;
            this.AddAllLinks_button.Text = "Add";
            this.AddAllLinks_button.UseVisualStyleBackColor = true;
            this.AddAllLinks_button.Click += new System.EventHandler(this.AddAllLinks_button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "Node";
            // 
            // Node_textBox
            // 
            this.Node_textBox.Location = new System.Drawing.Point(18, 39);
            this.Node_textBox.Name = "Node_textBox";
            this.Node_textBox.Size = new System.Drawing.Size(181, 19);
            this.Node_textBox.TabIndex = 4;
            // 
            // searchLinksWithNodeToolStripMenuItem
            // 
            this.searchLinksWithNodeToolStripMenuItem.Name = "searchLinksWithNodeToolStripMenuItem";
            this.searchLinksWithNodeToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.searchLinksWithNodeToolStripMenuItem.Text = "Search Links with Node";
            this.searchLinksWithNodeToolStripMenuItem.Click += new System.EventHandler(this.searchLinksWithNodeToolStripMenuItem_Click);
            // 
            // Browser_SemanticLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 552);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.Reloadbutton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.webBrowser1);
            this.Name = "Browser_SemanticLink";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ECOLOGViewer";
            this.ClickedcontextMenuStrip.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox linkidtextBox;
        private System.Windows.Forms.Button deletebutton;
        private System.Windows.Forms.ContextMenuStrip ClickedcontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addThisLinkToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Addbutton;
        private System.Windows.Forms.TextBox LinktextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SearchLinkbutton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox EndNodetextBox;
        private System.Windows.Forms.TextBox StartNodetextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label Driverlabel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label Semanticslabel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label SemanticLinkIDlabel;
        private System.Windows.Forms.ToolStripMenuItem addThisNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem node1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem node2ToolStripMenuItem;
        private System.Windows.Forms.Button Reloadbutton;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button AddAllLinks_button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Node_textBox;
        private System.Windows.Forms.ToolStripMenuItem searchLinksWithNodeToolStripMenuItem;
    }
}