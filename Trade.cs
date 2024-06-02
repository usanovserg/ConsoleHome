using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Trade
    {
        //---------------------------------------------- Filders ------------------------------------------------
        #region  = Filders =

        /// <summary>
        /// Цена интрумента
        /// </summary>
        public decimal Price = new Random().Next(7000, 8000);
        /// <summary>
        /// 
        /// </summary>
        public string SecCode = "";
        /// <summary>
        /// 
        /// </summary>
        public string ClassCode = "";
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateTime = DateTime.MinValue;
        /// <summary>
        /// 
        /// </summary>
        public int Portfolio = 010524;

        public string Instrument = "USDТ";

        #endregion
        //----------------------------------------------          ------------------------------------------------
        #region
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
        public decimal _volume = 0;

        internal static void Add(Trade trade)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}