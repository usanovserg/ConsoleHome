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
        //=================================== Fields ===============================================
        #region Fields
        public decimal PriceLavel = 0;
        public static decimal LotLevel = 0;
        public decimal Volume = 0;
        #endregion
        //=================================== Methods ===============================================
        #region Methods
        public static List<Level> CalculateLevels(decimal priceDown, decimal priceUp, decimal step)
        {
            List<Level> levels = new List<Level>();
            for (decimal price = priceUp; price >= priceDown; price -= step)
            {
                levels.Add(new Level() { PriceLavel = price });
            }
            return levels;
        }
        #endregion
    }
}
