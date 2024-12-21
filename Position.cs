using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;      // для СЕРГЕЯ почему здесь сделал такую запись если есть System.Timers

namespace ConsoleHome
{
    public class Position
    {
        public string Symbol = "WLD";
        public decimal openPraci = 0;
        public decimal openVolume = 0;
        

        

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
            Trade trade = new Trade();
            int num = random.Next(-10, 10);            
            trade.Volume = Math.Abs(num);
            trade.Price = random.Next(70000, 80000);
            trade.Symbol = Symbol;

            if (num > 0)
            {
                // Long
                
            }
            else if (num < 0)
            {
                // Short
            }           

            string str = "Volume =  " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString();

            Console.WriteLine(str); 
        }
    }
}
