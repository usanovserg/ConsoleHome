﻿using ConsoleHome.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    public class Position
    {
        public Position()
        {
            Timer timer = new Timer();

            timer.Interval = 5000;

            timer.Elapsed += NewTrade;

            timer.Start();
        }

        public string Account = "   ";
        public string SecCode = "   ";
        public decimal Price = 0;
        int CurrentPosition = 0;

        public TradeDirection deal = TradeDirection.Buy;


        Random random = new Random();

        private void NewTrade(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);

            trade.Volume = Math.Abs(num);
            
            trade.Price = random.Next(70000, 80000);

            trade.DateTime = DateTime.Now;

            if (num > 0)
            {
                CurrentPosition += trade.Volume;
                deal = TradeDirection.Buy;
            }
            else if (num < 0)
            { 
                CurrentPosition -= trade.Volume;
                deal = TradeDirection.Sell;
            }

            string str = "Volume = " + trade.Volume.ToString() + " | Price = " + trade.Price.ToString() + " | Position = " + CurrentPosition.ToString() + " | Date -  " + trade.DateTime +" | " + deal;
            
            Console.WriteLine(str);    

        }
    }
}
