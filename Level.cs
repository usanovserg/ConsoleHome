using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Console_урок1_3
{
    public class Level
    {

        public Level()
        {
          
           

        }

        //=============================================Filds============================ 

        #region Filds

        /// <summary>
        /// Цена уровня
        /// </summary>
        public decimal PriceLevel = 0;

        /// <summary>
        /// Лот на уровень
        /// </summary>
        public decimal LotLevel = 0;

        /// <summary>
        /// Открытый обьем на уровне
        /// </summary>
        public decimal Volume = 100;

        #endregion

        //=============================================Metods============================ 

        #region Metods

        public static List<Level> ColculateLevel(decimal priseUp, decimal step, int count) 
        { 
        List<Level> levels = new List<Level>();

            decimal priceLevel = priseUp;


           /* for (int i = 0; i < count; i++)
            {
                Level = new Level() { PriceLevel = priceLevel };


                levels.Add(Level);

                priceLevel -= step;
            }

           */ return levels;  
        }

        #endregion


    }



}
