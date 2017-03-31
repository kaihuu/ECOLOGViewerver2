using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ECOLOGViewerver2
{
    /// <summary>
    /// セマンティックリンクに関する情報を表示する画面を取り扱うクラス
    /// </summary>
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class SemanticLinkBrowser : Form
    {
        #region 変数定義

        private FormData user;
        private string query = "";
        private ChartArea chartArea1;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="u">表示するセマンティックリンクの情報</param>
        public SemanticLinkBrowser(FormData u)
        {
            InitializeComponent();
            user = new FormData(u);
            webBrowser.ObjectForScripting = this;

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

            this.SemanticLinkIDlabel.Text = user.semanticLinkID;
            this.Semanticslabel.Text = user.semantics;
            //this.DriverIDlabel.Text = user.driver_id;
            //this.CarIDlabel.Text = user.car_id;
            //this.Directionlabel.Text = user.direction;
            this.Focus();

            webBrowser.Navigate(user.currentFile);

            initCombobox();

            chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);

            //makeQuery();

            //PaintChart();
        }

        private void initCombobox()
        {
            ChartcomboBox.Items.Clear();
            AxisXcomboBox.Items.Clear();
            AxisYcomboBox.Items.Clear();

            ChartcomboBox.Items.Add("Scattergram");
            ChartcomboBox.Items.Add("Histogram");
            ChartcomboBox.Items.Add("Columngraph");

            if (user.semanticLinkID == "4")
            {
                ChartcomboBox.Items.Add("Pattern DEMO");
                ChartcomboBox.Items.Add("W2W Compare DEMO");
            }

            if (user.semanticLinkID == "124" || user.semanticLinkID == "125")
            {
                ChartcomboBox.Items.Add("House Moving DEMO");
            }

            AxisXcomboBox.Items.Add("LOST_ENERGY");
            AxisXcomboBox.Items.Add("AVG_SPEED");
            AxisXcomboBox.Items.Add("TRANSIT_TIME");
            AxisXcomboBox.Items.Add("HOUR");
            AxisXcomboBox.Items.Add("REGENE_ENERGY_PERCENT");
            AxisXcomboBox.Items.Add("REGENE_LOSS_PERCENT");
            AxisXcomboBox.Items.Add("LOST_ENERGY_BY_WELL_TO_WHEEL");
            AxisXcomboBox.Items.Add("CONSUMED_FUEL_BY_WELL_TO_WHEEL");

            AxisYcomboBox.Items.Add("LOST_ENERGY");
            AxisYcomboBox.Items.Add("AVG_SPEED");
            AxisYcomboBox.Items.Add("TRANSIT_TIME");
            AxisYcomboBox.Items.Add("REGENE_ENERGY_PERCENT");
            AxisYcomboBox.Items.Add("REGENE_LOSS_PERCENT");
            AxisYcomboBox.Items.Add("LOST_ENERGY_BY_WELL_TO_WHEEL");
            AxisYcomboBox.Items.Add("CONSUMED_FUEL_BY_WELL_TO_WHEEL");
        }

        private void PaintChart()
        {
            DataTable dt = new DataTable();

            #region 軸の最大値の設定
            try
            {
                chartArea1.AxisY.Maximum = double.Parse(MaxofYtextBox.Text);
            }
            catch (Exception)
            {
                chartArea1.AxisY.Maximum = double.NaN;
            }

            try
            {
                chartArea1.AxisX.Maximum = double.Parse(MaxofXtextBox.Text);
            }
            catch (Exception)
            {
                chartArea1.AxisX.Maximum = double.NaN;
            }
            #endregion

            #region 凡例の設定
            if (Legend_checkBox.Checked)
            {
                this.chart.Legends.Clear();
                System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
                legend1.Name = "Legend1";
                this.chart.Legends.Add(legend1);
            }
            else
            {
                this.chart.Legends.Clear();
            }
            #endregion

            #region 共通設定
            chartArea1.AxisX.TitleFont = new Font(chartArea1.AxisX.TitleFont.Name, 15F, chartArea1.AxisX.TitleFont.Style);
            chartArea1.AxisY.TitleFont = new Font(chartArea1.AxisY.TitleFont.Name, 15F, chartArea1.AxisY.TitleFont.Style);


            chartArea1.AxisX.LabelAutoFitMaxFontSize = 15;
            chartArea1.AxisY.LabelAutoFitMaxFontSize = 15;
            #endregion

            if (LeadSpy_checkBox.Checked)
            {
                #region 正解データとの比較
                query = "with TripLinks as ( \r\n";
                query += "select DISTINCT ECOLOG.TRIP_ID, ECOLOG.LINK_ID \r\n";
                query += "from [ECOLOGTable] as ECOLOG, ( \r\n";
                query += "	select DISTINCT LINK_ID \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                query += "	) SEMANTIC_LINKS \r\n";
                query += "where ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                if (!user.direction.Equals("All"))
                {
                    query += "	and ECOLOG.TRIP_DIRECTION = '" + user.direction + "' \r\n";
                }
                if (!user.driverID.Equals("All"))
                {
                    query += "	and ECOLOG.DRIVER_ID = " + user.driverID + " \r\n";
                }
                if (!user.carID.Equals("All"))
                {
                    query += "	and ECOLOG.CAR_ID = " + user.carID + " \r\n";
                }
                if (TORQUEData_checkBox.Checked)
                {
                    query += "	and ECOLOG.CONSUMED_FUEL is not null \r\n";
                }
                query += "), Compare as ( \r\n";
                query += "select TRIP_ID, count(*) as Number_of_Links \r\n";
                query += "from TripLinks, ( \r\n";
                query += "	select LINK_ID \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " ) SEMANTIC_LINKS \r\n";
                query += "where TripLinks.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "group by TRIP_ID \r\n";
                query += "having count(*) / 0.5 >= ( \r\n";
                query += "	select count(*) as Number_of_Links \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " ) \r\n";
                //query += ") \r\n";

                query += "), LEAFSPY_SOC as ( \r\n";
                query += "select START_SoC.TRIP_ID, ABS(END_SoC.GIDs - START_SoC.GIDs)*0.075 as LOST_ENERGY_SoC ";
                //query += "select START_SoC.TRIP_ID, ABS(END_SoC.SOC - START_SoC.SOC)*(281*0.075)/(10000) as LOST_ENERGY_SoC ";
                query += "from ( ";
                query += "	select LEAFSPY.TRIP_ID, GIDs, START_TIME ";
                //query += "	select LEAFSPY.TRIP_ID, SoC, START_TIME ";
                query += "	from LEAFSPY, ( ";
                query += "		select LEAFSPY.TRIP_ID, MIN(LEAFSPY.JST) as START_TIME ";
                query += "		from LEAFSPY, [ECOLOGTable] as ECOLOG, SEMANTIC_LINKS ";
                query += "		where LEAFSPY.TRIP_ID = ECOLOG.TRIP_ID ";
                query += "		and LEAFSPY.JST = ECOLOG.JST ";
                query += "		and ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID ";
                query += "      and SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                if (!user.direction.Equals("All"))
                {
                    query += "	and ECOLOG.TRIP_DIRECTION = '" + user.direction + "' \r\n";
                }
                if (!user.driverID.Equals("All"))
                {
                    query += "	and ECOLOG.DRIVER_ID = " + user.driverID + " \r\n";
                }
                if (!user.carID.Equals("All"))
                {
                    query += "	and ECOLOG.CAR_ID = " + user.carID + " \r\n";
                }
                query += "		group by LEAFSPY.TRIP_ID ";
                query += "		) LEAFSPY_TIME ";
                query += "	where LEAFSPY.TRIP_ID = LEAFSPY_TIME.TRIP_ID ";
                query += "	and LEAFSPY.JST = START_TIME ";
                query += "	) START_SoC, ( ";
                query += "	select LEAFSPY.TRIP_ID, GIDs, END_TIME ";
                //query += "	select LEAFSPY.TRIP_ID, SoC, END_TIME ";
                query += "	from LEAFSPY, ( ";
                query += "		select LEAFSPY.TRIP_ID, MAX(LEAFSPY.JST) as END_TIME ";
                query += "		from LEAFSPY, [ECOLOGTable] as ECOLOG, SEMANTIC_LINKS ";
                query += "		where LEAFSPY.TRIP_ID = ECOLOG.TRIP_ID ";
                query += "		and LEAFSPY.JST = ECOLOG.JST ";
                query += "		and ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID ";
                query += "      and SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                if (!user.direction.Equals("All"))
                {
                    query += "	and ECOLOG.TRIP_DIRECTION = '" + user.direction + "' \r\n";
                }
                if (!user.driverID.Equals("All"))
                {
                    query += "	and ECOLOG.DRIVER_ID = " + user.driverID + " \r\n";
                }
                if (!user.carID.Equals("All"))
                {
                    query += "	and ECOLOG.CAR_ID = " + user.carID + " \r\n";
                }
                query += "		group by LEAFSPY.TRIP_ID ";
                query += "		) LEAFSPY_TIME ";
                query += "	where LEAFSPY.TRIP_ID = LEAFSPY_TIME.TRIP_ID ";
                query += "	and LEAFSPY.JST = END_TIME ";
                query += "	) END_SoC ";
                query += "where START_SoC.TRIP_ID = END_SoC.TRIP_ID ";
                query += "and START_SoC.START_TIME != END_SoC.END_TIME ";
                query += ") \r\n";

                query += "	select ECOLOG.TRIP_ID, CONVERT(nchar(5), MIN(ECOLOG.JST), 114) as Hour, ROUND(AVG(SPEED),1) as AVG_SPEED, SUM(LOST_ENERGY) as SUM_LOST_ENERGY, SUM(DISTANCE_DIFFERENCE) as SUM_DIFFERENCE, count(*) as SUM_TIME, SUM(LOST_ENERGY_BY_WELL_TO_WHEEL) as SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, SUM(CONSUMED_FUEL_BY_WELL_TO_WHEEL) as SUM_FUEL_BY_WELL_TO_WHEEL, AVG(LOST_ENERGY_SoC) as LOST_ENERGY_SoC \r\n";
                query += "	from [ECOLOGTable] as ECOLOG, SEMANTIC_LINKS, Compare, LEAFSPY_SOC \r\n";
                query += "  where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                query += "	and ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "	and ECOLOG.TRIP_ID = Compare.TRIP_ID \r\n";
                query += "	and ECOLOG.TRIP_ID = LEAFSPY_SOC.TRIP_ID \r\n";
                if (!user.direction.Equals("All"))
                {
                    query += "	and ECOLOG.TRIP_DIRECTION = '" + user.direction + "' \r\n";
                }
                if (!user.driverID.Equals("All"))
                {
                    query += "	and ECOLOG.DRIVER_ID = " + user.driverID + " \r\n";
                }
                if (!user.carID.Equals("All"))
                {
                    query += "	and ECOLOG.CAR_ID = " + user.carID + " \r\n";
                }
                if (TORQUEData_checkBox.Checked)
                {
                    query += "	and ECOLOG.CONSUMED_FUEL is not null \r\n";
                }
                query += "	group by ECOLOG.TRIP_ID \r\n";

                if (QueryEdit_checkBox.Checked)
                {
                    QueryView form = new QueryView(query);

                    form.ShowDialog(this);

                    if (form.DialogResult == DialogResult.OK)
                    {
                        query = form.GetQuery();
                    }
                }

                query = query.Replace("[ECOLOGTable]", ECOLOGTable_textBox.Text);

                dt = DatabaseAccess.GetResult(query);


                System.Windows.Forms.DataVisualization.Charting.Series semantic_link = new System.Windows.Forms.DataVisualization.Charting.Series();

                semantic_link.ChartArea = "ChartArea1";
                semantic_link.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                semantic_link.Name = user.semantics;
                semantic_link.MarkerColor = Color.Red;
                semantic_link.YValuesPerPoint = 7;
                semantic_link.ToolTip = " TRIP_ID:#VALY2";

                #region X軸
                semantic_link.XValueMember = "LOST_ENERGY_SoC";
                chartArea1.AxisX.Title = "Lost Energy Senced by CAN[kWh]";
                chartArea1.AxisX.Minimum = 0;
                #endregion

                #region Y軸
                semantic_link.YValueMembers = "SUM_LOST_ENERGY, TRIP_ID";
                chartArea1.AxisY.Title = "Lost Energy Estimated by ECOLOG[kWh]";
                chartArea1.AxisY.Minimum = 0;
                #endregion

                chart.Series.Add(semantic_link);

                chart.DataSource = dt;
                #endregion
            }
            else if (ChartcomboBox.SelectedItem.ToString() == "Scattergram")
            {
                #region Scattergram
                dt = DatabaseAccess.GetResult(query);

                System.Windows.Forms.DataVisualization.Charting.Series semantic_link = new System.Windows.Forms.DataVisualization.Charting.Series();

                semantic_link.ChartArea = "ChartArea1";
                semantic_link.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                semantic_link.Name = user.semantics;
                semantic_link.MarkerColor = Color.Red;
                semantic_link.YValuesPerPoint = 7;
                semantic_link.ToolTip = " TRIP_ID:#VALY2";

                #region X軸
                if (AxisXcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                {
                    semantic_link.XValueMember = "AVG_SPEED";
                    chartArea1.AxisX.Title = "Average of Speed[km/h]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                {
                    semantic_link.XValueMember = "SUM_LOST_ENERGY";
                    chartArea1.AxisX.Title = "Lost Energy[kWh]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "HOUR")
                {
                    semantic_link.XValueMember = "Hour";
                    chartArea1.AxisX.Title = "Hour";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                {
                    semantic_link.XValueMember = "SUM_TIME";
                    chartArea1.AxisX.Title = "Transit Time[sec]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY_BY_WELL_TO_WHEEL")
                {
                    semantic_link.XValueMember = "SUM_LOST_ENERGY_BY_WELL_TO_WHEEL";
                    chartArea1.AxisX.Title = "Lost Energy by Well-to-Wheel[MJ]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "CONSUMED_FUEL_BY_WELL_TO_WHEEL")
                {
                    semantic_link.XValueMember = "SUM_FUEL_BY_WELL_TO_WHEEL";
                    chartArea1.AxisX.Title = "Consumed Fuel by Well-to-Wheel[MJ]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "REGENE_ENERGY_PERCENT")
                {
                    semantic_link.XValueMember = "SUM_REGENE_ENERGY_PERCENT";
                    chartArea1.AxisX.Title = "Percent of Regene-Energy[%]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "REGENE_LOSS_PERCENT")
                {
                    semantic_link.XValueMember = "SUM_REGENE_LOSS_PERCENT";
                    chartArea1.AxisX.Title = "Percent of Regene-Loss[%]";
                    chartArea1.AxisX.Minimum = 0;
                }
                #endregion

                #region Y軸
                if (AxisYcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                {
                    semantic_link.YValueMembers = "AVG_SPEED, TRIP_ID";
                    chartArea1.AxisY.Title = "Average of Speed[km/h]";
                    chartArea1.AxisY.Minimum = 0;
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                {
                    semantic_link.YValueMembers = "SUM_LOST_ENERGY, TRIP_ID";
                    chartArea1.AxisY.Title = "Lost Energy[kWh]";
                    chartArea1.AxisY.Minimum = 0;
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                {
                    semantic_link.YValueMembers = "SUM_TIME, TRIP_ID";
                    chartArea1.AxisY.Title = "Transit Time[sec]";
                    chartArea1.AxisY.Minimum = 0;
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "LOST_ENERGY_BY_WELL_TO_WHEEL")
                {
                    semantic_link.YValueMembers = "SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, TRIP_ID";
                    chartArea1.AxisY.Title = "Lost Energy by Well-to-Wheel[MJ]";
                    chartArea1.AxisY.Minimum = 0;
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "CONSUMED_FUEL_BY_WELL_TO_WHEEL")
                {
                    semantic_link.YValueMembers = "SUM_FUEL_BY_WELL_TO_WHEEL, TRIP_ID";
                    chartArea1.AxisY.Title = "Consumed Fuel by Well-to-Wheel[MJ]";
                    chartArea1.AxisY.Minimum = 0;
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "REGENE_ENERGY_PERCENT")
                {
                    semantic_link.YValueMembers = "SUM_REGENE_ENERGY_PERCENT, TRIP_ID";
                    chartArea1.AxisY.Title = "Percent of Regene-Energy[%]";
                    chartArea1.AxisY.Minimum = 0;
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "REGENE_LOSS_PERCENT")
                {
                    semantic_link.YValueMembers = "SUM_REGENE_LOSS_PERCENT, TRIP_ID";
                    chartArea1.AxisY.Title = "Percent of Regene-Loss[%]";
                    chartArea1.AxisY.Minimum = 0;
                }
                #endregion

                chart.Series.Add(semantic_link);

                chart.DataSource = dt;
                #endregion
            }
            else if (ChartcomboBox.SelectedItem.ToString() == "Histogram")
            {
                #region Histogram
                dt = DatabaseAccess.GetResult(query);

                System.Windows.Forms.DataVisualization.Charting.Series semantic_link = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series Series_EV = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series Series_ICV = new System.Windows.Forms.DataVisualization.Charting.Series();

                semantic_link.ChartArea = "ChartArea1";
                semantic_link.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                semantic_link.Name = user.semantics;
                semantic_link.MarkerColor = Color.Red;
                semantic_link.CustomProperties = "PointWidth=1";

                Series_EV.ChartArea = "ChartArea1";
                Series_ICV.ChartArea = "ChartArea1";
                Series_EV.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                Series_ICV.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                Series_EV.Name = "EV";
                Series_ICV.Name = "ICV";
                Series_EV.MarkerColor = Color.Red;
                Series_ICV.MarkerColor = Color.Red;
                Series_EV.CustomProperties = "PointWidth=1";
                Series_ICV.CustomProperties = "PointWidth=1";

                #region X軸
                if (AxisXcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                {
                    semantic_link.XValueMember = "AVG_SPEED";
                    semantic_link.ToolTip = " AVG_SPEED:#VALX\n Probability:#VALY";
                    chartArea1.AxisX.Title = "Average Speed[km/h]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                {
                    semantic_link.XValueMember = "AVG_LOST_ENERGY";
                    semantic_link.ToolTip = " AVG_LOST_ENERGY:#VALX\n Probability:#VALY";
                    chartArea1.AxisX.Title = "Average Lost Energy[kWh]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "HOUR")
                {
                    semantic_link.XValueMember = "Hour";
                    semantic_link.ToolTip = " Hour:#VALX\n Probability:#VALY";
                    chartArea1.AxisX.Title = "Hour";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                {
                    semantic_link.XValueMember = "AVG_TIME";
                    semantic_link.ToolTip = " Transit Time:#VALX\n Probability:#VALY";
                    chartArea1.AxisX.Title = "Transit Time[sec]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY_BY_WELL_TO_WHEEL")
                {
                    semantic_link.XValueMember = "AVG_LOST_ENERGY_BY_WELL_TO_WHEEL";
                    semantic_link.ToolTip = " LOST_ENERGY_BY_WELL_TO_WHEEL:#VALX\n Probability:#VALY";
                    chartArea1.AxisX.Title = "Average Lost Energy by Well-to-Wheel[MJ]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "CONSUMED_FUEL_BY_WELL_TO_WHEEL")
                {
                    semantic_link.XValueMember = "AVG_FUEL_BY_WELL_TO_WHEEL";
                    semantic_link.ToolTip = " CONSUMED_FUEL_BY_WELL_TO_WHEEL:#VALX\n Probability:#VALY";
                    chartArea1.AxisX.Title = "Average Consumed Fuel by Well-to-Wheel[MJ]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "ENERGY_BY_W2W")
                {
                    Series_EV.XValueMember = "AxisX";
                    Series_ICV.XValueMember = "AxisX";
                    chartArea1.AxisX.Title = "Lost Energy by Well-to-Wheel[MJ]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "REGENE_ENERGY_PERCENT")
                {
                    semantic_link.XValueMember = "AVG_REGENE_ENERGY_PERCENT";
                    chartArea1.AxisX.Title = "Percent of Regene-Energy[%]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "REGENE_LOSS_PERCENT")
                {
                    semantic_link.XValueMember = "AVG_REGENE_LOSS_PERCENT";
                    chartArea1.AxisX.Title = "Percent of Regene-Loss[%]";
                    chartArea1.AxisX.Minimum = 0;
                }
                #endregion

                #region Y軸
                semantic_link.YValueMembers = "number";

                Series_EV.YValueMembers = "number_EV";
                Series_ICV.YValueMembers = "number_ICV";

                chartArea1.AxisY.Title = "Probability[%]";
                //chartArea1.AxisY.Title = "Probability[n]";
                chartArea1.AxisY.Minimum = 0;
                #endregion

                if (AxisXcomboBox.SelectedItem.ToString() == "ENERGY_BY_W2W")
                {
                    chart.Series.Add(Series_EV);
                    chart.Series.Add(Series_ICV);
                }
                else
                {
                    chart.Series.Add(semantic_link);
                }

                chart.DataSource = dt;
                #endregion
            }
            else if (ChartcomboBox.SelectedItem.ToString() == "Columngraph")
            {
                #region Columngraph
                dt = DatabaseAccess.GetResult(query);

                System.Windows.Forms.DataVisualization.Charting.Series semantic_link = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series air_loss = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series rolling_loss = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series convert_loss = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series regene_loss = new System.Windows.Forms.DataVisualization.Charting.Series();

                semantic_link.ChartArea = "ChartArea1";
                semantic_link.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                semantic_link.Name = user.semantics;
                semantic_link.Color = Color.Red;

                air_loss.ChartArea = "ChartArea1";
                air_loss.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                air_loss.Name = "AIR_LOSS";
                air_loss.Color = Color.Yellow;

                rolling_loss.ChartArea = "ChartArea1";
                rolling_loss.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                rolling_loss.Name = "ROLLING_LOSS";
                rolling_loss.Color = Color.SandyBrown;

                convert_loss.ChartArea = "ChartArea1";
                convert_loss.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                convert_loss.Name = "CONVERT_LOSS";
                convert_loss.Color = Color.Red;

                regene_loss.ChartArea = "ChartArea1";
                regene_loss.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                regene_loss.Name = "REGENE_LOSS";
                regene_loss.Color = Color.Orchid;

                #region X軸
                if (AxisXcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                {
                    semantic_link.XValueMember = "AVG_SPEED";
                    air_loss.XValueMember = "AVG_SPEED";
                    rolling_loss.XValueMember = "AVG_SPEED";
                    convert_loss.XValueMember = "AVG_SPEED";
                    regene_loss.XValueMember = "AVG_SPEED";

                    chartArea1.AxisX.Title = "Average Speed[km/h]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                {
                    semantic_link.XValueMember = "AVG_LOST_ENERGY";
                    air_loss.XValueMember = "AVG_LOST_ENERGY";
                    rolling_loss.XValueMember = "AVG_LOST_ENERGY";
                    convert_loss.XValueMember = "AVG_LOST_ENERGY";
                    regene_loss.XValueMember = "AVG_LOST_ENERGY";

                    chartArea1.AxisX.Title = "Average Lost Energy[kWh]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "HOUR")
                {
                    semantic_link.XValueMember = "Hour";
                    air_loss.XValueMember = "Hour";
                    rolling_loss.XValueMember = "Hour";
                    convert_loss.XValueMember = "Hour";
                    regene_loss.XValueMember = "Hour";

                    chartArea1.AxisX.Title = "Hour";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                {
                    semantic_link.XValueMember = "AVG_TIME";
                    air_loss.XValueMember = "AVG_TIME";
                    rolling_loss.XValueMember = "AVG_TIME";
                    convert_loss.XValueMember = "AVG_TIME";
                    regene_loss.XValueMember = "AVG_TIME";

                    chartArea1.AxisX.Title = "Average Transit Time[sec]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY_BY_WELL_TO_WHEEL")
                {
                    semantic_link.XValueMember = "AVG_LOST_ENERGY_BY_WELL_TO_WHEEL";
                    air_loss.XValueMember = "AVG_LOST_ENERGY_BY_WELL_TO_WHEEL";
                    rolling_loss.XValueMember = "AVG_LOST_ENERGY_BY_WELL_TO_WHEEL";
                    convert_loss.XValueMember = "AVG_LOST_ENERGY_BY_WELL_TO_WHEEL";
                    regene_loss.XValueMember = "AVG_LOST_ENERGY_BY_WELL_TO_WHEEL";

                    chartArea1.AxisX.Title = "Average Lost Energy by Well-to-Wheel[MJ]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "CONSUMED_FUEL_BY_WELL_TO_WHEEL")
                {
                    semantic_link.XValueMember = "AVG_FUEL_BY_WELL_TO_WHEEL";
                    air_loss.XValueMember = "AVG_FUEL_BY_WELL_TO_WHEEL";
                    rolling_loss.XValueMember = "AVG_FUEL_BY_WELL_TO_WHEEL";
                    convert_loss.XValueMember = "AVG_FUEL_BY_WELL_TO_WHEEL";
                    regene_loss.XValueMember = "AVG_FUEL_BY_WELL_TO_WHEEL";

                    chartArea1.AxisX.Title = "Average Consumed Fuel by Well-to-Wheel[MJ]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "YEAR")
                {
                    semantic_link.XValueMember = "YearMonth";
                    air_loss.XValueMember = "YearMonth";
                    rolling_loss.XValueMember = "YearMonth";
                    convert_loss.XValueMember = "YearMonth";
                    regene_loss.XValueMember = "YearMonth";

                    chartArea1.AxisX.Title = "Month";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "REGENE_ENERGY_PERCENT")
                {
                    semantic_link.XValueMember = "AVG_REGENE_ENERGY_PERCENT";
                    air_loss.XValueMember = "AVG_REGENE_ENERGY_PERCENT";
                    rolling_loss.XValueMember = "AVG_REGENE_ENERGY_PERCENT";
                    convert_loss.XValueMember = "AVG_REGENE_ENERGY_PERCENT";
                    regene_loss.XValueMember = "AVG_REGENE_ENERGY_PERCENT";

                    chartArea1.AxisX.Title = "Percent of Regene-Energy[%]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "REGENE_LOSS_PERCENT")
                {
                    semantic_link.XValueMember = "AVG_REGENE_LOSS_PERCENT";
                    air_loss.XValueMember = "AVG_REGENE_LOSS_PERCENT";
                    rolling_loss.XValueMember = "AVG_REGENE_LOSS_PERCENT";
                    convert_loss.XValueMember = "AVG_REGENE_LOSS_PERCENT";
                    regene_loss.XValueMember = "AVG_REGENE_LOSS_PERCENT";

                    chartArea1.AxisX.Title = "Percent of Regene-Loss[%]";
                    chartArea1.AxisX.Minimum = 0;
                }
                #endregion

                #region Y軸
                if (AxisYcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                {
                    semantic_link.YValueMembers = "AVG_SPEED";
                    chartArea1.AxisY.Title = "Average Speed[km/h]";
                    chartArea1.AxisY.Minimum = 0;

                    chart.Series.Add(semantic_link);
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                {
                    chartArea1.AxisY.Title = "Average Lost Energy[kWh]";
                    chartArea1.AxisY.Minimum = 0;

                    air_loss.YValueMembers = "AVG_AIR_LOSS";
                    rolling_loss.YValueMembers = "AVG_ROLLING_LOSS";
                    convert_loss.YValueMembers = "AVG_CONVERT_LOSS";
                    regene_loss.YValueMembers = "AVG_REGENE_LOSS";

                    chart.Series.Add(air_loss);
                    chart.Series.Add(rolling_loss);
                    chart.Series.Add(convert_loss);
                    chart.Series.Add(regene_loss);
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                {
                    semantic_link.YValueMembers = "AVG_TIME";
                    chartArea1.AxisY.Title = "Average Transit Time[sec]";
                    chartArea1.AxisY.Minimum = 0;

                    chart.Series.Add(semantic_link);
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "LOST_ENERGY_BY_WELL_TO_WHEEL")
                {
                    semantic_link.YValueMembers = "AVG_LOST_ENERGY_BY_WELL_TO_WHEEL";
                    chartArea1.AxisY.Title = "Average Lost Energy by Well-to-Wheel[MJ]";
                    chartArea1.AxisY.Minimum = 0;

                    chart.Series.Add(semantic_link);
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "CONSUMED_FUEL_BY_WELL_TO_WHEEL")
                {
                    semantic_link.YValueMembers = "AVG_FUEL_BY_WELL_TO_WHEEL";
                    chartArea1.AxisY.Title = "Average Consumed Fuel by Well-to-Wheel[MJ]";
                    chartArea1.AxisY.Minimum = 0;

                    chart.Series.Add(semantic_link);
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "REGENE_ENERGY_PERCENT")
                {
                    semantic_link.YValueMembers = "AVG_REGENE_ENERGY_PERCENT";
                    chartArea1.AxisY.Title = "Percent of Regene-Energy[%]";
                    chartArea1.AxisY.Minimum = 0;

                    chart.Series.Add(semantic_link);
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "REGENE_LOSS_PERCENT")
                {
                    semantic_link.YValueMembers = "AVG_REGENE_LOSS_PERCENT";
                    chartArea1.AxisY.Title = "Percent of Regene-Loss[%]";
                    chartArea1.AxisY.Minimum = 0;

                    chart.Series.Add(semantic_link);
                }
                #endregion

                chart.DataSource = dt;
                #endregion
            }
            else if (ChartcomboBox.SelectedItem.ToString() == "W2W Compare DEMO")
            {
                #region ITS世界会議用
                #region クエリ
                string query = "with BLUE_EV as ( \r\n";
                query += "select ROUND(AVG(SubTable.SUM_LOST_ENERGY_BY_WELL_TO_WHEEL), 1) as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, count(*) as number_BLUE_EV \r\n";
                query += "from ( \r\n";
                query += "	select ECOLOG.TRIP_ID, SUM(LOST_ENERGY_BY_WELL_TO_WHEEL) as SUM_LOST_ENERGY_BY_WELL_TO_WHEEL \r\n";
                query += "	from [ECOLOGTable] as ECOLOG \r\n";
                query += "	right join ( \r\n";
                query += "		select LINK_ID \r\n";
                query += "		from SEMANTIC_LINKS \r\n";
                query += "		where SEMANTIC_LINK_ID = 4 \r\n";
                query += "		) SEMANTIC_LINKS \r\n";
                query += "	on ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "	right join ( \r\n";
                query += "		select TRIP_ID \r\n";
                query += "		from ANNOTATION \r\n";
                query += "		where EVENT_ID = 23 \r\n";
                query += "		) ANNOTATION \r\n";
                query += "	on ECOLOG.TRIP_ID = ANNOTATION.TRIP_ID \r\n";
                query += "	and TRIP_DIRECTION = 'homeward' \r\n";
                query += "	and DRIVER_ID = 1 \r\n";
                query += "	and CONSUMED_FUEL is not null \r\n";
                query += "	group by ECOLOG.TRIP_ID ) SubTable \r\n";
                query += "where SUM_LOST_ENERGY_BY_WELL_TO_WHEEL is not NULL \r\n";
                query += "group by ROUND(SubTable.SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, 1) \r\n";
                query += "), BLUE_ICV as ( \r\n";
                query += "select ROUND(AVG(SubTable.SUM_FUEL_BY_WELL_TO_WHEEL), 1) as AVG_FUEL_BY_WELL_TO_WHEEL, count(*) as number_BLUE_ICV \r\n";
                query += "from ( \r\n";
                query += "	select ECOLOG.TRIP_ID, SUM(CONSUMED_FUEL_BY_WELL_TO_WHEEL) as SUM_FUEL_BY_WELL_TO_WHEEL \r\n";
                query += "	from [ECOLOGTable] as ECOLOG \r\n";
                query += "	right join ( \r\n";
                query += "		select LINK_ID \r\n";
                query += "		from SEMANTIC_LINKS \r\n";
                query += "		where SEMANTIC_LINK_ID = 4 \r\n";
                query += "		) SEMANTIC_LINKS \r\n";
                query += "	on ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "	right join ( \r\n";
                query += "		select TRIP_ID \r\n";
                query += "		from ANNOTATION \r\n";
                query += "		where EVENT_ID = 23 \r\n";
                query += "		) ANNOTATION \r\n";
                query += "	on ECOLOG.TRIP_ID = ANNOTATION.TRIP_ID \r\n";
                query += "	and TRIP_DIRECTION = 'homeward' \r\n";
                query += "	and DRIVER_ID = 1 \r\n";
                query += "	and CONSUMED_FUEL is not null \r\n";
                query += "	group by ECOLOG.TRIP_ID ) SubTable \r\n";
                query += "where SUM_FUEL_BY_WELL_TO_WHEEL is not NULL \r\n";
                query += "group by ROUND(SubTable.SUM_FUEL_BY_WELL_TO_WHEEL, 1) \r\n";
                query += "), BLUE as ( \r\n";
                query += "select AVG_LOST_ENERGY_BY_WELL_TO_WHEEL as AxisX, number_BLUE_EV, number_BLUE_ICV \r\n";
                query += "from BLUE_EV \r\n";
                query += "left join BLUE_ICV \r\n";
                query += "on BLUE_EV.AVG_LOST_ENERGY_BY_WELL_TO_WHEEL = BLUE_ICV.AVG_FUEL_BY_WELL_TO_WHEEL \r\n";
                query += "union \r\n";
                query += "select AVG_FUEL_BY_WELL_TO_WHEEL as AxisX, number_BLUE_EV, number_BLUE_ICV \r\n";
                query += "from BLUE_ICV \r\n";
                query += "left join BLUE_EV \r\n";
                query += "on BLUE_EV.AVG_LOST_ENERGY_BY_WELL_TO_WHEEL = BLUE_ICV.AVG_FUEL_BY_WELL_TO_WHEEL \r\n";
                query += "), RED_EV as ( \r\n";
                query += "select ROUND(AVG(SubTable.SUM_LOST_ENERGY_BY_WELL_TO_WHEEL), 1) as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, count(*) as number_RED_EV \r\n";
                query += "from ( \r\n";
                query += "	select ECOLOG.TRIP_ID, SUM(LOST_ENERGY_BY_WELL_TO_WHEEL) as SUM_LOST_ENERGY_BY_WELL_TO_WHEEL \r\n";
                query += "	from [ECOLOGTable] as ECOLOG \r\n";
                query += "	right join ( \r\n";
                query += "		select LINK_ID \r\n";
                query += "		from SEMANTIC_LINKS \r\n";
                query += "		where SEMANTIC_LINK_ID = 4 \r\n";
                query += "		) SEMANTIC_LINKS \r\n";
                query += "	on ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "	right join ( \r\n";
                query += "		select TRIP_ID \r\n";
                query += "		from ANNOTATION \r\n";
                query += "		where EVENT_ID = 24 \r\n";
                query += "		) ANNOTATION \r\n";
                query += "	on ECOLOG.TRIP_ID = ANNOTATION.TRIP_ID \r\n";
                query += "	and TRIP_DIRECTION = 'homeward' \r\n";
                query += "	and DRIVER_ID = 1 \r\n";
                query += "	and CONSUMED_FUEL is not null \r\n";
                query += "	group by ECOLOG.TRIP_ID ) SubTable \r\n";
                query += "where SUM_LOST_ENERGY_BY_WELL_TO_WHEEL is not NULL \r\n";
                query += "group by ROUND(SubTable.SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, 1) \r\n";
                query += "), RED_ICV as ( \r\n";
                query += "select ROUND(AVG(SubTable.SUM_FUEL_BY_WELL_TO_WHEEL), 1) as AVG_FUEL_BY_WELL_TO_WHEEL, count(*) as number_RED_ICV \r\n";
                query += "from ( \r\n";
                query += "	select ECOLOG.TRIP_ID, SUM(CONSUMED_FUEL_BY_WELL_TO_WHEEL) as SUM_FUEL_BY_WELL_TO_WHEEL \r\n";
                query += "	from [ECOLOGTable] as ECOLOG \r\n";
                query += "	right join ( \r\n";
                query += "		select LINK_ID \r\n";
                query += "		from SEMANTIC_LINKS \r\n";
                query += "		where SEMANTIC_LINK_ID = 4 \r\n";
                query += "		) SEMANTIC_LINKS \r\n";
                query += "	on ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "	right join ( \r\n";
                query += "		select TRIP_ID \r\n";
                query += "		from ANNOTATION \r\n";
                query += "		where EVENT_ID = 24 \r\n";
                query += "		) ANNOTATION \r\n";
                query += "	on ECOLOG.TRIP_ID = ANNOTATION.TRIP_ID \r\n";
                query += "	and TRIP_DIRECTION = 'homeward' \r\n";
                query += "	and DRIVER_ID = 1 \r\n";
                query += "	and CONSUMED_FUEL is not null \r\n";
                query += "	group by ECOLOG.TRIP_ID ) SubTable \r\n";
                query += "where SUM_FUEL_BY_WELL_TO_WHEEL is not NULL \r\n";
                query += "group by ROUND(SubTable.SUM_FUEL_BY_WELL_TO_WHEEL, 1) \r\n";
                query += "), RED as ( \r\n";
                query += "select AVG_LOST_ENERGY_BY_WELL_TO_WHEEL as AxisX, number_RED_EV, number_RED_ICV \r\n";
                query += "from RED_EV \r\n";
                query += "left join RED_ICV \r\n";
                query += "on RED_EV.AVG_LOST_ENERGY_BY_WELL_TO_WHEEL = RED_ICV.AVG_FUEL_BY_WELL_TO_WHEEL \r\n";
                query += "union \r\n";
                query += "select AVG_FUEL_BY_WELL_TO_WHEEL as AxisX, number_RED_EV, number_RED_ICV \r\n";
                query += "from RED_ICV \r\n";
                query += "left join RED_EV \r\n";
                query += "on RED_EV.AVG_LOST_ENERGY_BY_WELL_TO_WHEEL = RED_ICV.AVG_FUEL_BY_WELL_TO_WHEEL \r\n";
                query += "), YELLOW_EV as ( \r\n";
                query += "select ROUND(AVG(SubTable.SUM_LOST_ENERGY_BY_WELL_TO_WHEEL), 1) as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, count(*) as number_YELLOW_EV \r\n";
                query += "from ( \r\n";
                query += "	select ECOLOG.TRIP_ID, SUM(LOST_ENERGY_BY_WELL_TO_WHEEL) as SUM_LOST_ENERGY_BY_WELL_TO_WHEEL \r\n";
                query += "	from [ECOLOGTable] as ECOLOG \r\n";
                query += "	right join ( \r\n";
                query += "		select LINK_ID \r\n";
                query += "		from SEMANTIC_LINKS \r\n";
                query += "		where SEMANTIC_LINK_ID = 4 \r\n";
                query += "		) SEMANTIC_LINKS \r\n";
                query += "	on ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "	right join ( \r\n";
                query += "		select TRIP_ID \r\n";
                query += "		from ANNOTATION \r\n";
                query += "		where EVENT_ID = 25 \r\n";
                query += "		) ANNOTATION \r\n";
                query += "	on ECOLOG.TRIP_ID = ANNOTATION.TRIP_ID \r\n";
                query += "	and TRIP_DIRECTION = 'homeward' \r\n";
                query += "	and DRIVER_ID = 1 \r\n";
                query += "	and CONSUMED_FUEL is not null \r\n";
                query += "	group by ECOLOG.TRIP_ID ) SubTable \r\n";
                query += "where SUM_LOST_ENERGY_BY_WELL_TO_WHEEL is not NULL \r\n";
                query += "group by ROUND(SubTable.SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, 1) \r\n";
                query += "), YELLOW_ICV as ( \r\n";
                query += "select ROUND(AVG(SubTable.SUM_FUEL_BY_WELL_TO_WHEEL), 1) as AVG_FUEL_BY_WELL_TO_WHEEL, count(*) as number_YELLOW_ICV \r\n";
                query += "from ( \r\n";
                query += "	select ECOLOG.TRIP_ID, SUM(CONSUMED_FUEL_BY_WELL_TO_WHEEL) as SUM_FUEL_BY_WELL_TO_WHEEL \r\n";
                query += "	from [ECOLOGTable] as ECOLOG \r\n";
                query += "	right join ( \r\n";
                query += "		select LINK_ID \r\n";
                query += "		from SEMANTIC_LINKS \r\n";
                query += "		where SEMANTIC_LINK_ID = 4 \r\n";
                query += "		) SEMANTIC_LINKS \r\n";
                query += "	on ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "	right join ( \r\n";
                query += "		select TRIP_ID \r\n";
                query += "		from ANNOTATION \r\n";
                query += "		where EVENT_ID = 25 \r\n";
                query += "		) ANNOTATION \r\n";
                query += "	on ECOLOG.TRIP_ID = ANNOTATION.TRIP_ID \r\n";
                query += "	and TRIP_DIRECTION = 'homeward' \r\n";
                query += "	and DRIVER_ID = 1 \r\n";
                query += "	and CONSUMED_FUEL is not null \r\n";
                query += "	group by ECOLOG.TRIP_ID ) SubTable \r\n";
                query += "where SUM_FUEL_BY_WELL_TO_WHEEL is not NULL \r\n";
                query += "group by ROUND(SubTable.SUM_FUEL_BY_WELL_TO_WHEEL, 1) \r\n";
                query += "), YELLOW as ( \r\n";
                query += "select AVG_LOST_ENERGY_BY_WELL_TO_WHEEL as AxisX, number_YELLOW_EV, number_YELLOW_ICV \r\n";
                query += "from YELLOW_EV \r\n";
                query += "left join YELLOW_ICV \r\n";
                query += "on YELLOW_EV.AVG_LOST_ENERGY_BY_WELL_TO_WHEEL = YELLOW_ICV.AVG_FUEL_BY_WELL_TO_WHEEL \r\n";
                query += "union \r\n";
                query += "select AVG_FUEL_BY_WELL_TO_WHEEL as AxisX, number_YELLOW_EV, number_YELLOW_ICV \r\n";
                query += "from YELLOW_ICV \r\n";
                query += "left join YELLOW_EV \r\n";
                query += "on YELLOW_EV.AVG_LOST_ENERGY_BY_WELL_TO_WHEEL = YELLOW_ICV.AVG_FUEL_BY_WELL_TO_WHEEL \r\n";
                query += ") \r\n";
                query += "select BLUE.AxisX as AxisX, number_BLUE_EV*100/130.0 as AxisY_BLUE_EV, number_BLUE_ICV*100/50.0 as AxisY_BLUE_ICV, number_RED_EV*100/50.0 as AxisY_RED_EV, number_RED_ICV*100/50.0 as AxisY_RED_ICV, number_YELLOW_EV*100/50.0 as AxisY_YELLOW_EV, number_YELLOW_ICV*100/50.0 as AxisY_YELLOW_ICV \r\n";
                query += "from BLUE \r\n";
                query += "left join RED \r\n";
                query += "on BLUE.AxisX = RED.AxisX \r\n";
                query += "left join YELLOW \r\n";
                query += "on BLUE.AxisX = YELLOW.AxisX \r\n";
                query += "union \r\n";
                query += "select RED.AxisX as AxisX, null as AxisY_BLUE_EV, null as AxisY_BLUE_ICV, number_RED_EV*100/50.0 as AxisY_RED_EV, number_RED_ICV*100/50.0 as AxisY_RED_ICV, number_YELLOW_EV*100/50.0 as AxisY_YELLOW_EV, number_YELLOW_ICV*100/50.0 as AxisY_YELLOW_ICV \r\n";
                query += "from RED \r\n";
                query += "left join YELLOW \r\n";
                query += "on RED.AxisX = YELLOW.AxisX \r\n";
                query += "left join BLUE \r\n";
                query += "on BLUE.AxisX = RED.AxisX \r\n";
                query += "where BLUE.AxisX is null \r\n";
                query += "union \r\n";
                query += "select YELLOW.AxisX as AxisX, null as AxisY_BLUE_EV, null as AxisY_BLUE_ICV, null as AxisY_RED_EV, null as AxisY_RED_ICV, number_YELLOW_EV*100/50.0 as AxisY_YELLOW_EV, number_YELLOW_ICV*100/50.0 as AxisY_YELLOW_ICV \r\n";
                query += "from YELLOW \r\n";
                query += "full outer join BLUE \r\n";
                query += "on BLUE.AxisX = YELLOW.AxisX \r\n";
                query += "full outer join RED \r\n";
                query += "on RED.AxisX = YELLOW.AxisX \r\n";
                query += "where BLUE.AxisX is null \r\n";
                query += "and RED.AxisX is null \r\n";
                #endregion

                query = query.Replace("[ECOLOGTable]", ECOLOGTable_textBox.Text);

                dt = DatabaseAccess.GetResult(query);

                System.Windows.Forms.DataVisualization.Charting.Series semantic_link1 = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series semantic_link2 = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series semantic_link3 = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series semantic_link4 = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series semantic_link5 = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series semantic_link6 = new System.Windows.Forms.DataVisualization.Charting.Series();

                semantic_link1.ChartArea = "ChartArea1";
                semantic_link2.ChartArea = "ChartArea1";
                semantic_link3.ChartArea = "ChartArea1";
                semantic_link5.ChartArea = "ChartArea1";
                semantic_link4.ChartArea = "ChartArea1";
                semantic_link6.ChartArea = "ChartArea1";

                semantic_link1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                semantic_link2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                semantic_link3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                semantic_link4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                semantic_link5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                semantic_link6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;

                semantic_link1.Legend = "Legend1";
                semantic_link2.Legend = "Legend1";
                semantic_link3.Legend = "Legend1";
                semantic_link4.Legend = "Legend1";
                semantic_link5.Legend = "Legend1";
                semantic_link6.Legend = "Legend1";

                semantic_link1.Name = "Blue Pattern(EV)";
                semantic_link2.Name = "Red Pattern(EV)";
                semantic_link3.Name = "Other Pattern(EV)";
                semantic_link4.Name = "Blue Pattern(ICV)";
                semantic_link5.Name = "Red Pattern(ICV)";
                semantic_link6.Name = "Other Pattern(ICV)";

                semantic_link1.Color = Color.Blue;
                semantic_link2.Color = Color.Red;
                semantic_link3.Color = Color.Orange;
                semantic_link4.Color = Color.DarkBlue;
                semantic_link5.Color = Color.DarkRed;
                semantic_link6.Color = Color.DarkOrange;

                semantic_link1.CustomProperties = "PointWidth=1";
                semantic_link2.CustomProperties = "PointWidth=1";
                semantic_link3.CustomProperties = "PointWidth=1";
                semantic_link4.CustomProperties = "PointWidth=1";
                semantic_link5.CustomProperties = "PointWidth=1";
                semantic_link6.CustomProperties = "PointWidth=1";

                #region X軸
                semantic_link1.XValueMember = "AxisX";
                semantic_link2.XValueMember = "AxisX";
                semantic_link3.XValueMember = "AxisX";
                semantic_link4.XValueMember = "AxisX";
                semantic_link5.XValueMember = "AxisX";
                semantic_link6.XValueMember = "AxisX";

                //semantic_link1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                //semantic_link2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                //semantic_link3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                chartArea1.AxisX.Title = "Lost Energy by Well-to-Wheel[MJ]";
                chartArea1.AxisX.Minimum = 0;
                #endregion

                #region Y軸
                semantic_link1.YValueMembers = "AxisY_BLUE_EV";
                semantic_link2.YValueMembers = "AxisY_RED_EV";
                semantic_link3.YValueMembers = "AxisY_YELLOW_EV";
                semantic_link4.YValueMembers = "AxisY_BLUE_ICV";
                semantic_link5.YValueMembers = "AxisY_RED_ICV";
                semantic_link6.YValueMembers = "AxisY_YELLOW_ICV";

                chartArea1.AxisY.Title = "Probability[%]";
                chartArea1.AxisY.Minimum = 0;
                #endregion

                chart.Series.Add(semantic_link1);
                chart.Series.Add(semantic_link3);
                chart.Series.Add(semantic_link2);
                chart.Series.Add(semantic_link4);
                chart.Series.Add(semantic_link5);
                chart.Series.Add(semantic_link6);
                chart.DataSource = dt;
                chart.Invalidate();
                #endregion
            }
            else if (ChartcomboBox.SelectedItem.ToString() == "Pattern DEMO")
            {
                #region DEIM用
                //DataSet ds = new DataSet();
                #region クエリ
                query = "with BLUE as ( ";

                if (AxisXcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                {
                    query += "select AVG(ROUND(AVG_SPEED, 0)) as AVG_SPEED, count(*) as number_BLUE \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                {
                    query += "select ROUND(AVG(ROUND(SUM_LOST_ENERGY, 2)),2) as AVG_LOST_ENERGY, count(*) as number_BLUE \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "HOUR")
                {
                    query += "select Hour, count(*) as number_BLUE \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                {
                    query += "select AVG(ROUND(SUM_TIME/10, 0))*10 as AVG_TIME, count(*) as number_BLUE \r\n";
                }


                //query += "select ROUND(AVG(ROUND(SubTable.SUM_LOST_ENERGY, 2)), 2) as AVG_LOST_ENERGY, count(*) as number_BLUE \r\n";

                query += "from ( \r\n";
                query += "	select ECOLOG.TRIP_ID, case when DATENAME(hour, MIN(ECOLOG.JST)) <= 6 then CONVERT(nchar(2), DATEADD(hour, 24, MIN(ECOLOG.JST)), 114) else CONVERT(nchar(2), MIN(ECOLOG.JST), 114) end as Hour, AVG(SPEED) as AVG_SPEED, SUM(CONSUMED_ELECTRIC_ENERGY) as SUM_ENERGY, SUM(LOST_ENERGY) as SUM_LOST_ENERGY, SUM(DISTANCE_DIFFERENCE) as SUM_DIFFERENCE, count(*) as SUM_TIME \r\n";
                query += "	from [ECOLOGTable] as ECOLOG \r\n";
                //query += "	right join ( \r\n";
                //query += "		select TRIP_ID, TRIP_DIRECTION \r\n";
                //query += "		from TRIPS \r\n";
                //query += "		where TRIP_ID in (1401,1395,1385,1365,1361,1353,1344,1333,1329,1305,1237,1231,1222,1206,1200,1178,1170,1152,1109,1099,1093,1081,1071,1067,1063,1037,1034,1036,985,975,951,949,961,898,894,892,886,882,855,851,849,847,738,736,733,730,726) \r\n";
                //query += "		) TRIPS \r\n";
                //query += "	on ECOLOG.TRIP_ID = TRIPS.TRIP_ID \r\n";
                query += "	right join ( \r\n";
                query += "		select TRIP_ID \r\n";
                query += "		from ANNOTATION \r\n";
                query += "		where EVENT_ID = 23 \r\n";
                query += "		) ANNOTATION \r\n";
                query += "	on ECOLOG.TRIP_ID = ANNOTATION.TRIP_ID \r\n";
                //query += "	right join  ( \r\n";
                //query += "		select DRIVER_ID, DRIVER_SENSOR_ID \r\n";
                //query += "		from DRIVER_SENSOR \r\n";
                //query += "		where DRIVER_ID = 1) DRIVER_SENSOR \r\n";
                //query += "	on ECOLOG.DRIVER_SENSOR_ID = DRIVER_SENSOR.DRIVER_SENSOR_ID \r\n";
                query += "	right join ( \r\n";
                query += "		select LINK_ID \r\n";
                query += "		from SEMANTIC_LINKS \r\n";
                query += "		where SEMANTIC_LINK_ID = 4 \r\n";
                query += "		) SubSubTable \r\n";
                query += "	on ECOLOG.LINK_ID = SubSubTable.LINK_ID \r\n";
                query += "  where DRIVER_ID = 1 \r\n";
                query += "	group by ECOLOG.TRIP_ID ) SubTable \r\n";
                //query += "group by ROUND(SubTable.SUM_LOST_ENERGY, 2) \r\n";
                query += "group by [Aggregation] \r\n";
                query += "), RED as ( \r\n";

                //query += "select ROUND(AVG(ROUND(SubTable.SUM_LOST_ENERGY, 2)), 2) as AVG_LOST_ENERGY, count(*) as number_RED \r\n";

                if (AxisXcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                {
                    query += "select AVG(ROUND(AVG_SPEED, 0)) as AVG_SPEED, count(*) as number_RED \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                {
                    query += "select ROUND(AVG(ROUND(SUM_LOST_ENERGY, 2)),2) as AVG_LOST_ENERGY, count(*) as number_RED \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "HOUR")
                {
                    query += "select Hour, count(*) as number_RED \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                {
                    query += "select AVG(ROUND(SUM_TIME/10, 0))*10 as AVG_TIME, count(*) as number_RED \r\n";
                }

                query += "from ( \r\n";
                query += "	select ECOLOG.TRIP_ID, case when DATENAME(hour, MIN(ECOLOG.JST)) <= 6 then CONVERT(nchar(2), DATEADD(hour, 24, MIN(ECOLOG.JST)), 114) else CONVERT(nchar(2), MIN(ECOLOG.JST), 114) end as Hour, AVG(SPEED) as AVG_SPEED, SUM(CONSUMED_ELECTRIC_ENERGY) as SUM_ENERGY, SUM(LOST_ENERGY) as SUM_LOST_ENERGY, SUM(DISTANCE_DIFFERENCE) as SUM_DIFFERENCE, count(*) as SUM_TIME \r\n";
                query += "	from [ECOLOGTable] as ECOLOG \r\n";
                //query += "	right join ( \r\n";
                //query += "		select TRIP_ID, TRIP_DIRECTION \r\n";
                //query += "		from TRIPS \r\n";
                //query += "		where TRIP_ID in (1431,1425,1357,1323,1315,1267,1182,1145,1142,1135,1115,1091,1069,1061,1043,1023,991,884,880,878,836,729) \r\n";
                //query += "		) TRIPS \r\n";
                //query += "	on ECOLOG.TRIP_ID = TRIPS.TRIP_ID \r\n";
                query += "	right join ( \r\n";
                query += "		select TRIP_ID \r\n";
                query += "		from ANNOTATION \r\n";
                query += "		where EVENT_ID = 24 \r\n";
                query += "		) ANNOTATION \r\n";
                query += "	on ECOLOG.TRIP_ID = ANNOTATION.TRIP_ID \r\n";
                //query += "	right join  ( \r\n";
                //query += "		select DRIVER_ID, DRIVER_SENSOR_ID \r\n";
                //query += "		from DRIVER_SENSOR \r\n";
                //query += "		where DRIVER_ID = 1) DRIVER_SENSOR \r\n";
                //query += "	on ECOLOG.DRIVER_SENSOR_ID = DRIVER_SENSOR.DRIVER_SENSOR_ID \r\n";
                query += "	right join ( \r\n";
                query += "		select LINK_ID \r\n";
                query += "		from SEMANTIC_LINKS \r\n";
                query += "		where SEMANTIC_LINK_ID = 4 \r\n";
                query += "		) SubSubTable \r\n";
                query += "	on ECOLOG.LINK_ID = SubSubTable.LINK_ID \r\n";
                query += "  where DRIVER_ID = 1 \r\n";
                query += "	group by ECOLOG.TRIP_ID ) SubTable \r\n";
                //query += "group by ROUND(SubTable.SUM_LOST_ENERGY, 2) \r\n";
                query += "group by [Aggregation] \r\n";
                query += "), YELLOW as ( \r\n";

                //query += "select ROUND(AVG(ROUND(SubTable.SUM_LOST_ENERGY, 2)), 2) as AVG_LOST_ENERGY, count(*) as number_YELLOW \r\n";

                if (AxisXcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                {
                    query += "select AVG(ROUND(AVG_SPEED, 0)) as AVG_SPEED, count(*) as number_YELLOW \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                {
                    query += "select ROUND(AVG(ROUND(SUM_LOST_ENERGY, 2)),2) as AVG_LOST_ENERGY, count(*) as number_YELLOW \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "HOUR")
                {
                    query += "select Hour, count(*) as number_YELLOW \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                {
                    query += "select AVG(ROUND(SUM_TIME/10, 0))*10 as AVG_TIME, count(*) as number_YELLOW \r\n";
                }

                query += "from ( \r\n";
                query += "	select ECOLOG.TRIP_ID, case when DATENAME(hour, MIN(ECOLOG.JST)) <= 6 then CONVERT(nchar(2), DATEADD(hour, 24, MIN(ECOLOG.JST)), 114) else CONVERT(nchar(2), MIN(ECOLOG.JST), 114) end as Hour, AVG(SPEED) as AVG_SPEED, SUM(CONSUMED_ELECTRIC_ENERGY) as SUM_ENERGY, SUM(LOST_ENERGY) as SUM_LOST_ENERGY, SUM(DISTANCE_DIFFERENCE) as SUM_DIFFERENCE, count(*) as SUM_TIME \r\n";
                query += "	from [ECOLOGTable] as ECOLOG \r\n";
                //query += "	right join ( \r\n";
                //query += "		select TRIP_ID, TRIP_DIRECTION \r\n";
                //query += "		from TRIPS \r\n";
                //query += "		where TRIP_ID in (1453,1446,1399,1391,1387,1367,1363,1359,1351,1349,1345,1342,1340,1331,1283,1275,1269,1255,1301,1239,1235,1226,1214,1199,1195,1176,1168,1160,1154,1150,1148,1137,1131,1129,1119,1107,1096,1087,1077,1059,1021,1006,973,953,909,957,890,888,873,871,863,853,843,834,832,830,748,746,750,819,735) \r\n";
                //query += "		) TRIPS \r\n";
                //query += "	on ECOLOG.TRIP_ID = TRIPS.TRIP_ID \r\n";
                query += "	right join ( \r\n";
                query += "		select TRIP_ID \r\n";
                query += "		from ANNOTATION \r\n";
                query += "		where EVENT_ID = 25 \r\n";
                query += "		) ANNOTATION \r\n";
                query += "	on ECOLOG.TRIP_ID = ANNOTATION.TRIP_ID \r\n";
                //query += "	right join  ( \r\n";
                //query += "		select DRIVER_ID, DRIVER_SENSOR_ID \r\n";
                //query += "		from DRIVER_SENSOR \r\n";
                //query += "		where DRIVER_ID = 1) DRIVER_SENSOR \r\n";
                //query += "	on ECOLOG.DRIVER_SENSOR_ID = DRIVER_SENSOR.DRIVER_SENSOR_ID \r\n";
                query += "	right join ( \r\n";
                query += "		select LINK_ID \r\n";
                query += "		from SEMANTIC_LINKS \r\n";
                query += "		where SEMANTIC_LINK_ID = 4 \r\n";
                query += "		) SubSubTable \r\n";
                query += "	on ECOLOG.LINK_ID = SubSubTable.LINK_ID \r\n";
                query += "  where DRIVER_ID = 1 \r\n";
                query += "	group by ECOLOG.TRIP_ID ) SubTable \r\n";
                //query += "group by ROUND(SubTable.SUM_LOST_ENERGY, 2) \r\n";
                query += "group by [Aggregation] \r\n";
                query += ") \r\n";


                if (AxisXcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                {
                    query += "select BLUE.AVG_SPEED as AxisX, number_BLUE/1.3 as AxisY_BLUE, number_RED/1.3 as AxisY_RED, number_YELLOW/1.3 as AxisY_YELLOW \r\n";
                    query += "from BLUE \r\n";
                    query += "left join RED \r\n";
                    query += "on BLUE.AVG_SPEED = RED.AVG_SPEED \r\n";
                    query += "left join YELLOW \r\n";
                    query += "on BLUE.AVG_SPEED = YELLOW.AVG_SPEED \r\n";

                    query += "union \r\n";

                    query += "select RED.AVG_SPEED as AxisX, null as AxisY_BLUE, number_RED/1.3 as AxisY_RED, number_YELLOW/1.3 as AxisY_YELLOW \r\n";
                    query += "from RED \r\n";
                    query += "left join YELLOW \r\n";
                    query += "on RED.AVG_SPEED = YELLOW.AVG_SPEED \r\n";
                    query += "left join BLUE \r\n";
                    query += "on BLUE.AVG_SPEED = RED.AVG_SPEED \r\n";
                    query += "where number_BLUE is null \r\n";

                    query += "union \r\n";

                    query += "select YELLOW.AVG_SPEED as AxisX, null as AxisY_BLUE, null as AxisY_RED, number_YELLOW/1.3 as AxisY_YELLOW \r\n";
                    query += "from YELLOW \r\n";
                    query += "full outer join BLUE \r\n";
                    query += "on BLUE.AVG_SPEED = YELLOW.AVG_SPEED \r\n";
                    query += "full outer join RED \r\n";
                    query += "on RED.AVG_SPEED = YELLOW.AVG_SPEED \r\n";
                    query += "where number_BLUE is null \r\n";
                    query += "and number_RED is null \r\n";
                    query += " \r\n";
                    query = query.Replace("[Aggregation]", "ROUND(AVG_SPEED, 0)");
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                {
                    query += "select BLUE.AVG_LOST_ENERGY as AxisX, number_BLUE/1.3 as AxisY_BLUE, number_RED/1.3 as AxisY_RED, number_YELLOW/1.3 as AxisY_YELLOW \r\n";
                    query += "from BLUE \r\n";
                    query += "left join RED \r\n";
                    query += "on BLUE.AVG_LOST_ENERGY = RED.AVG_LOST_ENERGY \r\n";
                    query += "left join YELLOW \r\n";
                    query += "on BLUE.AVG_LOST_ENERGY = YELLOW.AVG_LOST_ENERGY \r\n";

                    query += "union \r\n";

                    query += "select RED.AVG_LOST_ENERGY as AxisX, null as AxisY_BLUE, number_RED/1.3 as AxisY_RED, number_YELLOW/1.3 as AxisY_YELLOW \r\n";
                    query += "from RED \r\n";
                    query += "left join YELLOW \r\n";
                    query += "on RED.AVG_LOST_ENERGY = YELLOW.AVG_LOST_ENERGY \r\n";
                    query += "left join BLUE \r\n";
                    query += "on BLUE.AVG_LOST_ENERGY = RED.AVG_LOST_ENERGY \r\n";
                    query += "where number_BLUE is null \r\n";

                    query += "union \r\n";

                    query += "select YELLOW.AVG_LOST_ENERGY as AxisX, null as AxisY_BLUE, null as AxisY_RED, number_YELLOW/1.3 as AxisY_YELLOW \r\n";
                    query += "from YELLOW \r\n";
                    query += "full outer join BLUE \r\n";
                    query += "on BLUE.AVG_LOST_ENERGY = YELLOW.AVG_LOST_ENERGY \r\n";
                    query += "full outer join RED \r\n";
                    query += "on RED.AVG_LOST_ENERGY = YELLOW.AVG_LOST_ENERGY \r\n";
                    query += "where number_BLUE is null \r\n";
                    query += "and number_RED is null \r\n";
                    query += " \r\n";
                    query = query.Replace("[Aggregation]", "ROUND(SUM_LOST_ENERGY, 2)");
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "HOUR")
                {
                    query += "select BLUE.Hour as AxisX, number_BLUE/1.3 as AxisY_BLUE, number_RED/1.3 as AxisY_RED, number_YELLOW/1.3 as AxisY_YELLOW \r\n";
                    query += "from BLUE \r\n";
                    query += "left join RED \r\n";
                    query += "on BLUE.Hour = RED.Hour \r\n";
                    query += "left join YELLOW \r\n";
                    query += "on BLUE.Hour = YELLOW.Hour \r\n";

                    query += "union \r\n";

                    query += "select RED.Hour as AxisX, null as AxisY_BLUE, number_RED/1.3 as AxisY_RED, number_YELLOW/1.3 as AxisY_YELLOW \r\n";
                    query += "from RED \r\n";
                    query += "left join YELLOW \r\n";
                    query += "on RED.Hour = YELLOW.Hour \r\n";
                    query += "left join BLUE \r\n";
                    query += "on BLUE.Hour = RED.Hour \r\n";
                    query += "where number_BLUE is null \r\n";

                    query += "union \r\n";

                    query += "select YELLOW.Hour as AxisX, null as AxisY_BLUE, null as AxisY_RED, number_YELLOW/1.3 as AxisY_YELLOW \r\n";
                    query += "from YELLOW \r\n";
                    query += "full outer join BLUE \r\n";
                    query += "on BLUE.Hour = YELLOW.Hour \r\n";
                    query += "full outer join RED \r\n";
                    query += "on RED.Hour = YELLOW.Hour \r\n";
                    query += "where number_BLUE is null \r\n";
                    query += "and number_RED is null \r\n";
                    query += " \r\n";
                    query = query.Replace("[Aggregation]", "Hour");
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                {
                    query += "select BLUE.AVG_TIME as AxisX, number_BLUE/1.3 as AxisY_BLUE, number_RED/1.3 as AxisY_RED, number_YELLOW/1.3 as AxisY_YELLOW \r\n";
                    query += "from BLUE \r\n";
                    query += "left join RED \r\n";
                    query += "on BLUE.AVG_TIME = RED.AVG_TIME \r\n";
                    query += "left join YELLOW \r\n";
                    query += "on BLUE.AVG_TIME = YELLOW.AVG_TIME \r\n";

                    query += "union \r\n";

                    query += "select RED.AVG_TIME as AxisX, null as AxisY_BLUE, number_RED/1.3 as AxisY_RED, number_YELLOW/1.3 as AxisY_YELLOW \r\n";
                    query += "from RED \r\n";
                    query += "left join YELLOW \r\n";
                    query += "on RED.AVG_TIME = YELLOW.AVG_TIME \r\n";
                    query += "left join BLUE \r\n";
                    query += "on BLUE.AVG_TIME = RED.AVG_TIME \r\n";
                    query += "where number_BLUE is null \r\n";

                    query += "union \r\n";

                    query += "select YELLOW.AVG_TIME as AxisX, null as AxisY_BLUE, null as AxisY_RED, number_YELLOW/1.3 as AxisY_YELLOW \r\n";
                    query += "from YELLOW \r\n";
                    query += "full outer join BLUE \r\n";
                    query += "on BLUE.AVG_TIME = YELLOW.AVG_TIME \r\n";
                    query += "full outer join RED \r\n";
                    query += "on RED.AVG_TIME = YELLOW.AVG_TIME \r\n";
                    query += "where number_BLUE is null \r\n";
                    query += "and number_RED is null \r\n";
                    query += " \r\n";
                    query = query.Replace("[Aggregation]", "ROUND(SUM_TIME/10, 0)");
                }

                //#region クエリ発行
                //if (Program.local)
                //{
                //    using (System.Data.SqlServerCe.SqlCeConnection sqlConnection1 = new System.Data.SqlServerCe.SqlCeConnection(Program.cn))
                //    {
                //        System.Data.SqlServerCe.SqlCeDataAdapter da = new System.Data.SqlServerCe.SqlCeDataAdapter(query, sqlConnection1);

                //        try
                //        {
                //            sqlConnection1.Open();
                //            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                //            da.Fill(ds);
                //            System.Windows.Forms.Cursor.Current = Cursors.Default;
                //        }
                //        catch (Exception ex)
                //        {
                //            Program.WriteMessage(ex.ToString());
                //            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        }
                //        finally
                //        {
                //            sqlConnection1.Close();
                //        }
                //    }
                //}
                //else
                //{

                //    using (System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(Program.cn))
                //    {
                //        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(query, sqlConnection1);

                //        try
                //        {
                //            sqlConnection1.Open();
                //            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                //            da.Fill(ds);
                //            System.Windows.Forms.Cursor.Current = Cursors.Default;
                //        }
                //        catch (Exception ex)
                //        {
                //            Program.WriteMessage(ex.ToString());
                //            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        }
                //        finally
                //        {
                //            sqlConnection1.Close();
                //        }
                //    }
                //}
                //#endregion
                #endregion

                query = query.Replace("[ECOLOGTable]", ECOLOGTable_textBox.Text);

                dt = DatabaseAccess.GetResult(query);

                System.Windows.Forms.DataVisualization.Charting.Series semantic_link1 = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series semantic_link2 = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series semantic_link3 = new System.Windows.Forms.DataVisualization.Charting.Series();

                semantic_link1.ChartArea = "ChartArea1";
                semantic_link2.ChartArea = "ChartArea1";
                semantic_link3.ChartArea = "ChartArea1";

                semantic_link1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                semantic_link2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                semantic_link3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;

                semantic_link1.Legend = "Legend1";
                semantic_link2.Legend = "Legend1";
                semantic_link3.Legend = "Legend1";

                semantic_link1.Name = "Blue Pattern";
                semantic_link2.Name = "Red Pattern";
                semantic_link3.Name = "Other Pattern";

                semantic_link1.Color = Color.Blue;
                semantic_link2.Color = Color.Red;
                semantic_link3.Color = Color.Orange;

                semantic_link1.CustomProperties = "PointWidth=1";
                semantic_link2.CustomProperties = "PointWidth=1";
                semantic_link3.CustomProperties = "PointWidth=1";

                #region Y軸
                semantic_link1.YValueMembers = "AxisY_BLUE";
                semantic_link2.YValueMembers = "AxisY_RED";
                semantic_link3.YValueMembers = "AxisY_YELLOW";
                chartArea1.AxisY.Title = "Probability[%]";
                chartArea1.AxisY.Minimum = 0;
                #endregion

                #region X軸
                if (AxisXcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                {
                    semantic_link1.XValueMember = "AxisX";
                    semantic_link2.XValueMember = "AxisX";
                    semantic_link3.XValueMember = "AxisX";
                    chartArea1.AxisX.Title = "Average Speed[km/h]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                {
                    semantic_link1.XValueMember = "AxisX";
                    semantic_link2.XValueMember = "AxisX";
                    semantic_link3.XValueMember = "AxisX";
                    chartArea1.AxisX.Title = "Average Lost Energy[kWh]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "HOUR")
                {
                    semantic_link1.XValueMember = "AxisX";
                    semantic_link2.XValueMember = "AxisX";
                    semantic_link3.XValueMember = "AxisX";
                    chartArea1.AxisX.Title = "Hour";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                {
                    semantic_link1.XValueMember = "AxisX";
                    semantic_link2.XValueMember = "AxisX";
                    semantic_link3.XValueMember = "AxisX";
                    chartArea1.AxisX.Title = "Transit Time[sec]";
                    chartArea1.AxisX.Minimum = 0;
                }
                #endregion

                chart.Series.Add(semantic_link1);
                chart.Series.Add(semantic_link3);
                chart.Series.Add(semantic_link2);

                chart.DataSource = dt;
                chart.Invalidate();

                #endregion
            }
            else if (ChartcomboBox.SelectedItem.ToString() == "House Moving DEMO")
            {
                #region 引っ越し前後比較
                DataSet ds = new DataSet();

                #region 引っ越し前クエリ
                query = "with TripLinks as ( \r\n";
                query += "select DISTINCT ECOLOG.TRIP_ID, ECOLOG.LINK_ID \r\n";
                query += "from [ECOLOGTable] as ECOLOG, ( \r\n";
                query += "	select DISTINCT LINK_ID \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + (int.Parse(user.semanticLinkID) - 3).ToString() + " \r\n";
                query += "	) SEMANTIC_LINKS \r\n";
                query += "where ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                if (DirectioncomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                }
                if (DrivercomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (CarcomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (TORQUEData_checkBox.Checked)
                {
                    query += "	and ECOLOG.CONSUMED_FUEL is not null \r\n";
                }
                query += "), Compare as ( \r\n";
                query += "select TRIP_ID, count(*) as Number_of_Links \r\n";
                query += "from TripLinks, ( \r\n";
                query += "	select LINK_ID \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + (int.Parse(user.semanticLinkID) - 3).ToString() + " ) SEMANTIC_LINKS \r\n";
                query += "where TripLinks.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "group by TRIP_ID \r\n";
                query += "having count(*) / 0.8 >= ( \r\n";
                query += "	select count(*) as Number_of_Links \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + (int.Parse(user.semanticLinkID) - 3).ToString() + " ) \r\n";
                query += ") \r\n";


                query += "	select ECOLOG.TRIP_ID, CONVERT(nchar(5), MIN(ECOLOG.JST), 114) as Hour_before, ROUND(AVG(SPEED),1) as AVG_SPEED_before, SUM(LOST_ENERGY) as SUM_LOST_ENERGY_before, SUM(DISTANCE_DIFFERENCE) as SUM_DIFFERENCE_before, count(*) as SUM_TIME_before, SUM(LOST_ENERGY_BY_WELL_TO_WHEEL) as SUM_LOST_ENERGY_BY_WELL_TO_WHEEL_before, SUM(CONSUMED_FUEL_BY_WELL_TO_WHEEL) as SUM_FUEL_BY_WELL_TO_WHEEL_before \r\n";
                query += "	from [ECOLOGTable] as ECOLOG, SEMANTIC_LINKS, Compare \r\n";
                query += "  where SEMANTIC_LINK_ID = " + (int.Parse(user.semanticLinkID) - 3).ToString() + " \r\n";
                query += "	and ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "	and ECOLOG.TRIP_ID = Compare.TRIP_ID \r\n";
                if (DirectioncomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                }
                if (DrivercomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (CarcomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (TORQUEData_checkBox.Checked)
                {
                    query += "	and ECOLOG.CONSUMED_FUEL is not null \r\n";
                }
                query += "	group by ECOLOG.TRIP_ID \r\n";

                query = query.Replace("[ECOLOGTable]", ECOLOGTable_textBox.Text);
                #endregion

                #region データの取得
                //dt_before = Program.Get_Result(query);
                using (SqlConnection sqlConnection1 = new SqlConnection(MainForm.connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, MainForm.connectionString);

                    try
                    {
                        sqlConnection1.Open();
                        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                        SqlCommand cmd = new SqlCommand(query, sqlConnection1);
                        cmd.CommandTimeout = 600;
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        //string[,] data = new string[dt.Rows.Count, dt.Columns.Count];

                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    for (int j = 0; j < dt.Columns.Count; j++)
                        //    {

                        //        data[i, j] = dt.Rows[i][j].ToString();

                        //    }
                        //}
                        System.Windows.Forms.Cursor.Current = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        WriteLog.Write(ex.ToString());
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        sqlConnection1.Close();
                    }
                }
                #endregion

                #region 引っ越し後クエリ
                query = "with TripLinks as ( \r\n";
                query += "select DISTINCT ECOLOG.TRIP_ID, ECOLOG.LINK_ID \r\n";
                query += "from [ECOLOGTable] as ECOLOG, ( \r\n";
                query += "	select DISTINCT LINK_ID \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                query += "	) SEMANTIC_LINKS \r\n";
                query += "where ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                if (DirectioncomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                }
                if (DrivercomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (CarcomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (TORQUEData_checkBox.Checked)
                {
                    query += "	and ECOLOG.CONSUMED_FUEL is not null \r\n";
                }
                query += "), Compare as ( \r\n";
                query += "select TRIP_ID, count(*) as Number_of_Links \r\n";
                query += "from TripLinks, ( \r\n";
                query += "	select LINK_ID \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " ) SEMANTIC_LINKS \r\n";
                query += "where TripLinks.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "group by TRIP_ID \r\n";
                query += "having count(*) / 0.8 >= ( \r\n";
                query += "	select count(*) as Number_of_Links \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " ) \r\n";
                query += ") \r\n";


                query += "	select ECOLOG.TRIP_ID, CONVERT(nchar(5), MIN(ECOLOG.JST), 114) as Hour_after, ROUND(AVG(SPEED),1) as AVG_SPEED_after, SUM(LOST_ENERGY) as SUM_LOST_ENERGY_after, SUM(DISTANCE_DIFFERENCE) as SUM_DIFFERENCE_after, count(*) as SUM_TIME_after, SUM(LOST_ENERGY_BY_WELL_TO_WHEEL) as SUM_LOST_ENERGY_BY_WELL_TO_WHEEL_after, SUM(CONSUMED_FUEL_BY_WELL_TO_WHEEL) as SUM_FUEL_BY_WELL_TO_WHEEL_after \r\n";
                query += "	from [ECOLOGTable] as ECOLOG, SEMANTIC_LINKS, Compare \r\n";
                query += "  where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                query += "	and ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "	and ECOLOG.TRIP_ID = Compare.TRIP_ID \r\n";
                if (DirectioncomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                }
                if (DrivercomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (CarcomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (TORQUEData_checkBox.Checked)
                {
                    query += "	and ECOLOG.CONSUMED_FUEL is not null \r\n";
                }
                query += "	group by ECOLOG.TRIP_ID \r\n";

                query = query.Replace("[ECOLOGTable]", ECOLOGTable_textBox.Text);
                #endregion

                #region データの取得
                //dt_after = Program.Get_Result(query);
                using (SqlConnection sqlConnection1 = new SqlConnection(MainForm.connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, MainForm.connectionString);

                    try
                    {
                        sqlConnection1.Open();
                        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                        SqlCommand cmd = new SqlCommand(query, sqlConnection1);
                        cmd.CommandTimeout = 600;
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        //string[,] data = new string[dt.Rows.Count, dt.Columns.Count];

                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    for (int j = 0; j < dt.Columns.Count; j++)
                        //    {

                        //        data[i, j] = dt.Rows[i][j].ToString();

                        //    }
                        //}
                        System.Windows.Forms.Cursor.Current = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        WriteLog.Write(ex.ToString());
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        sqlConnection1.Close();
                    }
                }
                #endregion

                #region Scattergram
                System.Windows.Forms.DataVisualization.Charting.Series Series_before = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series Series_after = new System.Windows.Forms.DataVisualization.Charting.Series();

                Series_before.ChartArea = "ChartArea1";
                Series_before.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                Series_before.Name = "引っ越し前";
                Series_before.MarkerColor = Color.Red;
                Series_before.YValuesPerPoint = 7;
                Series_before.ToolTip = " TRIP_ID:#VALY2";

                Series_after.ChartArea = "ChartArea1";
                Series_after.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                Series_after.Name = "引っ越し後";
                Series_after.MarkerColor = Color.Blue;
                Series_after.YValuesPerPoint = 7;
                Series_after.ToolTip = " TRIP_ID:#VALY2";

                #region X軸
                if (AxisXcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                {
                    Series_before.XValueMember = "AVG_SPEED_before";
                    Series_after.XValueMember = "AVG_SPEED_after";
                    chartArea1.AxisX.Title = "Average of Speed[km/h]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                {
                    Series_before.XValueMember = "SUM_LOST_ENERGY_before";
                    Series_after.XValueMember = "SUM_LOST_ENERGY_after";
                    chartArea1.AxisX.Title = "Lost Energy[kWh]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "HOUR")
                {
                    Series_before.XValueMember = "Hour_before";
                    Series_after.XValueMember = "Hour_after";
                    chartArea1.AxisX.Title = "Hour";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                {
                    Series_before.XValueMember = "SUM_TIME_before";
                    Series_after.XValueMember = "SUM_TIME_after";
                    chartArea1.AxisX.Title = "Transit Time[sec]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY_BY_WELL_TO_WHEEL")
                {
                    Series_before.XValueMember = "SUM_LOST_ENERGY_BY_WELL_TO_WHEEL_before";
                    Series_after.XValueMember = "SUM_LOST_ENERGY_BY_WELL_TO_WHEEL_after";
                    chartArea1.AxisX.Title = "Lost Energy by Well-to-Wheel[MJ]";
                    chartArea1.AxisX.Minimum = 0;
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "CONSUMED_FUEL_BY_WELL_TO_WHEEL")
                {
                    Series_before.XValueMember = "SUM_FUEL_BY_WELL_TO_WHEEL_before";
                    Series_after.XValueMember = "SUM_FUEL_BY_WELL_TO_WHEEL_after";
                    chartArea1.AxisX.Title = "Consumed Fuel by Well-to-Wheel[MJ]";
                    chartArea1.AxisX.Minimum = 0;
                }
                #endregion

                #region Y軸
                if (AxisYcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                {
                    Series_before.YValueMembers = "AVG_SPEED_before, TRIP_ID";
                    Series_after.YValueMembers = "AVG_SPEED_after, TRIP_ID";
                    chartArea1.AxisY.Title = "Average of Speed[km/h]";
                    chartArea1.AxisY.Minimum = 0;
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                {
                    Series_before.YValueMembers = "SUM_LOST_ENERGY_before, TRIP_ID";
                    Series_after.YValueMembers = "SUM_LOST_ENERGY_after, TRIP_ID";
                    chartArea1.AxisY.Title = "Lost Energy[kWh]";
                    chartArea1.AxisY.Minimum = 0;
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                {
                    Series_before.YValueMembers = "SUM_TIME_before, TRIP_ID";
                    Series_after.YValueMembers = "SUM_TIME_after, TRIP_ID";
                    chartArea1.AxisY.Title = "Transit Time[sec]";
                    chartArea1.AxisY.Minimum = 0;
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "LOST_ENERGY_BY_WELL_TO_WHEEL")
                {
                    Series_before.YValueMembers = "SUM_LOST_ENERGY_BY_WELL_TO_WHEEL_before, TRIP_ID";
                    Series_after.YValueMembers = "SUM_LOST_ENERGY_BY_WELL_TO_WHEEL_after, TRIP_ID";
                    chartArea1.AxisY.Title = "Lost Energy by Well-to-Wheel[MJ]";
                    chartArea1.AxisY.Minimum = 0;
                }
                else if (AxisYcomboBox.SelectedItem.ToString() == "CONSUMED_FUEL_BY_WELL_TO_WHEEL")
                {
                    Series_before.YValueMembers = "SUM_FUEL_BY_WELL_TO_WHEEL_before, TRIP_ID";
                    Series_after.YValueMembers = "SUM_FUEL_BY_WELL_TO_WHEEL_after, TRIP_ID";
                    chartArea1.AxisY.Title = "Consumed Fuel by Well-to-Wheel[MJ]";
                    chartArea1.AxisY.Minimum = 0;
                }
                #endregion

                chart.Series.Add(Series_before);
                chart.Series.Add(Series_after);

                chart.DataSource = ds;
                #endregion
                #endregion
            }
            #region ふるいの
            //   else if (ChartcomboBox.SelectedItem.ToString() == "Histogram2")
            //{
            //    #region DEIM用
            //    DataSet ds = new DataSet();

            //    query = "	select ECOLOG.TRIP_ID, case when DATENAME(hour, MIN(JST)) <= 6 then CONVERT(nchar(5), DATEADD(hour, 24, MIN(JST)), 114) else CONVERT(nchar(5), MIN(JST), 114) end as HOUR_BLUE, AVG(SPEED) as AVG_SPEED_BLUE, SUM(LOST_ENERGY) as SUM_LOST_ENERGY_BLUE, count(*) as SUM_TIME_BLUE ";
            //    query += "	from ECOLOG, TRIPS, DRIVER_SENSOR, ( ";
            //    query += "		select LINK_ID ";
            //    query += "		from SEMANTIC_LINKS ";
            //    query += "		where SEMANTIC_LINK_ID = 4 ";
            //    query += "		) SubSubTable ";
            //    query += "	where ECOLOG.TRIP_ID = TRIPS.TRIP_ID ";
            //    query += "	and ECOLOG.DRIVER_SENSOR_ID = DRIVER_SENSOR.DRIVER_SENSOR_ID ";
            //    query += "	and ECOLOG.LINK_ID = SubSubTable.LINK_ID ";
            //    query += "	and TRIP_DIRECTION = 'homeward' ";
            //    query += "	and DRIVER_ID = 1 ";
            //    query += "  and DISTANCE_DIFFERENCE > 0 ";
            //    query += "	and ECOLOG.TRIP_ID in (1401,1395,1385,1365,1361,1353,1344,1333,1329,1305,1237,1231,1222,1206,1200,1178,1170,1152,1109,1099,1093,1081,1071,1067,1063,1037,1034,1036,985,975,951,949,961,898,894,892,886,882,855,851,849,847,738,736,733,730,726) ";
            //    query += "	group by ECOLOG.TRIP_ID  ";

            //    #region クエリ発行
            //    if (Program.local)
            //    {
            //        using (System.Data.SqlServerCe.SqlCeConnection sqlConnection1 = new System.Data.SqlServerCe.SqlCeConnection(Program.cn))
            //        {
            //            System.Data.SqlServerCe.SqlCeDataAdapter da = new System.Data.SqlServerCe.SqlCeDataAdapter(query, sqlConnection1);

            //            try
            //            {
            //                sqlConnection1.Open();
            //                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //                da.Fill(ds);
            //                System.Windows.Forms.Cursor.Current = Cursors.Default;
            //            }
            //            catch (Exception ex)
            //            {
            //                Program.WriteMessage(ex.ToString());
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            //                sqlConnection1.Close();
            //            }
            //        }
            //    }
            //    else
            //    {

            //        using (System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(Program.cn))
            //        {
            //            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(query, sqlConnection1);

            //            try
            //            {
            //                sqlConnection1.Open();
            //                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //                da.Fill(ds);
            //                System.Windows.Forms.Cursor.Current = Cursors.Default;
            //            }
            //            catch (Exception ex)
            //            {
            //                Program.WriteMessage(ex.ToString());
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            //                sqlConnection1.Close();
            //            }
            //        }
            //    }
            //    #endregion

            //    query = "	select ECOLOG.TRIP_ID, case when DATENAME(hour, MIN(JST)) <= 6 then CONVERT(nchar(5), DATEADD(hour, 24, MIN(JST)), 114) else CONVERT(nchar(5), MIN(JST), 114) end as HOUR_RED, AVG(SPEED) as AVG_SPEED_RED, SUM(LOST_ENERGY) as SUM_LOST_ENERGY_RED, count(*) as SUM_TIME_RED ";
            //    query += "	from ECOLOG, TRIPS, DRIVER_SENSOR, ( ";
            //    query += "		select LINK_ID ";
            //    query += "		from SEMANTIC_LINKS ";
            //    query += "		where SEMANTIC_LINK_ID = 4 ";
            //    query += "		) SubSubTable ";
            //    query += "	where ECOLOG.TRIP_ID = TRIPS.TRIP_ID ";
            //    query += "	and ECOLOG.DRIVER_SENSOR_ID = DRIVER_SENSOR.DRIVER_SENSOR_ID ";
            //    query += "	and ECOLOG.LINK_ID = SubSubTable.LINK_ID ";
            //    query += "	and TRIP_DIRECTION = 'homeward' ";
            //    query += "	and DRIVER_ID = 1 ";
            //    query += "  and DISTANCE_DIFFERENCE > 0 ";
            //    query += "	and ECOLOG.TRIP_ID in (1431,1425,1357,1323,1315,1267,1182,1145,1142,1135,1115,1091,1069,1061,1043,1023,991,884,880,878,836,729) ";
            //    query += "	group by ECOLOG.TRIP_ID ";

            //    #region クエリ発行
            //    if (Program.local)
            //    {
            //        using (System.Data.SqlServerCe.SqlCeConnection sqlConnection1 = new System.Data.SqlServerCe.SqlCeConnection(Program.cn))
            //        {
            //            System.Data.SqlServerCe.SqlCeDataAdapter da = new System.Data.SqlServerCe.SqlCeDataAdapter(query, sqlConnection1);

            //            try
            //            {
            //                sqlConnection1.Open();
            //                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //                da.Fill(ds);
            //                System.Windows.Forms.Cursor.Current = Cursors.Default;
            //            }
            //            catch (Exception ex)
            //            {
            //                Program.WriteMessage(ex.ToString());
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            //                sqlConnection1.Close();
            //            }
            //        }
            //    }
            //    else
            //    {

            //        using (System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(Program.cn))
            //        {
            //            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(query, sqlConnection1);

            //            try
            //            {
            //                sqlConnection1.Open();
            //                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //                da.Fill(ds);
            //                System.Windows.Forms.Cursor.Current = Cursors.Default;
            //            }
            //            catch (Exception ex)
            //            {
            //                Program.WriteMessage(ex.ToString());
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            //                sqlConnection1.Close();
            //            }
            //        }
            //    }
            //    #endregion

            //    query = "	select ECOLOG.TRIP_ID, case when DATENAME(hour, MIN(JST)) <= 6 then CONVERT(nchar(5), DATEADD(hour, 24, MIN(JST)), 114) else CONVERT(nchar(5), MIN(JST), 114) end as HOUR_YELLOW, AVG(SPEED) as AVG_SPEED_YELLOW, SUM(LOST_ENERGY) as SUM_LOST_ENERGY_YELLOW, count(*) as SUM_TIME_YELLOW ";
            //    query += "	from ECOLOG, TRIPS, DRIVER_SENSOR, ( ";
            //    query += "		select LINK_ID ";
            //    query += "		from SEMANTIC_LINKS ";
            //    query += "		where SEMANTIC_LINK_ID = 4 ";
            //    query += "		) SubSubTable ";
            //    query += "	where ECOLOG.TRIP_ID = TRIPS.TRIP_ID ";
            //    query += "	and ECOLOG.DRIVER_SENSOR_ID = DRIVER_SENSOR.DRIVER_SENSOR_ID ";
            //    query += "	and ECOLOG.LINK_ID = SubSubTable.LINK_ID ";
            //    query += "	and TRIP_DIRECTION = 'homeward' ";
            //    query += "	and DRIVER_ID = 1 ";
            //    query += "  and DISTANCE_DIFFERENCE > 0 ";
            //    query += "	and ECOLOG.TRIP_ID in (1453,1446,1399,1391,1387,1367,1363,1359,1351,1349,1345,1342,1340,1331,1283,1275,1269,1255,1301,1239,1235,1226,1214,1199,1195,1176,1168,1160,1154,1150,1148,1137,1131,1129,1119,1107,1096,1087,1077,1059,1021,1006,973,953,909,957,890,888,873,871,863,853,843,834,832,830,748,746,750,819,735) ";
            //    query += "	group by ECOLOG.TRIP_ID ";

            //    #region クエリ発行
            //    if (Program.local)
            //    {
            //        using (System.Data.SqlServerCe.SqlCeConnection sqlConnection1 = new System.Data.SqlServerCe.SqlCeConnection(Program.cn))
            //        {
            //            System.Data.SqlServerCe.SqlCeDataAdapter da = new System.Data.SqlServerCe.SqlCeDataAdapter(query, sqlConnection1);

            //            try
            //            {
            //                sqlConnection1.Open();
            //                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //                da.Fill(ds);
            //                System.Windows.Forms.Cursor.Current = Cursors.Default;
            //            }
            //            catch (Exception ex)
            //            {
            //                Program.WriteMessage(ex.ToString());
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            //                sqlConnection1.Close();
            //            }
            //        }
            //    }
            //    else
            //    {

            //        using (System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(Program.cn))
            //        {
            //            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(query, sqlConnection1);

            //            try
            //            {
            //                sqlConnection1.Open();
            //                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //                da.Fill(ds);
            //                System.Windows.Forms.Cursor.Current = Cursors.Default;
            //            }
            //            catch (Exception ex)
            //            {
            //                Program.WriteMessage(ex.ToString());
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            //                sqlConnection1.Close();
            //            }
            //        }
            //    }
            //    #endregion

            //    System.Windows.Forms.DataVisualization.Charting.Series semantic_link1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            //    System.Windows.Forms.DataVisualization.Charting.Series semantic_link2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            //    System.Windows.Forms.DataVisualization.Charting.Series semantic_link3 = new System.Windows.Forms.DataVisualization.Charting.Series();

            //    semantic_link1.ChartArea = "ChartArea1";
            //    semantic_link2.ChartArea = "ChartArea1";
            //    semantic_link3.ChartArea = "ChartArea1";
            //    semantic_link1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            //    semantic_link2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            //    semantic_link3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            //    semantic_link1.Legend = "Legend1";
            //    semantic_link2.Legend = "Legend1";
            //    semantic_link3.Legend = "Legend1";
            //    semantic_link1.Name = "Blue Pattern";
            //    semantic_link2.Name = "Red Pattern";
            //    semantic_link3.Name = "Other Pattern";
            //    semantic_link1.Color = Color.Blue;
            //    semantic_link2.Color = Color.Red;
            //    semantic_link3.Color = Color.Orange;

            //    semantic_link1.YValuesPerPoint = 4;
            //    semantic_link2.YValuesPerPoint = 4;
            //    semantic_link3.YValuesPerPoint = 4;

            //    semantic_link1.ToolTip = " TRIP_ID:#VALY2";
            //    semantic_link2.ToolTip = " TRIP_ID:#VALY2";
            //    semantic_link3.ToolTip = " TRIP_ID:#VALY2";

            //    #region X軸
            //    if (AVG_speed_X_radioButton.Checked)
            //    {
            //        semantic_link1.XValueMember = "AVG_SPEED_BLUE";
            //        semantic_link2.XValueMember = "AVG_SPEED_RED";
            //        semantic_link3.XValueMember = "AVG_SPEED_YELLOW";
            //        chartArea1.AxisX.Title = "Average Speed[km/h]";
            //        chartArea1.AxisX.TitleFont = new Font(chartArea1.AxisX.TitleFont.Name, 15F, chartArea1.AxisX.TitleFont.Style);
            //        chartArea1.AxisX.Minimum = 0;
            //    }
            //    else if (Lost_Energy_X_radioButton.Checked)
            //    {
            //        semantic_link1.XValueMember = "SUM_LOST_ENERGY_BLUE";
            //        semantic_link2.XValueMember = "SUM_LOST_ENERGY_RED";
            //        semantic_link3.XValueMember = "SUM_LOST_ENERGY_YELLOW";
            //        chartArea1.AxisX.Title = "Lost Energy[kWh]";
            //        chartArea1.AxisX.TitleFont = new Font(chartArea1.AxisX.TitleFont.Name, 15F, chartArea1.AxisX.TitleFont.Style);
            //        chartArea1.AxisX.Minimum = 0;
            //    }
            //    else if (Hour_radioButton.Checked)
            //    {
            //        semantic_link1.XValueMember = "HOUR_BLUE";
            //        semantic_link2.XValueMember = "HOUR_RED";
            //        semantic_link3.XValueMember = "HOUR_YELLOW";
            //        chartArea1.AxisX.Title = "Hour";
            //        chartArea1.AxisX.TitleFont = new Font(chartArea1.AxisX.TitleFont.Name, 15F, chartArea1.AxisX.TitleFont.Style);
            //    }
            //    else if(PassingTime_X_radioButton.Checked)
            //    {
            //        semantic_link1.XValueMember = "SUM_TIME_BLUE";
            //        semantic_link2.XValueMember = "SUM_TIME_RED";
            //        semantic_link3.XValueMember = "SUM_TIME_YELLOW";
            //        chartArea1.AxisX.Title = "Transit Time[sec]";
            //        chartArea1.AxisX.TitleFont = new Font(chartArea1.AxisX.TitleFont.Name, 15F, chartArea1.AxisX.TitleFont.Style);
            //        chartArea1.AxisX.Minimum = 0;
            //    }
            //    #endregion

            //    #region Y軸
            //    if (AVG_speed_Y_radioButton.Checked)
            //    {
            //        semantic_link1.YValueMembers = "AVG_SPEED_BLUE, TRIP_ID";
            //        semantic_link2.YValueMembers = "AVG_SPEED_RED, TRIP_ID";
            //        semantic_link3.YValueMembers = "AVG_SPEED_YELLOW, TRIP_ID"; 

            //        chartArea1.AxisY.Title = "Average Speed[km/h]";
            //        chartArea1.AxisY.TitleFont = new Font(chartArea1.AxisY.TitleFont.Name, 15F, chartArea1.AxisY.TitleFont.Style);
            //        chartArea1.AxisY.Minimum = 0;
            //    }
            //    else if (Lost_Energy_Y_radioButton.Checked)
            //    {
            //        semantic_link1.YValueMembers = "SUM_LOST_ENERGY_BLUE, TRIP_ID";
            //        semantic_link2.YValueMembers = "SUM_LOST_ENERGY_RED, TRIP_ID";
            //        semantic_link3.YValueMembers = "SUM_LOST_ENERGY_YELLOW, TRIP_ID";

            //        chartArea1.AxisY.Title = "Lost Energy[kWh]";
            //        chartArea1.AxisY.TitleFont = new Font(chartArea1.AxisY.TitleFont.Name, 15F, chartArea1.AxisY.TitleFont.Style);
            //        chartArea1.AxisY.Minimum = 0;
            //    }
            //    else if (PassingTime_Y_radioButton.Checked)
            //    {
            //        semantic_link1.YValueMembers = "SUM_TIME_BLUE, TRIP_ID";
            //        semantic_link2.YValueMembers = "SUM_TIME_RED, TRIP_ID";
            //        semantic_link3.YValueMembers = "SUM_TIME_YELLOW, TRIP_ID"; 

            //        chartArea1.AxisY.Title = "Transit Time[sec]";
            //        chartArea1.AxisY.TitleFont = new Font(chartArea1.AxisY.TitleFont.Name, 15F, chartArea1.AxisY.TitleFont.Style);
            //        chartArea1.AxisY.Minimum = 0;
            //    }
            //    #endregion

            //    chart.Series.Add(semantic_link1);
            //    chart.Series.Add(semantic_link2);
            //    chart.Series.Add(semantic_link3);
            //    chart.DataSource = ds;
            //    chart.Invalidate();
            //    #endregion
            //}
            //else 

            //    if (ChartcomboBox.SelectedItem.ToString() == "Histogram3")
            //{
            //    #region ガソリンデモ用
            //    DataSet ds = new DataSet();

            //    #region クエリ設定
            //    query = "with BLUE as ( ";
            //    query += "select ROUND(AVG(ROUND(SubTable.SUM_FUEL_ENERGY, 2)), 2) as AVG_FUEL_ENERGY, count(*) as number_BLUE ";
            //    query += "from ( ";
            //    query += "	select ECOLOG.TRIP_ID, case when DATENAME(hour, MIN(ECOLOG.JST)) <= 6 then CONVERT(nchar(2), DATEADD(hour, 24, MIN(ECOLOG.JST)), 114) else CONVERT(nchar(2), MIN(ECOLOG.JST), 114) end as Hour, AVG(SPEED) as AVG_SPEED, SUM(LOST_ENERGY) as SUM_LOST_ENERGY, SUM(DISTANCE_DIFFERENCE) as SUM_DIFFERENCE, count(*) as SUM_TIME, SUM(CONSUMED_FUEL) as SUM_FUEL_ENERGY ";
            //    query += "	from [ECOLOGTable] as ECOLOG, ( ";
            //    query += "		select LINK_ID ";
            //    query += "		from SEMANTIC_LINKS ";
            //    query += "		where SEMANTIC_LINK_ID = 4 ";
            //    query += "		) SubSubTable ";
            //    query += "	where ECOLOG.LINK_ID = SubSubTable.LINK_ID ";
            //    query += "	and TRIP_DIRECTION = 'homeward' ";
            //    query += "	and DRIVER_ID = 1 ";
            //    query += "	and ECOLOG.TRIP_ID in (1401,1395,1385,1365,1361,1353,1344,1333,1329,1305,1237,1231,1222,1206,1200,1178,1170,1152,1109,1099,1093,1081,1071,1067,1063,1037,1034,1036,985,975,951,949,961,898,894,892,886,882,855,851,849,847,738,736,733,730,726) ";
            //    query += "	group by ECOLOG.TRIP_ID ) SubTable ";
            //    query += "group by ROUND(SubTable.SUM_FUEL_ENERGY, 2) ";
            //    query += "), RED as ( ";
            //    query += "select ROUND(AVG(ROUND(SubTable.SUM_FUEL_ENERGY, 2)), 2) as AVG_FUEL_ENERGY, count(*) as number_RED ";
            //    query += "from ( ";
            //    query += "	select ECOLOG.TRIP_ID, case when DATENAME(hour, MIN(ECOLOG.JST)) <= 6 then CONVERT(nchar(2), DATEADD(hour, 24, MIN(ECOLOG.JST)), 114) else CONVERT(nchar(2), MIN(ECOLOG.JST), 114) end as Hour, AVG(SPEED) as AVG_SPEED, SUM(LOST_ENERGY) as SUM_LOST_ENERGY, SUM(DISTANCE_DIFFERENCE) as SUM_DIFFERENCE, count(*) as SUM_TIME, SUM(CONSUMED_FUEL) as SUM_FUEL_ENERGY ";
            //    query += "	from [ECOLOGTable] as ECOLOG, ( ";
            //    query += "		select LINK_ID ";
            //    query += "		from SEMANTIC_LINKS ";
            //    query += "		where SEMANTIC_LINK_ID = 4 ";
            //    query += "		) SubSubTable ";
            //    query += "	where ECOLOG.LINK_ID = SubSubTable.LINK_ID ";
            //    query += "	and TRIP_DIRECTION = 'homeward' ";
            //    query += "	and DRIVER_ID = 1 ";
            //    query += "	and ECOLOG.TRIP_ID in (1431,1425,1357,1323,1315,1267,1182,1145,1142,1135,1115,1091,1069,1061,1043,1023,991,884,880,878,836,729) ";
            //    query += "	group by ECOLOG.TRIP_ID ) SubTable ";
            //    query += "group by ROUND(SubTable.SUM_FUEL_ENERGY, 2) ";
            //    query += "), YELLOW as ( ";
            //    query += "select ROUND(AVG(ROUND(SubTable.SUM_FUEL_ENERGY, 2)), 2) as AVG_FUEL_ENERGY, count(*) as number_YELLOW ";
            //    query += "from ( ";
            //    query += "	select ECOLOG.TRIP_ID, case when DATENAME(hour, MIN(ECOLOG.JST)) <= 6 then CONVERT(nchar(2), DATEADD(hour, 24, MIN(ECOLOG.JST)), 114) else CONVERT(nchar(2), MIN(ECOLOG.JST), 114) end as Hour, AVG(SPEED) as AVG_SPEED, SUM(LOST_ENERGY) as SUM_LOST_ENERGY, SUM(DISTANCE_DIFFERENCE) as SUM_DIFFERENCE, count(*) as SUM_TIME, SUM(CONSUMED_FUEL) as SUM_FUEL_ENERGY ";
            //    query += "	from [ECOLOGTable] as ECOLOG, ( ";
            //    query += "		select LINK_ID ";
            //    query += "		from SEMANTIC_LINKS ";
            //    query += "		where SEMANTIC_LINK_ID = 4 ";
            //    query += "		) SubSubTable ";
            //    query += "	where ECOLOG.LINK_ID = SubSubTable.LINK_ID ";
            //    query += "	and TRIP_DIRECTION = 'homeward' ";
            //    query += "	and DRIVER_ID = 1 ";
            //    query += "	and ECOLOG.TRIP_ID in (1453,1446,1399,1391,1387,1367,1363,1359,1351,1349,1345,1342,1340,1331,1283,1275,1269,1255,1301,1239,1235,1226,1214,1199,1195,1176,1168,1160,1154,1150,1148,1137,1131,1129,1119,1107,1096,1087,1077,1059,1021,1006,973,953,909,957,890,888,873,871,863,853,843,834,832,830,748,746,750,819,735) ";
            //    query += "	group by ECOLOG.TRIP_ID ) SubTable ";
            //    query += "group by ROUND(SubTable.SUM_FUEL_ENERGY, 2) ";
            //    query += ") ";
            //    query += "select BLUE.AVG_FUEL_ENERGY as AxisX, number_BLUE as AxisY_BLUE, number_RED as AxisY_RED, number_YELLOW as AxisY_YELLOW ";
            //    query += "from BLUE ";
            //    query += "left join RED ";
            //    query += "on BLUE.AVG_FUEL_ENERGY = RED.AVG_FUEL_ENERGY ";
            //    query += "left join YELLOW ";
            //    query += "on BLUE.AVG_FUEL_ENERGY = YELLOW.AVG_FUEL_ENERGY ";
            //    query += "union ";
            //    query += "select RED.AVG_FUEL_ENERGY as AxisX, null as AxisY_BLUE, number_RED as AxisY_RED, number_YELLOW as AxisY_YELLOW ";
            //    query += "from RED ";
            //    query += "left join YELLOW ";
            //    query += "on RED.AVG_FUEL_ENERGY = YELLOW.AVG_FUEL_ENERGY ";
            //    query += "left join BLUE ";
            //    query += "on BLUE.AVG_FUEL_ENERGY = RED.AVG_FUEL_ENERGY ";
            //    query += "where number_BLUE is null ";
            //    query += "union ";
            //    query += "select YELLOW.AVG_FUEL_ENERGY as AxisX, null as AxisY_BLUE, null as AxisY_RED, number_YELLOW as AxisY_YELLOW ";
            //    query += "from YELLOW ";
            //    query += "full outer join BLUE ";
            //    query += "on BLUE.AVG_FUEL_ENERGY = YELLOW.AVG_FUEL_ENERGY ";
            //    query += "full outer join RED ";
            //    query += "on RED.AVG_FUEL_ENERGY = YELLOW.AVG_FUEL_ENERGY ";
            //    query += "where number_BLUE is null ";
            //    query += "and number_RED is null ";
            //    query += " ";
            //    #endregion

            //    #region クエリ発行
            //    if (Program.local)
            //    {
            //        using (System.Data.SqlServerCe.SqlCeConnection sqlConnection1 = new System.Data.SqlServerCe.SqlCeConnection(Program.cn))
            //        {
            //            System.Data.SqlServerCe.SqlCeDataAdapter da = new System.Data.SqlServerCe.SqlCeDataAdapter(query, sqlConnection1);

            //            try
            //            {
            //                sqlConnection1.Open();
            //                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //                da.Fill(ds);
            //                System.Windows.Forms.Cursor.Current = Cursors.Default;
            //            }
            //            catch (Exception ex)
            //            {
            //                Program.WriteMessage(ex.ToString());
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            //                sqlConnection1.Close();
            //            }
            //        }
            //    }
            //    else
            //    {

            //        using (System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(Program.cn))
            //        {
            //            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(query, sqlConnection1);

            //            try
            //            {
            //                sqlConnection1.Open();
            //                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //                da.Fill(ds);
            //                System.Windows.Forms.Cursor.Current = Cursors.Default;
            //            }
            //            catch (Exception ex)
            //            {
            //                Program.WriteMessage(ex.ToString());
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            //                sqlConnection1.Close();
            //            }
            //        }
            //    }
            //    #endregion

            //    System.Windows.Forms.DataVisualization.Charting.Series semantic_link1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            //    System.Windows.Forms.DataVisualization.Charting.Series semantic_link2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            //    System.Windows.Forms.DataVisualization.Charting.Series semantic_link3 = new System.Windows.Forms.DataVisualization.Charting.Series();

            //    semantic_link1.ChartArea = "ChartArea1";
            //    semantic_link2.ChartArea = "ChartArea1";
            //    semantic_link3.ChartArea = "ChartArea1";
            //    semantic_link1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            //    semantic_link2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            //    semantic_link3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            //    semantic_link1.Legend = "Legend1";
            //    semantic_link2.Legend = "Legend1";
            //    semantic_link3.Legend = "Legend1";
            //    semantic_link1.Name = "Blue Pattern";
            //    semantic_link2.Name = "Red Pattern";
            //    semantic_link3.Name = "Other Pattern";
            //    semantic_link1.Color = Color.Blue;
            //    semantic_link2.Color = Color.Red;
            //    semantic_link3.Color = Color.Orange;

            //    chartArea1.AxisX.TitleFont = new Font(chartArea1.AxisX.TitleFont.Name, 15F, chartArea1.AxisX.TitleFont.Style);
            //    chartArea1.AxisY.TitleFont = new Font(chartArea1.AxisY.TitleFont.Name, 15F, chartArea1.AxisY.TitleFont.Style);

            //    #region X軸
            //    semantic_link1.XValueMember = "AxisX";
            //    semantic_link2.XValueMember = "AxisX";
            //    semantic_link3.XValueMember = "AxisX";
            //    semantic_link1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            //    semantic_link2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            //    semantic_link3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            //    chartArea1.AxisX.Title = "Used Fuel[kWh]";
            //    chartArea1.AxisX.Minimum = 0;
            //    #endregion

            //    #region Y軸
            //    semantic_link1.YValueMembers = "AxisY_BLUE";
            //    semantic_link2.YValueMembers = "AxisY_RED";
            //    semantic_link3.YValueMembers = "AxisY_YELLOW";
            //    chartArea1.AxisY.Title = "Probability[n]";
            //    chartArea1.AxisY.Minimum = 0;
            //    #endregion

            //    chart.Series.Add(semantic_link1);
            //    chart.Series.Add(semantic_link3);
            //    chart.Series.Add(semantic_link2);
            //    chart.DataSource = ds;
            //    chart.Invalidate();
            //    #endregion
            //}
            //else if (ChartcomboBox.SelectedItem.ToString() == "Histogram4")
            //{
            //    #region ガソリンデモ用
            //    DataSet ds = new DataSet();

            //    query = "	select ECOLOG.TRIP_ID, case when DATENAME(hour, MIN(JST)) <= 6 then CONVERT(nchar(5), DATEADD(hour, 24, MIN(JST)), 114) else CONVERT(nchar(5), MIN(JST), 114) end as HOUR_BLUE, AVG(SPEED) as AVG_SPEED_BLUE, SUM(LOST_ENERGY) as SUM_LOST_ENERGY_BLUE, SUM(CONSUMED_ELECTRIC_ENERGY) as SUM_ENERGY_BLUE, SUM(CONSUMED_FUEL) as SUM_FUEL_ENERGY_BLUE, count(*) as SUM_TIME_BLUE ";
            //    query += "	from [ECOLOGTable] as ECOLOG, ( ";
            //    query += "		select LINK_ID ";
            //    query += "		from SEMANTIC_LINKS ";
            //    query += "		where SEMANTIC_LINK_ID = 4 ";
            //    query += "		) SubSubTable ";
            //    query += "	where ECOLOG.LINK_ID = SubSubTable.LINK_ID ";
            //    query += "	and TRIP_DIRECTION = 'homeward' ";
            //    query += "	and DRIVER_ID = 1 ";
            //    query += "	and ECOLOG.TRIP_ID in (1401,1395,1385,1365,1361,1353,1344,1333,1329,1305,1237,1231,1222,1206,1200,1178,1170,1152,1109,1099,1093,1081,1071,1067,1063,1037,1034,1036,985,975,951,949,961,898,894,892,886,882,855,851,849,847,738,736,733,730,726) ";
            //    query += "	group by ECOLOG.TRIP_ID  ";

            //    #region クエリ発行
            //    if (Program.local)
            //    {
            //        using (System.Data.SqlServerCe.SqlCeConnection sqlConnection1 = new System.Data.SqlServerCe.SqlCeConnection(Program.cn))
            //        {
            //            System.Data.SqlServerCe.SqlCeDataAdapter da = new System.Data.SqlServerCe.SqlCeDataAdapter(query, sqlConnection1);

            //            try
            //            {
            //                sqlConnection1.Open();
            //                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //                da.Fill(ds);
            //                System.Windows.Forms.Cursor.Current = Cursors.Default;
            //            }
            //            catch (Exception ex)
            //            {
            //                Program.WriteMessage(ex.ToString());
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            //                sqlConnection1.Close();
            //            }
            //        }
            //    }
            //    else
            //    {

            //        using (System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(Program.cn))
            //        {
            //            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(query, sqlConnection1);

            //            try
            //            {
            //                sqlConnection1.Open();
            //                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //                da.Fill(ds);
            //                System.Windows.Forms.Cursor.Current = Cursors.Default;
            //            }
            //            catch (Exception ex)
            //            {
            //                Program.WriteMessage(ex.ToString());
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            //                sqlConnection1.Close();
            //            }
            //        }
            //    }
            //    #endregion

            //    query = "	select ECOLOG.TRIP_ID, case when DATENAME(hour, MIN(JST)) <= 6 then CONVERT(nchar(5), DATEADD(hour, 24, MIN(JST)), 114) else CONVERT(nchar(5), MIN(JST), 114) end as HOUR_RED, AVG(SPEED) as AVG_SPEED_RED, SUM(LOST_ENERGY) as SUM_LOST_ENERGY_RED, SUM(CONSUMED_ELECTRIC_ENERGY) as SUM_ENERGY_RED, SUM(CONSUMED_FUEL) as SUM_FUEL_ENERGY_RED, count(*) as SUM_TIME_RED ";
            //    query += "	from [ECOLOGTable] as ECOLOG, ( ";
            //    query += "		select LINK_ID ";
            //    query += "		from SEMANTIC_LINKS ";
            //    query += "		where SEMANTIC_LINK_ID = 4 ";
            //    query += "		) SubSubTable ";
            //    query += "	where ECOLOG.LINK_ID = SubSubTable.LINK_ID ";
            //    query += "	and TRIP_DIRECTION = 'homeward' ";
            //    query += "	and DRIVER_ID = 1 ";
            //    query += "	and ECOLOG.TRIP_ID in (1431,1425,1357,1323,1315,1267,1182,1145,1142,1135,1115,1091,1069,1061,1043,1023,991,884,880,878,836,729) ";
            //    query += "	group by ECOLOG.TRIP_ID ";

            //    #region クエリ発行
            //    if (Program.local)
            //    {
            //        using (System.Data.SqlServerCe.SqlCeConnection sqlConnection1 = new System.Data.SqlServerCe.SqlCeConnection(Program.cn))
            //        {
            //            System.Data.SqlServerCe.SqlCeDataAdapter da = new System.Data.SqlServerCe.SqlCeDataAdapter(query, sqlConnection1);

            //            try
            //            {
            //                sqlConnection1.Open();
            //                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //                da.Fill(ds);
            //                System.Windows.Forms.Cursor.Current = Cursors.Default;
            //            }
            //            catch (Exception ex)
            //            {
            //                Program.WriteMessage(ex.ToString());
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            //                sqlConnection1.Close();
            //            }
            //        }
            //    }
            //    else
            //    {

            //        using (System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(Program.cn))
            //        {
            //            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(query, sqlConnection1);

            //            try
            //            {
            //                sqlConnection1.Open();
            //                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //                da.Fill(ds);
            //                System.Windows.Forms.Cursor.Current = Cursors.Default;
            //            }
            //            catch (Exception ex)
            //            {
            //                Program.WriteMessage(ex.ToString());
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            //                sqlConnection1.Close();
            //            }
            //        }
            //    }
            //    #endregion

            //    query = "	select ECOLOG.TRIP_ID, case when DATENAME(hour, MIN(JST)) <= 6 then CONVERT(nchar(5), DATEADD(hour, 24, MIN(JST)), 114) else CONVERT(nchar(5), MIN(JST), 114) end as HOUR_YELLOW, AVG(SPEED) as AVG_SPEED_YELLOW, SUM(LOST_ENERGY) as SUM_LOST_ENERGY_YELLOW, SUM(CONSUMED_ELECTRIC_ENERGY) as SUM_ENERGY_YELLOW, SUM(CONSUMED_FUEL) as SUM_FUEL_ENERGY_YELLOW, count(*) as SUM_TIME_YELLOW ";
            //    query += "	from [ECOLOGTable] as ECOLOG, ( ";
            //    query += "		select LINK_ID ";
            //    query += "		from SEMANTIC_LINKS ";
            //    query += "		where SEMANTIC_LINK_ID = 4 ";
            //    query += "		) SubSubTable ";
            //    query += "	where ECOLOG.LINK_ID = SubSubTable.LINK_ID ";
            //    query += "	and TRIP_DIRECTION = 'homeward' ";
            //    query += "	and DRIVER_ID = 1 ";
            //    query += "	and ECOLOG.TRIP_ID in (1453,1446,1399,1391,1387,1367,1363,1359,1351,1349,1345,1342,1340,1331,1283,1275,1269,1255,1301,1239,1235,1226,1214,1199,1195,1176,1168,1160,1154,1150,1148,1137,1131,1129,1119,1107,1096,1087,1077,1059,1021,1006,973,953,909,957,890,888,873,871,863,853,843,834,832,830,748,746,750,819,735) ";
            //    query += "	group by ECOLOG.TRIP_ID ";

            //    #region クエリ発行
            //    if (Program.local)
            //    {
            //        using (System.Data.SqlServerCe.SqlCeConnection sqlConnection1 = new System.Data.SqlServerCe.SqlCeConnection(Program.cn))
            //        {
            //            System.Data.SqlServerCe.SqlCeDataAdapter da = new System.Data.SqlServerCe.SqlCeDataAdapter(query, sqlConnection1);

            //            try
            //            {
            //                sqlConnection1.Open();
            //                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //                da.Fill(ds);
            //                System.Windows.Forms.Cursor.Current = Cursors.Default;
            //            }
            //            catch (Exception ex)
            //            {
            //                Program.WriteMessage(ex.ToString());
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            //                sqlConnection1.Close();
            //            }
            //        }
            //    }
            //    else
            //    {

            //        using (System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(Program.cn))
            //        {
            //            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(query, sqlConnection1);

            //            try
            //            {
            //                sqlConnection1.Open();
            //                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //                da.Fill(ds);
            //                System.Windows.Forms.Cursor.Current = Cursors.Default;
            //            }
            //            catch (Exception ex)
            //            {
            //                Program.WriteMessage(ex.ToString());
            //                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            //                sqlConnection1.Close();
            //            }
            //        }
            //    }
            //    #endregion

            //    System.Windows.Forms.DataVisualization.Charting.Series semantic_link1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            //    System.Windows.Forms.DataVisualization.Charting.Series semantic_link2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            //    System.Windows.Forms.DataVisualization.Charting.Series semantic_link3 = new System.Windows.Forms.DataVisualization.Charting.Series();

            //    semantic_link1.ChartArea = "ChartArea1";
            //    semantic_link2.ChartArea = "ChartArea1";
            //    semantic_link3.ChartArea = "ChartArea1";
            //    semantic_link1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            //    semantic_link2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            //    semantic_link3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            //    semantic_link1.Legend = "Legend1";
            //    semantic_link2.Legend = "Legend1";
            //    semantic_link3.Legend = "Legend1";
            //    semantic_link1.Name = "Blue Pattern";
            //    semantic_link2.Name = "Red Pattern";
            //    semantic_link3.Name = "Other Pattern";
            //    semantic_link1.Color = Color.Blue;
            //    semantic_link2.Color = Color.Red;
            //    semantic_link3.Color = Color.Orange;

            //    semantic_link1.YValuesPerPoint = 4;
            //    semantic_link2.YValuesPerPoint = 4;
            //    semantic_link3.YValuesPerPoint = 4;

            //    semantic_link1.ToolTip = "TRIP_ID:#VALY2";
            //    semantic_link2.ToolTip = "TRIP_ID:#VALY2";
            //    semantic_link3.ToolTip = "TRIP_ID:#VALY2";

            //    chartArea1.AxisX.TitleFont = new Font(chartArea1.AxisY.TitleFont.Name, 15F, chartArea1.AxisY.TitleFont.Style);
            //    chartArea1.AxisY.TitleFont = new Font(chartArea1.AxisY.TitleFont.Name, 15F, chartArea1.AxisY.TitleFont.Style);

            //    #region X軸
            //    if (AVG_speed_X_radioButton.Checked)
            //    {
            //        semantic_link1.XValueMember = "AVG_SPEED_BLUE";
            //        semantic_link2.XValueMember = "AVG_SPEED_RED";
            //        semantic_link3.XValueMember = "AVG_SPEED_YELLOW";

            //        chartArea1.AxisX.Title = "Average Speed[km/h]";
            //        chartArea1.AxisX.Minimum = 0;
            //    }
            //    else if (Lost_Energy_X_radioButton.Checked)
            //    {
            //        semantic_link1.XValueMember = "SUM_LOST_ENERGY_BLUE";
            //        semantic_link2.XValueMember = "SUM_LOST_ENERGY_RED";
            //        semantic_link3.XValueMember = "SUM_LOST_ENERGY_YELLOW";

            //        chartArea1.AxisX.Title = "Lost Energy[kWh]";
            //        chartArea1.AxisX.Minimum = 0;
            //    }
            //    else if (PassingTime_X_radioButton.Checked)
            //    {
            //        semantic_link1.XValueMember = "SUM_TIME_BLUE";
            //        semantic_link2.XValueMember = "SUM_TIME_RED";
            //        semantic_link3.XValueMember = "SUM_TIME_YELLOW";

            //        chartArea1.AxisX.Title = "Passing Time[sec]";
            //        chartArea1.AxisX.Minimum = 0;
            //    }
            //    //else if (Consumed_Energy_X_radioButton.Checked)
            //    //{
            //    //    semantic_link1.XValueMember = "SUM_ENERGY_BLUE";
            //    //    semantic_link2.XValueMember = "SUM_ENERGY_RED";
            //    //    semantic_link3.XValueMember = "SUM_ENERGY_YELLOW";

            //    //    chartArea1.AxisX.Title = "Consumed Energy[kWh]";
            //    //}
            //    else if (UsedFuel_X_radioButton.Checked)
            //    {
            //        semantic_link1.XValueMember = "SUM_FUEL_ENERGY_BLUE";
            //        semantic_link2.XValueMember = "SUM_FUEL_ENERGY_RED";
            //        semantic_link3.XValueMember = "SUM_FUEL_ENERGY_YELLOW";

            //        chartArea1.AxisX.Title = "Used Fuel[kWh]";
            //        chartArea1.AxisX.Minimum = 0;
            //    }
            //    #endregion

            //    #region Y軸
            //    if (AVG_speed_Y_radioButton.Checked)
            //    {
            //        semantic_link1.YValueMembers = "AVG_SPEED_BLUE, TRIP_ID";
            //        semantic_link2.YValueMembers = "AVG_SPEED_RED, TRIP_ID";
            //        semantic_link3.YValueMembers = "AVG_SPEED_YELLOW, TRIP_ID";

            //        chartArea1.AxisY.Title = "Average Speed[km/h]";
            //        chartArea1.AxisY.Minimum = 0;
            //    }
            //    else if (Lost_Energy_Y_radioButton.Checked)
            //    {
            //        semantic_link1.YValueMembers = "SUM_LOST_ENERGY_BLUE, TRIP_ID";
            //        semantic_link2.YValueMembers = "SUM_LOST_ENERGY_RED, TRIP_ID";
            //        semantic_link3.YValueMembers = "SUM_LOST_ENERGY_YELLOW, TRIP_ID";

            //        chartArea1.AxisY.Title = "Lost Energy[kWh]";
            //        chartArea1.AxisY.TitleFont = new Font(chartArea1.AxisY.TitleFont.Name, 15F, chartArea1.AxisY.TitleFont.Style);
            //        chartArea1.AxisY.Minimum = 0;
            //    }
            //    else if (PassingTime_Y_radioButton.Checked)
            //    {
            //        semantic_link1.YValueMembers = "SUM_TIME_BLUE, TRIP_ID";
            //        semantic_link2.YValueMembers = "SUM_TIME_RED, TRIP_ID";
            //        semantic_link3.YValueMembers = "SUM_TIME_YELLOW, TRIP_ID";

            //        chartArea1.AxisY.Title = "Transit Time[sec]";
            //        chartArea1.AxisY.TitleFont = new Font(chartArea1.AxisY.TitleFont.Name, 15F, chartArea1.AxisY.TitleFont.Style);
            //        chartArea1.AxisY.Minimum = 0;
            //    }
            //    else if (LostEnergyWelltoWheel_Y_radioButton.Checked)
            //    {
            //        semantic_link1.YValueMembers = "SUM_ENERGY_BLUE, TRIP_ID";
            //        semantic_link2.YValueMembers = "SUM_ENERGY_RED, TRIP_ID";
            //        semantic_link3.YValueMembers = "SUM_ENERGY_YELLOW, TRIP_ID";

            //        chartArea1.AxisY.Title = "Consumed Energy[kWh]";
            //        chartArea1.AxisY.TitleFont = new Font(chartArea1.AxisY.TitleFont.Name, 15F, chartArea1.AxisY.TitleFont.Style);
            //    }
            //    else if (UsedFuel_Y_radioButton.Checked)
            //    {
            //        semantic_link1.YValueMembers = "SUM_FUEL_ENERGY_BLUE, TRIP_ID";
            //        semantic_link2.YValueMembers = "SUM_FUEL_ENERGY_RED, TRIP_ID";
            //        semantic_link3.YValueMembers = "SUM_FUEL_ENERGY_YELLOW, TRIP_ID";

            //        chartArea1.AxisY.Title = "Used Fuel[kWh]";
            //        chartArea1.AxisY.TitleFont = new Font(chartArea1.AxisY.TitleFont.Name, 15F, chartArea1.AxisY.TitleFont.Style); 
            //        chartArea1.AxisY.Minimum = 0;
            //    }
            //    #endregion

            //    chart.Series.Add(semantic_link1);
            //    chart.Series.Add(semantic_link2);
            //    chart.Series.Add(semantic_link3);
            //    chart.DataSource = ds;
            //    chart.Invalidate();
            //    #endregion
            //}
            #endregion
        }

        private void makeQuery()
        {
            if (ChartcomboBox.SelectedItem.ToString() == "Scattergram")
            {
                #region Scattergram
                query = "with TripLinks as ( \r\n";
                query += "select DISTINCT ECOLOG.TRIP_ID, ECOLOG.LINK_ID \r\n";
                query += "from [ECOLOGTable] as ECOLOG, ( \r\n";
                query += "	select DISTINCT LINK_ID \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                query += "	) SEMANTIC_LINKS \r\n";
                query += "where ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                if (DirectioncomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                }
                if (DrivercomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (CarcomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (TORQUEData_checkBox.Checked)
                {
                    query += "	and ECOLOG.CONSUMED_FUEL is not null \r\n";
                }
                query += "), Compare as ( \r\n";
                query += "select TRIP_ID, count(*) as Number_of_Links \r\n";
                query += "from TripLinks, ( \r\n";
                query += "	select LINK_ID \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " ) SEMANTIC_LINKS \r\n";
                query += "where TripLinks.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "group by TRIP_ID \r\n";
                query += "having count(*) / 0.5 >= ( \r\n";
                query += "	select count(*) as Number_of_Links \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " ) \r\n";
                query += ") \r\n";


                query += "	select ECOLOG.TRIP_ID, CONVERT(nchar(5), MIN(ECOLOG.JST), 114) as Hour, ROUND(AVG(SPEED),1) as AVG_SPEED, SUM(LOST_ENERGY) as SUM_LOST_ENERGY, SUM(DISTANCE_DIFFERENCE) as SUM_DIFFERENCE, count(*) as SUM_TIME, SUM(LOST_ENERGY_BY_WELL_TO_WHEEL) as SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, SUM(CONSUMED_FUEL_BY_WELL_TO_WHEEL) as SUM_FUEL_BY_WELL_TO_WHEEL";
                query += ", SUM(case when CONSUMED_ELECTRIC_ENERGY < 0 then ABS(REGENE_ENERGY)/(ABS(ENERGY_BY_AIR_RESISTANCE) + ABS(ENERGY_BY_ROLLING_RESISTANCE) + ABS(CONVERT_LOSS) + ABS(REGENE_LOSS) + ABS(REGENE_ENERGY)) else 0 end) as SUM_REGENE_ENERGY_PERCENT \r\n";
                query += ", SUM(case when CONSUMED_ELECTRIC_ENERGY < 0 then ABS(REGENE_LOSS)/(ABS(ENERGY_BY_AIR_RESISTANCE) + ABS(ENERGY_BY_ROLLING_RESISTANCE) + ABS(CONVERT_LOSS) + ABS(REGENE_LOSS) + ABS(REGENE_ENERGY)) else 0 end) as SUM_REGENE_LOSS_PERCENT \r\n";
                query += "	from [ECOLOGTable] as ECOLOG, SEMANTIC_LINKS, Compare,TRIPS \r\n";
                query += "  where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                query += "	and ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "	and ECOLOG.TRIP_ID = Compare.TRIP_ID \r\n";
                query += "  and ECOLOG.TRIP_ID = TRIPS.TRIP_ID \r\n";
                query += "  and VALIDATION is null \r\n";
                if (DirectioncomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                }
                if (DrivercomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (CarcomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (TORQUEData_checkBox.Checked)
                {
                    query += "	and ECOLOG.CONSUMED_FUEL is not null \r\n";
                }
                query += "	group by ECOLOG.TRIP_ID \r\n";
                #endregion
            }
            else if (ChartcomboBox.SelectedItem.ToString() == "Histogram")
            {
                #region Histogram
                if (AxisXcomboBox.SelectedItem.ToString() == "ENERGY_BY_W2W")
                {
                    #region WtW
                    query = "with TripLinks as ( \r\n";
                    query += "select DISTINCT ECOLOG.TRIP_ID, ECOLOG.LINK_ID \r\n";
                    query += "from [ECOLOGTable] as ECOLOG \r\n";
                    query += "right join ( \r\n";
                    query += "	select DISTINCT LINK_ID \r\n";
                    query += "	from SEMANTIC_LINKS \r\n";
                    query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                    query += "	) SEMANTIC_LINKS \r\n";
                    query += "on ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                    query += "where CONSUMED_FUEL is not null \r\n";
                    if (DirectioncomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                    }
                    if (DrivercomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                    }
                    if (CarcomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                    }
                    query += "), Compare as ( \r\n";
                    query += "select TRIP_ID, count(*) as Number_of_Links \r\n";
                    query += "from TripLinks, ( \r\n";
                    query += "	select LINK_ID \r\n";
                    query += "	from SEMANTIC_LINKS \r\n";
                    query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                    query += "	) SEMANTIC_LINKS \r\n";
                    query += "where TripLinks.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                    query += "group by TRIP_ID \r\n";
                    query += "having count(*) / 0.5 >= ( \r\n";
                    query += "	select count(*) as Number_of_Links \r\n";
                    query += "	from SEMANTIC_LINKS \r\n";
                    query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " ) \r\n";
                    query += "), EV as ( \r\n";
                    query += "select ROUND(AVG(SubTable.SUM_LOST_ENERGY_BY_WELL_TO_WHEEL/10), 2)*10 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, count(*) as number_EV \r\n";
                    query += "from ( \r\n";
                    query += "	select ECOLOG.TRIP_ID, SUM(LOST_ENERGY_BY_WELL_TO_WHEEL) as SUM_LOST_ENERGY_BY_WELL_TO_WHEEL \r\n";
                    query += "	from [ECOLOGTable] as ECOLOG \r\n";
                    query += "	right join Compare \r\n";
                    query += "	on ECOLOG.TRIP_ID = Compare.TRIP_ID \r\n";
                    query += "	right join ( \r\n";
                    query += "		select LINK_ID \r\n";
                    query += "		from SEMANTIC_LINKS \r\n";
                    query += "		where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                    query += "		) SEMANTIC_LINKS \r\n";
                    query += "	on ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                    query += "where CONSUMED_FUEL is not null \r\n";
                    if (DirectioncomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                    }
                    if (DrivercomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                    }
                    if (CarcomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                    }
                    query += "	group by ECOLOG.TRIP_ID ) SubTable \r\n";
                    query += "where SUM_LOST_ENERGY_BY_WELL_TO_WHEEL is not NULL \r\n";
                    query += "group by ROUND(SubTable.SUM_LOST_ENERGY_BY_WELL_TO_WHEEL/10, 2) \r\n";
                    query += "), ICV as ( \r\n";
                    query += "select ROUND(AVG(SubTable.SUM_FUEL_BY_WELL_TO_WHEEL/10), 2)*10 as AVG_FUEL_BY_WELL_TO_WHEEL, count(*) as number_ICV \r\n";
                    query += "from ( \r\n";
                    query += "	select ECOLOG.TRIP_ID, SUM(CONSUMED_FUEL_BY_WELL_TO_WHEEL) as SUM_FUEL_BY_WELL_TO_WHEEL \r\n";
                    query += "	from [ECOLOGTable] as ECOLOG \r\n";
                    query += "	right join Compare \r\n";
                    query += "	on ECOLOG.TRIP_ID = Compare.TRIP_ID \r\n";
                    query += "	right join ( \r\n";
                    query += "		select LINK_ID \r\n";
                    query += "		from SEMANTIC_LINKS \r\n";
                    query += "		where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                    query += "		) SEMANTIC_LINKS \r\n";
                    query += "	on ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                    query += "where CONSUMED_FUEL is not null \r\n";
                    if (DirectioncomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                    }
                    if (DrivercomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                    }
                    if (CarcomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                    }
                    query += "	group by ECOLOG.TRIP_ID ) SubTable \r\n";
                    query += "where SUM_FUEL_BY_WELL_TO_WHEEL is not NULL \r\n";
                    query += "group by ROUND(SubTable.SUM_FUEL_BY_WELL_TO_WHEEL/10, 2) \r\n";
                    query += "), AllData as ( \r\n";
                    query += "select count(*) as number_All \r\n";
                    query += "from ( \r\n";
                    query += "	select DISTINCT ECOLOG.TRIP_ID \r\n";
                    query += "	from [ECOLOGTable] as ECOLOG \r\n";
                    query += "	right join Compare \r\n";
                    query += "	on ECOLOG.TRIP_ID = Compare.TRIP_ID \r\n";
                    query += "	right join ( \r\n";
                    query += "		select LINK_ID \r\n";
                    query += "		from SEMANTIC_LINKS \r\n";
                    query += "		where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                    query += "		) SEMANTIC_LINKS \r\n";
                    query += "	on ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                    query += "where CONSUMED_FUEL is not null \r\n";
                    if (DirectioncomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                    }
                    if (DrivercomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                    }
                    if (CarcomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                    }
                    query += "  ) SubTable \r\n";
                    query += ") \r\n";
                    query += "select DISTINCT AVG_LOST_ENERGY_BY_WELL_TO_WHEEL as AxisX, number_EV*100.0/number_All as number_EV, number_ICV*100.0/number_All as number_ICV \r\n";
                    query += "from AllData, EV \r\n";
                    query += "left join ICV \r\n";
                    query += "on EV.AVG_LOST_ENERGY_BY_WELL_TO_WHEEL = ICV.AVG_FUEL_BY_WELL_TO_WHEEL \r\n";
                    query += "union \r\n";
                    query += "select DISTINCT AVG_FUEL_BY_WELL_TO_WHEEL as AxisX, number_EV*100.0/number_All as number_EV, number_ICV*100.0/number_All as number_ICV \r\n";
                    query += "from AllData, ICV \r\n";
                    query += "left join EV \r\n";
                    query += "on EV.AVG_LOST_ENERGY_BY_WELL_TO_WHEEL = ICV.AVG_FUEL_BY_WELL_TO_WHEEL \r\n";
                    #endregion
                }
                else
                {
                    query = "with TripLinks as ( \r\n";
                    query += "select DISTINCT ECOLOG.TRIP_ID, ECOLOG.LINK_ID \r\n";
                    query += "from [ECOLOGTable] as ECOLOG, ( \r\n";
                    query += "	select DISTINCT LINK_ID \r\n";
                    query += "	from SEMANTIC_LINKS \r\n";
                    query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                    query += "	) SEMANTIC_LINKS \r\n";
                    query += "where ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                    if (DirectioncomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                    }
                    if (DrivercomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                    }
                    if (CarcomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                    }
                    if (TORQUEData_checkBox.Checked)
                    {
                        query += "	and ECOLOG.CONSUMED_FUEL is not null \r\n";
                    }
                    query += "), Compare as ( \r\n";
                    query += "select TRIP_ID, count(*) as Number_of_Links \r\n";
                    query += "from TripLinks, ( \r\n";
                    query += "	select LINK_ID \r\n";
                    query += "	from SEMANTIC_LINKS \r\n";
                    query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " ) SEMANTIC_LINKS \r\n";
                    query += "where TripLinks.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                    query += "group by TRIP_ID \r\n";
                    query += "having count(*) / 0.5 >= ( \r\n";
                    query += "	select count(*) as Number_of_Links \r\n";
                    query += "	from SEMANTIC_LINKS \r\n";
                    query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " ) \r\n";
                    query += ") \r\n";


                    if (AxisXcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                    {
                        query += "select AVG(ROUND(AVG_SPEED, 0)) as AVG_SPEED, count(*)*100.0/AVG(number_All) as number \r\n";
                    }
                    else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                    {
                        query += "select AVG(ROUND(SUM_LOST_ENERGY, 2)) as AVG_LOST_ENERGY, count(*)*100.0/AVG(number_All) as number \r\n";
                    }
                    else if (AxisXcomboBox.SelectedItem.ToString() == "HOUR")
                    {
                        query += "select Hour, count(*)*100.0/AVG(number_All) as number \r\n";
                    }
                    else if (AxisXcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                    {
                        query += "select AVG(ROUND(SUM_TIME/10, 0))*10 as AVG_TIME, count(*)*100.0/AVG(number_All) as number \r\n";
                    }
                    else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY_BY_WELL_TO_WHEEL")
                    {
                        query += "select AVG(ROUND(SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, 0)) as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, count(*)*100.0/AVG(number_All) as number \r\n";
                    }
                    else if (AxisXcomboBox.SelectedItem.ToString() == "CONSUMED_FUEL_BY_WELL_TO_WHEEL")
                    {
                        query += "select AVG(ROUND(SUM_FUEL_BY_WELL_TO_WHEEL, 0)) as AVG_FUEL_BY_WELL_TO_WHEEL, count(*)*100.0/AVG(number_All) as number \r\n";
                    }
                    else if (AxisXcomboBox.SelectedItem.ToString() == "REGENE_ENERGY_PERCENT")
                    {
                        query += "select AVG(ROUND(SUM_REGENE_ENERGY_PERCENT, 0)) as AVG_REGENE_ENERGY_PERCENT, count(*)*100.0/AVG(number_All) as number \r\n";
                    }
                    else if (AxisXcomboBox.SelectedItem.ToString() == "REGENE_LOSS_PERCENT")
                    {
                        query += "select AVG(ROUND(SUM_REGENE_LOSS_PERCENT, 1)) as AVG_REGENE_LOSS_PERCENT, count(*)*100.0/AVG(number_All) as number \r\n";
                    }

                    query += "from ( \r\n";

                    query += "	select ECOLOG.TRIP_ID, case when DATENAME(hour, MIN(ECOLOG.JST)) <= 6 then CONVERT(nchar(2), DATEADD(hour, 24, MIN(ECOLOG.JST)), 114) else CONVERT(nchar(2), MIN(ECOLOG.JST), 114) end as Hour, AVG(SPEED) as AVG_SPEED, SUM(CONSUMED_ELECTRIC_ENERGY) as SUM_ENERGY, SUM(LOST_ENERGY) as SUM_LOST_ENERGY, SUM(DISTANCE_DIFFERENCE) as SUM_DIFFERENCE, SUM(LOST_ENERGY_BY_WELL_TO_WHEEL) as SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, SUM(CONSUMED_FUEL_BY_WELL_TO_WHEEL) as SUM_FUEL_BY_WELL_TO_WHEEL, count(*) as SUM_TIME \r\n";
                    query += ", SUM(case when CONSUMED_ELECTRIC_ENERGY < 0 then ABS(REGENE_ENERGY)/(ABS(ENERGY_BY_AIR_RESISTANCE) + ABS(ENERGY_BY_ROLLING_RESISTANCE) + ABS(CONVERT_LOSS) + ABS(REGENE_LOSS) + ABS(REGENE_ENERGY)) else 0 end) as SUM_REGENE_ENERGY_PERCENT \r\n";
                    query += ", SUM(case when CONSUMED_ELECTRIC_ENERGY < 0 then ABS(REGENE_LOSS)/(ABS(ENERGY_BY_AIR_RESISTANCE) + ABS(ENERGY_BY_ROLLING_RESISTANCE) + ABS(CONVERT_LOSS) + ABS(REGENE_LOSS) + ABS(REGENE_ENERGY)) else 0 end) as SUM_REGENE_LOSS_PERCENT \r\n";
                    query += "	from [ECOLOGTable] as ECOLOG, SEMANTIC_LINKS, Compare \r\n";
                    query += "  where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                    query += "	and ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                    query += "	and ECOLOG.TRIP_ID = Compare.TRIP_ID \r\n";
                    if (DirectioncomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                    }
                    if (DrivercomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                    }
                    if (CarcomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                    }
                    if (TORQUEData_checkBox.Checked)
                    {
                        query += "	and ECOLOG.CONSUMED_FUEL is not null \r\n";
                    }
                    query += "	group by ECOLOG.TRIP_ID \r\n";
                    query += ") SubTable1, ( \r\n";

                    query += "	select count(*) as number_All \r\n";
                    query += "	from ( \r\n";
                    query += "	select DISTINCT ECOLOG.TRIP_ID \r\n";
                    query += "	from [ECOLOGTable] as ECOLOG, SEMANTIC_LINKS, Compare,TRIPS \r\n";
                    query += "  where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                    query += "	and ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                    query += "	and ECOLOG.TRIP_ID = Compare.TRIP_ID \r\n";
                    query += "  and ECOLOG.TRIP_ID = TRIPS.TRIP_ID \r\n";
                    query += "  and VALIDATION is null \r\n";
                    if (DirectioncomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                    }
                    if (DrivercomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                    }
                    if (CarcomboBox.SelectedIndex > 0)
                    {
                        query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                    }
                    if (TORQUEData_checkBox.Checked)
                    {
                        query += "	and ECOLOG.CONSUMED_FUEL is not null \r\n";
                    }
                    query += ") SubSubTable \r\n";
                    query += ") SubTable2 \r\n";

                    query += "group by [Aggregation] \r\n";
                    query += "order by [Aggregation] \r\n";

                    if (AxisXcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                    {
                        query = query.Replace("[Aggregation]", "ROUND(AVG_SPEED, 0)");
                    }
                    else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                    {
                        query = query.Replace("[Aggregation]", "ROUND(SUM_LOST_ENERGY, 2)");
                    }
                    else if (AxisXcomboBox.SelectedItem.ToString() == "HOUR")
                    {
                        query = query.Replace("[Aggregation]", "Hour");
                    }
                    else if (AxisXcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                    {
                        query = query.Replace("[Aggregation]", "ROUND(SUM_TIME/10, 0)");
                    }
                    else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY_BY_WELL_TO_WHEEL")
                    {
                        query = query.Replace("[Aggregation]", "ROUND(SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, 0)");
                    }
                    else if (AxisXcomboBox.SelectedItem.ToString() == "CONSUMED_FUEL_BY_WELL_TO_WHEEL")
                    {
                        query = query.Replace("[Aggregation]", "ROUND(SUM_FUEL_BY_WELL_TO_WHEEL, 0)");
                    }
                    else if (AxisXcomboBox.SelectedItem.ToString() == "REGENE_ENERGY_PERCENT")
                    {
                        query = query.Replace("[Aggregation]", "ROUND(SUM_REGENE_ENERGY_PERCENT, 0)");
                    }
                    else if (AxisXcomboBox.SelectedItem.ToString() == "REGENE_LOSS_PERCENT")
                    {
                        query = query.Replace("[Aggregation]", "ROUND(SUM_REGENE_LOSS_PERCENT, 1)");
                    }
                }
                #endregion
            }
            else if (ChartcomboBox.SelectedItem.ToString() == "Columngraph")
            {
                #region Columngraph
                query = "with TripLinks as ( \r\n";
                query += "select DISTINCT ECOLOG.TRIP_ID, ECOLOG.LINK_ID \r\n";
                query += "from [ECOLOGTable] as ECOLOG, ( \r\n";
                query += "	select DISTINCT LINK_ID \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                query += "	) SEMANTIC_LINKS \r\n";
                query += "where ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                if (DirectioncomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                }
                if (DrivercomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (CarcomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (TORQUEData_checkBox.Checked)
                {
                    query += "	and ECOLOG.CONSUMED_FUEL is not null \r\n";
                }
                query += "), Compare as ( \r\n";
                query += "select TRIP_ID, count(*) as Number_of_Links \r\n";
                query += "from TripLinks, ( \r\n";
                query += "	select LINK_ID \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " ) SEMANTIC_LINKS \r\n";
                query += "where TripLinks.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "group by TRIP_ID \r\n";
                query += "having count(*) / 0.5 >= ( \r\n";
                query += "	select count(*) as Number_of_Links \r\n";
                query += "	from SEMANTIC_LINKS \r\n";
                query += "	where SEMANTIC_LINK_ID = " + user.semanticLinkID + " ) \r\n";
                query += ") \r\n";

                if (AxisXcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                {
                    query += "select AVG(SUM_AIR_LOSS) as AVG_AIR_LOSS, AVG(SUM_ROLLING_LOSS) as AVG_ROLLING_LOSS, AVG(SUM_CONVERT_LOSS) as AVG_CONVERT_LOSS, AVG(SUM_REGENE_LOSS) as AVG_REGENE_LOSS, AVG(SUM_LOST_ENERGY) as AVG_LOST_ENERGY, AVG(ROUND(SubTable.AVG_SPEED, 0)) as AVG_SPEED, AVG(SUM_TIME) as AVG_TIME, AVG(ROUND(SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, 0)) as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, AVG(ROUND(SUM_FUEL_BY_WELL_TO_WHEEL, 0)) as AVG_FUEL_BY_WELL_TO_WHEEL, count(*) as number \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                {
                    query += "select AVG(SUM_AIR_LOSS) as AVG_AIR_LOSS, AVG(SUM_ROLLING_LOSS) as AVG_ROLLING_LOSS, AVG(SUM_CONVERT_LOSS) as AVG_CONVERT_LOSS, AVG(SUM_REGENE_LOSS) as AVG_REGENE_LOSS, AVG(ROUND(SubTable.SUM_LOST_ENERGY, 2)) as AVG_LOST_ENERGY, AVG(AVG_SPEED) as AVG_SPEED, AVG(SUM_TIME) as AVG_TIME, AVG(ROUND(SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, 0)) as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, AVG(ROUND(SUM_FUEL_BY_WELL_TO_WHEEL, 0)) as AVG_FUEL_BY_WELL_TO_WHEEL, count(*) as number \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "HOUR")
                {
                    query += "select Hour, AVG(SUM_AIR_LOSS) as AVG_AIR_LOSS, AVG(SUM_ROLLING_LOSS) as AVG_ROLLING_LOSS, AVG(SUM_CONVERT_LOSS) as AVG_CONVERT_LOSS, AVG(SUM_REGENE_LOSS) as AVG_REGENE_LOSS, AVG(SUM_LOST_ENERGY) as AVG_LOST_ENERGY, AVG(AVG_SPEED) as AVG_SPEED, AVG(SUM_TIME) as AVG_TIME, AVG(ROUND(SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, 0)) as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, AVG(ROUND(SUM_FUEL_BY_WELL_TO_WHEEL, 0)) as AVG_FUEL_BY_WELL_TO_WHEEL, count(*) as number \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                {
                    query += "select AVG(SUM_AIR_LOSS) as AVG_AIR_LOSS, AVG(SUM_ROLLING_LOSS) as AVG_ROLLING_LOSS, AVG(SUM_CONVERT_LOSS) as AVG_CONVERT_LOSS, AVG(SUM_REGENE_LOSS) as AVG_REGENE_LOSS, AVG(SUM_LOST_ENERGY) as AVG_LOST_ENERGY, AVG(AVG_SPEED) as AVG_SPEED, AVG(ROUND(SubTable.SUM_TIME/10, 0)) as AVG_TIME, AVG(ROUND(SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, 0)) as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, AVG(ROUND(SUM_FUEL_BY_WELL_TO_WHEEL, 0)) as AVG_FUEL_BY_WELL_TO_WHEEL, count(*) as number \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY_BY_WELL_TO_WHEEL")
                {
                    query += "select AVG(SUM_AIR_LOSS) as AVG_AIR_LOSS, AVG(SUM_ROLLING_LOSS) as AVG_ROLLING_LOSS, AVG(SUM_CONVERT_LOSS) as AVG_CONVERT_LOSS, AVG(SUM_REGENE_LOSS) as AVG_REGENE_LOSS, AVG(SUM_LOST_ENERGY) as AVG_LOST_ENERGY, AVG(AVG_SPEED) as AVG_SPEED, AVG(SUM_TIME) as AVG_TIME, AVG(ROUND(SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, 0)) as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, AVG(SUM_FUEL_BY_WELL_TO_WHEEL) as AVG_FUEL_BY_WELL_TO_WHEEL, count(*) as number \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "CONSUMED_FUEL_BY_WELL_TO_WHEEL")
                {
                    query += "select AVG(SUM_AIR_LOSS) as AVG_AIR_LOSS, AVG(SUM_ROLLING_LOSS) as AVG_ROLLING_LOSS, AVG(SUM_CONVERT_LOSS) as AVG_CONVERT_LOSS, AVG(SUM_REGENE_LOSS) as AVG_REGENE_LOSS, AVG(SUM_LOST_ENERGY) as AVG_LOST_ENERGY, AVG(AVG_SPEED) as AVG_SPEED, AVG(SUM_TIME) as AVG_TIME, AVG(SUM_LOST_ENERGY_BY_WELL_TO_WHEEL) as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, AVG(ROUND(SUM_FUEL_BY_WELL_TO_WHEEL, 0)) as AVG_FUEL_BY_WELL_TO_WHEEL, count(*) as number \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "YEAR")
                {
                    query += "select AVG(SUM_AIR_LOSS) as AVG_AIR_LOSS, AVG(SUM_ROLLING_LOSS) as AVG_ROLLING_LOSS, AVG(SUM_CONVERT_LOSS) as AVG_CONVERT_LOSS, AVG(SUM_REGENE_LOSS) as AVG_REGENE_LOSS, AVG(SUM_LOST_ENERGY) as AVG_LOST_ENERGY, AVG(AVG_SPEED) as AVG_SPEED, AVG(SUM_TIME) as AVG_TIME, AVG(SUM_LOST_ENERGY_BY_WELL_TO_WHEEL) as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, AVG(SUM_FUEL_BY_WELL_TO_WHEEL) as AVG_FUEL_BY_WELL_TO_WHEEL, YearMonth, count(*) as number \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "REGENE_ENERGY_PERCENT")
                {
                    query += "select AVG(SUM_AIR_LOSS) as AVG_AIR_LOSS, AVG(SUM_ROLLING_LOSS) as AVG_ROLLING_LOSS, AVG(SUM_CONVERT_LOSS) as AVG_CONVERT_LOSS, AVG(SUM_REGENE_LOSS) as AVG_REGENE_LOSS, AVG(SUM_LOST_ENERGY) as AVG_LOST_ENERGY, AVG(AVG_SPEED) as AVG_SPEED, AVG(SUM_TIME) as AVG_TIME, AVG(SUM_LOST_ENERGY_BY_WELL_TO_WHEEL) as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, AVG(SUM_FUEL_BY_WELL_TO_WHEEL) as AVG_FUEL_BY_WELL_TO_WHEEL, AVG(ROUND(SUM_REGENE_ENERGY_PERCENT, 0)) as AVG_REGENE_ENERGY_PERCENT, AVG(SUM_REGENE_LOSS_PERCENT) as AVG_REGENE_LOSS_PERCENT, count(*) as number \r\n";
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "REGENE_LOSS_PERCENT")
                {
                    query += "select AVG(SUM_AIR_LOSS) as AVG_AIR_LOSS, AVG(SUM_ROLLING_LOSS) as AVG_ROLLING_LOSS, AVG(SUM_CONVERT_LOSS) as AVG_CONVERT_LOSS, AVG(SUM_REGENE_LOSS) as AVG_REGENE_LOSS, AVG(SUM_LOST_ENERGY) as AVG_LOST_ENERGY, AVG(AVG_SPEED) as AVG_SPEED, AVG(SUM_TIME) as AVG_TIME, AVG(SUM_LOST_ENERGY_BY_WELL_TO_WHEEL) as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, AVG(SUM_FUEL_BY_WELL_TO_WHEEL) as AVG_FUEL_BY_WELL_TO_WHEEL, AVG(SUM_REGENE_ENERGY_PERCENT) as AVG_REGENE_ENERGY_PERCENT, AVG(ROUND(SUM_REGENE_LOSS_PERCENT, 1)) as AVG_REGENE_LOSS_PERCENT, count(*) as number \r\n";
                }

                query += "from ( \r\n";

                query += "	select ECOLOG.TRIP_ID, convert(nchar(6),DATEADD(hour,-1, MIN(JST)),112) as YearMonth, case when CONVERT(int, CONVERT(nchar(2), MIN(ECOLOG.JST), 114)) < 6 then CONVERT(int, CONVERT(nchar(2), MIN(ECOLOG.JST), 114)) + 24 else CONVERT(int, CONVERT(nchar(2), MIN(ECOLOG.JST), 114)) end as Hour, AVG(SPEED) as AVG_SPEED, SUM(LOST_ENERGY_BY_WELL_TO_WHEEL) as SUM_LOST_ENERGY_BY_WELL_TO_WHEEL, \r\n";
                query += "   SUM(ABS(ENERGY_BY_AIR_RESISTANCE)) as SUM_AIR_LOSS, SUM(ABS(ENERGY_BY_ROLLING_RESISTANCE)) as SUM_ROLLING_LOSS, SUM(ABS(CONVERT_LOSS)) as SUM_CONVERT_LOSS, SUM(ABS(REGENE_LOSS)) as SUM_REGENE_LOSS, SUM(LOST_ENERGY) as SUM_LOST_ENERGY, SUM(DISTANCE_DIFFERENCE) as SUM_DIFFERENCE, count(*) as SUM_TIME, SUM(CONSUMED_FUEL_BY_WELL_TO_WHEEL) as SUM_FUEL_BY_WELL_TO_WHEEL \r\n";
                query += ", SUM(case when CONSUMED_ELECTRIC_ENERGY < 0 then ABS(REGENE_ENERGY)/(ABS(ENERGY_BY_AIR_RESISTANCE) + ABS(ENERGY_BY_ROLLING_RESISTANCE) + ABS(CONVERT_LOSS) + ABS(REGENE_LOSS) + ABS(REGENE_ENERGY)) else 0 end) as SUM_REGENE_ENERGY_PERCENT \r\n";
                query += ", SUM(case when CONSUMED_ELECTRIC_ENERGY < 0 then ABS(REGENE_LOSS)/(ABS(ENERGY_BY_AIR_RESISTANCE) + ABS(ENERGY_BY_ROLLING_RESISTANCE) + ABS(CONVERT_LOSS) + ABS(REGENE_LOSS) + ABS(REGENE_ENERGY)) else 0 end) as SUM_REGENE_LOSS_PERCENT \r\n";
                query += "	from [ECOLOGTable] as ECOLOG, SEMANTIC_LINKS, Compare,TRIPS \r\n";
                
                query += "  where SEMANTIC_LINK_ID = " + user.semanticLinkID + " \r\n";
                query += "	and ECOLOG.LINK_ID = SEMANTIC_LINKS.LINK_ID \r\n";
                query += "	and ECOLOG.TRIP_ID = Compare.TRIP_ID \r\n";
                query += "  and ECOLOG.TRIP_ID = TRIPS.TRIP_ID \r\n";
                query += "  and VALIDATION is null \r\n";
                if (DirectioncomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.TRIP_DIRECTION = '" + DirectioncomboBox.SelectedItem.ToString() + "' \r\n";
                }
                if (DrivercomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.DRIVER_ID = " + MainForm.Driver[DrivercomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (CarcomboBox.SelectedIndex > 0)
                {
                    query += "	and ECOLOG.CAR_ID = " + MainForm.Car[CarcomboBox.SelectedItem.ToString()].ToString() + " \r\n";
                }
                if (TORQUEData_checkBox.Checked)
                {
                    query += "	and ECOLOG.CONSUMED_FUEL is not null \r\n";
                }
                query += "	group by ECOLOG.TRIP_ID \r\n";
                query += ") SubTable \r\n";
                query += "group by [Aggregation] \r\n";

                if (AxisXcomboBox.SelectedItem.ToString() == "HOUR")
                {
                    query += "union \r\n";
                    query += "select 6 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 7 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 8 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 9 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 10 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 11 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 12 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 13 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 14 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 15 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 16 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 17 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 18 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 19 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 20 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 21 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 22 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 23 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 24 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 25 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 26 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 27 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 28 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                    query += "union \r\n";
                    query += "select 29 as Hour, 0 as AVG_AIR_LOSS, 0 as AVG_ROLLING_LOSS, 0 as AVG_CONVERT_LOSS, 0 as AVG_REGENE_LOSS, 0 as AVG_LOST_ENERGY, 0 as AVG_SPEED, 0 as AVG_TIME, 0 as AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0 as AVG_FUEL_BY_WELL_TO_WHEEL, 0 as AVG_REGENE_ENERGY_PERCENT, 0 as AVG_REGENE_LOSS_PERCENT, 0 as number \r\n";
                }

                query += "order by [Aggregation] \r\n";

                if (AxisXcomboBox.SelectedItem.ToString() == "AVG_SPEED")
                {
                    query = query.Replace("[Aggregation]", "ROUND(AVG_SPEED, 0)");
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY")
                {
                    query = query.Replace("[Aggregation]", "ROUND(SUM_LOST_ENERGY, 2)");
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "HOUR")
                {
                    query = query.Replace("[Aggregation]", "Hour");
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "TRANSIT_TIME")
                {
                    query = query.Replace("[Aggregation]", "ROUND(SUM_TIME/10, 0)");
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "LOST_ENERGY_BY_WELL_TO_WHEEL")
                {
                    query = query.Replace("[Aggregation]", "ROUND(AVG_LOST_ENERGY_BY_WELL_TO_WHEEL, 0)");
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "CONSUMED_FUEL_BY_WELL_TO_WHEEL")
                {
                    query = query.Replace("[Aggregation]", "ROUND(AVG_FUEL_BY_WELL_TO_WHEEL, 0)");
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "YEAR")
                {
                    query = query.Replace("[Aggregation]", "YearMonth");
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "REGENE_ENERGY_PERCENT")
                {
                    query = query.Replace("[Aggregation]", "ROUND(SUM_REGENE_ENERGY_PERCENT, 0)");
                }
                else if (AxisXcomboBox.SelectedItem.ToString() == "REGENE_LOSS_PERCENT")
                {
                    query = query.Replace("[Aggregation]", "ROUND(SUM_REGENE_LOSS_PERCENT, 1)");
                }
                #endregion
            }

            query = query.Replace("[ECOLOGTable]", ECOLOGTable_textBox.Text);
        }

        private void Refreshbutton_Click(object sender, EventArgs e)
        {
            if (ChartcomboBox.SelectedIndex > -1 && AxisXcomboBox.SelectedIndex > -1 && AxisYcomboBox.SelectedIndex > -1)
            {
                // クエリを設定する
                makeQuery();

                if (QueryEdit_checkBox.Checked)
                {
                    QueryView form = new QueryView(query);

                    form.ShowDialog(this);

                    if (form.DialogResult == DialogResult.OK)
                    {
                        query = form.GetQuery();

                        chart.Series.Clear();

                        PaintChart();
                    }
                }
                else
                {
                    chart.Series.Clear();

                    PaintChart();
                }
            }
            else
            {
                MessageBox.Show("Any Box are NOT Filled.");

            }
        }

        private void ChartcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AxisXcomboBox.Items.Remove("ENERGY_BY_W2W");
            AxisXcomboBox.Items.Remove("YEAR");
            AxisYcomboBox.Items.Clear();

            if (ChartcomboBox.SelectedItem.ToString() == "Histogram")
            {
                AxisXcomboBox.Items.Add("ENERGY_BY_W2W");
                AxisYcomboBox.Items.Add("PROBABILITY");
            }
            else if (ChartcomboBox.SelectedItem.ToString() == "Columngraph")
            {
                AxisXcomboBox.Items.Add("YEAR");
                AxisYcomboBox.Items.Add("LOST_ENERGY");
                AxisYcomboBox.Items.Add("AVG_SPEED");
                AxisYcomboBox.Items.Add("TRANSIT_TIME");
                AxisYcomboBox.Items.Add("REGENE_ENERGY_PERCENT");
                AxisYcomboBox.Items.Add("REGENE_LOSS_PERCENT");
                AxisYcomboBox.Items.Add("LOST_ENERGY_BY_WELL_TO_WHEEL");
                AxisYcomboBox.Items.Add("CONSUMED_FUEL_BY_WELL_TO_WHEEL");
            }
            else if (ChartcomboBox.SelectedItem.ToString() == "W2W Compare DEMO")
            {
                AxisXcomboBox.Items.Add("ENERGY_BY_W2W");
                AxisYcomboBox.Items.Add("PROBABILITY");
            }
            else if (ChartcomboBox.SelectedItem.ToString() == "Pattern DEMO")
            {
                //AxisXcomboBox.Items.Add("LOST_ENERGY");
                AxisYcomboBox.Items.Add("PROBABILITY");
            }
            else
            {
                AxisYcomboBox.Items.Add("LOST_ENERGY");
                AxisYcomboBox.Items.Add("AVG_SPEED");
                AxisYcomboBox.Items.Add("TRANSIT_TIME");
                AxisYcomboBox.Items.Add("REGENE_ENERGY_PERCENT");
                AxisYcomboBox.Items.Add("REGENE_LOSS_PERCENT");
                AxisYcomboBox.Items.Add("LOST_ENERGY_BY_WELL_TO_WHEEL");
                AxisYcomboBox.Items.Add("CONSUMED_FUEL_BY_WELL_TO_WHEEL");

            }
        }

    }
}
