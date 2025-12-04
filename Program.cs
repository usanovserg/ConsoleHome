using ConsoleHome;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyConsole.Position;

namespace MyConsole
{
    public class Program
    {
        public static void MessageOfChange(decimal price)
        {
            Console.WriteLine("СОБЫТИЕ !  Цена изменилась ! " + price);
        }
            static void Main(string[] args)
        {
                     

           Position position = new Position();
            
        //
        

        //----------------------------------------------- Comment---------------------------------------------------- 
        #region Comment   
        /*
        levels = new List<Level>();

          WriteLine();

          string str = ReadLine("Введите количество уровней: ");

          countLevels = Convert.ToInt32(str);

          str = ReadLine("Задайте верхнюю цену: ");

          priceUp = decimal.Parse(str);

          str = ReadLine("Введите шаг уровня: ");

          StepLevel = decimal.Parse(str);

          str = ReadLine("Введите лот на уровень: ");

          lotLevel = decimal.Parse(str);

         // str = Console.ReadLine();

          WriteLine();   */
        #endregion
        //----------------------------------------------- End Comment-------------------------------------------------
      
             Console.ReadLine();

             
          
        }
        
       

        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Filds

        static List<Level> levels;

        static decimal priceUp;

        static int countLevels;
                
        static decimal lotLevel ;

        //  static decimal stepLevel;
        
        #endregion

         static Trade trade = new Trade();

        //----------------------------------------------- End Fields ----------------------------------------------------

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

                   levels = Level.CalculateLevels(priceUp, stepLevel, countLevels);
                }

            }
        }

        static decimal stepLevel;

        #endregion
        //----------------------------------------------- End Properties ------------------------------------------------




        //----------------------------------------------- Methods ---------------------------------------------------
        #region Methods
       
        static void WriteLine()
        {
            Console.WriteLine("Кол-во элементов в списке: " + levels.Count.ToString());
            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine(levels[i].PriceLevel);
            }
          //  Console.ReadLine();
          
        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine();
        }

        static void XXXXX()
        {
           //trade.Price = 2225;    
           //trade.Volume = 100;
       
           // Level level = new Level();

            
        }      

        #endregion
        //----------------------------------------------- End Methods ---------------------------------------------------
    }

    
}
