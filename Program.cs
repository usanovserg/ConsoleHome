using ConsoleHome;
using System;
using System.Collections.Generic;
using System.Data;

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
          //  position.PositionChanged(); если убрать event в объявлении делегата, то можно вызывать из вне
            Console.ReadLine();
        }
        //=================================== Methods ===============================================
        #region Methods
        static void OnPositionChanged()
        {
            Console.WriteLine("Позиция изменилась");
        }
        #endregion
    }

}

