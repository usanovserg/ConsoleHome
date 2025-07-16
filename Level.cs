using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Level
    {
        #region Fields    //----------------------------------------------------------------------//
        /// <summary>
        /// цена уровня
        /// </summary>
        public decimal PriceLevel = 0;
        /// <summary>
        /// открытый обьем лотов  на уровне
        /// </summary>
        public decimal VolumeLevel = 0;
        /// <summary>
        /// планируемый объем лотов на уровне
        /// </summary>
        public decimal LotLevel = 0;

        #endregion
        #region Properties //----------------------------------------------------------------------//

        #endregion

        #region Methods    //----------------------------------------------------------------------//   
        public Level(decimal price) 
        {
            PriceLevel = price;
        }

        public static List<Level> GetLevels(decimal priceHi, decimal priceLow, decimal priceStep)
        {
            List<Level> LevelList = new List<Level>();
            for (decimal priceLevel = priceHi; priceLevel >= priceLow; priceLevel -= priceStep)
                LevelList.Add(new Level(priceLevel));
            return LevelList;
        }
        #endregion
    }
}
