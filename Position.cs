using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    public class Position
    {
        public Position()
        {
            Timer timer = new Timer();

            timer.Interval = 5000;

            timer.Elapsed += NewTrade;

            timer.Start();
        }

        Random random = new Random();

        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);

            if (num > 0)
            {
                trade.OrderType = OrderType.Buy;
            }
            else if (num < 0)
            {
                trade.OrderType = OrderType.Sell;
            }

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(70000, 80000);

            string str = "Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString() + " / OrderType = " + trade.OrderType.ToString();

            Console.WriteLine(str);
        }
    }
}
