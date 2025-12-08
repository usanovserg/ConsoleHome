using ConsoleHome;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using static ConsoleHome.Position;

namespace MyConsole
{
    internal class Program
    {
        //=================================== Fields ===============================================
        #region Fields
        #endregion
        //=================================== Properties ===============================================
        #region Prperties
        #endregion
        static void Main(string[] args)
        {
            Position position = new();
            position.PositionChanged += OnPositionChanged;
            position.PositionChanged += Message;
            position.ProfitChanged += OnProfitChanged;
            Console.ReadLine();
        }
        //=================================== Methods ===============================================
        #region Methods
        /// <summary>
        /// Обработчик события . Изменение позиции
        /// </summary>
        static void OnPositionChanged(Position position)
        {
            Console.ForegroundColor = position.DirectionPosition == DPositon.Short ? ConsoleColor.Red :
                                      position.DirectionPosition == DPositon.Long ? ConsoleColor.Green:
                                      ConsoleColor.White;
            Console.WriteLine($"Позиция изменилась: {position.DirectionPosition}  : Volume = {Math.Abs(position.Volume)} / Price = {position.Price:F2}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Message(Position position)
        {
            Console.WriteLine("Ждем следующий трейд...");
        }
        /// <summary>
        /// Обработка события изменения профита
        /// </summary>
        /// <param name="position"></param>
        static void OnProfitChanged(Position position)
        {
            Console.WriteLine($"ПРИБЫЛЬ по трейду = {position.Profit:F2}");
            Console.WriteLine($"Суммарная ПРИБЫЛЬ  = {position.TotalProfit:F2}");
        }
        #endregion
    }

}

