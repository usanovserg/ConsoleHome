using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleHome
{
    public class Program
    {
        static void Main(string[] args)
        {
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
            }

            Connector.Connect();
            Connector.NewTradeEvent += WriteLine;

            Task.Run(() =>
            {
                while (true)
                {
                    if (Connector.Trades.Count > _lastCount)
                    {
                        // новый сделки появились
                    }
                    Thread.Sleep(100);
                }
            });


            //Position position = new Position();

            
            levels = new List<Level>();
            Load();
            WriteLine();
            string str = ReadLine("Введите количество уровней: ");
            countLevels = Convert.ToInt32(str);
            str = ReadLine("Задайте верхнюю цену: ");
            priceUp = decimal.Parse(str);
            str = ReadLine("Введите шаг уровня: ");
            StepLevel = decimal.Parse(str);
            str = ReadLine("Введите лотность: ");
            lotLevel = decimal.Parse(str);
            WriteLine();

            Save();

            Console.ReadLine();
        }

        
        #region Fields

        static List<Level> levels;

        static decimal priceUp;

        static internal int countLevels;

        static Trade trade = new Trade();

        static Level level = new Level(120);

        static decimal lotLevel;
        
        static Connector Connector = new Connector();

        static int _lastCount = 0;

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

                    levels = Level.CalculateLevels(priceUp, stepLevel, countLevels);
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
                Console.WriteLine(levels[i].PriceLevel);
            }
        }
        static string ReadLine(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
        static void rndmfnctn()
        {
            //trade.Price = 2225;
            //trade.Volume = 655000;
            //string str = priceUp.ToString();

            Level level = new Level(120);
        }
        static void Save()
        {
            using (StreamWriter writer = new StreamWriter("params.txt", false))
            {
                writer.WriteLine(priceUp.ToString());
                writer.WriteLine(countLevels.ToString());
                writer.WriteLine(stepLevel.ToString());
                writer.WriteLine(lotLevel.ToString());
            }
        }

        static void Load()
        {
            try
            {
                StreamReader reader = new StreamReader("params.txt");

                int index = 0;

                while (true)
                {
                    string line = reader.ReadLine();

                    index++;

                    //if (index == 1) {priceUp = decimal.Parse(line);}

                    //else if (index == 2) {countLevels = int.Parse(line);}

                    //else if (index == 3) {stepLevel = decimal.Parse(line);}

                    //else if (index == 4) {lotLevel = decimal.Parse(line);} равнозначно тому что внизу через switch


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

                        case 4:
                            lotLevel = decimal.Parse(line);
                            break;

                    }

                    if (line == null)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        #endregion
    }   
}