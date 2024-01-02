using ConsoleHome.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    public class Position
    {
        public delegate void PriceChange(string message);//объявление делегата PriceChange
        public event PriceChange? PriceChangeEvents;// объявление события PriceChangeEvents

        public PositionTrend PositionTrend;
        public decimal PriceOpen = 0;
        public decimal PriceClose = 0;
        public decimal StopLoss = 0;
        public int volumeOpen = 0;
        public int volumeClose = 0;
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
                if (num > 0)
                {
                  PositionTrend = PositionTrend.Long;   // long
                }
                else if (num <0)
                {
                    PositionTrend = PositionTrend.Short;  //short
                }
                trade.Volume = Math.Abs(num);
            trade.Price = random.Next(70000, 80000);
            string str = "Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString() + "/ Направление сделки: "+ PositionTrend;
            Console.WriteLine(str);
            //вызов события PriceChangeEvents (проверка на null)
            PriceChangeEvents?.Invoke($"New Price = {trade.Price}");

        }
        

    }
    
}
