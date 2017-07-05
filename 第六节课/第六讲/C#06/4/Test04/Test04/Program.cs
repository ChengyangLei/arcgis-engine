using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test04
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[] { 3, 9, 27, 6, 18, 12, 21, 15 };		//定义一个一维数组，并赋值
            foreach (int m in arr)	//循环遍历定义的一维数组，并输出其中的元素
                Console.Write(m + " ");
            Console.WriteLine();
            //定义两个int类型的变量，分别用来表示数组下标和存储新的数组元素
            int j, temp;
            for (int i = 0; i < arr.Length - 1; i++)				//根据数组下标的值遍历数组元素
            {
                j = i + 1;
            id:									//定义一个标识，以便从这里开始执行语句
                if (arr[i] > arr[j])							//判断前后两个数的大小
                {
                    temp = arr[i];	//将比较后大的元素赋值给定义的int变量
                    arr[i] = arr[j];//将后一个元素的值赋值给前一个元素
                    arr[j] = temp;	//将int变量中存储的元素值赋值给后一个元素
                    goto id;								//返回标识，继续判断后面的元素
                }
                else
                    if (j < arr.Length - 1)					//判断是否执行到最后一个元素
                    {
                        j++;							//如果没有，则再往后判断
                        goto id;							//返回标识，继续判断后面的元素
                    }
            }
            foreach (int n in arr)							//循环遍历排序后的数组元素并输出
                Console.Write(n + " ");
            Console.WriteLine();

        }
    }
}
