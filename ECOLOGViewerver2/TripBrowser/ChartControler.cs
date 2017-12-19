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
    /// チャートコントローラを取り扱うクラス
    /// </summary>
    public partial class ChartControler : Form
    {
        private FormData user;
        private TripBrowser browser = null;
        private LeafSpyBrowser LSbrowser = null;
        private DatabaseAccess dbaccess;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="b">親となるブラウザー画面</param>
        /// <param name="u">表示するトリップの情報</param>
        public ChartControler(TripBrowser b, FormData u)
        {
            InitializeComponent();

            browser = b;
            user = new FormData(u);

            setStartTime(b.StartTime);
            setEndTime(b.EndTime);

            TimechartcomboBox.SelectedIndex = 0;
            Aggregation_comboBox.SelectedIndex = 0;
            Value_comboBox.SelectedIndex = 0;
            AggregationcomboBox.SelectedIndex = 0;

            #region EVENT
            foreach (string key in MainForm.Event.Keys)
            {
                EventlistBox.Items.Add(key);
            }
            #endregion

            #region デモ
            if (u.startTime == "2012/04/26 19:56:57")
            {
                StartTimetextBox.Text = "2012/04/26 20:16:57";
                EndTimetextBox.Text = "2012/04/26 20:19:47";
                TimechartcomboBox.SelectedIndex = 0;
                MaxAxisYtextBox.Text = "180";
            }
            else if (u.startTime == "2012/05/02 23:10:33")
            {
                StartTimetextBox.Text = "2012/05/02 23:31:30";
                EndTimetextBox.Text = "2012/05/02 23:32:30";
                TimechartcomboBox.SelectedIndex = 0;
                MaxAxisYtextBox.Text = "180";
            }
            #endregion
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="b">親となるブラウザー画面</param>
        /// <param name="u">表示するトリップの情報</param>
        public ChartControler(LeafSpyBrowser b, FormData u)
        {
            InitializeComponent();

            LSbrowser = b;
            user = new FormData(u);

            setStartTime(b.StartTime);
            setEndTime(b.EndTime);

            TimechartcomboBox.SelectedIndex = 0;
            Aggregation_comboBox.SelectedIndex = 0;
            Value_comboBox.SelectedIndex = 0;
            AggregationcomboBox.SelectedIndex = 0;

            #region EVENT
            foreach (string key in MainForm.Event.Keys)
            {
                EventlistBox.Items.Add(key);
            }
            #endregion

            #region デモ
            if (u.startTime == "2012/04/26 19:56:57")
            {
                StartTimetextBox.Text = "2012/04/26 20:16:57";
                EndTimetextBox.Text = "2012/04/26 20:19:47";
                TimechartcomboBox.SelectedIndex = 0;
                MaxAxisYtextBox.Text = "180";
            }
            else if (u.startTime == "2012/05/02 23:10:33")
            {
                StartTimetextBox.Text = "2012/05/02 23:31:30";
                EndTimetextBox.Text = "2012/05/02 23:32:30";
                TimechartcomboBox.SelectedIndex = 0;
                MaxAxisYtextBox.Text = "180";
            }
            #endregion
        }

        #region ボタン
        private void Chartbutton_Click(object sender, EventArgs e)
        {

            DateTime t1 = new DateTime();
            DateTime t2 = new DateTime();

            if (DateTime.TryParse(StartTimetextBox.Text, out t1) && DateTime.TryParse(EndTimetextBox.Text, out t2))
            {
                if (t1 > t2)
                {
                    DateTime work = t1;
                    t1 = t2;
                    t2 = work;
                }
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                TimeChart chart = null;

                chart = new TimeChart(user, t1, t2, TimechartcomboBox.SelectedItem.ToString(), AxisXtextBox.Text, MaxAxisYtextBox.Text, MinAxisYtextBox.Text);

                //if (TimechartcomboBox.SelectedIndex == 2)
                //{
                //    chart = new TimeChart(user, t1, t2, TimechartcomboBox.SelectedItem.ToString(), AxisXtextBox.Text, MaxAxisYtextBox.Text, MinAxisYtextBox.Text);
                //}
                //else if (TimechartcomboBox.SelectedIndex == 3)
                //{
                //    chart = new TimeChart(user, t1, t2, "LongitudinalAcc", AxisXtextBox.Text, MaxAxisYtextBox.Text, MinAxisYtextBox.Text);
                //}
                //else if (TimechartcomboBox.SelectedIndex == 4)
                //{
                //    chart = new TimeChart(user, t1, t2, "LateralAcc", AxisXtextBox.Text, MaxAxisYtextBox.Text, MinAxisYtextBox.Text);
                //}
                //else if (TimechartcomboBox.SelectedIndex == 5)
                //{
                //    chart = new TimeChart(user, t1, t2, "ConsumedEnergy", AxisXtextBox.Text, MaxAxisYtextBox.Text, MinAxisYtextBox.Text);
                //}
                //else if (TimechartcomboBox.SelectedIndex == 6)
                //{
                //    chart = new TimeChart(user, t1, t2, "LostEnergy", AxisXtextBox.Text, MaxAxisYtextBox.Text, MinAxisYtextBox.Text);
                //}
                //else if (TimechartcomboBox.SelectedIndex == 0)
                //{
                //    chart = new TimeChart(user, t1, t2, "PowerModel", AxisXtextBox.Text, MaxAxisYtextBox.Text, MinAxisYtextBox.Text);
                //}
                //else if (TimechartcomboBox.SelectedIndex == 1)
                //{
                //    chart = new TimeChart(user, t1, t2, "EnergyModel", AxisXtextBox.Text, MaxAxisYtextBox.Text, MinAxisYtextBox.Text);
                //}
                MainForm.ShowWindow(chart);

                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("DateTime is Wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Displaybutton_Click(object sender, EventArgs e)
        {
            DateTime t1 = new DateTime();
            DateTime t2 = new DateTime();

            string str1 = (Aggregation_comboBox.SelectedIndex == 0) ? "LINK_ID" : "SEMANTIC_LINK_ID";
            string str2 = (Value_comboBox.SelectedIndex == 0) ? "SUM(LOST_ENERGY)/SUM(DISTANCE_DIFFERENCE)" : "COUNT('X')/SUM(DISTANCE_DIFFERENCE)";

            if (DateTime.TryParse(StartTimetextBox.Text, out t1) && DateTime.TryParse(EndTimetextBox.Text, out t2))
            {
                if (t1 > t2)
                {
                    DateTime work = t1;
                    t1 = t2;
                    t2 = work;
                }
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                Histogram chart = new Histogram(user, t1, t2, str1, str2);
                MainForm.ShowWindow(chart);

                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("DateTime is Wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RadarChart_button_Click(object sender, EventArgs e)
        {
            DateTime t1 = new DateTime();
            DateTime t2 = new DateTime();

            string str = (AggregationcomboBox.SelectedIndex == 0) ? "LINK_ID" : "SEMANTIC_LINK_ID";

            if (DateTime.TryParse(StartTimetextBox.Text, out t1) && DateTime.TryParse(EndTimetextBox.Text, out t2))
            {
                if (t1 > t2)
                {
                    DateTime work = t1;
                    t1 = t2;
                    t2 = work;
                }
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                Radarchart chart = new Radarchart(user, t1, t2, str);
                MainForm.ShowWindow(chart);

                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("DateTime is Wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
            DateTime t1 = new DateTime();
            DateTime t2 = new DateTime();

            if (DateTime.TryParse(StartTimetextBox.Text, out t1) && DateTime.TryParse(EndTimetextBox.Text, out t2))
            {
                try
                {
                    if (EventMemotextBox.Text == "")
                    {
                        EventMemotextBox.Text = null;
                    }

                    if (EventtextBox.Text != "")
                    {
                        string startLat = "0.0", startLng = "0.0", endLat = "0.0", endLng = "0.0";

                        #region 開始地点の緯度経度取得
                        string query = "select TOP 1 LATITUDE, LONGITUDE from ECOLOG where TRIP_ID = " + user.tripID + " and JST >= '" + StartTimetextBox.Text + "' order by JST";

                        DataTable work = new DataTable();

                        work = DatabaseAccess.GetResult(query);

                        startLat = work.Rows[0][0].ToString();
                        startLng = work.Rows[0][1].ToString();

                        //cmd = new SqlCommand(query, sqlConnection1);
                        //sqlConnection1.Open();
                        //r = cmd.ExecuteReader();
                        //r.Read();
                        //startLat = r.GetDouble(0).ToString();
                        //startLng = r.GetDouble(1).ToString();
                        //sqlConnection1.Close();
                        #endregion

                        #region 終了地点の緯度経度取得
                        query = "select TOP 1 LATITUDE, LONGITUDE from ECOLOG where TRIP_ID = " + user.tripID + " and JST <= '" + EndTimetextBox.Text + "' order by JST desc";

                        work = new DataTable();

                        work = DatabaseAccess.GetResult(query);

                        endLat = work.Rows[0][0].ToString();
                        endLng = work.Rows[0][1].ToString();

                        //cmd = new SqlCommand(query, sqlConnection1);
                        //sqlConnection1.Open();
                        //r = cmd.ExecuteReader();
                        //r.Read();
                        //endLat = r.GetDouble(0).ToString();
                        //endLng = r.GetDouble(1).ToString();
                        //sqlConnection1.Close();
                        #endregion

                        #region クエリ設定
                        query = "select * from EVENT where EVENT = '" + EventtextBox.Text + "' ";

                        work = new DataTable();

                        work = DatabaseAccess.GetResult(query);

                        if (work.Rows.Count == 0)
                        {
                            #region EVENTに追加がある場合
                            query = "select MAX(EVENT_ID) as MAX_of_ID ";
                            query += "from EVENT ";

                            work = new DataTable();

                            work = DatabaseAccess.GetResult(query);

                            int max = int.Parse(work.Rows[0][0].ToString()) + 1;

                            query = "SET IDENTITY_INSERT EVENT on;";
                            query = "insert into EVENT(EVENT_ID, EVENT) ";
                            query += "select " + max + ", '" + EventtextBox.Text + "' ";

                            DatabaseAccess.ExecuteQuery(query);

                            MainForm.Event = DatabaseAccess.GetEvent();
                            EventlistBox.Items.Clear();

                            foreach (string key in MainForm.Event.Keys)
                            {
                                EventlistBox.Items.Add(key);
                            }
                            #endregion
                        }


                        query = "insert into ANNOTATION(TRIP_ID, START_TIME, END_TIME, START_LATITUDE, START_LONGITUDE, END_LATITUDE, END_LONGITUDE, EVENT_ID, COMPLEMENT) ";
                        query += "values(" + user.tripID + ",'" + StartTimetextBox.Text + "','" + EndTimetextBox.Text + "'," + startLat + "," + startLng + "," + endLat + "," + endLng + "," + MainForm.Event[EventtextBox.Text] + ", '" + EventMemotextBox.Text + "')";
                        #endregion

                        DatabaseAccess.ExecuteQuery(query);

                        MessageBox.Show("The Event is Added.\n", "OK");

                    }
                    else
                    {
                        MessageBox.Show("Please Select a Event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    WriteLog.Write(ex.ToString());
                    MessageBox.Show("Error.\n\n" + ex.ToString(), "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("DateTime is Wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SemanticLinkbutton_Click(object sender, EventArgs e)
        {
            if (StartTimetextBox.Text != "" && EndTimetextBox.Text != "")
            {

                #region 挿入内容設定
                #region SemanticLinkID
                int MaxofID = 1;
                string query = "select MAX(SEMANTIC_LINK_ID) as number ";
                query += "	from SEMANTIC_LINKS ";

                DataTable work = new DataTable();

                work = DatabaseAccess.GetResult(query);

                if (work.Rows.Count > 0)
                {
                    MaxofID = int.Parse(work.Rows[0][0].ToString()) + 1;
                }

                //using (SqlConnection sqlConnection1 = new SqlConnection(Program.cn))
                //{
                //    sqlConnection1.Open();
                //    SqlCommand cmd = new SqlCommand(query, sqlConnection1);
                //    SqlDataReader r = cmd.ExecuteReader();

                //    // 現在の最大値+1
                //    if (r.Read())
                //    {
                //        if (!r.IsDBNull(0))
                //        {
                //            MaxofID = r.GetInt32(0) + 1;
                //        }
                //    }
                //    sqlConnection1.Close();
                //}
                #endregion

                #region Semantics
                String semantics;

                if (SemanticstextBox.Text == "")
                {
                    semantics = "NULL";
                }
                else
                {
                    semantics = "'" + SemanticstextBox.Text + "'";
                }
                #endregion
                #endregion

                #region クエリ
                query += "insert into SEMANTIC_LINKS ";
                query += "select DISTINCT " + MaxofID + ", DRIVER_ID, LINK_ID, " + semantics + " ";
                query += "from [ECOLOGTable] as ECOLOG ";
                query += "where JST between '" + StartTimetextBox.Text + "' and '" + EndTimetextBox.Text + "' ";
                query += "and TRIP_ID = " + user.tripID + " ";
                query += "and LINK_ID is not null ";
                #endregion
                query = query.Replace("[ECOLOGTable]", MainForm.ECOLOGTable);
                if (DatabaseAccess.ExecuteQuery(query))
                {
                    MessageBox.Show("Make a SemanticLink.", "Complete");
                }
                else
                {
                    MessageBox.Show("Error Ocurred.", "Imcomplete");
                }

            }
            else
            {
                MessageBox.Show("Please Select Links.", "Error");
            }
        }
        #endregion

        internal void setStartTime(DateTime t)
        {
            StartTimetextBox.Text = t.ToString();
        }

        internal void setEndTime(DateTime t)
        {
            EndTimetextBox.Text = t.ToString();
        }

        #region イベント検知
        // TimeChartチェックボックス変更時
        private void AllTimeSelectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AllTimeSelectCheckBox1.Checked == true)
            {
                StartTimetextBox.Text = user.startTime;
                EndTimetextBox.Text = user.endTime;

                StartTimetextBox.Enabled = false;
                EndTimetextBox.Enabled = false;
            }
            else
            {
                StartTimetextBox.Enabled = true;
                EndTimetextBox.Enabled = true;
            }
        }

        private void EventlistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EventtextBox.Text = EventlistBox.Text.ToString();
        }

        private void ChartControler_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (browser != null)
            {
                browser.ctrlShowed = false;
            }

            if (LSbrowser != null)
            {
                LSbrowser.ctrlShowed = false;
            }
        }
        #endregion

    }
}
