using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ECOLOGViewerver2
{
    /// <summary>
    /// 期間グラフを取り扱うクラス
    /// </summary>
    public partial class HistoryGraph : Form
    {
        private DataTable dt_aggregation;
        private string aggregation;
        private string driverinfo;
        private DateTime start_time;
        private DateTime end_time;
        private int driver_id;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dt">データを含むDataTable</param>
        /// <param name="a">集計単位</param>
        /// <param name="d">集計内容</param>
        public HistoryGraph(DataTable dt, string a, string d)
        {
            InitializeComponent();
            dt_aggregation = dt;
            aggregation = a;
            driverinfo = d;

            PaintColumnChart();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <param name="di"></param>
        /// <param name="a"></param>
        /// <param name="d"></param>
        public HistoryGraph(string s, string e, int di, string a, string d)
        {
            InitializeComponent();
            start_time = DateTime.Parse(s);
            end_time = DateTime.Parse(e);
            driver_id = di;
            aggregation = a;
            driverinfo = d;

            PaintBoxPlotChart();
        }

        void PaintColumnChart()
        {
            if (driverinfo == "CONSUMED_ELECTRIC_ENERGY")
            {
                #region 消費エネルギー
                Series consumed_energy = new Series();
                consumed_energy.ChartArea = "ChartArea1";
                consumed_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                consumed_energy.Color = Color.Green;
                consumed_energy.Legend = "Legend1";
                consumed_energy.Name = "Energy Consumed by Vehicle Running";
                consumed_energy.XValueMember = aggregation;
                consumed_energy.YValueMembers = "消費";
                chart1.Series.Add(consumed_energy);

                Series heating_energy = new Series();
                heating_energy.ChartArea = "ChartArea1";
                heating_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                heating_energy.Color = Color.Red;
                heating_energy.Legend = "Legend1";
                heating_energy.Name = "Energy Consumed by Heating";
                heating_energy.XValueMember = aggregation;
                heating_energy.YValueMembers = "ヒーター";
                chart1.Series.Add(heating_energy);

                Series cooling_energy = new Series();
                cooling_energy.ChartArea = "ChartArea1";
                cooling_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                cooling_energy.Color = Color.Blue;
                cooling_energy.Legend = "Legend1";
                cooling_energy.Name = "Energy Consumed by Cooling";
                cooling_energy.XValueMember = aggregation;
                cooling_energy.YValueMembers = "クーラー";
                chart1.Series.Add(cooling_energy);

                Series equipment_energy = new Series();
                equipment_energy.ChartArea = "ChartArea1";
                equipment_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                equipment_energy.Color = Color.Yellow;
                equipment_energy.Legend = "Legend1";
                equipment_energy.Name = "Energy Consumed by Electric Equipment";
                equipment_energy.XValueMember = aggregation;
                equipment_energy.YValueMembers = "電装品";
                chart1.Series.Add(equipment_energy);

                Series rest_energy = new Series();
                rest_energy.ChartArea = "ChartArea1";
                rest_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                rest_energy.Color = Color.Gray;
                rest_energy.Legend = "Legend1";
                rest_energy.Name = "Rest Energy in EV battery";
                rest_energy.XValueMember = aggregation;
                rest_energy.YValueMembers = "残余";
                chart1.Series.Add(rest_energy);


                //if (aggregation != "Hour")
                //{
                //    System.Windows.Forms.DataVisualization.Charting.Series consumed_energy_registance = new System.Windows.Forms.DataVisualization.Charting.Series();
                //    consumed_energy_registance.ChartArea = "ChartArea1";
                //    consumed_energy_registance.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                //    consumed_energy_registance.Color = System.Drawing.Color.FromArgb(79, 129, 189);
                //    consumed_energy_registance.Legend = "Legend1";
                //    consumed_energy_registance.Name = "Average of Outward";
                //    consumed_energy_registance.XValueMember = aggregation;
                //    //consumed_energy_registance.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                //    consumed_energy_registance.YValueMembers = "outward_E";
                //    consumed_energy_registance.CustomProperties = "StackedGroupName=outward";
                //    //this.chart1.Series.Add(consumed_energy_registance);

                //    System.Windows.Forms.DataVisualization.Charting.Series consumed_energy_regene = new System.Windows.Forms.DataVisualization.Charting.Series();
                //    consumed_energy_regene.ChartArea = "ChartArea1";
                //    consumed_energy_regene.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                //    consumed_energy_regene.Color = System.Drawing.Color.FromArgb(192, 80, 77);
                //    consumed_energy_regene.Legend = "Legend1";
                //    consumed_energy_regene.Name = "Average of Homeward";
                //    consumed_energy_regene.XValueMember = aggregation;
                //    //consumed_energy_regene.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                //    consumed_energy_regene.YValueMembers = "homeward_E";
                //    consumed_energy_regene.CustomProperties = "StackedGroupName=homeward";
                //    //this.chart1.Series.Add(consumed_energy_regene);

                //}
                //System.Windows.Forms.DataVisualization.Charting.Series consumed_energy_total = new System.Windows.Forms.DataVisualization.Charting.Series();
                //consumed_energy_total.ChartArea = "ChartArea1";
                //consumed_energy_total.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                //consumed_energy_total.Color = Color.Red;
                ////consumed_energy_total.Color = System.Drawing.Color.FromArgb(155, 187, 89);
                //consumed_energy_total.Legend = "Legend1";
                //consumed_energy_total.Name = "Average Consumed Electric Energy";
                //consumed_energy_total.XValueMember = aggregation;
                ////consumed_energy_total.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                //consumed_energy_total.YValueMembers = "total_E, number_of_trip";
                //consumed_energy_total.Label = "#VALY2";
                //consumed_energy_total.LabelForeColor = Color.White;
                //consumed_energy_total.YValuesPerPoint = 2;
                //consumed_energy_total.CustomProperties = "StackedGroupName=total";

                //if (aggregation == "Hour")
                //{
                //    consumed_energy_total.Name = "Average";
                //}

                //this.chart1.Series.Add(consumed_energy_total);

                //if (aggregation != "Hour")
                //{
                //    System.Windows.Forms.DataVisualization.Charting.Series rest_energy = new System.Windows.Forms.DataVisualization.Charting.Series();
                //    rest_energy.ChartArea = "ChartArea1";
                //    rest_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                //    //rest_energy.Color = Color.Orange;
                //    rest_energy.Color = System.Drawing.Color.FromArgb(155, 187, 89);
                //    rest_energy.Legend = "Legend1";
                //    rest_energy.Name = "Rest Energy";
                //    rest_energy.XValueMember = aggregation;
                //    rest_energy.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                //    rest_energy.YValueMembers = "rest_E";
                //    rest_energy.CustomProperties = "StackedGroupName=total"; 
                //    this.chart1.Series.Add(rest_energy);
                //}

                this.chart1.ChartAreas["ChartArea1"].AxisX.Title = aggregation;
                this.chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font(this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont.Name, 15F, this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont.Style);
                this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 12.0;
                this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "Average Electric Energy[kWh]";
                this.chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.Enabled = true;
                this.chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.Interval = 2;
                this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font(this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont.Name, 15F, this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont.Style);

                if (aggregation == "Hour" || aggregation == "Month")
                {
                    this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                    this.chart1.ChartAreas["ChartArea1"].AxisX.IntervalOffset = 1;
                }

                if (aggregation == "Hour")
                {
                    this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 6.0;
                }
                #endregion
            }
            else if (driverinfo == "LOST_ENERGY")
            {
                #region エネルギーロス
                Series consumed_energy = new Series();
                consumed_energy.ChartArea = "ChartArea1";
                consumed_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                consumed_energy.Color = Color.Green;
                consumed_energy.Legend = "Legend1";
                consumed_energy.Name = "Energy Consumed by Vehicle Running";
                consumed_energy.XValueMember = aggregation;
                consumed_energy.YValueMembers = "消費";
                chart1.Series.Add(consumed_energy);

                Series heating_energy = new Series();
                heating_energy.ChartArea = "ChartArea1";
                heating_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                heating_energy.Color = Color.Red;
                heating_energy.Legend = "Legend1";
                heating_energy.Name = "Energy Consumed by Heating";
                heating_energy.XValueMember = aggregation;
                heating_energy.YValueMembers = "ヒーター";
                chart1.Series.Add(heating_energy);

                Series cooling_energy = new Series();
                cooling_energy.ChartArea = "ChartArea1";
                cooling_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                cooling_energy.Color = Color.Blue;
                cooling_energy.Legend = "Legend1";
                cooling_energy.Name = "Energy Consumed by Cooling";
                cooling_energy.XValueMember = aggregation;
                cooling_energy.YValueMembers = "クーラー";
                chart1.Series.Add(cooling_energy);

                Series equipment_energy = new Series();
                equipment_energy.ChartArea = "ChartArea1";
                equipment_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                equipment_energy.Color = Color.Yellow;
                equipment_energy.Legend = "Legend1";
                equipment_energy.Name = "Energy Consumed by Electric Equipment";
                equipment_energy.XValueMember = aggregation;
                equipment_energy.YValueMembers = "電装品";
                chart1.Series.Add(equipment_energy);

                Series rest_energy = new Series();
                rest_energy.ChartArea = "ChartArea1";
                rest_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                rest_energy.Color = Color.Gray;
                rest_energy.Legend = "Legend1";
                rest_energy.Name = "Rest Energy in EV battery";
                rest_energy.XValueMember = aggregation;
                rest_energy.YValueMembers = "残余";
                chart1.Series.Add(rest_energy);

                //if (aggregation != "Hour")
                //{
                //    System.Windows.Forms.DataVisualization.Charting.Series lost_energy_registance = new System.Windows.Forms.DataVisualization.Charting.Series();
                //    lost_energy_registance.ChartArea = "ChartArea1";
                //    lost_energy_registance.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                //    lost_energy_registance.Color = System.Drawing.Color.FromArgb(79, 129, 189);
                //    lost_energy_registance.Legend = "Legend1";
                //    lost_energy_registance.Name = "Average of Outward";
                //    lost_energy_registance.XValueMember = aggregation;
                //    lost_energy_registance.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                //    lost_energy_registance.YValueMembers = "outward_E";
                //    lost_energy_registance.CustomProperties = "StackedGroupName=outward";
                //    //this.chart1.Series.Add(lost_energy_registance);

                //    System.Windows.Forms.DataVisualization.Charting.Series lost_energy_regene = new System.Windows.Forms.DataVisualization.Charting.Series();
                //    lost_energy_regene.ChartArea = "ChartArea1";
                //    lost_energy_regene.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                //    lost_energy_regene.Color = System.Drawing.Color.FromArgb(192, 80, 77);
                //    lost_energy_regene.Legend = "Legend1";
                //    lost_energy_regene.Name = "Average of Homeward";
                //    lost_energy_regene.XValueMember = aggregation;
                //    lost_energy_regene.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                //    lost_energy_regene.YValueMembers = "homeward_E";
                //    lost_energy_regene.CustomProperties = "StackedGroupName=homeward";
                //    //this.chart1.Series.Add(lost_energy_regene);
                //}

                //System.Windows.Forms.DataVisualization.Charting.Series lost_energy_total = new System.Windows.Forms.DataVisualization.Charting.Series();
                //lost_energy_total.ChartArea = "ChartArea1";
                //lost_energy_total.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                //lost_energy_total.Color = Color.Red;
                ////lost_energy_total.Color = System.Drawing.Color.FromArgb(155, 187, 89);
                //lost_energy_total.Legend = "Legend1";
                //lost_energy_total.Name = "Average Lost Energy";
                //lost_energy_total.XValueMember = aggregation;
                //lost_energy_total.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                //lost_energy_total.YValueMembers = "total_E, number_of_trip";
                //lost_energy_total.LabelForeColor = Color.White;
                //lost_energy_total.Label = "#VALY2";
                //lost_energy_total.YValuesPerPoint = 2;
                //lost_energy_total.CustomProperties = "StackedGroupName=total";

                //if (aggregation == "Hour")
                //{
                //    lost_energy_total.Name = "Average";
                //}

                //this.chart1.Series.Add(lost_energy_total);

                //if (aggregation != "Hour")
                //{
                //    System.Windows.Forms.DataVisualization.Charting.Series rest_energy = new System.Windows.Forms.DataVisualization.Charting.Series();
                //    rest_energy.ChartArea = "ChartArea1";
                //    rest_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                //    //rest_energy.Color = Color.Orange;
                //    rest_energy.Color = System.Drawing.Color.FromArgb(155, 187, 89);
                //    rest_energy.Legend = "Legend1";
                //    rest_energy.Name = "Average Rest Energy";
                //    rest_energy.XValueMember = aggregation;
                //    rest_energy.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                //    rest_energy.YValueMembers = "rest_E";
                //    rest_energy.CustomProperties = "StackedGroupName=total";
                //    this.chart1.Series.Add(rest_energy);
                //}

                this.chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font(this.chart1.ChartAreas["ChartArea1"].AxisX.TitleFont.Name, 15F, this.chart1.ChartAreas["ChartArea1"].AxisX.TitleFont.Style);
                this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 12.0;
                this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "Average Electric Energy[kWh]";
                this.chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.Enabled = true;
                this.chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.Interval = 2;
                this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font(this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont.Name, 15F, this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont.Style);

                if (aggregation == "Hour" || aggregation == "Month")
                {
                    this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                    this.chart1.ChartAreas["ChartArea1"].AxisX.IntervalOffset = 1;
                }
                #endregion
            }

            this.chart1.DataSource = dt_aggregation;
            this.chart1.Legends["Legend1"].Enabled = false;
        }

        void PaintBoxPlotChart()
        {
            string query = "";
            DataTable work = new DataTable();
            DateTime time = start_time;
            int i = 0;

            #region テーブルの初期化
            DataTable dt_chart = new DataTable();
            dt_chart.Columns.Add("Month");
            dt_chart.Columns.Add("Minimum");
            dt_chart.Columns.Add("Maximum");
            dt_chart.Columns.Add("Quartile1");
            dt_chart.Columns.Add("Quartile3");
            dt_chart.Columns.Add("Average");
            dt_chart.Columns.Add("Median");
            #endregion

            #region 月毎のループ処理
            while (time <= end_time)
            {
                #region クエリ設定
                query = "with SubTable as ( ";
                query += "select ECOLOG.TRIP_ID, MIN(JST) as START_TIME, SUM(CONSUMED_ELECTRIC_ENERGY) as 消費, SUM(ENERGY_BY_COOLING) as クーラー, SUM(ENERGY_BY_HEATING) as ヒーター, SUM(ENERGY_BY_EQUIPMENT) as 電装品, ECOLOG.TRIP_DIRECTION ";
                query += "from ECOLOG as ECOLOG ";
                query += "where DRIVER_ID = " + driver_id + " ";
                query += "and JST between '" + time.ToString("yyyy-MM-dd") + " 00:00:00' and '" + time.AddMonths(1).ToString("yyyy-MM-dd") + " 00:00:00' ";
                query += "and ENERGY_BY_COOLING is not null and ENERGY_BY_HEATING is not null and ENERGY_BY_EQUIPMENT is not null ";
                query += "group by ECOLOG.TRIP_ID, ECOLOG.TRIP_DIRECTION ";
                query += "), Out as ( ";
                query += "select DATENAME(year,DATEADD(hour,-1,START_TIME)) as Year, DATENAME(month,DATEADD(hour,-1,START_TIME)) as Month, DATENAME(day,DATEADD(hour,-1,START_TIME)) as Day, AVG(消費) + AVG(クーラー) + AVG(ヒーター) + AVG(電装品) as 消費 ";
                query += "from SubTable ";
                query += "where TRIP_DIRECTION = 'outward' ";
                query += "group by DATENAME(year,DATEADD(hour,-1,START_TIME)), DATENAME(month,DATEADD(hour,-1,START_TIME)), DATENAME(day,DATEADD(hour,-1,START_TIME)) ";
                query += "), Home as ( ";
                query += "select DATENAME(year,DATEADD(hour,-1,START_TIME)) as Year, DATENAME(month,DATEADD(hour,-1,START_TIME)) as Month, DATENAME(day,DATEADD(hour,-1,START_TIME)) as Day, AVG(消費) + AVG(クーラー) + AVG(ヒーター) + AVG(電装品) as 消費 ";
                query += "from SubTable ";
                query += "where TRIP_DIRECTION = 'homeward' ";
                query += "group by DATENAME(year,DATEADD(hour,-1,START_TIME)), DATENAME(month,DATEADD(hour,-1,START_TIME)), DATENAME(day,DATEADD(hour,-1,START_TIME)) ";
                query += ") ";
                query += " ";
                query += "select Out.Day, Out.消費+Home.消費 as 消費 ";
                query += "from Out, Home ";
                query += "where Out.Year = Home.Year ";
                query += "and Out.Month = Home.Month ";
                query += "and Out.Day = Home.Day ";
                query += "order by Out.消費+Home.消費 ";
                #endregion

                work = DatabaseAccess.GetResult(query);

                dt_chart.Rows.Add();
                dt_chart.Rows[i][0] = time.ToString("yyyyMM");

                if (work.Rows.Count > 0)
                {
                    dt_chart.Rows[i][1] = work.Rows[0][1];
                    dt_chart.Rows[i][2] = work.Rows[work.Rows.Count - 1][1];
                    dt_chart.Rows[i][3] = work.Rows[(int)(Math.Floor((work.Rows.Count - 1) * (1.0 / 4.0)))][1];
                    dt_chart.Rows[i][4] = work.Rows[(int)(Math.Floor((work.Rows.Count - 1) * (3.0 / 4.0)))][1];
                    dt_chart.Rows[i][5] = work.Rows[(int)(Math.Floor((work.Rows.Count - 1) * (2.0 / 4.0)))][1];
                    dt_chart.Rows[i][6] = work.Compute("AVG([消費])", null);
                }
                time = time.AddMonths(1);
                i++;
            }
            #endregion

            Series series = new Series();

            series.ChartArea = "ChartArea1";
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.BoxPlot;
            series.Color = Color.Red;
            series.Legend = "Legend1";
            series.Name = "Series1";
            series.XValueMember = "Month";
            series.YValueMembers = "Minimum,Maximum,Quartile1,Quartile3,Average,Median";
            series.YValuesPerPoint = 6;
            chart1.Series.Add(series);

            this.chart1.ChartAreas["ChartArea1"].AxisX.Title = aggregation;
            this.chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font(this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont.Name, 15F, this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont.Style);
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 12.0;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "Average Electric Energy[kWh]";
            this.chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.Enabled = true;
            this.chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.Interval = 1;
            this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font(this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont.Name, 15F, this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont.Style);
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            this.chart1.ChartAreas["ChartArea1"].AxisX.IntervalOffset = 1;
            this.chart1.Legends["Legend1"].Enabled = false;
            this.chart1.DataSource = dt_chart;
        }
    }
}
