
using System;

namespace ConsoleHome.Model
{
    internal class Position
    {
        public PositionDirection direction;
        public decimal price;
        public decimal volume;
        public string SecCode = null;
        public string ClassCode = null;
        public string Portfolio = null;
        public DateTime CreateTime = DateTime.MinValue;
    }
}
