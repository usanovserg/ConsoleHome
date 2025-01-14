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

            /*
            levels = new List<Level>();

            WriteLine();

            string str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            //str = ReadLine("Задайте нижнюю цену: ");

            //priceDown = decimal.Parse(str);

            str = ReadLine("Задайте количество уровней : ");

            countLevels = int.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите лот на уровень: ");

            lotLevel = decimal.Parse(str);

            WriteLine();
            */

            Console.ReadLine();
        }

        //====================================== Fields =====================================

        #region Fields

        static List<Level> levels;

        static decimal priceUp;

        static decimal priceDown;

        static int countLevels;

        static decimal lotLevel;

        //=======================================

        static Trade trade = new Trade();

        #endregion

        //====================================== Properties =====================================

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

                    levels = Level.CalculateLevels(priceUp, stepLevel, countLevels);
                }

            }
        }
        static decimal stepLevel;

        #endregion

        //====================================== Methods =====================================

        #region Methods

        static void WriteLine()
        {
            Console.WriteLine("Кол-во элементов в списке: " + levels.Count.ToString());

            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i].PriceLevel);
            }

        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine(); ;
        }

        /*
        static void Asdfgfn()
        {
            trade.Price = 2225;

            trade.Volume = 456542;

        }
        */

        #endregion




    }
}
