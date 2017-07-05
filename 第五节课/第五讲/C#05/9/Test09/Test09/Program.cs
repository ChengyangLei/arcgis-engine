using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Test09
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList alt = new ArrayList();				//实例化ArrayList类
            alt.Add("用一生下载你");					//使用Add方法向对象中添加数据
            alt.Add("芸烨湘枫");						//使用Add方法向对象中添加数据
            alt.Add("一生所爱");						//使用Add方法向对象中添加数据
            alt.Add("情茧");							//使用Add方法向对象中添加数据
            alt.Add("痞子CAI");						//使用Add方法向对象中添加数据
            Console.WriteLine("您收藏的网名有：");		//输出提示
            foreach (string InternetName in alt)				//使用foreach语句输出数据
            {
                Console.WriteLine(InternetName);			//输出ArrayList对象中的所有数据
            }
            Console.ReadLine();

        }
    }
}
