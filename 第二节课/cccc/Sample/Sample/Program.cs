using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using test;
namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
           A op=new A();
           op.aa();

        }
    }
}
namespace test {

    class A
    {
      public void aa()
      {
        Console.WriteLine("dasdasd");
            Console.ReadLine();
        
        }
    }


}