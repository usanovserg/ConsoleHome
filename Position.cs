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
            timer.Elapsed += NewTrade;              // + автоматом создался метод private void NewTrade(object? sender, ElapsedEventArgs e) 
            timer.Start();
            Console.ReadKey();
            timer.Stop();
        }

        Random random = new Random();

        private void NewTrade(object? sender, ElapsedEventArgs e)      // это само создалось при заполнении timer.Elapsed += Timer_Elapsed; Название можно своё выбрать
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

            trade.Volume = Math.Abs(num);               // объём сделки
            trade.Price = random.Next(70000, 80000);    // цена сделки
            //string str = "Volume = " + trade.Volume.ToString() + "\tPrice = " + trade.Price.ToString();
            Console.WriteLine($"Volume = {trade.Volume}\tPrice = {trade.Price}");

        }
    }
}
