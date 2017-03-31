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
    ///  運転振り返り画面を取り扱うクラス
    /// </summary>
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class LeafSpyBrowser : Form
    {
        #region 変数
        private FormData user;
        private DateTime selected_time;
        private string selected_link = "";
        private DataTable dt_picture = new DataTable();
        private DataTable dt_chart = new DataTable();
        private DataTable dt_chart_leafspy = new DataTable();
        private DataTable dt_soc = new DataTable();
        private double Latitude = 0.0;
        private double Longitude = 0.0;
        private double distance = 0.0;
        private ChartControler ctrl;
        internal bool ctrlShowed = false;
        private bool move_link = true;
        internal DateTime StartTime = new DateTime();
        internal DateTime EndTime = new DateTime();
        private System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
        private System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
        private System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
        private System.Windows.Forms.DataVisualization.Charting.Series speed = new System.Windows.Forms.DataVisualization.Charting.Series();
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="u">表示するトリップの情報</param>
        public LeafSpyBrowser(FormData u)
        {
            InitializeComponent();

            user = new FormData(u);

            selected_time = DateTime.Parse(user.startTime);

            StartTime = DateTime.Parse(user.startTime);
            EndTime = DateTime.Parse(user.endTime);

            webBrowser1.ObjectForScripting = this;

            webBrowser1.Navigate(user.currentFile);

            InitPictureViewer();

            InitChart();

            PaintChart(selected_time, true);

            ctrl = new ChartControler(this, user);

            this.Text = "";

            LoadImage(0);
        }

        #region PictureViewer
        private void InitPictureViewer()
        {
            LabelCurrent.Visible = true;
            LabelCurrent.Text = "1";
            LabelMax.Visible = true;
            LabelMax.Text = "0";

            //int count = DatabaseAccess.CorrectedPictureDataChecker(user.startTime,user.endTime);

            int checkNexus7 = DatabaseAccess.CorrectedPictureDataChecker(StartTime.ToString(), EndTime.ToString(), true);
            int checkCamera = DatabaseAccess.CorrectedPictureDataChecker(StartTime.ToString(), EndTime.ToString(), false);

            if (checkNexus7 != 0 || checkCamera != 0)
            {
                string query = "select SubTable.*,case when PICTURE is null then LAG(PICTURE) over (order by SubTable.JST) else PICTURE end as PICTURE ";
                query += "from (    ";
                query += "  select ECOLOG.*   ";
                query += "  from ECOLOG as ECOLOG   ";
                query += "  where ECOLOG.TRIP_ID = " + user.tripID + "  ";
                query += "    ) SubTable   ";
                query += "left join CORRECTED_PICTURE as PICT on SubTable.DRIVER_ID = PICT.DRIVER_ID   ";
                query += "and SubTable.JST = PICT.JST   ";
                if ((checkNexus7 != 0 && user.useNexus7Camera) || checkCamera == 0)
                {
                    query += "and PICT.SENSOR_ID in (17,18,20) ";
                }
                else
                {
                    query += "and PICT.SENSOR_ID = 19 ";
                }
                query += "order by SubTable.JST ";

                query = query.Replace("[ECOLOGTable]", MainForm.ECOLOGTable);

                dt_picture = DatabaseAccess.GetResult(query);

                int max = dt_picture.Rows.Count;
                Slider.Maximum = max;
                Slider.Minimum = 1;
                Slider.TickFrequency = max / 10;
                LabelMax.Text = "/ " + max.ToString();

            }
            else
            {
                string query = "select ECOLOG.*,null as PICTURE ";
                query += "from [ECOLOGTable] as ECOLOG ";
                query += "where ECOLOG.TRIP_ID = " + user.tripID + "  ";
                query += "order by JST ";

                query = query.Replace("[ECOLOGTable]", MainForm.ECOLOGTable);

                dt_picture = DatabaseAccess.GetResult(query);

                int max = dt_picture.Rows.Count;
                Slider.Maximum = max;
                Slider.Minimum = 1;
                Slider.TickFrequency = max / 10;
                LabelMax.Text = "/ " + max.ToString();

                pictureBoxImage.ImageLocation = System.Environment.CurrentDirectory + "/Image/noimage.gif";
                LabelCurrent.Visible = false;
                LabelMax.Visible = false;
            }
        }

        private void LoadImage(int ImageNum)
        {

            double Speed = 0;
            double LongitudinalACC = 0;
            double LateralACC = 0;
            double VerticalACC = 0;
            double ConsumedEnergy = 0;
            double LostEnergy = 0;

            if (dt_picture.Rows.Count < 2)
            {
                MessageBox.Show("Picture Not Found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (dt_picture.Rows[ImageNum]["LINK_ID"] != DBNull.Value)
                {
                    if (selected_link != (string)dt_picture.Rows[ImageNum]["LINK_ID"])
                    {
                        move_link = true;
                    }
                    else
                    {
                        move_link = false;
                    }
                }

                if (dt_picture.Rows[ImageNum]["SPEED"] != DBNull.Value)
                    Speed = (float)dt_picture.Rows[ImageNum]["SPEED"];
                if (dt_picture.Rows[ImageNum]["LATITUDE"] != DBNull.Value)
                    Latitude = (double)dt_picture.Rows[ImageNum]["LATITUDE"];
                if (dt_picture.Rows[ImageNum]["LONGITUDE"] != DBNull.Value)
                    Longitude = (double)dt_picture.Rows[ImageNum]["LONGITUDE"];
                if (dt_picture.Rows[ImageNum]["LONGITUDINAL_ACC"] != DBNull.Value)
                    LongitudinalACC = (float)dt_picture.Rows[ImageNum]["LONGITUDINAL_ACC"];
                if (dt_picture.Rows[ImageNum]["LATERAL_ACC"] != DBNull.Value)
                    LateralACC = (float)dt_picture.Rows[ImageNum]["LATERAL_ACC"];
                if (dt_picture.Rows[ImageNum]["VERTICAL_ACC"] != DBNull.Value)
                    VerticalACC = (float)dt_picture.Rows[ImageNum]["VERTICAL_ACC"];
                if (dt_picture.Rows[ImageNum]["CONSUMED_ELECTRIC_ENERGY"] != DBNull.Value)
                    ConsumedEnergy = Math.Round((float)dt_picture.Rows[ImageNum]["CONSUMED_ELECTRIC_ENERGY"], 4);
                if (dt_picture.Rows[ImageNum]["LOST_ENERGY"] != DBNull.Value)
                    LostEnergy = Math.Round((float)dt_picture.Rows[ImageNum]["LOST_ENERGY"], 4);
                if (dt_picture.Rows[ImageNum]["DISTANCE_DIFFERENCE"] != DBNull.Value)
                    distance = (float)dt_picture.Rows[ImageNum]["DISTANCE_DIFFERENCE"];
                if (dt_picture.Rows[ImageNum]["LINK_ID"] != DBNull.Value)
                {
                    selected_link = (string)dt_picture.Rows[ImageNum]["LINK_ID"];
                }
                else
                {
                    selected_link = "null";
                }
                //BLOB is read into Byte array, then used to construct MemoryStream,          
                //then passed to PictureBox. 
                Byte[] byteBLOBData = new Byte[0];
                if (dt_picture.Rows[ImageNum]["PICTURE"] != DBNull.Value)
                {
                    byteBLOBData = (Byte[])(dt_picture.Rows[ImageNum]["PICTURE"]);
                    MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
                    pictureBoxImage.Image = Image.FromStream(stmBLOBData);
                }
                else
                {
                    pictureBoxImage.ImageLocation = System.Environment.CurrentDirectory + "/Image/noimage.gif";

                }
                //時刻表示
                selected_time = (DateTime)dt_picture.Rows[ImageNum]["JST"];
                TimetextBox.Text = selected_time.ToString("yyyy/MM/dd HH:mm:ss");
                //データ表示
                labelSpeed.Text = Speed.ToString("f1");
                labelLongitudinal_ACC.Text = LongitudinalACC.ToString("f3");
                labelConsumedEnergy.Text = (ConsumedEnergy * 3600).ToString("f5");
                labelLoss.Text = (LostEnergy * 3600).ToString("f5");
                Linkidlabel.Text = selected_link;

            }
        }

        private void setGMapCenter(double lat, double lng)
        {
            // 中心座標の移動
            try
            {
                String scripts = "moveCenter();";
                scripts += "function moveCenter() { map.panTo(new google.maps.LatLng(" + lat + ", " + lng + "));";
                scripts += "center_marker.setPosition(map.getCenter());";
                scripts += "}";
                webBrowser1.Url = new Uri("javascript:" + Uri.EscapeDataString(scripts) + ";"); // 実行

                return;
            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

        #region Chart
        private void InitChart()
        {
            chartArea1.AxisX.MajorGrid.Interval = 30;
            chartArea1.AxisX.MajorTickMark.Interval = 10;
            chartArea1.AxisX.LabelStyle.Interval = 10;
            chartArea1.AxisY.MajorGrid.Interval = 20;
            //chartArea1.AxisY2.MajorGrid.Interval = 20;
            chartArea1.Name = "ChartArea1";
            this.Timechart.ChartAreas.Add(chartArea1);

            chartArea1.AxisY.Title = "Power(ECOLOG) [kW]";
            //chartArea1.AxisY2.Title = "Speed[km/h]";

            chartArea1.AxisY.Maximum = 100;
            chartArea1.AxisY.Minimum = -100;
            //chartArea1.AxisY2.Maximum = 100;
            //chartArea1.AxisY2.Minimum = -100;

            chartArea2.AxisX.MajorGrid.Interval = 30;
            chartArea2.AxisX.MajorTickMark.Interval = 10;
            chartArea2.AxisX.LabelStyle.Interval = 10;
            chartArea2.AxisY.MajorGrid.Interval = 20;
            chartArea2.Name = "ChartArea2";
            this.GIDSchart.ChartAreas.Add(chartArea2);

            //chartArea2.AxisY.Title = "GIDs";

            //chartArea2.AxisY.Maximum = 250;
            //chartArea2.AxisY.Minimum = 0;
            chartArea2.AxisY.Title = "Power(LeafSpy) [kW]";

            chartArea2.AxisY.Maximum = 100;
            chartArea2.AxisY.Minimum = -100;

            chartArea3.AxisX.MajorGrid.Interval = 30;
            chartArea3.AxisX.MajorTickMark.Interval = 10;
            chartArea3.AxisX.LabelStyle.Interval = 10;
            chartArea3.AxisY.MajorGrid.Interval = 20;
            chartArea3.Name = "ChartArea3";
            this.SOCchart.ChartAreas.Add(chartArea3);

            //chartArea3.AxisY.Title = "SoC[%]";

            //chartArea3.AxisY.Maximum = 100;
            //chartArea3.AxisY.Minimum = 0;
            chartArea3.AxisY.Title = "Power(Air-Conditioner) [kW]";

            chartArea3.AxisY.Maximum = 10;
            chartArea3.AxisY.Minimum = 0;

            #region インスタンス作成
            // Consumed_energy > 0
            System.Windows.Forms.DataVisualization.Charting.Series air_energy_plus = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series rolling_energy_plus = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series climbing_energy_plus_up = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series climbing_energy_plus_down = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series acc_energy = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series convert_loss_plus = new System.Windows.Forms.DataVisualization.Charting.Series();
            // Consumed_energy < 0
            System.Windows.Forms.DataVisualization.Charting.Series air_energy_minus = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series rolling_energy_minus = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series climbing_energy_minus_up = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series climbing_energy_minus_down = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series convert_loss_minus = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series regene_energy = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series regene_loss = new System.Windows.Forms.DataVisualization.Charting.Series();

            //System.Windows.Forms.DataVisualization.Charting.Series gids = new System.Windows.Forms.DataVisualization.Charting.Series();
            //System.Windows.Forms.DataVisualization.Charting.Series soc = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series power_ls = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series air_con = new System.Windows.Forms.DataVisualization.Charting.Series();
            #endregion

            #region 設定
            #region 運動エネルギー＝加速抵抗
            acc_energy.ChartArea = "ChartArea1";
            acc_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            acc_energy.Color = System.Drawing.Color.ForestGreen;
            acc_energy.Legend = "Legend1";
            acc_energy.Name = "運動エネルギー分[kW]";
            acc_energy.XValueMember = "time";
            acc_energy.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            acc_energy.YValueMembers = "ACC_ENERGY_PLUS";
            #endregion

            #region 空気抵抗
            air_energy_plus.ChartArea = "ChartArea1";
            air_energy_plus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            air_energy_plus.Color = System.Drawing.Color.Yellow;
            air_energy_plus.Legend = "Legend1";
            air_energy_plus.Name = "空気抵抗(力行)[kW]";
            air_energy_plus.XValueMember = "time";
            air_energy_plus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            air_energy_plus.YValueMembers = "AIR_ENERGY_PLUS";

            air_energy_minus.ChartArea = "ChartArea1";
            air_energy_minus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            air_energy_minus.Color = System.Drawing.Color.Silver;
            air_energy_minus.Legend = "Legend1";
            air_energy_minus.Name = "空気抵抗(回生)[kW]";
            air_energy_minus.XValueMember = "time";
            air_energy_minus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            air_energy_minus.YValueMembers = "AIR_ENERGY_MINUS";
            #endregion

            #region 転がり抵抗
            rolling_energy_plus.ChartArea = "ChartArea1";
            rolling_energy_plus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            rolling_energy_plus.Color = System.Drawing.Color.SandyBrown;
            rolling_energy_plus.Legend = "Legend1";
            rolling_energy_plus.Name = "転がり抵抗(力行)[kW]";
            rolling_energy_plus.XValueMember = "time";
            rolling_energy_plus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            rolling_energy_plus.YValueMembers = "ROLLING_ENERGY_PLUS";

            rolling_energy_minus.ChartArea = "ChartArea1";
            rolling_energy_minus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            rolling_energy_minus.Color = System.Drawing.Color.SlateGray;
            rolling_energy_minus.Legend = "Legend1";
            rolling_energy_minus.Name = "転がり抵抗(回生)[kW]";
            rolling_energy_minus.XValueMember = "time";
            rolling_energy_minus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            rolling_energy_minus.YValueMembers = "ROLLING_ENERGY_MINUS";
            #endregion

            #region 登坂抵抗
            climbing_energy_plus_up.ChartArea = "ChartArea1";
            climbing_energy_plus_up.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            climbing_energy_plus_up.Color = System.Drawing.Color.DarkRed;
            climbing_energy_plus_up.Color = System.Drawing.Color.MediumBlue;
            climbing_energy_plus_up.Legend = "Legend1";
            climbing_energy_plus_up.Name = "登坂抵抗消費分(力行)[kW]";
            climbing_energy_plus_up.XValueMember = "time";
            climbing_energy_plus_up.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            climbing_energy_plus_up.YValueMembers = "CLIMBING_ENERGY_PLUS_UP";

            climbing_energy_plus_down.ChartArea = "ChartArea1";
            climbing_energy_plus_down.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            climbing_energy_plus_down.Color = System.Drawing.Color.LightSkyBlue;
            climbing_energy_plus_down.Color = System.Drawing.Color.SkyBlue;
            climbing_energy_plus_down.Legend = "Legend1";
            climbing_energy_plus_down.Name = "登坂抵抗回生分(力行)[kW]";
            climbing_energy_plus_down.XValueMember = "time";
            climbing_energy_plus_down.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            climbing_energy_plus_down.YValueMembers = "CLIMBING_ENERGY_PLUS_DOWN";

            climbing_energy_minus_up.ChartArea = "ChartArea1";
            climbing_energy_minus_up.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            climbing_energy_minus_up.Color = System.Drawing.Color.DarkGray;
            climbing_energy_minus_up.Color = System.Drawing.Color.MediumBlue;
            climbing_energy_minus_up.Legend = "Legend1";
            climbing_energy_minus_up.Name = "登坂抵抗消費分(回生)[kW]";
            climbing_energy_minus_up.XValueMember = "time";
            climbing_energy_minus_up.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            climbing_energy_minus_up.YValueMembers = "CLIMBING_ENERGY_MINUS_UP";

            climbing_energy_minus_down.ChartArea = "ChartArea1";
            climbing_energy_minus_down.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            climbing_energy_minus_down.Color = System.Drawing.Color.LightSkyBlue;
            climbing_energy_minus_down.Color = System.Drawing.Color.RoyalBlue;
            climbing_energy_minus_down.Legend = "Legend1";
            climbing_energy_minus_down.Name = "登坂抵抗回生分(回生)[kW]";
            climbing_energy_minus_down.XValueMember = "time";
            climbing_energy_minus_down.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            climbing_energy_minus_down.YValueMembers = "CLIMBING_ENERGY_MINUS_DOWN";
            #endregion

            #region 変換ロス
            convert_loss_plus.ChartArea = "ChartArea1";
            convert_loss_plus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            convert_loss_plus.Color = System.Drawing.Color.Red;
            convert_loss_plus.Legend = "Legend1";
            convert_loss_plus.Name = "エネルギー変換ロス(力行)[kW]";
            convert_loss_plus.XValueMember = "time";
            convert_loss_plus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            convert_loss_plus.YValueMembers = "CONVERT_LOSS_PLUS";

            convert_loss_minus.ChartArea = "ChartArea1";
            convert_loss_minus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            convert_loss_minus.Color = System.Drawing.Color.Red;
            convert_loss_minus.Legend = "Legend1";
            convert_loss_minus.Name = "エネルギー変換ロス(回生)[kW]";
            convert_loss_minus.XValueMember = "time";
            convert_loss_minus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            convert_loss_minus.YValueMembers = "CONVERT_LOSS_MINUS";
            #endregion

            #region 回生ロス
            regene_loss.ChartArea = "ChartArea1";
            regene_loss.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            regene_loss.Color = System.Drawing.Color.Orchid;
            regene_loss.Legend = "Legend1";
            regene_loss.Name = "回生ロス[kW]";
            regene_loss.XValueMember = "time";
            regene_loss.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            regene_loss.YValueMembers = "REGENE_LOSS";
            #endregion

            #region 回生エネルギー
            regene_energy.ChartArea = "ChartArea1";
            regene_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            regene_energy.Color = System.Drawing.Color.LimeGreen;
            regene_energy.Legend = "Legend1";
            regene_energy.Name = "回生エネルギー[kW]";
            regene_energy.XValueMember = "time";
            regene_energy.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            regene_energy.YValueMembers = "REGENE_ENERGY";
            #endregion

            #region LEAFSPY
            power_ls.ChartArea = "ChartArea2";
            power_ls.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            power_ls.Color = System.Drawing.Color.Red;
            power_ls.Name = "Power";
            power_ls.XValueMember = "time";
            power_ls.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            power_ls.YValueMembers = "CONSUMED_ELECTRIC_ENERGY";
            #endregion

            #region エアコン
            air_con.ChartArea = "ChartArea3";
            air_con.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            air_con.Color = System.Drawing.Color.Red;
            air_con.Name = "AIR_CONDITIONER";
            air_con.XValueMember = "time";
            air_con.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            air_con.YValueMembers = "CONSUMED_AIR_CONDITIONING_ENERGY";
            #endregion

            //#region GIDs
            //gids.ChartArea = "ChartArea2";
            //gids.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            //gids.Color = System.Drawing.Color.Red;
            //gids.Name = "GIDs";
            //gids.XValueMember = "time";
            //gids.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            //gids.YValueMembers = "GIDs";
            //#endregion

            //#region SoC
            //soc.ChartArea = "ChartArea3";
            //soc.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            //soc.Color = System.Drawing.Color.Red;
            //soc.Name = "SoC";
            //soc.XValueMember = "time";
            //soc.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            //soc.YValueMembers = "SoC";
            //#endregion

            //#region ガソリン
            //speed.ChartArea = "ChartArea1";
            //speed.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //speed.Color = System.Drawing.Color.Red;
            //speed.Legend = "Legend1";
            //speed.Name = "ガソリン[kW]";
            //speed.XValueMember = "time";
            //speed.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            //speed.YValueMembers = "CONSUMED_FUEL_ENERGY";
            //speed.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            //#endregion

            //#region 速度
            //speed.ChartArea = "ChartArea1";
            //speed.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //speed.Color = System.Drawing.Color.Red;
            //speed.Legend = "Legend1";
            //speed.Name = "速度[km/h]";
            //speed.XValueMember = "time";
            //speed.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            //speed.YValueMembers = "SPEED";
            //speed.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            //#endregion

            //#region 速度
            //acc_x.ChartArea = "ChartArea1";
            //acc_x.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //acc_x.Color = System.Drawing.Color.Red;
            //acc_x.Legend = "Legend1";
            //acc_x.Name = "加速度[m/s^2]";
            //acc_x.XValueMember = "time";
            //acc_x.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            //acc_x.YValueMembers = "LONGITUDINAL_ACC";
            //acc_x.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            //#endregion
            #endregion

            #region Seriesに追加
            Timechart.Series.Add(air_energy_plus);
            Timechart.Series.Add(air_energy_minus);
            Timechart.Series.Add(rolling_energy_plus);
            Timechart.Series.Add(rolling_energy_minus);
            Timechart.Series.Add(convert_loss_plus);
            Timechart.Series.Add(climbing_energy_plus_up);
            Timechart.Series.Add(climbing_energy_plus_down);
            Timechart.Series.Add(acc_energy);
            Timechart.Series.Add(regene_loss);
            Timechart.Series.Add(convert_loss_minus);
            Timechart.Series.Add(climbing_energy_minus_up);
            Timechart.Series.Add(climbing_energy_minus_down);
            Timechart.Series.Add(regene_energy);
            //Timechart.Series.Add(speed);

            //GIDSchart.Series.Add(gids);
            //SOCchart.Series.Add(soc);


            GIDSchart.Series.Add(power_ls);
            SOCchart.Series.Add(air_con);
            #endregion

            #region データ取得
            string query = "select ECOLOG.JST, CONVERT(nchar(8), ECOLOG.JST, 108) as time, SPEED, CONSUMED_ELECTRIC_ENERGY*3600 as CONSUMED_ELECTRIC_ENERGY, LOST_ENERGY*3600 as LOST_ENERGY,  ";
            query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY >= 0) THEN ENERGY_BY_AIR_RESISTANCE*3600 ELSE 0 END) as AIR_ENERGY_PLUS,  ";
            query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY < 0) THEN ENERGY_BY_AIR_RESISTANCE*3600 ELSE 0 END) as AIR_ENERGY_MINUS,  ";
            query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY >= 0) THEN ENERGY_BY_ROLLING_RESISTANCE*3600 ELSE 0 END) as ROLLING_ENERGY_PLUS, ";
            query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY < 0) THEN ENERGY_BY_ROLLING_RESISTANCE*3600 ELSE 0 END) as ROLLING_ENERGY_MINUS, ";
            query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY >= 0 and ENERGY_BY_CLIMBING_RESISTANCE > 0) THEN ENERGY_BY_CLIMBING_RESISTANCE*3600 ELSE 0 END) as CLIMBING_ENERGY_PLUS_UP, 0 as CLIMBING_ENERGY_PLUS_DOWN, ";
            query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY < 0 and ENERGY_BY_CLIMBING_RESISTANCE > 0) THEN ENERGY_BY_CLIMBING_RESISTANCE*3600 ELSE 0 END) as CLIMBING_ENERGY_MINUS_UP, 0 as CLIMBING_ENERGY_MINUS_DOWN, ";
            query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY >= 0) THEN ENERGY_BY_ACC_RESISTANCE*3600 ELSE 0 END) as ACC_ENERGY_PLUS, 0 as ACC_ENERGY_MINUS,  ";
            query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY >= 0) THEN CONVERT_LOSS*3600 ELSE 0 END) as CONVERT_LOSS_PLUS,  ";
            query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY < 0) THEN CONVERT_LOSS*3600 ELSE 0 END) as CONVERT_LOSS_MINUS,  ";
            query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY < 0) THEN REGENE_LOSS*3600 ELSE 0 END) as REGENE_LOSS,  ";
            query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY < 0) THEN REGENE_ENERGY*3600 ELSE 0 END) as REGENE_ENERGY ";
            query += "from [ECOLOGTable] as ECOLOG ";
            query += "where ECOLOG.TRIP_ID = " + user.tripID + " ";
            query += "order by ECOLOG.JST ";

            if (user.usefixed)
            {
                query = query.Replace("[ECOLOGTable]", "ECOLOG_ALTITUDE_FIXED");
            }

            query = query.Replace("[ECOLOGTable]", MainForm.ECOLOGTable);

            dt_chart = DatabaseAccess.GetResult(query);


            query = "select ECOLOG.JST, CONVERT(nchar(8), LEAF.DATETIME, 108) as time, LEAF.CONSUMED_ELECTRIC_ENERGY_BY_SOC*3600 as CONSUMED_ELECTRIC_ENERGY  ";
            query += "from LEAFSPY_EQUALLY_DIVIDED_AIR_CONDITION as LEAF ";
            query += "right join [ECOLOGTable] as ECOLOG ";
            query += "on LEAF.TRIP_ID = ECOLOG.TRIP_ID ";
            query += "and LEAF.DATETIME = ECOLOG.JST ";
            query += "where LEAF.TRIP_ID = " + user.tripID + " ";
            query += "order by ECOLOG.JST ";

            query = query.Replace("[ECOLOGTable]", MainForm.ECOLOGTable);

            dt_chart_leafspy = DatabaseAccess.GetResult(query);


            query = "select ECOLOG.JST, CONVERT(nchar(8), LEAF.CAPTURED_TIME, 108) as time, CONSUMED_AIR_CONDITIONING_ENERGY  ";
            query += "from LEAF_MONITOR as LEAF ";
            query += "right join [ECOLOGTable] as ECOLOG ";
            query += "on LEAF.TRIP_ID = ECOLOG.TRIP_ID ";
            query += "and LEAF.CAPTURED_TIME = ECOLOG.JST ";
            query += "where LEAF.TRIP_ID = " + user.tripID + " ";
            query += "order by ECOLOG.JST ";

            query = query.Replace("[ECOLOGTable]", MainForm.ECOLOGTable);

            dt_soc = DatabaseAccess.GetResult(query);
            #endregion

            #region データの穴埋め
            DataTable dt_work = new DataTable();

            dt_work = dt_chart.Clone();

            DateTime start = DateTime.Parse(dt_chart.Rows[0][0].ToString()).AddSeconds(-29);
            DateTime end = DateTime.Parse(dt_chart.Rows[dt_chart.Rows.Count - 1][0].ToString()).AddSeconds(30);
            DateTime now = start;

            // i:dt_chart用 j;dt_work用
            int i = 0, j = 0;

            while (now >= start && now <= end)
            {
                dt_work.Rows.Add();

                if (now < DateTime.Parse(dt_chart.Rows[0][0].ToString()) || now > DateTime.Parse(dt_chart.Rows[dt_chart.Rows.Count - 1][0].ToString()))
                {
                    // トリップの範囲外データ
                    dt_work.Rows[j][0] = now;
                    dt_work.Rows[j][1] = now.ToLongTimeString();

                    for (int k = 2; k <= 18; k++)
                    {
                        dt_work.Rows[j][k] = 0;
                    }
                }
                else if (now == DateTime.Parse(dt_chart.Rows[i][0].ToString()))
                {
                    // この秒のデータがECOLOGにある場合
                    for (int k = 0; k < dt_chart.Columns.Count; k++)
                    {
                        dt_work.Rows[j][k] = dt_chart.Rows[i][k];
                    }
                    i++;
                }
                else
                {
                    // ない場合
                    dt_work.Rows[j][0] = now;
                    dt_work.Rows[j][1] = now.ToLongTimeString();

                    for (int k = 2; k <= 18; k++)
                    {
                        dt_work.Rows[j][k] = 0;
                    }
                }

                now = now.AddSeconds(1);
                j++;
            }

            dt_chart = new DataTable();
            dt_chart = dt_work.Copy();
            #endregion
        }

        private void PaintChart(DateTime time, bool move)
        {
            try
            {

                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                string start = time.AddSeconds(-29).ToString();
                string end = time.AddSeconds(30).ToString();

                DataTable dt = dt_chart.Clone();
                DataRow r = null;

                #region パワーグラフ
                foreach (DataRow dtRow in dt_chart.Select("JST >= '" + start + "' and JST <= '" + end + "' "))
                {
                    r = dt.NewRow();
                    for (int n = 0; n < dtRow.ItemArray.Length; n++)
                    {
                        r[n] = dtRow[n];
                    }
                    dt.Rows.Add(r);
                }

                Timechart.DataSource = dt;

                Timechart.Invalidate();
                #endregion

                try
                {
                    int diff = int.Parse(TimeDiff_textBox.Text);
                    time = time.AddSeconds(diff);
                }
                catch
                {

                }

                #region LEAFSPY
                start = time.AddSeconds(-29).ToString();
                end = time.AddSeconds(30).ToString();

                DataTable dt2 = new DataTable();

                dt2 = dt_chart_leafspy.Clone();

                foreach (DataRow dtRow2 in dt_chart_leafspy.Select("JST >= '" + start + "' and JST <= '" + end + "' "))
                {
                    r = dt2.NewRow();
                    for (int n = 0; n < dtRow2.ItemArray.Length; n++)
                    {
                        r[n] = dtRow2[n];
                    }
                    dt2.Rows.Add(r);
                }

                if (dt2.Rows.Count > 0)
                {
                    GIDSchart.DataSource = dt2;

                    GIDSchart.Invalidate();
                }

                foreach (DataRow dtRow in dt_chart_leafspy.Select("JST = '" + time + "' "))
                {
                    if (dtRow["CONSUMED_ELECTRIC_ENERGY"] != DBNull.Value)
                    {
                        GIDslabel.Text = Math.Round(double.Parse(dtRow["CONSUMED_ELECTRIC_ENERGY"].ToString()), 5).ToString();
                    }
                    else
                    {
                        GIDslabel.Text = "null";
                    }
                }
                #endregion

                #region エアコン
                start = time.AddSeconds(-29).ToString();
                end = time.AddSeconds(30).ToString();

                DataTable dt3 = new DataTable();

                dt3 = dt_soc.Clone();

                foreach (DataRow dtRow3 in dt_soc.Select("JST >= '" + start + "' and JST <= '" + end + "' "))
                {
                    r = dt3.NewRow();
                    for (int n = 0; n < dtRow3.ItemArray.Length; n++)
                    {
                        r[n] = dtRow3[n];
                    }
                    dt3.Rows.Add(r);
                }

                if (dt3.Rows.Count > 0)
                {
                    SOCchart.DataSource = dt3;

                    SOCchart.Invalidate();
                }

                foreach (DataRow dtRow in dt_soc.Select("JST = '" + time + "' "))
                {
                    if (dtRow["CONSUMED_AIR_CONDITIONING_ENERGY"] != DBNull.Value)
                    {
                        SoClabel.Text = Math.Round(double.Parse(dtRow["CONSUMED_AIR_CONDITIONING_ENERGY"].ToString()), 5).ToString();
                    }
                    else
                    {
                        SoClabel.Text = "null";
                    }
                }
                #endregion

                if (GIDslabel.Text != "null" && SoClabel.Text != "null")
                {
                    LEAF_ENERGY_label.Text = Math.Round((double.Parse(GIDslabel.Text) - double.Parse(SoClabel.Text)), 5).ToString();
                }
                else
                {
                    LEAF_ENERGY_label.Text = "null";
                }

                //#region LEAFSPY
                //start = time.AddSeconds(-149).ToString();
                //end = time.AddSeconds(150).ToString();

                //DataTable dt2 = new DataTable();

                //dt2 = dt_chart_leafspy.Clone();

                //foreach (DataRow dtRow2 in dt_chart_leafspy.Select("JST >= '" + start + "' and JST <= '" + end + "' "))
                //{
                //    r = dt2.NewRow();
                //    for (int n = 0; n < dtRow2.ItemArray.Length; n++)
                //    {
                //        r[n] = dtRow2[n];
                //    }
                //    dt2.Rows.Add(r);
                //}

                //if (dt2.Rows.Count > 0)
                //{
                //    GIDSchart.DataSource = dt2;

                //    GIDSchart.Invalidate();

                //    SOCchart.DataSource = dt2;

                //    SOCchart.Invalidate();
                //}

                //foreach (DataRow dtRow in dt_soc.Select("JST = '" + time + "' "))
                //{
                //    if (dtRow["GIDs"] != DBNull.Value)
                //    {
                //        GIDslabel.Text = dtRow["GIDs"].ToString();
                //    }
                //    else
                //    {
                //        GIDslabel.Text = "null";
                //    }

                //    if (dtRow["SoC"] != DBNull.Value)
                //    {
                //        SoClabel.Text = Math.Round(double.Parse(dtRow["SoC"].ToString()), 1).ToString();
                //    }
                //    else
                //    {
                //        SoClabel.Text = "null";
                //    }
                //}
                //#endregion

                //DataTable dt3 = new DataTable();

                //dt3 = dt_soc.Clone();

                //foreach (DataRow dtRow in dt_soc.Select("JST >= '" + start + "' and JST <= '" + end + "' "))
                //{
                //    r = dt3.NewRow();
                //    for (int n = 0; n < dtRow.ItemArray.Length; n++)
                //    {
                //        r[n] = dtRow[n];
                //    }
                //    dt3.Rows.Add(r);
                //}

                //if (dt3.Rows.Count > 0)
                //{
                //    SOCchart.DataSource = dt3;

                //    SOCchart.Invalidate();
                //}

                System.Windows.Forms.Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erorr");
            }
        }
        #endregion

        #region イベント検知
        private void Controllerbutton_Click(object sender, EventArgs e)
        {
            if (!ctrlShowed)
            {
                ctrlShowed = true;
                ctrl = new ChartControler(this, user);
                MainForm.ShowWindow(ctrl);
            }
        }
        private void Slider_Scroll(object sender, EventArgs e)
        {
            if (dt_picture.Rows.Count > 3)
            {
                LabelCurrent.Text = Slider.Value.ToString();
                LoadImage(int.Parse(LabelCurrent.Text) - 1);

                setGMapCenter(Latitude, Longitude);
                PaintChart(selected_time, move_link);
            }
        }
        private void TimetextBox_Enter(object sender, EventArgs e)
        {
            TimetextBox.SelectAll();
        }
        /// <summary>
        /// Google Map上でマーカーがクリックされた時の処理
        /// </summary>
        /// <param name="t">クリックされたマーカー時点のJST</param>
        /// <param name="x">クリックされたマーカー時点のLongitudinal Acc</param>
        /// <param name="y">クリックされたマーカー時点のLateral Acc</param>
        /// <param name="z">クリックされたマーカー時点のVertical Acc</param>
        /// <param name="s">クリックされたマーカー時点のSpeed</param>
        /// <param name="e">クリックされたマーカー時点のConsumed Electric Energy</param>
        /// <param name="l">クリックされたマーカー時点のLost Energy</param>
        public void IconClick(string t, double x, double y, double z, double s, double e, double l)
        {
            //selected_time = DateTime.Parse(t);
            //TimetextBox.Text = selected_time.ToString("yyyy/MM/dd HH:mm:ss");
            //labelSpeed.Text = s.ToString("f1");
            //labelLongitudinal_ACC.Text = x.ToString("f3");
            //labelConsumedEnergy.Text = e.ToString("f5");
            //labelLoss.Text = l.ToString("f5");
        }
        /// <summary>
        /// Google Map上でマーカーが右クリックされた時の処理
        /// </summary>
        /// <param name="t">クリックされたマーカーのJST</param>
        public void IconRightClick(string t)
        {
            MainForm.main.TopMost = true;

            selected_time = DateTime.Parse(t);
            ClickedcontextMenuStrip.Show(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
            MainForm.main.TopMost = false;
        }
        private void Browser_FormClosed(object sender, FormClosedEventArgs e)
        {
            ctrl.Dispose();
        }
        #endregion

        #region 右クリックメニュー
        private void SetStartTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartTime = selected_time;
            ctrl.setStartTime(StartTime);
        }
        private void SetEndTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EndTime = selected_time;
            ctrl.setEndTime(EndTime);
        }
        private void DisplayImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;

            for (i = 0; i < dt_picture.Rows.Count - 1; i++)
            {
                if (dt_picture.Rows[i]["JST"].ToString() == selected_time.ToString())
                {
                    LabelCurrent.Text = i.ToString();
                    Slider.Value = i + 1;
                    LoadImage(i);
                    setGMapCenter(Latitude, Longitude);

                    this.Slider.Focus();

                }
            }

            PaintChart(selected_time, move_link);

        }
        #endregion

    }
}
