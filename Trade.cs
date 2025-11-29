using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Trade
    {
        //=================================== Fields ===============================================
        #region Fields
        /// <summary>
        /// Цена инструмента
        /// </summary>
        public decimal Price = 0;
        public string SecCode = "";
        public string ClassCode = "";
        public DateTime DateTime = DateTime.MinValue;
        public string Portfolio = "";
        #endregion
        //=================================== Properties ===============================================
        #region Properties
        /// <summary>
        /// Объем сделки
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
        //=================================== Methods ===============================================
        #endregion

        #region methods

        #endregion
    }
}
