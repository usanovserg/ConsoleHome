using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            levels = new List<decimal>();
            WriteLine();
            string str = ReadLine("Введите количество уровней: ");
            countLevels = Convert.ToInt32(str);
            str = ReadLine("Задайте верхнюю цену: ");
            priceUp = decimal.Parse(str);
            str = ReadLine("Введите шаг уровня: ");
            StepLevel = decimal.Parse(str);
            WriteLine();
            Console.ReadLine();
        }

        
        #region Fields
        static List<decimal> levels;

        static decimal priceUp;

        static internal int countLevels;

        Trade trade = new Trade();


        #endregion

        #region Properties
    public static decimal StepLevel
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

                    for (int i = 0; i < countLevels; i++)
                    {
                        levels.Add(priceLevel);
                        priceLevel -= stepLevel;
                    }
                }
            }
        }
        static decimal stepLevel;
        #endregion

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
        #endregion
    }

    class Trade
    {
        #region Fields
        decimal Price = 0;
        decimal Volume = 0;
        string SecCode = "";
        string ClassCode = "";
        DateTime DateTime = DateTime.MinValue;
        string Portfolio = "";
        #endregion
    }
}