using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECOLOGViewerver2
{
    /// <summary>
    /// セマンティックリンク選択ダイアログを取り扱うクラス
    /// </summary>
    public partial class SemanticLinkDialog : Form
    {
        private DatabaseAccess dbaccess;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SemanticLinkDialog(string connection)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Focus();

            dbaccess = new DatabaseAccess(connection);

            string query = "select DISTINCT SEMANTIC_LINK_ID, SEMANTICS, DRIVER_ID ";
            query += "from SEMANTIC_LINKS ";
            query += "order by SEMANTIC_LINK_ID ";

            DataTable dt_semanticlink = DatabaseAccess.GetResult(query);

            SemanticLinksdataGridView.DataSource = dt_semanticlink;
            SemanticLinksdataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            #region ドライバー
            DrivercomboBox.Items.Add("All");
            foreach (string key in MainForm.Driver.Keys)
            {
                DrivercomboBox.Items.Add(key);
            }
            DrivercomboBox.SelectedIndex = 0;
            #endregion

            #region カー
            CarcomboBox.Items.Add("All");
            foreach (string key in MainForm.Car.Keys)
            {
                CarcomboBox.Items.Add(key);
            }
            CarcomboBox.SelectedIndex = 0;
            #endregion

            DirectioncomboBox.SelectedIndex = 0;

        }

        private void Displaybutton_Click(object sender, System.EventArgs e)
        {
            //if (DrivercomboBox.SelectedIndex == 0)
            //{
            //    ScatterChart chart = new ScatterChart("","","");
            //    Program.ShowWindow(chart);
            //    this.Dispose();
            //}
            //else
            {
                #region FormDataインスタンス作成
                FormData user = new FormData();

                if (CarcomboBox.SelectedIndex == 0)
                {
                    user.carID = CarcomboBox.SelectedItem.ToString();
                }
                else
                {
                    user.carID = MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString();
                }

                if (DrivercomboBox.SelectedIndex == 0)
                {
                    user.driverID = DrivercomboBox.SelectedItem.ToString();
                }
                else
                {
                    user.driverID = MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString();
                }

                user.direction = DirectioncomboBox.SelectedItem.ToString();

                #region DataGridViewから読み出し
                foreach (DataGridViewRow row in SemanticLinksdataGridView.SelectedRows)
                {
                    user.semanticLinkID = SemanticLinksdataGridView[0, row.Index].Value.ToString();
                    user.semantics = SemanticLinksdataGridView[1, row.Index].Value.ToString();
                }
                #endregion

                user.currentDirectory = System.Environment.CurrentDirectory + @"\Log\SemanticLinks\" + user.semanticLinkID + "";
                user.currentFile = user.currentDirectory + @"\SemanticLink.html";
                #endregion

                if (!File.Exists(user.currentFile))
                {
                    Trajectory form = new Trajectory(user);
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                    form.makeFile_SemanticLink();
                    System.Windows.Forms.Cursor.Current = Cursors.Default;
                }

                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                //SemanticLinkBrowser browser = new SemanticLinkBrowser(user);
                SemanticLinkBrowser browser = new SemanticLinkBrowser(user);
                MainForm.ShowWindow(browser);
                System.Windows.Forms.Cursor.Current = Cursors.Default;

            }

        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Editbutton_Click(object sender, EventArgs e)
        {
            #region FormDataインスタンス作成
            FormData user = new FormData();
            user.currentDirectory = System.Environment.CurrentDirectory + @"\Log\SemanticLinks\" + DateTime.Now.ToString("yyyyMMddHHmmss");
            user.currentFile = "";
            #endregion

            #region DataGridViewから読み出し
            foreach (DataGridViewRow row in SemanticLinksdataGridView.SelectedRows)
            {
                user.semanticLinkID = SemanticLinksdataGridView[0, row.Index].Value.ToString();
                user.semantics = SemanticLinksdataGridView[1, row.Index].Value.ToString();
                user.driverID = SemanticLinksdataGridView[2, row.Index].Value.ToString();
            }
            #endregion

            Trajectory form = new Trajectory(user);
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            form.makeFile_SemanticLink_Old();

            SemanticLinkEditor browser = new SemanticLinkEditor(user);
            MainForm.ShowWindow(browser);
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        #region イベント検知
        private void SemanticLinkDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Dispose();
            }
        }
        #endregion
    }
}
