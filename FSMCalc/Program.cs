using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMCalc
{
    class Program
    {

        static void PrintToConsole(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
        }
        static void Main(string[] args)
        {
            Brain b = new Brain();
            b.invoker = PrintToConsole;
            while (true)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();

                switch (pressedKey.Key)
                {
                    case ConsoleKey.D0:
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                    case ConsoleKey.D9:
                        b.Processor(pressedKey.KeyChar);
                        break;
                    case ConsoleKey.C:
                        b.Processor(',');
                        break;
                    case ConsoleKey.P:
                        b.Processor('+');
                        break;
                    case ConsoleKey.M:
                        b.Processor('-');
                        break;
                    case ConsoleKey.E:
                        b.Processor('=');
                        break;

                }
            }
        }
    }
}
