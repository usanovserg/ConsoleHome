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
            levels = new List<Level>();

            WriteLine();

            string str = ReadLine("Введите количество уровней: ");

            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите лот на уровень: ");

            lotLevel = decimal.Parse(str);

            WriteLine();

            Console.ReadLine();
        }

        //=================================== Fields =====================================

        #region Fields

        static List<Level> levels;

        static decimal priceUp;

        static int countLevels;

        static decimal lotLevel;

        //-----------------------------------------------


        static Trade trade = new Trade();

        


        #endregion

        //=================================== Properties ==================================
        #region Properties

        public static decimal StepLevel
        {
            get
            {
                return StepLevel;
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


        //=================================== Methods =====================================
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

            return Console.ReadLine();
        }

        static void Jddsdsy()
        {

        }

        #endregion


    }

  
}
