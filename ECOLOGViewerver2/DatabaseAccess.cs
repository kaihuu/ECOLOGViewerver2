using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECOLOGViewerver2
{
    public class DatabaseAccess
    {
        private static string connectionString;

        public DatabaseAccess(string connection)
        {
            connectionString = connection;
        }

        /// <summary>
        /// DBにアクセスして、データを取得する
        /// </summary>
        /// <param name="query">SQL文</param>
        /// <returns>取得されたデータ(DataTable形式)</returns>
        public static DataTable GetResult(string query)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, connectionString);

                try
                {
                    sqlConnection.Open();
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    cmd.CommandTimeout = 600;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
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
                catch (SqlException se)
                {
                    WriteLog.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + se.ToString());
                    MessageBox.Show(se.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return dt;
        }


        /// <summary>
        /// DBにアクセスして、クエリを実行する
        /// </summary>
        /// <param name="query">SQL文</param>
        /// <returns></returns>
        public static bool ExecuteQuery(string query)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool flg = false;
                try
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    cmd.ExecuteNonQuery();
                    flg = true;
                }
                catch (SqlException se)
                {
                    WriteLog.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + se.ToString());
                    MessageBox.Show(se.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    sqlConnection.Close();
                }
                return flg;
            }
        }

        /// <summary>
        /// DBにアクセスして、クエリを実行する
        /// </summary>
        /// <param name="query">SQL文</param>
        /// <returns></returns>
        public static bool ExecuteQuery(string query, int timeout)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool flg = false;
                try
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    cmd.CommandTimeout = timeout;

                    cmd.ExecuteNonQuery();
                    flg = true;
                }
                catch (SqlException se)
                {
                    WriteLog.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + se.ToString());
                    MessageBox.Show(se.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    sqlConnection.Close();
                }
                return flg;
            }
        }

        /// <summary>
        /// 画像データが存在するか確認する
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int CorrectedPictureDataChecker(string startTime, string endTime)
        {
            int check = 0;

            using (SqlConnection sqlConnection = new SqlConnection(MainForm.connectionString))
            {

                string query = "select COUNT(*) ";
                query += "from CORRECTED_PICTURE ";
                query += "where JST>= '" + startTime + "' ";
                query += "and JST <= '" + endTime + "' ";

                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataReader r = null;

                try
                {
                    sqlConnection.Open();
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        check = r.GetInt32(0);
                    }
                }
                catch (SqlException se)
                {
                }
                finally
                {
                    if (r != null) r.Close();
                    sqlConnection.Close();
                }
            }

            return check;
        }

        /// <summary>
        /// 画像データが存在するか確認する
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int CorrectedPictureDataChecker(string startTime, string endTime, bool useNexus7)
        {
            int check = 0;

            using (SqlConnection sqlConnection = new SqlConnection(MainForm.connectionString))
            {

                string query = "select COUNT(*) ";
                query += "from CORRECTED_PICTURE ";
                query += "where JST>= '" + startTime + "' ";
                query += "and JST <= '" + endTime + "' ";
                if(useNexus7)
                {
                    query += "and SENSOR_ID != 19 ";
                }
                else
                {
                    query += "and SENSOR_ID = 19 ";
                }
                

                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataReader r = null;

                try
                {
                    sqlConnection.Open();
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        check = r.GetInt32(0);
                    }
                }
                catch (SqlException se)
                {
                }
                finally
                {
                    if (r != null) r.Close();
                    sqlConnection.Close();
                }
            }

            return check;
        }

        /// <summary>
        /// DBにアクセスして、ドライバーデータのリストを取得する
        /// </summary>
        /// <returns>取得されたリスト</returns>
        public static Dictionary<string, int> GetDriver()
        {
            Dictionary<string, int> Driver = new Dictionary<string, int>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = "select DRIVERS.NAME, DRIVERS.DRIVER_ID ";
                query += "from DRIVERS ";

                //匿名用クエリ(DEMO用)
                query = "select CONCAT('被験者',DRIVERS.DRIVER_ID) as NAME,DRIVERS.DRIVER_ID  ";
                query += "from DRIVERS ";

                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataReader r = null;

                try
                {
                    sqlConnection.Open();
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        string driver = r.GetInt32(1) + " [" + r.GetString(0) + "]";
                        Driver.Add(driver, r.GetInt32(1));
                    }
                }
                catch (SqlException se)
                {
                    WriteLog.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + se.ToString());
                    //MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (r != null) r.Close();
                    sqlConnection.Close();
                }
            }

            return Driver;
        }
        /// <summary>
        /// DBにアクセスして、センサデータのリストを取得する
        /// </summary>
        /// <returns>取得されたリスト</returns>
        public static Dictionary<string, int> GetSensor()
        {
            Dictionary<string, int> Sensor = new Dictionary<string, int>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = "select SENSORS.SENSOR_MODEL, SENSORS.SENSOR_ID ";
                query += "from SENSORS ";

                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataReader r = null;

                try
                {
                    sqlConnection.Open();
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        string driver_sensor = r.GetInt32(1) + " [" + r.GetString(0) + "]";
                        Sensor.Add(driver_sensor, r.GetInt32(1));
                    }
                }
                catch (SqlException se)
                {
                    WriteLog.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + se.ToString());
                    //MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (r != null) r.Close();
                    sqlConnection.Close();
                }
            }

            return Sensor;
        }
        /// <summary>
        /// DBにアクセスして、カーデータのリストを取得する
        /// </summary>
        /// <returns>取得されたリスト</returns>
        public static Dictionary<string, int> GetCar()
        {
            Dictionary<string, int> Car = new Dictionary<string, int>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = "select MODEL,CAR_ID ";
                query += "from CARS ";

                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataReader r = null;

                try
                {
                    sqlConnection.Open();
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        string car = r.GetInt32(1) + " [" + r.GetString(0).Trim() + "]";
                        Car.Add(car, r.GetInt32(1));
                    }
                }
                catch (SqlException se)
                {
                    WriteLog.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + se.ToString());
                    //MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (r != null) r.Close();
                    sqlConnection.Close();
                }
            }

            return Car;
        }
        /// <summary>
        /// DBにアクセスして、セマンティックリンクデータのリストを取得する
        /// </summary>
        /// <returns>取得されたリスト</returns>
        public static Dictionary<string, int> GetSemanticLink()
        {
            Dictionary<string, int> SemanticLink = new Dictionary<string, int>();

            using (SqlConnection sqlConnection1 = new SqlConnection(connectionString))
            {
                string query = "select DISTINCT SEMANTICS, SEMANTIC_LINK_ID, DRIVER_ID ";
                query += "from SEMANTIC_LINKS ";

                SqlCommand cmd = new SqlCommand(query, sqlConnection1);
                SqlDataReader r = null;

                try
                {
                    sqlConnection1.Open();
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        SemanticLink.Add(r.GetString(0), r.GetInt32(1));
                    }
                }
                catch (SqlException se)
                {
                    WriteLog.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + se.ToString());
                    //MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (r != null) r.Close();
                    sqlConnection1.Close();
                }

            }

            return SemanticLink;
        }
        /// <summary>
        /// DBにアクセスして、イベントデータのリストを取得する
        /// </summary>
        /// <returns>取得されたリスト</returns>
        public static Dictionary<string, int> GetEvent()
        {
            Dictionary<string, int> Event = new Dictionary<string, int>();

            string query = "select EVENT, EVENT_ID ";
            query += "from EVENT ";

            using (SqlConnection sqlConnection1 = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                SqlDataReader r = null;

                try
                {
                    cmd = new SqlCommand(query, sqlConnection1);

                    sqlConnection1.Open();
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        Event.Add(r.GetString(0), r.GetInt32(1));
                    }
                }
                catch (SqlException se)
                {
                    WriteLog.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + se.ToString());
                    //MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (r != null) r.Close();
                    sqlConnection1.Close();
                }
            }

            return Event;
        }
    }
}
