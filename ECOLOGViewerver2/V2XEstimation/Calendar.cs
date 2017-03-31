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
    /// カレンダー表示を取り扱うクラス
    /// </summary>
    public partial class Calendar : Form
    {
        #region 変数定義
        private Control[] cs, cs2, cs3, cs4, cs5, cs6, cs7, cs8, cs9, cs10;

        private string panelname = "panel";
        private int differenceInDays = -1;
        private int labelnumber = 0;
        private DateTime PastDate = DateTime.Now;
        private int Past_Date = -1;
        private string Year;
        private string Month;
        private bool workstation;
        //private int Number_of_Cars = 0;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="y">表示する年</param>
        /// <param name="m">表示する月</param>
        /// <param name="w">事業所表示か否か</param>
        public Calendar(string y, string m, bool w)
        {
            InitializeComponent();

            this.ClientSize = new System.Drawing.Size(846, 820);

            Year = y;
            Month = m;
            workstation = w;

            #region ラベル初期化
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            for (int i = 1; i < 43; i++)
            {
                panelname = "panel" + i;

                cs = this.Controls.Find(panelname + "_day", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "";

                cs = this.Controls.Find(panelname + "_outward", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "";

                cs = this.Controls.Find(panelname + "_outward_resistance", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "";

                cs = this.Controls.Find(panelname + "_outward_regene", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "";

                cs = this.Controls.Find(panelname + "_homeward", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "";

                cs = this.Controls.Find(panelname + "_homeward_resistance", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "";

                cs = this.Controls.Find(panelname + "_homeward_regene", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "";

                cs = this.Controls.Find(panelname + "_total", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "";

                cs = this.Controls.Find(panelname + "_total_resistance", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "";

                cs = this.Controls.Find(panelname + "_total_regene", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "";

                cs = this.Controls.Find(panelname + "_rest", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "";

                panelname = "paneltotal" + i;

                cs = this.Controls.Find(panelname + "_day", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "0";

                cs = this.Controls.Find(panelname + "_outward", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "0";

                cs = this.Controls.Find(panelname + "_outward_resistance", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "0";

                cs = this.Controls.Find(panelname + "_outward_regene", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "0";

                cs = this.Controls.Find(panelname + "_homeward", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "0";

                cs = this.Controls.Find(panelname + "_homeward_resistance", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "0";

                cs = this.Controls.Find(panelname + "_homeward_regene", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "0";

                cs = this.Controls.Find(panelname + "_total", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "0";

                cs = this.Controls.Find(panelname + "_total_resistance", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "0";

                cs = this.Controls.Find(panelname + "_total_regene", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "0";

                cs = this.Controls.Find(panelname + "_rest", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = "0";


            }

            //タイトルの年月
            if (int.Parse(Month) < 9)
            {
                YearMonth.Text = Year + "/" + Month + "";
            }
            else
            {
                YearMonth.Text = Year + "/" + Month + "";
            }

            rectangleShapeExtra.Visible = true;
            lineShapeExtra1.Visible = true;
            lineShapeExtra2.Visible = true;
            lineShapeExtra3.Visible = true;
            lineShapeExtra4.Visible = true;
            lineShapeExtra5.Visible = true;
            lineShapeExtra6.Visible = true;
            lineShapeExtra7.Visible = true;
            Column6.Visible = true;
            panel36.Visible = true;
            panel37.Visible = true;
            panel38.Visible = true;
            panel39.Visible = true;
            panel40.Visible = true;
            panel41.Visible = true;
            panel42.Visible = true;
            #endregion

            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        internal void addData(DataTable dt_calendar)
        {

            #region 往路・復路の値を反映
            foreach (DataRow dr in dt_calendar.Rows)
            {
                if (differenceInDays == -1)
                {
                    differenceInDays = 0;

                    switch (DateTime.Parse(Year + "-" + Month + "-01 00:00:00").ToString("ddd"))
                    {
                        case "日":
                            labelnumber = 1;
                            break;
                        case "月":
                            labelnumber = 2;
                            break;
                        case "火":
                            labelnumber = 3;
                            break;
                        case "水":
                            labelnumber = 4;
                            break;
                        case "木":
                            labelnumber = 5;
                            break;
                        case "金":
                            labelnumber = 6;
                            break;
                        case "土":
                            labelnumber = 7;
                            break;
                    }

                    int d = int.Parse(dr["START_DAY"].ToString());

                    int daylabelnumber = labelnumber;
                    panelname = "panel" + daylabelnumber;

                    for (int i = 1; i <= DateTime.DaysInMonth(Int32.Parse(Year), Int32.Parse(Month)); i++)
                    {
                        cs = this.Controls.Find(panelname + "_day", true);
                        if (cs.Length > 0)
                            ((Label)cs[0]).Text = i.ToString();
                        panelname = "panel" + (daylabelnumber + i);
                    }

                    Past_Date = int.Parse(dr["START_DAY"].ToString());
                    labelnumber = labelnumber + d - 1;
                }
                else
                {
                    //１つ前との日付差を求める                    
                    differenceInDays = int.Parse(dr["START_DAY"].ToString()) - Past_Date;
                    labelnumber = labelnumber + differenceInDays;
                    Past_Date = int.Parse(dr["START_DAY"].ToString());
                }

                //往路・復路の値を反映
                panelname = "panel" + labelnumber;

                cs = this.Controls.Find(panelname + "_outward", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = dr["消費_out"].ToString();

                cs = this.Controls.Find(panelname + "_outward_resistance", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = dr["力行_out"].ToString();

                cs = this.Controls.Find(panelname + "_outward_regene", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = dr["回生_out"].ToString();

                cs = this.Controls.Find(panelname + "_homeward", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = dr["消費_home"].ToString();

                cs = this.Controls.Find(panelname + "_homeward_resistance", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = dr["力行_home"].ToString();

                cs = this.Controls.Find(panelname + "_homeward_regene", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = dr["回生_home"].ToString();

                //車の台数を仮反映
                cs = this.Controls.Find(panelname + "_rest", true);
                if (cs.Length > 0)
                    ((Label)cs[0]).Text = dr["台数"].ToString();

            }
            #endregion

            #region 合計・残余の値を反映
            //合計・残余の値を反映
            for (int i = 1; i < 43; i++)
            {
                panelname = "panel" + i;

                double outward_energy, homeward_energy;

                cs = this.Controls.Find(panelname + "_total", true);
                cs2 = this.Controls.Find(panelname + "_outward", true);
                cs3 = this.Controls.Find(panelname + "_homeward", true);

                if (cs.Length > 0 && cs2.Length > 0 && cs3.Length > 0)
                {
                    if (((Label)cs2[0]).Text != "")
                    {
                        outward_energy = double.Parse(((Label)cs2[0]).Text);
                    }
                    else
                    {
                        outward_energy = 0;
                    }


                    if (((Label)cs3[0]).Text != "")
                    {
                        homeward_energy = double.Parse(((Label)cs3[0]).Text);
                    }
                    else
                    {
                        homeward_energy = 0;
                    }

                    if (outward_energy + homeward_energy != 0)
                    {
                        ((Label)cs[0]).Text = (outward_energy + homeward_energy).ToString();
                    }
                    else
                    {
                        ((Label)cs[0]).Text = "";
                    }
                }

                cs = this.Controls.Find(panelname + "_total_resistance", true);
                cs2 = this.Controls.Find(panelname + "_outward_resistance", true);
                cs3 = this.Controls.Find(panelname + "_homeward_resistance", true);

                if (cs.Length > 0 && cs2.Length > 0 && cs3.Length > 0)
                {
                    if (((Label)cs2[0]).Text != "")
                    {
                        outward_energy = double.Parse(((Label)cs2[0]).Text);
                    }
                    else
                    {
                        outward_energy = 0;
                    }


                    if (((Label)cs3[0]).Text != "")
                    {
                        homeward_energy = double.Parse(((Label)cs3[0]).Text);
                    }
                    else
                    {
                        homeward_energy = 0;
                    }

                    if (outward_energy + homeward_energy != 0)
                    {
                        ((Label)cs[0]).Text = (outward_energy + homeward_energy).ToString();
                    }
                    else
                    {
                        ((Label)cs[0]).Text = "";
                    }
                }

                cs = this.Controls.Find(panelname + "_total_regene", true);
                cs2 = this.Controls.Find(panelname + "_outward_regene", true);
                cs3 = this.Controls.Find(panelname + "_homeward_regene", true);

                if (cs.Length > 0 && cs2.Length > 0 && cs3.Length > 0)
                {
                    if (((Label)cs2[0]).Text != "")
                    {
                        outward_energy = double.Parse(((Label)cs2[0]).Text);
                    }
                    else
                    {
                        outward_energy = 0;
                    }


                    if (((Label)cs3[0]).Text != "")
                    {
                        homeward_energy = double.Parse(((Label)cs3[0]).Text);
                    }
                    else
                    {
                        homeward_energy = 0;
                    }

                    if (outward_energy + homeward_energy != 0)
                    {
                        ((Label)cs[0]).Text = (outward_energy + homeward_energy).ToString();
                    }
                    else
                    {
                        ((Label)cs[0]).Text = "";
                    }
                }

                cs = this.Controls.Find(panelname + "_rest", true);
                cs2 = this.Controls.Find(panelname + "_total", true);
                cs3 = this.Controls.Find(panelname + "_outward", true);
                cs4 = this.Controls.Find(panelname + "_homeward", true);

                if (cs.Length > 0 && cs2.Length > 0)
                {
                    if (((Label)cs2[0]).Text != "")
                    {
                        double number = double.Parse(((Label)cs[0]).Text);

                        if (!workstation)
                        {
                            ((Label)cs[0]).Text = Math.Round((12.0 * number - double.Parse(((Label)cs2[0]).Text)), 1).ToString();
                        }
                        else if (number > 1)
                        {
                            ((Label)cs[0]).Text = Math.Round((12.0 * number - double.Parse(((Label)cs2[0]).Text)), 1).ToString() + "(" + number + " Cars)";
                        }
                        else
                        {
                            ((Label)cs[0]).Text = Math.Round((12.0 * number - double.Parse(((Label)cs2[0]).Text)), 1).ToString() + "(" + number + " Car)";
                        }
                        ((Label)cs[0]).BackColor = System.Drawing.Color.Silver;
                    }
                    else
                    {
                        ((Label)cs[0]).Text = "";
                    }

                    if (((Label)cs3[0]).Text != "")
                    {
                        outward_energy = double.Parse(((Label)cs3[0]).Text);
                    }
                    else
                    {
                        outward_energy = 0;
                    }


                    if (((Label)cs4[0]).Text != "")
                    {
                        homeward_energy = double.Parse(((Label)cs4[0]).Text);
                    }
                    else
                    {
                        homeward_energy = 0;
                    }

                    if (outward_energy != 0 && homeward_energy != 0)
                    {
                        ((Label)cs[0]).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
                        ((Label)cs[0]).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(80)))));

                    }

                }

            }
            #endregion

            #region 月平均値を反映
            int day_count = 0;

            for (int i = 1; i < 43; i++)
            {
                panelname = "panel" + i;

                cs = this.Controls.Find(panelname + "_outward", true);
                cs2 = this.Controls.Find(panelname + "_outward_resistance", true);
                cs3 = this.Controls.Find(panelname + "_outward_regene", true);
                cs4 = this.Controls.Find(panelname + "_homeward", true);
                cs5 = this.Controls.Find(panelname + "_homeward_resistance", true);
                cs6 = this.Controls.Find(panelname + "_homeward_regene", true);
                cs7 = this.Controls.Find(panelname + "_total", true);
                cs8 = this.Controls.Find(panelname + "_total_resistance", true);
                cs9 = this.Controls.Find(panelname + "_total_regene", true);
                cs10 = this.Controls.Find(panelname + "_rest", true);

                panelname = "paneltotal1";

                if (cs.Length > 0 && cs2.Length > 0 && cs3.Length > 0 && cs4.Length > 0 && cs5.Length > 0 && cs6.Length > 0 && cs7.Length > 0 && cs8.Length > 0 && cs9.Length > 0 && cs10.Length > 0)
                {
                    if ((((Label)cs[0]).Text != "") && (((Label)cs4[0]).Text != "") && (((Label)cs7[0]).Text != ""))
                    {
                        paneltotal1_outward.Text = (double.Parse(paneltotal1_outward.Text) + double.Parse(((Label)cs[0]).Text)).ToString();
                        paneltotal1_outward_resistance.Text = (double.Parse(paneltotal1_outward_resistance.Text) + double.Parse(((Label)cs2[0]).Text)).ToString();
                        paneltotal1_outward_regene.Text = (double.Parse(paneltotal1_outward_regene.Text) + double.Parse(((Label)cs3[0]).Text)).ToString();
                        paneltotal1_homeward.Text = (double.Parse(paneltotal1_homeward.Text) + double.Parse(((Label)cs4[0]).Text)).ToString();
                        paneltotal1_homeward_resistance.Text = (double.Parse(paneltotal1_homeward_resistance.Text) + double.Parse(((Label)cs5[0]).Text)).ToString();
                        paneltotal1_homeward_regene.Text = (double.Parse(paneltotal1_homeward_regene.Text) + double.Parse(((Label)cs6[0]).Text)).ToString();
                        paneltotal1_total.Text = (double.Parse(paneltotal1_total.Text) + double.Parse(((Label)cs7[0]).Text)).ToString();
                        paneltotal1_total_resistance.Text = (double.Parse(paneltotal1_total_resistance.Text) + double.Parse(((Label)cs8[0]).Text)).ToString();
                        paneltotal1_total_regene.Text = (double.Parse(paneltotal1_total_regene.Text) + double.Parse(((Label)cs9[0]).Text)).ToString();
                        paneltotal1_rest.Text = (double.Parse(paneltotal1_rest.Text) + double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", ""))).ToString();

                        day_count++;
                    }
                }
            }

            paneltotal1_outward.Text = Math.Round((double.Parse(paneltotal1_outward.Text) / day_count), 1).ToString();
            paneltotal1_outward_resistance.Text = Math.Round((double.Parse(paneltotal1_outward_resistance.Text) / day_count), 1).ToString();
            paneltotal1_outward_regene.Text = Math.Round((double.Parse(paneltotal1_outward_regene.Text) / day_count), 1).ToString();
            paneltotal1_homeward.Text = Math.Round((double.Parse(paneltotal1_homeward.Text) / day_count), 1).ToString();
            paneltotal1_homeward_resistance.Text = Math.Round((double.Parse(paneltotal1_homeward_resistance.Text) / day_count), 1).ToString();
            paneltotal1_homeward_regene.Text = Math.Round((double.Parse(paneltotal1_homeward_regene.Text) / day_count), 1).ToString();
            paneltotal1_total.Text = Math.Round((double.Parse(paneltotal1_total.Text) / day_count), 1).ToString();
            paneltotal1_total_resistance.Text = Math.Round((double.Parse(paneltotal1_total_resistance.Text) / day_count), 1).ToString();
            paneltotal1_total_regene.Text = Math.Round((double.Parse(paneltotal1_total_regene.Text) / day_count), 1).ToString();
            paneltotal1_rest.Text = Math.Round((double.Parse(paneltotal1_rest.Text) / day_count), 1).ToString();

            #endregion

            #region 標準偏差を反映
            day_count = 0;

            for (int i = 1; i < 43; i++)
            {
                panelname = "panel" + i;

                cs = this.Controls.Find(panelname + "_outward", true);
                cs2 = this.Controls.Find(panelname + "_outward_resistance", true);
                cs3 = this.Controls.Find(panelname + "_outward_regene", true);
                cs4 = this.Controls.Find(panelname + "_homeward", true);
                cs5 = this.Controls.Find(panelname + "_homeward_resistance", true);
                cs6 = this.Controls.Find(panelname + "_homeward_regene", true);
                cs7 = this.Controls.Find(panelname + "_total", true);
                cs8 = this.Controls.Find(panelname + "_total_resistance", true);
                cs9 = this.Controls.Find(panelname + "_total_regene", true);
                cs10 = this.Controls.Find(panelname + "_rest", true);

                if (cs.Length > 0 && cs2.Length > 0 && cs3.Length > 0 && cs4.Length > 0 && cs5.Length > 0 && cs6.Length > 0 && cs7.Length > 0 && cs8.Length > 0 && cs9.Length > 0 && cs10.Length > 0)
                {
                    if ((((Label)cs[0]).Text != "") && (((Label)cs4[0]).Text != "") && (((Label)cs7[0]).Text != ""))
                    {
                        paneltotal2_outward.Text = (double.Parse(paneltotal2_outward.Text) + Math.Pow((double.Parse(((Label)cs[0]).Text) - double.Parse(paneltotal1_outward.Text)), 2)).ToString();
                        paneltotal2_outward_resistance.Text = (double.Parse(paneltotal2_outward_resistance.Text) + Math.Pow((double.Parse(((Label)cs2[0]).Text) - double.Parse(paneltotal1_outward_resistance.Text)), 2)).ToString();
                        paneltotal2_outward_regene.Text = (double.Parse(paneltotal2_outward_regene.Text) + Math.Pow((double.Parse(((Label)cs3[0]).Text) - double.Parse(paneltotal1_outward_regene.Text)), 2)).ToString();
                        paneltotal2_homeward.Text = (double.Parse(paneltotal2_homeward.Text) + Math.Pow((double.Parse(((Label)cs4[0]).Text) - double.Parse(paneltotal1_homeward.Text)), 2)).ToString();
                        paneltotal2_homeward_resistance.Text = (double.Parse(paneltotal2_homeward_resistance.Text) + Math.Pow((double.Parse(((Label)cs5[0]).Text) - double.Parse(paneltotal1_homeward_resistance.Text)), 2)).ToString();
                        paneltotal2_homeward_regene.Text = (double.Parse(paneltotal2_homeward_regene.Text) + Math.Pow((double.Parse(((Label)cs6[0]).Text) - double.Parse(paneltotal1_homeward_regene.Text)), 2)).ToString();
                        paneltotal2_total.Text = (double.Parse(paneltotal2_total.Text) + Math.Pow((double.Parse(((Label)cs7[0]).Text) - double.Parse(paneltotal1_total.Text)), 2)).ToString();
                        paneltotal2_total_resistance.Text = (double.Parse(paneltotal2_total_resistance.Text) + Math.Pow((double.Parse(((Label)cs8[0]).Text) - double.Parse(paneltotal1_total_resistance.Text)), 2)).ToString();
                        paneltotal2_total_regene.Text = (double.Parse(paneltotal2_total_regene.Text) + Math.Pow((double.Parse(((Label)cs9[0]).Text) - double.Parse(paneltotal1_total_regene.Text)), 2)).ToString();
                        paneltotal2_rest.Text = (double.Parse(paneltotal2_rest.Text) + Math.Pow((double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) - double.Parse(paneltotal1_rest.Text)), 2)).ToString();

                        day_count++;
                    }
                }
            }

            paneltotal2_outward.Text = Math.Round((double.Parse(paneltotal2_outward.Text) / day_count), 1).ToString();
            paneltotal2_outward_resistance.Text = Math.Round((double.Parse(paneltotal2_outward_resistance.Text) / day_count), 1).ToString();
            paneltotal2_outward_regene.Text = Math.Round((double.Parse(paneltotal2_outward_regene.Text) / day_count), 1).ToString();
            paneltotal2_homeward.Text = Math.Round((double.Parse(paneltotal2_homeward.Text) / day_count), 1).ToString();
            paneltotal2_homeward_resistance.Text = Math.Round((double.Parse(paneltotal2_homeward_resistance.Text) / day_count), 1).ToString();
            paneltotal2_homeward_regene.Text = Math.Round((double.Parse(paneltotal2_homeward_regene.Text) / day_count), 1).ToString();
            paneltotal2_total.Text = Math.Round((double.Parse(paneltotal2_total.Text) / day_count), 1).ToString();
            paneltotal2_total_resistance.Text = Math.Round((double.Parse(paneltotal2_total_resistance.Text) / day_count), 1).ToString();
            paneltotal2_total_regene.Text = Math.Round((double.Parse(paneltotal2_total_regene.Text) / day_count), 1).ToString();
            paneltotal2_rest.Text = Math.Round((double.Parse(paneltotal2_rest.Text) / day_count), 1).ToString();

            #endregion

            #region 最大・最小を反映

            for (int i = 1; i < 43; i++)
            {
                panelname = "panel" + i;

                cs = this.Controls.Find(panelname + "_outward", true);
                cs2 = this.Controls.Find(panelname + "_outward_resistance", true);
                cs3 = this.Controls.Find(panelname + "_outward_regene", true);
                cs4 = this.Controls.Find(panelname + "_homeward", true);
                cs5 = this.Controls.Find(panelname + "_homeward_resistance", true);
                cs6 = this.Controls.Find(panelname + "_homeward_regene", true);
                cs7 = this.Controls.Find(panelname + "_total", true);
                cs8 = this.Controls.Find(panelname + "_total_resistance", true);
                cs9 = this.Controls.Find(panelname + "_total_regene", true);
                cs10 = this.Controls.Find(panelname + "_rest", true);

                //最小

                if ((((Label)cs[0]).Text != "") && (((Label)cs4[0]).Text != "") && (((Label)cs7[0]).Text != ""))
                {
                    #region 最初の一回
                    if (cs.Length > 0 && ((Label)cs[0]).Text != "")
                    {
                        if (paneltotal3_outward.Text == "0")
                        {
                            paneltotal3_outward.Text = Math.Round(double.Parse(cs[0].Text), 1).ToString();
                        }
                    }

                    if (cs2.Length > 0 && ((Label)cs2[0]).Text != "")
                    {
                        if (paneltotal3_outward_resistance.Text == "0")
                        {
                            paneltotal3_outward_resistance.Text = Math.Round(double.Parse(cs2[0].Text), 1).ToString();
                        }
                    }

                    if (cs3.Length > 0 && ((Label)cs3[0]).Text != "")
                    {
                        if (paneltotal3_outward_regene.Text == "0")
                        {
                            paneltotal3_outward_regene.Text = Math.Round(double.Parse(cs3[0].Text), 1).ToString();
                        }
                    }

                    if (cs4.Length > 0 && ((Label)cs4[0]).Text != "")
                    {
                        if (paneltotal3_homeward.Text == "0")
                        {
                            paneltotal3_homeward.Text = Math.Round(double.Parse(cs4[0].Text), 1).ToString();
                        }
                    }

                    if (cs5.Length > 0 && ((Label)cs5[0]).Text != "")
                    {
                        if (paneltotal3_homeward_resistance.Text == "0")
                        {
                            paneltotal3_homeward_resistance.Text = Math.Round(double.Parse(cs5[0].Text), 1).ToString();
                        }
                    }

                    if (cs6.Length > 0 && ((Label)cs6[0]).Text != "")
                    {
                        if (paneltotal3_homeward_regene.Text == "0")
                        {
                            paneltotal3_homeward_regene.Text = Math.Round(double.Parse(cs6[0].Text), 1).ToString();
                        }
                    }

                    if (cs7.Length > 0 && ((Label)cs7[0]).Text != "")
                    {
                        if (paneltotal3_total.Text == "0")
                        {
                            paneltotal3_total.Text = Math.Round(double.Parse(cs7[0].Text), 1).ToString();
                        }
                    }

                    if (cs8.Length > 0 && ((Label)cs8[0]).Text != "")
                    {
                        if (paneltotal3_total_resistance.Text == "0")
                        {
                            paneltotal3_total_resistance.Text = Math.Round(double.Parse(cs8[0].Text), 1).ToString();
                        }
                    }

                    if (cs9.Length > 0 && ((Label)cs9[0]).Text != "")
                    {
                        if (paneltotal3_total_regene.Text == "0")
                        {
                            paneltotal3_total_regene.Text = Math.Round(double.Parse(cs9[0].Text), 1).ToString();
                        }
                    }

                    if (cs10.Length > 0 && ((Label)cs10[0]).Text != "")
                    {
                        if (paneltotal3_rest.Text == "0")
                        {
                            paneltotal3_rest.Text = Math.Round(double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")), 1).ToString();
                        }
                    }
                    #endregion

                    #region 現在値と比較して更新
                    if (cs.Length > 0 && ((Label)cs[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) <= double.Parse(paneltotal3_rest.Text))
                        {
                            paneltotal3_outward.Text = Math.Round(double.Parse(cs[0].Text), 1).ToString();
                        }
                    }

                    if (cs2.Length > 0 && ((Label)cs2[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) <= double.Parse(paneltotal3_rest.Text))
                        {
                            paneltotal3_outward_resistance.Text = Math.Round(double.Parse(cs2[0].Text), 1).ToString();
                        }
                    }

                    if (cs3.Length > 0 && ((Label)cs3[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) <= double.Parse(paneltotal3_rest.Text))
                        {
                            paneltotal3_outward_regene.Text = Math.Round(double.Parse(cs3[0].Text), 1).ToString();
                        }
                    }


                    if (cs4.Length > 0 && ((Label)cs4[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) <= double.Parse(paneltotal3_rest.Text))
                        {
                            paneltotal3_homeward.Text = Math.Round(double.Parse(cs4[0].Text), 1).ToString();
                        }
                    }

                    if (cs5.Length > 0 && ((Label)cs5[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) <= double.Parse(paneltotal3_rest.Text))
                        {
                            paneltotal3_homeward_resistance.Text = Math.Round(double.Parse(cs5[0].Text), 1).ToString();
                        }
                    }

                    if (cs6.Length > 0 && ((Label)cs6[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) <= double.Parse(paneltotal3_rest.Text))
                        {
                            paneltotal3_homeward_regene.Text = Math.Round(double.Parse(cs6[0].Text), 1).ToString();
                        }
                    }


                    if (cs7.Length > 0 && ((Label)cs7[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) <= double.Parse(paneltotal3_rest.Text))
                        {
                            paneltotal3_total.Text = Math.Round(double.Parse(cs7[0].Text), 1).ToString();
                        }
                    }

                    if (cs8.Length > 0 && ((Label)cs8[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) <= double.Parse(paneltotal3_rest.Text))
                        {
                            paneltotal3_total_resistance.Text = Math.Round(double.Parse(cs8[0].Text), 1).ToString();
                        }
                    }

                    if (cs9.Length > 0 && ((Label)cs9[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) <= double.Parse(paneltotal3_rest.Text))
                        {
                            paneltotal3_total_regene.Text = Math.Round(double.Parse(cs9[0].Text), 1).ToString();
                        }
                    }

                    if (cs10.Length > 0 && ((Label)cs10[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) <= double.Parse(paneltotal3_rest.Text))
                        {
                            paneltotal3_rest.Text = Math.Round(double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")), 1).ToString();
                        }
                    }
                    #endregion
                }

                //paneltotal3_rest.Text = Math.Round((12 - double.Parse(paneltotal3_total.Text)),1).ToString();

                //最大
                if ((((Label)cs[0]).Text != "") && (((Label)cs4[0]).Text != "") && (((Label)cs7[0]).Text != ""))
                {
                    #region 最初の一回
                    if (cs.Length > 0 && ((Label)cs[0]).Text != "")
                    {
                        if (paneltotal6_outward.Text == "0")
                        {
                            paneltotal6_outward.Text = Math.Round(double.Parse(cs[0].Text), 1).ToString();
                        }
                    }

                    if (cs2.Length > 0 && ((Label)cs2[0]).Text != "")
                    {
                        if (paneltotal6_outward_resistance.Text == "0")
                        {
                            paneltotal6_outward_resistance.Text = Math.Round(double.Parse(cs2[0].Text), 1).ToString();
                        }
                    }

                    if (cs3.Length > 0 && ((Label)cs3[0]).Text != "")
                    {
                        if (paneltotal6_outward_regene.Text == "0")
                        {
                            paneltotal6_outward_regene.Text = Math.Round(double.Parse(cs3[0].Text), 1).ToString();
                        }
                    }

                    if (cs4.Length > 0 && ((Label)cs4[0]).Text != "")
                    {
                        if (paneltotal6_homeward.Text == "0")
                        {
                            paneltotal6_homeward.Text = Math.Round(double.Parse(cs4[0].Text), 1).ToString();
                        }
                    }

                    if (cs5.Length > 0 && ((Label)cs5[0]).Text != "")
                    {
                        if (paneltotal6_homeward_resistance.Text == "0")
                        {
                            paneltotal6_homeward_resistance.Text = Math.Round(double.Parse(cs5[0].Text), 1).ToString();
                        }
                    }

                    if (cs6.Length > 0 && ((Label)cs6[0]).Text != "")
                    {
                        if (paneltotal6_homeward_regene.Text == "0")
                        {
                            paneltotal6_homeward_regene.Text = Math.Round(double.Parse(cs6[0].Text), 1).ToString();
                        }
                    }

                    if (cs7.Length > 0 && ((Label)cs7[0]).Text != "")
                    {
                        if (paneltotal6_total.Text == "0")
                        {
                            paneltotal6_total.Text = Math.Round(double.Parse(cs7[0].Text), 1).ToString();
                        }
                    }

                    if (cs8.Length > 0 && ((Label)cs8[0]).Text != "")
                    {
                        if (paneltotal6_total_resistance.Text == "0")
                        {
                            paneltotal6_total_resistance.Text = Math.Round(double.Parse(cs8[0].Text), 1).ToString();
                        }
                    }

                    if (cs9.Length > 0 && ((Label)cs9[0]).Text != "")
                    {
                        if (paneltotal6_total_regene.Text == "0")
                        {
                            paneltotal6_total_regene.Text = Math.Round(double.Parse(cs9[0].Text), 1).ToString();
                        }
                    }

                    if (cs10.Length > 0 && ((Label)cs10[0]).Text != "")
                    {
                        if (paneltotal6_rest.Text == "0")
                        {
                            paneltotal6_rest.Text = Math.Round(double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")), 1).ToString();
                        }
                    }
                    #endregion

                    #region 現在値と比較して更新
                    if (cs.Length > 0 && ((Label)cs[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) >= double.Parse(paneltotal6_rest.Text))
                        {
                            paneltotal6_outward.Text = Math.Round(double.Parse(cs[0].Text), 1).ToString();
                        }
                    }

                    if (cs2.Length > 0 && ((Label)cs2[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) >= double.Parse(paneltotal6_rest.Text))
                        {
                            paneltotal6_outward_resistance.Text = Math.Round(double.Parse(cs2[0].Text), 1).ToString();
                        }
                    }

                    if (cs3.Length > 0 && ((Label)cs3[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) >= double.Parse(paneltotal6_rest.Text))
                        {
                            paneltotal6_outward_regene.Text = Math.Round(double.Parse(cs3[0].Text), 1).ToString();
                        }
                    }


                    if (cs4.Length > 0 && ((Label)cs4[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) >= double.Parse(paneltotal6_rest.Text))
                        {
                            paneltotal6_homeward.Text = Math.Round(double.Parse(cs4[0].Text), 1).ToString();
                        }
                    }

                    if (cs5.Length > 0 && ((Label)cs5[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) >= double.Parse(paneltotal6_rest.Text))
                        {
                            paneltotal6_homeward_resistance.Text = Math.Round(double.Parse(cs5[0].Text), 1).ToString();
                        }
                    }

                    if (cs6.Length > 0 && ((Label)cs6[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) >= double.Parse(paneltotal6_rest.Text))
                        {
                            paneltotal6_homeward_regene.Text = Math.Round(double.Parse(cs6[0].Text), 1).ToString();
                        }
                    }


                    if (cs7.Length > 0 && ((Label)cs7[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) >= double.Parse(paneltotal6_rest.Text))
                        {
                            paneltotal6_total.Text = Math.Round(double.Parse(cs7[0].Text), 1).ToString();
                        }
                    }

                    if (cs8.Length > 0 && ((Label)cs8[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) >= double.Parse(paneltotal6_rest.Text))
                        {
                            paneltotal6_total_resistance.Text = Math.Round(double.Parse(cs8[0].Text), 1).ToString();
                        }
                    }

                    if (cs9.Length > 0 && ((Label)cs9[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) >= double.Parse(paneltotal6_rest.Text))
                        {
                            paneltotal6_total_regene.Text = Math.Round(double.Parse(cs9[0].Text), 1).ToString();
                        }
                    }

                    if (cs10.Length > 0 && ((Label)cs10[0]).Text != "")
                    {
                        if (double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")) >= double.Parse(paneltotal6_rest.Text))
                        {
                            paneltotal6_rest.Text = Math.Round(double.Parse(System.Text.RegularExpressions.Regex.Replace(((Label)cs10[0]).Text, @"\(.*\)", "")), 1).ToString();
                        }
                    }
                    #endregion
                }

                //paneltotal6_rest.Text = Math.Round((12 - double.Parse(paneltotal6_total.Text)),1).ToString();

            }

            #endregion

            #region 平均-標準偏差,平均+標準偏差を反映
            //平均-標準偏差
            paneltotal4_outward.Text = Math.Round(double.Parse(paneltotal1_outward.Text) - double.Parse(paneltotal2_outward.Text), 1).ToString();
            paneltotal4_outward_resistance.Text = Math.Round(double.Parse(paneltotal1_outward_resistance.Text) - double.Parse(paneltotal2_outward_resistance.Text), 1).ToString();
            paneltotal4_outward_regene.Text = Math.Round(double.Parse(paneltotal1_outward_regene.Text) - double.Parse(paneltotal2_outward_regene.Text), 1).ToString();
            paneltotal4_homeward.Text = Math.Round(double.Parse(paneltotal1_homeward.Text) - double.Parse(paneltotal2_homeward.Text), 1).ToString();
            paneltotal4_homeward_resistance.Text = Math.Round(double.Parse(paneltotal1_homeward_resistance.Text) - double.Parse(paneltotal2_homeward_resistance.Text), 1).ToString();
            paneltotal4_homeward_regene.Text = Math.Round(double.Parse(paneltotal1_homeward_regene.Text) - double.Parse(paneltotal2_homeward_regene.Text), 1).ToString();
            paneltotal4_total.Text = Math.Round(double.Parse(paneltotal1_total.Text) - double.Parse(paneltotal2_total.Text), 1).ToString();
            paneltotal4_total_resistance.Text = Math.Round(double.Parse(paneltotal1_total_resistance.Text) - double.Parse(paneltotal2_total_resistance.Text), 1).ToString();
            paneltotal4_total_regene.Text = Math.Round(double.Parse(paneltotal1_total_regene.Text) - double.Parse(paneltotal2_total_regene.Text), 1).ToString();
            paneltotal4_rest.Text = Math.Round(double.Parse(paneltotal1_rest.Text) - double.Parse(paneltotal2_rest.Text), 1).ToString();

            //平均+標準偏差
            paneltotal5_outward.Text = Math.Round(double.Parse(paneltotal1_outward.Text) + double.Parse(paneltotal2_outward.Text), 1).ToString();
            paneltotal5_outward_resistance.Text = Math.Round(double.Parse(paneltotal1_outward_resistance.Text) + double.Parse(paneltotal2_outward_resistance.Text), 1).ToString();
            paneltotal5_outward_regene.Text = Math.Round(double.Parse(paneltotal1_outward_regene.Text) + double.Parse(paneltotal2_outward_regene.Text), 1).ToString();
            paneltotal5_homeward.Text = Math.Round(double.Parse(paneltotal1_homeward.Text) + double.Parse(paneltotal2_homeward.Text), 1).ToString();
            paneltotal5_homeward_resistance.Text = Math.Round(double.Parse(paneltotal1_homeward_resistance.Text) + double.Parse(paneltotal2_homeward_resistance.Text), 1).ToString();
            paneltotal5_homeward_regene.Text = Math.Round(double.Parse(paneltotal1_homeward_regene.Text) + double.Parse(paneltotal2_homeward_regene.Text), 1).ToString();
            paneltotal5_total.Text = Math.Round(double.Parse(paneltotal1_total.Text) + double.Parse(paneltotal2_total.Text), 1).ToString();
            paneltotal5_total_resistance.Text = Math.Round(double.Parse(paneltotal1_total_resistance.Text) + double.Parse(paneltotal2_total_resistance.Text), 1).ToString();
            paneltotal5_total_regene.Text = Math.Round(double.Parse(paneltotal1_total_regene.Text) + double.Parse(paneltotal2_total_regene.Text), 1).ToString();
            paneltotal5_rest.Text = Math.Round(double.Parse(paneltotal1_rest.Text) + double.Parse(paneltotal2_rest.Text), 1).ToString();
            #endregion

        }

        private void displayPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalendarProperty calendar_proper = new CalendarProperty(this, Year, Month);
            MainForm.ShowWindow(calendar_proper);
        }
    }
}
