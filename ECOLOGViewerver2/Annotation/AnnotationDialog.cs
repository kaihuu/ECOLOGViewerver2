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
    /// レビュー一覧ダイアログを取り扱うクラス
    /// </summary>
    public partial class AnnotationDialog : Form
    {
        private DataTable dt_event;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AnnotationDialog()
        {
            InitializeComponent();
            //フォームサイズを固定
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            select_event();
        }

        private void select_event()
        {
            string query = "select DISTINCT ANNOTATION.TRIP_ID, ANNOTATION.START_TIME, ANNOTATION.END_TIME, ANNOTATION.EVENT_ID, EVENT, COMPLEMENT     ";
            query += "from ANNOTATION, EVENT where ANNOTATION.EVENT_ID = EVENT.EVENT_ID ";
            query += "order by TRIP_ID desc ";

            dt_event = DatabaseAccess.GetResult(query);

            EventListdataGridView.DataSource = dt_event;
            EventListdataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void Displaybutton_Click(object sender, EventArgs e)
        {
            #region FormDataインスタンス作成
            FormData user = new FormData();
            user.currentDirectory = System.Environment.CurrentDirectory + @"\Log\" + DateTime.Now.ToString("yyyyMMddHHmmss");
            user.currentFile = "";
            user.PointingDistance = 1;
            user.aggregation = "LINK_ID";
            #endregion

            #region DataGridViewから読み出し
            foreach (DataGridViewRow row in EventListdataGridView.SelectedRows)
            {
                user.tripID = EventListdataGridView[0, row.Index].Value.ToString();
                user.startTime = EventListdataGridView[1, row.Index].Value.ToString();
                user.endTime = EventListdataGridView[2, row.Index].Value.ToString();
                user.annotation = EventListdataGridView[4, row.Index].Value.ToString();
            }
            #endregion

            Trajectory form = new Trajectory(user);

            if (form.makeFile_Annotation())
            {
                EasyBrowser easybrowser = new EasyBrowser(user);
                MainForm.ShowWindow(easybrowser);

                this.Dispose();
            }
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            if (EventListdataGridView.SelectedRows.Count > 0)
            {
                string trip_id = "", start_time = "", end_time = "", annotation = "";

                #region DataGridViewから読み出し
                foreach (DataGridViewRow row in EventListdataGridView.SelectedRows)
                {
                    trip_id = EventListdataGridView[0, row.Index].Value.ToString();
                    start_time = EventListdataGridView[1, row.Index].Value.ToString();
                    end_time = EventListdataGridView[2, row.Index].Value.ToString();
                    annotation = EventListdataGridView[3, row.Index].Value.ToString();
                }
                #endregion

                string query = "delete from ANNOTATION ";
                query += "where TRIP_ID = " + trip_id + " ";
                query += "and START_TIME = '" + start_time + "' ";
                query += "and END_TIME = '" + end_time + "' ";
                query += "and EVENT_ID = " + annotation + " ";

                try
                {
                    DatabaseAccess.ExecuteQuery(query);

                    MessageBox.Show("The Event is Deleted.\n", "OK");

                    select_event();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(ex.ToString());
                    MessageBox.Show("Error.\n\n" + ex.ToString(), "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please Select an Event.\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
