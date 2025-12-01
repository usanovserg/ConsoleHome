using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Level
    {
        /// <summary>
        /// Конструктор класса, используется для задания каких-либо значений или выполнить действие
        /// </summary>
        public Level()
        {

        }
        //================================================= Fields
        #region
        /// <summary>
        /// Цена уровня
        /// </summary>
        public decimal priceLevel = 0;

        /// <summary>
        /// Количество лотов на уровень
        /// </summary>
        public decimal lotLevel = 0;

        /// <summary>
        /// Открытый объем на уровне
        /// </summary>
        public decimal volume = 0;
        #endregion

        //================================================= Methods
        #region Methods
        public static List<Level> CalculateLevels(decimal priceUp, decimal step, int count)
        { 
            List<Level> levels = new List<Level>();

            decimal priceLevel = priceUp;

            for (int i = 0; i < count; i++)
            {
                Level level = new Level(); { level.priceLevel = priceLevel; }//Создаем новый объект Level

                levels.Add(level);

                priceLevel -= step;
            }

            return levels;
        }
        #endregion
    }
}
