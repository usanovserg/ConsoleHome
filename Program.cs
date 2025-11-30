using ConsoleHome;
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
            Position position = new Position();

            Console.WriteLine("Таймер запущен. Нажмите Enter для выхода...");
            Console.WriteLine();
            Console.WriteLine("{0,-10} {1,-6} {2,-8} {3,-12} {4, -12} {5, -12} {6,-20} {7,-10} {8,-12}", "Ticker", "Type", "Volume", "Price", "StopLoss", "TakeProfit", "Date", "Day", "Indicator");

            Console.ReadLine();
            /*
            levels = new List<decimal>();

            WriteLine();

            string str = ReadLine("Введите количество уровней: ");

            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: "); //tests

            StepLevel = decimal.Parse(str);

            str = Console.ReadLine();

            WriteLine();

            Console.ReadLine();
            */

        }

        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Filds

        static int countLevels;

        static decimal priceUp;

        static decimal priceLevel = priceUp;

        static decimal stepLevel;

        #endregion
        //----------------------------------------------- Fields ----------------------------------------------------

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

                    decimal priceLevel = priceUp;

                    for (int i = 0; i < countLevels; i++)
                    {
                        levels.Add(priceLevel);

                        priceLevel -= stepLevel;
                    }
                }

            }
        }

        #endregion
        //----------------------------------------------- Properties ------------------------------------------------

        static List<decimal> levels;

        //----------------------------------------------- Methods ---------------------------------------------------
        #region Methods

        static void WriteLine()
        {
            Console.WriteLine("Кол-во элементов в списке: " + levels.Count.ToString());
            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i]);
            }
            Console.ReadLine();
            //1            
            //2
            //3

        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine();
        }


        #endregion
        

        //"StopLoss": 74800,
        //"TakeProfit": 76000,
        



        static Trade trade = new Trade();


            

    }

        //----------------------------------------------- Methods ---------------------------------------------------
    }

