using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test13
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入要查找的文字：");				//输出提示信息
            string inputstr = Console.ReadLine();					//获取输入值
            string[] mystr = new string[5];							//创建一个字符串数组
            mystr[0] = "用一生下载你";							//向数组中添加第一个元素
            mystr[1] = "芸烨湘枫";								//向数组中添加第二个元素
            mystr[2] = "一生所爱";								//向数组中添加第三个元素
            mystr[3] = "情茧";									//向数组中添加第四个元素
            mystr[4] = "风华绝代";								//向数组中添加第五个元素
            for (int i = 0; i < mystr.Length; i++)						//调用for循环语句
            {
                //通过if语句判断是否存在输入的字符串
                if (mystr[i].Equals(inputstr))
                {
                    goto Found; 								//调用goto语句跳转到Found
                }
            }
            Console.WriteLine("您查找的{0}不存在！", inputstr);		//输出信息
            goto Finish;										//调用goto语句跳转到Finish
        Found:
            Console.WriteLine("您查找的{0}存在！", inputstr);			//输出信息，提示存在输入的字符串
        Finish:
            Console.WriteLine("查找完毕！");						//输出信息，提示查找完毕
            Console.ReadLine();

        }
    }
}
