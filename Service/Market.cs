using ConsoleHome.Model;
using System;
using System.Timers;

namespace ConsoleHome.Service
{
    public class Market
    {
        #region Fields
        
        private Timer timer;
        private Random random;
        private IStrategy strategy;
        private decimal currentPrice;
        private Position position;

        #endregion

        public void Start(decimal startPrice, IStrategy strategy)
        {
            this.currentPrice = startPrice;
            this.strategy = strategy;
            this.random = new Random();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += OnMarketTick;
            timer.Start();
        }

        private void OnMarketTick(object sender, ElapsedEventArgs e)
        {
            int priceDelta = random.Next(-100, 100);
            
            Order trade = strategy.Trade(currentPrice, currentPrice + priceDelta);
            UpatePosition(trade);
            
            currentPrice += priceDelta;

        }

        private void UpatePosition(Order order)
        {
            if (order == null)
            {
                return;
            }
            if (position == null)
            {
                position = new Position();
                position.openTime = DateTime.Now;
                Console.WriteLine($"Position opened at {position.openTime}");
            }
            position.price = order.price;
            position.volume += order.volume;
            position.direction = position.volume > 0 ? PositionDirection.Long : PositionDirection.Short;
            position.secCode = order.secCode;
            if (position.volume == 0)
            {
                position.closeTime = DateTime.Now;
                Console.WriteLine($"Position closed at {position.closeTime}");
                position = null;
            }
            else
            {
                Console.WriteLine($"SecCode: {position.secCode}; Price: {position.price}; Volume: {position.volume}; Direction: {position.direction}");
            }
        }
    }
}
