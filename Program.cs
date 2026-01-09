using ConsoleHome;
using ConsoleHome.Enums;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position();

            // Подписка на события;
            position.PositionChanged += (sender, args) =>
            {
                // Здесь вывод данных о позиции на консоль (по подписке на события);
                Console.WriteLine($"\t Data Time: {args.DealTime}");                                                     // Время новой сделки;
                Console.WriteLine($"New transaction: {args.TradeSide}" +                                                 // Направление новой сделки (покупка, продажа);
                    $" \nVolume new transaction = {args.TradeVolume} \t Price new transaction = {args.TradePrice:N0}");  // Объем и Цена новой сделки;

                if (args.ChangeType == PositionChangeType.Changed)
                {
                    Console.WriteLine($"The position has changed. \tNew volume position (lot): {args.NewVolume}");     // Сообщение при изменении позиции;
                }
                else
                {
                    Console.WriteLine($"The position has not changed. \tVolume position (lot): {args.NewVolume}");     // Сообщение при неизменяемой позиции (number = 0);
                }

                Console.WriteLine($"Trade Side = {(args.NewVolume > 0 ? SidePosition.Long : args.NewVolume < 0 ? SidePosition.Short : SidePosition.None)}");
                Console.WriteLine($"Averege price position (all lots) = {args.AveragePrice:N}");                       // Средняя цена открытия позиции;
                Console.WriteLine($"Initial Margin (IM): {args.InitialMargin:N0}");                                    // Размер ГО на текущую позицию;
                Console.WriteLine($"PnL from trade: {args.PnL:N2}");                                                   // Фиксированная прибыль/убыток на часть закрытой позиции;
                Console.WriteLine($"Fixed total PnL: {args.TotalPnL:N2}");                                             // Фиксированная прибыль/убыток (накопительно);
                Console.WriteLine();
            };





            //position.PositionChanged += (sender, args) =>                                       // Второй вариант (можно удалить);
            //{
            //    if (args.ChangeType == PositionChangeType.Changed)                                     // Подписка на событие (изменение позиции);
            //    {
            //        Console.WriteLine($"The position has changed.\tNew volume: {args.NewVolume}");     // Сообщение при изменении позиции;
            //    }
            //    else
            //    {
            //        Console.WriteLine($"The position has not changed.\tVolume: {args.NewVolume}");     // Сообщение при неизменяемой позиции (number = 0);
            //    }
            //};




            // Первоначальный вариант (можно удалить);
            //position.PositionChanged += (changeType) =>            // Подписка на событие (изменение позиции);
            //{
            //    switch (changeType)
            //    {
            //        case PositionChangeType.Changed:
            //            Console.WriteLine("Позиция изменилась:");
            //            break;
            //        case PositionChangeType.NotChanged:
            //            Console.WriteLine("Позиция не менялась:");
            //            break;
            //    }
            //};

            // Первоначальный вариант (можно удалить);

            //// Подписка на событие изменения позиции
            //position.ChangePos += () =>
            //{
            //    Console.WriteLine("Позиция изменилась");
            //};



            /*
            levels = new List<Level>();

            WriteLine();

            string str = ReadLine("Введите количество уровней: ");

            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите лот уровень: ");

            lotLevel = decimal.Parse(str);

            str = Console.ReadLine();

            WriteLine();
            */

            Console.ReadLine();

        } // iofgijfpogj

        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Filds

        static List<Level> levels;

        static int countLevels;

        static decimal priceUp;

        static decimal priceLevel = priceUp;

        static decimal stepLevel;

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
