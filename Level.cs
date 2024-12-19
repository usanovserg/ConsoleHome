using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Level
    {
        public Level()   //можно сделать по другому, не используя конструктор
        {

           

        }
        



        //============================================================= Fields =======================================
        #region Fields

        /// <summary>
        /// цена уровня
        /// </summary>
        public decimal PriceLevel = 0;

        /// <summary>
        /// Лот на уровень
        /// </summary>
        public static decimal LotLevel = 0;

        /// <summary>
        /// Открытый оъём на уровне
        /// </summary>
        public decimal Volume = 100;

        #endregion


        //============================================================= Methods =======================================
        #region Methods

        public static List<Level> CalculatedLevels(decimal priceUp, decimal stepLevel, int count)
        {
            List<Level> levels = new List<Level>();

            decimal priceLevel = priceUp;                     

            if (levels != null)
            {
                for (decimal i = 0; i < count; i++)
                {
                    Level level = new Level() { PriceLevel = priceLevel };

                    levels.Add(level);

                    priceLevel -= stepLevel;
                }

                return levels;
            }
        }
    }


        #endregion

    

}
