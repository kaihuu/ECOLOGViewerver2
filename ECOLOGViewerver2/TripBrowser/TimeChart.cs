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
    /// 時間軸グラフを取り扱うクラス
    /// </summary>
    public partial class TimeChart : Form
    {
        #region 変数
        private FormData user;
        private DataTable dt;
        private DateTime start_time;
        private DateTime end_time;
        private string content;
        private string air_energy;
        private string rolling_energy;
        private string climbing_energy;
        private string acc_energy;
        private string convert_loss;
        private string regene_loss;
        private string regene_energy;
        private string consumed_energy;
        private string lost_energy;
        internal bool propertyShowed = false;
        System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
        System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="u">表示するトリップの情報</param>
        /// <param name="t1">開始時刻</param>
        /// <param name="t2">終了時刻</param>
        /// <param name="str3">表示する内容</param>
        /// <param name="str4">X軸の最大値</param>
        /// <param name="str5">Y軸の最大値</param>
        /// <param name="str6">Y軸の最小値</param>
        public TimeChart(FormData u, DateTime t1, DateTime t2, string str3, string str4, string str5, string str6)
        {
            InitializeComponent();

            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            //this.chart1.Legends.Add(legend1);

            #region 表示内容設定
            content = str3;
            chartArea1.AxisX.Title = "Time";
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);

            if (content == "EnergyModel")
            {
                chartArea1.AxisY.Title = "Energy[kWh]";
                #region エネルギーモデル
                #region インスタンス作成
                System.Windows.Forms.DataVisualization.Charting.Series air_energy = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series rolling_energy = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series climbing_energy_plus = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series climbing_energy_minus = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series acc_energy = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series convert_loss = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series regene_loss = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series electric_energy = new System.Windows.Forms.DataVisualization.Charting.Series();
                #endregion

                #region 設定
                #region 電気エネルギー
                electric_energy.ChartArea = "ChartArea1";
                electric_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
                electric_energy.Color = System.Drawing.Color.MediumPurple;
                electric_energy.Legend = "Legend1";
                electric_energy.Name = "ElectricEnergy-in-EV";
                electric_energy.XValueMember = "time";
                electric_energy.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                electric_energy.YValueMembers = "OTHER_ENERGY";
                electric_energy.BorderWidth = 0;
                #endregion

                #region 運動エネルギー＝加速抵抗
                acc_energy.ChartArea = "ChartArea1";
                acc_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
                acc_energy.Color = System.Drawing.Color.LimeGreen;
                acc_energy.Legend = "Legend1";
                acc_energy.Name = "KineticEnergy";
                acc_energy.XValueMember = "time";
                acc_energy.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                acc_energy.YValueMembers = "ACC_ENERGY";
                acc_energy.BorderWidth = 0;
                #endregion

                #region 空気抵抗
                air_energy.ChartArea = "ChartArea1";
                air_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
                air_energy.Color = System.Drawing.Color.Yellow;
                air_energy.Legend = "Legend1";
                air_energy.Name = "AirResistanceLoss";
                air_energy.XValueMember = "time";
                air_energy.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                air_energy.YValueMembers = "AIR_ENERGY";
                air_energy.BorderWidth = 0;
                #endregion

                #region 転がり抵抗
                rolling_energy.ChartArea = "ChartArea1";
                rolling_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
                rolling_energy.Color = System.Drawing.Color.SandyBrown;
                rolling_energy.Legend = "Legend1";
                rolling_energy.Name = "RollingResistanceLoss";
                rolling_energy.XValueMember = "time";
                rolling_energy.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                rolling_energy.YValueMembers = "ROLLING_ENERGY";
                rolling_energy.BorderWidth = 0;
                #endregion

                #region 登坂抵抗
                climbing_energy_plus.ChartArea = "ChartArea1";
                climbing_energy_plus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
                climbing_energy_plus.Color = System.Drawing.Color.MediumBlue;
                climbing_energy_plus.Legend = "Legend1";
                climbing_energy_plus.Name = "PotentialEnergy";
                climbing_energy_plus.XValueMember = "time";
                climbing_energy_plus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                climbing_energy_plus.YValueMembers = "CLIMBING_ENERGY_PLUS";
                climbing_energy_plus.BorderWidth = 0;

                climbing_energy_minus.ChartArea = "ChartArea1";
                climbing_energy_minus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
                climbing_energy_minus.Color = System.Drawing.Color.LightSkyBlue;
                climbing_energy_minus.Legend = "Legend1";
                climbing_energy_minus.Name = "Spouted-PotentialEnergy";
                climbing_energy_minus.XValueMember = "time";
                climbing_energy_minus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                climbing_energy_minus.YValueMembers = "CLIMBING_ENERGY_MINUS";
                climbing_energy_minus.BorderWidth = 0;
                #endregion

                #region 変換ロス
                convert_loss.ChartArea = "ChartArea1";
                convert_loss.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
                convert_loss.Color = System.Drawing.Color.Red;
                convert_loss.Legend = "Legend1";
                convert_loss.Name = "ConvertingLoss";
                convert_loss.XValueMember = "time";
                convert_loss.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                convert_loss.YValueMembers = "CONVERT_LOSS";
                convert_loss.BorderWidth = 0;
                #endregion

                #region 回生ロス
                regene_loss.ChartArea = "ChartArea1";
                regene_loss.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
                regene_loss.Color = System.Drawing.Color.Orchid;
                regene_loss.Legend = "Legend1";
                regene_loss.Name = "RegeneratingLoss";
                regene_loss.XValueMember = "time";
                regene_loss.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                regene_loss.YValueMembers = "REGENE_LOSS";
                regene_loss.BorderWidth = 0;
                #endregion
                #endregion

                #region Seriesに追加
                this.chart1.Series.Add(electric_energy);
                this.chart1.Series.Add(climbing_energy_plus);
                this.chart1.Series.Add(acc_energy);
                this.chart1.Series.Add(regene_loss);
                this.chart1.Series.Add(convert_loss);
                this.chart1.Series.Add(air_energy);
                this.chart1.Series.Add(rolling_energy);
                this.chart1.Series.Add(climbing_energy_minus);
                #endregion
                #endregion
            }
            if (content == "PowerModel")
            {
                chartArea1.AxisY.Title = "Power[kW]";
                //chartArea1.AxisY.MajorGrid.Interval = 0.01D;
                #region パワーモデル
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
                #endregion

                #region 設定

                #region 運動エネルギー＝加速抵抗
                acc_energy.ChartArea = "ChartArea1";
                acc_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                //acc_energy.Color = System.Drawing.Color.LimeGreen;
                acc_energy.Color = System.Drawing.Color.ForestGreen;
                acc_energy.Legend = "Legend1";
                acc_energy.Name = "運動エネルギー";
                acc_energy.XValueMember = "time";
                acc_energy.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                acc_energy.YValueMembers = "ACC_ENERGY_PLUS";
                #endregion

                #region 空気抵抗
                air_energy_plus.ChartArea = "ChartArea1";
                air_energy_plus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                air_energy_plus.Color = System.Drawing.Color.Yellow;
                air_energy_plus.Legend = "Legend1";
                air_energy_plus.Name = "空気抵抗(力行時)";
                air_energy_plus.XValueMember = "time";
                air_energy_plus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                air_energy_plus.YValueMembers = "AIR_ENERGY_PLUS";

                air_energy_minus.ChartArea = "ChartArea1";
                air_energy_minus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                air_energy_minus.Color = System.Drawing.Color.Silver;
                air_energy_minus.Legend = "Legend1";
                air_energy_minus.Name = "空気抵抗(回生時)";
                air_energy_minus.XValueMember = "time";
                air_energy_minus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                air_energy_minus.YValueMembers = "AIR_ENERGY_MINUS";
                #endregion

                #region 転がり抵抗
                rolling_energy_plus.ChartArea = "ChartArea1";
                rolling_energy_plus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                rolling_energy_plus.Color = System.Drawing.Color.SandyBrown;
                rolling_energy_plus.Legend = "Legend1";
                rolling_energy_plus.Name = "転がり抵抗(力行時)";
                rolling_energy_plus.XValueMember = "time";
                rolling_energy_plus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                rolling_energy_plus.YValueMembers = "ROLLING_ENERGY_PLUS";

                rolling_energy_minus.ChartArea = "ChartArea1";
                rolling_energy_minus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                rolling_energy_minus.Color = System.Drawing.Color.SlateGray;
                rolling_energy_minus.Legend = "Legend1";
                rolling_energy_minus.Name = "転がり抵抗(回生時)";
                rolling_energy_minus.XValueMember = "time";
                rolling_energy_minus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                rolling_energy_minus.YValueMembers = "ROLLING_ENERGY_MINUS";
                #endregion

                #region 登坂抵抗
                climbing_energy_plus_up.ChartArea = "ChartArea1";
                climbing_energy_plus_up.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                //climbing_energy_plus_up.Color = System.Drawing.Color.DarkRed;
                climbing_energy_plus_up.Color = System.Drawing.Color.MediumBlue;
                climbing_energy_plus_up.Legend = "Legend1";
                climbing_energy_plus_up.Name = "登坂抵抗消費分(力行時)";
                climbing_energy_plus_up.XValueMember = "time";
                climbing_energy_plus_up.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                climbing_energy_plus_up.YValueMembers = "CLIMBING_ENERGY_PLUS_UP";

                climbing_energy_plus_down.ChartArea = "ChartArea1";
                climbing_energy_plus_down.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                //climbing_energy_plus_down.Color = System.Drawing.Color.LightSkyBlue;
                climbing_energy_plus_down.Color = System.Drawing.Color.SkyBlue;
                climbing_energy_plus_down.Legend = "Legend1";
                climbing_energy_plus_down.Name = "登坂抵抗回生分(力行時)";
                climbing_energy_plus_down.XValueMember = "time";
                climbing_energy_plus_down.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                climbing_energy_plus_down.YValueMembers = "CLIMBING_ENERGY_PLUS_DOWN";

                climbing_energy_minus_up.ChartArea = "ChartArea1";
                climbing_energy_minus_up.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                //climbing_energy_minus_up.Color = System.Drawing.Color.DarkGray;
                climbing_energy_minus_up.Color = System.Drawing.Color.MediumBlue;
                climbing_energy_minus_up.Legend = "Legend1";
                climbing_energy_minus_up.Name = "登坂抵抗消費分(回生時)";
                climbing_energy_minus_up.XValueMember = "time";
                climbing_energy_minus_up.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                climbing_energy_minus_up.YValueMembers = "CLIMBING_ENERGY_MINUS_UP";

                climbing_energy_minus_down.ChartArea = "ChartArea1";
                climbing_energy_minus_down.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                //climbing_energy_minus_down.Color = System.Drawing.Color.LightSkyBlue;
                climbing_energy_minus_down.Color = System.Drawing.Color.RoyalBlue;
                climbing_energy_minus_down.Legend = "Legend1";
                climbing_energy_minus_down.Name = "登坂抵抗回生分(回生時)";
                climbing_energy_minus_down.XValueMember = "time";
                climbing_energy_minus_down.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                climbing_energy_minus_down.YValueMembers = "CLIMBING_ENERGY_MINUS_DOWN";
                #endregion

                #region 変換ロス
                convert_loss_plus.ChartArea = "ChartArea1";
                convert_loss_plus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                convert_loss_plus.Color = System.Drawing.Color.Red;
                convert_loss_plus.Legend = "Legend1";
                convert_loss_plus.Name = "エネルギー変換ロス(力行時)";
                convert_loss_plus.XValueMember = "time";
                convert_loss_plus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                convert_loss_plus.YValueMembers = "CONVERT_LOSS_PLUS";

                convert_loss_minus.ChartArea = "ChartArea1";
                convert_loss_minus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                convert_loss_minus.Color = System.Drawing.Color.Red;
                convert_loss_minus.Legend = "Legend1";
                convert_loss_minus.Name = "エネルギー変換ロス(回生時)";
                convert_loss_minus.XValueMember = "time";
                convert_loss_minus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                convert_loss_minus.YValueMembers = "CONVERT_LOSS_MINUS";
                #endregion

                #region 回生ロス
                regene_loss.ChartArea = "ChartArea1";
                regene_loss.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                regene_loss.Color = System.Drawing.Color.Orchid;
                regene_loss.Legend = "Legend1";
                regene_loss.Name = "回生ロス";
                regene_loss.XValueMember = "time";
                regene_loss.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                regene_loss.YValueMembers = "REGENE_LOSS";
                #endregion

                #region 回生エネルギー
                regene_energy.ChartArea = "ChartArea1";
                regene_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                regene_energy.Color = System.Drawing.Color.LimeGreen;
                regene_energy.Legend = "Legend1";
                regene_energy.Name = "回生エネルギー";
                regene_energy.XValueMember = "time";
                regene_energy.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                regene_energy.YValueMembers = "REGENE_ENERGY";
                #endregion
                #endregion

                #region Seriesに追加
                this.chart1.Series.Add(air_energy_plus);
                this.chart1.Series.Add(air_energy_minus);
                this.chart1.Series.Add(rolling_energy_plus);
                this.chart1.Series.Add(rolling_energy_minus);
                this.chart1.Series.Add(convert_loss_plus);
                this.chart1.Series.Add(climbing_energy_plus_up);
                this.chart1.Series.Add(climbing_energy_plus_down);
                this.chart1.Series.Add(acc_energy);
                this.chart1.Series.Add(regene_loss);
                this.chart1.Series.Add(convert_loss_minus);
                this.chart1.Series.Add(climbing_energy_minus_up);
                this.chart1.Series.Add(climbing_energy_minus_down);
                this.chart1.Series.Add(regene_energy);
                #endregion
                #endregion
            }
            else if (content == "ConsumedEnergy")
            {
                chartArea1.AxisY.Title = "Power[kW]";
                //chartArea1.AxisY.MajorGrid.Interval = 25D;
                #region 消費エネルギー
                System.Windows.Forms.DataVisualization.Charting.Series consumed_energy = new System.Windows.Forms.DataVisualization.Charting.Series();

                consumed_energy.ChartArea = "ChartArea1";
                consumed_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                consumed_energy.Color = System.Drawing.Color.Red;
                consumed_energy.Legend = "Legend1";
                consumed_energy.Name = "ConsumedEnergy";
                consumed_energy.XValueMember = "time";
                consumed_energy.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                consumed_energy.YValueMembers = "CONSUMED_ELECTRIC_ENERGY";

                this.chart1.Series.Add(consumed_energy);
                #endregion
            }
            else if (content == "LostEnergy")
            {
                //chartArea1.AxisY.MajorGrid.Interval = 25D;
                chartArea1.AxisY.Title = "Power[kW]";
                #region エネルギーロス
                //System.Windows.Forms.DataVisualization.Charting.Series lost_energy = new System.Windows.Forms.DataVisualization.Charting.Series();

                //lost_energy.ChartArea = "ChartArea1";
                //lost_energy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                //lost_energy.Color = System.Drawing.Color.Red;
                //lost_energy.Legend = "Legend1";
                //lost_energy.Name = "瞬間のエネルギーロス[kW]";
                //lost_energy.XValueMember = "time";
                //lost_energy.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                //lost_energy.YValueMembers = "LOST_ENERGY";

                //this.chart1.Series.Add(lost_energy);

                #region インスタンス作成
                // Consumed_energy > 0
                System.Windows.Forms.DataVisualization.Charting.Series air_energy_plus = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series rolling_energy_plus = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series convert_loss_plus = new System.Windows.Forms.DataVisualization.Charting.Series();
                // Consumed_energy < 0
                System.Windows.Forms.DataVisualization.Charting.Series air_energy_minus = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series rolling_energy_minus = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series convert_loss_minus = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series regene_loss = new System.Windows.Forms.DataVisualization.Charting.Series();
                #endregion

                #region 設定
                #region 空気抵抗
                air_energy_plus.ChartArea = "ChartArea1";
                air_energy_plus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                air_energy_plus.Color = System.Drawing.Color.Yellow;
                air_energy_plus.Legend = "Legend1";
                air_energy_plus.Name = "空気抵抗(力行時)";
                air_energy_plus.XValueMember = "time";
                air_energy_plus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                air_energy_plus.YValueMembers = "AIR_ENERGY_PLUS";

                air_energy_minus.ChartArea = "ChartArea1";
                air_energy_minus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                air_energy_minus.Color = System.Drawing.Color.Silver;
                air_energy_minus.Legend = "Legend1";
                air_energy_minus.Name = "空気抵抗(回生時)";
                air_energy_minus.XValueMember = "time";
                air_energy_minus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                air_energy_minus.YValueMembers = "AIR_ENERGY_MINUS";
                #endregion

                #region 転がり抵抗
                rolling_energy_plus.ChartArea = "ChartArea1";
                rolling_energy_plus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                rolling_energy_plus.Color = System.Drawing.Color.SandyBrown;
                rolling_energy_plus.Legend = "Legend1";
                rolling_energy_plus.Name = "転がり抵抗(力行時)";
                rolling_energy_plus.XValueMember = "time";
                rolling_energy_plus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                rolling_energy_plus.YValueMembers = "ROLLING_ENERGY_PLUS";

                rolling_energy_minus.ChartArea = "ChartArea1";
                rolling_energy_minus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                rolling_energy_minus.Color = System.Drawing.Color.SlateGray;
                rolling_energy_minus.Legend = "Legend1";
                rolling_energy_minus.Name = "転がり抵抗(回生時)";
                rolling_energy_minus.XValueMember = "time";
                rolling_energy_minus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                rolling_energy_minus.YValueMembers = "ROLLING_ENERGY_MINUS";
                #endregion

                #region 変換ロス
                convert_loss_plus.ChartArea = "ChartArea1";
                convert_loss_plus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                convert_loss_plus.Color = System.Drawing.Color.Red;
                convert_loss_plus.Legend = "Legend1";
                convert_loss_plus.Name = "エネルギー変換ロス(力行時)";
                convert_loss_plus.XValueMember = "time";
                convert_loss_plus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                convert_loss_plus.YValueMembers = "CONVERT_LOSS_PLUS";

                convert_loss_minus.ChartArea = "ChartArea1";
                convert_loss_minus.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                convert_loss_minus.Color = System.Drawing.Color.Red;
                convert_loss_minus.Legend = "Legend1";
                convert_loss_minus.Name = "エネルギー変換ロス(回生時)";
                convert_loss_minus.XValueMember = "time";
                convert_loss_minus.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                convert_loss_minus.YValueMembers = "ABS_CONVERT_LOSS_MINUS";
                #endregion

                #region 回生ロス
                regene_loss.ChartArea = "ChartArea1";
                regene_loss.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                regene_loss.Color = System.Drawing.Color.Orchid;
                regene_loss.Legend = "Legend1";
                regene_loss.Name = "回生ロス";
                regene_loss.XValueMember = "time";
                regene_loss.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                regene_loss.YValueMembers = "ABS_REGENE_LOSS";
                #endregion
                #endregion

                #region Seriesに追加
                this.chart1.Series.Add(air_energy_plus);
                this.chart1.Series.Add(air_energy_minus);
                this.chart1.Series.Add(rolling_energy_plus);
                this.chart1.Series.Add(rolling_energy_minus);
                this.chart1.Series.Add(convert_loss_plus);
                this.chart1.Series.Add(regene_loss);
                this.chart1.Series.Add(convert_loss_minus);
                #endregion
                #endregion
            }
            else if (content == "Speed")
            {
                showPropertyToolStripMenuItem.Enabled = false;
                chartArea1.AxisY.Title = "Speed[km/h]";
                #region 速度
                System.Windows.Forms.DataVisualization.Charting.Series speed = new System.Windows.Forms.DataVisualization.Charting.Series();

                speed.ChartArea = "ChartArea1";
                speed.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                speed.Color = System.Drawing.Color.Red;
                speed.Legend = "Legend1";
                speed.Name = "Speed";
                speed.XValueMember = "time";
                speed.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                speed.YValueMembers = "SPEED";

                this.chart1.Series.Add(speed);
                #endregion
            }
            else if (content == "LongitudinalAcc")
            {
                showPropertyToolStripMenuItem.Enabled = false;
                chartArea1.AxisY.Title = "Acceleration[m/s^2]";
                #region 進行方向加速度
                System.Windows.Forms.DataVisualization.Charting.Series LongitudinalAcc = new System.Windows.Forms.DataVisualization.Charting.Series();

                LongitudinalAcc.ChartArea = "ChartArea1";
                LongitudinalAcc.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                LongitudinalAcc.Color = System.Drawing.Color.Red;
                LongitudinalAcc.Legend = "Legend1";
                LongitudinalAcc.Name = "LongitudinalAcc";
                LongitudinalAcc.XValueMember = "time";
                LongitudinalAcc.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                LongitudinalAcc.YValueMembers = "LONGITUDINAL_ACC";

                this.chart1.Series.Add(LongitudinalAcc);
                #endregion
            }
            else if (content == "LateralAcc")
            {
                showPropertyToolStripMenuItem.Enabled = false;
                chartArea1.AxisY.Title = "Acceleration[m/s^2]";
                #region 左右方向加速度
                System.Windows.Forms.DataVisualization.Charting.Series LateralAcc = new System.Windows.Forms.DataVisualization.Charting.Series();

                LateralAcc.ChartArea = "ChartArea1";
                LateralAcc.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                LateralAcc.Color = System.Drawing.Color.Red;
                LateralAcc.Legend = "Legend1";
                LateralAcc.Name = "LateralAcc";
                LateralAcc.XValueMember = "time";
                LateralAcc.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                LateralAcc.YValueMembers = "LATERAL_ACC";

                this.chart1.Series.Add(LateralAcc);
                #endregion
            }
            #endregion

            user = new FormData(u);

            dt = new DataTable();

            start_time = t1;
            end_time = t2;

            if (!str4.Equals("0"))
            {
                chartArea1.AxisX.Maximum = Double.Parse(str4);
            }

            if (!str5.Equals("0"))
            {
                chartArea1.AxisY.Maximum = Double.Parse(str5);
            }

            if (!str6.Equals("0"))
            {
                chartArea1.AxisY.Minimum = Double.Parse(str6);
            }

            PaintChart();
            EnergySummation();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="outward">行きトリップ</param>
        /// <param name="homeward">帰りトリップ</param>
        public TimeChart(FormData outward, FormData homeward)
        {
            InitializeComponent();

            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);

            showPropertyToolStripMenuItem.Enabled = false;

            #region 表示内容設定

            chartArea1.AxisX.Title = "Time";
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            chartArea1.AxisY.Title = "Energy[kWh]";
            chartArea1.AxisY.MajorGrid.Interval = 1D;
            chartArea1.AxisX.LabelStyle.Interval = 3600;

            #region インスタンス作成
            System.Windows.Forms.DataVisualization.Charting.Series air_energy_series = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series rolling_energy_series = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series climbing_energy_plus_series = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series climbing_energy_minus_series = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series acc_energy_series = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series convert_loss_series = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series regene_loss_series = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series electric_energy_series = new System.Windows.Forms.DataVisualization.Charting.Series();
            #endregion

            #region 設定
            #region 電気エネルギー
            electric_energy_series.ChartArea = "ChartArea1";
            electric_energy_series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            electric_energy_series.Color = System.Drawing.Color.MediumSlateBlue;
            electric_energy_series.Legend = "Legend1";
            electric_energy_series.Name = "ElectricEnergy-in-EV";
            electric_energy_series.XValueMember = "time";
            electric_energy_series.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            electric_energy_series.YValueMembers = "OTHER_ENERGY";
            electric_energy_series.BorderWidth = 0;
            #endregion

            #region 運動エネルギー＝加速抵抗
            acc_energy_series.ChartArea = "ChartArea1";
            acc_energy_series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            acc_energy_series.Color = System.Drawing.Color.LimeGreen;
            acc_energy_series.Legend = "Legend1";
            acc_energy_series.Name = "KineticEnergy";
            acc_energy_series.XValueMember = "time";
            acc_energy_series.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            acc_energy_series.YValueMembers = "ACC_ENERGY";
            acc_energy_series.BorderWidth = 0;
            #endregion

            #region 空気抵抗
            air_energy_series.ChartArea = "ChartArea1";
            air_energy_series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            air_energy_series.Color = System.Drawing.Color.Yellow;
            air_energy_series.Legend = "Legend1";
            air_energy_series.Name = "AirResistanceLoss";
            air_energy_series.XValueMember = "time";
            air_energy_series.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            air_energy_series.YValueMembers = "AIR_ENERGY";
            air_energy_series.BorderWidth = 0;
            #endregion

            #region 転がり抵抗
            rolling_energy_series.ChartArea = "ChartArea1";
            rolling_energy_series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            rolling_energy_series.Color = System.Drawing.Color.SandyBrown;
            rolling_energy_series.Legend = "Legend1";
            rolling_energy_series.Name = "RollingResistanceLoss";
            rolling_energy_series.XValueMember = "time";
            rolling_energy_series.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            rolling_energy_series.YValueMembers = "ROLLING_ENERGY";
            rolling_energy_series.BorderWidth = 0;
            #endregion

            #region 登坂抵抗
            climbing_energy_plus_series.ChartArea = "ChartArea1";
            climbing_energy_plus_series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            climbing_energy_plus_series.Color = System.Drawing.Color.MediumBlue;
            climbing_energy_plus_series.Legend = "Legend1";
            climbing_energy_plus_series.Name = "PotentialEnergy";
            climbing_energy_plus_series.XValueMember = "time";
            climbing_energy_plus_series.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            climbing_energy_plus_series.YValueMembers = "CLIMBING_ENERGY_PLUS";
            climbing_energy_plus_series.BorderWidth = 0;

            climbing_energy_minus_series.ChartArea = "ChartArea1";
            climbing_energy_minus_series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            climbing_energy_minus_series.Color = System.Drawing.Color.LightSkyBlue;
            climbing_energy_minus_series.Legend = "Legend1";
            climbing_energy_minus_series.Name = "Spouted-PotentialEnergy";
            climbing_energy_minus_series.XValueMember = "time";
            climbing_energy_minus_series.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            climbing_energy_minus_series.YValueMembers = "CLIMBING_ENERGY_MINUS";
            climbing_energy_minus_series.BorderWidth = 0;
            #endregion

            #region 変換ロス
            convert_loss_series.ChartArea = "ChartArea1";
            convert_loss_series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            convert_loss_series.Color = System.Drawing.Color.Red;
            convert_loss_series.Legend = "Legend1";
            convert_loss_series.Name = "ConvertingLoss";
            convert_loss_series.XValueMember = "time";
            convert_loss_series.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            convert_loss_series.YValueMembers = "CONVERT_LOSS";
            convert_loss_series.BorderWidth = 0;
            #endregion

            #region 回生ロス
            regene_loss_series.ChartArea = "ChartArea1";
            regene_loss_series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            regene_loss_series.Color = System.Drawing.Color.Orchid;
            regene_loss_series.Legend = "Legend1";
            regene_loss_series.Name = "RegeneratingLoss";
            regene_loss_series.XValueMember = "time";
            regene_loss_series.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            regene_loss_series.YValueMembers = "REGENE_LOSS";
            regene_loss_series.BorderWidth = 0;
            #endregion
            #endregion

            #region Seriesに追加
            this.chart1.Series.Add(electric_energy_series);
            this.chart1.Series.Add(climbing_energy_plus_series);
            this.chart1.Series.Add(acc_energy_series);
            this.chart1.Series.Add(regene_loss_series);
            this.chart1.Series.Add(convert_loss_series);
            this.chart1.Series.Add(air_energy_series);
            this.chart1.Series.Add(rolling_energy_series);
            this.chart1.Series.Add(climbing_energy_minus_series);
            #endregion
            #endregion

            dt = new DataTable();

            start_time = DateTime.Parse(outward.startTime);
            end_time = DateTime.Parse(homeward.endTime);

            DateTime time = new DateTime();

            #region PaintChart
            #region データの取得
            string query = "select ECOLOG.JST, CONVERT(nchar(8), ECOLOG.JST, 108) as time, TERRAIN_ALTITUDE_DIFFERENCE, SPEED, ENERGY_BY_AIR_RESISTANCE, ENERGY_BY_ROLLING_RESISTANCE, ENERGY_BY_CLIMBING_RESISTANCE, ENERGY_BY_ACC_RESISTANCE, CONVERT_LOSS, REGENE_LOSS, REGENE_ENERGY, CONSUMED_ELECTRIC_ENERGY  ";
            query += "from ECOLOG   ";
            query += "where ECOLOG.TRIP_ID = " + outward.tripID + " ";
            query += "order by ECOLOG.JST ";

            //QueryView form = new QueryView(query);

            //form.ShowDialog(this);

            //if (form.DialogResult == DialogResult.OK)
            //{
            //    query = form.GetQuery();
            //}

            DataTable dt_PowerModel = new DataTable();
            dt_PowerModel = DatabaseAccess.GetResult(query);
            #endregion

            #region テーブルの設定
            double air = 0.0, rolling = 0.0, climbing = 0.0, acc = 0.0, convertL = 0.0, regeneL = 0.0, energy = 6.0;

            // 列の追加
            dt.Columns.Add("time");
            dt.Columns.Add("AIR_ENERGY");
            dt.Columns.Add("ROLLING_ENERGY");
            dt.Columns.Add("CLIMBING_ENERGY_PLUS");
            dt.Columns.Add("CLIMBING_ENERGY_MINUS");
            dt.Columns.Add("ACC_ENERGY");
            dt.Columns.Add("CONVERT_LOSS");
            dt.Columns.Add("REGENE_LOSS");
            dt.Columns.Add("OTHER_ENERGY");
            // 行の追加 
            int j = 0;
            for (int i = 0; i < dt_PowerModel.Rows.Count; i++)
            {
                // 値の更新
                air += (float)dt_PowerModel.Rows[i]["ENERGY_BY_AIR_RESISTANCE"];
                rolling += (float)dt_PowerModel.Rows[i]["ENERGY_BY_ROLLING_RESISTANCE"];
                //climbing_energy += (float)dt_PowerModel.Rows[i]["ENERGY_BY_CLIMBING_RESISTANCE"];
                climbing += 1600 * 9.8 * (float)dt_PowerModel.Rows[i]["TERRAIN_ALTITUDE_DIFFERENCE"] / 3600000;
                acc = Math.Abs(0.5 * 1600 * ((float)dt_PowerModel.Rows[i]["SPEED"] / 3.6) * ((float)dt_PowerModel.Rows[i]["SPEED"] / 3.6) / 3600000);

                if ((float)dt_PowerModel.Rows[i]["CONSUMED_ELECTRIC_ENERGY"] >= 0)
                {
                    convertL += Math.Abs((float)dt_PowerModel.Rows[i]["CONVERT_LOSS"]);
                }
                else
                {
                    convertL += Math.Abs((float)dt_PowerModel.Rows[i]["CONVERT_LOSS"]);
                    regeneL += Math.Abs((float)dt_PowerModel.Rows[i]["REGENE_LOSS"]);
                }

                // air, rolling, acc : 正のみ
                // convert, regene : 正負(意味は同じ)
                // climbing: 正の時は位置エネルギーとして保持、負の時は湧き出し
                energy = 12.0 - air - rolling - (climbing > 0 ? climbing : 0) - acc - convertL - regeneL;

                dt.Rows.Add();
                dt.Rows[j]["time"] = dt_PowerModel.Rows[i]["time"];
                time = DateTime.Parse(dt_PowerModel.Rows[i]["time"].ToString());

                dt.Rows[j]["AIR_ENERGY"] = air;
                dt.Rows[j]["ROLLING_ENERGY"] = rolling;

                if (climbing > 0)
                {
                    dt.Rows[j]["CLIMBING_ENERGY_PLUS"] = Math.Abs(climbing);
                    dt.Rows[j]["CLIMBING_ENERGY_MINUS"] = 0;
                }
                else
                {
                    dt.Rows[j]["CLIMBING_ENERGY_PLUS"] = 0;
                    dt.Rows[j]["CLIMBING_ENERGY_MINUS"] = Math.Abs(climbing);
                }

                dt.Rows[j]["ACC_ENERGY"] = acc;
                dt.Rows[j]["CONVERT_LOSS"] = convertL;
                dt.Rows[j]["REGENE_LOSS"] = regeneL;
                dt.Rows[j]["OTHER_ENERGY"] = energy;
                j++;
            }
            #endregion

            #region データの取得
            query = "select ECOLOG.JST, CONVERT(nchar(8), ECOLOG.JST, 108) as time, TERRAIN_ALTITUDE_DIFFERENCE, SPEED, ENERGY_BY_AIR_RESISTANCE, ENERGY_BY_ROLLING_RESISTANCE, ENERGY_BY_CLIMBING_RESISTANCE, ENERGY_BY_ACC_RESISTANCE, CONVERT_LOSS, REGENE_LOSS, REGENE_ENERGY, CONSUMED_ELECTRIC_ENERGY  ";
            query += "from ECOLOG   ";
            query += "where ECOLOG.TRIP_ID = " + homeward.tripID + " ";
            query += "order by ECOLOG.JST ";

            //QueryView form1 = new QueryView(query);

            //form1.ShowDialog(this);

            //if (form1.DialogResult == DialogResult.OK)
            //{
            //    query = form1.GetQuery();
            //}

            dt_PowerModel = DatabaseAccess.GetResult(query);
            #endregion

            #region テーブルの設定
            time = time.AddMinutes(1);
            TimeSpan ts = DateTime.Parse(dt_PowerModel.Rows[0]["time"].ToString()) - time;

            while (ts.TotalSeconds > 0)
            {
                dt.Rows.Add();
                dt.Rows[j]["time"] = time;
                dt.Rows[j]["AIR_ENERGY"] = air;
                dt.Rows[j]["ROLLING_ENERGY"] = rolling;

                if (climbing > 0)
                {
                    dt.Rows[j]["CLIMBING_ENERGY_PLUS"] = Math.Abs(climbing);
                    dt.Rows[j]["CLIMBING_ENERGY_MINUS"] = 0;
                }
                else
                {
                    dt.Rows[j]["CLIMBING_ENERGY_PLUS"] = 0;
                    dt.Rows[j]["CLIMBING_ENERGY_MINUS"] = Math.Abs(climbing);
                }

                dt.Rows[j]["ACC_ENERGY"] = acc;
                dt.Rows[j]["CONVERT_LOSS"] = convertL;
                dt.Rows[j]["REGENE_LOSS"] = regeneL;
                dt.Rows[j]["OTHER_ENERGY"] = energy;

                j++;
                time = time.AddMinutes(1);
                ts = DateTime.Parse(dt_PowerModel.Rows[0]["time"].ToString()) - time;
            }

            for (int i = 0; i < dt_PowerModel.Rows.Count; i++)
            {
                // 値の更新
                air += (float)dt_PowerModel.Rows[i]["ENERGY_BY_AIR_RESISTANCE"];
                rolling += (float)dt_PowerModel.Rows[i]["ENERGY_BY_ROLLING_RESISTANCE"];
                //climbing_energy += (float)dt_PowerModel.Rows[i]["ENERGY_BY_CLIMBING_RESISTANCE"];
                climbing += 1600 * 9.8 * (float)dt_PowerModel.Rows[i]["TERRAIN_ALTITUDE_DIFFERENCE"] / 3600000;
                acc = Math.Abs(0.5 * 1600 * ((float)dt_PowerModel.Rows[i]["SPEED"] / 3.6) * ((float)dt_PowerModel.Rows[i]["SPEED"] / 3.6) / 3600000);

                if ((float)dt_PowerModel.Rows[i]["CONSUMED_ELECTRIC_ENERGY"] >= 0)
                {
                    convertL += Math.Abs((float)dt_PowerModel.Rows[i]["CONVERT_LOSS"]);
                }
                else
                {
                    convertL += Math.Abs((float)dt_PowerModel.Rows[i]["CONVERT_LOSS"]);
                    regeneL += Math.Abs((float)dt_PowerModel.Rows[i]["REGENE_LOSS"]);
                }

                // air, rolling, acc : 正のみ
                // convert, regene : 正負(意味は同じ)
                // climbing: 正の時は位置エネルギーとして保持、負の時は湧き出し
                energy = 12.0 - air - rolling - (climbing > 0 ? climbing : 0) - acc - convertL - regeneL;

                dt.Rows.Add();
                dt.Rows[j]["time"] = dt_PowerModel.Rows[i]["time"];
                dt.Rows[j]["AIR_ENERGY"] = air;
                dt.Rows[j]["ROLLING_ENERGY"] = rolling;

                if (climbing > 0)
                {
                    dt.Rows[j]["CLIMBING_ENERGY_PLUS"] = Math.Abs(climbing);
                    dt.Rows[j]["CLIMBING_ENERGY_MINUS"] = 0;
                }
                else
                {
                    dt.Rows[j]["CLIMBING_ENERGY_PLUS"] = 0;
                    dt.Rows[j]["CLIMBING_ENERGY_MINUS"] = Math.Abs(climbing);
                }

                dt.Rows[j]["ACC_ENERGY"] = acc;
                dt.Rows[j]["CONVERT_LOSS"] = convertL;
                dt.Rows[j]["REGENE_LOSS"] = regeneL;
                dt.Rows[j]["OTHER_ENERGY"] = energy;
                j++;


            }
            #endregion

            chart1.DataSource = dt;
            #endregion

            #region EnergySummation
            query = "select SUM(ENERGY_BY_AIR_RESISTANCE) as SUM_AIR_ENERGY, SUM(ENERGY_BY_ROLLING_RESISTANCE) as SUM_ROLLING_ENERGY, SUM(case when ENERGY_BY_CLIMBING_RESISTANCE >= 0 then ENERGY_BY_CLIMBING_RESISTANCE else 0 end) as SUM_CLIMBING_ENERGY, SUM(case when ENERGY_BY_ACC_RESISTANCE >= 0 then ENERGY_BY_ACC_RESISTANCE else 0 end) as SUM_ACC_ENERGY, SUM(ABS(CONVERT_LOSS)) as SUM_CONVERT_LOSS, SUM(ABS(REGENE_ENERGY)) as SUM_REGENE_ENERGY, SUM(ABS(REGENE_LOSS)) as SUM_REGENE_LOSS , SUM(ABS(LOST_ENERGY)) as SUM_LOST_ENERGY, SUM(CONSUMED_ELECTRIC_ENERGY) as SUM_CONSUMED_E ";
            query += "from ECOLOG ";
            query += "where JST between '" + start_time + "' and '" + end_time + "'    ";
            query += "and ( TRIP_ID = " + outward.tripID + " or TRIP_ID = " + homeward.tripID + ") ";

            DataTable dt_sum = new DataTable();
            dt_sum = DatabaseAccess.GetResult(query);

            air_energy = Math.Round(double.Parse(dt_sum.Rows[0][0].ToString()), 4).ToString();
            rolling_energy = Math.Round(double.Parse(dt_sum.Rows[0][1].ToString()), 4).ToString();
            climbing_energy = Math.Round(double.Parse(dt_sum.Rows[0][2].ToString()), 4).ToString();
            acc_energy = Math.Round(double.Parse(dt_sum.Rows[0][3].ToString()), 4).ToString();
            convert_loss = Math.Round(double.Parse(dt_sum.Rows[0][4].ToString()), 4).ToString();
            regene_energy = Math.Round(double.Parse(dt_sum.Rows[0][5].ToString()), 4).ToString();
            regene_loss = Math.Round(double.Parse(dt_sum.Rows[0][6].ToString()), 4).ToString();
            lost_energy = Math.Round(double.Parse(dt_sum.Rows[0][7].ToString()), 4).ToString();
            consumed_energy = Math.Round(double.Parse(dt_sum.Rows[0][8].ToString()), 4).ToString();
            #endregion

            content = "PowerModel";
        }

        private void EnergySummation()
        {
            string query = "select SUM(ENERGY_BY_AIR_RESISTANCE) as SUM_AIR_ENERGY, SUM(ENERGY_BY_ROLLING_RESISTANCE) as SUM_ROLLING_ENERGY, SUM(case when ENERGY_BY_CLIMBING_RESISTANCE >= 0 then ENERGY_BY_CLIMBING_RESISTANCE else 0 end) as SUM_CLIMBING_ENERGY, SUM(case when ENERGY_BY_ACC_RESISTANCE >= 0 then ENERGY_BY_ACC_RESISTANCE else 0 end) as SUM_ACC_ENERGY, SUM(ABS(CONVERT_LOSS)) as SUM_CONVERT_LOSS, SUM(ABS(REGENE_ENERGY)) as SUM_REGENE_ENERGY, SUM(ABS(REGENE_LOSS)) as SUM_REGENE_LOSS , SUM(ABS(LOST_ENERGY)) as SUM_LOST_ENERGY, SUM(CONSUMED_ELECTRIC_ENERGY) as SUM_CONSUMED_E ";
            query += "from ECOLOG ";
            query += "where JST between '" + start_time + "' and '" + end_time + "'    ";
            query += "and TRIP_ID = " + user.tripID + " ";

            if (user.usefixed)
            {
                query = query.Replace("ECOLOG", "ECOLOG_ALTITUDE_FIXED");
            }

            DataTable dt = new DataTable();
            dt = DatabaseAccess.GetResult(query);

            air_energy = Math.Round(double.Parse(dt.Rows[0][0].ToString()), 4).ToString();
            rolling_energy = Math.Round(double.Parse(dt.Rows[0][1].ToString()), 4).ToString();
            climbing_energy = Math.Round(double.Parse(dt.Rows[0][2].ToString()), 4).ToString();
            acc_energy = Math.Round(double.Parse(dt.Rows[0][3].ToString()), 4).ToString();
            convert_loss = Math.Round(double.Parse(dt.Rows[0][4].ToString()), 4).ToString();
            regene_energy = Math.Round(double.Parse(dt.Rows[0][5].ToString()), 4).ToString();
            regene_loss = Math.Round(double.Parse(dt.Rows[0][6].ToString()), 4).ToString();
            lost_energy = Math.Round(double.Parse(dt.Rows[0][7].ToString()), 4).ToString();
            consumed_energy = Math.Round(double.Parse(dt.Rows[0][8].ToString()), 4).ToString();
        }

        private void PaintChart()
        {
            string query = "";

            if (content == "EnergyModel")
            {
                #region 資料用
                //query = "select ECOLOG.JST, CONVERT(varchar(10), CONVERT(time(0), ECOLOG.JST)) as time, TERRAIN_ALTITUDE_DIFFERENCE, SPEED, ENERGY_BY_AIR_RESISTANCE, ENERGY_BY_ROLLING_RESISTANCE, ENERGY_BY_CLIMBING_RESISTANCE, ENERGY_BY_ACC_RESISTANCE, CONVERT_LOSS, REGENE_LOSS, REGENE_ENERGY, CONSUMED_ELECTRIC_ENERGY  ";
                //query += "from ECOLOG   ";
                //query += "where ECOLOG.TRIP_ID = " + 976 + " ";
                //query += "order by ECOLOG.JST ";

                //DataTable dt_PowerModel = new DataTable();
                //dt_PowerModel = Program.Get_Result(query);

                //#region テーブルの設定
                //double air_energy = 0.0, rolling_energy = 0.0, climbing_energy = 0.0, acc_energy = 0.0, convert_loss = 0.0, regene_loss = 0.0, electric_energy = 6.0;

                //// 列の追加
                //dt.Columns.Add("time");
                //dt.Columns.Add("AIR_ENERGY");
                //dt.Columns.Add("ROLLING_ENERGY");
                //dt.Columns.Add("CLIMBING_ENERGY_PLUS");
                //dt.Columns.Add("CLIMBING_ENERGY_MINUS");
                //dt.Columns.Add("ACC_ENERGY");
                //dt.Columns.Add("CONVERT_LOSS");
                //dt.Columns.Add("REGENE_LOSS");
                //dt.Columns.Add("OTHER_ENERGY");
                //// 行の追加 
                //int j = 0;
                //for (int i = 0; i < dt_PowerModel.Rows.Count; i++)
                //{
                //    // 値の更新
                //    air_energy += (float)dt_PowerModel.Rows[i]["ENERGY_BY_AIR_RESISTANCE"];
                //    rolling_energy += (float)dt_PowerModel.Rows[i]["ENERGY_BY_ROLLING_RESISTANCE"];
                //    //climbing_energy += (float)dt_PowerModel.Rows[i]["ENERGY_BY_CLIMBING_RESISTANCE"];
                //    climbing_energy += 1500 * 9.8 * (float)dt_PowerModel.Rows[i]["TERRAIN_ALTITUDE_DIFFERENCE"] / 3600000;
                //    acc_energy = Math.Abs(0.5 * 1500 * ((float)dt_PowerModel.Rows[i]["SPEED"] / 3.6) * ((float)dt_PowerModel.Rows[i]["SPEED"] / 3.6) / 3600000);

                //    if ((float)dt_PowerModel.Rows[i]["CONSUMED_ELECTRIC_ENERGY"] >= 0)
                //    {
                //        convert_loss += Math.Abs((float)dt_PowerModel.Rows[i]["CONVERT_LOSS"]);
                //    }
                //    else
                //    {
                //        convert_loss += Math.Abs((float)dt_PowerModel.Rows[i]["CONVERT_LOSS"]);
                //        regene_loss += Math.Abs((float)dt_PowerModel.Rows[i]["REGENE_LOSS"]);
                //    }

                //    // air, rolling, acc : 正のみ
                //    // convert, regene : 正負(意味は同じ)
                //    // climbing: 正の時は位置エネルギーとして保持、負の時は湧き出し
                //    electric_energy = 12.0 - air_energy - rolling_energy - (climbing_energy > 0 ? climbing_energy : 0) - acc_energy - convert_loss - regene_loss;
                //}
                //#endregion

                //query = "select ECOLOG.JST, CONVERT(varchar(10), CONVERT(time(0), ECOLOG.JST)) as time, TERRAIN_ALTITUDE_DIFFERENCE, SPEED, ENERGY_BY_AIR_RESISTANCE, ENERGY_BY_ROLLING_RESISTANCE, ENERGY_BY_CLIMBING_RESISTANCE, ENERGY_BY_ACC_RESISTANCE, CONVERT_LOSS, REGENE_LOSS, REGENE_ENERGY, CONSUMED_ELECTRIC_ENERGY  ";
                //query += "from ECOLOG   ";
                //query += "where ECOLOG.TRIP_ID = " + 985 + " ";
                //query += "order by ECOLOG.JST ";

                //dt_PowerModel = Program.Get_Result(query);

                //#region テーブルの設定
                //for (int i = 0; i < dt_PowerModel.Rows.Count; i++)
                //{
                //    // 値の更新
                //    air_energy += (float)dt_PowerModel.Rows[i]["ENERGY_BY_AIR_RESISTANCE"];
                //    rolling_energy += (float)dt_PowerModel.Rows[i]["ENERGY_BY_ROLLING_RESISTANCE"];
                //    //climbing_energy += (float)dt_PowerModel.Rows[i]["ENERGY_BY_CLIMBING_RESISTANCE"];
                //    climbing_energy += 1500 * 9.8 * (float)dt_PowerModel.Rows[i]["TERRAIN_ALTITUDE_DIFFERENCE"] / 3600000;
                //    acc_energy = Math.Abs(0.5 * 1500 * ((float)dt_PowerModel.Rows[i]["SPEED"] / 3.6) * ((float)dt_PowerModel.Rows[i]["SPEED"] / 3.6) / 3600000);

                //    if ((float)dt_PowerModel.Rows[i]["CONSUMED_ELECTRIC_ENERGY"] >= 0)
                //    {
                //        convert_loss += Math.Abs((float)dt_PowerModel.Rows[i]["CONVERT_LOSS"]);
                //    }
                //    else
                //    {
                //        convert_loss += Math.Abs((float)dt_PowerModel.Rows[i]["CONVERT_LOSS"]);
                //        regene_loss += Math.Abs((float)dt_PowerModel.Rows[i]["REGENE_LOSS"]);
                //    }

                //    // air, rolling, acc : 正のみ
                //    // convert, regene : 正負(意味は同じ)
                //    // climbing: 正の時は位置エネルギーとして保持、負の時は湧き出し
                //    electric_energy = 12.0 - air_energy - rolling_energy - (climbing_energy > 0 ? climbing_energy : 0) - acc_energy - convert_loss - regene_loss;


                //    dt.Rows.Add();
                //    dt.Rows[j]["time"] = dt_PowerModel.Rows[i]["time"];
                //    dt.Rows[j]["AIR_ENERGY"] = air_energy;
                //    dt.Rows[j]["ROLLING_ENERGY"] = rolling_energy;

                //    if (climbing_energy > 0)
                //    {
                //        dt.Rows[j]["CLIMBING_ENERGY_PLUS"] = Math.Abs(climbing_energy);
                //        dt.Rows[j]["CLIMBING_ENERGY_MINUS"] = 0;
                //    }
                //    else
                //    {
                //        dt.Rows[j]["CLIMBING_ENERGY_PLUS"] = 0;
                //        dt.Rows[j]["CLIMBING_ENERGY_MINUS"] = Math.Abs(climbing_energy);
                //    }

                //    dt.Rows[j]["ACC_ENERGY"] = acc_energy;
                //    dt.Rows[j]["CONVERT_LOSS"] = convert_loss;
                //    dt.Rows[j]["REGENE_LOSS"] = regene_loss;
                //    dt.Rows[j]["OTHER_ENERGY"] = electric_energy;
                //    j++;


                //}
                //#endregion

                //chartArea1.AxisY.MajorGrid.Interval = 1D;
                #endregion

                #region エネルギーモデル
                #region クエリ
                query = "select ECOLOG.JST, CONVERT(nchar(8), ECOLOG.JST, 108) as time, TERRAIN_ALTITUDE_DIFFERENCE, SPEED, ENERGY_BY_AIR_RESISTANCE, ENERGY_BY_ROLLING_RESISTANCE, ENERGY_BY_CLIMBING_RESISTANCE, ENERGY_BY_ACC_RESISTANCE, CONVERT_LOSS, REGENE_LOSS, REGENE_ENERGY, CONSUMED_ELECTRIC_ENERGY  ";
                query += "from ECOLOG   ";
                query += "where ECOLOG.TRIP_ID = " + user.tripID + " ";
                query += "order by ECOLOG.JST ";
                #endregion

                DataTable dt_PowerModel = new DataTable();
                dt_PowerModel = DatabaseAccess.GetResult(query);

                #region テーブルの設定
                double air_energy = 0.0, rolling_energy = 0.0, climbing_energy = 0.0, acc_energy = 0.0, convert_loss = 0.0, regene_loss = 0.0, electric_energy = 6.0;

                // 列の追加
                dt.Columns.Add("time");
                dt.Columns.Add("AIR_ENERGY");
                dt.Columns.Add("ROLLING_ENERGY");
                dt.Columns.Add("CLIMBING_ENERGY_PLUS");
                dt.Columns.Add("CLIMBING_ENERGY_MINUS");
                dt.Columns.Add("ACC_ENERGY");
                dt.Columns.Add("CONVERT_LOSS");
                dt.Columns.Add("REGENE_LOSS");
                dt.Columns.Add("OTHER_ENERGY");
                // 行の追加 
                int j = 0;
                for (int i = 0; i < dt_PowerModel.Rows.Count; i++)
                {
                    // 値の更新
                    air_energy += (float)dt_PowerModel.Rows[i]["ENERGY_BY_AIR_RESISTANCE"];
                    rolling_energy += (float)dt_PowerModel.Rows[i]["ENERGY_BY_ROLLING_RESISTANCE"];
                    //climbing_energy += (float)dt_PowerModel.Rows[i]["ENERGY_BY_CLIMBING_RESISTANCE"];
                    climbing_energy += 1500 * 9.8 * (float)dt_PowerModel.Rows[i]["TERRAIN_ALTITUDE_DIFFERENCE"] / 3600000;
                    acc_energy = Math.Abs(0.5 * 1500 * ((float)dt_PowerModel.Rows[i]["SPEED"] / 3.6) * ((float)dt_PowerModel.Rows[i]["SPEED"] / 3.6) / 3600000);
                    convert_loss += Math.Abs((float)dt_PowerModel.Rows[i]["CONVERT_LOSS"]);
                    regene_loss += Math.Abs((float)dt_PowerModel.Rows[i]["REGENE_LOSS"]);

                    // air, rolling, acc : 正のみ
                    // convert, regene : 正負(意味は同じ)
                    // climbing: 正の時は位置エネルギーとして保持、負の時は湧き出し
                    electric_energy = 12.0 - air_energy - rolling_energy - (climbing_energy > 0 ? climbing_energy : 0) - acc_energy - convert_loss - regene_loss;

                    if (DateTime.Parse(dt_PowerModel.Rows[i]["JST"].ToString()) >= start_time && DateTime.Parse(dt_PowerModel.Rows[i]["JST"].ToString()) <= end_time)
                    {
                        dt.Rows.Add();
                        dt.Rows[j]["time"] = dt_PowerModel.Rows[i]["time"];
                        dt.Rows[j]["AIR_ENERGY"] = air_energy;
                        dt.Rows[j]["ROLLING_ENERGY"] = rolling_energy;

                        if (climbing_energy > 0)
                        {
                            dt.Rows[j]["CLIMBING_ENERGY_PLUS"] = Math.Abs(climbing_energy);
                            dt.Rows[j]["CLIMBING_ENERGY_MINUS"] = 0;
                        }
                        else
                        {
                            dt.Rows[j]["CLIMBING_ENERGY_PLUS"] = 0;
                            dt.Rows[j]["CLIMBING_ENERGY_MINUS"] = Math.Abs(climbing_energy);
                        }

                        dt.Rows[j]["ACC_ENERGY"] = acc_energy;
                        dt.Rows[j]["CONVERT_LOSS"] = convert_loss;
                        dt.Rows[j]["REGENE_LOSS"] = regene_loss;
                        dt.Rows[j]["OTHER_ENERGY"] = electric_energy;
                        j++;
                    }

                }
                #endregion

                #endregion
            }
            else
            {
                #region その他
                query = "select ECOLOG.JST, CONVERT(nchar(8), ECOLOG.JST, 108) as time, SPEED, CONSUMED_ELECTRIC_ENERGY*3600 as CONSUMED_ELECTRIC_ENERGY, LOST_ENERGY*3600 as LOST_ENERGY,  ";
                query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY > 0) THEN ENERGY_BY_AIR_RESISTANCE*3600 ELSE 0 END) as AIR_ENERGY_PLUS,  ";
                query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY <= 0) THEN ENERGY_BY_AIR_RESISTANCE*3600 ELSE 0 END) as AIR_ENERGY_MINUS,  ";
                query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY > 0) THEN ENERGY_BY_ROLLING_RESISTANCE*3600 ELSE 0 END) as ROLLING_ENERGY_PLUS, ";
                query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY <= 0) THEN ENERGY_BY_ROLLING_RESISTANCE*3600 ELSE 0 END) as ROLLING_ENERGY_MINUS, ";
                query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY > 0 and ENERGY_BY_CLIMBING_RESISTANCE > 0) THEN ENERGY_BY_CLIMBING_RESISTANCE*3600 ELSE 0 END) as CLIMBING_ENERGY_PLUS_UP, 0 as CLIMBING_ENERGY_PLUS_DOWN, ";
                query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY <= 0 and ENERGY_BY_CLIMBING_RESISTANCE > 0) THEN ENERGY_BY_CLIMBING_RESISTANCE*3600 ELSE 0 END) as CLIMBING_ENERGY_MINUS_UP, 0 as CLIMBING_ENERGY_MINUS_DOWN, ";
                query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY > 0) THEN ENERGY_BY_ACC_RESISTANCE*3600 ELSE 0 END) as ACC_ENERGY_PLUS, 0 as ACC_ENERGY_MINUS,  ";
                query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY > 0) THEN CONVERT_LOSS*3600 ELSE 0 END) as CONVERT_LOSS_PLUS,  ";
                query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY <= 0) THEN CONVERT_LOSS*3600 ELSE 0 END) as CONVERT_LOSS_MINUS,  ";
                query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY <= 0) THEN REGENE_LOSS*3600 ELSE 0 END) as REGENE_LOSS,  ";
                query += "	(CASE WHEN (CONSUMED_ELECTRIC_ENERGY <= 0) THEN REGENE_ENERGY*3600 ELSE 0 END) as REGENE_ENERGY ";
                query += "from [ECOLOGTable] as ECOLOG ";
                query += "where ECOLOG.TRIP_ID = " + user.tripID + " ";
                query += "and JST between '" + start_time + "' and '" + end_time + "' ";
                query += "order by ECOLOG.JST ";

                query = query.Replace("[ECOLOGTable]", MainForm.ECOLOGTable);

                if (user.usefixed)
                {
                    query = query.Replace("[ECOLOGTable]", "ECOLOG_ALTITUDE_FIXED");
                }

                dt = DatabaseAccess.GetResult(query);
                #endregion
            }

            chart1.DataSource = dt;
        }

        private void showPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!propertyShowed)
            {
                if (content.Equals("PowerModel"))
                {
                    TimeChartProperty property = new TimeChartProperty(this, start_time.ToString(), end_time.ToString(), content, consumed_energy, lost_energy, air_energy, rolling_energy, climbing_energy, acc_energy, convert_loss, regene_loss, regene_energy);
                    MainForm.ShowWindow(property);
                }
                else
                {
                    TimeChartProperty property = new TimeChartProperty(this, start_time.ToString(), end_time.ToString(), content, consumed_energy, lost_energy);
                    MainForm.ShowWindow(property);

                }
                propertyShowed = true;
            }
        }

    }
}
