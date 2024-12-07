using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urok_1_5
{
    public class Trade
    {
        //============================================ Fields ==========================================

        #region Fields

        public decimal Price = 0;

        public string SecCode = ""; // название инструмента

        public string ClassCode = ""; // классификация биржы

        public DateTime DateTime = DateTime.MinValue;

        public string Portfolio = ""; // номер счета

        #endregion

        //=========================================== Properties ========================================

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

        #endregion

        //============================================ Methods ==========================================

        #region Methods



        #endregion


    }
}

