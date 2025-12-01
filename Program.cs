using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            levels = new List<decimal>();

            //WriteLine();

            string str = ReadLine("Введите количество уровней: ");

            countLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхнюю цену: ");

            priceUp = decimal.Parse(str);

            str = ReadLine("Введите шаг уровня: ");

            StepLevel = decimal.Parse(str);

            str = Console.ReadLine();

            WriteLine();

            Console.ReadLine();

        }

        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Filds

        static List<decimal> levels;

        static int countLevels;

        static decimal priceUp;

        static decimal stepLevel;

        static Trade trade = new Trade(); //Создаем новый класс типа Trade
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

        static void Prosto()
        {
            trade.Price = 225;
            trade.Volume = 100; 
        }
        #endregion
        //----------------------------------------------- Methods ---------------------------------------------------
    }

    //==============================================Trade==============================
    class Trade
    {
        //===========================================Fields (поля)
        #region Fields
        public decimal Price = 0;


        public  string SecCode = "";

        public string ClassCode = "";

        public DateTime DateTime  = DateTime.MinValue;

        string Portfolio = "";

        #endregion

        //=============================================Properties (свойства)
        #region Properties 
        public decimal Volume
        {
            get
            {
                return _volume; 
            }
            set
            {
                _volume = value; 
            }
            
        }
        //Внутренние поля рекомендуется начинать с нижнего подчеркивания и с маленькой буквы!
        //Это приватные поля
        //Публичные поля рекомендуется писать с большой буквы
        decimal _volume = 0;

        #endregion


    }
}
