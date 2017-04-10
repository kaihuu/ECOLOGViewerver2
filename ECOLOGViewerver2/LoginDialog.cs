using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECOLOGViewerver2
{
    /// <summary>
    /// ログイン画面を取り扱うクラス
    /// </summary>
    public partial class LoginDialog : Form
    {
        string connectionString;
        private MainForm main;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LoginDialog()
        {
            InitializeComponent();
            // ENTERボタン押下で[LOGIN]ボタンが押されたことにする
            this.AcceptButton = this.Loginbutton;
            // コントロールボックスの無効化
            this.ControlBox = false;
        }

        public LoginDialog(MainForm main)
        {
            InitializeComponent();
            // ENTERボタン押下で[LOGIN]ボタンが押されたことにする
            this.AcceptButton = this.Loginbutton;
            // コントロールボックスの無効化
            this.ControlBox = false;

            this.main = main;
        }

        private void Loginbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Domain_checkBox.Checked)
                {
                    connectionString = "Data Source=" + Server_textBox.Text + ";Initial Catalog=ECOLOGDBver2;Integrated Security=No;User Id="
                        + UsernameTextBox.Text + ";Password=" + PasswordTextBox.Text + " ;Connect Timeout=15;";
                }
                else
                {
                    connectionString = "Data Source=" + Server_textBox.Text + ";Initial Catalog=ECOLOGDBver2;Integrated Security=Yes;Connect Timeout=15;";

                }

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    Loginbutton.Text = "Trying to Login......";
                    Application.DoEvents();

                    //
                    sqlConnection.Open();
                    sqlConnection.Close();

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

                    Loginbutton.Text = "Login";
                    Application.DoEvents();

                    main.ConnectionString = connectionString;
                }
                this.Dispose();
            }
            catch (Exception ex)
            {
                WriteLog.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + ex.ToString());
                MessageBox.Show("Your Login is Failure.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loginbutton.Text = "Login";
            }
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Domain_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Domain_checkBox.Checked)
            {
                UsernameTextBox.Enabled = true;
                PasswordTextBox.Enabled = true;
            }
            else
            {
                UsernameTextBox.Enabled = false;
                PasswordTextBox.Enabled = false;
            }
        }

        private void Server_textBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
