using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static MyConsole.Trade;
using Timer = System.Timers.Timer;

namespace MyConsole
{
    public class Position
    {
        public Position() 
        {
            Timer timer = new Timer();

            timer.Interval = 1000;

            timer.Elapsed += Timer_Elapsed;

            timer.Start();
        }

        Random random = new Random();   
        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade(); // Что делает эта строка?
            DealDirection dealDirection = new DealDirection(); // Что делает эта строка?

            int num = random.Next(-10, 10);

            if (num > 0)
            {
                // Сделка в лонг
                dealDirection = DealDirection.Long;
            }
            else if (num < 0) 
            {
                // Сделка в шорт
                dealDirection = DealDirection.Short;
            }

            trade.Volume = Math.Abs(num);

            trade.price = random.Next(7000, 10000);

            string str = "Volume = " + trade.Volume.ToString() + " / Price = " + trade.price.ToString() + " / dealDirection = " + dealDirection.ToString();

            Console.WriteLine(str);
        }
    }
}
