
using System;

namespace ConsoleHome.Model
{
    internal class Position
    {
        public delegate void UpdatePosition(decimal price, decimal volume);
        public event UpdatePosition? PositionUpdated;

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

        public decimal Volume
        {
            get => volume;
            set{
                volume = value;
                if (PositionUpdated != null)
                {
                    PositionUpdated(price, volume);
                }
            }
        }
    }
}
