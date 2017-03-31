using System;
using System.Data;
using System.Windows.Forms;
namespace ECOLOGViewerver2
{
    /// <summary>
    /// トリップ検索ダイアログを取り扱うクラス
    /// </summary>
    public partial class TripSelectionDialog_Daily : Form
    {
        #region 変数定義
        // データ保持用クラス
        private FormData outward = new FormData();
        // データ保持用クラス
        private FormData homeward = new FormData();
        // DB用
        private DataTable dt_trip;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TripSelectionDialog_Daily()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            TripDataGrid.Columns.Add(column);

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

            CarcomboBox.SelectedIndex = 0;
            DrivercomboBox.SelectedIndex = 0;
            SensorcomboBox.SelectedIndex = 0;
            DirectioncomboBox.SelectedIndex = 0;

            this.DirectioncomboBox.SelectedIndexChanged += new System.EventHandler(this.DirectioncomboBox_SelectedIndexChanged);
            this.DrivercomboBox.SelectedIndexChanged += new System.EventHandler(this.drivercomboBox_SelectedIndexChanged);
            this.SensorcomboBox.SelectedIndexChanged += new System.EventHandler(this.sensorcomboBox_SelectedIndexChanged);
            this.CarcomboBox.SelectedIndexChanged += new System.EventHandler(this.carcomboBox_SelectedIndexChanged);
            #endregion

            select_trajectory();

        }
        // トリップ検索
        private void select_trajectory()
        {
            #region クエリ
            string query = "select DISTINCT ECOLOG.TRIP_ID, ECOLOG.DRIVER_ID, ECOLOG.SENSOR_ID, ECOLOG.CAR_ID, START_TIME, END_TIME, round(CONSUMED_ENERGY, 3) as CONSUMED_ENERGY, ECOLOG.TRIP_DIRECTION     ";
            query += "from TRIPS ";
            query += "right join ECOLOG as ECOLOG ";
            query += "on TRIPS.TRIP_ID = ECOLOG.TRIP_ID ";

            query += "where CONSUMED_ENERGY is not null ";

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
                //query += "from ECOLOG as ECOLOG ";
                //query += "right join TRIPS_EXTRA ";
                //query += "on ECOLOG.TRIP_ID = TRIPS_EXTRA.TRIP_ID ";
                //query += "where CONSUMED_ENERGY is not null ";

                query = "select DISTINCT TRIPS.TRIP_ID, ECOLOG.DRIVER_ID, ECOLOG.SENSOR_ID, ECOLOG.CAR_ID, START_TIME, END_TIME, round(CONSUMED_ENERGY, 3) as CONSUMED_ENERGY, ECOLOG.TRIP_DIRECTION     ";
                query += "from ECOLOG as ECOLOG ";
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

            query += "order by START_TIME desc ";
            #endregion

            dt_trip = DatabaseAccess.GetResult(query);
            TripDataGrid.DataSource = dt_trip;
            TripDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            TripDataGrid.Columns["CONSUMED_ENERGY"].DefaultCellStyle.Format = "F3";
        }
        // キャンセル
        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Display_button_Click(object sender, EventArgs e)
        {
            outward.tripID = homeward.tripID = "-1";

            foreach (DataGridViewRow row in TripDataGrid.Rows)
            {
                if (TripDataGrid[0, row.Index].Value != null)
                {
                    if ((Boolean)TripDataGrid[0, row.Index].Value)
                    {
                        if (outward.tripID == "-1")
                        {
                            outward.tripID = TripDataGrid[1, row.Index].Value.ToString();
                            outward.driverID = TripDataGrid[2, row.Index].Value.ToString();
                            outward.carID = TripDataGrid[4, row.Index].Value.ToString();
                            outward.startTime = TripDataGrid[5, row.Index].Value.ToString();
                            outward.endTime = TripDataGrid[6, row.Index].Value.ToString();
                            outward.consumedenergy = TripDataGrid[7, row.Index].Value.ToString();
                            outward.direction = TripDataGrid[8, row.Index].Value.ToString();
                        }
                        else if (homeward.tripID == "-1")
                        {
                            homeward.tripID = TripDataGrid[1, row.Index].Value.ToString();
                            homeward.driverID = TripDataGrid[2, row.Index].Value.ToString();
                            homeward.carID = TripDataGrid[4, row.Index].Value.ToString();
                            homeward.startTime = TripDataGrid[5, row.Index].Value.ToString();
                            homeward.endTime = TripDataGrid[6, row.Index].Value.ToString();
                            homeward.consumedenergy = TripDataGrid[7, row.Index].Value.ToString();
                            homeward.direction = TripDataGrid[8, row.Index].Value.ToString();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            DateTime t1 = DateTime.Parse(outward.startTime);
            DateTime t2 = DateTime.Parse(homeward.startTime);

            if (t1 > t2)
            {
                FormData work = new FormData(outward);
                outward = new FormData(homeward);
                homeward = new FormData(work);
            }

            if (t1.Date == t2.Date)
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                TimeChart chart = new TimeChart(outward, homeward);

                MainForm.ShowWindow(chart);

                System.Windows.Forms.Cursor.Current = Cursors.Default;

                //this.Dispose();
            }
            else
            {
                MessageBox.Show("Trips must be the same day.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #region イベント検知
        private void TripsExtra_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            select_trajectory();
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
        #endregion
    }
}