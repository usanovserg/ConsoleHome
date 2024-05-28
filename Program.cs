using System;
using System.Collections.Generic;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            levels = new List<decimal>();

            string str = ReadLine("Введите верхнюю цену: ");
            priceUp = decimal.Parse(str);

            str = ReadLine("Введите нижнюю цену: ");
            priceDown = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");
            StepLevel = decimal.Parse(str);

            CalculateLevels();

            WriteLine();

            Console.ReadLine();
        }

        static List<decimal> levels;

        static decimal priceUp;
        static decimal priceDown;
        static int contLevels;

        public static decimal StepLevel
        {
            get { return stepLevel; }
            set
            {
                if (value > 0)
                {
                    stepLevel = value;
                }
                else
                {
                    Console.WriteLine("Шаг уровня должен быть положительным числом.");
                    Environment.Exit(1);
                }
            }
        }

        static decimal stepLevel;

        static void CalculateLevels()
        {
            decimal range = priceUp - priceDown;
            contLevels = (int)Math.Ceiling(range / stepLevel);

            decimal priceLevel = priceUp;

            for (int i = 0; i <= contLevels; i++)
            {
                levels.Add(priceLevel);
                priceLevel -= stepLevel;
            }
        }

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
