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
            //声明一个int类型的数组，并初始化
            int[] myNum = new int[6] { 927, 112, 111, 524, 521, 2008 };
            int s = 0;								//声明一个int类型的变量s并初始化为0
            while (s < 6)								//调用while语句当s小于6时执行
            {
                //输出数组中的值
                Console.WriteLine("myNum[{0}]的值为{1}", s, myNum[s]);
                s++;								//s自增1
            }
            Console.ReadLine();

        }
    }
}
