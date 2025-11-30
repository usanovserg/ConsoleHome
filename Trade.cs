using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Trade
    {
        #region Fields
        /// <summary>
        /// Цена инструмента
        /// </summary>
        public decimal Price = 0;
        public string SecCode = "";
        public string ClassCode = "";
        public DateTime DateTime = DateTime.MinValue;
        public string Portfolio = "";
        #endregion  //Fields


        #region Properties

        /// <summary>
        /// Объём сделки
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
        private decimal _volume = 0;

        #endregion  // Properties


        #region Methods


        #endregion  // Methods

    }
}
