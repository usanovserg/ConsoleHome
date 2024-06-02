using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Program
    {
        static void Main(string[] args)
        {
            //---------------------------
            #region
            // ------------------------------------------------------------- //43.11 проверка ошибок = 45мин  try-catch
            /*Connector.Connect();
            Connector.NewTredeEvent += WriteLine; 

            string price = "100,1";

            int num = 0;

            decimal dec = 0;

            try
            {
                num = int.Parse(price);

                Console.WriteLine(num);
            }
            catch (Exception ex)
            {
                dec = decimal.Parse(price);

                Console.WriteLine(dec);
            }*/



            /*
            number = WriteLine;

            levels = new List<Level>();

            Load();

            number();

            #region  //25.47
            /*
            Task.Run(() =>     //25.47
            {
                while (true)
                {
                    if (Connector.Trades.Count > 0)
                    {
                        //Значит появилась новая сделка
                        
                    }
                    Thread.Sleep(100);
                }

            });

            
            

            string str;

            str = ReadLine("Введите кол-во уровней: ");
            countLevels = Convert.ToInt32(str);

            str = ReadLine("Введите верхнюю цену: ");
            priceUp = decimal.Parse(str);

            //str = ReadLine("Задайте нижнию цену: ");
            //priceDown = decimal.Parse(str);

            str = ReadLine("Введите шашг уровня: ");
            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите лот на уровень: ");
            LotLevel = decimal.Parse(str);

            number();
            
            Save();
            */
            #endregion


            Position position = new Position();

            Console.ReadLine();

        }
        //---------------------------------------------- Fildes ------------------------------------------------
        #region = Fildes =

        static List<Level> levels;

        static decimal priceUp;

        static int countLevels;

        static decimal LotLevel;

        //static Connector Connector = new Connector();

        static int _lastCount = 0;

        static decimal priceDown;

        //----------------------------------------------

        static Trade trade = new Trade();

        #endregion

        //---------------------------------------------- Propertis ----------------------------------------------
        #region = Propertis =

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

                    //levels = CalculateLevels(priceUp, priceDown, stepLevel);

                    levels = Level.CalculateLevels(priceUp, stepLevel, countLevels);

                }
            }
        }
        static decimal stepLevel;
        #endregion

        //---------------------------------------------- Methods -----------------------------------------------
        #region = Methods =

        #region ---//---
        /*static List<decimal> CalculateLevels(decimal upperPrice, decimal lowerPrice, decimal step)
        {
            List<decimal> calculatedLevels = new List<decimal>();

            for (decimal priceLevel = upperPrice; priceLevel >= lowerPrice; priceLevel -= step)
            {
                calculatedLevels.Add(priceLevel);
            }
            return calculatedLevels;
        }*/
        #endregion



        static void WriteLine()
        {
            Console.WriteLine("Количество элементов в списке: " + levels.Count.ToString());

            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i].PriceLevel);
            }
        }
        static string ReadLine(string massage)
        {
            Console.WriteLine(massage);

            return Console.ReadLine();
        }

        static void Save()
        {
            using (StreamWriter writer = new StreamWriter("params.txt", false))
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
                            StepLevel = decimal.Parse(line);
                            break;
                    }

                    if (line == null)
                    {
                        break;
                    }
                }
            }



        }

        #endregion

        delegate void Number();      //  12.30  время видео урока 1,6

        static Number number;

    }

    //-------
    #region  //15,30
    /*
    public class Connector        
    {
        public delegate void newTredeEvent();
        public event newTredeEvent NewTredeEvent;

        public List<Trade> Trades = new List<Trade>();

        private void NewTrade(Trade trade)
        {
            Trades.Add(trade);

            //NewTredeEvent();
        }
        public void Connect()
        {
            Console.Write("Connect Is ExChange");
        }

        
    }*/
    #endregion

}
