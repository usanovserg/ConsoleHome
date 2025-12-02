using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ConsoleHome;  // подключен namespace ConsoleHome

namespace ConsoleHome
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position();
            /*
            levels = new List<Level>();

            WriteLine();

            string str = ReadLine("Введите количество уровней: ");
            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");
            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");
            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите лот на уровнь: ");
            lotLevel = decimal.Parse(str);
                        
            WriteLine();
            */
        }

        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Fields

        static List<Level> levels;      // список уровней (список объектов класса Level)
        static decimal priceUp;         // верхняя цена
        static int countLevels;         // кол-во уровней
        static decimal lotLevel;        // лот на уровень                                

        #endregion // Fields 

        static Trade trade = new Trade();
        static Level level = new Level();


        //----------------------------------------------- Properties ------------------------------------------------
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

        #endregion //Properties 



        //----------------------------------------------- Methods ---------------------------------------------------
        #region Methods

        static void WriteLine()
        {
            Console.WriteLine("Кол-во элементов в списке: " + levels.Count.ToString());
            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i].PriceLevel);
            }
            //Console.ReadLine();
            //1            
            //2
            //3

        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        #endregion // Methods 

    }
}
