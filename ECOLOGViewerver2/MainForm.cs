using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECOLOGViewerver2
{
    /// <summary>
    /// メイン画面を取り扱うクラス
    /// </summary>
    public partial class MainForm : Form
    {
        #region 変数定義
        internal static MainForm main;
        internal static string aggregation = "LINK_ID";
        internal static string value = "SUM(LOST_ENERGY)/SUM(DISTANCE_DIFFERENCE)";
        internal static bool WindowMode = false;
        internal static bool DebugMode = false;
        internal static string ECOLOGTable = "ECOLOG";
        internal static bool marker = false;
        /// <summary>
        /// ConnectionString
        /// </summary>
        public static string connectionString;
        /// <summary>
        /// ドライバーデータのリスト
        /// </summary>
        public static Dictionary<string, int> Driver;
        /// <summary>
        /// センサデータのリスト
        /// </summary>
        public static Dictionary<string, int> Sensor;
        /// <summary>
        /// カーデータのリスト
        /// </summary>
        public static Dictionary<string, int> Car;
        /// <summary>
        /// セマンティックリンクデータのリスト
        /// </summary>
        public static Dictionary<string, int> SemanticLink;
        /// <summary>
        /// イベントデータのリスト
        /// </summary>
        public static Dictionary<string, int> Event;

        /// <summary>
        /// データベースアクセス用のクラス
        /// </summary>
        private DatabaseAccess dbaccess;

        public string ConnectionString
        {
            set { connectionString = value; }
            get { return connectionString; }
        }
        

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            
        }

        #region メニュークリック
        private void tripBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TripSelectionDialog dialog = new TripSelectionDialog(connectionString);
            ShowWindow(dialog);
        }

        private void semanticLinkBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SemanticLinkDialog dialog = new SemanticLinkDialog(connectionString);
            ShowWindow(dialog);
        }

        private void v2XEstimationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            V2XDialog dialog = new V2XDialog();
            ShowWindow(dialog);
        }

        private void historyGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoryGraphDialog dialog = new HistoryGraphDialog();
            ShowWindow(dialog);
        }

        private void registeredAnnotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnnotationDialog dialog = new AnnotationDialog();
            ShowWindow(dialog);
        }

        private void dailyEnergyGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TripSelectionDialog_Daily dialog = new TripSelectionDialog_Daily();
            ShowWindow(dialog);
        }
        #endregion

        private void WindowTogglecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (WindowMode)
            {
                WindowMode = false;
            }
            else
            {
                WindowMode = true;
            }
        }

        private void updateECOLOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "update ECOLOG ";
            //query += "set LOST_ENERGY_BY_WELL_TO_WHEEL = LOST_ENERGY * 10.14 ";
            query += "set LOST_ENERGY_BY_WELL_TO_WHEEL = LOST_ENERGY * 9.0 ";
            query += "where LOST_ENERGY_BY_WELL_TO_WHEEL is null; ";
            query += " ";
            //query += "update ECOLOG ";
            //query += "set CONSUMED_FUEL = (select CONSUMED_FUEL from CORRECTED_TORQUE where ECOLOG.JST = CORRECTED_TORQUE.JST and ECOLOG.DRIVER_ID = 1) ";
            //query += "where CONSUMED_FUEL is null; ";
            //query += " ";
            query += "update ECOLOG ";
            //query += "set CONSUMED_FUEL_BY_WELL_TO_WHEEL = CONSUMED_FUEL * 0.04129 ";
            query += "set CONSUMED_FUEL_BY_WELL_TO_WHEEL = CONSUMED_FUEL * 0.0415 ";
            query += "where CONSUMED_FUEL_BY_WELL_TO_WHEEL is null; ";
            query += " ";
            query += "update ECOLOG ";
            query += "set ENERGY_BY_EQUIPMENT = case when DATENAME(HOUR, JST) > 5 and DATENAME(HOUR, JST) <= 17 then 0.2 / 3600 else 0.3 / 3600 end ";
            query += "where ENERGY_BY_EQUIPMENT is null; ";
            query += " ";
            query += "update ECOLOG ";
            query += "set ENERGY_BY_COOLING = ( ";
            query += "	select DISTINCT case when AVG_TEMP >= 20 then (0.035 * AVG_TEMP + -0.49) / 3600.0 else 0 end ";
            query += "	from ( ";
            query += "		select TRIPS.TRIP_ID, AVG(TEMPERATURE) as AVG_TEMP ";
            query += "		FROM WEATHER, TRIPS ";
            query += "		where WEATHER.DATETIME between TRIPS.START_TIME and END_TIME ";
            query += "		group by TRIPS.TRIP_ID ) TEMP ";
            query += "	where ECOLOG.TRIP_ID = TEMP.TRIP_ID ";
            query += "	) ";
            query += "where ENERGY_BY_COOLING is null; ";
            query += " ";
            query += "update ECOLOG ";
            query += "set ENERGY_BY_HEATING = ( ";
            query += "	select DISTINCT case when AVG_TEMP <= 15 then (-0.092 * AVG_TEMP + 1.91) / 3600.0 else 0 end ";
            query += "	from ( ";
            query += "		select TRIPS.TRIP_ID, AVG(TEMPERATURE) as AVG_TEMP ";
            query += "		FROM WEATHER, TRIPS ";
            query += "		where WEATHER.DATETIME between TRIPS.START_TIME and END_TIME ";
            query += "		group by TRIPS.TRIP_ID ) TEMP ";
            query += "	where ECOLOG.TRIP_ID = TEMP.TRIP_ID ";
            query += "	) ";
            query += "where ENERGY_BY_HEATING is null; ";

            QueryView form = new QueryView(query);

            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                query = form.GetQuery();

                if (DatabaseAccess.ExecuteQuery(query,300))
                {
                    MessageBox.Show("Complete", "Success.");
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            using (LoginDialog objLogin = new LoginDialog(this))
            {
                main = new MainForm();


                #region 最新版のチェック
                //try
                //{
                //    string localFile, serverFile;

                //    #region プログラムのバージョンチェック
                //    //ローカルバージョンの取得
                //    localFile = System.Environment.CurrentDirectory;
                //    localFile += @"\ECOLOGViewer.exe";


                //    //サーバーバージョンの取得
                //    using (StreamReader r = new StreamReader(@"Server.txt"))
                //    {
                //        serverFile = r.ReadLine();
                //    }

                //    serverFile = Directory.GetDirectories(serverFile)[0];
                //    serverFile += @"\ECOLOGViewer.exe";
                //    #endregion

                //    if (System.IO.File.GetLastWriteTime(localFile) < System.IO.File.GetLastWriteTime(serverFile))
                //    {
                //        DialogResult result = MessageBox.Show("the Latest Version Exists.\nUpdate now?",
                //           "ECOLOG Viewer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                //        if (result == DialogResult.Yes)
                //        {
                //            string program = System.Environment.CurrentDirectory + @"\ECOLOGViewerUpdater.exe";

                //            if (File.Exists(program))
                //            {
                //                //外部プロセスの起動
                //                try
                //                {
                //                    Process.Start(program);
                //                    return;

                //                }
                //                catch (Exception)
                //                {
                //                }
                //            }
                //            else
                //            {
                //                MessageBox.Show("Updater is Not Found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            }
                //        }
                //    }
                //}
                //catch (IOException)
                //{

                //}
                #endregion

                if (DialogResult.OK == objLogin.ShowDialog(this))
                {
                    dbaccess = new DatabaseAccess(connectionString);

                    Driver = new Dictionary<string, int>();
                    Driver = DatabaseAccess.GetDriver();

                    Sensor = new Dictionary<string, int>();
                    Sensor = DatabaseAccess.GetSensor();

                    Car = new Dictionary<string, int>();
                    Car = DatabaseAccess.GetCar();

                    SemanticLink = new Dictionary<string, int>();
                    SemanticLink = DatabaseAccess.GetSemanticLink();

                    Event = new Dictionary<string, int>();
                    Event = DatabaseAccess.GetEvent();

                    //Application.Run(main);

                    this.Focus();

                    this.TopMost = true;
                    this.TopMost = false;
                }
                else
                {
                    Application.Exit();
                }

                
            }
        }
        

        /// <summary>
        /// フォームを入れ子で表示する
        /// </summary>
        /// <param name="f">表示するフォーム</param>
        public static void ShowWindow(Form f)
        {
            main.TopMost = true;
            main.TopMost = false;

            if (WindowMode)
            {
                //f.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                f.TopLevel = false;
                main.Mainpanel.Controls.Add(f);

                //f.Location = new System.Drawing.Point(main.Location.X + (main.Width - f.Width) / 12, main.Location.Y + (main.Height - f.Height) / 12); 
                //f.Location = new System.Drawing.Point(main.Location.X, main.Location.Y);

                f.Show();
                f.BringToFront();
                f.Focus();
            }
            else
            {
                f.Show(main);
                f.Focus();
            }
        }
    }
}
