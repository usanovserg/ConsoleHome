using System;

namespace ConsoleHome
{
    public class Trade
    {
        public Exchange ExchangeName { get; set; }

        public UInt32 TraderAccount { get; set; }

        public DateTime DateTime { get; set; }

        public SecurityClass SecurityClass { get; set; }

        public Security SecurityCode { get; set; }

        public TypeOrder TypeOrderTrade { get; set; }

        public Decimal Volume { get; set; }

        public Decimal Price { get; set; }
    }
}
