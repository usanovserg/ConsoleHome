using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


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
            Trade trade = new Trade();

            DateTime DateTimes = DateTime.Now;

            //trade.type_order = Trade.Order.Short;

            int num = random.Next(-10, 10);

            if (num > 0)
            {
                // Сделка в лонг
                trade.type_order = Trade.Order.Long;
            }
            else if (num < 0)
            {
                // Сделка в шорт
                trade.type_order = Trade.Order.Short;
            }

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(70000, 80000);

            string str = "Сделка : " + trade.type_order + "\tVolume = " + trade.Volume.ToString() + "\tPrice = " + trade.Price.ToString() + "\tДатаВремя : " + DateTimes;

            Console.WriteLine(str);
        }
    }
}
