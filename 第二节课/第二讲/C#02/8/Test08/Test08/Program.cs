using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test08
{
    class Program
    {
        static void Main(string[] args)
        {
            int MyInt = 927;							//声明一个整型变量
            const int MyWInt = 112;						//声明一个整型常量
            Console.WriteLine("变量MyInt={0}", MyInt);		//输出
            Console.WriteLine("常量MyWInt={0}", MyWInt);	//输出
            MyInt = 1039;							//重新将变量赋值为1039
            Console.WriteLine("变量MyInt={0}", MyInt);		//输出
            Console.ReadLine();

        }
    }
}
