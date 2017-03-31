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
    /// ヒストグラムを表示する画面を取り扱うクラス
    /// </summary>
    public partial class Histogram : Form
    {
        #region 変数定義
        //private FormData user;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="u">表示するトリップの情報</param>
        /// <param name="start">開始時刻</param>
        /// <param name="end">終了時刻</param>
        /// <param name="aggregation">集約単位</param>
        /// <param name="value">表示する内容</param>
        public Histogram(FormData u, DateTime start, DateTime end, string aggregation, string value)
        {
            InitializeComponent();
            //user = new FormData(u);

            //chartArea1.AxisY.Title = "Probability";

            //if (value.Equals("SUM(LOST_ENERGY)/SUM(DISTANCE_DIFFERENCE)"))
            //{
            //    chartArea1.AxisX.Title = "Lost Energy per Meter[kWh/m]";
            //}
            //else
            //{
            //    chartArea1.AxisX.Title = "Passing Time per Meter[s/m]";
            //}

            ////chartArea1.AxisX.Maximum = Program.MaxofAxisX_Histogram;
            ////chartArea1.AxisX.Minimum = Program.MinofAxisX_Histogram;

            ////chartArea1.AxisY.Maximum = Program.MaxofAxisY_Histogram;
            ////chartArea1.AxisY.Minimum = Program.MinofAxisY_Histogram;

            //System.Windows.Forms.DataVisualization.Charting.Series series_all = new System.Windows.Forms.DataVisualization.Charting.Series();
            //System.Windows.Forms.DataVisualization.Charting.Series series_this = new System.Windows.Forms.DataVisualization.Charting.Series();

            //series_all.ChartArea = "ChartArea1";
            //series_all.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            //series_all.Color = System.Drawing.Color.Blue;
            //series_all.Legend = "Legend1";
            //series_all.XValueMember = "AxisX";
            //series_all.YValueMembers = "AxisY";

            //series_this.ChartArea = "ChartArea1";
            //series_this.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            //series_this.Color = System.Drawing.Color.Red;
            //series_this.Legend = "Legend1";
            //series_this.XValueMember = "AxisX";
            //series_this.YValueMembers = "ThisAxisY";

            //Chart.Series.Add(series_all);
            //Chart.Series.Add(series_this);

            //DataSet ds_histogram = new DataSet();

            //#region リンクの取得
            //string selected_item = "'0'";

            //string query = "select DISTINCT " + aggregation + " from ECOLOG where TRIP_ID = " + user.trip_id + " and JST between '" + start + "' and '" + end + "' ";

            //DataTable dt = new DataTable();
            //dt = Program.Get_Result(query);

            //int i = 0;

            //while (i < dt.Rows.Count)
            //{
            //    selected_item += ", ";
            //    selected_item += "'" + dt.Rows[i][aggregation].ToString().Trim() +"'";
            //    i++;
            //}
            //#endregion

            //#region クエリ
            //query = "select SUM as AxisX, ROUND(COUNT(*),0) as AxisY ";
            //query += "from ( ";
            //query += "  select ECOLOG.TRIP_ID, ROUND(" + Program.value + ", " + Program.Round + ") as SUM ";
            //query += "  from ECOLOG ";
            //query += "  left join TRIPS ";
            //query += "  on ECOLOG.TRIP_ID = TRIPS.TRIP_ID ";
            //query += "  where " + Program.aggregation + " in (" + selected_item + ") ";
            //query += "  and DISTANCE_DIFFERENCE != 0 ";
            //if (user.direction != "")
            //{
            //    query += "  and TRIP_DIRECTION = '" + user.direction + "' ";
            //}
            //query += "  group by ECOLOG.TRIP_ID ";
            //query += ") SubTable ";
            //query += "group by SUM ";
            //query += "order by SUM ";
            //#endregion

            //#region クエリ発行
            //if (Program.local)
            //{
            //    using (System.Data.SqlServerCe.SqlCeConnection sqlConnection1 = new System.Data.SqlServerCe.SqlCeConnection(Program.cn))
            //    {
            //        System.Data.SqlServerCe.SqlCeDataAdapter da = new System.Data.SqlServerCe.SqlCeDataAdapter(query, sqlConnection1);

            //        try
            //        {
            //            sqlConnection1.Open();
            //            Cursor.Current = Cursors.WaitCursor;
            //            da.Fill(ds_histogram);
            //            Cursor.Current = Cursors.Default;
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
            //            Cursor.Current = Cursors.WaitCursor;
            //            da.Fill(ds_histogram);
            //            Cursor.Current = Cursors.Default;
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

            //#region クエリ
            //query = "select SubTable.SUM as AxisX, AxisY as ThisAxisY ";
            //query += "from ( ";
            //query += "  select ECOLOG.TRIP_ID, ROUND(" + Program.value + ", " + Program.Round + ") as SUM ";
            //query += "  from ECOLOG ";
            ////query += "  left join TRIPS ";
            ////query += "  on ECOLOG.TRIP_ID = TRIPS.TRIP_ID ";
            //query += "  where " + Program.aggregation + " in (" + selected_item + ") ";
            //query += "  and DISTANCE_DIFFERENCE != 0 ";
            //query += "  group by ECOLOG.TRIP_ID ";
            //query += ") SubTable, ( ";
            //query += "  select SUM, COUNT(*) as AxisY ";
            //query += "  from ( ";
            //query += "    select ECOLOG.TRIP_ID, ROUND(" + Program.value + ", " + Program.Round + ") as SUM ";
            //query += "    from ECOLOG ";
            ////query += "    left join TRIPS ";
            ////query += "    on ECOLOG.TRIP_ID = TRIPS.TRIP_ID ";
            //query += "    where " + Program.aggregation + " in (" + selected_item + ") ";
            //query += "    and DISTANCE_DIFFERENCE != 0 ";
            //query += "    group by ECOLOG.TRIP_ID ";
            //query += "  ) SubTable ";
            //query += "  group by SUM ";
            //query += ") ThisTable ";
            //query += "where TRIP_ID = " + user.trip_id + " ";
            //query += "and SubTable.SUM = ThisTable.SUM ";

            ////query = "with SubTable as ( ";
            ////query += "select ECOLOG.TRIP_ID, ROUND(SUM(LOST_ENERGY)/SUM(DISTANCE_DIFFERENCE), " + Program.Round + ") as SUM ";
            ////query += "from ECOLOG ";
            ////query += "left join TRIPS ";
            ////query += "on ECOLOG.TRIP_ID = TRIPS.TRIP_ID ";
            ////query += "where " + Program.aggregation + " = '" + selected_item + "' ";
            ////query += "and DISTANCE_DIFFERENCE != 0 ";
            ////query += "and TRIP_DIRECTION = '" + user.direction + "' ";
            ////query += "group by ECOLOG.TRIP_ID ";
            ////query += "), ThisTable as ( ";
            ////query += "select SUM, COUNT(*) as AxisY ";
            ////query += "from SubTable ";
            ////query += "group by SUM ";
            ////query += ") ";
            ////query += "select SubTable.SUM as AxisX, AxisY as ThisAxisY ";
            ////query += "from SubTable, ThisTable ";
            ////query += "where TRIP_ID = " + user.trip_id + " ";
            ////query += "and SubTable.SUM = ThisTable.SUM ";
            //#endregion

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
            //            da.Fill(ds_histogram);
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
            //            da.Fill(ds_histogram);
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

            //Chart.DataSource = ds_histogram;

        }
    }
}
