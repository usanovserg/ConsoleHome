using System;
using System.Collections.Generic;

namespace ConsoleHome
{
    public class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position("BTCUSD");

            position.PositionChanged += Position_PositionChanged;
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

        private static void Position_PositionChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Позиция изменилась");
        }
        #endregion
    }
}
