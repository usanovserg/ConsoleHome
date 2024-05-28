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
        /// Level price
        /// </summary>
        public decimal Price = 0;
        #endregion


        //======================================== Methods ============================================
        #region Methods

        public static List<Level> CalculateLevels(decimal price, decimal step, int count) 
        {
            List<Level> levels = new List<Level>();

            for (int i = 0; i < count; i++)
            {
                Level level = new Level() { Price = price };
                levels.Add(level);

                price -= step;
            }
            return levels;
        }
        #endregion
    }
}
