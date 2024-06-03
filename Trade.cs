using System;

namespace ConsoleHome
{
    public class Trade
    {
        #region Fields
        public decimal Price = 0;
        public string SecCode = "";
        public string ClassCode = "";
        public DateTime DateTime = DateTime.MinValue;
        public string Portfolio = "";
        #endregion

        #region Properties
        public decimal Volume { get; set; } = 0;

        public TradeDirection Direction { get; set; }
        #endregion
    }
}
