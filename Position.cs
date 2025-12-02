using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MyConsole;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    public class Position
    {
        public Position()
        {
            Timer timer = new Timer();

            timer.Interval = 5000;

            timer.Elapsed += Timer_Elapsed;

            timer.Start();  

        }

        Random random = new Random();
        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade(); //Создадим экземпляр класса Trade

            int num = random.Next(-10, 10);

            if (num > 0 )
            {
                //сделка в лонг
            }
            else if (num < 0)
            {
                //сделка в шорт
            }
            trade.Volume = Math.Abs(num); // число num берем по модулю, чтобы оно всегда было положительным

            trade.Price = random.Next(70000, 80000);

            string str = "Volume = " + trade.Volume.ToString() + " / "+ trade.Price.ToString();

            Console.WriteLine(str);
        }
    }
}
