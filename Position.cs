using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleHome
{
    public class Position
    {
        public Position()
        {
            System.Timers.Timer timer = new System.Timers.Timer();

            timer.Interval = 2000;

            timer.Elapsed += NewTrade;

            timer.Start();


        }

        #region Properties

        public string Instrument { get; set; }
        public decimal CurrentPosition { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal CurrentPrice { get; set; }

        #endregion


        Random random = new Random();

        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int vol = random.Next(-10, 9);
            if (vol >= 0)
                ++vol;
            trade.Volume = Math.Abs(vol);
            trade.Price = random.Next(10000, 20000);
            trade.DateTime = DateTime.Now;
            trade.SecCode = "SecCode";
            trade.ClassCode = "ClassCode";
            trade.Portfolio = "Portfolio";
            trade.Direction = vol < 0 ? TradeDirection.Short : TradeDirection.Long;

            Instrument = trade.SecCode;
            CurrentPrice = trade.Price;

            // avg price
            if (vol * CurrentPosition == 0)
                // new position
                AvgPrice = CurrentPrice;
            else if (vol * CurrentPosition < 0)
                // overflow
                // todo add PnL calculations
                AvgPrice = CurrentPrice;
            else
                // same direction
                AvgPrice = Math.Round((AvgPrice * CurrentPosition + CurrentPrice * vol) / (CurrentPosition + vol));

            CurrentPosition += vol;

            if (vol > 0)
                Console.Write($"+Long : Объем = {trade.Volume}, \tЦена = {trade.Price}");
            else
                Console.Write($"+Short: Объем = {trade.Volume}, \tЦена = {trade.Price}");

            Console.WriteLine($"\tCurrent pos: {CurrentPosition}, \t Avg price: {AvgPrice}");
        }
    }
}
