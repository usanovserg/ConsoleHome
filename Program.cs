using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* ДЗ 1.5
 * создать ernum для класса Trade
 * Поле направления сделки лонг/шорт
 * добавиь поля и свойства в класс Position для описания позиции: 
 *     - кол. открытых лотов 
 *     - цена открытия сделки
 *     - что ещё должна учитывать сделка? какие поля ещё нужны для описания сделки
 * 
 */

namespace ConsoleHome
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position();


            /*
            levels = new List<Level>();

            //WriteLine();

            string str = ReadLine("Введите количество уровней: ");

            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите количество лотов на уровень: ");

            lotLevel = decimal.Parse(str);

            //str = Console.ReadLine();

            WriteLine();
            */
            Console.ReadLine(); // фиксатор от закрытия программы
            
        } // iofgijfpogj


        //zzzzzzzzzzzzzzzzzzzzzzzzzzzz

        static Trade trade = new Trade();

        static Level level = new Level();


    //static void SomeMethod() 
    //    {
    //        level.LotForLevel = 5;
    //    }







        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Fields

        static List<Level> levels;

        static int countLevels;

        static decimal priceUp;

        static decimal priceLevel = priceUp;

        static decimal stepLevel;

        static decimal lotLevel;

        #endregion
        //----------------------------------------------- Fields end -------------------------------------------------

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

                    levels = Level.CalculateLevels(priceUp, StepLevel, countLevels);

                    //decimal priceLevel = priceUp;

                    //Level.LotForLevel = lotLevel;

                    //for (int i = 0; i < countLevels; i++)
                    //{
                    //    Level level = new Level();
                    //    level.PriceLevel = priceLevel;
                    //    // Level level = new Level() {PriceLevel = priceLevel}; // второй вариант

                    //    levels.Add(level);

                    //    priceLevel -= stepLevel;
                    //}
                }

            }
        }

        #endregion
        //----------------------------------------------- Properties end --------------------------------------------


        //----------------------------------------------- Methods ---------------------------------------------------
        #region Methods

        static void TestMethod() 
        {
            trade.Price = 1000;
            trade.Volume = 50;
            string str = priceUp.ToString();
        }


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
        //----------------------------------------------- Methods end -----------------------------------------------
    }
}
