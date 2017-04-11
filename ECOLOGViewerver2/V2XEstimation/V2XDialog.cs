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
    /// カレンダー表示ダイアログを取り扱うクラス
    /// </summary>
    public partial class V2XDialog : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public V2XDialog()
        {
            InitializeComponent();
            //フォームサイズを固定
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            #region CheckListBox初期化
            foreach (string key in MainForm.Driver.Keys)
            {
                DrivercheckedListBox.Items.Add(key);
            }
            #endregion
        }
        //カレンダー表示
        private void DisplayCalendar_Click(object sender, EventArgs e)
        {
            string Year = Year_dateTimePicker.Value.ToString("yyyy");
            string Month = Month_dateTimePicker.Value.ToString("MM");
            DataTable dt_calendar;
            Calendar calendar;

            string drivers = "0";

            foreach (object obj in DrivercheckedListBox.CheckedItems)
            {
                drivers += ", ";
                drivers += MainForm.Driver[obj.ToString()];
            }

            drivers = drivers.Replace("0, ", "");

            #region データの取得
            if (DrivercheckedListBox.CheckedItems.Count < 2)
            {
                #region クエリ
                string query = "with SubTable as ( ";
                query += "select ECOLOG.TRIP_ID, DRIVER_ID, MIN(JST) as START_TIME, SUM(CONSUMED_ELECTRIC_ENERGY) as 消費, ";
                query += "SUM(case when CONSUMED_ELECTRIC_ENERGY > 0  then CONSUMED_ELECTRIC_ENERGY else 0 end) as 力行 , ";
                query += "SUM(case when CONSUMED_ELECTRIC_ENERGY < 0  then CONSUMED_ELECTRIC_ENERGY else 0 end) as 回生 , ";
                query += "SUM(ENERGY_BY_COOLING) as クーラー, ";
                query += "SUM(ENERGY_BY_HEATING) as ヒーター, ";
                query += "SUM(ENERGY_BY_EQUIPMENT) as 電装品,  ECOLOG.TRIP_DIRECTION ";
                query += "from ECOLOG as ECOLOG ";
                query += "where DRIVER_ID in (" + drivers + ") ";
                query += "and JST >= '" + Year + "-" + Month + "-01 00:00:00' ";

                if (Month == "12")
                {
                    query += "and JST < '" + (int.Parse(Year) + 1) + "-01-01 00:00:00' ";
                }
                else
                {
                    query += "and JST < '" + Year + "-" + (int.Parse(Month) + 1) + "-01 00:00:00' ";
                }
                query += "and ECOLOG.TRIP_ID != 1442 and ECOLOG.TRIP_Id != 1383";
                query += "group by ECOLOG.TRIP_ID, DRIVER_ID, ECOLOG.TRIP_DIRECTION ";
                query += "), Out as ( ";
                query += "select DRIVER_ID, DATENAME(year,DATEADD(hour,-1,START_TIME)) as Year, DATENAME(month,DATEADD(hour,-1,START_TIME)) as Month, DATENAME(day,DATEADD(hour,-1,START_TIME)) as Day, AVG(消費) as 消費, AVG(力行) as 力行, AVG(回生) as 回生, AVG(クーラー) as クーラー, AVG(ヒーター) as ヒーター, AVG(電装品) as 電装品 ";
                query += "from SubTable ";
                query += "where TRIP_DIRECTION = 'outward' ";
                query += "group by DATENAME(year,DATEADD(hour,-1,START_TIME)), DATENAME(month,DATEADD(hour,-1,START_TIME)), DATENAME(day,DATEADD(hour,-1,START_TIME)), DRIVER_ID ";
                query += "), Home as ( ";
                query += "select DRIVER_ID, DATENAME(year,DATEADD(hour,-1,START_TIME)) as Year, DATENAME(month,DATEADD(hour,-1,START_TIME)) as Month, DATENAME(day,DATEADD(hour,-1,START_TIME)) as Day, AVG(消費) as 消費, AVG(力行) as 力行, AVG(回生) as 回生, AVG(クーラー) as クーラー, AVG(ヒーター) as ヒーター, AVG(電装品) as 電装品 ";
                query += "from SubTable ";
                query += "where TRIP_DIRECTION = 'homeward' ";
                query += "group by DATENAME(year,DATEADD(hour,-1,START_TIME)), DATENAME(month,DATEADD(hour,-1,START_TIME)), DATENAME(day,DATEADD(hour,-1,START_TIME)), DRIVER_ID ";
                query += ") ";
                query += " ";
                query += "select case when Out.Day is NULL then Home.Day else Out.Day end as START_DAY, ";
                if (EquipmentcheckBox.Checked && AirConcheckBox.Checked)
                {
                    query += "round((SUM(Out.消費) + SUM(Out.クーラー) + SUM(Out.ヒーター) + SUM(Out.電装品)),1) as 消費_out, ";
                    query += "round((SUM(Out.力行) + SUM(Out.クーラー) + SUM(Out.ヒーター) + SUM(Out.電装品)),1) as 力行_out, ";
                    query += "round((SUM(Home.消費) + SUM(Home.クーラー) + SUM(Home.ヒーター) + SUM(Home.電装品)),1) as 消費_home, ";
                    query += "round((SUM(Home.力行) + SUM(Home.クーラー) + SUM(Home.ヒーター) + SUM(Home.電装品)),1) as 力行_home, ";
                }
                if (EquipmentcheckBox.Checked && !AirConcheckBox.Checked)
                {
                    query += "round((SUM(Out.消費) + SUM(Out.電装品)),1) as 消費_out, ";
                    query += "round((SUM(Out.力行) + SUM(Out.電装品)),1) as 力行_out, ";
                    query += "round((SUM(Home.消費) + SUM(Home.電装品)),1) as 消費_home, ";
                    query += "round((SUM(Home.力行) + SUM(Home.電装品)),1) as 力行_home, ";
                }
                if (!EquipmentcheckBox.Checked && AirConcheckBox.Checked)
                {
                    query += "round((SUM(Out.消費) + SUM(Out.クーラー) + SUM(Out.ヒーター)),1) as 消費_out, ";
                    query += "round((SUM(Out.力行) + SUM(Out.クーラー) + SUM(Out.ヒーター)),1) as 力行_out, ";
                    query += "round((SUM(Home.消費) + SUM(Home.クーラー) + SUM(Home.ヒーター)),1) as 消費_home, ";
                    query += "round((SUM(Home.力行) + SUM(Home.クーラー) + SUM(Home.ヒーター)),1) as 力行_home, ";
                }
                if (!EquipmentcheckBox.Checked && !AirConcheckBox.Checked)
                {
                    query += "round((SUM(Out.消費)),1) as 消費_out, ";
                    query += "round((SUM(Out.力行)),1) as 力行_out, ";
                    query += "round((SUM(Home.消費)),1) as 消費_home, ";
                    query += "round((SUM(Home.力行)),1) as 力行_home, ";
                }
                query += "round(SUM(Out.回生),1) as 回生_out, ";
                query += "round(SUM(Home.回生),1) as 回生_home, COUNT(*) as 台数 ";
                query += "from Out full outer join Home ";
                query += "on Out.Day = Home.Day ";
                query += "and Out.DRIVER_ID = Home.DRIVER_ID ";
                query += "group by Out.Day, Home.Day ";
                query += "order by Out.Day ";
                #endregion

                dt_calendar = DatabaseAccess.GetResult(query);
                calendar = new Calendar(Year, Month, false);
            }
            else
            {
                #region クエリ
                string query = "with SubTable as ( ";
                query += "select ECOLOG.TRIP_ID, DRIVER_ID, MIN(JST) as START_TIME, SUM(CONSUMED_ELECTRIC_ENERGY) as 消費, ";
                query += "SUM(case when CONSUMED_ELECTRIC_ENERGY > 0  then CONSUMED_ELECTRIC_ENERGY else 0 end) as 力行 , ";
                query += "SUM(case when CONSUMED_ELECTRIC_ENERGY < 0  then CONSUMED_ELECTRIC_ENERGY else 0 end) as 回生 , ";
                query += "SUM(ENERGY_BY_COOLING) as クーラー, ";
                query += "SUM(ENERGY_BY_HEATING) as ヒーター, ";
                query += "SUM(ENERGY_BY_EQUIPMENT) as 電装品,  ECOLOG.TRIP_DIRECTION ";
                query += "from ECOLOG as ECOLOG ";
                query += "where DRIVER_ID in (" + drivers + ") ";
                query += "and JST >= '" + Year + "-" + Month + "-01 00:00:00' ";
                if (Month == "12")
                {
                    query += "and JST < '" + (int.Parse(Year) + 1) + "-01-01 00:00:00' ";
                }
                else
                {
                    query += "and JST < '" + Year + "-" + (int.Parse(Month) + 1) + "-01 00:00:00' ";
                }
                query += "and ECOLOG.TRIP_ID != 1442 and ECOLOG.TRIP_Id != 1383";
                query += "group by ECOLOG.TRIP_ID, DRIVER_ID, ECOLOG.TRIP_DIRECTION ";
                query += "), Out as ( ";
                query += "select DRIVER_ID, DATENAME(year,DATEADD(hour,-1,START_TIME)) as Year, DATENAME(month,DATEADD(hour,-1,START_TIME)) as Month, DATENAME(day,DATEADD(hour,-1,START_TIME)) as Day, AVG(消費) as 消費, AVG(力行) as 力行, AVG(回生) as 回生, AVG(クーラー) as クーラー, AVG(ヒーター) as ヒーター, AVG(電装品) as 電装品 ";
                query += "from SubTable ";
                query += "where TRIP_DIRECTION = 'outward' ";
                query += "group by DATENAME(year,DATEADD(hour,-1,START_TIME)), DATENAME(month,DATEADD(hour,-1,START_TIME)), DATENAME(day,DATEADD(hour,-1,START_TIME)), DRIVER_ID ";
                query += "), Home as ( ";
                query += "select DRIVER_ID, DATENAME(year,DATEADD(hour,-1,START_TIME)) as Year, DATENAME(month,DATEADD(hour,-1,START_TIME)) as Month, DATENAME(day,DATEADD(hour,-1,START_TIME)) as Day, AVG(消費) as 消費, AVG(力行) as 力行, AVG(回生) as 回生, AVG(クーラー) as クーラー, AVG(ヒーター) as ヒーター, AVG(電装品) as 電装品 ";
                query += "from SubTable ";
                query += "where TRIP_DIRECTION = 'homeward' ";
                query += "group by DATENAME(year,DATEADD(hour,-1,START_TIME)), DATENAME(month,DATEADD(hour,-1,START_TIME)), DATENAME(day,DATEADD(hour,-1,START_TIME)), DRIVER_ID ";
                query += ") ";
                query += " ";
                query += "select Out.Day as START_DAY, ";

                if (EquipmentcheckBox.Checked && AirConcheckBox.Checked)
                {
                    query += "round((SUM(Out.消費) + SUM(Out.クーラー) + SUM(Out.ヒーター) + SUM(Out.電装品)),1) as 消費_out, ";
                    query += "round((SUM(Out.力行) + SUM(Out.クーラー) + SUM(Out.ヒーター) + SUM(Out.電装品)),1) as 力行_out, ";
                    query += "round((SUM(Home.消費) + SUM(Home.クーラー) + SUM(Home.ヒーター) + SUM(Home.電装品)),1) as 消費_home, ";
                    query += "round((SUM(Home.力行) + SUM(Home.クーラー) + SUM(Home.ヒーター) + SUM(Home.電装品)),1) as 力行_home, ";
                }
                if (EquipmentcheckBox.Checked && !AirConcheckBox.Checked)
                {
                    query += "round((SUM(Out.消費) + SUM(Out.電装品)),1) as 消費_out, ";
                    query += "round((SUM(Out.力行) + SUM(Out.電装品)),1) as 力行_out, ";
                    query += "round((SUM(Home.消費) + SUM(Home.電装品)),1) as 消費_home, ";
                    query += "round((SUM(Home.力行) + SUM(Home.電装品)),1) as 力行_home, ";
                }
                if (!EquipmentcheckBox.Checked && AirConcheckBox.Checked)
                {
                    query += "round((SUM(Out.消費) + SUM(Out.クーラー) + SUM(Out.ヒーター)),1) as 消費_out, ";
                    query += "round((SUM(Out.力行) + SUM(Out.クーラー) + SUM(Out.ヒーター)),1) as 力行_out, ";
                    query += "round((SUM(Home.消費) + SUM(Home.クーラー) + SUM(Home.ヒーター)),1) as 消費_home, ";
                    query += "round((SUM(Home.力行) + SUM(Home.クーラー) + SUM(Home.ヒーター)),1) as 力行_home, ";
                }
                if (!EquipmentcheckBox.Checked && !AirConcheckBox.Checked)
                {
                    query += "round((SUM(Out.消費)),1) as 消費_out, ";
                    query += "round((SUM(Out.力行)),1) as 力行_out, ";
                    query += "round((SUM(Home.消費)),1) as 消費_home, ";
                    query += "round((SUM(Home.力行)),1) as 力行_home, ";
                }

                query += "round(SUM(Out.回生),1) as 回生_out, ";
                query += "round(SUM(Home.回生),1) as 回生_home, COUNT(*) as 台数 ";
                query += "from Out, Home ";
                query += "where Out.DRIVER_ID = Home.DRIVER_ID ";
                query += "and Out.Day = Home.Day ";
                query += "group by Out.Day ";
                query += "order by Out.Day ";
                #endregion

                dt_calendar = DatabaseAccess.GetResult(query);
                calendar = new Calendar(Year, Month, true);
            }
            #endregion

        //    calendar.addData(dt_calendar);
        //    calendar.AutoScrollMargin = new System.Drawing.Size(0, 30);
         //   calendar.Text = "Calendar of Consumed Energy [" + Year + "/" + Month + "]";
         //   MainForm.ShowWindow(calendar);

        }
        private void CalendarDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Dispose();
            }
        }
        //ヒストグラム表示
        private void DisplayHistogrambutton_Click(object sender, EventArgs e)
        {
            string Year = Year_dateTimePicker.Value.ToString("yyyy");
            string Month = Month_dateTimePicker.Value.ToString("MM");
            string drivers = "0";
            double maxX, minX, maxY, minY;

            foreach (object obj in DrivercheckedListBox.CheckedItems)
            {
                drivers += ", ";
                drivers += MainForm.Driver[obj.ToString()];
            }

            drivers = drivers.Replace("0, ", "");

            #region 軸の最大値の設定
            try
            {
                maxX = double.Parse(maxX_textBox.Text);
            }
            catch (Exception)
            {
                maxX = double.NaN;
            }

            try
            {
                minX = double.Parse(minX_textBox.Text);
            }
            catch (Exception)
            {
                minX = double.NaN;
            }

            try
            {
                maxY = double.Parse(maxY_textBox.Text);
            }
            catch (Exception)
            {
                maxY = double.NaN;
            }

            try
            {
                minY = double.Parse(minY_textBox.Text);
            }
            catch (Exception)
            {
                minY = double.NaN;
            }
            #endregion

            #region クエリ
            string query = "with SubTable as ( \r\n";
            query += "select ECOLOG.TRIP_ID, ECOLOG.DRIVER_ID, MIN(JST) as START_TIME, SUM(CONSUMED_ELECTRIC_ENERGY) as 消費, \r\n";
            query += "SUM(case when CONSUMED_ELECTRIC_ENERGY > 0  then CONSUMED_ELECTRIC_ENERGY else 0 end) as 力行 , \r\n";
            query += "SUM(case when CONSUMED_ELECTRIC_ENERGY < 0  then CONSUMED_ELECTRIC_ENERGY else 0 end) as 回生 , \r\n";
            query += "SUM(ENERGY_BY_COOLING) as クーラー, \r\n";
            query += "SUM(ENERGY_BY_HEATING) as ヒーター, \r\n";
            query += "SUM(ENERGY_BY_EQUIPMENT) as 電装品,  ECOLOG.TRIP_DIRECTION \r\n";
            query += "from ECOLOG as ECOLOG \r\n";
            query += "inner join TRIPS on ECOLOG.TRIP_ID = TRIPS.TRIP_ID \r\n";
            query += "where ECOLOG.DRIVER_ID in (" + drivers + ") \r\n";
            query += "and JST between '" + start_dateTimePicker.Value.ToString("yyyy-MM-dd") + " 00:00:00' and '" + end_dateTimePicker.Value.ToString("yyyy-MM-dd") + " 23:59:59'  \r\n";
            query += "and TRIPS.VALIDATION is null ";
            query += "group by ECOLOG.TRIP_ID, ECOLOG.DRIVER_ID, ECOLOG.TRIP_DIRECTION \r\n";
            query += "), Out as ( \r\n";
            query += "select DRIVER_ID, DATENAME(year,DATEADD(hour,-1,START_TIME)) as Year, DATENAME(month,DATEADD(hour,-1,START_TIME)) as Month, DATENAME(day,DATEADD(hour,-1,START_TIME)) as Day, AVG(消費) as 消費, AVG(力行) as 力行, AVG(回生) as 回生, AVG(クーラー) as クーラー, AVG(ヒーター) as ヒーター, AVG(電装品) as 電装品 \r\n";
            query += "from SubTable \r\n";
            query += "where TRIP_DIRECTION = 'outward' \r\n";
            query += "group by DATENAME(year,DATEADD(hour,-1,START_TIME)), DATENAME(month,DATEADD(hour,-1,START_TIME)), DATENAME(day,DATEADD(hour,-1,START_TIME)), DRIVER_ID \r\n";
            query += "), Home as ( \r\n";
            query += "select DRIVER_ID, DATENAME(year,DATEADD(hour,-1,START_TIME)) as Year, DATENAME(month,DATEADD(hour,-1,START_TIME)) as Month, DATENAME(day,DATEADD(hour,-1,START_TIME)) as Day, AVG(消費) as 消費, AVG(力行) as 力行, AVG(回生) as 回生, AVG(クーラー) as クーラー, AVG(ヒーター) as ヒーター, AVG(電装品) as 電装品 \r\n";
            query += "from SubTable \r\n";
            query += "where TRIP_DIRECTION = 'homeward' \r\n";
            query += "group by DATENAME(year,DATEADD(hour,-1,START_TIME)), DATENAME(month,DATEADD(hour,-1,START_TIME)), DATENAME(day,DATEADD(hour,-1,START_TIME)), DRIVER_ID \r\n";
            query += ") \r\n";
            query += " \r\n";
            query += "select 残余, 100.0 * cast(count('X') as real) /AVG(whole) as number \r\n";
            query += "from ( \r\n";
            query += "select Out.Year, Out.Month, Out.Day, ";

            if (EquipmentcheckBox.Checked && AirConcheckBox.Checked)
            {
                query += "ROUND(12 * COUNT(*) - (SUM(Out.消費) + SUM(Home.消費) + SUM(Out.クーラー) + SUM(Out.ヒーター) + SUM(Home.クーラー) + SUM(Home.ヒーター) + SUM(Out.電装品) + SUM(Home.電装品)), 0) as 残余";
            }
            if (EquipmentcheckBox.Checked && !AirConcheckBox.Checked)
            {
                query += "ROUND(12 * COUNT(*) - (SUM(Out.消費) + SUM(Home.消費) + SUM(Out.電装品) + SUM(Home.電装品)), 0) as 残余";
            }
            if (!EquipmentcheckBox.Checked && AirConcheckBox.Checked)
            {
                query += "ROUND(12 * COUNT(*) - (SUM(Out.消費) + SUM(Home.消費) + SUM(Out.クーラー) + SUM(Out.ヒーター) + SUM(Home.クーラー) + SUM(Home.ヒーター)), 0) as 残余";
            }
            if (!EquipmentcheckBox.Checked && !AirConcheckBox.Checked)
            {
                query += "ROUND(12 * COUNT(*) - (SUM(Out.消費) + SUM(Home.消費)), 0) as 残余";
            }

            query += ", COUNT(*) as 台数 \r\n";
            query += "from Out, Home \r\n";
            query += "where Out.DRIVER_ID = Home.DRIVER_ID \r\n";
            query += "and Out.Year = Home.Year \r\n";
            query += "and Out.Month = Home.Month \r\n";
            query += "and Out.Day = Home.Day \r\n";
            query += "group by Out.Year, Out.Month, Out.Day \r\n";
            query += ") SubTable1, ( \r\n";

            query += "select count('X') as whole \r\n";

            query += "from (  \r\n";
            query += "select DISTINCT Out.Year, Out.Month, Out.Day \r\n";
            query += "from Out, Home \r\n";
            query += "where Out.DRIVER_ID = Home.DRIVER_ID \r\n";
            query += "and Out.Year = Home.Year \r\n";
            query += "and Out.Month = Home.Month \r\n";
            query += "and Out.Day = Home.Day \r\n";
            query += ") SubSub \r\n";
            query += ") SubTable2 \r\n"; query += "where 残余 is not null \r\n";
            query += "group by 残余 \r\n";
            query += "order by 残余 \r\n";
            #endregion

            #region デモ用クエリ
            if (DEMO_checkBox.Checked)
            {
                //query = "with SubTable as ( \r\n";
                //query += "select ECOLOG.TRIP_ID, DRIVER_ID, MIN(JST) as START_TIME, SUM(CONSUMED_ELECTRIC_ENERGY) as 消費, SUM(case when CONSUMED_ELECTRIC_ENERGY > 0  then CONSUMED_ELECTRIC_ENERGY else 0 end) as 力行 , SUM(case when CONSUMED_ELECTRIC_ENERGY < 0  then CONSUMED_ELECTRIC_ENERGY else 0 end) as 回生 , SUM(ENERGY_BY_COOLING) as クーラー, SUM(ENERGY_BY_HEATING) as ヒーター, SUM(ENERGY_BY_EQUIPMENT) as 電装品,  ECOLOG.TRIP_DIRECTION \r\n";
                //query += "from ECOLOG as ECOLOG \r\n";
                //query += "where DRIVER_ID in (1, 4, 9) \r\n";
                //query += "and JST between '2013-01-01 00:00:00' and '2013-12-31 23:59:59' \r\n";
                //query += "group by ECOLOG.TRIP_ID, DRIVER_ID, ECOLOG.TRIP_DIRECTION \r\n";
                //query += "), Out as ( \r\n";
                //query += "select DRIVER_ID, DATENAME(year,DATEADD(hour,-1,START_TIME)) as Year, DATENAME(month,DATEADD(hour,-1,START_TIME)) as Month, DATENAME(day,DATEADD(hour,-1,START_TIME)) as Day, AVG(消費) as 消費, AVG(力行) as 力行, AVG(回生) as 回生, AVG(クーラー) as クーラー, AVG(ヒーター) as ヒーター, AVG(電装品) as 電装品 \r\n";
                //query += "from SubTable \r\n";
                //query += "where TRIP_DIRECTION = 'outward' \r\n";
                //query += "group by DATENAME(year,DATEADD(hour,-1,START_TIME)), DATENAME(month,DATEADD(hour,-1,START_TIME)), DATENAME(day,DATEADD(hour,-1,START_TIME)), DRIVER_ID \r\n";
                //query += "), Home as ( \r\n";
                //query += "select DRIVER_ID, DATENAME(year,DATEADD(hour,-1,START_TIME)) as Year, DATENAME(month,DATEADD(hour,-1,START_TIME)) as Month, DATENAME(day,DATEADD(hour,-1,START_TIME)) as Day, AVG(消費) as 消費, AVG(力行) as 力行, AVG(回生) as 回生, AVG(クーラー) as クーラー, AVG(ヒーター) as ヒーター, AVG(電装品) as 電装品 \r\n";
                //query += "from SubTable \r\n";
                //query += "where TRIP_DIRECTION = 'homeward' \r\n";
                //query += "group by DATENAME(year,DATEADD(hour,-1,START_TIME)), DATENAME(month,DATEADD(hour,-1,START_TIME)), DATENAME(day,DATEADD(hour,-1,START_TIME)), DRIVER_ID \r\n";
                //query += "), SubTable2 as ( \r\n";

                //query += "select count('X') as whole \r\n";
                //query += "from (  \r\n";
                //query += "select DISTINCT Out.Year, Out.Month, Out.Day \r\n";
                //query += "from Out, Home \r\n";
                //query += "where Out.DRIVER_ID = Home.DRIVER_ID and Out.Year = Home.Year and Out.Month = Home.Month and Out.Day = Home.Day \r\n";
                //query += ") SubSub \r\n";

                //query += "), OneCar as ( \r\n";
                //query += "select 残余, 100.0 * count(*)/AVG(whole) as numberofOneCar \r\n";
                //query += "from ( \r\n";
                //query += "select Out.Year, Out.Month, Out.Day, ROUND(12 * COUNT(*) - (SUM(Out.消費) + SUM(Home.消費) + SUM(Out.クーラー) + SUM(Out.ヒーター) + SUM(Home.クーラー) + SUM(Home.ヒーター) + SUM(Out.電装品) + SUM(Home.電装品)), 0) as 残余, COUNT(*) as 台数 from Out, Home \r\n";
                //query += "where Out.DRIVER_ID = Home.DRIVER_ID \r\n";
                //query += "and Out.Year = Home.Year and Out.Month = Home.Month and Out.Day = Home.Day \r\n";
                //query += "group by Out.Year, Out.Month, Out.Day \r\n";
                //query += "having count(*) = 1 ) ST, SubTable2 \r\n";
                //query += "group by 残余 \r\n";
                //query += "), TwoCars as ( \r\n";
                //query += "select 残余, 100.0 * count(*)/AVG(whole) as numberofTwoCars \r\n";
                //query += "from ( \r\n";
                //query += "select Out.Year, Out.Month, Out.Day, ROUND(12 * COUNT(*) - (SUM(Out.消費) + SUM(Home.消費) + SUM(Out.クーラー) + SUM(Out.ヒーター) + SUM(Home.クーラー) + SUM(Home.ヒーター) + SUM(Out.電装品) + SUM(Home.電装品)), 0) as 残余, COUNT(*) as 台数 from Out, Home \r\n";
                //query += "where Out.DRIVER_ID = Home.DRIVER_ID \r\n";
                //query += "and Out.Year = Home.Year and Out.Month = Home.Month and Out.Day = Home.Day \r\n";
                //query += "group by Out.Year, Out.Month, Out.Day \r\n";
                //query += "having count(*) = 2 ) ST, SubTable2 \r\n";
                //query += "group by 残余 \r\n";
                //query += "), ThreeCars as ( \r\n";
                //query += "select 残余, 100.0 * count(*)/AVG(whole) as numberofThreeCars \r\n";
                //query += "from ( \r\n";
                //query += "select Out.Year, Out.Month, Out.Day, ROUND(12 * COUNT(*) - (SUM(Out.消費) + SUM(Home.消費) + SUM(Out.クーラー) + SUM(Out.ヒーター) + SUM(Home.クーラー) + SUM(Home.ヒーター) + SUM(Out.電装品) + SUM(Home.電装品)), 0) as 残余, COUNT(*) as 台数 from Out, Home \r\n";
                //query += "where Out.DRIVER_ID = Home.DRIVER_ID \r\n";
                //query += "and Out.Year = Home.Year and Out.Month = Home.Month and Out.Day = Home.Day \r\n";
                //query += "group by Out.Year, Out.Month, Out.Day \r\n";
                //query += "having count(*) = 3 ) ST, SubTable2 \r\n";
                //query += "group by 残余 \r\n";
                //query += ") \r\n";
                //query += " \r\n";
                //query += "select case when OneCar.残余 is null then case when TwoCars.残余 is null then ThreeCars.残余 else TwoCars.残余 end else OneCar.残余 end as 残余, numberofOneCar, numberofTwoCars, numberofThreeCars \r\n";
                //query += "from OneCar \r\n";
                //query += "full outer join TwoCars \r\n";
                //query += "on OneCar.残余 = TwoCars.残余 \r\n";
                //query += "full outer join ThreeCars \r\n";
                //query += "on OneCar.残余 = ThreeCars.残余 \r\n";
                //query += "or TwoCars.残余 = ThreeCars.残余 \r\n";
                //query += "order by 残余 \r\n";

                query = "with SubTable as ( \r\n";
                query += "select ECOLOG.TRIP_ID, DRIVER_ID, MIN(JST) as START_TIME, SUM(CONSUMED_ELECTRIC_ENERGY) as 消費, SUM(case when CONSUMED_ELECTRIC_ENERGY > 0  then CONSUMED_ELECTRIC_ENERGY else 0 end) as 力行 , SUM(case when CONSUMED_ELECTRIC_ENERGY < 0  then CONSUMED_ELECTRIC_ENERGY else 0 end) as 回生 , SUM(ENERGY_BY_COOLING) as クーラー, SUM(ENERGY_BY_HEATING) as ヒーター, SUM(ENERGY_BY_EQUIPMENT) as 電装品,  ECOLOG.TRIP_DIRECTION \r\n";
                query += "from ECOLOG as ECOLOG \r\n";
                query += "where DRIVER_ID in (1, 4, 9) \r\n";
                query += "and JST between '2013-01-01 00:00:00' and '2013-12-31 23:59:59' \r\n";
                query += "group by ECOLOG.TRIP_ID, DRIVER_ID, ECOLOG.TRIP_DIRECTION \r\n";
                query += "), Out as ( \r\n";
                query += "select DRIVER_ID, DATENAME(year,DATEADD(hour,-1,START_TIME)) as Year, DATENAME(month,DATEADD(hour,-1,START_TIME)) as Month, DATENAME(day,DATEADD(hour,-1,START_TIME)) as Day, AVG(消費) as 消費, AVG(力行) as 力行, AVG(回生) as 回生, AVG(クーラー) as クーラー, AVG(ヒーター) as ヒーター, AVG(電装品) as 電装品 \r\n";
                query += "from SubTable \r\n";
                query += "where TRIP_DIRECTION = 'outward' \r\n";
                query += "group by DATENAME(year,DATEADD(hour,-1,START_TIME)), DATENAME(month,DATEADD(hour,-1,START_TIME)), DATENAME(day,DATEADD(hour,-1,START_TIME)), DRIVER_ID \r\n";
                query += "), Home as ( \r\n";
                query += "select DRIVER_ID, DATENAME(year,DATEADD(hour,-1,START_TIME)) as Year, DATENAME(month,DATEADD(hour,-1,START_TIME)) as Month, DATENAME(day,DATEADD(hour,-1,START_TIME)) as Day, AVG(消費) as 消費, AVG(力行) as 力行, AVG(回生) as 回生, AVG(クーラー) as クーラー, AVG(ヒーター) as ヒーター, AVG(電装品) as 電装品 \r\n";
                query += "from SubTable \r\n";
                query += "where TRIP_DIRECTION = 'homeward' \r\n";
                query += "group by DATENAME(year,DATEADD(hour,-1,START_TIME)), DATENAME(month,DATEADD(hour,-1,START_TIME)), DATENAME(day,DATEADD(hour,-1,START_TIME)), DRIVER_ID \r\n";
                query += "), whole_onecar as ( \r\n";
                query += "select count('X') as whole \r\n";
                query += "from ( \r\n";
                query += "select Out.Year, Out.Month, Out.Day \r\n";
                query += "from Out, Home \r\n";
                query += "where Out.DRIVER_ID = Home.DRIVER_ID and Out.Year = Home.Year and Out.Month = Home.Month and Out.Day = Home.Day \r\n";
                query += "and Out.Month in ('07', '08', '09') \r\n";
                query += "group by Out.Year, Out.Month, Out.Day \r\n";
                query += ") SubSub \r\n";
                query += "), whole_twocars as ( \r\n";
                query += "select count('X') as whole \r\n";
                query += "from ( \r\n";
                query += "select Out.Year, Out.Month, Out.Day \r\n";
                query += "from Out, Home \r\n";
                query += "where Out.DRIVER_ID = Home.DRIVER_ID and Out.Year = Home.Year and Out.Month = Home.Month and Out.Day = Home.Day \r\n";
                query += "and Out.Month in ('04', '05', '06', '10', '11') \r\n";
                query += "group by Out.Year, Out.Month, Out.Day \r\n";
                query += ") SubSub \r\n";
                query += "), whole_threecars as ( \r\n";
                query += "select count('X') as whole \r\n";
                query += "from ( \r\n";
                query += "select Out.Year, Out.Month, Out.Day \r\n";
                query += "from Out, Home \r\n";
                query += "where Out.DRIVER_ID = Home.DRIVER_ID and Out.Year = Home.Year and Out.Month = Home.Month and Out.Day = Home.Day \r\n";
                query += "and Out.Month in ('12', '01', '02', '03') \r\n";
                query += "group by Out.Year, Out.Month, Out.Day \r\n";
                query += ") SubSub \r\n";
                query += "), OneCar as ( \r\n";
                query += "select 残余, 100.0 * count(*)/AVG(whole) as numberofOneCar \r\n";
                query += "from ( \r\n";
                query += "select Out.Year, Out.Month, Out.Day, ROUND(12 * COUNT(*) - (SUM(Out.消費) + SUM(Home.消費) + SUM(Out.クーラー) + SUM(Out.ヒーター) + SUM(Home.クーラー) + SUM(Home.ヒーター) + SUM(Out.電装品) + SUM(Home.電装品)), 0) as 残余, COUNT(*) as 台数 from Out, Home \r\n";
                query += "where Out.DRIVER_ID = Home.DRIVER_ID \r\n";
                query += "and Out.Year = Home.Year and Out.Month = Home.Month and Out.Day = Home.Day \r\n";
                query += "and Out.Month in ('07', '08', '09') \r\n";
                query += "group by Out.Year, Out.Month, Out.Day \r\n";
                query += ") ST, whole_onecar \r\n";
                query += "group by 残余 \r\n";
                query += "), TwoCars as ( \r\n";
                query += "select 残余, 100.0 * count(*)/AVG(whole) as numberofTwoCars \r\n";
                query += "from ( \r\n";
                query += "select Out.Year, Out.Month, Out.Day, ROUND(12 * COUNT(*) - (SUM(Out.消費) + SUM(Home.消費) + SUM(Out.クーラー) + SUM(Out.ヒーター) + SUM(Home.クーラー) + SUM(Home.ヒーター) + SUM(Out.電装品) + SUM(Home.電装品)), 0) as 残余, COUNT(*) as 台数 from Out, Home \r\n";
                query += "where Out.DRIVER_ID = Home.DRIVER_ID \r\n";
                query += "and Out.Year = Home.Year and Out.Month = Home.Month and Out.Day = Home.Day \r\n";
                query += "and Out.Month in ('04', '05', '06', '10', '11') \r\n";
                query += "group by Out.Year, Out.Month, Out.Day \r\n";
                query += ") ST, whole_twocars \r\n";
                query += "group by 残余 \r\n";
                query += "), ThreeCars as ( \r\n";
                query += "select 残余, 100.0 * count(*)/AVG(whole) as numberofThreeCars \r\n";
                query += "from ( \r\n";
                query += "select Out.Year, Out.Month, Out.Day, ROUND(12 * COUNT(*) - (SUM(Out.消費) + SUM(Home.消費) + SUM(Out.クーラー) + SUM(Out.ヒーター) + SUM(Home.クーラー) + SUM(Home.ヒーター) + SUM(Out.電装品) + SUM(Home.電装品)), 0) as 残余, COUNT(*) as 台数 from Out, Home \r\n";
                query += "where Out.DRIVER_ID = Home.DRIVER_ID \r\n";
                query += "and Out.Year = Home.Year and Out.Month = Home.Month and Out.Day = Home.Day \r\n";
                query += "and Out.Month in ('12', '01', '02', '03') \r\n";
                query += "group by Out.Year, Out.Month, Out.Day \r\n";
                query += ") ST, whole_threecars \r\n";
                query += "where 残余 >= -1 \r\n";
                query += "group by 残余 \r\n";
                query += ") \r\n";
                query += " \r\n";
                query += "select case when OneCar.残余 is null then case when TwoCars.残余 is null then ThreeCars.残余 else TwoCars.残余 end else OneCar.残余 end as 残余, numberofOneCar, numberofTwoCars, numberofThreeCars \r\n";
                query += "from OneCar \r\n";
                query += "full outer join TwoCars \r\n";
                query += "on OneCar.残余 = TwoCars.残余 \r\n";
                query += "full outer join ThreeCars \r\n";
                query += "on OneCar.残余 = ThreeCars.残余 \r\n";
                query += "or TwoCars.残余 = ThreeCars.残余 \r\n";
                query += "order by 残余 \r\n";
                query += " \r\n";


            }
            #endregion

            double max;

            try
            {
                max = double.Parse(MaxtextBox.Text);
            }
            catch
            {
                max = 20.0;
            }

            if (QueryEdit_checkBox.Checked)
            {
                QueryView form = new QueryView(query);

                form.ShowDialog(this);

                if (form.DialogResult == DialogResult.OK)
                {
                    query = form.GetQuery();

                    DataTable dt = new DataTable();
                    dt = DatabaseAccess.GetResult(query);

                    AvailableEnergyChart chart = new AvailableEnergyChart(dt, maxX, minX, maxY, minY, max, LinecheckBox.Checked, DEMO_checkBox.Checked);
                    MainForm.ShowWindow(chart);
                }
            }
            else
            {
                DataTable dt = new DataTable();
                dt = DatabaseAccess.GetResult(query);

                AvailableEnergyChart chart = new AvailableEnergyChart(dt, maxX, minX, maxY, minY, max, LinecheckBox.Checked, DEMO_checkBox.Checked);
                MainForm.ShowWindow(chart);
            }

        }
    }
}
