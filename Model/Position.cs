
using System;

namespace ConsoleHome.Model
{
    internal class Position
    {
        public PositionDirection direction;
        public decimal price;
        public decimal volume;
        public string secCode = null;
        public string classCode = null;
        public string portfolio = null;
        public decimal takeProfitPrice;
        public decimal stopLossPrice;
        public DateTime openTime = DateTime.MinValue;
        public DateTime closeTime = DateTime.MinValue;

    }
}
