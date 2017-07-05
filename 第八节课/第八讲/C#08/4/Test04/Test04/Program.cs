using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test04
{
    class MyClass
    {
        private int x = 0;								//定义int型变量，作为加数
        private int y = 0;								//定义int型变量，作为被加数
        private int z = 0;								//定义int型变量，记录加法运算的和
        /// <summary>
        /// 加数
        /// </summary>
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        /// <summary>
        /// 被加数
        /// </summary>
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        /// <summary>
        /// 和
        /// </summary>
        public int Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public MyClass()
        {
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int x = 3;
            int y = 5;
            MyClass myclass = new MyClass();		//实例化MyClass对象
            myclass.X = x;						//通过对象设置类中的属性X
            myclass.Y = y;						//通过对象设置类中的属性Y
            myclass.Z = myclass.X + myclass.Y;
            int z = myclass.Z;						//定义一个int型变量，并通过对象访问类中的属性Z
            Console.WriteLine(z);
        }
    }

}
