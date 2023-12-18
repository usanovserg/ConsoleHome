using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    public class cPosition
    {
        public cPosition() 
        {
            Timer timer = new Timer();
            timer.Interval = 5000;
            timer.Elapsed += New_Trade;
            timer.Start();
        }

        #region Fields
        string account = "123";

        

        #endregion

        Random random = new Random();
        private void New_Trade(object? sender, ElapsedEventArgs e)
        {
            cTrade trade = new cTrade() {dateTime = DateTime.Now};
            int num = random.Next(-10,10);
            if (num > 0) {
                trade.direction = cTrade.Direction.Long;
            } else if (num < 0)
            {
                trade.direction = cTrade.Direction.Short;
            }
            else
            {
                trade.direction = cTrade.Direction.none;
            }

            trade.price = random.Next(100, 200);
            trade.Lot = Math.Abs(num);


            string str = trade.toString(trade.direction) + " лот: " + trade.Lot.ToString() + " / цена: " + trade.price.ToString();

            Console.WriteLine(str);


        }
    }
}
