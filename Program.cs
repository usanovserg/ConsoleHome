using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleHome

{
    class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position();

 /*           string price = "100,1";

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
            }
 */

  //          Connector.Connect();

  //          Connector.NewTradeEvent += WriteLine;

 /*
            number = WriteLine;

            levels = new List<Level>();

            Load();

            number();

            Task.Run(() =>
            {

                while (true)
                {
                    if (Connector.Trades.Count > _lastCount)
                    {
                        // Значит появились новые сделки
                    }

                    Thread.Sleep(100);
                }
            });

            string str = ReadLine("Введите количество уровней:    ");

            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену:  ");

            priceUp = decimal.Parse(str);

//            str = ReadLine("Задайте нижнюю цену:  ");

//            priceDwn = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня:  ");

            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите лот на уровень:  ");

            lotLevel = decimal.Parse(str);

            number();

            Save();
*/

            Console.ReadLine();
        }
        
        // ======= Fields ======

        #region Fields

        static List<Level> levels;

        static decimal priceUp;

        //        static decimal priceDwn;

        static int countLevels;

        static decimal lotLevel;
        static int _lastCount;

        static Connector Connector = new Connector();

        // =====================

        static Trade trade = new Trade();

        
        #endregion

        // ======= Properties ======

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

                    levels = Level.Calculatelevels(priceUp, stepLevel, countLevels);
                }

            }
        }

        static decimal stepLevel;


        #endregion

        // ======= Methods ======

        #region Methods

        static void WriteLine()
        {
            Console.WriteLine("Количество элементов в списке:  " + levels.Count.ToString());

            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i].Pricelevel);
            }
        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);

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

        delegate void Number();

        static Number number;


        //        static int contLevels;




    }

    public class Connector
    {
        public delegate void newTradeEvent();

        public event newTradeEvent NewTradeEvent;
        
        public List<Trade> Trades = new List<Trade>();

        private void NewTrade(Trade trade)
        {
            Trades.Add(trade);
        }

        public void Connect()
        {
            Console.WriteLine("Connect is Exchange");
        }

        /* public void AddDelegate(newTradeEvent method)
        {
            NewTradeEvent = method;
        }
        */
    }
}
