using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    /// <summary>
    /// Класс, описывающий сделку
    /// </summary>
    public class Trade
    {
        #region Fields

        public DateTime DateTime    = DateTime.MinValue;
        public string   ClassCode   = "";
        public string   SecCode     = "";
        public string   SecName     = "";
        public string   BuySell     = "";
        public decimal  Price       = 0;
        public decimal  Qty         = 0;        
        public long     TransNumber = 0;
        public decimal  ComisRate   = 0;
        public string   Portfolio   = "";   // ?

        #endregion  //Fields


        #region Properties

        /// <summary>
        /// Объём сделки
        /// </summary>
        private decimal _volume = 0;
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
        #endregion  // Properties

        enum LongShort : byte
        {
            Long,
            Short
        }

        private int BuySelldfg()
        {
            return 0;
        }
    #region Methods
    #endregion  // Methods

}
}
