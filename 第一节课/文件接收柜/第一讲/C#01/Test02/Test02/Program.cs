using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N1;

namespace Test02
{
    class Program
    {
        static void Main(string[] args)
        {
            A oa = new A();						//实例化N1中的类A
            oa.Myls();							//调用类A中的Myls方法
        }
    }
}






namespace N1									//建立命名空间N1
{
    class A                                         //实例化命名空间N1中的类A
    {
        public void Myls()
        {
            Console.WriteLine("用一生下载你");		//输出字符串
            Console.ReadLine();
        }
    }
}