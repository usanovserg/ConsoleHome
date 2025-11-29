using ConsoleHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Position position = new Position();

            // Выводим начальную позицию

            // 1. Открываем лонг 100 лотов
            Trade trade1 = new Trade("GAZP", 160.50m, 100, Direction.Long);
            position.Open(trade1);
            position.Print();


        }
    }
}
