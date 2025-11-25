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
            levels = new List<decimal>();
            WriteLine();

            //string str = ReadLine("Введите количество уровней: ");

            //contLevels = Convert.ToInt32(str);

            string str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Задайте нижнюю цену: ");

            priceDown = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");



            StepLevel = decimal.Parse(str);


            WriteLine();
            Console.ReadLine();
        }

        static List<decimal> levels;
        static decimal priceUp;
        static decimal priceDown;
        static int contLevels;
        static decimal StepLevel
        {
            get
            {
                return stepLevel;
            }

            set
            {
                if (value <= 100)
                {
                    stepLevel = value;
                    decimal priceLevel = priceUp;
                    contLevels = Convert.ToInt32(decimal.Round((priceUp - priceDown) / stepLevel));

                    for (int i = 0; i < contLevels; i++)
                    {
                        levels.Add(priceLevel);
                        priceLevel -= stepLevel;

                    }
                }
            }
        }

        static decimal stepLevel;

        static void WriteLine()
        {
            Console.WriteLine("Кол-во элементов в списке: " + levels.Count.ToString());
            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i]);

            }

        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();

        }

    }
}
