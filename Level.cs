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
        /// цена уровня
        /// </summary>
        public decimal PriceLevel = 0;

        /// <summary>
        /// Лот на урвоень
        /// </summary>
        public decimal LotLevel = 0;

        /// <summary>
        /// открытый объем на уровне
        /// </summary>
        public decimal Volume = 0;


        #region Methods 
        public static List<Level> CalculateLevels(decimal priceUp, decimal step, int count)
        {
            List<Level> levels = new List<Level>();
            decimal priceLevel = priceUp;

            for (int i = 0; i < count; i++)
            {
                Level level = new Level() { PriceLevel = priceLevel };
                levels.Add(level); 
                priceLevel -= step;
            }

            return levels;  
        }
        #endregion
    }
}
