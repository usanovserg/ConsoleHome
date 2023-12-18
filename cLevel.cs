using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class cLevel
    { 
        #region Fields
        public decimal priceLevel = 0;
        public decimal lotLevel = 0;
        public decimal volumeLevel = 0;
        #endregion

        //=============Methods=========\\
        #region Methods
        public static List<cLevel> CalculateLevels(decimal priceUp, decimal priceDown, decimal priceStep) 
        { 
            List<cLevel> levels = new List<cLevel>();
            int countLevels = (int)((priceUp - priceDown) / priceStep) + 1;
            decimal curPrLevel = priceUp;

            for (int i = 0; i < countLevels; i++) 
            {
                cLevel level = new cLevel() {priceLevel = curPrLevel};
                levels.Add(level);
                curPrLevel -= priceStep;
            }

            return levels; 
        }
        #endregion



    }
}
