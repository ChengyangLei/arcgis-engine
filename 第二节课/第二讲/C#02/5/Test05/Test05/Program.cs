using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test05
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 2008;								//声明一个int类型变量i，并初始化为2008
            object obj = i;	//声明一个object类型obj，其初始化值为i
            Console.WriteLine("1、i的值为{0}，装箱之后的对象为{1}", i, obj);
            i = 927;									//重新将I赋值为927
            Console.WriteLine("2、i的值为{0}，装箱之后的对象为{1}", i, obj);
            Console.ReadLine();

        }
    }
}
