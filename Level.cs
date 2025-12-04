using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Level
    {
        public Level(decimal price) //Создан конструктор Level в классе Level
        {
            PriceLevel = price;
        }


        //======================================== Fields =============================================
        #region Fields
        /// <summary>
        /// Цена уровня
        /// </summary>
        public decimal PriceLevel = 0;

        /// <summary>
        /// Лот на уровень. ОН может быть дробным (например, крипта).
        /// </summary>
        public decimal LotLevel = 0;

        /// <summary>
        /// Открытй объем на уровне
        /// </summary>
        public decimal Volume = 100;
        #endregion
    }
}
