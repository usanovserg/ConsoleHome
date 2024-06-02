using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleHome
{
    public class Position
    {
        private decimal openPrice;
        private decimal currentPrice;
        private int openLots;
        private string instrument;

        public Position(string instrument)
        {
            this.instrument = instrument;
            openLots = 0;
            openPrice = 0;
            currentPrice = 0;

            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private Random random = new Random();

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();
            int num = random.Next(-10, 10);

            if (num > 0)
            {
                trade.Direction = TradeDirection.Long;
            }
            else if (num < 0)
            {
                trade.Direction = TradeDirection.Short;
            }

            trade.Volume = Math.Abs(num);
            trade.Price = random.Next(70000, 80000);

            ProcessTrade(trade);

            string str = $"Volume = {trade.Volume} / Price = {trade.Price}/ num = {num} / Direction = {trade.Direction}";
            Console.WriteLine(str);
        }

        public void ProcessTrade(Trade trade)
        {
            if (trade.Direction == TradeDirection.Long)
            {
                openLots += (int)trade.Volume;
            }
            else if (trade.Direction == TradeDirection.Short)
            {
                openLots -= (int)trade.Volume;
            }

            currentPrice = trade.Price;

            if (openLots != 0)
            {
                openPrice = (openPrice * (openLots - (int)trade.Volume) + trade.Price * trade.Volume) / openLots;
                Math.Abs(openPrice);
            }
            else
            {
                openPrice = 0;
            }

            PrintPosition();
        }

        private void PrintPosition()
        {
            Console.WriteLine($"Instrument: {instrument}, Open Lots: {openLots}, Open Price: {openPrice}, Current Price: {currentPrice}");
        }
    }
}
