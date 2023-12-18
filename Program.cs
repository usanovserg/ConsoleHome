
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Program
    {
        static void Main(string[] args)
        {

            Position position = new Position();

            /*
            levels = new List<Level>();

            WriteLine();

            string str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Задайте нижнюю цену: ");

            priceDown = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            stepLevel = decimal.Parse(str);

            ContLevels = FindLevels(priceUp, priceDown, stepLevel);

            WriteLine();
            */

            Console.ReadLine();

        }

       /* static List<Level> levels;

        static decimal priceUp;

        static decimal priceDown;

        static int contLevels;

        static decimal stepLevel;

        static int ContLevels
        {
            get
            {
                return contLevels;
            }

            set
            {
                if (value > 0)

                {
                    contLevels = value;
                    levels = Level.CalculateLevels(priceUp, priceDown, stepLevel);
                }

            }
        }

     

        //=========================================== Fields ======================================================
        #region fields

    

        static int FindLevels(decimal up, decimal down, decimal count)
        {

            contLevels = Convert.ToInt32((up - down) / count + 1);

            return contLevels;

        }

        #endregion

        static void WriteLine()
        {
            Console.WriteLine("Кол-во элементов в списке: " + contLevels.ToString());

            for (int i = 0; i < contLevels; i++)
            {
                if (levels is not null)
                {
                    Console.WriteLine(levels[i].PriceLevel);
                }
                
            }

        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine();

        }

        static Trade trade = new Trade();

        */

    }
}
