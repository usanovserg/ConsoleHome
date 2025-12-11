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

        // =============================== Fields ================================
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
        /// Открытый объем на уровне
        /// </summary>
        public decimal VolumeLevel = 100;

        #endregion

        // =============================== Methods ================================
        #region Methods
        
        public static List<Level> CalculateLevels(decimal priceUp, decimal step, int levelCount)
        {
            List <Level> levels = new List <Level>();

            decimal priceStep = priceUp;
            Level.LotLevel = step;

            for (int i = 0; i < levelCount; i++)
            {
                Level level = new Level() { PriceLevel = priceStep };

                levels.Add(level);

                priceStep -= step;

                Console.WriteLine(levels[i].PriceLevel.ToString());
            }

            return levels;
        }
        
        #endregion
    }
}
