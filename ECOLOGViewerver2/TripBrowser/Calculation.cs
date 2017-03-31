using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOLOGViewerver2
{
    /// <summary>
    /// Google Map上に奇跡を描く際の計算処理を取り扱うクラス
    /// </summary>
    public class Calculation
    {

        #region [運転情報]、[運転情報＋平均]で使用
        // heading, XY加速度→x変位
        static internal double calc_acc_x(string h, string i, string j, string mode)
        {

            double heading, x, y, result;
            heading = ((double.Parse(h) - 90) * Math.PI) / 180;

            if (i == "")
            {
                x = 0;
            }
            else
            {
                x = double.Parse(i);
            }

            if (j == "")
            {
                y = 0;
            }
            else
            {
                y = double.Parse(j);
            }

            // モードに応じて表示内容を変更
            if (mode == "LongitudinalAcc")
            {
                // 加減速
                result = x / 5000;
            }
            else
            {
                // 左右
                result = -y / 5000;
            }

            return result * Math.Sin(heading);
        }
        // heading, XY加速度→y変位
        static internal double calc_acc_y(string h, string i, string j, string mode)
        {

            double heading, x, y, result;
            heading = ((double.Parse(h) - 90) * Math.PI) / 180;

            if (i == "")
            {
                x = 0;
            }
            else
            {
                x = double.Parse(i);
            }

            if (j == "")
            {
                y = 0;
            }
            else
            {
                y = double.Parse(j);
            }

            // モードに応じて表示内容を変更
            if (mode == "LongitudinalAcc")
            {
                // 加減速
                result = x / 5000;
            }
            else
            {
                // 左右
                result = -y / 5000;
            }

            return result * Math.Cos(heading);
        }
        // heading, speed→x変位
        static internal double calc_speed_x(string h, string s)
        {

            double heading, speed, result;
            heading = ((double.Parse(h) - 90) * Math.PI) / 180;

            if (s == "")
            {
                speed = 0;
            }
            else
            {
                speed = double.Parse(s);
            }

            result = speed / 100000;

            return result * Math.Cos(heading);
        }
        // heading, speed→y変位
        static internal double calc_speed_y(string h, string s)
        {

            double heading, speed, result;
            heading = ((double.Parse(h) - 90) * Math.PI) / 180;

            if (s == "")
            {
                speed = 0;
            }
            else
            {
                speed = double.Parse(s);
            }

            result = speed / 100000;

            return result * Math.Sin(heading);
        }
        // heading, energy→x変位
        static internal double calc_energy_x(string h, string s)
        {

            double heading, speed, result;
            heading = double.Parse(h) + Math.PI / 2;

            if (s == "")
            {
                speed = 0;
            }
            else
            {
                speed = double.Parse(s);
            }

            result = speed / 2;

            return result * Math.Cos(heading);
        }
        // heading, energy→y変位
        static internal double calc_energy_y(string h, string s)
        {

            double heading, speed, result;
            heading = double.Parse(h) + Math.PI / 2;

            if (s == "")
            {
                speed = 0;
            }
            else
            {
                speed = double.Parse(s);
            }

            result = speed / 2;

            return result * Math.Sin(heading);
        }
        // heading, energyloss→x変位
        static internal double calc_energyloss_x(string h, string s)
        {

            double heading, speed, result;
            heading = double.Parse(h) + Math.PI / 2;

            if (s == "")
            {
                speed = 0;
            }
            else
            {
                speed = double.Parse(s);
            }

            result = speed * 2;

            return result * Math.Cos(heading);
        }
        // heading, energyloss→y変位
        static internal double calc_energyloss_y(string h, string s)
        {

            double heading, speed, result;
            heading = double.Parse(h) + Math.PI / 2;

            if (s == "")
            {
                speed = 0;
            }
            else
            {
                speed = double.Parse(s);
            }

            result = speed * 2;

            return result * Math.Sin(heading);
        }
        // heading, fuel→x変位
        static internal double calc_fuel_x(string h, string f)
        {

            double heading, fuel, result;
            heading = double.Parse(h) + Math.PI / 2;

            if (f == "")
            {
                fuel = 0;
            }
            else
            {
                fuel = double.Parse(f);
            }

            result = fuel / 10;

            return result * Math.Cos(heading);
        }
        // heading, energy→y変位
        static internal double calc_fuel_y(string h, string f)
        {

            double heading, fuel, result;
            heading = double.Parse(h) + Math.PI / 2;

            if (f == "")
            {
                fuel = 0;
            }
            else
            {
                fuel = double.Parse(f);
            }

            result = fuel / 10;

            return result * Math.Sin(heading);
        }
        #endregion

        #region [運転情報＋平均]で使用
        // heading, energy→x分散
        static internal double sigma_energy_x(string h, string s)
        {

            double heading, speed, result;
            heading = double.Parse(h) + Math.PI / 2;

            if (s == "")
            {
                speed = 0;
            }
            else
            {
                speed = double.Parse(s);
            }

            result = speed / 10;

            return result * Math.Cos(heading);
        }
        // heading, energy→y分散
        static internal double sigma_energy_y(string h, string s)
        {

            double heading, speed, result;
            heading = double.Parse(h) + Math.PI / 2;

            if (s == "")
            {
                speed = 0;
            }
            else
            {
                speed = double.Parse(s);
            }

            result = speed / 10;

            return result * Math.Sin(heading);
        }
        // heading, energyloss→x分散
        static internal double sigma_energyloss_x(string h, string e)
        {

            double heading, energy, result;
            heading = double.Parse(h) + Math.PI / 2;

            if (e == "")
            {
                energy = 0;
            }
            else
            {
                energy = double.Parse(e);
            }

            result = energy / 5;// 10;

            return result * Math.Cos(heading);
        }
        // heading, energyloss→y分散
        static internal double sigma_energyloss_y(string h, string e)
        {

            double heading, energy, result;
            heading = double.Parse(h) + Math.PI / 2;

            if (e == "")
            {
                energy = 0;
            }
            else
            {
                energy = double.Parse(e);
            }

            result = energy / 5;//10;

            return result * Math.Sin(heading);
        }
        // heading, speed→x分散
        static internal double sigma_speed_x(string h, string s)
        {

            double heading, speed, result;
            heading = ((double.Parse(h) - 90) * Math.PI) / 180;

            if (s == "")
            {
                speed = 0;
            }
            else
            {
                speed = double.Parse(s);
            }

            result = speed / 500000;

            return result * Math.Sin(heading);
        }
        // heading, speed→y分散
        static internal double sigma_speed_y(string h, string s)
        {

            double heading, speed, result;
            heading = ((double.Parse(h) - 90) * Math.PI) / 180;

            if (s == "")
            {
                speed = 0;
            }
            else
            {
                speed = double.Parse(s);
            }

            result = speed / 500000;

            return result * Math.Cos(heading);
        }
        // heading, XY加速度→x分散
        static internal double sigma_acc_x(string h, string i, string j, string mode)
        {

            double heading, x, y, result;
            heading = ((double.Parse(h) - 90) * Math.PI) / 180;

            if (i == "")
            {
                x = 0;
            }
            else
            {
                x = double.Parse(i);
            }

            if (j == "")
            {
                y = 0;
            }
            else
            {
                y = double.Parse(j);
            }

            // モードに応じて表示内容を変更
            if (mode == "LongitudinalAcc")
            {
                // 加減速
                result = x / 50000;
            }
            else
            {
                // 左右
                result = y / 50000;
            }

            return result * Math.Sin(heading);
        }
        // heading, XY加速度→y分散
        static internal double sigma_acc_y(string h, string i, string j, string mode)
        {

            double heading, x, y, result;
            heading = ((double.Parse(h) - 90) * Math.PI) / 180;

            if (i == "")
            {
                x = 0;
            }
            else
            {
                x = double.Parse(i);
            }

            if (j == "")
            {
                y = 0;
            }
            else
            {
                y = double.Parse(j);
            }

            // モードに応じて表示内容を変更
            if (mode == "LongitudinalAcc")
            {
                // 加減速
                result = x / 50000;
            }
            else
            {
                // 左右
                result = y / 50000;
            }

            return result * Math.Cos(heading);
        }
        #endregion

    }
}
