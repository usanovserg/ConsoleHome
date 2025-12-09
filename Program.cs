using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleHome;
using static MyConsole.Program;
using static MyConsole.Program.Connector;

namespace MyConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Position position = new Position(); //Создаем экземпляр класса Position

            //Сonnector.Connect();

            //Сonnector.NewTradeEvent += ProstoWrite; //создали событие котороое вызывает метод ProstoWrite

            number = WriteLine; 

            /*
            //Запускаем отдельный поток, который ждет новую сделку и потом исполняется
            Task.Run(() =>
            {
                while (true)
                {
                    if (Сonnector.Trades.Count > 0 && Сonnector.Trades.Count > _lastCount)
                    {
                        //Значит появились новые сделки
                        Console.WriteLine("Запустили бесконечный цикл");
                    }
                }

                //Установим задерку в 100 млсек
                Thread.Sleep(100);
            });
            */

            levels = new List<Level>();

            Load();

            number();

            string str = ReadLine("Введите количество уровней: ");

            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите лот на уровень: ");

            lotLevel = decimal.Parse(str);

            number();

            Save();
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

        static Connector Сonnector = new Connector();

        static int _lastCount = 0;
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
            Console.WriteLine("Кол-во элементов в списке: " + countLevels.ToString());
            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i].priceLevel);
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Запись информации в текстовый файл
        /// </summary>
        static void Save() 
        {
            //почитать что такое using!!!! Создали внутри себя конструкцию и исполняет

            using (StreamWriter writer = new StreamWriter("params.txt", false)) //пытаемся прочитать файл, если нет то создадим заново
                                                    {
                writer.WriteLine(priceUp.ToString());

                writer.WriteLine(countLevels.ToString());

                writer.WriteLine(stepLevel.ToString());
            }
            
        }

        /// <summary>
        /// Загрузка информации из файла
        /// </summary>
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
                        break;
                }
            }
        }
        

        static string ReadLine(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine();
        }

        delegate void Number();

        static Number number;

        public class Connector
        {
            public delegate void newTradeEvent(); //Объявили делегат

            public event newTradeEvent NewTradeEvent; //Объявляем событие


            public List<Trade> Trades = new List<Trade>();

            private void NewTrade( Trade trade)
            {
                Trades.Add(trade);

                //NewTradeEvent();
            }

            public void Connect()
            {
                Console.WriteLine("Connect is ExChange");
            }
        }
        #endregion
        //----------------------------------------------- Methods ---------------------------------------------------
    }
}
