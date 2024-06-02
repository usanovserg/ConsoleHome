using ConsoleHome;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position("MyInstrument");

            Console.ReadLine();
        }

        #region Fields
        static List<Level> levels;
        static decimal priceUp;
        static int contLevels;
        static decimal lotLevel;
        static decimal stepLevel;

        static Trade trade = new Trade();
        #endregion

        #region Properties
        public static decimal StepLevel
        {
            get { return stepLevel; }
            set
            {
                if (value <= 100)
                {
                    stepLevel = value;
                    levels = Level.CalculateLevels(priceUp, stepLevel, contLevels);
                }
            }
        }
        #endregion

        #region Methods
        static void WriteLine()
        {
            Console.WriteLine("Кол-во элементов в списке " + levels.Count.ToString());
            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i].PriceLevel);
            }
        }

        static string Readline(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        static void asdad()
        {
            Level level = new Level();
            level.PriceLevel = 9900;
        }
        #endregion
    }
}
