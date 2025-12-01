using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleHome;

namespace MyConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            levels = new List<Level>();

            //WriteLine();

            string str = ReadLine("Введите количество уровней: ");

            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите лот на уровень: ");

            lotLevel = decimal.Parse(str);

            //str = Console.ReadLine();

            WriteLine();

            Console.ReadLine();

        }

        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Filds

        static List<Level> levels;

        static int countLevels;

        static decimal priceUp;

        static decimal stepLevel;

        static decimal lotLevel;

        static Trade trade = new Trade(); //Создаем новый класс типа Trade, также мы вызываем конструктор, и можем задать установки по умолчанию

        static Level level = new Level(); //Создали переменную level типа Level
        #endregion

        //----------------------------------------------- Properties ------------------------------------------------
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

                    levels = Level.CalculateLevels(priceUp, stepLevel, countLevels);
              
                }

            }
        }

        #endregion


        //----------------------------------------------- Methods ---------------------------------------------------
        #region Methods

        static void WriteLine()
        {
            Console.WriteLine("Кол-во элементов в списке: " + levels.Count.ToString());
            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i].priceLevel);
            }
            Console.ReadLine();
        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine();
        }
        #endregion
        //----------------------------------------------- Methods ---------------------------------------------------
    }
}
