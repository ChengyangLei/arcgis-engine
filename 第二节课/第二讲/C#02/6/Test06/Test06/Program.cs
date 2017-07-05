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
            int i = 112;								//声明一个int类型的变量i，并初始化为112
            object obj = i;								//执行装箱操作
            Console.WriteLine("装箱操作：值为{0}，装箱之后对象为{1}", i, obj);
            int j = (int)obj;							//执行拆箱操作
            Console.WriteLine("拆箱操作：装箱对象为{0}，值为{1}", obj, j);
            Console.ReadLine();

        }
    }
}
