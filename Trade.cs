using ConsoleHome.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Trade
    {
        #region =======================================Fields=====================================================
        public int Volume = 0;

        public decimal Price = 0;

        public DateTime DateTime = DateTime.MinValue;

        public TradeDirection tradeDirection;

        // string ClientCode = "";

        // string Portfolio = "";

        // string SecCode = "";
        #endregion

    }
}
