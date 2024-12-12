using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Position position = new Position();

            position.AddDelegat(WriteLine_delegat);

            position.ChangePosition_event += WriteLine_event;

            /*
            Hhbjknk();

            levels = new List<Level>();

            WriteLine();

            string str = ReadLine("Введите количество уровней: ");

            contLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите лот на уровень: ");

            lotLevel = decimal.Parse(str);

            WriteLine();

            */

            Console.ReadLine();
        }
        
        //============================================ Fields ==========================================

        #region Fields

        static List<Level> levels;

        static decimal priceUp;

        static int contLevels;

        static decimal lotLevel;

        //==========================

        static Trade trade = new Trade();

        
        #endregion

        //=========================================== Properties ========================================

        #region Properties

        static decimal StepLevel
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

                    levels = Level.Calculate(priceUp, stepLevel, contLevels);
                }

            }
        }
        static decimal stepLevel;

        #endregion

        //============================================ Methods ==========================================

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

        static void Hhbjknk()
        {
            Level level = new Level();
            level.PriceLevel = 9900;
        }

        public static void WriteLine_delegat()
        {
            Console.WriteLine($"Сработал delegat. Позиция изменилась. Текущая позиция: {Trade.VolumePosition}");
        }

        public static void WriteLine_event()
        {
            Console.WriteLine($"Сработал event. Позиция изменилась. Средняя цена позиции: {Trade.AvgPrice}");
        }
        #endregion
    }
}
