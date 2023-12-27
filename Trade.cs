using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Trade
    {
        public Trade() 
        {
        
        }

        public enum TradeDirection : byte
        { 
            Buy = 1,
            Sell
        }

        public decimal Volume = 0;

        public decimal Price = 0;

        public static decimal Balance = 0;

        // string SecCode = "";

        public DateTime DateTime = DateTime.Now;

        // string ClientCode = "";

        // string Portfolio = "";
    }
}
