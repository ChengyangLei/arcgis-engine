using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test12
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 4; i++)						//调用for循环
            {
                Console.Write("\n第{0}次循环：", i);		//输出提示第几次循环
                for (int j = 0; j < 20; j++)					//调用for循环
                {
                    if (j % 2 == 0)						//调用if语句判断j是否是偶数
                        continue;						//如果是偶数则使用continue语句继续下一循环
                    Console.Write(j + " ");					//输出j
                }
                Console.WriteLine();
            }
            Console.ReadLine();

        }
    }
}
