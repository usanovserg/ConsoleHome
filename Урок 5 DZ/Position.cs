using MyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static MyConsole.Trade;

namespace MyConsole
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

        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);
            
            if (num > 0)
            {
                trade.typeOrder = TypeOrder.Long;

            }
            else if (num < 0)
            {
                trade.typeOrder = TypeOrder.Short;
            }

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(70000, 80000);

            string str = $"Volume = {trade.Volume} / Price = {trade.Price} / OrderType = {trade.typeOrder}";

            Console.WriteLine(str);
        }


    }
}
