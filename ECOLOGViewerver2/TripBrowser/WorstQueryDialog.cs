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
    /// ワーストクエリ編集ダイアログを取り扱うクラス
    /// </summary>
    public partial class WorstQueryDialog : Form
    {
        internal string query;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WorstQueryDialog()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            WorsttextBox.Text = "with AvgALL as (  \r\n";
            WorsttextBox.Text += "select LINK_ID,(SUM(LOST_ENERGY) / SUM(DISTANCE_DIFFERENCE))as AVG_LOSS,COUNT(*) as count \r\n";
            WorsttextBox.Text += "from ECOLOG \r\n";
            WorsttextBox.Text += "where SPEED > 1  \r\n";
            WorsttextBox.Text += "and DRIVER_ID = driverID \r\n";
            WorsttextBox.Text += "and CAR_ID = carID \r\n";
            WorsttextBox.Text += "and SENSOR_ID = sensorID \r\n";
            WorsttextBox.Text += "group by LINK_ID  \r\n";
            WorsttextBox.Text += "),  \r\n";
            WorsttextBox.Text += "ThisTime as( \r\n";
            WorsttextBox.Text += "select LINK_ID,(SUM(LOST_ENERGY) / SUM(DISTANCE_DIFFERENCE))as This_LOSS \r\n";
            WorsttextBox.Text += "from ECOLOG \r\n";
            WorsttextBox.Text += "where SPEED > 1 \r\n";
            WorsttextBox.Text += "and DRIVER_ID = driverID \r\n";
            WorsttextBox.Text += "and CAR_ID = carID \r\n";
            WorsttextBox.Text += "and SENSOR_ID = sensorID \r\n";
            WorsttextBox.Text += "and JST > 'startTime' \r\n";
            WorsttextBox.Text += "and JST < 'endTime' \r\n";
            WorsttextBox.Text += "group by LINK_ID  \r\n";
            WorsttextBox.Text += "), \r\n";
            WorsttextBox.Text += "DIF_LOS as( \r\n";
            WorsttextBox.Text += "select TOP 20 AvgALL.LINK_ID,This_LOSS,AVG_LOSS,(This_LOSS - AVG_LOSS) as DIF \r\n";
            WorsttextBox.Text += "from ThisTime , AvgALL \r\n";
            WorsttextBox.Text += "where ThisTime.LINK_ID = AvgALL.LINK_ID \r\n";
            WorsttextBox.Text += "and AvgALL.count > 20 \r\n";
            WorsttextBox.Text += "order by DIF desc\r\n";
            WorsttextBox.Text += ") \r\n";
            WorsttextBox.Text += "select * \r\n  ";
            WorsttextBox.Text += "from ECOLOG right join DIF_LOS \r\n";
            WorsttextBox.Text += "on ECOLOG.LINK_ID = DIF_LOS.LINK_ID \r\n";
            WorsttextBox.Text += "where DRIVER_ID = driverID \r\n";
            WorsttextBox.Text += "and CAR_ID = carID\r\n";
            WorsttextBox.Text += "where SENSOR_ID = sensorID \r\n";
            WorsttextBox.Text += "and JST > 'startTime' \r\n";
            WorsttextBox.Text += "and JST < 'endTime' \r\n";

        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="str">現在のクエリ</param>
        public WorstQueryDialog(string str)
        {
            InitializeComponent();
            WorsttextBox.Text = str;
        }

        private void Okbutton_Click(object sender, EventArgs e)
        {
            query = WorsttextBox.Text;
            this.Dispose();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void WorstQueryDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
