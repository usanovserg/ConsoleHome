using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using ConsoleHome.Enums;
using Timer = System.Timers.Timer;

namespace ConsoleHome.Entity
{

    public class Position
    {
        public delegate void PositionChangeDelegate(Position position, Trade trade);

        public event PositionChangeDelegate? PositionChangeDelegateNotify;

        public decimal sumPosition;

        public decimal pnlPosition;

        int tradeNumber;


        public Position()
        {
            Timer timer = new Timer();

            timer.Interval = 4000;

            timer.Elapsed += NewTrade;

            timer.Start();

        }

        Random random = new Random();

        public void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);

            if (num > 0)
            {
                // Покупка
                trade.TradeType = TradeType.Bye;

            }
            else if (num < 0)
            {
                // Продажа
                trade.TradeType = TradeType.Sell;

            }

            tradeNumber++;

            sumPosition += num;

            if (sumPosition > 0)
            {
                // Направление Лонг
                trade.Direction = Direction.Long;

            }
            else if (sumPosition < 0)
            {
                // Направление
                trade.Direction = Direction.Short;

            }

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(80000, 100000);

            trade.Commision = Math.Round(trade.Volume * trade.Price / 100000, 2);

            pnlPosition = Math.Round(trade.Price / trade.Volume / sumPosition, 2);

            trade.AccountNumber = "qwe123";

            string str = "Trade # " + tradeNumber + "\tDateTime: " + DateTime.Now + " Account: " + trade.AccountNumber +

                "\n\n\t\tTrade Type: " + trade.TradeType + " Volume = " + trade.Volume.ToString() + " Price = " + trade.Price.ToString() + " Commision = " + trade.Commision +

                //"\n\n\t\tPosition " + sumPosition + " PnL = " + pnlPosition + 

                "\n----------------------------------------------------";

            Console.WriteLine(str);

            PositionChangeDelegateNotify?.Invoke(this, trade);
        }
    }
}
