using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTrainingBot
{
    public class Levels
    {
        /// <summary>
        /// Цена уровня
        /// </summary>
        public decimal price_level = 0;

        /// <summary>
        /// Лот на уровень
        /// </summary>
        static decimal lot_level_ = 0;

        /// <summary>
        /// Открытый объем
        /// </summary>
        decimal open_volume_ = 0;

        /// <summary>
        /// возвращает значение лота на уровень
        /// </summary>
        public decimal lot_level
        {
            get { return lot_level_; }
        }
        public decimal open_volume
        {
            get { return open_volume_; }
        }
        protected void SetLotLevel (decimal lot_level_geted)
        {
            lot_level_ = lot_level_geted;
        }

    }
}
