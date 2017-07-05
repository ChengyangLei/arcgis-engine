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
            double x = 19810927.0112;		//建立double类型变量x
            int y = (int)x;					//显示转换成整型变量y
            int i;
            int b = 312;
            double a=b;
            string cv = "wo shi world";
            string[] intArray;
            intArray = cv.Split(' ');
            for (i = 0; i <= 10;i++ )
                Console.WriteLine(intArray[i]);
            

            Console.WriteLine(y);						//输出整型变量y
            Console.ReadLine();

        }
    }
}
