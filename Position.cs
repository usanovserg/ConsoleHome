using System;
using System.Timers;
using ConsoleHome.Enums;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    public class Position
    {

        decimal sumPosition, pnlPosition;

        public Position()
        {
            Timer timer = new Timer();

            timer.Interval = 3000;

            timer.Elapsed += NewTrade;

            timer.Start();

            sumPosition = new decimal();

            pnlPosition = new decimal();

        }

        Random random = new Random();

        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);

            if (num >= 0)
            {
                // Сделка в лонг
                trade.OrderType = OrderType.Long;

            }
            else if (num < 0)
            {
                // Сделка в шорт
                trade.OrderType = OrderType.Short;

            }

            sumPosition += num;

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(90000, 93000);

            trade.Commision = ((double)(trade.Volume * trade.Price / 100000));

            pnlPosition = (int)(trade.Price / trade.Volume);

            trade.AccountNumber = "qwe123";

            string str = "DateTime: " + DateTime.Now + " Account: " + trade.AccountNumber + "\tPosition = " + sumPosition + " PnL = " + pnlPosition
                + "\tOrder Type: " + trade.OrderType + " Volume = " + trade.Volume.ToString() + "\tPrice = " + trade.Price.ToString() + " Commision = " + trade.Commision + "\n";

            Console.WriteLine(str);
        }
    }
}
