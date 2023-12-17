using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static ConsoleHome.Trade;
using Timer = System.Timers.Timer;



namespace ConsoleHome
{  
    public class Position
    {
        public Position()
        {
            Timer timer = new Timer();

            timer.Interval = 1000;

            timer.Elapsed += NewTrade;

            timer.Start();

        }

        Random random = new Random();


        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade
            {
                //Accaunt = "MyAccaunt",

                SecCode = "CRUZ3",

                ClassCode = "SPBFUT",

                Price = random.Next(78000, 80000),

                DateTime = DateTime.Now,

            };

            int num = random.Next(-10, 10);

            if (num > 0) trade.TradeType = TradeType.Long;

            else if (num < 0) trade.TradeType = TradeType.Short;

            if (num != 0)
            {
                trade.Volume = Math.Abs(num);

                CalcPos(trade.Volume, trade.TradeType, trade.Price);

                string str = "Trade:  " + "{DateTime: " + trade.DateTime.ToString() + " / SecCode: " + trade.SecCode + " / ClassCode:" + trade.ClassCode + " / TradeType: " + trade.TradeType.ToString() + " / Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString() + "}";

                Console.WriteLine(str);

                Console.WriteLine($"SecCode: {trade.SecCode} / OpenPrice =  {OpenPrice} / Position = {Pos}\n");
            }
        }

        #region Fields

        decimal Pos = 0m;
        decimal OpenPrice = 0m;
        #endregion

        #region Methods

        void CalcPos(decimal volume, TradeType tradeType, decimal price)
        {
            if (Pos == 0) OpenPrice = price;

            if (tradeType == TradeType.Long)
            {
                if (Pos > 0) OpenPrice = Math.Round((OpenPrice * Pos + price * volume) / (Pos + volume));

                else if (Pos < 0)
                {
                    if (volume == Math.Abs(Pos)) OpenPrice = 0;

                    else if (volume > Math.Abs(Pos)) OpenPrice = price;
                }

                Pos += volume; 
            }
            else
            {
                if (Pos < 0) OpenPrice = Math.Round((OpenPrice * Math.Abs(Pos) + price * volume) / (Math.Abs(Pos) + volume));

                else if (Pos < 0)
                {
                    if (volume == Math.Abs(Pos)) OpenPrice = 0;

                    else if (volume > Math.Abs(Pos)) OpenPrice = price;
                }

                Pos -= volume;
            }

        }

        #endregion
    }
}
