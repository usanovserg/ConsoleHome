using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    public class Level
    {
        // Вариант присвоения значения через конструктор
        public Level() 
        {
            
        }  
        //============================== Felds =====================================   /

        #region Felds

        /// <summary>
        /// Цена уровня
        /// </summary>
        public decimal PriceLevel = 0;

        /// <summary>
        /// Лот на уровень
        /// </summary>
        public static decimal LotLevel = 0;

        /// <summary>
        /// Открытый объем на уровне
        /// </summary>
        public decimal VolumeLevel = 100;

        #endregion

        // ============================== Methods =====================================   */

        #region Methods
        /// <summary>
        /// Создание уровней
        /// </summary>
        /// <param name="priceUp"></param>
        /// <param name="step"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Level> CalculateLevels(decimal priceUp, decimal step, int count)
        {
            List<Level> levels = new List<Level>();

            decimal priceLevel = priceUp;

            Level.LotLevel = step;

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
