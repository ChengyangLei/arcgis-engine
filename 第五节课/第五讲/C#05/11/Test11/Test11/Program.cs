using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test11
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 4; i++)						//调用for语句
            {
                Console.Write("\n第{0}次循环：", i);		//输出提示是第几次循环
                for (int j = 0; j < 200; j++)					//调用for语句
                {
                    if (j == 12)						//如果j的值等于12
                        break;						//终止循环
                    Console.Write(j + " ");					//输出j
                }
            }
            Console.ReadLine();

        }
    }
}
