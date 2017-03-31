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
    public partial class CalendarProperty : Form
    {
        #region 変数定義
        Control[] cs, cs2, cs3, cs4, cs5, cs6, cs7, cs8, cs9, cs10;

        string panelname = "panel";
        DateTime PastDate = DateTime.Now;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="calendar">表示するデータを含むCalendarクラスのインスタンス</param>
        /// <param name="year">表示する年</param>
        /// <param name="month">表示する月</param>
        public CalendarProperty(Calendar calendar, string year, string month)
        {
            InitializeComponent();

            #region ラベル初期化
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            for (int i = 1; i < 7; i++)
            {
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
            //if (int.Parse(month) < 9)
            //{
            //    YearMonth.Text = year + "/0" + month + "";
            //}
            //else
            //{
            //    YearMonth.Text = year + "/" + month + "";
            //}

            YearMonth.Text = year + "/" + month + "";

            #endregion

            #region 月平均値を反映
            int day_count = 0;

            for (int i = 1; i < 43; i++)
            {
                panelname = "panel" + i;

                cs = calendar.Controls.Find(panelname + "_outward", true);
                cs2 = calendar.Controls.Find(panelname + "_outward_resistance", true);
                cs3 = calendar.Controls.Find(panelname + "_outward_regene", true);
                cs4 = calendar.Controls.Find(panelname + "_homeward", true);
                cs5 = calendar.Controls.Find(panelname + "_homeward_resistance", true);
                cs6 = calendar.Controls.Find(panelname + "_homeward_regene", true);
                cs7 = calendar.Controls.Find(panelname + "_total", true);
                cs8 = calendar.Controls.Find(panelname + "_total_resistance", true);
                cs9 = calendar.Controls.Find(panelname + "_total_regene", true);
                cs10 = calendar.Controls.Find(panelname + "_rest", true);

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

                cs = calendar.Controls.Find(panelname + "_outward", true);
                cs2 = calendar.Controls.Find(panelname + "_outward_resistance", true);
                cs3 = calendar.Controls.Find(panelname + "_outward_regene", true);
                cs4 = calendar.Controls.Find(panelname + "_homeward", true);
                cs5 = calendar.Controls.Find(panelname + "_homeward_resistance", true);
                cs6 = calendar.Controls.Find(panelname + "_homeward_regene", true);
                cs7 = calendar.Controls.Find(panelname + "_total", true);
                cs8 = calendar.Controls.Find(panelname + "_total_resistance", true);
                cs9 = calendar.Controls.Find(panelname + "_total_regene", true);
                cs10 = calendar.Controls.Find(panelname + "_rest", true);

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

                cs = calendar.Controls.Find(panelname + "_outward", true);
                cs2 = calendar.Controls.Find(panelname + "_outward_resistance", true);
                cs3 = calendar.Controls.Find(panelname + "_outward_regene", true);
                cs4 = calendar.Controls.Find(panelname + "_homeward", true);
                cs5 = calendar.Controls.Find(panelname + "_homeward_resistance", true);
                cs6 = calendar.Controls.Find(panelname + "_homeward_regene", true);
                cs7 = calendar.Controls.Find(panelname + "_total", true);
                cs8 = calendar.Controls.Find(panelname + "_total_resistance", true);
                cs9 = calendar.Controls.Find(panelname + "_total_regene", true);
                cs10 = calendar.Controls.Find(panelname + "_rest", true);

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

            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }
    }
}
