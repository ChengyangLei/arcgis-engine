using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test06
{
    class Program
    {
        static void Main(string[] args)
        {
            int I1 = 55;										//声明整型变量I1，并赋值为55
            int I2 = 10;										//声明整型变量I1，并赋值为10
            int I3;											//声明整型变量I3
            int M1 = 95;
            int M2 = 12;

            I3 = I1 % I2;									//使I3等于I1与I2求余运算之后的值
            Console.WriteLine(M2.ToString());
            Console.WriteLine(I3.ToString());
            Console.Read();

        }
    }
}
