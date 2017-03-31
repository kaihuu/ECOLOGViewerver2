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
    /// 
    /// </summary>
    public partial class AvailableEnergyChart : Form
    {
        private System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="MaxX"></param>
        /// <param name="MinX"></param>
        /// <param name="MaxY"></param>
        /// <param name="MinY"></param>
        /// <param name="Max"></param>
        /// <param name="line"></param>
        /// <param name="demo"></param>
        public AvailableEnergyChart(DataTable dt1, double MaxX, double MinX, double MaxY, double MinY, double Max, bool line, bool demo)
        {
            InitializeComponent();

            DataTable dt = dt1.Copy();

            if (dt.Rows.Count > 0)
            {

                chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
                chartArea1.Name = "ChartArea1";
                this.chart.ChartAreas.Add(chartArea1);
                chartArea1.AxisY.Maximum = 50;
                chartArea1.AxisX.Maximum = 6;

                chartArea1.AxisX.TitleFont = new Font(chartArea1.AxisX.TitleFont.Name, 15F, chartArea1.AxisX.TitleFont.Style);
                chartArea1.AxisY.TitleFont = new Font(chartArea1.AxisY.TitleFont.Name, 15F, chartArea1.AxisY.TitleFont.Style);

                chartArea1.AxisX.LabelAutoFitMaxFontSize = 15;
                chartArea1.AxisY.LabelAutoFitMaxFontSize = 15;

                chartArea1.AxisX.Title = "Electric Energy Available for V2X[kWh]";

                chartArea1.AxisX.Maximum = MaxX;
                chartArea1.AxisX.Minimum = MinX;
                chartArea1.AxisY.Maximum = MaxY;
                chartArea1.AxisY.Minimum = MinY;

                if (demo)
                {
                    System.Windows.Forms.DataVisualization.Charting.Series Series1 = new System.Windows.Forms.DataVisualization.Charting.Series();

                    Series1.ChartArea = "ChartArea1";
                    Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                    Series1.Name = "1 Car";
                    Series1.Color = Color.Blue;
                    Series1.BorderWidth = 5;
                    Series1.EmptyPointStyle.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                    Series1.EmptyPointStyle.BorderWidth = 5;
                    Series1.EmptyPointStyle.Color = System.Drawing.Color.Blue;
                    Series1.CustomProperties = "EmptyPointValue=Zero";
                    Series1.XValueMember = "残余";
                    Series1.YValueMembers = "numberofOneCar";
                    chart.Series.Add(Series1);

                    System.Windows.Forms.DataVisualization.Charting.Series Series2 = new System.Windows.Forms.DataVisualization.Charting.Series();

                    Series2.ChartArea = "ChartArea1";
                    Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                    Series2.Name = "2 Cars";
                    Series2.Color = Color.Gold;
                    Series2.BorderWidth = 5;
                    Series2.EmptyPointStyle.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                    Series2.EmptyPointStyle.BorderWidth = 5;
                    Series2.EmptyPointStyle.Color = System.Drawing.Color.Gold;
                    Series2.CustomProperties = "EmptyPointValue=Zero";
                    Series2.XValueMember = "残余";
                    Series2.YValueMembers = "numberofTwoCars";
                    chart.Series.Add(Series2);

                    System.Windows.Forms.DataVisualization.Charting.Series Series3 = new System.Windows.Forms.DataVisualization.Charting.Series();

                    Series3.ChartArea = "ChartArea1";
                    Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                    Series3.Name = "3Cars";
                    Series3.Color = Color.Red;
                    Series3.BorderWidth = 5;
                    Series3.EmptyPointStyle.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                    Series3.EmptyPointStyle.BorderWidth = 5;
                    Series3.EmptyPointStyle.Color = Color.Red;
                    Series3.CustomProperties = "EmptyPointValue=Zero";
                    Series3.XValueMember = "残余";
                    Series3.YValueMembers = "numberofThreeCars";
                    chart.Series.Add(Series3);

                    if (line)
                    {
                        double Percentile = 0;
                        int i = 0;

                        for (i = 0; Percentile <= Max; i++)
                        {
                            try
                            {
                                Percentile += double.Parse(dt.Rows[i]["numberofOneCar"].ToString());
                            }
                            catch { }
                        }

                        dt.Columns.Add("Line1");

                        dt.Rows[int.Parse(dt.Rows[i - 1]["残余"].ToString())][4] = 100;

                        System.Windows.Forms.DataVisualization.Charting.Series Line1 = new System.Windows.Forms.DataVisualization.Charting.Series();

                        Line1.ChartArea = "ChartArea1";
                        Line1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                        Line1.Color = System.Drawing.Color.Black;
                        Line1.CustomProperties = "PointWidth=0.1";
                        Line1.XValueMember = "残余";
                        Line1.YValueMembers = "Line1";
                        Line1.Name = "確率" + Max + "%ライン";
                        Line1.ToolTip = "確率" + Max + "%ライン";
                        chart.Series.Add(Line1);

                        Percentile = 0;

                        for (i = 0; Percentile <= Max; i++)
                        {
                            try
                            {
                                Percentile += double.Parse(dt.Rows[i]["numberofTwoCars"].ToString());
                            }
                            catch { }
                        }

                        dt.Columns.Add("Line2");

                        dt.Rows[int.Parse(dt.Rows[i - 1]["残余"].ToString())][5] = 100;

                        System.Windows.Forms.DataVisualization.Charting.Series Line2 = new System.Windows.Forms.DataVisualization.Charting.Series();

                        Line2.ChartArea = "ChartArea1";
                        Line2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                        Line2.Color = System.Drawing.Color.Black;
                        Line2.CustomProperties = "PointWidth=0.1";
                        Line2.XValueMember = "残余";
                        Line2.YValueMembers = "Line2";
                        Line2.Name = "確率" + Max + "%ライン2";
                        Line2.ToolTip = "確率" + Max + "%ライン2";
                        chart.Series.Add(Line2);

                        Percentile = 0;

                        for (i = 0; Percentile <= Max; i++)
                        {
                            try
                            {
                                Percentile += double.Parse(dt.Rows[i]["numberofThreeCars"].ToString());
                            }
                            catch { }
                        }

                        dt.Columns.Add("Line3");

                        dt.Rows[int.Parse(dt.Rows[i - 1]["残余"].ToString())][6] = 100;

                        System.Windows.Forms.DataVisualization.Charting.Series Line3 = new System.Windows.Forms.DataVisualization.Charting.Series();

                        Line3.ChartArea = "ChartArea1";
                        Line3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                        Line3.Color = System.Drawing.Color.Black;
                        Line3.CustomProperties = "PointWidth=0.1";
                        Line3.XValueMember = "残余";
                        Line3.YValueMembers = "Line3";
                        Line3.Name = "確率" + Max + "%ライン3";
                        Line3.ToolTip = "確率" + Max + "%ライン3";
                        chart.Series.Add(Line3);
                    }
                }
                else
                {
                    System.Windows.Forms.DataVisualization.Charting.Series Series = new System.Windows.Forms.DataVisualization.Charting.Series();

                    Series.ChartArea = "ChartArea1";
                    Series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                    Series.Name = "余剰電力量ヒストグラム";
                    Series.Color = Color.Blue;
                    Series.BorderWidth = 5;
                    Series.EmptyPointStyle.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
                    Series.EmptyPointStyle.BorderWidth = 5;
                    Series.EmptyPointStyle.Color = Color.Blue;
                    Series.CustomProperties = "EmptyPointValue=Zero";
                    Series.XValueMember = "残余";
                    Series.YValueMembers = "number";
                    chart.Series.Add(Series);

                    if (line)
                    {
                        double Percentile = 0;
                        int i = 0;

                        for (i = 0; Percentile <= Max; i++)
                        {
                            try
                            {
                                Percentile += double.Parse(dt.Rows[i]["number"].ToString());
                            }
                            catch { }
                        }

                        dt.Columns.Add("Line");

                        dt.Rows[int.Parse(dt.Rows[i - 1]["残余"].ToString())][2] = 100;

                        System.Windows.Forms.DataVisualization.Charting.Series Line = new System.Windows.Forms.DataVisualization.Charting.Series();

                        Line.ChartArea = "ChartArea1";
                        Line.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                        Line.Color = System.Drawing.Color.Black;
                        Line.CustomProperties = "PointWidth=0.1";
                        Line.XValueMember = "残余";
                        Line.YValueMembers = "Line";
                        Line.Name = "確率" + Max + "%ライン";
                        Line.ToolTip = "確率" + Max + "%ライン";
                        chart.Series.Add(Line);
                    }

                }

                chartArea1.AxisY.Title = "Probability[%]";
                chartArea1.AxisY.Minimum = 0;

                chart.DataSource = dt;
                //chart.DataBind();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="demo"></param>
        public AvailableEnergyChart(DataTable dt, bool demo)
        {
            InitializeComponent();

            chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            chartArea1.AxisY.Maximum = 50;
            chartArea1.AxisX.Maximum = 6;

            chartArea1.AxisX.TitleFont = new Font(chartArea1.AxisX.TitleFont.Name, 15F, chartArea1.AxisX.TitleFont.Style);
            chartArea1.AxisY.TitleFont = new Font(chartArea1.AxisY.TitleFont.Name, 15F, chartArea1.AxisY.TitleFont.Style);

            chartArea1.AxisX.LabelAutoFitMaxFontSize = 15;
            chartArea1.AxisY.LabelAutoFitMaxFontSize = 15;

            chartArea1.AxisX.Title = "Electric Energy Available for V2X[kWh]";
            chartArea1.AxisX.Minimum = 0;

            if (demo)
            {
                System.Windows.Forms.DataVisualization.Charting.Series Series1 = new System.Windows.Forms.DataVisualization.Charting.Series();

                Series1.ChartArea = "ChartArea1";
                Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                Series1.Name = "1 Car";
                Series1.MarkerColor = Color.Red;
                Series1.CustomProperties = "PointWidth=1";
                Series1.XValueMember = "残余";
                Series1.YValueMembers = "numberofOneCar";
                chart.Series.Add(Series1);

                System.Windows.Forms.DataVisualization.Charting.Series Series2 = new System.Windows.Forms.DataVisualization.Charting.Series();

                Series2.ChartArea = "ChartArea1";
                Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                Series2.Name = "2 Cars";
                Series2.MarkerColor = Color.Yellow;
                Series2.CustomProperties = "PointWidth=1";
                Series2.XValueMember = "残余";
                Series2.YValueMembers = "numberofTwoCars";
                chart.Series.Add(Series2);

                System.Windows.Forms.DataVisualization.Charting.Series Series3 = new System.Windows.Forms.DataVisualization.Charting.Series();

                Series3.ChartArea = "ChartArea1";
                Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                Series3.Name = "3Cars";
                Series3.MarkerColor = Color.Blue;
                Series3.CustomProperties = "PointWidth=1";
                Series3.XValueMember = "残余";
                Series3.YValueMembers = "numberofThreeCars";
                chart.Series.Add(Series3);
            }
            else
            {
                System.Windows.Forms.DataVisualization.Charting.Series Series = new System.Windows.Forms.DataVisualization.Charting.Series();

                Series.ChartArea = "ChartArea1";
                Series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                Series.Name = "余剰電力量ヒストグラム";
                Series.MarkerColor = Color.Blue;
                Series.CustomProperties = "PointWidth=1";
                Series.XValueMember = "残余";
                Series.YValueMembers = "number";
                chart.Series.Add(Series);
            }

            chartArea1.AxisY.Title = "Probability[%]";
            chartArea1.AxisY.Minimum = 0;


            chart.DataSource = dt;
        }
    }
}
