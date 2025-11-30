using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Level
    {

        //public Level(decimal price) { PriceLevel = price; }

        public Level() { }
        
        //----------------------------------------------- Fields ---------------------------------------------------- 

        #region Fields

        public decimal PriceLevel = 0;

        public decimal LotLevel = 0;
        
        public decimal Volume = 0;

        #endregion

        //----------------------------------------------- Methods ---------------------------------------------------

        #region Methods

        public static List<Level> CalculateLevels(decimal PriceUp, decimal step, int count) 
        { 
            List<Level> levels = new List<Level>();        
            
            decimal priceLevel = PriceUp;

            for (int i=0; i<count;i++) { 
                Level level = new Level() { PriceLevel = priceLevel };
                levels.Add(level);
                priceLevel-=step;
            
            }


            return levels;
        }



        #endregion

    }
}
