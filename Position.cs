using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Timer timer = new Timer();

            timer.Interval = 2000;

            timer.Elapsed += NewTrade;

            timer.Start();

        }

        Random random = new Random();
        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            //throw new NotImplementedException();

            Trade trade = new Trade();          


            string _dir = DefineDirection();

            trade.Volume = DefineVolume();

            trade._price = DefinePrice();

            trade._secCode = "xxx";

            trade._classCode = "zzz";

            decimal totalVolume = CountTotalVolume(trade.Volume, trade._price);

            string dateTimeOrder = DefineTimeOrder();

            WriteLine(dateTimeOrder, trade.Volume.ToString(), trade._price.ToString(), _dir, trade._secCode, trade._classCode, totalVolume.ToString());

            //int h = 9;
            //int y = 54;
            //int b = 45;
            //int z = h * y / b;

            //Console.WriteLine(z.ToString());
        }

        public string DefineDirection()
        {
            int num = random.Next(-10, 10);
            string dir ="";

            if (num > 0)
            {
                dir = Trade.direction.Long.ToString();             
            }
            else if (num <= 0)
            {
                dir = Trade.direction.Short.ToString();               
            }

            return dir;
        }


        public int DefineVolume()
        {
            int volume = random.Next(-9, -1);
            
            return Math.Abs(volume);
        }


        public int DefinePrice()
        {
            int price = random.Next(70000, 80000);
            
            return price;
        }


        public void WriteLine(string datetime, string volume, string price, string dir, string secCode, string classCode, string totalVolume)
        {

            string msg = datetime + " // Lots = " + volume + " / Price = " + price + " / направление: " + dir + " / SecCode: " + secCode + "/ classCode: " + classCode + " / совокупный объем = " + totalVolume; 
             
            Console.WriteLine(msg);
        }


        public decimal CountTotalVolume(decimal lot, decimal price)
        {
            Level level = new Level();
            level.LotLevel = lot;
            Level.Volume = Level.Volume + level.LotLevel;
            level.PriceLevel = price;

            return Level.Volume;
        }

        public string DefineTimeOrder()
        {
            DateTime DateTime = DateTime.MinValue;
            DateTime = DateTime.Now;

            return DateTime.Now.ToString();
        }


    }
}

//propfull