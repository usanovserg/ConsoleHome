using ConsoleHome.Models;

using System;

namespace ConsoleHome
{
    public class Program // имитируем Вьюшку (Окно Виндос)
    {
        static void Main(string[] args)
        {
            VM vm = new VM();

            vm.EventMessageDelegate += Print;

            Console.ReadLine();
        }

        private static void Print(string message)
        {
            Console.WriteLine(message);
        }

    }
}
