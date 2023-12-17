using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    public class Program
    {

        static void Main(string[] args)
        {

            Positions positions = new Positions();


            /*levels = new List<Level>();

            WriteLine();

            Console.WriteLine("Введите количество уровней:  ");

            string str = Console.ReadLine();

            kollevels = Convert.ToInt32(str);

            Console.WriteLine("Задайте верхнюю цену:  ");

            str = Console.ReadLine();

            priceUP = decimal.Parse(str);

            Console.WriteLine("Введите шаг уровня:  ");

            str = Console.ReadLine();

            StepLevel = decimal.Parse(str);

            Console.WriteLine("Введите лот на уровень:  ");

            str = Console.ReadLine();

            lotlevel = decimal.Parse(str);

            //decimal stepLevel = Convert.ToDecimal(str);

            WriteLine();*/

            Console.ReadLine();

        }
        //================================================Pole===================================================================================
        #region Field  Pole

        static List<Level> levels;  // создали поле, типа список состоящее из объектов типа decimal. Обычное поле класса Program

        static decimal priceUP;

        static int kollevels;

        static decimal lotlevel;
        //===============================================


        static Trade trade = new Trade();

        #endregion

        //================================================Svoystva===============================================================================
        #region Properties Svoystva

        static decimal stepLevel; // создали поле

        public static decimal StepLevel // создаем поле типа Свойства, имеет два метода Get и Set

        {
            get
            {
                return stepLevel;
            }
            set
            {
                stepLevel = value;

                levels = Level.CalculateLevels(priceUP, stepLevel, kollevels);

            }
        }
        #endregion
        //================================================Metod==================================================================================
        #region Metod

        static void WriteLine()   // создаем метод WriteLine
        {
            Console.WriteLine("Количество элементов в списке:  " + levels.Count.ToString());

            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i].PriceLevel);
            }

        }


        #endregion



    }











}
