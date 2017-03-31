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
    /// 平均対象選択クエリの編集ダイアログを取り扱うクラス
    /// </summary>
    public partial class AverageQueryDialog : Form
    {
        internal string query;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="str">現在のクエリ</param>
        public AverageQueryDialog(string str)
        {
            InitializeComponent();
            //フォームサイズを固定
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            AveragetextBox.Text = str;
        }

        private void Okbutton_Click(object sender, EventArgs e)
        {
            query = AveragetextBox.Text;
            this.Dispose();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AverageQueryDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
