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
    /// <summary>
    /// セマンティックリンクを編集する画面
    /// </summary>
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class SemanticLinkEditor : Form
    {
        private FormData user;
        private string selected_link;
        private string selected_node;

        /// <summary>
        /// セマンティックリンクを編集する画面
        /// </summary>
        /// <param name="u">表示するセマンティックリンクの情報</param>
        public SemanticLinkEditor(FormData u)
        {
            InitializeComponent();
            user = new FormData(u);
            webBrowser1.ObjectForScripting = this;

            this.SemanticLinkIDlabel.Text = user.semanticLinkID.ToString();
            this.Semanticslabel.Text = user.semantics;
            this.Driverlabel.Text = user.driverID;
            this.Focus();

            Browse();
        }

        private void Browse()
        {
            webBrowser1.Navigate(user.currentFile);
        }
        /// <summary>
        /// Google Map上でマーカーが右クリックされた時の処理
        /// </summary>
        /// <param name="str1">クリックされたマーカーのリンク</param>
        /// <param name="str2">クリックされたマーカーのノード</param>
        public void IconRightClick(string str1, string str2)
        {
            selected_link = str1.Trim();
            selected_node = str2.Trim();
            ClickedcontextMenuStrip.Show(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
        }

        private void deletebutton_Click(object sender, EventArgs e)
        {
            if (linkidtextBox.Text != "")
            {

                string query = "delete ";
                query += "from SEMANTIC_LINKS ";
                query += "where SEMANTIC_LINK_ID = " + user.semanticLinkID + " ";
                query += "and LINK_ID in ('" + linkidtextBox.Text + "') ";

                DatabaseAccess.ExecuteQuery(query);

                MessageBox.Show("OK.", "Complete");

                System.IO.File.Delete(user.currentFile);
                linkidtextBox.Text = "";

                Trajectory form = new Trajectory(user);

                form.makeFile_SemanticLink_Old();

                this.Browse();
            }
            else
            {
                MessageBox.Show("Please Select Links.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addThisLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (linkidtextBox.Text == "")
            {
                linkidtextBox.Text = "'" + selected_link + "'";
            }
            else
            {
                linkidtextBox.Text += ", '" + selected_link + "'";
            }
        }

        private void node1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartNodetextBox.Text = selected_node;
        }

        private void node2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EndNodetextBox.Text = selected_node;
        }

        private void SearchLinkbutton_Click(object sender, EventArgs e)
        {
            if (StartNodetextBox.Text != "" && EndNodetextBox.Text != "")
            {

                string query = "select LINK_ID ";
                query += "from LINKS ";
                query += "where NODE_ID in('" + StartNodetextBox.Text + "', '" + EndNodetextBox.Text + "') ";
                query += "group by LINK_ID ";
                query += "having count(*) > 1 ";

                DataTable dt = new DataTable();

                dt = DatabaseAccess.GetResult(query);

                if (dt.Rows.Count > 0)
                {
                    LinktextBox.Text = dt.Rows[0][0].ToString().Trim();
                    MessageBox.Show("OK.", "Complete");
                }
                else
                {
                    MessageBox.Show("Link Not Found.", "Complete");
                }
            }
            else
            {
                MessageBox.Show("Please Select Nodes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
            if (LinktextBox.Text != "")
            {
                string query = "insert into SEMANTIC_LINKS ";
                query += "values(" + user.semanticLinkID + ", " + user.driverID + ", '" + LinktextBox.Text + "', '" + user.semantics + "')";

                if (DatabaseAccess.ExecuteQuery(query))
                {
                    MessageBox.Show("OK.", "Complete");
                    //System.IO.File.Delete(user.currentfile);
                    //System.IO.Directory.Delete(user.currentdirectory);
                    LinktextBox.Text = "";
                    StartNodetextBox.Text = "";
                    EndNodetextBox.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Please Select Links.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void Reloadbutton_Click(object sender, EventArgs e)
        {
            Trajectory form = new Trajectory(user);

            form.makeFile_SemanticLink_Old();

            this.Browse();
        }

        private void AddAllLinks_button_Click(object sender, EventArgs e)
        {
            if (Node_textBox.Text != "")
            {
                //string query = "insert into SEMANTIC_LINKS ";
                //query += "values(" + user.semanticLinkID + ", " + user.driverID + ", '" + LinktextBox.Text + "', '" + user.semantics + "')";

                string query = "insert into SEMANTIC_LINKS ";
                query += "select " + user.semanticLinkID + ", " + user.driverID + ", LINK_ID, '" + user.semantics + "' ";
                query += "from LINKS ";
                query += "where NODE_ID = '" + Node_textBox.Text + "' ";
                query += "except ";
                query += "select " + user.semanticLinkID + ", " + user.driverID + ", LINK_ID, '" + user.semantics + "' ";
                query += "from SEMANTIC_LINKS ";
                query += "where SEMANTIC_LINK_ID = " + user.semanticLinkID + " ";

                if (DatabaseAccess.ExecuteQuery(query))
                {
                    MessageBox.Show("OK.", "Complete");
                    Node_textBox.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Please Select Node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchLinksWithNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Node_textBox.Text = selected_node;
        }
    }
}
