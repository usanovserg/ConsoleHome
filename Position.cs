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
            Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += NewTrade;
            timer.Start();
            Console.ReadKey();
            timer.Stop();
        }

        Random random = new Random();

        private void NewTrade(object? sender, ElapsedEventArgs e)      // это само создалось при заполнении timer.Elapsed += Timer_Elapsed;
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);

            if (num > 0)
            {
                // сделка в лонг
            }
            else if (num < 0) 
            {
                // сделка в шорт
            }

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(70000, 80000);

            string str = "Volume = " + trade.Volume.ToString() + "\tPrice = " + trade.Price.ToString();
            Console.WriteLine(str);

        }
    }
}
