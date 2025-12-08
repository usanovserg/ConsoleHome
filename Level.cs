using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ConsoleHome.Enums;

namespace ConsoleHome
{
    public class Level
    {
        public Level() 
        { 

        }
        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Fields
        /// <summary>Цена уровня</summary>
        public decimal PriceLevel = 0;

        /// <summary>Лотов на уровень/summary>
        public static decimal LotForLevel = 0;

        /// <summary>Объём уровня</summary>
        public decimal VolumeLevel = 0;

        //--------------------------------------------- Fields end -------------------------------------------------- 
        #endregion

        //----------------------------------------------- Methods ------------------------------------------------
        #region Methods
        public static List<Level> CalculateLevels(decimal priceUp, decimal step, int count)
        {
            List<Level> levels = new List<Level>();

            decimal priceLevel = priceUp;

            for (int i = 0; i < count; i++)
            {
                Level level = new Level();
                level.PriceLevel = priceLevel;
                // Level level = new Level() {PriceLevel = priceLevel}; // второй вариант

                levels.Add(level);

                priceLevel -= step;
            }

            return levels;
        }

        //--------------------------------------------- Methods end ---------------------------------------------- 
            #endregion

    }
}
