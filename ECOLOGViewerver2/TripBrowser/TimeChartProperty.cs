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
    /// 時間軸グラフの詳細表示画面を取り扱うクラス
    /// </summary>
    public partial class TimeChartProperty : Form
    {
        #region 変数
        private string start_time = "";
        private string end_time = "";
        private string content = "";
        private string consumed_energy = "";
        private string lost_energy = "";
        private string air_energy = "";
        private string rolling_energy = "";
        private string climbing_energy = "";
        private string acc_energy = "";
        private string convert_loss = "";
        private string regene_loss = "";
        private string regene_energy = "";
        private TimeChart chart;
        #endregion

        /// <summary>
        /// コンストラクタ（ConsumedEnergy, LostEnergy）
        /// </summary>
        /// <param name="c"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="con"></param>
        /// <param name="ce"></param>
        /// <param name="le"></param>
        public TimeChartProperty(TimeChart c, string start, string end, string con, string ce, string le)
        {
            InitializeComponent();
            chart = c;
            start_time = start;
            end_time = end;
            content = con;
            consumed_energy = ce;
            lost_energy = le;
            init();

            this.ClientSize = new System.Drawing.Size(409, 712);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="con"></param>
        /// <param name="consumed_e"></param>
        /// <param name="lost_e"></param>
        /// <param name="air_e"></param>
        /// <param name="rolling_e"></param>
        /// <param name="climbing_e"></param>
        /// <param name="acc_e"></param>
        /// <param name="conv_l"></param>
        /// <param name="regene_l"></param>
        /// <param name="regene_e"></param>
        public TimeChartProperty(TimeChart c, string start, string end, string con, string consumed_e, string lost_e, string air_e, string rolling_e, string climbing_e, string acc_e, string conv_l, string regene_l, string regene_e)
        {
            InitializeComponent();

            chart = c;
            start_time = start;
            end_time = end;
            content = con;
            consumed_energy = consumed_e;
            lost_energy = lost_e;
            air_energy = air_e;
            rolling_energy = rolling_e;
            climbing_energy = climbing_e;
            acc_energy = acc_e;
            convert_loss = conv_l;
            regene_loss = regene_l;
            regene_energy = regene_e;

            init();
        }

        void init()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            StartTimelabel.Text = start_time;
            EndTimelabel.Text = end_time;
            Chartlabel.Text = content;
            ConsumedEnergylabel.Text = consumed_energy + "[kWh]";
            LostEnergylabel.Text = lost_energy + "[kWh]";
            AirEnergylabel.Text = air_energy + "[kWh]";
            RollingEnergylabel.Text = rolling_energy + "[kWh]";
            ClimbingEmergyPluslabel.Text = climbing_energy + "[kWh]";
            AccEnergylabel.Text = acc_energy + "[kWh]";
            ConvertLossPluslabel.Text = convert_loss + "[kWh]";
            RegeneLosslabel.Text = regene_loss + "[kWh]";
            RegeneEnergylabel.Text = regene_energy + "[kWh]";
        }

        private void TimeChartProperty_FormClosed(object sender, FormClosedEventArgs e)
        {
            chart.propertyShowed = false;
        }
    }
}
