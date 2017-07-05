using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test02
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] arr = new int[2, 2] { { 1, 2 }, { 3, 4 } };					//自定义一个二维数组
            Console.Write("数组的行数为：");
            Console.Write(arr.Rank);								//获得二维数组的行数
            Console.Write("\n");
            Console.Write("数组的列数为：");
            Console.Write(arr.GetUpperBound(arr.Rank - 1) + 1);			//获得二维数组的列数
            Console.Write("\n");
            for (int i = 0; i < arr.Rank; i++)
            {
                string str = "";
                for (int j = 0; j < arr.GetUpperBound(arr.Rank - 1) + 1; j++)
                {
                    str = str + Convert.ToString(arr[i, j]) + " ";				//循环输出二维数组中的每个元素
                }
                Console.Write(str);
                Console.Write("\n");
                Console.ReadLine();
            }

        }
    }
}
