using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test14
{
    class Program
    {
        static string MyStr(string str)						//创建一个string类型方法
        {
            string OutStr;								//声明一个字符串变量
            OutStr = "您输入的数据是：" + str;				//为字符串变量赋值
            return OutStr;							//使用return语句返回字符串变量
        }

        static void Main(string[] args)
        {
            Console.WriteLine("请您输入内容：");			//输出提示信息
            string inputstr = Console.ReadLine();			//获取输入的数据
            Console.WriteLine(MyStr(inputstr));				//调用MyStr方法并将结果显示出来
            Console.ReadLine();

        }
    }
}
