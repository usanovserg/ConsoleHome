using System;
using System.Timers;
using ConsoleHome.Enums;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    public class Position
    {

        decimal sumPosition, pnlPosition;

        int tradeNumber;

        public Position()
        {
            Timer timer = new Timer();

            timer.Interval = 3000;

            timer.Elapsed += NewTrade;

            timer.Start();

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

            tradeNumber ++;

            sumPosition += num;

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(80000, 100000);

            trade.Commision = Math.Round((trade.Volume * trade.Price / 100000), 2);

            pnlPosition = Math.Round(trade.Price / trade.Volume / sumPosition, 2);

            trade.AccountNumber = "qwe123";

            string str = "Сделка № " + tradeNumber + " DateTime: " + DateTime.Now + " Account: " + trade.AccountNumber + "\tPosition = " + sumPosition + " PnL = " + pnlPosition
                + "\tOrder Type: " + trade.OrderType + " Volume = " + trade.Volume.ToString() + "\tPrice = " + trade.Price.ToString() + " Commision = " + trade.Commision + "\n";

            Console.WriteLine(str);
        }
    }
}
