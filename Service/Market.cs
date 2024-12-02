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
            
            Trade trade = strategy.Trade(currentPrice, priceDelta);
            UpatePosition(trade);
            
            currentPrice += priceDelta;

        }

        private void UpatePosition(Trade trade)
        {
            if (trade == null)
            {
                return;
            }
            if (position == null)
            {
                position = new Position();
            }
            position.price = trade.price;
            position.volume += trade.volume;
            if (position.volume > 0)
            {
                position.direction = PositionDirection.Long;
            }
            else if (position.volume < 0)
            {
                position.direction = PositionDirection.Short;
            } else
            {
                position = null;
            }
            Console.WriteLine($"Position: {position?.volume} lots at price {position?.price}");
        }
    }
}
