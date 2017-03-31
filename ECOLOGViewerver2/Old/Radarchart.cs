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
    /// （セマンティック）リンクの傾向チャートを取り扱うクラス
    /// </summary>
    public partial class Radarchart : Form
    {
        #region 変数定義
        private FormData user;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="u">表示するトリップの情報</param>
        /// <param name="start">開始時刻</param>
        /// <param name="end">終了時刻</param>
        /// <param name="aggregation">集約単位</param>
        public Radarchart(FormData u, DateTime start, DateTime end, string aggregation)
        {
            InitializeComponent();
            user = new FormData(u);

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            #region テーブルの設定
            // テーブルの追加
            //dt = ds.Tables.Add("table");
            // 列の追加
            dt.Columns.Add("SUBJECT");
            dt.Columns.Add("SCORE");
            dt.Columns.Add("AVERAGE");
            // 行の追加 
            for (int i = 0; i < 5; i++)
            //for (int i = 0; i < 6; i++)
            {
                dt.Rows.Add();
            }
            #endregion

            #region リンクの取得
            string selected_item = "'0'";

            string query = "select DISTINCT " + aggregation + " from ECOLOG where TRIP_ID = " + user.tripID + " and JST between '" + start + "' and '" + end + "' ";

            DataTable dt_link = new DataTable();
            dt_link = DatabaseAccess.GetResult(query);

            int num = 0;

            while (num < dt_link.Rows.Count)
            {
                selected_item += ", ";
                selected_item += "'" + dt_link.Rows[num][aggregation].ToString().Trim() + "'";
                num++;
            }
            #endregion

            #region データの取得
            //query = "with avgDATA as (  ";
            //query += "select TRIP_ID,  ";
            //query += "SUM(ENERGY_BY_AIR_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as AVG_AIR_ENERGY,  ";
            //query += "SUM(ENERGY_BY_ROLLING_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as AVG_ROLLING_ENERGY,  ";
            //query += "SUM(ENERGY_BY_CLIMBING_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as AVG_CLIMBING_ENERGY,  ";
            //query += "SUM(ABS(CONVERT_LOSS))/SUM(DISTANCE_DIFFERENCE) as AVG_CONVERT_LOSS,  ";
            //query += "SUM(ABS(REGENE_LOSS))/SUM(DISTANCE_DIFFERENCE) as AVG_REGENE_LOSS,  ";
            ////query += "SUM(OVER_CURVE_LOSS)/SUM(DISTANCE_DIFFERENCE) as AVG_OVER_CURVE_LOSS,  ";
            //query += "SUM(LOST_ENERGY)/SUM(DISTANCE_DIFFERENCE) as AVG_LOST_ENERGY ";
            //query += "from ECOLOG  ";
            //query += "where " + aggregation + " = '" + link_id + "' ";
            //query += "group by TRIP_ID ";
            //query += "), thisDATA as ( ";
            //query += "select SUM(ENERGY_BY_AIR_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as ENERGY_BY_AIR_RESISTANCE,  ";
            //query += "SUM(ENERGY_BY_ROLLING_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as ENERGY_BY_ROLLING_RESISTANCE,  ";
            //query += "SUM(ENERGY_BY_CLIMBING_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as ENERGY_BY_CLIMBING_RESISTANCE,  ";
            //query += "SUM(ABS(CONVERT_LOSS))/SUM(DISTANCE_DIFFERENCE) as CONVERT_LOSS,  ";
            //query += "SUM(ABS(REGENE_LOSS))/SUM(DISTANCE_DIFFERENCE) as REGENE_LOSS,  ";
            ////query += "SUM(OVER_CURVE_LOSS)/SUM(DISTANCE_DIFFERENCE) as AVG_OVER_CURVE_LOSS,  ";
            //query += "SUM(LOST_ENERGY)/SUM(DISTANCE_DIFFERENCE) as LOST_ENERGY ";
            //query += "from ECOLOG  ";
            //query += "where " + aggregation + " = '" + link_id + "' ";
            //query += "and TRIP_ID = " + user.trip_id + " ";
            //query += ") ";

            //query += "select * ";
            //query += "from avgDATA, thisDATA ";

            query = "select * ";
            query += "from (  ";
            query += "  select AVG(AVG_AIR_ENERGY) as AVG_AIR_ENERGY, AVG(AVG_ROLLING_ENERGY) as AVG_ROLLING_ENERGY, AVG(AVG_CLIMBING_ENERGY) as AVG_CLIMBING_ENERGY, AVG(AVG_CONVERT_LOSS) as AVG_CONVERT_LOSS, AVG(AVG_REGENE_LOSS) as AVG_REGENE_LOSS, AVG(AVG_LOST_ENERGY) as AVG_LOST_ENERGY ";
            query += "  from ( ";
            query += "    select TRIP_ID,  ";
            query += "    SUM(ENERGY_BY_AIR_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as AVG_AIR_ENERGY,  ";
            query += "    SUM(ENERGY_BY_ROLLING_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as AVG_ROLLING_ENERGY,  ";
            query += "    SUM(ENERGY_BY_CLIMBING_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as AVG_CLIMBING_ENERGY,  ";
            query += "    SUM(ABS(CONVERT_LOSS))/SUM(DISTANCE_DIFFERENCE) as AVG_CONVERT_LOSS,  ";
            query += "    SUM(ABS(REGENE_LOSS))/SUM(DISTANCE_DIFFERENCE) as AVG_REGENE_LOSS,  ";
            query += "    SUM(LOST_ENERGY)/SUM(DISTANCE_DIFFERENCE) as AVG_LOST_ENERGY ";
            query += "    from ECOLOG  ";
            query += "    left join TRIPS ";
            query += "    on ECOLOG.TRIP_ID = TRIPS.TRIP_ID ";
            query += "    where " + aggregation + " in (" + selected_item + ") ";
            query += "    and DISTANCE_DIFFERENCE != 0 ";
            if (user.direction != "")
            {
                query += "  and TRIP_DIRECTION = '" + user.direction + "' ";
            }
            query += "    group by TRIP_ID ";
            query += "    ) average ";
            query += "  ) avgDATA, ( ";

            query += "  select SUM(ENERGY_BY_AIR_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as ENERGY_BY_AIR_RESISTANCE,  ";
            query += "  SUM(ENERGY_BY_ROLLING_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as ENERGY_BY_ROLLING_RESISTANCE,  ";
            query += "  SUM(ENERGY_BY_CLIMBING_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as ENERGY_BY_CLIMBING_RESISTANCE,  ";
            query += "  SUM(ABS(CONVERT_LOSS))/SUM(DISTANCE_DIFFERENCE) as CONVERT_LOSS,  ";
            query += "  SUM(ABS(REGENE_LOSS))/SUM(DISTANCE_DIFFERENCE) as REGENE_LOSS,  ";
            query += "  SUM(LOST_ENERGY)/SUM(DISTANCE_DIFFERENCE) as LOST_ENERGY ";
            query += "  from ECOLOG  ";
            query += "    left join TRIPS ";
            query += "    on ECOLOG.TRIP_ID = TRIPS.TRIP_ID ";
            query += "  where " + aggregation + " in (" + selected_item + ") ";
            query += "  and TRIP_ID = " + user.tripID + " ";
            query += "  and DISTANCE_DIFFERENCE != 0 ";
            query += "  ) thisDATA";

            dt2 = DatabaseAccess.GetResult(query);
            #endregion

            #region DataTableの設定
            double Energy_by_air_resistance = 0.0;
            double Average_Energy_by_air_resistance = 0.0;

            double Energy_by_rolling_resistance = 0.0;
            double Average_Energy_by_rolling_resistance = 0.0;

            double Energy_by_climbing_resistance = 0.0;
            double Average_Energy_by_climbing_resistance = 0.0;

            double Convert_loss = 0.0;
            double Average_Convert_loss = 0.0;

            double Regene_loss = 0.0;
            double Average_Regene_loss = 0.0;

            double Lost_Energy = 0.0;
            double Average_Lost_Energy = 0.0;

            if (dt2.Rows[0]["ENERGY_BY_AIR_RESISTANCE"] != DBNull.Value && !dt2.Rows[0]["ENERGY_BY_AIR_RESISTANCE"].Equals(""))
                Energy_by_air_resistance = (double)dt2.Rows[0]["ENERGY_BY_AIR_RESISTANCE"];

            if (dt2.Rows[0]["AVG_AIR_ENERGY"] != DBNull.Value && !dt2.Rows[0]["AVG_AIR_ENERGY"].Equals(""))
                Average_Energy_by_air_resistance = (double)dt2.Rows[0]["AVG_AIR_ENERGY"];

            if (dt2.Rows[0]["ENERGY_BY_ROLLING_RESISTANCE"] != DBNull.Value && !dt2.Rows[0]["ENERGY_BY_ROLLING_RESISTANCE"].Equals(""))
                Energy_by_rolling_resistance = (double)dt2.Rows[0]["ENERGY_BY_ROLLING_RESISTANCE"];

            if (dt2.Rows[0]["AVG_ROLLING_ENERGY"] != DBNull.Value && !dt2.Rows[0]["AVG_ROLLING_ENERGY"].Equals(""))
                Average_Energy_by_rolling_resistance = (double)dt2.Rows[0]["AVG_ROLLING_ENERGY"];

            if (dt2.Rows[0]["ENERGY_BY_CLIMBING_RESISTANCE"] != DBNull.Value && !dt2.Rows[0]["ENERGY_BY_CLIMBING_RESISTANCE"].Equals(""))
                Energy_by_climbing_resistance = (double)dt2.Rows[0]["ENERGY_BY_CLIMBING_RESISTANCE"];

            if (dt2.Rows[0]["AVG_CLIMBING_ENERGY"] != DBNull.Value && !dt2.Rows[0]["AVG_CLIMBING_ENERGY"].Equals(""))
                Average_Energy_by_climbing_resistance = (double)dt2.Rows[0]["AVG_CLIMBING_ENERGY"];

            if (dt2.Rows[0]["CONVERT_LOSS"] != DBNull.Value && !dt2.Rows[0]["CONVERT_LOSS"].Equals(""))
                Convert_loss = (double)dt2.Rows[0]["CONVERT_LOSS"];

            if (dt2.Rows[0]["AVG_CONVERT_LOSS"] != DBNull.Value && !dt2.Rows[0]["AVG_CONVERT_LOSS"].Equals(""))
                Average_Convert_loss = (double)dt2.Rows[0]["AVG_CONVERT_LOSS"];

            if (dt2.Rows[0]["REGENE_LOSS"] != DBNull.Value && !dt2.Rows[0]["REGENE_LOSS"].Equals(""))
                Regene_loss = (double)dt2.Rows[0]["REGENE_LOSS"];

            if (dt2.Rows[0]["AVG_REGENE_LOSS"] != DBNull.Value && !dt2.Rows[0]["AVG_REGENE_LOSS"].Equals(""))
                Average_Regene_loss = (double)dt2.Rows[0]["AVG_REGENE_LOSS"];

            if (dt2.Rows[0]["LOST_ENERGY"] != DBNull.Value && !dt2.Rows[0]["LOST_ENERGY"].Equals(""))
                Lost_Energy = (double)dt2.Rows[0]["LOST_ENERGY"];

            if (dt2.Rows[0]["AVG_LOST_ENERGY"] != DBNull.Value && !dt2.Rows[0]["AVG_LOST_ENERGY"].Equals(""))
                Average_Lost_Energy = (double)dt2.Rows[0]["AVG_LOST_ENERGY"];


            dt.Rows[0][0] = "AIR_RESISTANCE";
            dt.Rows[0][1] = Math.Round(Energy_by_air_resistance * 100.0 / Lost_Energy);
            dt.Rows[0][2] = Math.Round(Average_Energy_by_air_resistance * 100.0 / Average_Lost_Energy);

            dt.Rows[1][0] = "ROLLING_RESISTANCE";
            dt.Rows[1][1] = Math.Round(Energy_by_rolling_resistance * 100.0 / Lost_Energy);
            dt.Rows[1][2] = Math.Round(Average_Energy_by_rolling_resistance * 100.0 / Average_Lost_Energy);

            dt.Rows[2][0] = "CLIMBING_RESISTANCE";
            if (Energy_by_climbing_resistance > 0)
            {
                dt.Rows[2][1] = Math.Round(Energy_by_climbing_resistance * 100.0 / Lost_Energy);
            }
            else
            {
                dt.Rows[2][1] = 0;
            }

            if (Average_Energy_by_climbing_resistance > 0)
            {
                dt.Rows[2][2] = Math.Round(Average_Energy_by_climbing_resistance * 100.0 / Average_Lost_Energy);
            }
            else
            {
                dt.Rows[2][2] = 0;
            }

            dt.Rows[3][0] = "CONVERT_LOSS";
            dt.Rows[3][1] = Math.Round(Convert_loss * 100.0 / Lost_Energy);
            dt.Rows[3][2] = Math.Round(Average_Convert_loss * 100.0 / Average_Lost_Energy);

            dt.Rows[4][0] = "REGENE_LOSS";
            dt.Rows[4][1] = Math.Round(Regene_loss * 100.0 / Lost_Energy);
            dt.Rows[4][2] = Math.Round(Average_Regene_loss * 100.0 / Average_Lost_Energy);
            #endregion

            Chart.DataSource = dt;
            Chart.Show();

        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="direction">方向</param>
        /// <param name="slink">セマンティックリンク</param>
        /// <param name="semantics">意味</param>
        public Radarchart(string direction, string slink, string semantics)
        {
            InitializeComponent();

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            #region テーブルの設定
            // テーブルの追加
            //dt = ds.Tables.Add("table");
            // 列の追加
            dt.Columns.Add("SUBJECT");
            dt.Columns.Add("SCORE");
            dt.Columns.Add("AVERAGE");
            // 行の追加 
            for (int i = 0; i < 5; i++)
            //for (int i = 0; i < 6; i++)
            {
                dt.Rows.Add();
            }
            #endregion

            #region データの取得
            string query = "select ECOLOG.TRIP_ID,  ";
            query += "SUM(ENERGY_BY_AIR_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as AVG_AIR_ENERGY,  ";
            query += "SUM(ENERGY_BY_ROLLING_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as AVG_ROLLING_ENERGY,  ";
            query += "SUM(ENERGY_BY_CLIMBING_RESISTANCE)/SUM(DISTANCE_DIFFERENCE) as AVG_CLIMBING_ENERGY,  ";
            query += "SUM(ABS(CONVERT_LOSS))/SUM(DISTANCE_DIFFERENCE) as AVG_CONVERT_LOSS,  ";
            query += "SUM(ABS(REGENE_LOSS))/SUM(DISTANCE_DIFFERENCE) as AVG_REGENE_LOSS,  ";
            //query += "SUM(OVER_CURVE_LOSS)/SUM(DISTANCE_DIFFERENCE) as AVG_OVER_CURVE_LOSS,  ";
            query += "SUM(LOST_ENERGY)/SUM(DISTANCE_DIFFERENCE) as AVG_LOST_ENERGY ";
            query += "from ECOLOG, TRIPS  ";
            query += "where SEMANTIC_LINK_ID in (" + slink + ") ";
            query += "and TRIPS.TRIP_ID = ECOLOG.TRIP_ID ";
            query += "and DISTANCE_DIFFERENCE != 0 ";
            if (!direction.Equals("All"))
            {
                query += "and TRIPS.TRIP_DIRECTION = '" + direction + "' ";
            }
            query += "group by ECOLOG.TRIP_ID ";

            dt2 = DatabaseAccess.GetResult(query);
            #endregion

            dt.Rows[0][0] = "ENERGY_BY_AIR_RESISTANCE";
            dt.Rows[0][2] = Math.Round(Double.Parse(dt2.Rows[0]["AVG_AIR_ENERGY"].ToString()) * 100.0 / Double.Parse(dt2.Rows[0]["AVG_LOST_ENERGY"].ToString()));

            dt.Rows[1][0] = "ENERGY_BY_ROLLING_RESISTANCE";
            dt.Rows[1][2] = Math.Round(Double.Parse(dt2.Rows[0]["AVG_ROLLING_ENERGY"].ToString()) * 100.0 / Double.Parse(dt2.Rows[0]["AVG_LOST_ENERGY"].ToString()));

            dt.Rows[2][0] = "ENERGY_BY_CLIMBING_RESISTANCE";
            if (Double.Parse(dt2.Rows[0]["AVG_CLIMBING_ENERGY"].ToString()) > 0)
            {
                dt.Rows[2][2] = Math.Round(Double.Parse(dt2.Rows[0]["AVG_CLIMBING_ENERGY"].ToString()) * 100.0 / Double.Parse(dt2.Rows[0]["AVG_LOST_ENERGY"].ToString()));
            }
            else
            {
                dt.Rows[2][2] = 0;
            }

            dt.Rows[3][0] = "CONVERT_LOSS";
            dt.Rows[3][2] = Math.Round(Double.Parse(dt2.Rows[0]["AVG_CONVERT_LOSS"].ToString()) * 100.0 / Double.Parse(dt2.Rows[0]["AVG_LOST_ENERGY"].ToString()));

            dt.Rows[4][0] = "REGENE_LOSS";
            dt.Rows[4][2] = Math.Round(Double.Parse(dt2.Rows[0]["AVG_REGENE_LOSS"].ToString()) * 100.0 / Double.Parse(dt2.Rows[0]["AVG_LOST_ENERGY"].ToString()));

            //dt.Rows[5][0] = "OVER_CURVE_LOSS";
            //dt.Rows[5][2] = Math.Round(Double.Parse(dt2.Rows[0]["AVG_OVER_CURVE_LOSS"].ToString()) * 100.0 / Double.Parse(dt2.Rows[0]["AVG_LOST_ENERGY"].ToString()));

            AirEnergylabel.Text = "(avg: " + Math.Round(Double.Parse(dt2.Rows[0]["AVG_AIR_ENERGY"].ToString()), 5) + ")";

            RollingEnergylabel.Text = "(avg: " + Math.Round(Double.Parse(dt2.Rows[0]["AVG_ROLLING_ENERGY"].ToString()), 5) + ")";

            ClimbingEnergylabel.Text = "(avg: " + Math.Round(Double.Parse(dt2.Rows[0]["AVG_CLIMBING_ENERGY"].ToString()), 5) + ")";

            ConvertLosslabel.Text = "(avg: " + Math.Round(Double.Parse(dt2.Rows[0]["AVG_CONVERT_LOSS"].ToString()), 5) + ")";

            RegeneLosslabel.Text = "(avg: " + Math.Round(Double.Parse(dt2.Rows[0]["AVG_REGENE_LOSS"].ToString()), 5) + ")";

            //OverCurveLosslabel.Text = "(avg: " + Math.Round(Double.Parse(dt2.Rows[0]["AVG_OVER_CURVE_LOSS"].ToString()), 5) + ")";

            LostEnergylabel.Text = "(avg: " + Math.Round(Double.Parse(dt2.Rows[0]["AVG_LOST_ENERGY"].ToString()), 5) + ")";

            Chart.DataSource = dt;
            Chart.Show();

        }
    }
}
