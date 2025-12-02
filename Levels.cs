using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    public class Levels
    {
        public Levels() 
        {
  
        }

        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Fields

        /// <summary>
        /// Цена уровня
        /// </summary>
        public decimal PriceLevel = 0;

        /// <summary>
        /// Кол-во лотов на уровне
        /// </summary>
        public static decimal LotLevel = 0;

        /// <summary>
        /// Открытый объем на уровне
        /// </summary>
        public decimal OpenVolumeLevel = 0;

        #endregion
        //-----------------------------------------------------------------------------------------------------------

        #region Methods

        public static void CalculateLevels(decimal priceUp, decimal step, int count)
        {
            List<Levels> levels = new List<Levels>();
            
            decimal priceLevel = priceUp;

            for (int i = 0; i < count; i++)
            {
                Levels level = new Levels() { PriceLevel = priceLevel };

                levels.Add(level);

                priceLevel -= step;
            }

        }

        #endregion

    }




}
