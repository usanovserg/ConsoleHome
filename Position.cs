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

        Random random = new Random();

        private void NewTrade(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);

            trade.Volume = Math.Abs(num);
            
            trade.Price = random.Next(70000, 80000);

            if (num > 0)
            {
                Trade.Balance += trade.Volume;
            }
            else if (num < 0)
            { 
                Trade.Balance -= trade.Volume;
            }

            string str = "Volume = " + trade.Volume.ToString() + " | Price = " + trade.Price.ToString() + " | Position = " + Trade.Balance.ToString() + " | Date -  " + trade.DateTime;
            
            Console.WriteLine(str);    

        }
    }
}
