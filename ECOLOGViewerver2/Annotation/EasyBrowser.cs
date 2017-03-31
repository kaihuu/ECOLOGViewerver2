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
    /// 簡易版ブラウザを取り扱うクラス
    /// </summary>
    public partial class EasyBrowser : Form
    {
        private FormData user;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="u">表示するトリップの情報</param>
        public EasyBrowser(FormData u)
        {
            InitializeComponent();
            user = new FormData(u);
            webBrowser1.Navigate(user.currentFile);
        }

    }
}
