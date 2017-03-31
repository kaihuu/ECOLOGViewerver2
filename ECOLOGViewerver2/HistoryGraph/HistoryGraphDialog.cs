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
    /// 期間グラフを表示するためのダイアログを取り扱うクラス
    /// </summary>
    public partial class HistoryGraphDialog : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HistoryGraphDialog()
        {
            InitializeComponent();
            //フォームサイズを固定
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            StartTime_dateTimePicker.Value = new System.DateTime(2011, 7, 1, 0, 0, 0, 0);

            EndTime_dateTimePicker.Value = new System.DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0, 0);

            #region Combobox初期化
            #region ドライバー
            foreach (string key in MainForm.Driver.Keys)
            {
                Driver_comboBox.Items.Add(key);
            }
            #endregion

            AggregationcomboBox.SelectedIndex = 0;
            Driver_comboBox.SelectedIndex = 0;
            InfocomboBox.SelectedIndex = 0;
            ChartcomboBox.SelectedIndex = 0;
            #endregion

        }

        // 期間別グラフ
        private void search_button_Click(object sender, EventArgs e)
        {
            string query = "";
            string aggregation = "Month";
            string driverinfo = "CONSUMED_ELECTRIC_ENERGY";
            DataTable dt_aggregation;
            DateTime start_time = StartTime_dateTimePicker.Value;
            DateTime end_time = EndTime_dateTimePicker.Value;

            #region 可視化内容
            switch (InfocomboBox.SelectedIndex)
            {
                case 0:
                    driverinfo = "CONSUMED_ELECTRIC_ENERGY";
                    break;
                case 1:
                    driverinfo = "LOST_ENERGY";
                    break;
                default:
                    break;
            }
            #endregion

            #region 集約単位
            switch (AggregationcomboBox.SelectedIndex)
            {
                case 0:
                    aggregation = "Month";
                    break;
                case 1:
                    aggregation = "Weekday";
                    break;
                //case 2:
                //    aggregation = "Hour";
                //    break;
                default:
                    break;
            }
            #endregion

            if (ChartcomboBox.SelectedIndex == 0)
            {
                #region クエリ設定
                query = "with SubTable as ( ";
                query += "select ECOLOG.TRIP_ID, MIN(JST) as START_TIME, SUM(" + driverinfo + ") as 消費, ";
                query += "SUM(ENERGY_BY_COOLING) as クーラー, ";
                query += "SUM(ENERGY_BY_HEATING) as ヒーター, ";
                query += "SUM(ENERGY_BY_EQUIPMENT) as 電装品, ECOLOG.TRIP_DIRECTION ";
                query += "from ECOLOG as ECOLOG ";
                query += "inner join TRIPS on ECOLOG.TRIP_ID = TRIPS.TRIP_ID ";//
                query += "where ECOLOG.DRIVER_ID = " + MainForm.Driver[Driver_comboBox.Text.ToString()] + " ";
                query += "and JST between '" + start_time.ToString("yyyy-MM-dd") + " 00:00:00' and '" + end_time.AddMonths(1).ToString("yyyy-MM-dd") + " 00:00:00' ";
                query += "and ENERGY_BY_COOLING is not null and ENERGY_BY_HEATING is not null and ENERGY_BY_EQUIPMENT is not null ";
                query += "and TRIPS.VALIDATION is null ";//
                query += "group by ECOLOG.TRIP_ID, ECOLOG.TRIP_DIRECTION ";
                query += "), Out as ( ";
                query += "select DATENAME(year,DATEADD(hour,-1,START_TIME)) as Year, DATENAME(month,DATEADD(hour,-1,START_TIME)) as Month, DATENAME(day,DATEADD(hour,-1,START_TIME)) as Day, AVG(消費) as 消費, AVG(クーラー) as クーラー, AVG(ヒーター) as ヒーター, AVG(電装品) as 電装品 ";
                query += "from SubTable ";
                query += "where TRIP_DIRECTION = 'outward' ";
                query += "group by DATENAME(year,DATEADD(hour,-1,START_TIME)), DATENAME(month,DATEADD(hour,-1,START_TIME)), DATENAME(day,DATEADD(hour,-1,START_TIME)) ";
                query += "), Home as ( ";
                query += "select DATENAME(year,DATEADD(hour,-1,START_TIME)) as Year, DATENAME(month,DATEADD(hour,-1,START_TIME)) as Month, DATENAME(day,DATEADD(hour,-1,START_TIME)) as Day, AVG(消費) as 消費, AVG(クーラー) as クーラー, AVG(ヒーター) as ヒーター, AVG(電装品) as 電装品 ";
                query += "from SubTable ";
                query += "where TRIP_DIRECTION = 'homeward' ";
                query += "group by DATENAME(year,DATEADD(hour,-1,START_TIME)), DATENAME(month,DATEADD(hour,-1,START_TIME)), DATENAME(day,DATEADD(hour,-1,START_TIME)) ";
                query += ") ";
                query += " ";
                query += "select Out.Year+Out.Month as Month, ";
                query += "AVG(Out.消費 + Home.消費) as 消費, ";
                query += "AVG(Out.ヒーター + Home.ヒーター) as ヒーター, ";
                query += "AVG(Out.クーラー + Home.クーラー) as クーラー, ";
                query += "AVG(Out.電装品 + Home.電装品) as 電装品, ";
                query += "ABS(AVG(12 - Out.消費 - Home.消費 - Out.ヒーター - Home.ヒーター - Out.クーラー - Home.クーラー - Out.電装品 - Home.電装品)) as 残余 ";
                query += "from Out, Home ";
                query += "where Out.Year = Home.Year ";
                query += "and Out.Month = Home.Month ";
                query += "and Out.Day = Home.Day ";
                query += "group by Out.Year+Out.Month ";
                #endregion

                dt_aggregation = DatabaseAccess.GetResult(query);
                HistoryGraph columngraph = new HistoryGraph(dt_aggregation, aggregation, driverinfo);
                columngraph.Text = "Column Graph [" + MainForm.Driver[Driver_comboBox.Text.ToString()] + ", " + aggregation + ", " + driverinfo + "]";
                MainForm.ShowWindow(columngraph);
            }
            else
            {
                HistoryGraph columngraph = new HistoryGraph(StartTime_dateTimePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00", EndTime_dateTimePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59", MainForm.Driver[Driver_comboBox.Text.ToString()], aggregation, driverinfo);
                columngraph.Text = "Column Graph [" + MainForm.Driver[Driver_comboBox.Text.ToString()] + ", " + aggregation + ", " + driverinfo + "]";
                MainForm.ShowWindow(columngraph);
            }
        }

        private void ColmunGraphDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Dispose();
            }
        }

    }
}
