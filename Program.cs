using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urok_1_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            levels = new List<decimal>();

            WriteLine();

            string str = ReadLine("Введите количество уровней: ");

            contLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            WriteLine();

            Console.ReadLine();
        }

        //============================================ Fields ==========================================

        #region Fields

        static List<decimal> levels;
        static decimal priceUp;
        static int contLevels;

        //==========================

        static Trade trade = new Trade();


        #endregion

        //=========================================== Properties ========================================

        #region Properties

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

                    for (int i = 0; i < contLevels; i++)
                    {
                        levels.Add(priceLevel);
                        priceLevel -= stepLevel;
                    }
                }

            }
        }
        static decimal stepLevel;

        #endregion

        //============================================ Methods ==========================================

        #region Methods


        static void WriteLine()
        {
            Console.WriteLine("Количество элементов в списке: " + levels.Count.ToString());

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

        static void Hhbjknk()
        {
            trade.Price = 2225;

            trade.Volume = 232325656;

        }

        #endregion
    }
}
