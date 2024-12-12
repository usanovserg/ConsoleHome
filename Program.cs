using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace УровниСетки
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<decimal> levels = new List<decimal>();

            Console.WriteLine("Введите верхнию чену(знак припенания , ) : ");

            string str = Console.ReadLine();

            decimal priceUp = decimal.Parse(str);

            Console.WriteLine("Введите нижнию чену: ");

            str = Console.ReadLine();

            decimal priceDown = decimal.Parse(str);

            Console.WriteLine("Введите шаг уровня: ");

            str = Console.ReadLine();


            decimal stepLevel = decimal.Parse(str);

            decimal stepPrice = priceUp;

            int count = (int)((priceUp - priceDown) / stepLevel);

            for (decimal i = 0; i < count; i++) 
            {
                levels.Add(stepPrice);

                stepPrice -= stepLevel;

            }
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(levels[i]);    

            }
            Console.ReadLine();
        }
    }
}
