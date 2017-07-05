using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请您输入一个月份：");		//输出提示信息
            int MyMouth = int.Parse(Console.ReadLine());		//声明一个int类型变量用于获取用户输入的数据
            string MySeason="'";							//声明一个字符串变量
            switch (MyMouth)							//调用switch语句
            {
                case 12:
                    break;
                case 1:
                    break;
                case 2:
                    MySeason = "您输入的月份属于冬季！";	//如果输入的数据是1、2或者12则执行此分支
                    break;							//跳出switch语句
                case 3:
                case 4:
                case 5:
                    MySeason = "您输入的月份属于春季！";	//如果输入的数据是3、4或5则执行此分支
                    break;							//跳出switch语句
                case 6:
                case 7:
                case 8:
                    MySeason = "您输入的月份属于夏季！";	//如果输入的数据是6、7或8则执行此分支
                    break;							//跳出switch语句
                case 9:
                case 10:
                case 11:
                    MySeason = "您输入的月份属于秋季！";	//如果输入的数据是9、10或11则执行此分支
                    break;							//跳出switch语句
                //如果输入的数据不满足以上4个分支的内容则执行default语句
                default:
                    MySeason = "月份输入错误！";
                    break;							//跳出switch语句
            }
            Console.WriteLine(MySeason);				//输出字符串MySeason 
            Console.ReadLine();

        }
    }
}
