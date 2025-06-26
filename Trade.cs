using System;

namespace ConsoleHome
{
    public class Trade
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public uint Volume { get; set; }
        public int Price { get; set; }
        public Operation Operation { get; set; }
        public DateTime Time { get; set; }
    }
}