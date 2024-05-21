using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Timers;
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


        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            // to do
        }
    }
}
