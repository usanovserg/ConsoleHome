using ConsoleHome_1_6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyConsole_1_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position();

            
            Console.WriteLine("Таймер запущен. Нажмите Enter для выхода...");
          
            Console.WriteLine("{0,-10} {1,-10} {2,-10} {3, -6} {4,-8} {5,-12} {6, -12} {7, -12} {8,-20} {9,-10} {10,-12} {11, -50}"
                , "ID", "GUID", "Ticker", "Type", "Volume", "Price", "StopLoss", "TakeProfit", "Date", "Day", "Indicator", "Comment");

            position.ChangePosEvent += DisplayMessage;
            

            Console.ReadLine();


            
            //number = WriteLine;

            //levels = new List<Level>();

            //Load();

            //number();

            //string str = ReadLine("Введите количество уровней: ");

            //countLevels = Convert.ToInt32(str);

            //str = ReadLine("Задайте верхнюю цену: ");

            //priceUp = decimal.Parse(str);

            //str = ReadLine("Введите шаг уровня: ");

            //StepLevel = decimal.Parse(str);

            //str = ReadLine("Введите лот на уровень: ");

            //lotLevel = decimal.Parse(str);

            //number();

            //Save();

            //Console.ReadLine();

        }

        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Filds
        static List<Level> levels;

        static decimal priceUp;

        static int countLevels;

        static decimal lotLevel;

        //-----------------------------------------------


        static Trade trade = new Trade();

        #endregion
        //----------------------------------------------- Fields ----------------------------------------------------

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
        static decimal stepLevel;

        //static decimal stepLevel;

        #endregion
        //----------------------------------------------- Properties ------------------------------------------------

        //static List<decimal> levels;

        //----------------------------------------------- Methods ---------------------------------------------------
        #region Methods
        

        public static void DisplayMessage(string ID, string GUID, string Ticker, string Type, string Volume, string Price, string StopLoss, string TakeProfit, string Date, string Day, string Indicator, string Comment)
        {
            Console.WriteLine("{0,-10} {1,-10} {2,-10} {3, -6} {4,-8} {5,-12} {6, -12} {7, -12} {8, -20} {9, -10} {10, -12} {11, -50}"
                                , ID, GUID, Ticker, Type, Volume, Price, StopLoss, TakeProfit, Date, Day, Indicator, Comment);

        }

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


        #endregion
        

        static void Save()
        {

            using (StreamWriter writer = new StreamWriter("params.txt.", false))  //false - перезаписать файл
            {
                writer.WriteLine(priceUp.ToString());
                writer.WriteLine(countLevels.ToString());
                writer.WriteLine(stepLevel.ToString());

            }
        }

        static void Load()
        {

            using (StreamReader reader = new StreamReader("params.txt"))
            {
                int index = 0;
                while (true)
                {
                    string line = reader.ReadLine();
                    index++;
                    switch (index)
                    {
                        case 1:
                            priceUp = decimal.Parse(line);
                            break;
                        case 2:
                            countLevels = int.Parse(line);
                            break;
                        case 3:
                            StepLevel = int.Parse(line);
                            break;
                    }
                    if (line == null)
                    {
                        break;
                    }
                }
            }

        }

        delegate void Number();
        static Number number;




    }
}

//----------------------------------------------- Methods ---------------------------------------------------


