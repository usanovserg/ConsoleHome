using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;


namespace MyConsole
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

        private void NewTrade(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);

            if (num > 0)
            {
               //long            
            }

            else
            {
                //short
            }

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(70000, 80000);

            string str = "Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString();
            
            Console.WriteLine(str);
        }
    } 
}
