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
        //----------------------------------------------- Fields ------------------------------
        #region Fields
        /// <summary>
        /// Цена уровня сетки
        /// </summary>
        public decimal PriceLevel = 0;
        /// <summary>
        /// Кол-во лотов на уровне сетки
        /// </summary>
        public static decimal LotLevel = 0;
        /// <summary>
        /// Открытый объем на уровне сетки
        /// </summary>
        public decimal VolumeLevel = 0;

        #endregion
        //----------------------------------------------- Fields ------------------------------

        //----------------------------------------------- Methods ------------------------------
        #region Methods

        public static List<Level> CalculateLevels(decimal priceUp, decimal step, int count)
        {
            List<Level> levels = new List<Level>();

            decimal priceLevel = priceUp;

            for (int i = 0; i < count; i++)
            {
                Level level = new Level() { PriceLevel = priceLevel };

                // Альтернативный вариант присвоения level.PriceLevel = priceLevel;

                levels.Add(level);

                priceLevel -= step;
            }

            return levels;
        }

        #endregion
        //----------------------------------------------- Methods ------------------------------
    }
}
