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
            int ls1 = 10;										//声明整型变量ls1，并赋值为10
            int ls2 = 20;										//声明整型变量ls2，并赋值为20
            int sum;										    //声明整型变量sum
            sum = ls1 * ls2;									//使sum的值为ls1和ls2的乘积
            Console.WriteLine(sum.ToString());
            Console.Read();

        }
    }
}
