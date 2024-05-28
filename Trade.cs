using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{

    public enum TradeDirection
    {
        Long,
        Short
    }

    public class Trade
    {
        #region Fileds

        public decimal Price = 0;
        public string SecCode = "";
        public string ClassCode = "";
        public DateTime DateTime = DateTime.Now;
        public string Portfolio = "";

        #endregion

        #region Properties

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

        public TradeDirection Direction { get; set; }

        #endregion
    }
}
