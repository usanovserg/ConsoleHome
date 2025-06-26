using System;

namespace ConsoleHome
{
    public interface IDateProvider : IDisposable
    {
        event EventHandler<TradeEventArgs> TradeReceived;
    }
}