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


        //======================================== Fields ============================================
        #region Fields

        /// <summary>
        /// Price level
        /// </summary>
        public decimal PriceLevel = 0;

        /// <summary>
        /// Lot for level
        /// </summary>
        public static decimal LotLevel = 0;


        /// <summary>
        /// Opened volume on level
        /// </summary>
        public decimal Volume = 100;

        #endregion


        //======================================== Methods ============================================
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
