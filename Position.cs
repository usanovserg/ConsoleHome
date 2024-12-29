using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static ConsoleHome.Enums.Enums;
using static ConsoleHome.Trade;
using Timer = System.Timers.Timer;


namespace ConsoleHome
{
    public class Position
    {
        public Position()
        {/*
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += NewTrade;
            timer.Start();*/
        }


        //============================================================= Fields =======================================
        #region Fields

        Random random = new Random();       

        #endregion


        //============================================================= Methods ========================================
        #region Methods
        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Connector connector = new Connector();
            Trade trade = new Trade();
            int num = random.Next(-10, 10);

            if (num > 0) connector.direction = Direction.Long;
            
            else if (num < 0) connector.direction = Direction.Short;
           

            trade.Volume = Math.Abs(num);
            trade.Price = random.Next(70000, 800000);
            trade.AssetName = "WLD";
            trade.PriceEnter = random.Next(70000, 800000);
            trade.PriceExit = random.Next(60000, 900000);
            string str = "AssetName = " + trade.AssetName.ToString() + " / Direction = " + connector.direction?.ToString() + " / Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString() + " / PriceEnter = " + trade.PriceEnter.ToString() + " / PriceExit = " + trade.PriceExit.ToString();      
            
            Console.WriteLine(str);            
        }       

        #endregion
    }
}
