using ConsoleHome;
using System;
using System.Collections.Generic;
using System.Linq;                               
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Program
    {       
        static Trade trade = new Trade();  
       
        static void Main(string[] args)
        {
            Position position = new Position();
            /*
            levels = new List<Level>();
            WriteLine();
            string str = ReadLine("Введите верхнию цену(знак припенания , ) : ");
            priceUp = decimal.Parse(str);
            str = ReadLine("Введите нижнию чену: ");
            priceDown = decimal.Parse(str);
            str = ReadLine("Введите шаг уровня: ");
            StepLevel = decimal.Parse(str);
            str = ReadLine("Введите лот на уровень: ");
            lotlevel = decimal.Parse(str);
            WriteLine();*/
            Console.ReadLine();
        }
        



        //============================================================= Fields =======================================
        #region Fields

        static List<Level>? levels;  //поле       = null!   точно не будет null  или можно ставить вопрос
        static decimal stepLevel;
        static decimal priceUp;
        static decimal priceDown;
        static decimal lotlevel;

       
        #endregion


        //============================================================= Properties ===================================
        #region Properties

        public static decimal StepLevel                       //развернутая запись свойства ( в ней можно расписывать различные действия)
        {
            get
            {
                return stepLevel;
            }
            set
            {
                if (value <= 100)                                            //проверка на коректность
                {
                    stepLevel = value;
                    levels = Level.CalculateLevels(priceUp, stepLevel, priceDown);

                }
            }
        }
        

        #endregion





        //============================================================= Methods ========================================
        #region Methods

        static void WriteLine()
        {
            Console.WriteLine("Количество элементов в списке: " + levels?.Count.ToString());

            for (int i = 0; i < levels?.Count; i++)
            {
                Console.WriteLine(levels[i].PriceLevel);
            }
        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);
            //string? str = Console.ReadLine();
            // if (str == null) return "";
            return Console.ReadLine();  //str;
        }

        static void Fhjksk()
        {
            Level level = new Level();
            level.PriceLevel = 9900;
        }



        #endregion


    }
    
}

