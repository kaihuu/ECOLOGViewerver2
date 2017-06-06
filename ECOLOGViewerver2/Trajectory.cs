using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECOLOGViewerver2
{
    /// <summary>
    /// Google Map上に軌跡を描くメソッドを扱うクラス
    /// </summary>
    public class Trajectory
    {
        #region 変数定義
        // ユーザデータ
        private FormData user;
        // DB用
        private DataTable dt;
        //private DatabaseAccess dbaccess;

        #endregion

        /// <summary>
        /// コンストラクタ2
        /// </summary>
        /// <param name="u">表示するトリップの情報</param>
        public Trajectory(FormData u)
        {
            dt = new DataTable();
            user = u;
            //this.dbaccess = dbaccess;
        }
        /// <summary>
        /// 指定したトリップを軌跡として表示する
        /// </summary>
        public bool makeFile()
        {
            #region フォルダ作成
            if (!Directory.Exists(user.currentDirectory))
            {
                Directory.CreateDirectory(user.currentDirectory);
            }
            #endregion

            #region 前準備
            StringBuilder sb = new StringBuilder();
            user.currentFile = @user.currentDirectory + @"\car_trajectory.html";
            StreamWriter sw = new StreamWriter(user.currentFile);
            #endregion

            if (user.startTime != null && user.endTime != null)
            {
                DataTable dtSelect = new DataTable();

                string querySelect = user.worstQuery.Replace("carID", user.carID);
                querySelect = querySelect.Replace("driverID", user.driverID);
                querySelect = querySelect.Replace("sensorID", user.sensorID);
                querySelect = querySelect.Replace("startTime", user.startTime);
                querySelect = querySelect.Replace("endTime", user.endTime);

                #region トップ部
                sb.Append("<html>\r\n");
                sb.Append("<head>\r\n");
                sb.Append("<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>\r\n");
 //               sb.Append("<script type=\"text/javascript\" src=\"http://maps.google.com/maps/api/js?sensor=false\"></script>\r\n");
                sb.Append("<script type=\"text/javascript\" src=\"http://maps.google.com/maps/api/js?v=3.7&sensor=false&language=ja\"></script>\r\n");
                sb.Append("<script type=\"text/javascript\">\r\n");
                #endregion

                #region 初期化関数
                sb.Append("var map;\r\n");
                sb.Append("var image_center;\r\n");
                sb.Append("var center_marker;\r\n");
                sb.Append("function initialize() {\r\n");
                sb.Append("     map = new google.maps.Map(document.getElementById(\"map\"), {\r\n");
                sb.Append("     zoom: 17,\r\n");
                sb.Append("     mapTypeId: google.maps.MapTypeId.ROADMAP\r\n");
                sb.Append(" });\r\n");
                sb.Append(" \r\n");
                #endregion

                #region DBからデータ取得
                string query = "";

                if (user.value == "UsedFuel")
                {
                    #region ガソリン
                    query = "select * ";
                    query += "from ECOLOG ";
                    query += "left join ( ";
                    query += "    select LINK_ID, SUM(CONSUMED_FUEL)/SUM(DISTANCE_DIFFERENCE) as FUEL_PER_METER, SUM(CONSUMED_ELECTRIC_ENERGY)/SUM(DISTANCE_DIFFERENCE) as ENERGY_PER_METER, SUM(LOST_ENERGY)/SUM(DISTANCE_DIFFERENCE) as LOSS_PER_METER ";
                    query += "    from ECOLOG ";
                    query += "    where TRIP_ID =" + user.tripID + " ";
                    query += "    group by LINK_ID ";
                    query += "    having SUM(DISTANCE_DIFFERENCE) > 0 ";
                    query += "    ) SubTable ";
                    query += "on ECOLOG.LINK_ID = SubTable.LINK_ID ";
                    query += "where TRIP_ID = " + user.tripID + " ";
                    query += "order by JST ";
                    #endregion
                }
                else if (user.value == "LostEnergy+UsedFuel")
                {
                    #region エネルギーロス＋ガソリン
                    query = "select * ";
                    query += "from ECOLOG ";
                    query += "left join ( ";
                    query += "    select LINK_ID, SUM(CONSUMED_FUEL_BY_WELL_TO_WHEEL)/SUM(DISTANCE_DIFFERENCE) as FUEL_PER_METER, SUM(LOST_ENERGY_BY_WELL_TO_WHEEL)/SUM(DISTANCE_DIFFERENCE) as LOSS_PER_METER ";
                    query += "    from ECOLOG ";
                    query += "    where TRIP_ID =" + user.tripID + " ";
                    query += "    group by LINK_ID ";
                    query += "    having SUM(DISTANCE_DIFFERENCE) > 0 ";
                    query += "    ) SubTable ";
                    query += "on ECOLOG.LINK_ID = SubTable.LINK_ID ";
                    query += "where TRIP_ID = " + user.tripID + " ";
                    query += "order by JST ";
                    #endregion
                }
                else if (user.polyline == "trajectory")
                {
                    #region 軌跡のみ
                    query = "select *, LOST_ENERGY/DISTANCE_DIFFERENCE as LOSS_PER_METER ";
                    query += "from ECOLOG ";
                    query += "where TRIP_ID  = " + user.tripID + " and DISTANCE_DIFFERENCE > 0";
                    query += "order by JST ";

                    if (user.usefixed)
                    {
                        query = query.Replace("ECOLOG", "ECOLOG_ALTITUDE_FIXED");
                    }
                    #endregion
                }
                else if (user.polyline == "information")
                {
                    #region 運転情報
                    query = "select * ";
                    query += "from ECOLOG ";
                    query += "left join ( ";
                    query += "    select LINK_ID, SUM(CONSUMED_ELECTRIC_ENERGY)/SUM(DISTANCE_DIFFERENCE) as ENERGY_PER_METER, SUM(LOST_ENERGY)/SUM(DISTANCE_DIFFERENCE) as LOSS_PER_METER ";
                    query += "    from ECOLOG ";
                    query += "    where TRIP_ID = " + user.tripID + " ";
                    query += "    group by LINK_ID ";
                    query += "    having SUM(DISTANCE_DIFFERENCE) > 0 ";
                    query += "    ) SubTable ";
                    query += "on ECOLOG.LINK_ID = SubTable.LINK_ID ";
                    query += "where TRIP_ID = " + user.tripID + " ";
                    query += "order by JST ";

                    if (user.usefixed)
                    {
                        query = query.Replace("ECOLOG", "ECOLOG_ALTITUDE_FIXED");
                    }
                    #endregion
                }
                else if (user.polyline == "average")
                {
                    #region 運転情報＋平均・分散
                    if (user.value == "ConsumedEnergy")
                    {
                        #region 消費エネルギー
                        query = "with thisLinks as ( ";
                        query += "select LINK_ID, SUM(DISTANCE_DIFFERENCE) as SUM_DISTANCE  ";
                        query += "from ECOLOG ";
                        query += "where TRIP_ID = " + user.tripID + " ";
                        query += "group by LINK_ID";
                        query += "), thisLinksPlus as ( ";
                        query += "select ECOLOG.LINK_ID, SUM(CONSUMED_ELECTRIC_ENERGY)/SUM(DISTANCE_DIFFERENCE) as ENERGY_PER_METER_PLUS ";
                        query += "from thisLinks ";
                        query += "left join ECOLOG ";
                        query += "on thisLinks.LINK_ID = ECOLOG.LINK_ID ";
                        query += "where CONSUMED_ELECTRIC_ENERGY > 0 ";
                        query += "and TRIP_ID = " + user.tripID + " ";
                        query += "group by ECOLOG.LINK_ID ";
                        query += "having SUM(DISTANCE_DIFFERENCE) > 0 ";
                        query += "), thisLinksMinus as ( ";
                        query += "select ECOLOG.LINK_ID, SUM(CONSUMED_ELECTRIC_ENERGY)/SUM(DISTANCE_DIFFERENCE) as ENERGY_PER_METER_MINUS ";
                        query += "from thisLinks ";
                        query += "left join ECOLOG ";
                        query += "on thisLinks.LINK_ID = ECOLOG.LINK_ID ";
                        query += "where CONSUMED_ELECTRIC_ENERGY < 0 ";
                        query += "and TRIP_ID = " + user.tripID + " ";
                        query += "group by ECOLOG.LINK_ID ";
                        query += "having SUM(DISTANCE_DIFFERENCE) > 0 ";
                        query += "), AllLinksPlus as ( ";
                        query += "select ECOLOG.TRIP_ID, ECOLOG.LINK_ID, SUM(CONSUMED_ELECTRIC_ENERGY)/SUM(DISTANCE_DIFFERENCE) as ENERGY_PER_METER_PLUS ";
                        if (!user.direction.Equals(""))
                        {
                            query += "from ECOLOG ";
                            query += "right join thisLinks ";
                            query += "on ECOLOG.LINK_ID = thisLinks.LINK_ID ";
                            query += "right join ( ";
                            query += "    select * ";
                            query += "    from TRIPS ";
                            query += "    where TRIP_DIRECTION = '" + user.direction + "' ";
                            query += "    ) TRIPS ";
                            query += "on ECOLOG.TRIP_ID = TRIPS.TRIP_ID  ";
                        }
                        else
                        {
                            query += "from ECOLOG ";
                            query += "right join thisLinks ";
                            query += "on ECOLOG.LINK_ID = thisLinks.LINK_ID ";
                        }
                        query += "where CONSUMED_ELECTRIC_ENERGY > 0 ";
                        query += "group by ECOLOG.TRIP_ID, ECOLOG.LINK_ID ";
                        query += "having SUM(DISTANCE_DIFFERENCE) > 0 ";
                        query += "), AllLinksMinus as ( ";
                        query += "select ECOLOG.TRIP_ID, ECOLOG.LINK_ID, SUM(CONSUMED_ELECTRIC_ENERGY)/SUM(DISTANCE_DIFFERENCE) as ENERGY_PER_METER_MINUS ";
                        if (!user.direction.Equals(""))
                        {
                            query += "from ECOLOG ";
                            query += "right join thisLinks ";
                            query += "on ECOLOG.LINK_ID = thisLinks.LINK_ID ";
                            query += "right join ( ";
                            query += "    select * ";
                            query += "    from TRIPS ";
                            query += "    where TRIP_DIRECTION = '" + user.direction + "' ";
                            query += "    ) TRIPS ";
                            query += "on ECOLOG.TRIP_ID = TRIPS.TRIP_ID  ";
                        }
                        else
                        {
                            query += "from ECOLOG ";
                            query += "right join thisLinks ";
                            query += "on ECOLOG.LINK_ID = thisLinks.LINK_ID ";
                        }
                        query += "where CONSUMED_ELECTRIC_ENERGY < 0 ";
                        query += "group by ECOLOG.TRIP_ID, ECOLOG.LINK_ID ";
                        query += "having SUM(DISTANCE_DIFFERENCE) > 0 ";
                        query += "), AveragePlus as ( ";
                        query += "select LINK_ID, AVG(ENERGY_PER_METER_PLUS) as AVG_ENERGY_PLUS, STDEV(ENERGY_PER_METER_PLUS) as SIGMA_ENERGY_PLUS ";
                        query += "from AllLinksPlus ";
                        query += "group by LINK_ID ";
                        query += "), AverageMinus as ( ";
                        query += "select LINK_ID, AVG(ENERGY_PER_METER_MINUS) as AVG_ENERGY_MINUS, STDEV(ENERGY_PER_METER_MINUS) as SIGMA_ENERGY_MINUS ";
                        query += "from AllLinksMinus ";
                        query += "group by LINK_ID ";
                        query += ") ";
                        query += " ";
                        query += "select ECOLOG.*, ENERGY_PER_METER_PLUS, ENERGY_PER_METER_MINUS, AVG_ENERGY_PLUS, SIGMA_ENERGY_PLUS, AVG_ENERGY_MINUS, SIGMA_ENERGY_MINUS  ";
                        query += "from ECOLOG ";
                        query += "left join thisLinksPlus ";
                        query += "on ECOLOG.LINK_ID = thisLinksPlus.LINK_ID ";
                        query += "left join thisLinksMinus ";
                        query += "on ECOLOG.LINK_ID = thisLinksMinus.LINK_ID ";
                        query += "left join AveragePlus ";
                        query += "on ECOLOG.LINK_ID = AveragePlus.LINK_ID ";
                        query += "left join AverageMinus ";
                        query += "on ECOLOG.LINK_ID = AverageMinus.LINK_ID ";
                        query += "where TRIP_ID = " + user.tripID + " ";
                        query += "order by JST ";

                        if (user.usefixed)
                        {
                            query = query.Replace("ECOLOG", "ECOLOG_ALTITUDE_FIXED\r\n");
                        }
                        #endregion
                    }
                    else
                    {
                        #region エネルギーロス・速度・加速度
                        query = "with thisLinks as ( ";
                        query += "select LINK_ID, SUM(LOST_ENERGY)/SUM(DISTANCE_DIFFERENCE) as LOSS_PER_METER  ";
                        query += "from ECOLOG ";
                        query += "where TRIP_ID = " + user.tripID + " ";
                        query += "group by LINK_ID ";
                        query += "having SUM(DISTANCE_DIFFERENCE) > 0 ";
                        query += "), AllLinks as ( ";
                        query += "select ECOLOG.TRIP_ID, ECOLOG.LINK_ID, AVG(SPEED) as AVG_SPEED, AVG(LONGITUDINAL_ACC) as AVG_LONGITUDINAL_ACC, AVG(LATERAL_ACC) as AVG_LATERAL_ACC, SUM(LOST_ENERGY)/SUM(DISTANCE_DIFFERENCE) as LOSS_PER_METER ";

                        if (!user.direction.Equals(""))
                        {
                            query += "from ECOLOG ";
                            query += "right join thisLinks ";
                            query += "on ECOLOG.LINK_ID = thisLinks.LINK_ID ";
                            query += "right join ( ";
                            query += "    select * ";
                            query += "    from TRIPS ";
                            query += "    where TRIP_DIRECTION = '" + user.direction + "' ";
                            query += "    ) TRIPS ";
                            query += "on ECOLOG.TRIP_ID = TRIPS.TRIP_ID  ";
                        }
                        else
                        {
                            query += "from ECOLOG ";
                            query += "right join thisLinks ";
                            query += "on ECOLOG.LINK_ID = thisLinks.LINK_ID ";
                        }

                        query += "group by ECOLOG.TRIP_ID, ECOLOG.LINK_ID ";
                        query += "having SUM(DISTANCE_DIFFERENCE) > 0 ";
                        query += "), Average as ( ";
                        query += "select LINK_ID, AVG(AVG_SPEED) as AVG_SPEED, STDEV(AVG_SPEED) as SIGMA_SPEED, AVG(AVG_LONGITUDINAL_ACC) as AVG_LONGITUDINAL_ACC, STDEV(AVG_LONGITUDINAL_ACC) as SIGMA_LONGITUDINAL_ACC, AVG(AVG_LATERAL_ACC) as AVG_LATERAL_ACC, STDEV(AVG_LATERAL_ACC) as SIGMA_LATERAL_ACC, AVG(LOSS_PER_METER) as AVG_LOSS, STDEV(LOSS_PER_METER) as SIGMA_LOSS ";
                        query += "from AllLinks ";
                        query += "group by LINK_ID ";
                        query += ") ";
                        query += " ";
                        query += "select ECOLOG.*, AVG_SPEED, SIGMA_SPEED, AVG_LONGITUDINAL_ACC, SIGMA_LONGITUDINAL_ACC, AVG_LATERAL_ACC, SIGMA_LATERAL_ACC, LOSS_PER_METER, AVG_LOSS, SIGMA_LOSS  ";
                        query += "from ECOLOG ";
                        query += "left join thisLinks ";
                        query += "on ECOLOG.LINK_ID = thisLinks.LINK_ID ";
                        query += "left join Average ";
                        query += "on ECOLOG.LINK_ID = Average.LINK_ID ";
                        query += "where TRIP_ID = " + user.tripID + " ";
                        query += "order by JST ";

                        if (user.usefixed)
                        {
                            query = query.Replace("ECOLOG", "ECOLOG_ALTITUDE_FIXED");
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (user.polyline == "worst")
                {
                    #region ワースト
                    query = "select * ";
                    query += "from ECOLOG ";
                    query += "where TRIP_ID  = " + user.tripID + " ";
                    query += "order by JST ";

                    dtSelect = DatabaseAccess.GetResult(querySelect);

                    DataColumnCollection columsSelect = dtSelect.Columns;
                    DataRowCollection rowsSelect = dtSelect.Rows;
                    #endregion
                }

                if (MainForm.DebugMode)
                {
                    QueryView form = new QueryView(query);
                    form.ShowDialog();

                    if (form.DialogResult == DialogResult.OK)
                    {
                        query = form.GetQuery();
                    }
                }
                query = query.Replace("ECOLOG", MainForm.ECOLOGTable);
                dt = DatabaseAccess.GetResult(query);
                #endregion

                #region 緯度・経度データの設定
                sb.Append(" map.setCenter(new google.maps.LatLng(" + dt.Rows[0]["LATITUDE"].ToString() + ", " + dt.Rows[0]["LONGITUDE"].ToString() + "));\r\n");

                if (user.value == "UsedFuel")
                {
                    #region ガソリン
                    sb.Append(" var GPSdata = new Array();\r\n");

                    string lat = dt.Rows[0]["LATITUDE"].ToString();
                    string lng = dt.Rows[0]["LONGITUDE"].ToString();
                    string time = dt.Rows[0]["JST"].ToString();
                    string acc_x = dt.Rows[0]["LONGITUDINAL_ACC"].ToString();
                    string acc_y = dt.Rows[0]["LATERAL_ACC"].ToString();
                    string acc_z = dt.Rows[0]["VERTICAL_ACC"].ToString();
                    string speed = dt.Rows[0]["SPEED"].ToString();
                    string heading;
                    string efficiency = dt.Rows[0]["EFFICIENCY"].ToString();
                    string consumption_energy = dt.Rows[0]["ENERGY_PER_METER"].ToString();
                    string lost_energy = dt.Rows[0]["LOSS_PER_METER"].ToString();
                    string agg = dt.Rows[0][user.aggregation].ToString().Trim();

                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        heading =
                            (Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng))) > 0)
                            ? Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng))).ToString()
                            : (Math.PI * 2 + Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng)))).ToString();

                        sb.Append(" GPSdata.push({" +
                        "lat:'" + lat +
                        "', lng:'" + lng +
                        "', time:'" + time +
                        "', acc_x:'" + acc_x +
                        "', acc_y:'" + acc_y +
                        "', acc_z:'" + acc_z +
                        "', speed:'" + speed +
                        "', heading:'" + double.Parse(heading) * 180 / Math.PI +
                        "', efficiency:'" + efficiency +
                        "', consumption_energy:'" + consumption_energy +
                        "', lost_energy:'" + lost_energy +
                        "', " + user.aggregation + ":'" + agg +
                        "', dx:'" + Calculation.calc_fuel_x(heading, dt.Rows[i]["FUEL_PER_METER"].ToString()) +
                        "', dy:'" + Calculation.calc_fuel_y(heading, dt.Rows[i]["FUEL_PER_METER"].ToString()) +
                        "'});\r\n");

                        sb.Append(" GPSdata.push({" +
                        "lat:'" + dt.Rows[i]["LATITUDE"].ToString() +
                        "', lng:'" + dt.Rows[i]["LONGITUDE"].ToString() +
                        "', time:'" + dt.Rows[i]["JST"].ToString() +
                        "', acc_x:'" + dt.Rows[i]["LONGITUDINAL_ACC"].ToString() +
                        "', acc_y:'" + dt.Rows[i]["LATERAL_ACC"].ToString() +
                        "', acc_z:'" + dt.Rows[i]["VERTICAL_ACC"].ToString() +
                        "', speed:'" + dt.Rows[i]["SPEED"].ToString() +
                        "', heading:'" + double.Parse(heading) * 180 / Math.PI +
                        "', efficiency:'" + dt.Rows[i]["EFFICIENCY"].ToString() +
                        "', consumption_energy:'" + dt.Rows[i]["CONSUMED_ELECTRIC_ENERGY"].ToString() +
                        "', lost_energy:'" + dt.Rows[i]["LOSS_PER_METER"].ToString() +
                        "', " + user.aggregation + ":'" + dt.Rows[i][user.aggregation].ToString() +
                        "', dx:'" + Calculation.calc_fuel_x(heading, dt.Rows[i]["FUEL_PER_METER"].ToString()) +
                        "', dy:'" + Calculation.calc_fuel_y(heading, dt.Rows[i]["FUEL_PER_METER"].ToString()) +
                        "'});\r\n");

                        lat = dt.Rows[i]["LATITUDE"].ToString();
                        lng = dt.Rows[i]["LONGITUDE"].ToString();
                        time = dt.Rows[i]["JST"].ToString();
                        acc_x = dt.Rows[i]["LONGITUDINAL_ACC"].ToString();
                        acc_y = dt.Rows[i]["LATERAL_ACC"].ToString();
                        acc_z = dt.Rows[i]["VERTICAL_ACC"].ToString();
                        speed = dt.Rows[i]["SPEED"].ToString();
                        efficiency = dt.Rows[i]["EFFICIENCY"].ToString();
                        consumption_energy = dt.Rows[i]["ENERGY_PER_METER"].ToString();
                        lost_energy = dt.Rows[i]["LOSS_PER_METER"].ToString();
                        agg = dt.Rows[i][user.aggregation].ToString().Trim();
                    }
                    sb.Append(" \r\n");

                    #endregion
                }
                else if (user.value == "LostEnergy+UsedFuel")
                {
                    #region エネルギーロス＋ガソリン
                    sb.Append(" var GPSdata = new Array();\r\n");

                    string lat = dt.Rows[0]["LATITUDE"].ToString();
                    string lng = dt.Rows[0]["LONGITUDE"].ToString();
                    string time = dt.Rows[0]["JST"].ToString();
                    string acc_x = dt.Rows[0]["LONGITUDINAL_ACC"].ToString();
                    string acc_y = dt.Rows[0]["LATERAL_ACC"].ToString();
                    string acc_z = dt.Rows[0]["VERTICAL_ACC"].ToString();
                    string speed = dt.Rows[0]["SPEED"].ToString();
                    string heading;
                    string efficiency = dt.Rows[0]["EFFICIENCY"].ToString();
                    string consumption_energy = dt.Rows[0]["ENERGY_PER_METER"].ToString();
                    string lost_energy = dt.Rows[0]["LOSS_PER_METER"].ToString();
                    string agg = dt.Rows[0][user.aggregation].ToString().Trim();

                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        heading =
                            (Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng))) > 0)
                            ? Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng))).ToString()
                            : (Math.PI * 2 + Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng)))).ToString();

                        sb.Append(" GPSdata.push({" +
                        "lat:'" + lat +
                        "', lng:'" + lng +
                        "', time:'" + time +
                        "', acc_x:'" + acc_x +
                        "', acc_y:'" + acc_y +
                        "', acc_z:'" + acc_z +
                        "', speed:'" + speed +
                        "', heading:'" + double.Parse(heading) * 180 / Math.PI +
                        "', efficiency:'" + efficiency +
                        "', consumption_energy:'" + consumption_energy +
                        "', lost_energy:'" + lost_energy +
                        "', " + user.aggregation + ":'" + agg +
                        "', dx_plus:'" + Calculation.calc_fuel_x(heading, dt.Rows[i]["FUEL_PER_METER"].ToString()) +
                        "', dy_plus:'" + Calculation.calc_fuel_y(heading, dt.Rows[i]["FUEL_PER_METER"].ToString()) +
                        "', dx_minus:'" + Calculation.calc_fuel_x(heading, dt.Rows[i]["LOSS_PER_METER"].ToString()) +
                        "', dy_minus:'" + Calculation.calc_fuel_y(heading, dt.Rows[i]["LOSS_PER_METER"].ToString()) +
                        "'});\r\n");

                        sb.Append(" GPSdata.push({" +
                        "lat:'" + dt.Rows[i]["LATITUDE"].ToString() +
                        "', lng:'" + dt.Rows[i]["LONGITUDE"].ToString() +
                        "', time:'" + dt.Rows[i]["JST"].ToString() +
                        "', acc_x:'" + dt.Rows[i]["LONGITUDINAL_ACC"].ToString() +
                        "', acc_y:'" + dt.Rows[i]["LATERAL_ACC"].ToString() +
                        "', acc_z:'" + dt.Rows[i]["VERTICAL_ACC"].ToString() +
                        "', speed:'" + dt.Rows[i]["SPEED"].ToString() +
                        "', heading:'" + double.Parse(heading) * 180 / Math.PI +
                        "', efficiency:'" + dt.Rows[i]["EFFICIENCY"].ToString() +
                        "', consumption_energy:'" + dt.Rows[i]["CONSUMED_ELECTRIC_ENERGY"].ToString() +
                        "', lost_energy:'" + dt.Rows[i]["LOSS_PER_METER"].ToString() +
                        "', " + user.aggregation + ":'" + dt.Rows[i][user.aggregation].ToString() +
                        "', dx_plus:'" + Calculation.calc_fuel_x(heading, dt.Rows[i]["FUEL_PER_METER"].ToString()) +
                        "', dy_plus:'" + Calculation.calc_fuel_y(heading, dt.Rows[i]["FUEL_PER_METER"].ToString()) +
                        "', dx_minus:'" + Calculation.calc_fuel_x(heading, dt.Rows[i]["LOSS_PER_METER"].ToString()) +
                        "', dy_minus:'" + Calculation.calc_fuel_y(heading, dt.Rows[i]["LOSS_PER_METER"].ToString()) +
                        "'});\r\n");

                        lat = dt.Rows[i]["LATITUDE"].ToString();
                        lng = dt.Rows[i]["LONGITUDE"].ToString();
                        time = dt.Rows[i]["JST"].ToString();
                        acc_x = dt.Rows[i]["LONGITUDINAL_ACC"].ToString();
                        acc_y = dt.Rows[i]["LATERAL_ACC"].ToString();
                        acc_z = dt.Rows[i]["VERTICAL_ACC"].ToString();
                        speed = dt.Rows[i]["SPEED"].ToString();
                        efficiency = dt.Rows[i]["EFFICIENCY"].ToString();
                        consumption_energy = dt.Rows[i]["ENERGY_PER_METER"].ToString();
                        lost_energy = dt.Rows[i]["LOSS_PER_METER"].ToString();
                        agg = dt.Rows[i][user.aggregation].ToString().Trim();
                    }
                    sb.Append(" \r\n");
                    #endregion
                }
                else if (user.polyline == "trajectory")
                {
                    #region 軌跡のみ
                    sb.Append(" var GPSdata = new Array();\r\n");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append(" GPSdata.push({" +
                        "lat:'" + dt.Rows[i]["LATITUDE"].ToString() +
                        "', lng:'" + dt.Rows[i]["LONGITUDE"].ToString() +
                        "', time:'" + dt.Rows[i]["JST"].ToString() +
                        "', acc_x:'" + dt.Rows[i]["LONGITUDINAL_ACC"].ToString() +
                        "', acc_y:'" + dt.Rows[i]["LATERAL_ACC"].ToString() +
                        "', acc_z:'" + dt.Rows[i]["VERTICAL_ACC"].ToString() +
                        "', speed:'" + dt.Rows[i]["SPEED"].ToString() +
                        "', heading:'" + dt.Rows[i]["HEADING"].ToString() +
                        "', efficiency:'" + dt.Rows[i]["EFFICIENCY"].ToString() +
                        "', lost_energy:'" + dt.Rows[i]["LOSS_PER_METER"].ToString() +
                        "', consumption_energy:'" + dt.Rows[i]["CONSUMED_ELECTRIC_ENERGY"].ToString() +
                        "', " + user.aggregation + ":'" + dt.Rows[i][user.aggregation].ToString().Trim() +
                        "'});\r\n");
                    }
                    sb.Append(" \r\n");
                    #endregion
                }
                else if (user.polyline == "information")
                {
                    #region 運転情報
                    sb.Append(" var GPSdata = new Array();\r\n");

                    string lat = dt.Rows[0]["LATITUDE"].ToString();
                    string lng = dt.Rows[0]["LONGITUDE"].ToString();
                    string time = dt.Rows[0]["JST"].ToString();
                    string acc_x = dt.Rows[0]["LONGITUDINAL_ACC"].ToString();
                    string acc_y = dt.Rows[0]["LATERAL_ACC"].ToString();
                    string acc_z = dt.Rows[0]["VERTICAL_ACC"].ToString();
                    string speed = dt.Rows[0]["SPEED"].ToString();
                    string heading;
                    string efficiency = dt.Rows[0]["EFFICIENCY"].ToString();
                    string consumption_energy = dt.Rows[0]["ENERGY_PER_METER"].ToString();
                    string lost_energy = dt.Rows[0]["LOSS_PER_METER"].ToString();
                    string agg = dt.Rows[0][user.aggregation].ToString().Trim();

                    if (user.value == "ConsumedEnergy")
                    {
                        #region 消費エネルギー表示
                        for (int i = 1; i < dt.Rows.Count; i++)
                        {
                            heading =
                                    (Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng))) > 0)
                                    ? Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng))).ToString()
                                    : (Math.PI * 2 + Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng)))).ToString();

                            sb.Append(" GPSdata.push({" +
                            "lat:'" + lat +
                            "', lng:'" + lng +
                            "', time:'" + time +
                            "', acc_x:'" + acc_x +
                            "', acc_y:'" + acc_y +
                            "', acc_z:'" + acc_z +
                            "', speed:'" + speed +
                            "', heading:'" + double.Parse(heading) * 180 / Math.PI +
                            "', efficiency:'" + efficiency +
                            "', consumption_energy:'" + consumption_energy +
                            "', lost_energy:'" + lost_energy +
                            "', " + user.aggregation + ":'" + agg +
                            "', dx:'" + Calculation.calc_energy_x(heading, dt.Rows[i]["ENERGY_PER_METER"].ToString()) +
                            "', dy:'" + Calculation.calc_energy_y(heading, dt.Rows[i]["ENERGY_PER_METER"].ToString()) +
                            "'});\r\n");

                            sb.Append(" GPSdata.push({" +
                            "lat:'" + dt.Rows[i]["LATITUDE"].ToString() +
                            "', lng:'" + dt.Rows[i]["LONGITUDE"].ToString() +
                            "', time:'" + dt.Rows[i]["JST"].ToString() +
                            "', acc_x:'" + dt.Rows[i]["LONGITUDINAL_ACC"].ToString() +
                            "', acc_y:'" + dt.Rows[i]["LATERAL_ACC"].ToString() +
                            "', acc_z:'" + dt.Rows[i]["VERTICAL_ACC"].ToString() +
                            "', speed:'" + dt.Rows[i]["SPEED"].ToString() +
                            "', heading:'" + double.Parse(heading) * 180 / Math.PI +
                            "', efficiency:'" + dt.Rows[i]["EFFICIENCY"].ToString() +
                            "', consumption_energy:'" + dt.Rows[i]["CONSUMED_ELECTRIC_ENERGY"].ToString() +
                            "', lost_energy:'" + dt.Rows[i]["LOSS_PER_METER"].ToString() +
                            "', " + user.aggregation + ":'" + dt.Rows[i][user.aggregation].ToString() +
                            "', dx:'" + Calculation.calc_energy_x(heading, dt.Rows[i]["ENERGY_PER_METER"].ToString()) +
                            "', dy:'" + Calculation.calc_energy_y(heading, dt.Rows[i]["ENERGY_PER_METER"].ToString()) +
                            "'});\r\n");

                            lat = dt.Rows[i]["LATITUDE"].ToString();
                            lng = dt.Rows[i]["LONGITUDE"].ToString();
                            time = dt.Rows[i]["JST"].ToString();
                            acc_x = dt.Rows[i]["LONGITUDINAL_ACC"].ToString();
                            acc_y = dt.Rows[i]["LATERAL_ACC"].ToString();
                            acc_z = dt.Rows[i]["VERTICAL_ACC"].ToString();
                            speed = dt.Rows[i]["SPEED"].ToString();
                            efficiency = dt.Rows[i]["EFFICIENCY"].ToString();
                            consumption_energy = dt.Rows[i]["ENERGY_PER_METER"].ToString();
                            lost_energy = dt.Rows[i]["LOSS_PER_METER"].ToString();
                            agg = dt.Rows[i][user.aggregation].ToString().Trim();
                        }
                        #endregion
                    }
                    else if (user.value == "LostEnergy")
                    {
                        #region エネルギーロス表示
                        for (int i = 1; i < dt.Rows.Count; i++)
                        {
                            heading =
                                (Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng))) > 0)
                                ? Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng))).ToString()
                                : (Math.PI * 2 + Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng)))).ToString();

                            sb.Append(" GPSdata.push({" +
                            "lat:'" + lat +
                            "', lng:'" + lng +
                            "', time:'" + time +
                            "', acc_x:'" + acc_x +
                            "', acc_y:'" + acc_y +
                            "', acc_z:'" + acc_z +
                            "', speed:'" + speed +
                            "', heading:'" + double.Parse(heading) * 180 / Math.PI +
                            "', efficiency:'" + efficiency +
                            "', consumption_energy:'" + consumption_energy +
                            "', lost_energy:'" + lost_energy +
                            "', " + user.aggregation + ":'" + agg +
                            "', dx:'" + Calculation.calc_energyloss_x(heading, dt.Rows[i]["LOSS_PER_METER"].ToString()) +
                            "', dy:'" + Calculation.calc_energyloss_y(heading, dt.Rows[i]["LOSS_PER_METER"].ToString()) +
                            "'});\r\n");

                            sb.Append(" GPSdata.push({" +
                            "lat:'" + dt.Rows[i]["LATITUDE"].ToString() +
                            "', lng:'" + dt.Rows[i]["LONGITUDE"].ToString() +
                            "', time:'" + dt.Rows[i]["JST"].ToString() +
                            "', acc_x:'" + dt.Rows[i]["LONGITUDINAL_ACC"].ToString() +
                            "', acc_y:'" + dt.Rows[i]["LATERAL_ACC"].ToString() +
                            "', acc_z:'" + dt.Rows[i]["VERTICAL_ACC"].ToString() +
                            "', speed:'" + dt.Rows[i]["SPEED"].ToString() +
                            "', heading:'" + double.Parse(heading) * 180 / Math.PI +
                            "', efficiency:'" + dt.Rows[i]["EFFICIENCY"].ToString() +
                            "', consumption_energy:'" + dt.Rows[i]["CONSUMED_ELECTRIC_ENERGY"].ToString() +
                            "', lost_energy:'" + dt.Rows[i]["LOSS_PER_METER"].ToString() +
                            "', " + user.aggregation + ":'" + dt.Rows[i][user.aggregation].ToString() +
                            "', dx:'" + Calculation.calc_energyloss_x(heading, dt.Rows[i]["LOSS_PER_METER"].ToString()) +
                            "', dy:'" + Calculation.calc_energyloss_y(heading, dt.Rows[i]["LOSS_PER_METER"].ToString()) +
                            "'});\r\n");

                            lat = dt.Rows[i]["LATITUDE"].ToString();
                            lng = dt.Rows[i]["LONGITUDE"].ToString();
                            time = dt.Rows[i]["JST"].ToString();
                            acc_x = dt.Rows[i]["LONGITUDINAL_ACC"].ToString();
                            acc_y = dt.Rows[i]["LATERAL_ACC"].ToString();
                            acc_z = dt.Rows[i]["VERTICAL_ACC"].ToString();
                            speed = dt.Rows[i]["SPEED"].ToString();
                            efficiency = dt.Rows[i]["EFFICIENCY"].ToString();
                            consumption_energy = dt.Rows[i]["ENERGY_PER_METER"].ToString();
                            lost_energy = dt.Rows[i]["LOSS_PER_METER"].ToString();
                            agg = dt.Rows[i][user.aggregation].ToString().Trim();
                        }
                        #endregion
                    }
                    else if (user.value == "Speed")
                    {
                        #region 速度表示
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sb.Append(" GPSdata.push({" +
                            "lat:'" + dt.Rows[i]["LATITUDE"].ToString() +
                            "', lng:'" + dt.Rows[i]["LONGITUDE"].ToString() +
                            "', time:'" + dt.Rows[i]["JST"].ToString() +
                            "', acc_x:'" + dt.Rows[i]["LONGITUDINAL_ACC"].ToString() +
                            "', acc_y:'" + dt.Rows[i]["LATERAL_ACC"].ToString() +
                            "', acc_z:'" + dt.Rows[i]["VERTICAL_ACC"].ToString() +
                            "', speed:'" + dt.Rows[i]["SPEED"].ToString() +
                            "', heading:'" + dt.Rows[i]["HEADING"].ToString() +
                            "', efficiency:'" + dt.Rows[i]["EFFICIENCY"].ToString() +
                            "', consumption_energy:'" + dt.Rows[i]["ENERGY_PER_METER"].ToString() +
                            "', lost_energy:'" + dt.Rows[i]["LOSS_PER_METER"].ToString() +
                            "', " + user.aggregation + ":'" + dt.Rows[i][user.aggregation].ToString().Trim() +
                            "', dx:'" + Calculation.calc_speed_x(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["SPEED"].ToString()) +
                            "', dy:'" + Calculation.calc_speed_y(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["SPEED"].ToString()) +
                            "'});\r\n");

                        }
                        #endregion
                    }
                    else
                    {
                        #region 加速度表示
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sb.Append(" GPSdata.push({" +
                            "lat:'" + dt.Rows[i]["LATITUDE"].ToString() +
                            "', lng:'" + dt.Rows[i]["LONGITUDE"].ToString() +
                            "', time:'" + dt.Rows[i]["JST"].ToString() +
                            "', acc_x:'" + dt.Rows[i]["LONGITUDINAL_ACC"].ToString() +
                            "', acc_y:'" + dt.Rows[i]["LATERAL_ACC"].ToString() +
                            "', acc_z:'" + dt.Rows[i]["VERTICAL_ACC"].ToString() +
                            "', speed:'" + dt.Rows[i]["SPEED"].ToString() +
                            "', heading:'" + dt.Rows[i]["HEADING"].ToString() +
                            "', efficiency:'" + dt.Rows[i]["EFFICIENCY"].ToString() +
                            "', consumption_energy:'" + dt.Rows[i]["ENERGY_PER_METER"].ToString() +
                            "', lost_energy:'" + dt.Rows[i]["LOSS_PER_METER"].ToString() +
                            "', " + user.aggregation + ":'" + dt.Rows[i][user.aggregation].ToString().Trim() +
                            "', dx:'" + Calculation.calc_acc_x(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["LONGITUDINAL_ACC"].ToString(), dt.Rows[i]["LATERAL_ACC"].ToString(), user.value) +
                            "', dy:'" + Calculation.calc_acc_y(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["LONGITUDINAL_ACC"].ToString(), dt.Rows[i]["LATERAL_ACC"].ToString(), user.value) +
                            "'});\r\n");
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (user.polyline == "average")
                {
                    #region 運転情報＋平均・分散
                    sb.Append(" var GPSdata = new Array();\r\n");

                    String lat = dt.Rows[0]["LATITUDE"].ToString();
                    String lng = dt.Rows[0]["LONGITUDE"].ToString();
                    String time = dt.Rows[0]["JST"].ToString();
                    String acc_x = dt.Rows[0]["LONGITUDINAL_ACC"].ToString();
                    String acc_y = dt.Rows[0]["LATERAL_ACC"].ToString();
                    String acc_z = dt.Rows[0]["VERTICAL_ACC"].ToString();
                    String speed = dt.Rows[0]["SPEED"].ToString();
                    String heading;
                    String efficiency = dt.Rows[0]["EFFICIENCY"].ToString();
                    String consumption_energy = dt.Rows[0]["CONSUMED_ELECTRIC_ENERGY"].ToString();
                    String energy_loss = dt.Rows[0]["LOST_ENERGY"].ToString();
                    String agg = dt.Rows[0][user.aggregation].ToString().Trim();

                    if (user.value == "ConsumedEnergy")
                    {
                        #region 消費エネルギー表示
                        for (int i = 1; i < dt.Rows.Count; i++)
                        {
                            heading =
                                (Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng))) > 0)
                                ? Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng))).ToString()
                                : (Math.PI * 2 + Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng)))).ToString();

                            sb.Append(" GPSdata.push({" +
                            "lat:'" + lat +
                            "', lng:'" + lng +
                            "', time:'" + time +
                            "', acc_x:'" + acc_x +
                            "', acc_y:'" + acc_y +
                            "', acc_z:'" + acc_z +
                            "', speed:'" + speed +
                            "', heading:'" + double.Parse(heading) * 180 / Math.PI +
                            "', efficiency:'" + efficiency +
                            "', consumption_energy:'" + consumption_energy +
                            "', lost_energy:'" + energy_loss +
                            "', " + user.aggregation + ":'" + agg +
                            "', dx_plus:'" + Calculation.calc_energy_x(heading, dt.Rows[i]["ENERGY_PER_METER_PLUS"].ToString()) +
                            "', dy_plus:'" + Calculation.calc_energy_y(heading, dt.Rows[i]["ENERGY_PER_METER_PLUS"].ToString()) +
                            "', dx_minus:'" + Calculation.calc_energy_x(heading, dt.Rows[i]["ENERGY_PER_METER_MINUS"].ToString()) +
                            "', dy_minus:'" + Calculation.calc_energy_y(heading, dt.Rows[i]["ENERGY_PER_METER_MINUS"].ToString()) +
                            "', ave_x_plus:'" + Calculation.calc_energy_x(heading, dt.Rows[i]["AVG_ENERGY_PLUS"].ToString()) +
                            "', ave_y_plus:'" + Calculation.calc_energy_y(heading, dt.Rows[i]["AVG_ENERGY_PLUS"].ToString()) +
                            "', sigma_x_plus:'" + Calculation.sigma_energy_x(heading, dt.Rows[i]["SIGMA_ENERGY_PLUS"].ToString()) +
                            "', sigma_y_plus:'" + Calculation.sigma_energy_y(heading, dt.Rows[i]["SIGMA_ENERGY_PLUS"].ToString()) +
                            "', ave_x_minus:'" + Calculation.calc_energy_x(heading, dt.Rows[i]["AVG_ENERGY_MINUS"].ToString()) +
                            "', ave_y_minus:'" + Calculation.calc_energy_y(heading, dt.Rows[i]["AVG_ENERGY_MINUS"].ToString()) +
                            "', sigma_x_minus:'" + Calculation.sigma_energy_x(heading, dt.Rows[i]["SIGMA_ENERGY_MINUS"].ToString()) +
                            "', sigma_y_minus:'" + Calculation.sigma_energy_y(heading, dt.Rows[i]["SIGMA_ENERGY_MINUS"].ToString()) +
                            "'});\r\n");

                            sb.Append(" GPSdata.push({" +
                            "lat:'" + dt.Rows[i]["LATITUDE"].ToString() +
                            "', lng:'" + dt.Rows[i]["LONGITUDE"].ToString() +
                            "', time:'" + dt.Rows[i]["JST"].ToString() +
                            "', acc_x:'" + dt.Rows[i]["LONGITUDINAL_ACC"].ToString() +
                            "', acc_y:'" + dt.Rows[i]["LATERAL_ACC"].ToString() +
                            "', acc_z:'" + dt.Rows[i]["VERTICAL_ACC"].ToString() +
                            "', speed:'" + dt.Rows[i]["SPEED"].ToString() +
                            "', heading:'" + double.Parse(heading) * 180 / Math.PI +
                            "', efficiency:'" + dt.Rows[i]["EFFICIENCY"].ToString() +
                            "', consumption_energy:'" + dt.Rows[i]["CONSUMED_ELECTRIC_ENERGY"].ToString() +
                            "', " + user.aggregation + ":'" + dt.Rows[i][user.aggregation].ToString() +
                            "', dx_plus:'" + Calculation.calc_energy_x(heading, dt.Rows[i]["ENERGY_PER_METER_PLUS"].ToString()) +
                            "', dy_plus:'" + Calculation.calc_energy_y(heading, dt.Rows[i]["ENERGY_PER_METER_PLUS"].ToString()) +
                            "', dx_minus:'" + Calculation.calc_energy_x(heading, dt.Rows[i]["ENERGY_PER_METER_MINUS"].ToString()) +
                            "', dy_minus:'" + Calculation.calc_energy_y(heading, dt.Rows[i]["ENERGY_PER_METER_MINUS"].ToString()) +
                            "', ave_x_plus:'" + Calculation.calc_energy_x(heading, dt.Rows[i]["AVG_ENERGY_PLUS"].ToString()) +
                            "', ave_y_plus:'" + Calculation.calc_energy_y(heading, dt.Rows[i]["AVG_ENERGY_PLUS"].ToString()) +
                            "', sigma_x_plus:'" + Calculation.sigma_energy_x(heading, dt.Rows[i]["SIGMA_ENERGY_PLUS"].ToString()) +
                            "', sigma_y_plus:'" + Calculation.sigma_energy_y(heading, dt.Rows[i]["SIGMA_ENERGY_PLUS"].ToString()) +
                            "', ave_x_minus:'" + Calculation.calc_energy_x(heading, dt.Rows[i]["AVG_ENERGY_MINUS"].ToString()) +
                            "', ave_y_minus:'" + Calculation.calc_energy_y(heading, dt.Rows[i]["AVG_ENERGY_MINUS"].ToString()) +
                            "', sigma_x_minus:'" + Calculation.sigma_energy_x(heading, dt.Rows[i]["SIGMA_ENERGY_MINUS"].ToString()) +
                            "', sigma_y_minus:'" + Calculation.sigma_energy_y(heading, dt.Rows[i]["SIGMA_ENERGY_MINUS"].ToString()) +
                            "'});\r\n");

                            lat = dt.Rows[i]["LATITUDE"].ToString();
                            lng = dt.Rows[i]["LONGITUDE"].ToString();
                            time = dt.Rows[i]["JST"].ToString();
                            acc_x = dt.Rows[i]["LONGITUDINAL_ACC"].ToString();
                            acc_y = dt.Rows[i]["LATERAL_ACC"].ToString();
                            acc_z = dt.Rows[i]["VERTICAL_ACC"].ToString();
                            speed = dt.Rows[i]["SPEED"].ToString();
                            efficiency = dt.Rows[i]["EFFICIENCY"].ToString();
                            consumption_energy = dt.Rows[i]["CONSUMED_ELECTRIC_ENERGY"].ToString();
                            energy_loss = dt.Rows[i]["LOST_ENERGY"].ToString();
                            agg = dt.Rows[i][user.aggregation].ToString().Trim();
                        }
                        #endregion
                    }
                    else if (user.value == "LostEnergy")
                    {
                        #region エネルギーロス表示
                        for (int i = 1; i < dt.Rows.Count; i++)
                        {
                            heading =
                                (Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng))) > 0)
                                ? Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng))).ToString()
                                : (Math.PI * 2 + Math.Atan2((float.Parse(dt.Rows[i]["LATITUDE"].ToString()) - float.Parse(lat)), (float.Parse(dt.Rows[i]["LONGITUDE"].ToString()) - float.Parse(lng)))).ToString();

                            sb.Append(" GPSdata.push({" +
                            "lat:'" + lat +
                            "', lng:'" + lng +
                            "', time:'" + time +
                            "', acc_x:'" + acc_x +
                            "', acc_y:'" + acc_y +
                            "', acc_z:'" + acc_z +
                            "', speed:'" + speed +
                            "', heading:'" + double.Parse(heading) * 180 / Math.PI +
                            "', efficiency:'" + efficiency +
                            "', consumption_energy:'" + consumption_energy +
                            "', lost_energy:'" + energy_loss +
                            "', " + user.aggregation + ":'" + agg +
                            "', dx_plus:'" + Calculation.calc_energyloss_x(heading, dt.Rows[i]["LOSS_PER_METER"].ToString()) +
                            "', dy_plus:'" + Calculation.calc_energyloss_y(heading, dt.Rows[i]["LOSS_PER_METER"].ToString()) +
                            "', ave_x_plus:'" + Calculation.calc_energyloss_x(heading, dt.Rows[i]["AVG_LOSS"].ToString()) +
                            "', ave_y_plus:'" + Calculation.calc_energyloss_y(heading, dt.Rows[i]["AVG_LOSS"].ToString()) +
                            "', sigma_x_plus:'" + Calculation.sigma_energyloss_x(heading, dt.Rows[i]["SIGMA_LOSS"].ToString()) +
                            "', sigma_y_plus:'" + Calculation.sigma_energyloss_y(heading, dt.Rows[i]["SIGMA_LOSS"].ToString()) +
                            "', dx_minus:'" + 0 +
                            "', dy_minus:'" + 0 +
                            "', ave_x_minus:'" + 0 +
                            "', ave_y_minus:'" + 0 +
                            "', sigma_x_minus:'" + 0 +
                            "', sigma_y_minus:'" + 0 +
                            "'});\r\n");

                            sb.Append(" GPSdata.push({" +
                            "lat:'" + dt.Rows[i]["LATITUDE"].ToString() +
                            "', lng:'" + dt.Rows[i]["LONGITUDE"].ToString() +
                            "', time:'" + dt.Rows[i]["JST"].ToString() +
                            "', acc_x:'" + dt.Rows[i]["LONGITUDINAL_ACC"].ToString() +
                            "', acc_y:'" + dt.Rows[i]["LATERAL_ACC"].ToString() +
                            "', acc_z:'" + dt.Rows[i]["VERTICAL_ACC"].ToString() +
                            "', speed:'" + dt.Rows[i]["SPEED"].ToString() +
                            "', heading:'" + double.Parse(heading) * 180 / Math.PI +
                            "', efficiency:'" + dt.Rows[i]["EFFICIENCY"].ToString() +
                            "', consumption_energy:'" + dt.Rows[i]["CONSUMED_ELECTRIC_ENERGY"].ToString() +
                            "', energy_loss:'" + dt.Rows[i]["LOST_ENERGY"].ToString() +
                            "', " + user.aggregation + ":'" + dt.Rows[i][user.aggregation].ToString() +
                            "', dx_plus:'" + Calculation.calc_energyloss_x(heading, dt.Rows[i]["LOSS_PER_METER"].ToString()) +
                            "', dy_plus:'" + Calculation.calc_energyloss_y(heading, dt.Rows[i]["LOSS_PER_METER"].ToString()) +
                            "', ave_x_plus:'" + Calculation.calc_energyloss_x(heading, dt.Rows[i]["AVG_LOSS"].ToString()) +
                            "', ave_y_plus:'" + Calculation.calc_energyloss_y(heading, dt.Rows[i]["AVG_LOSS"].ToString()) +
                            "', sigma_x_plus:'" + Calculation.sigma_energyloss_x(heading, dt.Rows[i]["SIGMA_LOSS"].ToString()) +
                            "', sigma_y_plus:'" + Calculation.sigma_energyloss_y(heading, dt.Rows[i]["SIGMA_LOSS"].ToString()) +
                            "', dx_minus:'" + 0 +
                            "', dy_minus:'" + 0 +
                            "', ave_x_minus:'" + 0 +
                            "', ave_y_minus:'" + 0 +
                            "', sigma_x_minus:'" + 0 +
                            "', sigma_y_minus:'" + 0 +
                            "'});\r\n");

                            lat = dt.Rows[i]["LATITUDE"].ToString();
                            lng = dt.Rows[i]["LONGITUDE"].ToString();
                            time = dt.Rows[i]["JST"].ToString();
                            acc_x = dt.Rows[i]["LONGITUDINAL_ACC"].ToString();
                            acc_y = dt.Rows[i]["LATERAL_ACC"].ToString();
                            acc_z = dt.Rows[i]["VERTICAL_ACC"].ToString();
                            speed = dt.Rows[i]["SPEED"].ToString();
                            efficiency = dt.Rows[i]["EFFICIENCY"].ToString();
                            consumption_energy = dt.Rows[i]["CONSUMED_ELECTRIC_ENERGY"].ToString();
                            energy_loss = dt.Rows[i]["LOST_ENERGY"].ToString();
                            agg = dt.Rows[i][user.aggregation].ToString().Trim();
                        }
                        #endregion
                    }
                    else if (user.value == "Speed")
                    {
                        #region 速度表示
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sb.Append(" GPSdata.push({" +
                            "lat:'" + dt.Rows[i]["LATITUDE"].ToString() +
                            "', lng:'" + dt.Rows[i]["LONGITUDE"].ToString() +
                            "', time:'" + dt.Rows[i]["JST"].ToString() +
                            "', acc_x:'" + dt.Rows[i]["LONGITUDINAL_ACC"].ToString() +
                            "', acc_y:'" + dt.Rows[i]["LATERAL_ACC"].ToString() +
                            "', acc_z:'" + dt.Rows[i]["VERTICAL_ACC"].ToString() +
                            "', speed:'" + dt.Rows[i]["SPEED"].ToString() +
                            "', heading:'" + dt.Rows[i]["HEADING"].ToString() +
                            "', efficiency:'" + dt.Rows[i]["EFFICIENCY"].ToString() +
                            "', consumption_energy:'" + consumption_energy +
                            "', lost_energy:'" + energy_loss +
                            "', " + user.aggregation + ":'" + dt.Rows[i][user.aggregation].ToString().Trim() +
                            "', dx_plus:'" + Calculation.calc_speed_x(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["SPEED"].ToString()) +
                            "', dy_minus:'" + Calculation.calc_speed_y(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["SPEED"].ToString()) +
                            "', ave_x_plus:'" + Calculation.calc_speed_x(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["AVG_SPEED"].ToString()) +
                            "', ave_y_plus:'" + Calculation.calc_speed_y(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["AVG_SPEED"].ToString()) +
                            "', sigma_x_plus:'" + Calculation.sigma_speed_x(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["SIGMA_SPEED"].ToString()) +
                            "', sigma_y_plus:'" + Calculation.sigma_speed_y(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["SIGMA_SPEED"].ToString()) +
                            "'});\r\n");
                        }
                        #endregion
                    }
                    else
                    {
                        #region 加速度表示
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sb.Append(" GPSdata.push({" +
                            "lat:'" + dt.Rows[i]["LATITUDE"].ToString() +
                            "', lng:'" + dt.Rows[i]["LONGITUDE"].ToString() +
                            "', time:'" + dt.Rows[i]["JST"].ToString() +
                            "', acc_x:'" + dt.Rows[i]["LONGITUDINAL_ACC"].ToString() +
                            "', acc_y:'" + dt.Rows[i]["LATERAL_ACC"].ToString() +
                            "', acc_z:'" + dt.Rows[i]["VERTICAL_ACC"].ToString() +
                            "', speed:'" + dt.Rows[i]["SPEED"].ToString() +
                            "', heading:'" + dt.Rows[i]["HEADING"].ToString() +
                            "', efficiency:'" + dt.Rows[i]["EFFICIENCY"].ToString() +
                            "', consumption_energy:'" + dt.Rows[i]["ENERGY_PER_METER"].ToString() +
                            "', lost_energy:'" + dt.Rows[i]["LOSS_PER_METER"].ToString() +
                            "', " + user.aggregation + ":'" + dt.Rows[i][user.aggregation].ToString().Trim() +
                            "', dx_plus:'" + Calculation.calc_acc_x(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["LONGITUDINAL_ACC"].ToString(), dt.Rows[i]["LATERAL_ACC"].ToString(), user.value) +
                            "', dy_plus:'" + Calculation.calc_acc_y(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["LONGITUDINAL_ACC"].ToString(), dt.Rows[i]["LATERAL_ACC"].ToString(), user.value) +
                            "', ave_x_plus:'" + Calculation.calc_acc_x(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["AVG_LONGITUDINAL_ACC"].ToString(), dt.Rows[i]["AVG_LATERAL_ACC"].ToString(), user.value) +
                            "', ave_y_plus:'" + Calculation.calc_acc_y(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["AVG_LONGITUDINAL_ACC"].ToString(), dt.Rows[i]["AVG_LATERAL_ACC"].ToString(), user.value) +
                            "', sigma_x_plus:'" + Calculation.sigma_acc_x(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["SIGMA_LONGITUDINAL_ACC"].ToString(), dt.Rows[i]["SIGMA_LATERAL_ACC"].ToString(), user.value) +
                            "', sigma_y_plus:'" + Calculation.sigma_acc_y(dt.Rows[i]["HEADING"].ToString(), dt.Rows[i]["SIGMA_LONGITUDINAL_ACC"].ToString(), dt.Rows[i]["SIGMA_LATERAL_ACC"].ToString(), user.value) +
                            "'});\r\n");
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (user.polyline == "worst")
                {
                    #region ワースト
                    sb.Append(" var GPSdata = new Array();\r\n");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append(" GPSdata.push({" +
                        "lat:'" + dt.Rows[i]["LATITUDE"].ToString() +
                        "', lng:'" + dt.Rows[i]["LONGITUDE"].ToString() +
                        "', time:'" + dt.Rows[i]["JST"].ToString() +
                        "', acc_x:'" + dt.Rows[i]["LONGITUDINAL_ACC"].ToString() +
                        "', acc_y:'" + dt.Rows[i]["LATERAL_ACC"].ToString() +
                        "', acc_z:'" + dt.Rows[i]["VERTICAL_ACC"].ToString() +
                        "', speed:'" + dt.Rows[i]["SPEED"].ToString() +
                        "', heading:'" + dt.Rows[i]["HEADING"].ToString() +
                        "', efficiency:'" + dt.Rows[i]["EFFICIENCY"].ToString() +
                        "', lost_energy:'" + dt.Rows[i]["LOST_ENERGY"].ToString() +
                        "', consumption_energy:'" + dt.Rows[i]["CONSUMED_ELECTRIC_ENERGY"].ToString() +
                        "', " + user.aggregation + ":'" + dt.Rows[i][user.aggregation].ToString().Trim() +
                        "'});\r\n");
                    }
                    sb.Append(" \r\n");

                    sb.Append(" var GPSdataSelect = new Array();\r\n");
                    for (int i = 0; i < dtSelect.Rows.Count; i++)
                    {
                        if ((double)dtSelect.Rows[i]["LONGITUDE"] > 139.445)
                        {
                            sb.Append(" GPSdataSelect.push({" +
                            "lat:'" + dtSelect.Rows[i]["LATITUDE"].ToString() +
                            "', lng:'" + dtSelect.Rows[i]["LONGITUDE"].ToString() +
                            "', time:'" + dtSelect.Rows[i]["JST"].ToString() +
                            "', acc_x:'" + dtSelect.Rows[i]["LONGITUDINAL_ACC"].ToString() +
                            "', acc_y:'" + dtSelect.Rows[i]["LATERAL_ACC"].ToString() +
                            "', acc_z:'" + dtSelect.Rows[i]["VERTICAL_ACC"].ToString() +
                            "', speed:'" + dtSelect.Rows[i]["SPEED"].ToString() +
                            "', heading:'" + dtSelect.Rows[i]["HEADING"].ToString() +
                            "', efficiency:'" + dtSelect.Rows[i]["EFFICIENCY"].ToString() +
                            "', consumption_energy:'" + dtSelect.Rows[i]["CONSUMED_ELECTRIC_ENERGY"].ToString() +
                            "', " + user.aggregation + ":'" + dtSelect.Rows[i][user.aggregation].ToString().Trim() +
                            "'});\r\n");
                        }
                    }
                    #endregion
                }
                #endregion

                #region Polylineの描画
                if (user.polyline == "trajectory" || user.polyline == "worst")
                {
                    #region 軌跡のみ
                    sb.Append(" var paths =	new Array();\r\n");
                    sb.Append(" for (var i = 0; i < GPSdata.length; i++){\r\n");
                    sb.Append(" var point = new google.maps.LatLng(GPSdata[i].lat,GPSdata[i].lng);\r\n");
                    sb.Append("   paths.push(point);\r\n");
                    sb.Append("   }\r\n");
                    sb.Append("    var poly = new google.maps.Polyline({\r\n");
                    sb.Append("      path: paths,\r\n");
                    sb.Append("      strokeWeight: 3,\r\n");
                    sb.Append("      strokeOpacity: 0.7,\r\n");
                    sb.Append("      strokeColor: \"#FF0000\"\r\n");
                    sb.Append("    });\r\n");
                    sb.Append(" \r\n");
                    sb.Append("    poly.setMap(map);\r\n");
                    sb.Append(" \r\n");
                    #endregion
                }
                else if (user.value == "UsedFuel" || user.polyline == "information")
                {
                    #region 一方向のみ
                    sb.Append(" var paths =	new Array();\r\n");
                    sb.Append(" for (var i in GPSdata){\r\n");
                    sb.Append("	    paths.push(new google.maps.LatLng(eval(GPSdata[i].lat),eval(GPSdata[i].lng)));\r\n");
                    sb.Append(" }\r\n");
                    sb.Append(" for (var i in GPSdata) {\r\n");
                    sb.Append("	    paths.push(new google.maps.LatLng(eval(GPSdata[GPSdata.length -1 - i].lat)+ eval(GPSdata[GPSdata.length -1 - i].dy),eval(GPSdata[GPSdata.length -1 - i].lng)+ eval(GPSdata[GPSdata.length -1 - i].dx)));\r\n");
                    sb.Append(" }\r\n");
                    sb.Append(" var poly = new google.maps.Polygon({\r\n");
                    sb.Append("     paths: paths,\r\n");
                    sb.Append("     strokeColor: \"#FF0000\",\r\n");
                    sb.Append("     strokeOpacity: 0.8,\r\n");
                    sb.Append("     strokeWeight: 2,\r\n");
                    sb.Append("     fillColor: \"#FF0000\",\r\n");
                    sb.Append("     fillOpacity: 0.35\r\n");
                    sb.Append("    });\r\n");
                    sb.Append(" \r\n");
                    sb.Append(" \r\n");
                    sb.Append(" poly.setMap(map);\r\n");
                    sb.Append(" \r\n");
                    #endregion
                }
                else if (user.value == "LostEnergy+UsedFuel")
                {
                    #region エネルギーロス＋ガソリン
                    sb.Append(" var paths =	new Array();\r\n");
                    sb.Append(" for (var i in GPSdata){\r\n");
                    sb.Append("	    paths.push(new google.maps.LatLng(eval(GPSdata[i].lat),eval(GPSdata[i].lng)));\r\n");
                    sb.Append(" }\r\n");
                    sb.Append(" for (var i in GPSdata) {\r\n");
                    sb.Append("	    paths.push(new google.maps.LatLng(eval(GPSdata[GPSdata.length -1 - i].lat)+ eval(GPSdata[GPSdata.length -1 - i].dy_plus),eval(GPSdata[GPSdata.length -1 - i].lng)+ eval(GPSdata[GPSdata.length -1 - i].dx_plus)));\r\n");
                    sb.Append(" }\r\n");
                    sb.Append(" var poly = new google.maps.Polygon({\r\n");
                    sb.Append("     paths: paths,\r\n");
                    sb.Append("     strokeColor: \"#FF0000\",\r\n");
                    sb.Append("     strokeOpacity: 0.8,\r\n");
                    sb.Append("     strokeWeight: 4,\r\n");
                    sb.Append("     fillColor: \"#FF0000\",\r\n");
                    sb.Append("     fillOpacity: 0.35\r\n");
                    sb.Append("    });\r\n");
                    sb.Append(" \r\n");
                    sb.Append(" \r\n");
                    sb.Append(" poly.setMap(map);\r\n");
                    sb.Append(" \r\n");
                    sb.Append(" var paths2 =	new Array();\r\n");
                    sb.Append(" for (var i in GPSdata){\r\n");
                    sb.Append("	    paths2.push(new google.maps.LatLng(eval(GPSdata[i].lat),eval(GPSdata[i].lng)));\r\n");
                    sb.Append(" }\r\n");
                    sb.Append(" for (var i in GPSdata) {\r\n");
                    sb.Append("	    paths2.push(new google.maps.LatLng(eval(GPSdata[GPSdata.length -1 - i].lat)- eval(GPSdata[GPSdata.length -1 - i].dy_minus),eval(GPSdata[GPSdata.length -1 - i].lng)- eval(GPSdata[GPSdata.length -1 - i].dx_minus)));\r\n");
                    sb.Append(" }\r\n");
                    sb.Append(" var poly2 = new google.maps.Polygon({\r\n");
                    sb.Append("     paths: paths2,\r\n");
                    sb.Append("     strokeColor: \"#FF0000\",\r\n");
                    sb.Append("     strokeOpacity: 0.8,\r\n");
                    sb.Append("     strokeWeight: 4,\r\n");
                    sb.Append("     fillColor: \"#FF0000\",\r\n");
                    sb.Append("     fillOpacity: 0.35\r\n");
                    sb.Append("    });\r\n");
                    sb.Append(" \r\n");
                    sb.Append(" \r\n");
                    sb.Append(" poly2.setMap(map);\r\n");
                    sb.Append(" \r\n");
                    #endregion
                }
                else
                {
                    #region 運転情報＋平均・分散
                    sb.Append(" var paths =	new Array();\r\n");
                    sb.Append(" var paths1 = new Array();\r\n");
                    sb.Append(" var paths2 = new Array();\r\n");
                    sb.Append(" var paths3 = new Array();\r\n");
                    sb.Append(" var paths4 = new Array();\r\n");
                    sb.Append(" var paths5 = new Array();\r\n");
                    sb.Append(" for (var i in GPSdata){\r\n");
                    sb.Append("	    paths.push(new google.maps.LatLng(eval(GPSdata[i].lat),eval(GPSdata[i].lng)));\r\n");
                    sb.Append("	    paths1.push(new google.maps.LatLng(eval(GPSdata[i].lat),eval(GPSdata[i].lng)));\r\n");
                    sb.Append("	    paths2.push(new google.maps.LatLng(eval(GPSdata[i].lat),eval(GPSdata[i].lng)));\r\n");
                    sb.Append("     paths3.push(new google.maps.LatLng(eval(GPSdata[i].lat)+ eval(GPSdata[i].ave_y_plus)-eval(GPSdata[i].sigma_y_plus),eval(GPSdata[i].lng)+ eval(GPSdata[i].ave_x_plus)-eval(GPSdata[i].sigma_x_plus)));\r\n");
                    sb.Append("	    paths4.push(new google.maps.LatLng(eval(GPSdata[i].lat),eval(GPSdata[i].lng)));\r\n");
                    sb.Append("     paths5.push(new google.maps.LatLng(eval(GPSdata[i].lat)+ eval(GPSdata[i].ave_y_minus)-eval(GPSdata[i].sigma_y_minus),eval(GPSdata[i].lng)+ eval(GPSdata[i].ave_x_minus)-eval(GPSdata[i].sigma_x_minus)));\r\n");
                    sb.Append(" }\r\n");
                    sb.Append(" for (var i in GPSdata) {\r\n");
                    sb.Append("	    paths.push(new google.maps.LatLng(eval(GPSdata[GPSdata.length -1 - i].lat)+ eval(GPSdata[GPSdata.length -1 - i].dy_plus),eval(GPSdata[GPSdata.length -1 - i].lng)+ eval(GPSdata[GPSdata.length -1 - i].dx_plus)));\r\n");
                    sb.Append("	    paths1.push(new google.maps.LatLng(eval(GPSdata[GPSdata.length -1 - i].lat)+ eval(GPSdata[GPSdata.length -1 - i].dy_minus),eval(GPSdata[GPSdata.length -1 - i].lng)+ eval(GPSdata[GPSdata.length -1 - i].dx_minus)));\r\n");
                    sb.Append("	    paths2.push(new google.maps.LatLng(eval(GPSdata[GPSdata.length -1 - i].lat)+ eval(GPSdata[GPSdata.length -1 - i].ave_y_plus),eval(GPSdata[GPSdata.length -1 - i].lng)+ eval(GPSdata[GPSdata.length -1 - i].ave_x_plus)));\r\n");
                    sb.Append("     paths3.push(new google.maps.LatLng(eval(GPSdata[GPSdata.length -1 - i].lat)+ eval(GPSdata[GPSdata.length -1 - i].ave_y_plus)+eval(GPSdata[GPSdata.length -1 - i].sigma_y_plus),eval(GPSdata[GPSdata.length -1 - i].lng)+ eval(GPSdata[GPSdata.length -1 - i].ave_x_plus)+eval(GPSdata[GPSdata.length -1 - i].sigma_x_plus)));\r\n");
                    sb.Append("	    paths4.push(new google.maps.LatLng(eval(GPSdata[GPSdata.length -1 - i].lat)+ eval(GPSdata[GPSdata.length -1 - i].ave_y_minus),eval(GPSdata[GPSdata.length -1 - i].lng)+ eval(GPSdata[GPSdata.length -1 - i].ave_x_minus)));\r\n");
                    sb.Append("     paths5.push(new google.maps.LatLng(eval(GPSdata[GPSdata.length -1 - i].lat)+ eval(GPSdata[GPSdata.length -1 - i].ave_y_minus)+eval(GPSdata[GPSdata.length -1 - i].sigma_y_minus),eval(GPSdata[GPSdata.length -1 - i].lng)+ eval(GPSdata[GPSdata.length -1 - i].ave_x_minus)+eval(GPSdata[GPSdata.length -1 - i].sigma_x_minus)));\r\n");
                    sb.Append(" }\r\n");
                    sb.Append(" var poly = new google.maps.Polygon({\r\n");
                    sb.Append("     paths: paths,\r\n");
                    sb.Append("     strokeColor: \"#FF0000\",\r\n");
                    sb.Append("     strokeOpacity: 0.8,\r\n");
                    sb.Append("     strokeWeight: 2,\r\n");
                    sb.Append("     fillColor: \"#FF0000\",\r\n");
                    sb.Append("     fillOpacity: 0.5\r\n");
                    sb.Append("    });\r\n");
                    sb.Append(" \r\n");
                    sb.Append(" var poly1 = new google.maps.Polygon({\r\n");
                    sb.Append("     paths: paths1,\r\n");
                    sb.Append("     strokeColor: \"#FF0000\",\r\n");
                    sb.Append("     strokeOpacity: 0.8,\r\n");
                    sb.Append("     strokeWeight: 2,\r\n");
                    sb.Append("     fillColor: \"#FF0000\",\r\n");
                    sb.Append("     fillOpacity: 0.5\r\n");
                    sb.Append("    });\r\n");
                    sb.Append(" \r\n");
                    sb.Append(" var poly2 = new google.maps.Polygon({\r\n");
                    sb.Append("     paths: paths2,\r\n");
                    sb.Append("     strokeColor: \"#0000FF\",\r\n");
                    sb.Append("     strokeOpacity: 0.8,\r\n");
                    sb.Append("     strokeWeight: 2,\r\n");
                    sb.Append("     fillColor: \"#0000FF\",\r\n");
                    sb.Append("     fillOpacity: 0.2\r\n");
                    sb.Append("    });\r\n");
                    sb.Append(" \r\n");
                    sb.Append(" var poly3 = new google.maps.Polygon({\r\n");
                    sb.Append("     paths: paths3,\r\n");
                    sb.Append("     strokeColor: \"#00FF00\",\r\n");
                    sb.Append("     strokeOpacity: 0.8,\r\n");
                    sb.Append("     strokeWeight: 2,\r\n");
                    sb.Append("     fillOpacity: 0.2\r\n");
                    sb.Append("    });\r\n");
                    sb.Append(" \r\n");
                    sb.Append(" var poly4 = new google.maps.Polygon({\r\n");
                    sb.Append("     paths: paths4,\r\n");
                    sb.Append("     strokeColor: \"#000099\",\r\n");
                    sb.Append("     strokeOpacity: 0.8,\r\n");
                    sb.Append("     strokeWeight: 2,\r\n");
                    sb.Append("     fillColor: \"#0000FF\",\r\n");
                    sb.Append("     fillOpacity: 0.2\r\n");
                    sb.Append("    });\r\n");
                    sb.Append(" \r\n");
                    sb.Append(" var poly5 = new google.maps.Polygon({\r\n");
                    sb.Append("     paths: paths5,\r\n");
                    sb.Append("     strokeColor: \"#009900\",\r\n");
                    sb.Append("     strokeOpacity: 0.8,\r\n");
                    sb.Append("     strokeWeight: 2,\r\n");
                    sb.Append("     fillOpacity: 0.2\r\n");
                    sb.Append("    });\r\n");
                    sb.Append(" \r\n");
                    sb.Append(" poly.setMap(map);\r\n");
                    sb.Append(" poly1.setMap(map);\r\n");
                    sb.Append(" poly2.setMap(map);\r\n");
                    sb.Append(" poly3.setMap(map);\r\n");
                    sb.Append(" poly4.setMap(map);\r\n");
                    sb.Append(" poly5.setMap(map);\r\n");
                    sb.Append(" \r\n");
                    #endregion
                }
                #endregion

                #region マーカー配置
                if (user.polyline != "worst")
                {
                    #region 通常表示
                    string image_work_src = "";
                    System.Drawing.Bitmap image_work;
                    if (MainForm.marker)
                    {
                        #region マーカーの大きさで1m辺りのエネルギーロス[kWh/m]を表す
                        image_work_src = "image/icon_large.png";
                        image_work = new System.Drawing.Bitmap(image_work_src);

                        sb.Append(" var image_large = new google.maps.MarkerImage('../../../" + image_work_src + "',\r\n");
                        sb.Append("     new google.maps.Size(" + image_work.Width + ", " + image_work.Height + "),\r\n");
                        sb.Append("     new google.maps.Point(0, 0),\r\n");
                        sb.Append("     new google.maps.Point(" + image_work.Width / 2 + ", " + image_work.Height / 2 + "));\r\n");

                        image_work_src = "image/icon_normal.png";
                        image_work = new System.Drawing.Bitmap(image_work_src);

                        sb.Append(" var image_normal = new google.maps.MarkerImage('../../../" + image_work_src + "',\r\n");
                        sb.Append("     new google.maps.Size(" + image_work.Width + ", " + image_work.Height + "),\r\n");
                        sb.Append("     new google.maps.Point(0, 0),\r\n");
                        sb.Append("     new google.maps.Point(" + image_work.Width / 2 + ", " + image_work.Height / 2 + "));\r\n");

                        image_work_src = "image/icon_small.png";
                        image_work = new System.Drawing.Bitmap(image_work_src);

                        sb.Append(" var image_small = new google.maps.MarkerImage('../../../" + image_work_src + "',\r\n");
                        sb.Append("     new google.maps.Size(" + image_work.Width + ", " + image_work.Height + "),\r\n");
                        sb.Append("     new google.maps.Point(0, 0),\r\n");
                        sb.Append("     new google.maps.Point(" + image_work.Width / 2 + ", " + image_work.Height / 2 + "));\r\n");

                        sb.Append(" var markers = new Array();\r\n");
                        sb.Append(" for (i = 0; i < GPSdata.length; i += " + user.PointingDistance + ") {\r\n");
                        sb.Append("     if(Math.log(GPSdata[i].lost_energy) / Math.log(10.0) <= -4){\r\n");
                        sb.Append("         markers[i] = new google.maps.Marker({\r\n");
                        sb.Append("         position: new google.maps.LatLng(GPSdata[i].lat, GPSdata[i].lng),\r\n");
                        sb.Append("         map: map\r\n");
                        sb.Append("         ,icon: image_small\r\n");
                        sb.Append("         });\r\n");
                        sb.Append("     }else if(Math.log(GPSdata[i].lost_energy) / Math.log(10.0) <= -3.5){\r\n");
                        sb.Append("         markers[i] = new google.maps.Marker({\r\n");
                        sb.Append("         position: new google.maps.LatLng(GPSdata[i].lat, GPSdata[i].lng),\r\n");
                        sb.Append("         map: map\r\n");
                        sb.Append("         ,icon: image_normal\r\n");
                        sb.Append("         });\r\n");
                        sb.Append("     }else{\r\n");
                        sb.Append("         markers[i] = new google.maps.Marker({\r\n");
                        sb.Append("         position: new google.maps.LatLng(GPSdata[i].lat, GPSdata[i].lng),\r\n");
                        sb.Append("         map: map\r\n");
                        sb.Append("         ,icon: image_large\r\n");
                        sb.Append("         });\r\n");
                        sb.Append("     }\r\n");
                        sb.Append(" dispInfo(markers[i],GPSdata[i].time,GPSdata[i].acc_x,GPSdata[i].acc_y,GPSdata[i].acc_z,GPSdata[i].speed,GPSdata[i].heading,GPSdata[i].efficiency,GPSdata[i].consumption_energy,GPSdata[i].lost_energy,GPSdata[i]." + user.aggregation + "); \r\n");
                        sb.Append(" }\r\n");
                        #endregion
                    }
                    else
                    {
                        #region マーカーの色でモーター効率を表す
                        image_work_src = "../../image/icon_red.gif";
                        image_work = new System.Drawing.Bitmap(image_work_src);

                        sb.Append(" var image_red = new google.maps.MarkerImage('../../../" + image_work_src + "',\r\n");
                        sb.Append("     new google.maps.Size(" + image_work.Width + ", " + image_work.Height + "),\r\n");
                        sb.Append("     new google.maps.Point(0, 0),\r\n");
                        sb.Append("     new google.maps.Point(" + image_work.Width / 2 + ", " + image_work.Height / 2 + "));\r\n");

                        image_work_src = "../../image/icon_blue.gif";
                        image_work = new System.Drawing.Bitmap(image_work_src);

                        sb.Append(" var image_blue = new google.maps.MarkerImage('../../../" + image_work_src + "',\r\n");
                        sb.Append("     new google.maps.Size(" + image_work.Width + ", " + image_work.Height + "),\r\n");
                        sb.Append("     new google.maps.Point(0, 0),\r\n");
                        sb.Append("     new google.maps.Point(" + image_work.Width / 2 + ", " + image_work.Height / 2 + "));\r\n");

                        image_work_src = "../../image/icon_white.gif";
                        image_work = new System.Drawing.Bitmap(image_work_src);

                        sb.Append(" var image_white = new google.maps.MarkerImage('../../../" + image_work_src + "',\r\n");
                        sb.Append("     new google.maps.Size(" + image_work.Width + ", " + image_work.Height + "),\r\n");
                        sb.Append("     new google.maps.Point(0, 0),\r\n");
                        sb.Append("     new google.maps.Point(" + image_work.Width / 2 + ", " + image_work.Height / 2 + "));\r\n");

                        sb.Append(" var markers = new Array();\r\n");
                        sb.Append(" for (i = 0; i < GPSdata.length; i += " + user.PointingDistance + ") {\r\n");
                        sb.Append("     if(GPSdata[i].efficiency >= 94){\r\n");
                        sb.Append("         markers[i] = new google.maps.Marker({\r\n");
                        sb.Append("         position: new google.maps.LatLng(GPSdata[i].lat, GPSdata[i].lng),\r\n");
                        sb.Append("         map: map\r\n");
                        sb.Append("         ,icon: image_blue\r\n");
                        sb.Append("         });\r\n");
                        sb.Append("     }else if(GPSdata[i].efficiency > 80){\r\n");
                        sb.Append("         markers[i] = new google.maps.Marker({\r\n");
                        sb.Append("         position: new google.maps.LatLng(GPSdata[i].lat, GPSdata[i].lng),\r\n");
                        sb.Append("         map: map\r\n");
                        sb.Append("         ,icon: image_white\r\n");
                        sb.Append("         });\r\n");
                        sb.Append("     }else{\r\n");
                        sb.Append("         markers[i] = new google.maps.Marker({\r\n");
                        sb.Append("         position: new google.maps.LatLng(GPSdata[i].lat, GPSdata[i].lng),\r\n");
                        sb.Append("         map: map\r\n");
                        sb.Append("         ,icon: image_red\r\n");
                        sb.Append("         });\r\n");
                        sb.Append("     }\r\n");
                        sb.Append(" dispInfo(markers[i],GPSdata[i].time,GPSdata[i].acc_x,GPSdata[i].acc_y,GPSdata[i].acc_z,GPSdata[i].speed,GPSdata[i].heading,GPSdata[i].efficiency,GPSdata[i].consumption_energy,GPSdata[i].lost_energy,GPSdata[i]." + user.aggregation + "); \r\n");
                        sb.Append(" }\r\n");
                        #endregion
                    }

                    //マーカクリック時に情報窓表示+ダブルクリックで画像表示                    
                    sb.Append(" function dispInfo(marker,time, acc_x,acc_y,acc_z,speed,heading,efficiency,consumption_energy,lost_energy,aggregation) {\r\n");
                    sb.Append("     google.maps.event.addListener(marker, 'click', function(event) {\r\n");
                    sb.Append("         var string = \" time : \" + time + \"<br> longitudinal_acc : \" + acc_x + \"<br> lateral_acc : \" + acc_y+ \"<br> vertical_z : \" + acc_z+ \"<br> speed : \" + speed+ \"<br> heading : \" + heading+ \"<br> efficiency : \" + efficiency + \"<br> consumption_energy : \" + consumption_energy + \"<br> lost_energy : \" + lost_energy + \"<br> " + user.aggregation + " : \" + aggregation + \"<br>\";\r\n");
                    sb.Append("         new google.maps.InfoWindow({content:string,disableAutoPan: true}).open(marker.getMap(), marker);\r\n");
                    sb.Append("         window.external.IconClick(time, acc_x,acc_y,acc_z,speed,consumption_energy,lost_energy);\r\n");
                    sb.Append("     });\r\n");
                    sb.Append("     google.maps.event.addListener(marker, 'rightclick', function(event) {\r\n");
                    sb.Append("         window.external.IconRightClick(time);\r\n");
                    sb.Append("     });\r\n");
                    sb.Append(" }\r\n");
                    #endregion
                }
                else
                {
                    #region ワースト表示
                    sb.Append(" var image_black = new google.maps.MarkerImage('../../image/icon_black.gif',\r\n");
                    sb.Append("     new google.maps.Size(15, 15),\r\n");
                    sb.Append("     new google.maps.Point(0, 0),\r\n");
                    sb.Append("     new google.maps.Point(8, 8));\r\n");
                    sb.Append(" var markers = new Array();\r\n");
                    sb.Append(" for (i = 0; i < GPSdataSelect.length; i++) {\r\n");
                    sb.Append("         markers[i] = new google.maps.Marker({\r\n");
                    sb.Append("         position: new google.maps.LatLng(GPSdataSelect[i].lat, GPSdataSelect[i].lng),\r\n");
                    sb.Append("         map: map\r\n");
                    sb.Append("         ,icon: image_black\r\n");
                    sb.Append("         });\r\n");
                    sb.Append(" dispInfo(markers[i],GPSdataSelect[i].time,GPSdataSelect[i].acc_x,GPSdataSelect[i].acc_y,GPSdataSelect[i].acc_z,GPSdataSelect[i].speed,GPSdataSelect[i].heading,GPSdataSelect[i].efficiency,GPSdataSelect[i].consumption_energy,GPSdataSelect[i].lost_energy,GPSdataSelect[i]." + user.aggregation + "); \r\n");
                    sb.Append(" }\r\n");

                    //マーカクリック時に情報窓表示+ダブルクリックで画像表示                    
                    sb.Append(" function dispInfo(marker,time, acc_x,acc_y,acc_z,speed,heading,efficiency,consumption_energy,lost_energy,aggregation) {\r\n");
                    sb.Append("     google.maps.event.addListener(marker, 'click', function(event) {\r\n");
                    sb.Append("         var string = \" time : \" + time + \"<br> longitudinal_acc : \" + acc_x + \"<br> lateral_acc : \" + acc_y+ \"<br> vertical_z : \" + acc_z+ \"<br> speed : \" + speed+ \"<br> heading : \" + heading+ \"<br> efficiency : \" + efficiency + \"<br> consumption_energy : \" + consumption_energy + \"<br> lost_energy : \" + lost_energy + \"<br> " + user.aggregation + " : \" + aggregation + \"<br>\";\r\n");
                    sb.Append("         new google.maps.InfoWindow({content:string,disableAutoPan: true}).open(marker.getMap(), marker);\r\n");
                    sb.Append("         window.external.IconClick(time, acc_x,acc_y,acc_z,speed,consumption_energy,lost_energy);\r\n");
                    sb.Append("     });\r\n");
                    sb.Append("     google.maps.event.addListener(marker, 'rightclick', function(event) {\r\n");
                    sb.Append("         window.external.IconRightClick(time);\r\n");
                    sb.Append("     });\r\n");
                    sb.Append(" }\r\n");
                    #endregion
                }
                #endregion

                #region ボトム部
                sb.Append("image_center = new google.maps.MarkerImage('../../../image/center_marker.gif', new google.maps.Size(25, 25), new google.maps.Point(0, 0), new google.maps.Point(12, 12));\r\n");
                sb.Append("\r\n");
                sb.Append("center_marker = new google.maps.Marker({position: map.getCenter(), icon: image_center, zIndex: 400});\r\n");
                sb.Append("center_marker.setMap(map);\r\n");
                sb.Append("\r\n");
                sb.Append("}\r\n");
                sb.Append("</script>\r\n");
                sb.Append("</head>\r\n");
                sb.Append("<body style=\"margin:0px; padding:0px;\" onload=\"initialize()\">\r\n");
                sb.Append("<div id=\"map\" style=\"width: 480; height: 360;\"></div>\r\n");
                sb.Append("</body>\r\n");
                sb.Append("</html>\r\n");
                #endregion

                #region ファイルに書き出す
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
                sb.Clear();
                dt.Reset();
                #endregion

                return true;
            }
            else
            {
                MessageBox.Show("Please Select a Trip.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// 指定したセマンティックリンク中の全リンクを軌跡として表示する
        /// </summary>
        public bool makeFile_SemanticLink()
        {
            if (user.semanticLinkID != null)
            {
                #region HTMLフォルダ作成
                if (!Directory.Exists(user.currentDirectory))
                {
                    Directory.CreateDirectory(user.currentDirectory);
                }
                #endregion

                #region 前準備
                StringBuilder sb = new StringBuilder();
                user.currentFile = user.currentDirectory + @"\SemanticLink.html";
                StreamWriter sw = new StreamWriter(user.currentFile);
                #endregion

                #region DBからデータ取得
                DataTable dt2 = new DataTable();

                string query = "select * ";
                query += "from SEMANTIC_LINKS ";
                query += "where SEMANTIC_LINK_ID = '" + user.semanticLinkID + "' ";

                dt = DatabaseAccess.GetResult(query);

                DataColumnCollection colums = dt.Columns;
                DataRowCollection rows = dt.Rows;

                sb.Append("<html>\r\n");
                sb.Append("<head>\r\n");
                sb.Append("<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>\r\n");
//                sb.Append("<script type=\"text/javascript\" src=\"http://maps.google.com/maps/api/js?sensor=false\"></script>\r\n");
                sb.Append("<script type=\"text/javascript\" src=\"http://maps.google.com/maps/api/js?v=3.7&sensor=false&language=ja\"></script>\r\n");
                sb.Append("<script type=\"text/javascript\">\r\n");

                #region リンクデータの取得
                for (int j = 0; j < rows.Count; j++)
                {
                    // リンクの取得
                    query = "select * ";
                    query += "from LINKS ";
                    query += "where LINK_ID = '" + dt.Rows[j]["LINK_ID"].ToString().Trim() + "' ";
                    query += "order by NUM ";

                    dt2 = DatabaseAccess.GetResult(query);

                    DataColumnCollection colums2 = dt2.Columns;
                    DataRowCollection rows2 = dt2.Rows;

                    sb.Append(" var GPSdata" + j + " = new Array();\r\n");

                    for (int i = 0; i < rows2.Count; i++)
                    {
                        sb.Append(" GPSdata" + j + ".push({" +
                        "lat:'" + rows2[i]["LATITUDE"].ToString() +
                        "', lng:'" + rows2[i]["LONGITUDE"].ToString() +
                        "', LINK_ID:'" + rows2[i]["LINK_ID"].ToString().Trim() +
                        "', NODE_ID:'" + rows2[i]["NODE_ID"].ToString().Trim() +
                        "'});\r\n");
                    }
                    sb.Append(" \r\n");

                }
                #endregion


                #endregion

                #region 初期化関数
                sb.Append("var map;\r\n");
                sb.Append("var image_center;\r\n");
                sb.Append("var center_marker;\r\n");
                sb.Append("function initialize() {\r\n");
                sb.Append(" map = new google.maps.Map(document.getElementById(\"map\"), {\r\n");
                sb.Append("     zoom: 14,\r\n");
                sb.Append("     center: new google.maps.LatLng(" + dt2.Rows[0]["LATITUDE"].ToString() + ", " + dt2.Rows[0]["LONGITUDE"].ToString() + "),\r\n");
                sb.Append("     mapTypeId: google.maps.MapTypeId.ROADMAP\r\n");
                sb.Append(" });\r\n");
                sb.Append(" \r\n");
                sb.Append(" var image_black = new google.maps.MarkerImage('../../../image/icon_black.gif',\r\n");
                sb.Append("     new google.maps.Size(15, 15),\r\n");
                sb.Append("     new google.maps.Point(0, 0),\r\n");
                sb.Append("     new google.maps.Point(8, 8));\r\n");
                sb.Append(" \r\n");
                #endregion

                for (int j = 0; j < rows.Count; j++)
                {
                    // Polyline描画
                    sb.Append(" var paths" + j + " =	new Array();\r\n");
                    sb.Append(" for (var i = 0; i < GPSdata" + j + ".length; i++){\r\n");
                    sb.Append("	    paths" + j + ".push(new google.maps.LatLng(eval(GPSdata" + j + "[i].lat),eval(GPSdata" + j + "[i].lng)));\r\n");
                    sb.Append(" }\r\n");
                    sb.Append(" var poly" + j + " = new google.maps.Polyline({\r\n");
                    sb.Append("     path: paths" + j + ",\r\n");
                    sb.Append("     strokeColor: \"#FF0000\",\r\n");
                    sb.Append("     strokeOpacity: 0.8,\r\n");
                    sb.Append("     strokeWeight: 2\r\n");
                    sb.Append("    });\r\n");
                    sb.Append(" \r\n");
                    sb.Append(" poly" + j + ".setMap(map);\r\n");
                    sb.Append(" \r\n");

                    // マーカー配置
                    sb.Append(" var markers" + j + " = new Array();\r\n");
                    sb.Append(" for (i = 0; i < GPSdata" + j + ".length; i ++) {\r\n");
                    sb.Append("       markers" + j + "[i] = new google.maps.Marker({\r\n");
                    sb.Append("       position: new google.maps.LatLng(GPSdata" + j + "[i].lat, GPSdata" + j + "[i].lng),\r\n");
                    sb.Append("       map: map\r\n");
                    sb.Append("       ,icon: image_black\r\n");
                    sb.Append("       });\r\n");
                    sb.Append(" dispInfo(markers" + j + "[i], GPSdata" + j + "[i].LINK_ID, GPSdata" + j + "[i].NODE_ID); \r\n");
                    sb.Append(" }\r\n");
                    sb.Append(" \r\n");

                }

                //マーカクリック時に情報窓表示
                sb.Append(" function dispInfo(marker,aggregation,node) { \r\n");
                sb.Append("     google.maps.event.addListener(marker, 'click', function(event) {\r\n");
                sb.Append("         var string = \"SEMANTIC_LINK_ID : " + dt.Rows[0]["SEMANTIC_LINK_ID"].ToString() + "<br>SEMANTICS : " + dt.Rows[0]["SEMANTICS"].ToString() + "<br>LINK_ID : \" + aggregation + \"<br>NODE_ID : \" + node + \"<br>\";\r\n");
                sb.Append("         new google.maps.InfoWindow({content:string,disableAutoPan: true}).open(marker.getMap(), marker);\r\n");
                sb.Append("     }); \r\n");
                sb.Append(" }\r\n");
                sb.Append("}\r\n");
                sb.Append("</script>\r\n");
                sb.Append("</head>\r\n");
                sb.Append("<body style=\"margin:0px; padding:0px;\" onload=\"initialize()\">\r\n");
                sb.Append("  <div id=\"map\" style=\"width: 300; height: 300;\"></div>\r\n");
                sb.Append("</body>\r\n");
                sb.Append("</html>\r\n");

                #region ファイルに書き出す
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
                sb.Clear();
                dt.Reset();
                #endregion

                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 指定したセマンティックリンク中の全リンクを軌跡として表示する
        /// </summary>
        public bool makeFile_SemanticLink_Old()
        {
            if (user.semanticLinkID != null)
            {
                #region HTMLフォルダ作成
                if (!Directory.Exists(user.currentDirectory))
                {
                    Directory.CreateDirectory(user.currentDirectory);
                }
                #endregion

                #region 前準備
                StringBuilder sb = new StringBuilder();
                user.currentFile = user.currentDirectory + @"\CarTrajectory" + DateTime.Now.ToString("yyyyMMddHHmmss") + @".html";
                StreamWriter sw = new StreamWriter(user.currentFile);
                #endregion

                #region DBからデータ取得
                DataTable dt2 = new DataTable();

                string query = "select * ";
                query += "from SEMANTIC_LINKS ";
                query += "where SEMANTIC_LINK_ID = '" + user.semanticLinkID + "' ";

                dt = DatabaseAccess.GetResult(query);

                DataColumnCollection colums = dt.Columns;
                DataRowCollection rows = dt.Rows;

                sb.Append("<html>\r\n");
                sb.Append("<head>\r\n");
                sb.Append("<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>\r\n");
 //               sb.Append("<script type=\"text/javascript\" src=\"http://maps.google.com/maps/api/js?sensor=false\"></script>\r\n");
                sb.Append("<script type=\"text/javascript\" src=\"http://maps.google.com/maps/api/js?v=3.7&sensor=false&language=ja\"></script>\r\n");
                sb.Append("<script type=\"text/javascript\">\r\n");

                #region リンクデータの取得
                for (int j = 0; j < rows.Count; j++)
                {
                    // リンクの取得
                    query = "select * ";
                    query += "from LINKS ";
                    query += "where LINK_ID = '" + dt.Rows[j]["LINK_ID"].ToString().Trim() + "' ";
                    query += "order by NUM ";

                    dt2 = DatabaseAccess.GetResult(query);

                    DataColumnCollection colums2 = dt2.Columns;
                    DataRowCollection rows2 = dt2.Rows;

                    sb.Append(" var GPSdata" + j + " = new Array();\r\n");

                    for (int i = 0; i < rows2.Count; i++)
                    {
                        sb.Append(" GPSdata" + j + ".push({" +
                        "lat:'" + rows2[i]["LATITUDE"].ToString() +
                        "', lng:'" + rows2[i]["LONGITUDE"].ToString() +
                        "', LINK_ID:'" + rows2[i]["LINK_ID"].ToString().Trim() +
                        "', NODE_ID:'" + rows2[i]["NODE_ID"].ToString().Trim() +
                        "'});\r\n");
                    }
                    sb.Append(" \r\n");

                }
                #endregion


                #endregion

                #region 初期化関数
                sb.Append("var map;\r\n");
                sb.Append("var image_center;\r\n");
                sb.Append("var center_marker;\r\n");
                sb.Append("function initialize() {\r\n");
                sb.Append(" map = new google.maps.Map(document.getElementById(\"map\"), {\r\n");
                sb.Append("     zoom: 14,\r\n");
                sb.Append("     center: new google.maps.LatLng(" + dt2.Rows[0]["LATITUDE"].ToString() + ", " + dt2.Rows[0]["LONGITUDE"].ToString() + "),\r\n");
                sb.Append("     mapTypeId: google.maps.MapTypeId.ROADMAP\r\n");
                sb.Append(" });\r\n");
                sb.Append(" \r\n");
                sb.Append(" var image_black = new google.maps.MarkerImage('../../../image/icon_black.gif',\r\n");
                sb.Append("     new google.maps.Size(15, 15),\r\n");
                sb.Append("     new google.maps.Point(0, 0),\r\n");
                sb.Append("     new google.maps.Point(8, 8));\r\n");
                sb.Append(" \r\n");
                #endregion

                for (int j = 0; j < rows.Count; j++)
                {
                    // Polyline描画
                    sb.Append(" var paths" + j + " =	new Array();\r\n");
                    sb.Append(" for (var i = 0; i < GPSdata" + j + ".length; i++){\r\n");
                    sb.Append("	    paths" + j + ".push(new google.maps.LatLng(eval(GPSdata" + j + "[i].lat),eval(GPSdata" + j + "[i].lng)));\r\n");
                    sb.Append(" }\r\n");
                    sb.Append(" var poly" + j + " = new google.maps.Polyline({\r\n");
                    sb.Append("     path: paths" + j + ",\r\n");
                    sb.Append("     strokeColor: \"#FF0000\",\r\n");
                    sb.Append("     strokeOpacity: 0.8,\r\n");
                    sb.Append("     strokeWeight: 2\r\n");
                    sb.Append("    });\r\n");
                    sb.Append(" \r\n");
                    sb.Append(" poly" + j + ".setMap(map);\r\n");
                    sb.Append(" \r\n");

                    // マーカー配置
                    sb.Append(" var markers" + j + " = new Array();\r\n");
                    sb.Append(" for (i = 0; i < GPSdata" + j + ".length; i ++) {\r\n");
                    sb.Append("       markers" + j + "[i] = new google.maps.Marker({\r\n");
                    sb.Append("       position: new google.maps.LatLng(GPSdata" + j + "[i].lat, GPSdata" + j + "[i].lng),\r\n");
                    sb.Append("       map: map\r\n");
                    sb.Append("       ,icon: image_black\r\n");
                    sb.Append("       });\r\n");
                    sb.Append(" dispInfo(markers" + j + "[i], GPSdata" + j + "[i].LINK_ID, GPSdata" + j + "[i].NODE_ID); \r\n");
                    sb.Append(" }\r\n");
                    sb.Append(" \r\n");

                }

                //マーカクリック時に情報窓表示
                sb.Append(" function dispInfo(marker,aggregation,node) { \r\n");
                sb.Append("     google.maps.event.addListener(marker, 'click', function(event) {\r\n");
                sb.Append("         var string = \"SEMANTIC_LINK_ID : " + dt.Rows[0]["SEMANTIC_LINK_ID"].ToString() + "<br>SEMANTICS : " + dt.Rows[0]["SEMANTICS"].ToString() + "<br>LINK_ID : \" + aggregation + \"<br>NODE_ID : \" + node + \"<br>\";\r\n");
                sb.Append("         new google.maps.InfoWindow({content:string,disableAutoPan: true}).open(marker.getMap(), marker);\r\n");
                sb.Append("     }); \r\n");
                sb.Append("     google.maps.event.addListener(marker, 'rightclick', function(event) {\r\n");
                sb.Append("         window.external.IconRightClick(aggregation, node);\r\n");
                sb.Append("     });\r\n");
                sb.Append(" }\r\n");
                sb.Append("}\r\n");
                sb.Append("</script>\r\n");
                sb.Append("</head>\r\n");
                sb.Append("<body style=\"margin:0px; padding:0px;\" onload=\"initialize()\">\r\n");
                sb.Append("  <div id=\"map\" style=\"width: 550; height: 550;\"></div>\r\n");
                sb.Append("</body>\r\n");
                sb.Append("</html>\r\n");

                #region ファイルに書き出す
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
                sb.Clear();
                dt.Reset();
                #endregion

                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// 指定したラベルを表示する
        /// </summary>
        public bool makeFile_Annotation()
        {
            #region フォルダ作成
            if (!Directory.Exists(user.currentDirectory))
            {
                Directory.CreateDirectory(user.currentDirectory);
            }
            #endregion

            #region 前準備
            StringBuilder sb = new StringBuilder();
            user.currentFile = @user.currentDirectory + @"\car_trajectory.html";
            StreamWriter sw = new StreamWriter(user.currentFile);
            #endregion

            if (user.startTime != null && user.endTime != null)
            {
                #region トップ部
                sb.Append("<html>\r\n");
                sb.Append("<head>\r\n");
                sb.Append("<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>\r\n");
//              sb.Append("<script type=\"text/javascript\" src=\"http://maps.google.com/maps/api/js?sensor=false\"></script>\r\n");
                sb.Append("<script type=\"text/javascript\" src=\"http://maps.google.com/maps/api/js?v=3.7&sensor=false&language=ja\"></script>\r\n");
                sb.Append("<script type=\"text/javascript\">\r\n");
                #endregion

                #region 初期化関数
                sb.Append("var map;\r\n");
                sb.Append("var image_center;\r\n");
                sb.Append("var center_marker;\r\n");
                sb.Append("function initialize() {\r\n");
                sb.Append("     map = new google.maps.Map(document.getElementById(\"map\"), {\r\n");
                sb.Append("     zoom: 17,\r\n");
                sb.Append("     mapTypeId: google.maps.MapTypeId.ROADMAP\r\n");
                sb.Append(" });\r\n");
                sb.Append(" \r\n");
                #endregion

                #region 走行データをDBより取得
                string query = "select * ";
                query += "from ECOLOG ";
                query += "where TRIP_ID  = " + user.tripID + " ";
                query += "and JST between '" + user.startTime + "' and '" + user.endTime + "' ";
                query += "order by JST ";

                dt = DatabaseAccess.GetResult(query);

                DataColumnCollection colums = dt.Columns;
                DataRowCollection rows = dt.Rows;
                #endregion

                #region 緯度・経度データ書き込み
                sb.Append(" map.setCenter(new google.maps.LatLng(" + dt.Rows[0]["LATITUDE"].ToString() + ", " + dt.Rows[0]["LONGITUDE"].ToString() + "));\r\n");

                sb.Append(" var GPSdata = new Array();\r\n");
                for (int i = 0; i < rows.Count; i++)
                {
                    sb.Append(" GPSdata.push({" +
                    "lat:'" + dt.Rows[i]["LATITUDE"].ToString() +
                    "', lng:'" + dt.Rows[i]["LONGITUDE"].ToString() +
                    "', time:'" + dt.Rows[i]["JST"].ToString() +
                    "', acc_x:'" + dt.Rows[i]["LONGITUDINAL_ACC"].ToString() +
                    "', acc_y:'" + dt.Rows[i]["LATERAL_ACC"].ToString() +
                    "', acc_z:'" + dt.Rows[i]["VERTICAL_ACC"].ToString() +
                    "', speed:'" + dt.Rows[i]["SPEED"].ToString() +
                    "', heading:'" + dt.Rows[i]["HEADING"].ToString() +
                    "', efficiency:'" + dt.Rows[i]["EFFICIENCY"].ToString() +
                    "', lost_energy:'" + dt.Rows[i]["LOST_ENERGY"].ToString() +
                    "', consumption_energy:'" + dt.Rows[i]["CONSUMED_ELECTRIC_ENERGY"].ToString() +
                    "', " + user.aggregation + ":'" + dt.Rows[i][user.aggregation].ToString().Trim() +
                    "', annotation:'" + user.annotation +
                    "'});\r\n");
                }
                sb.Append(" \r\n");
                #endregion

                #region Polylineによる経路表示
                sb.Append(" var paths =	new Array();\r\n");
                sb.Append(" for (var i = 0; i < GPSdata.length; i++){\r\n");
                sb.Append(" var point = new google.maps.LatLng(GPSdata[i].lat,GPSdata[i].lng);\r\n");
                sb.Append("   paths.push(point);\r\n");
                sb.Append("   }\r\n");
                sb.Append("    var poly = new google.maps.Polyline({\r\n");
                sb.Append("      path: paths,\r\n");
                sb.Append("      strokeWeight: 3,\r\n");
                sb.Append("      strokeOpacity: 0.7,\r\n");
                sb.Append("      strokeColor: \"#FF0000\"\r\n");
                sb.Append("    });\r\n");
                sb.Append(" \r\n");
                sb.Append("    poly.setMap(map);\r\n");
                sb.Append(" \r\n");
                #endregion

                #region マーカ配置
                sb.Append(" var image_red = new google.maps.MarkerImage('../../image/icon_red.gif',\r\n");
                sb.Append("     new google.maps.Size(15, 15),\r\n");
                sb.Append("     new google.maps.Point(0, 0),\r\n");
                sb.Append("     new google.maps.Point(8, 8));\r\n");
                sb.Append(" var image_blue = new google.maps.MarkerImage('../../image/icon_blue.gif',\r\n");
                sb.Append("     new google.maps.Size(15, 15),\r\n");
                sb.Append("     new google.maps.Point(0, 0),\r\n");
                sb.Append("     new google.maps.Point(8, 8));\r\n");
                sb.Append(" var image_white = new google.maps.MarkerImage('../../image/icon_white.gif',\r\n");
                sb.Append("     new google.maps.Size(15, 15),\r\n");
                sb.Append("     new google.maps.Point(0, 0),\r\n");
                sb.Append("     new google.maps.Point(8, 8));\r\n");
                sb.Append(" var markers = new Array();\r\n");
                sb.Append(" for (i = 0; i < GPSdata.length; i += " + user.PointingDistance + ") {\r\n");
                sb.Append("     if(GPSdata[i].efficiency >= 94){\r\n");
                sb.Append("         markers[i] = new google.maps.Marker({\r\n");
                sb.Append("         position: new google.maps.LatLng(GPSdata[i].lat, GPSdata[i].lng),\r\n");
                sb.Append("         map: map\r\n");
                sb.Append("         ,icon: image_blue\r\n");
                sb.Append("         });\r\n");
                sb.Append("     }else if(GPSdata[i].efficiency > 80){\r\n");
                sb.Append("         markers[i] = new google.maps.Marker({\r\n");
                sb.Append("         position: new google.maps.LatLng(GPSdata[i].lat, GPSdata[i].lng),\r\n");
                sb.Append("         map: map\r\n");
                sb.Append("         ,icon: image_white\r\n");
                sb.Append("         });\r\n");
                sb.Append("     }else{\r\n");
                sb.Append("         markers[i] = new google.maps.Marker({\r\n");
                sb.Append("         position: new google.maps.LatLng(GPSdata[i].lat, GPSdata[i].lng),\r\n");
                sb.Append("         map: map\r\n");
                sb.Append("         ,icon: image_red\r\n");
                sb.Append("         });\r\n");
                sb.Append("     }\r\n");
                sb.Append(" dispInfo(markers[i],GPSdata[i].time,GPSdata[i].acc_x,GPSdata[i].acc_y,GPSdata[i].acc_z,GPSdata[i].speed,GPSdata[i].heading,GPSdata[i].efficiency,GPSdata[i].consumption_energy,GPSdata[i].lost_energy,GPSdata[i]." + user.aggregation + ", GPSdata[i].annotation); \r\n");
                sb.Append(" }\r\n");

                //マーカクリック時に情報窓表示+ダブルクリックで画像表示                    
                sb.Append(" function dispInfo(marker,time, acc_x,acc_y,acc_z,speed,heading,efficiency,consumption_energy,lost_energy,aggregation,annotation) {\r\n");
                sb.Append("     google.maps.event.addListener(marker, 'click', function(event) {\r\n");
                sb.Append("         var string = \" time : \" + time + \"<br> longitudinal_acc : \" + acc_x + \"<br> lateral_acc : \" + acc_y+ \"<br> vertical_z : \" + acc_z+ \"<br> speed : \" + speed+ \"<br> heading : \" + heading+ \"<br> efficiency : \" + efficiency + \"<br> consumption_energy : \" + consumption_energy + \"<br> lost_energy : \" + lost_energy + \"<br> " + user.aggregation + " : \" + aggregation + \"<br> event : \" + annotation +\"<br>\";\r\n");
                sb.Append("         new google.maps.InfoWindow({content:string,disableAutoPan: true}).open(marker.getMap(), marker);\r\n");
                sb.Append("     });\r\n");
                sb.Append(" }\r\n");
                #endregion

                #region ボトム部
                sb.Append("image_center = new google.maps.MarkerImage('../../image/center_marker.gif', new google.maps.Size(25, 25), new google.maps.Point(0, 0), new google.maps.Point(12, 12));\r\n");
                sb.Append("\r\n");
                sb.Append("center_marker = new google.maps.Marker({position: map.getCenter(), icon: image_center, zIndex: 400});\r\n");
                sb.Append("center_marker.setMap(map);\r\n");
                sb.Append("\r\n");
                sb.Append("}\r\n");
                sb.Append("</script>\r\n");
                sb.Append("</head>\r\n");
                sb.Append("<body style=\"margin:0px; padding:0px;\" onload=\"initialize()\">\r\n");
                sb.Append("<div id=\"map\" style=\"width: 480; height: 360;\"></div>\r\n");
                sb.Append("</body>\r\n");
                sb.Append("</html>\r\n");
                #endregion

                #region ファイルに書き出す
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
                sb.Clear();
                dt.Reset();
                #endregion

                return true;
            }
            else
            {
                MessageBox.Show("Please Select an Annotation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
