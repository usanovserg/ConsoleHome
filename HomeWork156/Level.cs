using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork156
{
    public class Level
    {

        
        
        //======================================fields====================================
        #region fields

        /// <summary>
        /// Цена уровня
        /// </summary>
        public decimal PriceLevel=0;

        /// <summary>
        /// Лот на уровень
        /// </summary>
        public decimal LotLevel=0;

        /// <summary>
        /// Открытый обьем на уровень
        /// </summary>
        public decimal Volume=0;

        #endregion

        //======================================methods====================================

        #region methods
        public static List<Level> CalculateLevels(decimal priceUp, decimal priceDown, decimal step)
        {
            List<Level> levels = new List<Level>();

            int contLevels;
            decimal priceLevel = priceUp;

            contLevels = Convert.ToInt32(decimal.Round((priceUp - priceDown)/step));

            for (int i = 0; i < contLevels; i++)
            {
                Level level = new Level();
                level.PriceLevel = priceLevel;
                levels.Add(level);
                priceLevel -= step;

            }
            return levels;
        }


        #endregion

    }
}
