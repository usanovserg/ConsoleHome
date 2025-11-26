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
            List<decimal> priceLevel = new List<decimal>();
            int countLevel = 0;

            Console.WriteLine("Введите нижний уровень цены = ");
            decimal downLevel = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Введите верхний уровень цены = ");
            decimal upLevel = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Введите шаг цены = ");
            decimal stepPrice = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Всего уровней цены = " + (countLevel = GetCountLevel(downLevel, upLevel, stepPrice)));
            priceLevel = GetPriceLevel(downLevel, stepPrice, countLevel);

            Console.WriteLine("Уровни цены = ");
            for (int i = 0; i < countLevel; i++)
            {
                Console.WriteLine(priceLevel[i]);
            }




            static int GetCountLevel(decimal downLevel, decimal upLevel, decimal stepPrice)
            {
                int countLevel = Convert.ToInt32((upLevel - downLevel) / stepPrice);
                return countLevel;
            }

            static List<decimal> GetPriceLevel(decimal downLevel, decimal stepPrice, int countLevel)
            {
                List<decimal> priceLevel = new List<decimal>();
                decimal beginPrice = downLevel;
                for (int i = 0; i <= countLevel; i++)
                {
                    priceLevel.Add(beginPrice);
                    beginPrice += stepPrice;
                }
                return priceLevel;

            }


        }
    }
}
