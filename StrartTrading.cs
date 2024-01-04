using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace Lesson_6
{
    public class StartTrading
    {
        private Random random = new Random();
        private PositionValue? _positionValue;
        public StartTrading()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 4000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            CurrentPosition newPosition = new CurrentPosition();

            _positionValue = newPosition.PositionValue;

            decimal _volume = random.Next(-50, 50);
            decimal _lotprice = random.Next(100, 200);
            decimal _pricePos = Math.Abs(_volume) * _lotprice;
            DateTime _dateTime;

            _positionValue?.Invoke (_volume, _lotprice, _pricePos, _dateTime = DateTime.Now);

            newPosition.ConsoleWrite();

        }
       
    }
}
