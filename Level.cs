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

        //================================================= Fields ======================================
        #region Fields

        /// <summary>
        /// цена уровня
        /// </summary>
        public decimal PriceLevel = 0;
        /// <summary>
        /// лот на уровень
        /// </summary>
        public decimal LotLevel = 0;
        /// <summary>
        /// Открытый оъём на уровень
        /// </summary>
        public decimal Volume = 0;

        #endregion
    }
}
