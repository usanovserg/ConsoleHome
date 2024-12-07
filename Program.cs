using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Setka
{
    internal class Program
    {



        static void Main(string[] args)

        {



            Position position = new Position();



            //levels = new List<Level>();
            //Writeline();
            //string str = Read("Введите количество уровней:  ");
            //contLevels = Convert.ToInt32(str);
            //str = Read("Задайте верхнюю цену:  ");
            //priceUp = decimal.Parse(str);
            //str = Read("Введите шаг уровня:  ");
            //StepLevel = decimal.Parse(str);
            //Writeline();

            Console.ReadLine();
        }
        //================================================ Fields ===========================
        #region Fields

        static List<Level> levels;
        static decimal priceUp;
        static int contLevels;
        static decimal stepLevel;

        //========================================================================================



        static Trade trades = new Trade();






        #endregion
        //================================================ Properties ===========================
        #region Properties
        static decimal StepLevel
        {
            get
            {
                return stepLevel;
            }
            set
            {
                stepLevel = value;

                decimal priceLevel = priceUp;

                for (int i = 0; i < contLevels; i++)
                {
                    Level level = new Level() { PriceLevel = priceLevel };
                    levels.Add(level);

                    priceLevel -= stepLevel;
                }
            }
        }
        #endregion

        //================================================ Methods ===========================
        #region Methods
        static void Writeline()
        {
            Console.WriteLine("Кол-во элементов в списке: " + levels.Count.ToString());

            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i].PriceLevel);
            }

        }

        static string Read(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine();
        }


        static void AAAdd()
        {
            Level level = new Level();



        }
        #endregion

    }


}
