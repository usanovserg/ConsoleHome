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

            /*
            levels = new List<decimal>();

            WriteLine();

            string str = ReadLine("Введите количество уровней: ");

            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            str = ReadLine("Введите лот на уровень: ");

            lotLevel = decimal.Parse(str);
            
            str = Console.ReadLine();

            WriteLine();

            Console.ReadLine();
            */
        }

        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Filds

        static int countLevels;

        static decimal priceUp;

        static decimal priceLevel = priceUp;

        static decimal stepLevel;



        #endregion
        //-----------------------------------------------------------------------------------------------------------

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

                   // levels = Levels.CalculateLevels(priceUp, stepLevel, countLevels);
                }

            }
        }

        #endregion
        //-----------------------------------------------------------------------------------------------------------

        static List<decimal> levels;

        //----------------------------------------------- Methods ---------------------------------------------------
        #region Methods

        static void WriteLine()
        {
            Console.WriteLine("Кол-во элементов в списке: " + levels.Count.ToString());
            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i]);
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

        static void SomeMethode() 
        {
            Levels level = new Levels();





        }


        #endregion
        //-----------------------------------------------------------------------------------------------------------
    }
   



 

}
