using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleHome
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Инициализация списка и ввод данных для расчета 

            priceLevels = new List<decimal>();

            WriteLines();

            Console.Write("Задайте верхний уровень цены: ");

            decimal topPrice = Decimal.Parse(Console.ReadLine());

            Console.Write("Задайте нижний уровень цены: ");

            decimal bottomPrice = Decimal.Parse(Console.ReadLine());

            Console.Write("Задайте шаг цены: ");

            decimal priceStep = Decimal.Parse(Console.ReadLine());

            //Расчет сетки уровней

            decimal priceLevel = topPrice;

            while (priceLevel >= bottomPrice)
            {
                priceLevels.Add(priceLevel);
                priceLevel -= priceStep;
            }

            //Вывод сетки на консоль

            WriteLines();

            Console.ReadKey();
        }

        static List<Decimal> priceLevels;

        static void WriteLines()
        {
            Console.WriteLine("Количество уровней: " + priceLevels.Count.ToString());

            Console.WriteLine("Уровни: ");
            for (int i = 0; i < priceLevels.Count; i++)
            {
                Console.WriteLine(priceLevels[i]);
            }
        }
    }
}

