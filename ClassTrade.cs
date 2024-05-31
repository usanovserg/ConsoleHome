using System;

namespace ConsoleHome
{
    public class ClassTrade
    {
        public ClassExchange ExchangeName { get; set; }

        public UInt16 TradingAccount { get; set; }

        public ClassSecurity SecurityIndex { get; set; }

        public ClassTypeOrder TypeOrderTrade { get; set; }

        public Decimal Volume { get; set; }

        public Decimal Price { get; set; }
    }
}
