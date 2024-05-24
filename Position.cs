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
                // Long trade
            }
            else if (num < 0)
            {
                // Shor trade
            }

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(70000, 8000);

            string str = $"{DateTime.Now} Volume = {trade.Volume.ToString()} Price = {trade.Price.ToString()}";

            Console.WriteLine(str);
        }
    }
}
