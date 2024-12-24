using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
            Trade trade = new Trade();
            int num = random.Next(-10, 10);
            if (num > 0)
            {
              

            }
            else if (num < 0)
            {

            }
            trade.Volume = Math.Abs(num);
            trade.Price = random.Next(70000, 800000);
            string str = "Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString();      
            
            Console.WriteLine();            
        }
        public enum Trades: byte
        {
            Long,
            Short
        }


    }
}
