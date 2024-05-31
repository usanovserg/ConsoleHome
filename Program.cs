using System;

// Урок 1.5. Программа ... // .NET Framework 4.8 // 31.05.2024 

namespace Levels
{
    internal class Program
    {
        /// <summary>
        /// Метод 1. Генератор ценовых уровней
        /// </summary>
        /// <returns></returns>
        static Decimal[] LevelsGen(Decimal[] levels, UInt32 numLevels, Decimal priceLow, Decimal stepLevels)
        {
            levels[0] = priceLow;

            for (int i = 1; i < numLevels; i++)
            {
                levels[i] = levels[i - 1] + stepLevels;
            }

            return levels;
        }

        /// <summary>
        /// Метод 2. Вывод ценовых уровней на консоль
        /// </summary>
        /// <param name="levels"></param>
        /// <param name="numLevels"></param>
        static void LevelsToConsole(Decimal[] levels, UInt32 numLevels)
        {
            Console.WriteLine("Ценовые уровни для робота:");

            for (int i = 0; i < numLevels; i++)
            {
                Console.WriteLine(levels[i]);
            }

            Console.WriteLine();
        }

        static void Main()
        {
            Console.WriteLine("Расчёт уровней для робота");

            Console.WriteLine();

            Console.Write("Введите цену нижнего уровня:\t");

            Decimal priceLow = Decimal.Parse(Console.ReadLine().Replace('.', ','));

            Console.Write("Введите цену верхнего уровня:\t");

            Decimal priceHigh = Decimal.Parse(Console.ReadLine().Replace('.', ','));

            if (priceLow >= priceHigh)
            {
                Console.WriteLine("Ошибка! Введены неверные данные уровней.");

                Environment.Exit(13);
            }

            Console.Write("Введите шаг уровней:\t\t");

            Decimal stepLevels = Decimal.Parse(Console.ReadLine().Replace('.', ','));

            if (stepLevels > (priceHigh - priceLow))
            {
                Console.WriteLine("Ошибка! Введён неверный шаг уровней.");

                Environment.Exit(13);
            }

            UInt32 numLevels = Convert.ToUInt32((priceHigh - priceLow) / stepLevels);

            Console.WriteLine("\nКоличество уровней:\t\t" + numLevels);

            Console.WriteLine();

            Decimal[] levels = new Decimal[numLevels];

            LevelsGen(levels, numLevels, priceLow, stepLevels);

            LevelsToConsole(levels, numLevels);
        }
    }
}


