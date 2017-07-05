using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test01
{
    class Program
    {
        static void Main(string[] args)
        {

            bool tr;
            int s;

            float fl = 3;
            double d = 3.1415926;

            fl = (float)d;
            Console.WriteLine(fl);
            Console.WriteLine(d );
            int ls = 927;						//声明一个int类型的变量ls
            byte shj = 255;						//声明一个byte类型的变量shj
            Console.WriteLine("ls={0}", ls);				//输出int类型变量ls
            Console.WriteLine("shj={0}", shj);				//输出byte类型变量shj
            Console.ReadLine();

        }
    }
}
