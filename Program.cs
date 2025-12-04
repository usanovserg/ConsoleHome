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
            //  position.PositionChanged(); если убрать event в объявлении делегата, то можно вызывать из вне
            Console.ReadLine();
        }
        //=================================== Methods ===============================================
        #region Methods
        /// <summary>
        /// Обработчик события . Изменение позиции
        /// </summary>
        /// <param name="position"></param>
        static void OnPositionChanged(Position position)
        {
            Console.ForegroundColor = position.DirectionPosition == DPositon.Short ? ConsoleColor.Red :
                                      position.DirectionPosition == DPositon.Long ? ConsoleColor.Green:
                                      ConsoleColor.White;
            Console.WriteLine($"Позиция изменилась: {position.DirectionPosition}  : Volume = {Math.Abs(position.Volume).ToString()} / Price = {position.Price.ToString("F2")}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Message(Position position)
        {
            Console.WriteLine("Ждем следующий трейд...");
        }
        #endregion
    }

}

