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
            Timer timer = new Timer();      // создаем объект таймер

            timer.Interval = 5000;          // задаём свойство: интервал 1000 мсек = 1 сек

            timer.Elapsed += NewTrade;      // наш таймер раз в 1 сек будет вызывать метод Timer_Elapsed

            timer.Start();                  // запускаем таймер. Он в параллельном потоке: private void Timer_Elapsed(* * *) начинает раз в секунду вызывать Timer_Elapsed

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
