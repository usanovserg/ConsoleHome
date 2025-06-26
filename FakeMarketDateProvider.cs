using System.Timers;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    public sealed class FakeMarketDateProvider : IDateProvider
    {
        public event EventHandler<TradeEventArgs> TradeReceived;

        private readonly Random _random = new Random();
        private void RaiseTradeReceived(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            var secList = new[]  { "RiU5", "SiU5", "CRU5", "USDRUBF"};
            var operation = _random.Next(0, 2) == 0 ? Operation.Buy : Operation.Sell;
            var trade = new Trade
            {
                Id = Guid.NewGuid().ToString(),
                Symbol = secList[_random.Next(secList.Length)],
                Operation = operation,
                Volume = (uint)_random.Next(1, 10),
                Price = _random.Next(70000, 80000),
                Time = DateTime.Now
            };
            var tradeEventArgs = new TradeEventArgs(trade);
            TradeReceived?.Invoke(sender, tradeEventArgs);
        }
        
        private const int Interval = 3000; 
        private readonly Timer _timer = new Timer(Interval);
        private bool _disposed;

        public FakeMarketDateProvider()
        {
            _timer.Elapsed += RaiseTradeReceived;
            _timer.Start();
        }

        ~FakeMarketDateProvider()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                if (_timer != null)
                {
                    _timer.Elapsed -= RaiseTradeReceived;
                    _timer.Stop();
                    _timer.Dispose();
                }
            }
            _disposed = true;
        }
    }
}