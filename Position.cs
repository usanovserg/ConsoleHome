using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;


namespace ConsoleHome
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
        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
           Trade trade = new Trade();
           int num = random.Next(-10,10);
        }
    }
}
