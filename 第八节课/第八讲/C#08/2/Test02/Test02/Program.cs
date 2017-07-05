using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test02
{
    class Program
    {
        public int x = 3;								//定义int型变量，作为加数
        public int y = 5;								//定义int型变量，作为被加数
        public int z = 0;								//定义int型变量，记录加法运算的和
        public Program()
        {
            z = x + y;								//在构造函数中为和赋值
        }
        static void Main(string[] args)
        {
            Program program = new Program();			//使用构造函数实例化Program对象
            Console.WriteLine("结果：" + program.z);		//使用实例化的Program对象输出加法运算的和
        }

    }
}
