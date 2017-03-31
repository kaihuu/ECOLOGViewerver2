using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECOLOGViewerver2
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            //using (LoginDialog objLogin = new LoginDialog())
            //{
            //    (main = new MainForm()).Show();
                

            //    #region 最新版のチェック
            //    try
            //    {
            //        string localFile, serverFile;

            //        #region プログラムのバージョンチェック
            //        //ローカルバージョンの取得
            //        localFile = System.Environment.CurrentDirectory;
            //        localFile += @"\ECOLOGViewer.exe";


            //        //サーバーバージョンの取得
            //        using (StreamReader r = new StreamReader(@"Server.txt"))
            //        {
            //            serverFile = r.ReadLine();
            //        }

            //        serverFile = Directory.GetDirectories(serverFile)[0];
            //        serverFile += @"\ECOLOGViewer.exe";
            //        #endregion

            //        if (System.IO.File.GetLastWriteTime(localFile) < System.IO.File.GetLastWriteTime(serverFile))
            //        {
            //            DialogResult result = MessageBox.Show("the Latest Version Exists.\nUpdate now?",
            //               "ECOLOG Viewer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            //            if (result == DialogResult.Yes)
            //            {
            //                string program = System.Environment.CurrentDirectory + @"\ECOLOGViewerUpdater.exe";

            //                if (File.Exists(program))
            //                {
            //                    //外部プロセスの起動
            //                    try
            //                    {
            //                        Process.Start(program);
            //                        return;

            //                    }
            //                    catch (Exception)
            //                    {
            //                    }
            //                }
            //                else
            //                {
            //                    MessageBox.Show("Updater is Not Found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }
            //            }
            //        }
            //    }
            //    catch (IOException)
            //    {

            //    }
            //    #endregion

            //    if (DialogResult.OK == objLogin.ShowDialog(main))
            //    {
            //        Driver = new Dictionary<string, int>();
            //        Driver = Get_Driver();

            //        Sensor = new Dictionary<string, int>();
            //        Sensor = Get_Sensor();

            //        Car = new Dictionary<string, int>();
            //        Car = Get_Car();

            //        SemanticLink = new Dictionary<string, int>();
            //        SemanticLink = Get_SemanticLink();

            //        Event = new Dictionary<string, int>();
            //        Event = Get_Event();

            //        Application.Run(main);

            //        main.Focus();

            //        main.TopMost = true;
            //        main.TopMost = false;
            //    }
            //}
        }

        
    }
}
