using System;
using System.Security.Permissions;

namespace ConsoleHome
{
    public class Trade
    {
        public Exchange ExchangeName { get; set; }

        public UInt16 TradingAccount { get; set; }

        public Security SecurityIndex { get; set; }

        public TypeOrder TypeOrderTrade { get; set; }

        public Decimal Volume { get; set; }

        public Decimal Price { get; set; }
    }
}
