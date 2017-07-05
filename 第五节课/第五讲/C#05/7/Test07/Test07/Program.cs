using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test07
{
    class Program
    {
        static void Main(string[] args)
        {
            bool term = false;							//声明一个bool型变量term并初始化为false
            int[] myArray = new int[5] { 0, 1, 2, 3, 4 };			//声明一个int数组并初始化
            do									//调用do…while语句
            {
                for (int i = 0; i < myArray.Length; i++)		//调用for语句输出数组中的所有数据
                {
                    Console.WriteLine(myArray[i]);			//输出数组中数据
                }
            } while (term);							//设置do…while语句的条件
            Console.ReadLine();

        }
    }
}
