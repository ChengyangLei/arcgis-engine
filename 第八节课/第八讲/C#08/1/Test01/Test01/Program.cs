using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test01
{
    class Program
    {
        public struct Rect								//定义一个矩形结构
        {
            public double width;							//矩形的宽
            public double height;							//矩形的高
            /// <summary>
            /// 构造函数，初始化矩形的宽和高
            /// </summary>
            /// <param name="x">矩形的宽</param>
            /// <param name="y">矩形的高</param>
            public Rect(double x, double y)
            {
                width = x;
                height = y;
            }
            /// <summary>
            /// 计算矩形面积
            /// </summary>
            /// <returns>矩形面积</returns>
            public double Area()
            {
                return width * height;
            }
        }
        static void Main(string[] args)
        {
            Rect rect1;									//实例化矩形结构
            rect1.width = 5;								//为矩形宽赋值
            rect1.height = 3;								//为矩形高赋值
            Console.WriteLine("矩形面积为：" + rect1.Area());
            Rect rect2 = new Rect(6, 4);						//使用构造函数实例化矩形结构
            Console.WriteLine("矩形面积为：" + rect2.Area());
            Console.ReadLine();
        }

    }
}
