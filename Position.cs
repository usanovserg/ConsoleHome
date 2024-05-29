using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    public class Position
    {
        public Position() 
        { 
            Timer timer = new();

            timer.Interval = 1000;

            timer.Elapsed += NewTrade;

            timer.Start();

        }
        Random random = new Random();
                
        static int _tradesCounter = 0;
        static decimal _position = 0;
        static decimal _averagePrice = 0;
        List<Trade> _trades= new List<Trade>();

        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();
          
            int num = random.Next(-10, 11);

            trade.DateTime = DateTime.Now;
            trade.Volume = Math.Abs(num);
            trade.Price = random.Next(70000, 80000);

            if (num > 0)
            {
                //Long deal
                trade.tradeDirection = TradeDirection.Long;
                _position += trade.Volume * trade.Price;
                
            }
            else if (num < 0)
            {
                //Short deal 
                trade.tradeDirection = TradeDirection.Short;
                _position -= trade.Volume * trade.Price;
            }
            else 
            {
                return; // нет сделки если random = 0
            }
            

            _trades.Add(trade);
            _tradesCounter = _trades.Count;


            string tradeInfo = $"DateTime: {trade.DateTime} Count = {_tradesCounter} Volume = {trade.Volume.ToString()}" +
                $" Price = {trade.Price.ToString()} Direction: {trade.tradeDirection}     Current _position: {_position} ";
            Console.WriteLine(tradeInfo);
          
        }
    }
}
