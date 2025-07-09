using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace AlexHolyGun
{
    public class Position
    {
        #region Fields //--------------------------------------------------------------//
        public static System.Timers.Timer timer = new();
        private Random rand = new();
        #endregion

        #region Properties  //--------------------------------------------------------------//

        #endregion

        #region Methods  //--------------------------------------------------------------//
        public Position()
        {
            timer.Interval = 5000;
            timer.Elapsed += NewTrade;
            timer.Start();
        }
        
        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            int num = rand.Next(-10, 10);
            Trade trade = new();
            trade.Price = rand.Next(70000, 80000);
            trade.Volume = Math.Abs(num);

            if (num > 0) //сделка в лонг
                trade.Direction = Trade.Dir.Long;
            else //сделка в шорт
                trade.Direction = Trade.Dir.Short;

            Console.WriteLine($"Volume: {trade.Volume.ToString()}  Price: {trade.Price.ToString()}  Direction:{trade.Direction.ToString()} ");
        }
        #endregion

    }
}
