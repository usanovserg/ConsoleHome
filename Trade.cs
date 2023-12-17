using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Trade
    {

        public string Accaunt = "";

        public string SecCode = "";

        public string ClassCode = "";

        public decimal Price = 0;

        public DateTime DateTime = DateTime.MinValue;

        public TradeType TradeType;
        public decimal Volume { get; set; }


    }
    public enum TradeType : byte
    {
        Long,
        Short
    }
}
