using System;
using System.Collections.Generic;
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

            timer.Interval = 5000;

            timer.Elapsed += NewTrade;

            timer.Start();


        }

        #region Properties

        public string Instrument {get; set;}

        public decimal LongPosition {get; set;}

        public decimal ShortPosition {get; set;}

        public decimal OpeningPrice {get; set;}

        public decimal CurrentPrice {get; set;}

        #endregion


        Random random = new Random();


        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();
            
            int num = random.Next(-10, 10);
            trade.Volume = Math.Abs(num);
            trade.Price = random.Next(70000, 80000);
            trade.DateTime = DateTime.Now;
            trade.SecCode = "SecCode";
            trade.ClassCode = "ClassCode";
            trade.Portfolio = "Portfolio";


            if (num > 0)
            {
                // сделка в лонг
                trade.Direction = TradeDirection.Long;
                LongPosition += trade.Volume;
                OpeningPrice = trade.Price;
                Instrument = trade.SecCode;

                Console.WriteLine($"Исполнена в лонг: Объем = {trade.Volume}, Цена = {trade.Price}");
            }
            else if (num < 0)
            {
                // сделка в шорт 
                trade.Direction = TradeDirection.Short;
                ShortPosition -= trade.Volume;
                OpeningPrice = trade.Price;
                Instrument = trade.SecCode;

                Console.WriteLine($"Исполнена в шорт: Объем = {trade.Volume}, Цена = {trade.Price}");
            }



            CurrentPrice = trade.Price;
            //string str = "Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString();

           // Console.WriteLine(str);
            Console.WriteLine($"Текущая позиция: Инструмент = {Instrument}, Long Position = {LongPosition}, ShortPosition = {ShortPosition}, Цена открытия = {OpeningPrice}, Текущая цена = {CurrentPrice}");
        }
    }
}
