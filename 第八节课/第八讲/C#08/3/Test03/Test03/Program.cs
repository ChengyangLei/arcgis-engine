using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test03
{
    class Program
    {
        ~Program()								//析构函数
        {
            Console.WriteLine("析构函数自动调用");		//输出一个字符串
        }
        static void Main(string[] args)
        {
            Program program = new Program();				//实例化Program对象
        }

    }
}
