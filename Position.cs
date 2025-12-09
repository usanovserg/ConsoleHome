using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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

            timer.Interval = 1000; // Интервал в миллисекундах (1 секунда)

            timer.Elapsed += Timer_Elapsed; // Подписка на событие Elapsed

            timer.Start(); // Запуск таймера

        }
        Random random = new Random();
        
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade
            {
                int num = random.Next(-10, 10);

                if (num > 0)
            {
                // Сделка Long

            }


                else if (num < 0)
            {
                // Сделка Short

            }
                trade.Volume = Math.Abs(num);

                trade.Price = Random.Next(70000, 80000);

            string str = "Volume = " + trade.Volume.ToString()


            Console.WriteLine(str);

        }
    }
}
