using System;

namespace ConsoleHome
{
    public class ClassTrade
    {
        public ClassExchange ExchangeName { get; set; }

        public UInt32 TraderAccount { get; set; }

        public DateTime DateTime { get; set; }

        public ClassSecurityClass SecurityClass { get; set; }

        public ClassSecurity SecurityCode { get; set; }

        public ClassTypeOrder TypeOrderTrade { get; set; }

        public Decimal Volume { get; set; }

        public Decimal Price { get; set; }
    }
}
