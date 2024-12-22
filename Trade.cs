using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Trade
    {
        //================================================= Fields ======================================
        #region Fields

        /// <summary>
        /// цена актива
        /// </summary>
        public decimal Price = 0;       
        /// <summary>
        /// название инструмента
        /// </summary>
        public string? SecCode = "";
        /// <summary>
        /// класиффикация биржи
        /// </summary>
        public string? ClassCode = "";
        DateTime DateTime = DateTime.MinValue;
        /// <summary>
        /// название портфеля
        /// </summary>
        public string? Portfolie = "";

        #endregion


        //====================================================== Properties ================================
        #region Properties
        /// <summary>
        /// объём сделки
        /// </summary>
        public decimal Volume
        {
            get
            {
                return _volume;
            }

            set
            {
                _volume = value;
            }
        }
        decimal _volume = 0;

        #endregion

    }
}
