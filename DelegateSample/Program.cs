using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateSample
{

    class SuperHello
    {
        public void PrintHello()
        {
            Console.WriteLine("hello1");
        }
    }

    class SuperHello2
    {
        public void PrintHello()
        {
            Console.WriteLine("hello2");
        }
    }


    class SuperHello3
    {
        public void SuperPrintHello()
        {
            Console.WriteLine("hello3");
        }
    }


    public delegate void PrintHelloDelegate();

    class SimpleHello
    {
        public PrintHelloDelegate invoker;
        public void PrintHello()
        {
            invoker.Invoke();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            SuperHello sh = new SuperHello();
            SuperHello2 sh2 = new SuperHello2();
            SuperHello3 sh3 = new SuperHello3();
            SimpleHello shh = new SimpleHello();

            shh.invoker = sh3.SuperPrintHello;
            shh.PrintHello();
        }
    }
}
