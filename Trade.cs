using System;
using System.Security.Authentication;

namespace ConsoleHome
{
    public class Trade
    {
        public enum Direction
        {
            Long,
            Short
        }

        public Exchange ExchangeName { get; set; }

        public UInt32 TradingAccount { get; set; }

        public TypeOrder TypeOrderTrade { get; set; }

        public Decimal Volume { get; set; }

        public Decimal Price { get; set; }



    }
}
