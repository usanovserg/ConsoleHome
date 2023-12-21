using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Level
    {

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
        public decimal Volume = 100;

        public static List<Level> CalculateLevels(decimal priceUp, decimal stepLevel, decimal count)
        {
            List<Level> levels = new List<Level>();

            decimal priceLevel = priceUp;

            for (int i = 0; i < count; i++)
            {
                Level level = new Level();

                level.PriceLevel = priceLevel;

                levels.Add(level);

                priceLevel -= stepLevel;
            }

            return levels;

        }
    }
    
}
