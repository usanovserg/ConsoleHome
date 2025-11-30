using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Level
    {
        public Level() 
        { 
            
        }
        
        
        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Fields

        /// <summary>
        /// Цена уровня
        /// </summary>
        public decimal PriceLevel = 0;

        /// <summary>
        /// Лот на уровень
        /// </summary>
        public static decimal LotLevel = 0;

        /// <summary>
        /// Открытый объём на уровне
        /// </summary>
        public decimal Volume = 100;

        #endregion //-- Fields --

        //----------------------------------------------- Methods ---------------------------------------------------
        #region Methods

        public static List<Level> CalculateLevels(decimal priceUp, decimal step, int count)
        {
            List<Level> levels = new List<Level>();

            decimal priceLevel = priceUp;
            
            for (int i = 0; i < count; i++)
            {
                //способ 1 в одну строку
                Level level = new Level() { PriceLevel = priceLevel };

                //способ 2 в две строки
                //Level level = new Level();
                //level.PriceLevel = priceLevel;

                levels.Add(level);

                priceLevel -= step;
            }

            return levels;
        }


        #endregion // Methods

    }
}
