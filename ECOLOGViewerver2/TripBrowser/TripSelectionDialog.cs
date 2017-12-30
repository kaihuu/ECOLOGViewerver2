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
    /// トリップ検索ダイアログを取り扱うクラス
    /// </summary>
    public partial class TripSelectionDialog : Form
    {
        #region 変数定義
        // データ保持用クラス
        private FormData user;
        // ブラウザ
        private TripBrowser browser;
        private LeafSpyBrowser LSbrowser;
        // DB用
        private DataTable dt_trip;
        //private DatabaseAccess dbaccess;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TripSelectionDialog(string connection)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //dbaccess = new DatabaseAccess(connection);


            #region FormDataインスタンス作成
            user = new FormData();
            user.currentDirectory = System.Environment.CurrentDirectory + "\\LOG";
            user.currentFile = "";
            user.PointingDistance = int.Parse(pdtextBox.Text);

            user.averageQuery = "with AvgALL as (  \r\n";
            user.worstQuery += "select LINK_ID,(SUM(LOST_ENERGY) / SUM(DISTANCE_DIFFERENCE))as AVG_LOSS,COUNT(*) as count \r\n";
            user.worstQuery += "from ECOLOG \r\n";
            user.worstQuery += "where SPEED > 1  \r\n";
            user.worstQuery += "and DRIVER_ID = driverID \r\n";
            user.worstQuery += "and CAR_ID = carID \r\n";
            user.worstQuery += "and SENSOR_ID = sensorID \r\n";
            user.worstQuery += "group by LINK_ID  \r\n";
            user.worstQuery += "),  \r\n";
            user.worstQuery += "ThisTime as( \r\n";
            user.worstQuery += "select LINK_ID,(SUM(LOST_ENERGY) / SUM(DISTANCE_DIFFERENCE))as This_LOSS \r\n";
            user.worstQuery += "from ECOLOG \r\n";
            user.worstQuery += "where SPEED > 1 \r\n";
            user.worstQuery += "and DRIVER_ID = driverID \r\n";
            user.worstQuery += "and CAR_ID = carID \r\n";
            user.worstQuery += "and SENSOR_ID = sensorID \r\n";
            user.worstQuery += "and JST > 'startTime' \r\n";
            user.worstQuery += "and JST < 'endTime' \r\n";
            user.worstQuery += "group by LINK_ID  \r\n";
            user.worstQuery += "), \r\n";
            user.worstQuery += "DIF_LOS as( \r\n";
            user.worstQuery += "select TOP 20 AvgALL.LINK_ID,This_LOSS,AVG_LOSS,(This_LOSS - AVG_LOSS) as DIF \r\n";
            user.worstQuery += "from ThisTime , AvgALL \r\n";
            user.worstQuery += "where ThisTime.LINK_ID = AvgALL.LINK_ID \r\n";
            user.worstQuery += "and AvgALL.count > 20 \r\n";
            user.worstQuery += "order by DIF desc\r\n";
            user.worstQuery += ") \r\n";
            user.worstQuery += "select * \r\n  ";
            user.worstQuery += "from ECOLOG right join DIF_LOS \r\n";
            user.worstQuery += "on ECOLOG.LINK_ID = DIF_LOS.LINK_ID \r\n";
            user.worstQuery += "and DRIVER_ID = driverID \r\n";
            user.worstQuery += "and CAR_ID = carID \r\n";
            user.worstQuery += "and SENSOR_ID = sensorID \r\n";
            user.worstQuery += "and JST > 'startTime' \r\n";
            user.worstQuery += "and JST < 'endTime' \r\n";
            #endregion

            #region Combobox初期化
            #region ドライバー
            foreach (string key in MainForm.Driver.Keys)
            {
                DrivercomboBox.Items.Add(key);
            }
            #endregion

            #region センサ
            foreach (string key in MainForm.Sensor.Keys)
            {
                SensorcomboBox.Items.Add(key);
            }
            #endregion

            #region カー
            foreach (string key in MainForm.Car.Keys)
            {
                CarcomboBox.Items.Add(key);
            }
            #endregion

            AggregationcomboBox.SelectedIndex = 0;
            CarcomboBox.SelectedIndex = 0;
            DatacomboBox.SelectedIndex = 0;
            DrivercomboBox.SelectedIndex = 0;
            SensorcomboBox.SelectedIndex = 0;
            DirectioncomboBox.SelectedIndex = 0;
            InfocomboBox.SelectedIndex = 0;
            ECOLOGTable_comboBox.SelectedIndex = 0;

            this.InfocomboBox.SelectedIndexChanged += new System.EventHandler(this.InfocomboBox_SelectedIndexChanged);
            this.DirectioncomboBox.SelectedIndexChanged += new System.EventHandler(this.DirectioncomboBox_SelectedIndexChanged);
            this.DrivercomboBox.SelectedIndexChanged += new System.EventHandler(this.drivercomboBox_SelectedIndexChanged);
            this.SensorcomboBox.SelectedIndexChanged += new System.EventHandler(this.sensorcomboBox_SelectedIndexChanged);
            this.CarcomboBox.SelectedIndexChanged += new System.EventHandler(this.carcomboBox_SelectedIndexChanged);
            this.ECOLOGTable_comboBox.SelectedIndexChanged += new System.EventHandler(this.ECOLOGTable_comboBox_SelectedIndexChanged);

            #endregion
           
            select_trajectory();

        }
        // トリップ検索
        private void select_trajectory()
        {
            MainForm.ECOLOGTable = ECOLOGTable_comboBox.Text;

            #region クエリ
            string query = "";

            if (ConsumedEnergycheckBox.Checked)
            {
                query = "select DISTINCT TRIPS.TRIP_ID, ECOLOG.DRIVER_ID, ECOLOG.SENSOR_ID, ECOLOG.CAR_ID, START_TIME, END_TIME, round(SUM_ENERGY, 3) as CONSUMED_ENERGY, ECOLOG.TRIP_DIRECTION     ";
                query += "from TRIPS ";
                query += "left join ( ";
                query += "select TRIP_ID, SUM(CONSUMED_ELECTRIC_ENERGY) as SUM_ENERGY ";
                query += "from ECOLOG ";
                query += "group by TRIP_ID ) SubTable ";
                query += "on TRIPS.TRIP_ID = SubTable.TRIP_ID ";
            }
            else
            {
                query = "select DISTINCT ECOLOG.TRIP_ID, ECOLOG.DRIVER_ID, ECOLOG.SENSOR_ID, ECOLOG.CAR_ID, START_TIME, END_TIME, round(CONSUMED_ENERGY, 3) as CONSUMED_ENERGY, ECOLOG.TRIP_DIRECTION     ";
                query += "from TRIPS ";
            }

            query += "right join ECOLOG ";
            query += "on TRIPS.TRIP_ID = ECOLOG.TRIP_ID ";

            if (LeafSpy_checkBox.Checked)
            {
                query += "right join ( ";
                query += "	select DISTINCT TRIP_ID ";
                query += "	from LEAFSPY_EQUALLY_DIVIDED_AIR_CONDITION ) LEAFSPY ";
                query += "on TRIPS.TRIP_ID = LEAFSPY.TRIP_ID ";

            }

            query += "where CONSUMED_ENERGY is not null ";

            if (GasolinecheckBox.Checked)
            {
                query += "and CONSUMED_FUEL is not null ";
            }
            if (CarcomboBox.SelectedIndex > 0)
            {
                query += "and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.Text.ToString()] + " ";
            }
            if (SensorcomboBox.SelectedIndex > 0)
            {
                query += "and ECOLOG.SENSOR_ID = " + MainForm.Sensor[SensorcomboBox.Text.ToString()] + " ";
            }
            if (DrivercomboBox.SelectedIndex > 0)
            {
                query += "and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.Text.ToString()] + " ";
            }
            if (DirectioncomboBox.SelectedIndex > 0)
            {
                query += "and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.Text.ToString() + "' ";
            }

            if (TripsExtra_checkBox.Checked)
            {
                //query = "select DISTINCT TRIPS_EXTRA.TRIP_ID, ECOLOG.DRIVER_ID, ECOLOG.SENSOR_ID, ECOLOG.CAR_ID, START_TIME, END_TIME, round(CONSUMED_ENERGY, 3) as CONSUMED_ENERGY, ECOLOG.TRIP_DIRECTION     ";
                //query += "from ECOLOG ";
                //query += "right join TRIPS_EXTRA ";
                //query += "on ECOLOG.TRIP_ID = TRIPS_EXTRA.TRIP_ID ";
                //query += "where CONSUMED_ENERGY is not null ";

                query = "select DISTINCT TRIPS.TRIP_ID, ECOLOG.DRIVER_ID, ECOLOG.SENSOR_ID, ECOLOG.CAR_ID, START_TIME, END_TIME, round(CONSUMED_ENERGY, 3) as CONSUMED_ENERGY, ECOLOG.TRIP_DIRECTION     ";
                query += "from ECOLOG ";
                query += "right join TRIPS ";
                query += "on ECOLOG.TRIP_ID = TRIPS.TRIP_ID ";
                query += "where CONSUMED_ENERGY is not null ";
                query += "and VALIDATION = 'extra' ";
                if (CarcomboBox.SelectedIndex > 0)
                {
                    query += "and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.Text.ToString()] + " ";
                }
                if (SensorcomboBox.SelectedIndex > 0)
                {
                    query += "and ECOLOG.SENSOR_ID = " + MainForm.Sensor[SensorcomboBox.Text.ToString()] + " ";
                }
                if (DrivercomboBox.SelectedIndex > 0)
                {
                    query += "and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.Text.ToString()] + " ";
                }
                if (DirectioncomboBox.SelectedIndex > 0)
                {
                    query += "and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.Text.ToString() + "' ";
                }
            }
            
            if(demoCheckBox.Checked)
            {
                query = "select DISTINCT TRIPS.TRIP_ID, ECOLOG.DRIVER_ID, ECOLOG.SENSOR_ID, ECOLOG.CAR_ID, START_TIME, END_TIME, round(CONSUMED_ENERGY, 3) as CONSUMED_ENERGY, ECOLOG.TRIP_DIRECTION     ";
                query += "from ECOLOG ";
                query += "right join TRIPS ";
                query += "on ECOLOG.TRIP_ID = TRIPS.TRIP_ID ";
                query += "where CONSUMED_ENERGY is not null ";
                //query += "and ECOLOG.TRIP_ID in (1585,2291,2086,2157) ";
                query += "and ECOLOG.TRIP_ID in (1982,2291,3444) ";
            }

            query += "order by START_TIME desc ";

            string tripsTable = DBName.trips;
            if (MainForm.ECOLOGTable == "ECOLOG_MM_LINKS_LOOKUP")
            {
                tripsTable = DBName.trips_mm;
            }
            else if (MainForm.ECOLOGTable == "[ECOLOG_SPEEDLPF0.05_MM_LINKS_LOOKUP]")
            {
                tripsTable = DBName.trips_lpf_mm;
            }
            else if (MainForm.ECOLOGTable == "ECOLOG_Doppler"){
                tripsTable = DBName.trips_doppler_mm;
            }
            query = query.Replace("TRIPS", tripsTable);
            query = query.Replace("ECOLOG", MainForm.ECOLOGTable);
            if (useFixed_checkBox.Checked)
            {
                query = query.Replace("ECOLOG", "ECOLOG_ALTITUDE_FIXED");
            }
            #endregion

            dt_trip = DatabaseAccess.GetResult(query);
            TripDataGrid.DataSource = dt_trip;

            if (dt_trip.Rows.Count > 0)
            {
                TripDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                TripDataGrid.Columns["CONSUMED_ENERGY"].DefaultCellStyle.Format = "F3";
            }
        }
        // キャンセル
        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        // 軌跡表示関係
        private void Display_button_Click(object sender, EventArgs e)
        {
            #region 引数設定
            //PointingDistanceの設定
            user.PointingDistance = int.Parse(pdtextBox.Text);

            #region DataGridViewから読み出し
            foreach (DataGridViewRow row in TripDataGrid.SelectedRows)
            {
                user.tripID = TripDataGrid[0, row.Index].Value.ToString();
                user.driverID = TripDataGrid[1, row.Index].Value.ToString();
                user.carID = TripDataGrid[3, row.Index].Value.ToString();
                user.sensorID = TripDataGrid[1, row.Index].Value.ToString();
                user.startTime = TripDataGrid[4, row.Index].Value.ToString();
                user.endTime = TripDataGrid[5, row.Index].Value.ToString();
                user.consumedenergy = TripDataGrid[6, row.Index].Value.ToString();
                user.direction = TripDataGrid[7, row.Index].Value.ToString();
            }
            #endregion

            #region 集約単位
            if (AggregationcomboBox.SelectedIndex == 1)
            {
                user.aggregation = "MESH_ID";
            }
            else if (AggregationcomboBox.SelectedIndex == 0)
            {
                user.aggregation = "LINK_ID";
            }
            else if (AggregationcomboBox.SelectedIndex == 2)
            {
                user.aggregation = "SEMANTIC_LINK_ID";
            }
            #endregion

            #region 表示内容
            if (DatacomboBox.SelectedIndex == 0)
            {
                // 消費エネルギー
                user.value = "ConsumedEnergy";
            }
            else if (DatacomboBox.SelectedIndex == 1)
            {
                // エネルギーロス
                user.value = "LostEnergy";
            }
            else if (DatacomboBox.SelectedIndex == 2)
            {
                // ガソリン流量
                user.value = "UsedFuel";
            }
            else if (DatacomboBox.SelectedIndex == 9)
            {
                // エネルギーロス＋ガソリン流量
                user.value = "LostEnergy+UsedFuel";
            }
            else if (DatacomboBox.SelectedIndex == 4)
            {
                // 加減速表示
                user.value = "LongitudinalAcc";
            }
            else if (DatacomboBox.SelectedIndex == 5)
            {
                // 左右
                user.value = "LateralAcc";
            }
            else if (DatacomboBox.SelectedIndex == 3)
            {
                // 速度
                user.value = "Speed";
            }
            #endregion

            #region Polyline
            if (InfocomboBox.SelectedIndex == 0)
            {
                user.polyline = "trajectory";
            }
            else if (InfocomboBox.SelectedIndex == 1)
            {
                user.polyline = "information";
            }
            else if (InfocomboBox.SelectedIndex == 2)
            {
                user.polyline = "average";
            }
            else if (InfocomboBox.SelectedIndex == 3)
            {
                user.polyline = "worst";
            }
            #endregion

            if (useFixed_checkBox.Checked)
            {
                user.usefixed = true;
            }

            if (useNexus7CameraCheckBox.Checked)
            {
                user.useNexus7Camera = true;
            }
            else
            {
                user.useNexus7Camera = false;
            }

            //Program.ECOLOGTable = ECOLOGTable_textBox.Text;
            MainForm.ECOLOGTable = ECOLOGTable_comboBox.Text;

            user.currentDirectory = System.Environment.CurrentDirectory + @"\Log" + @"\"+ ECOLOGTable_comboBox.Text + @"\[" + user.tripID + ", " + user.value + ", " + user.polyline + "]";
            #endregion

            if (Marker_checkBox.Checked)
                MainForm.marker = true;

            if (File.Exists(user.currentDirectory + @"\Log.txt"))
            {
                #region 設定の読み込み
                using (StreamReader r = new StreamReader(user.currentDirectory + @"\Log.txt"))
                {
                    r.ReadLine();//DataTime
                    user.driverID = r.ReadLine();
                    user.carID = r.ReadLine();
                    user.sensorID = r.ReadLine();
                    user.tripID = r.ReadLine();
                    user.startTime = r.ReadLine();
                    user.endTime = r.ReadLine();
                    user.direction = r.ReadLine();
                    user.value = r.ReadLine();
                    user.polyline = r.ReadLine();
                    user.consumedenergy = r.ReadLine();
                    user.currentFile = user.currentDirectory + @"\car_trajectory.html";
                    r.ReadLine();//EOD
                }
                #endregion
            }
            else
            {
                Trajectory form = new Trajectory(user);
                Cursor.Current = Cursors.WaitCursor;

                MainForm.DebugMode = EditQuerycheckBox.Checked;

                if (form.makeFile())
                {
                    #region 設定の保存
                    using (StreamWriter w = new StreamWriter(user.currentDirectory + @"\Log.txt"))
                    {
                        w.WriteLine(DateTime.Now);
                        w.WriteLine(user.driverID);
                        w.WriteLine(user.carID);
                        w.WriteLine(user.sensorID);
                        w.WriteLine(user.tripID);
                        w.WriteLine(user.startTime);
                        w.WriteLine(user.endTime);
                        w.WriteLine(user.direction);
                        w.WriteLine(user.value);
                        w.WriteLine(user.polyline);
                        w.WriteLine(user.consumedenergy);
                        w.WriteLine("EOD");
                    }
                    #endregion
                }

                Cursor.Current = Cursors.Default;
            }

            if (LeafSpycheckBox.Checked)
            {
                LSbrowser = new LeafSpyBrowser(user);
                LSbrowser.Text = "Trip Browser[" + user.tripID + ", " + user.driverID + ", " + user.carID + ", " + user.sensorID + ", " + user.startTime + ", " + user.endTime + ", " + user.value + ", " + user.consumedenergy + ", " + user.direction + "]";
                MainForm.ShowWindow(LSbrowser);
            }
            else
            {
                browser = new TripBrowser(user);
                browser.Text = "Trip Browser[" + user.tripID + ", " + user.driverID + ", " + user.carID + ", " + user.sensorID + ", " + user.startTime + ", " + user.endTime + ", " + user.value + ", " + user.consumedenergy + ", " + user.direction + "]";
                MainForm.ShowWindow(browser);
            }
        }
        // 平均クエリ編集
        private void AverageQuerybutton_Click(object sender, EventArgs e)
        {
            AverageQueryDialog form = new AverageQueryDialog(user.averageQuery);

            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                user.averageQuery = form.query;
            }

        }
        // ワーストクエリ編集
        private void WorstQuerybutton_Click(object sender, EventArgs e)
        {
            if (user.worstQuery == "")
            {
                WorstQueryDialog form = new WorstQueryDialog();

                form.ShowDialog(this);

                if (form.DialogResult == DialogResult.OK)
                {
                    user.worstQuery = form.query;
                }
            }
            else
            {
                WorstQueryDialog form = new WorstQueryDialog(user.worstQuery);

                form.ShowDialog(this);

                if (form.DialogResult == DialogResult.OK)
                {
                    user.worstQuery = form.query;
                }
            }
        }

        #region ボタンクリック以外のイベント
        private void InfocomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (InfocomboBox.SelectedIndex == 2)
            {
                // 平均
                AverageQuerybutton.Enabled = true;
                WorstQuerybutton.Enabled = false;
            }
            else if (InfocomboBox.SelectedIndex == 3)
            {
                //要注意地点
                AverageQuerybutton.Enabled = false;
                WorstQuerybutton.Enabled = true;
            }
            else
            {
                AverageQuerybutton.Enabled = false;
                WorstQuerybutton.Enabled = false;
            }
        }

        private void drivercomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            select_trajectory();
        }

        private void carcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            select_trajectory();
        }

        private void sensorcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            select_trajectory();
        }

        private void DirectioncomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            select_trajectory();
        }

        private void TripSelectionDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void useFixed_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            select_trajectory();

            #region クエリ
            //string query = "";

            //if (DriverSencorcomboBox.Text == "Demo")
            //{
            //    query = "select DISTINCT TRIPS.TRIP_ID, TRIPS.DRIVER_SENSOR_ID, TRIPS.CAR_ID, START_TIME, END_TIME, round(CONSUMED_ENERGY, 3) as CONSUMED_ENERGY, TRIP_DIRECTION     ";
            //    query += "from TRIPS ";
            //    query += "where CONSUMED_ENERGY is not null ";
            //    query += "and (TRIPS.TRIP_ID = 722 ";
            //    query += "or TRIPS.TRIP_ID = 720 ";
            //    query += "or TRIPS.TRIP_ID = 716 ";
            //    query += "or TRIPS.TRIP_ID = 713 ";
            //    query += "or TRIPS.TRIP_ID = 682 ";
            //    query += "or TRIPS.TRIP_ID = 664 ";
            //    query += "or TRIPS.TRIP_ID = 657 ";
            //    query += "or TRIPS.TRIP_ID = 650 ";
            //    query += "or TRIPS.TRIP_ID = 646 ";
            //    query += "or TRIPS.TRIP_ID = 645 ";
            //    query += "or TRIPS.TRIP_ID = 641 ";
            //    query += "or TRIPS.TRIP_ID = 639 ";
            //    query += "or TRIPS.TRIP_ID = 628 ";
            //    query += "or TRIPS.TRIP_ID = 612 ";
            //    query += "or TRIPS.TRIP_ID = 556 ";
            //    query += "or TRIPS.TRIP_ID = 550 ";
            //    query += "or TRIPS.TRIP_ID = 549 ";
            //    query += "or TRIPS.TRIP_ID = 445 ";
            //    query += "or TRIPS.TRIP_ID = 434 ";
            //    query += "or TRIPS.TRIP_ID = 430 ";
            //    query += "or TRIPS.TRIP_ID = 425 ";
            //    query += "or TRIPS.TRIP_ID = 407 ";
            //    query += "or TRIPS.TRIP_ID = 404 ";

            //    query += "or TRIPS.TRIP_ID = 653 ";
            //    query += "or TRIPS.TRIP_ID = 637 ";
            //    query += "or TRIPS.TRIP_ID = 633 ";
            //    query += "or TRIPS.TRIP_ID = 631 ";
            //    query += "or TRIPS.TRIP_ID = 630 ";
            //    query += "or TRIPS.TRIP_ID = 625 ";
            //    query += "or TRIPS.TRIP_ID = 623 ";
            //    query += "or TRIPS.TRIP_ID = 620 ";
            //    query += "or TRIPS.TRIP_ID = 552 ";
            //    query += "or TRIPS.TRIP_ID = 442 ";
            //    query += "or TRIPS.TRIP_ID = 437 ";
            //    query += "or TRIPS.TRIP_ID = 421 ";
            //    query += "or TRIPS.TRIP_ID = 415 ";
            //    query += "or TRIPS.TRIP_ID = 411) ";
            //    query += "order by START_TIME desc ";

            //    InfocomboBox.SelectedIndex = 2;
            //}
            //else
            //{
            //    query = "select DISTINCT TRIPS.TRIP_ID, TRIPS.DRIVER_SENSOR_ID, TRIPS.CAR_ID, START_TIME, END_TIME, round(CONSUMED_ENERGY, 3) as CONSUMED_ENERGY, TRIP_DIRECTION     ";
            //    query += "from ECOLOG, TRIPS ";
            //    query += "left join DRIVER_SENSOR ";
            //    query += "on TRIPS.DRIVER_SENSOR_ID = DRIVER_SENSOR.DRIVER_SENSOR_ID ";
            //    query += "where CONSUMED_ENERGY is not null ";
            //    query += "and TRIPS.TRIP_ID = ECOLOG.TRIP_ID ";
            //    if (CarcomboBox.SelectedIndex > 0)
            //    {
            //        query += "and TRIPS.CAR_ID = " + Program.Car[CarcomboBox.Text.ToString()] + " ";

            //    }
            //    if (DriverSencorcomboBox.SelectedIndex > 0)
            //    {
            //        //query += "and TRIPS.DRIVER_SENSOR_ID = " + Program.DriverSensor[DriverSencorcomboBox.Text.ToString()] + " ";
            //        query += "and DRIVER_SENSOR.DRIVER_ID = " + Program.Driver[DriverSencorcomboBox.Text.ToString()] + " ";
            //    }
            //    if (DirectioncomboBox.SelectedIndex > 0)
            //    {
            //        query += "and TRIPS.TRIP_DIRECTION = '" + DirectioncomboBox.Text.ToString() + "' ";
            //    }

            //    if (TripsExtra_checkBox.Checked)
            //    {
            //        query += "union ";

            //        query += "select DISTINCT TRIPS_EXTRA.TRIP_ID, TRIPS_EXTRA.DRIVER_SENSOR_ID, TRIPS_EXTRA.CAR_ID, START_TIME, END_TIME, round(CONSUMED_ENERGY, 3) as CONSUMED_ENERGY, TRIP_DIRECTION     ";
            //        query += "from ECOLOG, TRIPS_EXTRA ";
            //        query += "left join DRIVER_SENSOR ";
            //        query += "on TRIPS_EXTRA.DRIVER_SENSOR_ID = DRIVER_SENSOR.DRIVER_SENSOR_ID ";
            //        query += "where uCONSUMED_ENERGY is not null ";
            //        query += "and TRIPS_EXTRA.TRIP_ID = ECOLOG.TRIP_ID ";
            //        if (CarcomboBox.SelectedIndex > 0)
            //        {
            //            query += "and TRIPS_EXTRA.CAR_ID = " + Program.Car[CarcomboBox.Text.ToString()] + " ";

            //        }
            //        if (DriverSencorcomboBox.SelectedIndex > 0)
            //        {
            //            //query += "and TRIPS_EXTRA.DRIVER_SENSOR_ID = " + Program.DriverSensor[DriverSencorcomboBox.Text.ToString()] + " ";
            //            query += "and DRIVER_SENSOR.DRIVER_ID = " + Program.Driver[DriverSencorcomboBox.Text.ToString()] + " ";
            //        }
            //        if (DirectioncomboBox.SelectedIndex > 0)
            //        {
            //            query += "and TRIPS_EXTRA.TRIP_DIRECTION = '" + DirectioncomboBox.Text.ToString() + "' ";
            //        }
            //    }
            //    query += "order by TRIPS.START_TIME desc ";

            //    //query += "order by START_TIME desc ";
            //}

            //if (useFixed_checkBox.Checked)
            //{
            //    query = query.Replace("ECOLOG", "ECOLOG_LEAF");
            //}
            #endregion

            //dt_trip = Program.Get_Result(query);
            //TripDataGrid.DataSource = dt_trip;
        }

        private void TripsExtra_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            select_trajectory();
        }

        private void GasolinecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            select_trajectory();
        }

        private void LeafSpy_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            select_trajectory();
        }
        #endregion

        private void demoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            select_trajectory();
        }

        private void ECOLOGTable_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            select_trajectory();
        }

        private void ECOLOGTable_comboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
