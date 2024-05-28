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

        public decimal Price = 0;
        public decimal Volume = 0;
        public string SecCode = "";
        public string ClassCode = "";
        public DateTime DateTime = DateTime.MinValue;
        public string Portfolio = "";
        public int tradeId;
        public TradeDirection tradeDirection;
              
        #endregion





    }
}
