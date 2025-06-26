using System;

namespace ConsoleHome
{
    public class TradeEventArgs : EventArgs
    {
        public Trade Trade { get; private set; }
        public TradeEventArgs(Trade trade)
        {
            Trade = trade;
        }
    }
}