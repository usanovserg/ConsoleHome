using ConsoleHome;
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
            AsdfSst();

            levels = new List<decimal>();//Так как сейчас мы пишем "Сеточник", то List будет сложнее, чем decimal

            WriteLine();

            string str = ReadLine("Введите количество уровней: ");

            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            WriteLine();

            Console.ReadLine();
        }

        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Fields

        static List<decimal> levels;

        static decimal priceUp;

        static int countLevels;
        //=========================


        static Trade trade = new Trade(); 

     

        #endregion

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

                    decimal priceLevel = priceUp;

                    for (int i = 0; i < countLevels; i++)
                    {
                        levels.Add(priceLevel);

                        priceLevel -= stepLevel;
                    }
                }

            }
        }
        static decimal stepLevel;
        #endregion


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
        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine();
        }

        static void AsdfSst()
        {
            Level level = new Level(9000);

            trade.Price = 2555; //Наводим и видим "поле" ("кирпичик")

            trade.Volume = 34142134; //Наводим и видим "свойство" (гаечный ключ)

            string str = priceUp.ToString();
        }

        #endregion

       
    }

    

}
