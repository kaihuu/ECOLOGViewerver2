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
    /// 
    /// </summary>
    public partial class QueryView : Form
    {
        private string query = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        public QueryView(string str)
        {
            InitializeComponent();

            QuerytextBox.Text = str;
            query = QuerytextBox.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetQuery()
        {
            return query;
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            query = QuerytextBox.Text;
            this.Dispose();
        }

        private void CANCELbutton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
