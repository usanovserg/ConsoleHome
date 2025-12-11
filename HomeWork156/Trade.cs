using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork156
{
    public class Trade
    {

        public Trade()
            { DateTime = DateTime.Now; }

        //=====================================Fields=========================================
        #region Fields
        /// <summary>
        /// Цена иструмента
        /// </summary>
        public decimal Price = 0;

        public string SetCode = "";
        public string ClassCode = "";
        public DateTime DateTime= DateTime.MinValue;
        public string Portfolio = "";
        public enum Direct
        {
            Long,
            Short
        }
        public Direct Direction = Direct.Long;
        #endregion

        //=====================================Property========================================
        #region Property
        /// <summary>
        /// Обьем сделки
        /// </summary>
        private decimal _volume=0;

        public decimal Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        #endregion

    }
}
