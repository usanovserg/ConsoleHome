using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleHome.Enums;

namespace ConsoleHome.Entities
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
        public decimal EntryPrice = 0;
        public TradeDirection tradeDirection;

        #endregion





    }
}
