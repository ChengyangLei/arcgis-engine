using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("请输入动态数组的行数:");
            int row = Convert.ToInt32(Console.ReadLine());			//定义动态数组的行数
            Console.Write("请输入动态数组的列数:");
            int col = Convert.ToInt32(Console.ReadLine());			//定义动态数组的列数
            int[,] arr = new int[row, col];							//根据定义的行数和列数定义动态数组
            Console.WriteLine("结果:");
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Console.Write(i + j.ToString() + " ");				//输出动态数组中的元素
                }
                Console.WriteLine();
               
            }
               Console.ReadLine();
        }
    }
}
