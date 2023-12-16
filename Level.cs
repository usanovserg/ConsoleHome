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
            //конструктор, например прочитать данные из файла
        }

        #region ================================================Fields===================================================

        /// <summary>
        /// Цена уровня
        /// </summary>
        public decimal PriceLevel = 0;

        /// <summary>
        /// Рабочий объем (Лот), который будет выставляться на уровень - общий для всех уровней - объектов класса 
        /// </summary>
        public static decimal LotLevel = 10;

        /// <summary>
        /// Открытая позиция на уровне (фактически открытый объём на уровне)
        /// </summary>
        public decimal Volume = 0;

        #endregion

        #region ================================================Methods===================================================
        /// <summary>
        /// Формирование списка ценовых уровней
        /// </summary>
        /// <param name="priceUp"></param>
        /// <param name="priceDown"></param>
        /// <param name="step"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Level> CalculateLevels(decimal priceUp, decimal priceDown, decimal step, int count)
        {
            List<Level> levels = new List<Level>();

            decimal priceLevel = priceUp;

            for (int i = 0; i < count; i++)
            {
                Level level = new Level() { PriceLevel = (priceLevel > priceDown) ? priceLevel : priceDown }; //?: - "сахарная" проверка достижения нижнего уровня

                levels.Add(level);

                priceLevel -= step;
            }

            return levels;
        }

        #endregion
    }
}
