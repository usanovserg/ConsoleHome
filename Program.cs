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

            // Выводим начальную позицию

            // 1. Открываем лонг 100 лотов
            Trade trade1 = new Trade("GAZP", 160.50m, 100, DateTime.Now,Direction.Long);
            position.Open(trade1);
            position.Print();

            Trade trade2 = new Trade("GAZP", 162.00m, 50, DateTime.Now, Direction.Long);
            position.Change(trade2);
            position.Print();

            // 3. Закрываем часть лонга (70 лотов)
            Trade trade3 = new Trade("GAZP", 161.80m, 70, DateTime.Now, Direction.Short);
            position.Change(trade3);
            position.Print();

            // 4. Переворачиваемся в шорт
            Trade trade4 = new Trade("GAZP", 161.90m, 100, DateTime.Now, Direction.Short);
            position.Change(trade4);
            position.Print();

            // 5. Добавляем в шорт
            Trade trade5 = new Trade("GAZP", 160.00m, 50, DateTime.Now, Direction.Short);
            position.Change(trade5);
            position.Print();

            // 6. Полностью закрываем позицию
            Trade trade6 = new Trade("GAZP", 159.50m, 70, DateTime.Now, Direction.Long);
            position.Close(trade6);
            position.Print();


            levels = new List<decimal>();

            WriteLine();

            string str = ReadLine("Введите количество уровней: ");

            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            str = Console.ReadLine();

            WriteLine();

            Console.ReadLine();

        } // iofgijfpogj

        // comment Andrew Zavedeev

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
        //----------------------------------------------- Methods ---------------------------------------------------
    }

}
