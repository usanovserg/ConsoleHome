using ConsoleHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyConsole
{

    internal class Program
    {
        static void Main(string[] args)
        {
            
            // Работа с позицией
            Position position = new Position();

            
            //Пописываемся на события
            position.ChangePositionEvent += EventToConsole;
            position.OpenPositionEvent += EventToConsole;
            position.ClosePositionEvent += EventToConsole;

            /*
            //Создаем позицию
            Trade trade1 = new Trade("SBER", 275.50m, 200, DateTime.Now,Direction.Long);
            //Position position = Position.OpenPosition(trade1);
            position.Open(trade1);
            position.Print();

            //Изменяем
            Trade trade2 = new Trade("SBER", 255.00m, 150, DateTime.Now, Direction.Long);
            position.Change(trade2);
            position.Print();

            Trade trade3 = new Trade("SBER", 228.65m, 100, DateTime.Now, Direction.Short);
            position.Change(trade3);
            position.Print();

            Trade trade4 = new Trade("SBER", 210.70m, 300, DateTime.Now, Direction.Short);
            position.Change(trade4);
            position.Print();

            Trade trade5 = new Trade("SBER", 190.00m, 50, DateTime.Now, Direction.Short);
            position.Change(trade5);
            position.Print();

            //Закрываем позицию
            Trade trade6 = new Trade("SBER", 159.50m, 100, DateTime.Now, Direction.Long);
            position.Close(trade6);
            position.Print();
            Console.WriteLine($"Сумма комиссии: { position.TotalCost:F2}");
            Console.WriteLine($"Общий результат: { position.TotalResult:F2}");
            Console.WriteLine($"Общий чистый результат:{ (position.TotalResult- position.TotalCost):F2}");

            */
            /*
            number = WriteLine;

            levels = new List<Level>();

            Load();

            number();

            string str = ReadLine("Введите количество уровней: ");

            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите лот уровня: ");

            lotLevel = decimal.Parse(str);


            number();

            Save();
            
            Console.ReadLine();
            */
            while (true) 
            { 
                Thread.Sleep(1000);
            }
        }


        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Filds

        static List<Level> levels;
        
        static decimal priceUp;

        static int countLevels;

        static decimal lotLevel;

        #endregion
        //----------------------------------------------- Fields ----------------------------------------------------

        static Trade trade = new Trade();

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
        #endregion
        //----------------------------------------------- Properties ------------------------------------------------
        //----------------------------------------------- Methods ---------------------------------------------------
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
            try { 
            
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
                
                    if (line == null) { break; }
                }
            }

                  
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
    
        }

        static void EventToConsole(string message) { Console.WriteLine(message); }
        #endregion
        //----------------------------------------------- Methods ---------------------------------------------------
        delegate void Number();

        static Number number;
    
    }

}
