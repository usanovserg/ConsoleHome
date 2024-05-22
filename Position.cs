using System;
using System.Timers;
using ConsoleHome.Enums;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    public class Position
    {

        public Position()
        {
            Timer timer = new Timer();

            timer.Interval = 2000;

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
                // Сделка в лонг
                trade.OrderType = OrderType.Long;

            }
            else if (num < 0)
            {
                // Сделка в шорт
                trade.OrderType = OrderType.Short;

            }

            trade.PositionVolume += num;

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(90000, 93000);

            trade.Commision = (double)(trade.Volume * trade.Price / 100000 );

            trade.AccountNumber = "qwe123";

            string str = "DateTime: " + DateTime.Now + "\tAccount: " + trade.AccountNumber + "\tPosition = " + trade.PositionVolume + "\tOrder Type: " + trade.OrderType + "\tVolume = " + trade.Volume.ToString() + "\tPrice = " + trade.Price.ToString() + "\tCommision = " + trade.Commision;

            Console.WriteLine(str);
        }
    }
}
