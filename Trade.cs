using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ConsoleHome.Enums;

namespace ConsoleHome
{
    public class Trade
    {
        public TypeOrder TypeOrderTrade { get; set; }
        public decimal Volume { get; set; }

        public double Commision { get; set; }

        public Exchange ExchangeName { get; set; }

        public int AccountNumber { get; set; }
    }
}















