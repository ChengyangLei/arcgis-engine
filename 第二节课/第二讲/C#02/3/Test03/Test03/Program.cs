using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test03
{
    class Program
    {
        enum MyDate								//使用enum创建枚举
        {
            Sun = 0,								//设置枚举值名称Sun，枚举值为0
            Mon = 1,								//设置枚举值名称Mon，枚举值为1
            Tue = 2,								//设置枚举值名称Tue，枚举值为2
            Wed = 3,								//设置枚举值名称Wed，枚举值为3
            Thi = 4,									//设置枚举值名称Thi，枚举值为4
            Fri = 5,									//设置枚举值名称Fri，枚举值为5
            Sat = 6									//设置枚举值名称Sat，枚举值为6
        }

        static void Main(string[] args)
        {
            int k = (int)DateTime.Now.DayOfWeek;			//获取代表星期几的返回值
            switch (k)
            {
                //如果k等于枚举变量MyDate中的Sun的枚举值，则输出今天是星期日
                case (int)MyDate.Sun: Console.WriteLine("今天是星期日"); break;
                //如果k等于枚举变量MyDate中的Mon的枚举值，则输出今天是星期一
                case (int)MyDate.Mon: Console.WriteLine("今天是星期一"); break;
                //如果k等于枚举变量MyDate中的Tue的枚举值，则输出今天是星期二
                case (int)MyDate.Tue: Console.WriteLine("今天是星期二"); break;
                //如果k等于枚举变量MyDate中的Wed的枚举值，则输出今天是星期三
                case (int)MyDate.Wed: Console.WriteLine("今天是星期三"); break;
                //如果k等于枚举变量MyDate中的Thi的枚举值，则输出今天是星期四
                case (int)MyDate.Thi: Console.WriteLine("今天是星期四"); break;
                //如果k等于枚举变量MyDate中的Fri的枚举值，则输出今天是星期五
                case (int)MyDate.Fri: Console.WriteLine("今天是星期五"); break;
                //如果k等于枚举变量MyDate中的Sat的枚举值，则输出今天是星期六
                case (int)MyDate.Sat: Console.WriteLine("今天是星期六"); break;
            }
            Console.ReadLine();

        }
    }
}
