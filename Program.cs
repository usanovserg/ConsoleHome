using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position();

            position.ChangePosition += PrintChangePosition;     // подписка на событие

            Console.ReadLine();
        }

        /// <summary>
        /// Вывод изменения позиции (по событию)
        /// </summary>
        /// <param name="oldQty"> предыдущая позиция </param>
        /// <param name="newQty"> текущая позиция </param>
        static void PrintChangePosition(decimal oldQty, decimal newQty)
        {
            Console.WriteLine($"Позиция изменилась: была {oldQty}, стала: {newQty}");
        }

    }
}
