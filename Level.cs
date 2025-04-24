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

        //================================================= Fields ======================================
        #region Fields

        /// <summary>
        /// цена уровня
        /// </summary>
        public decimal PriceLevel = 0;
        /// <summary>
        /// лот на уровень
        /// </summary>
        public static decimal LotLevel = 0;
        /// <summary>
        /// Открытый оъём на уровень
        /// </summary>
        public decimal Volume = 0;

        #endregion

        //============================================================= Methods ========================================
        #region Methods

        public static List<Level> CalculateLevels( decimal priceUp, decimal stepLevel, decimal priceDown)
        {
            List<Level> levels = new List<Level>();
            decimal priceLevel = priceUp;            
            int count = (int)((priceUp - priceDown) / stepLevel);  //надо притащит значение этих переменных
            for (int i = 0; i < count; i++)
            {
                Level level = new Level() { PriceLevel = priceLevel };
                levels?.Add(level);
                priceLevel -= stepLevel;
            }
            return levels;
        }

        #endregion
    }
}
