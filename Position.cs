using System;
using System.Collections.Generic;
using System.Linq;
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

            timer.Interval = 2000;

            timer.Elapsed += NewTrade;

            timer.Start();

        }

        Random random = new Random();
        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            //throw new NotImplementedException();

            Trade trade = new Trade();

            int num = random.Next(-10, 10);

            string dir = "";
            if (num > 0)
            {
                dir = Trade.direction.Long.ToString();

            }
            else if (num < 0)
            {
                dir = Trade.direction.Short.ToString();

            }

            trade.Volume = Math.Abs(num);

            trade._price = random.Next(70000, 80000);

            string str = "Volume = " + trade.Volume.ToString() + " / Price = " + trade._price.ToString() + " / направление: " + dir;

            Console.WriteLine(str);

            


                
            //propfull
            
        }


    }
}
