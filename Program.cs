using ConsoleHome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;                               // ВСЁ ЧТО ЗАКОМЕНТИРОВАНО ЯВЛЯЕТСЯ ПЕРВОНОЧАЛЬНОЙ ВЕРСИЕЙ
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace УровниСетки
{
    public class Program
    {
        Position position = new Position();

        /*
        static void Main(string[] args)
        {
                       
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

            WriteLine();
       
            Console.ReadLine();
        }       */

        static Trade trade = new Trade();
        

        //============================================================= Fields =======================================
        #region Fields

        static List<Level> levels = null!;  //поле        ! точно не будет null

        static decimal stepLevel; //поле

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

                    int count = (int)((priceUp - priceDown) / stepLevel);

                    levels = Level.CalculatedLevels(priceUp, stepLevel, count);
                }
            }
        }

        #endregion



        //============================================================= Methods ========================================
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

            string? str = Console.ReadLine();

            if (str == null) return "";

            return str;
        }

        #endregion

    }

    

}

