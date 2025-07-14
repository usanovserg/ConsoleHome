using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_урок1_3
{
    public class Trade
    {

        #region Filds 

        /// <summary>
        /// Цена инструмента
        /// </summary>

        public decimal Price = 0;

        public string SecCode = "";

        public string ClassCode = "";

        public DateTime DateTime = DateTime.MinValue;

        public string Portfolio = "";

        #endregion

        #region Propirtis
        /// <summary>
        /// Обьем сделки
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


    }

    #endregion
}
