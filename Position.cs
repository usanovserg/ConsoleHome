using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static MyConsole.Program;
using System.Timers;
namespace ConsoleHome
{
    internal class Position
    {
        public Position()
        {
            System.Timers.Timer timer = new System.Timers.Timer();  //????? поч System.Timers

            timer.Interval = 1000;

            timer.Elapsed += NewTrade;

            timer.Start();  
        }
        Random random = new Random();

        public System.Timers.Timer Timer { get; }

        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade= new Trade();

            int num = random.Next(-10, 10);
            byte num_day = (byte)random.Next(1,8);
            byte num_ticker = (byte)random.Next(1, 4);

            

            DateTime startDate = new DateTime(2022, 10, 1, 00, 00, 00);
            DateTime endDate = new DateTime(2022, 12, 1, 00, 00, 00);
            
            int totalSeconds = (int)(endDate - startDate).TotalSeconds;


            int randomSeconds = (int)random.Next(totalSeconds);             // Случайные секунды для смещения по дате 
            DateTime randomDateTime = startDate.AddSeconds(randomSeconds);

            string randomDateTimeStr = randomDateTime.ToString("dd.MM.yyyy HH:mm:ss");

            trade.Price = random.Next(70000, 80000);
            trade.Volume = Math.Abs(num);

            if (num > 0)
            {
                trade.TypeOrder = "LONG";
                trade.StopLoss = trade.Price - random.Next(0, 10000);
                trade.TakeProfit = trade.Price + random.Next(0, 10000);
            }
            else if (num<0)
            {
                trade.TypeOrder = "SHORT";
                trade.StopLoss = trade.Price + random.Next(0, 10000);
                trade.TakeProfit = trade.Price - random.Next(0, 10000);
            }







            //string str = "Type_Order - " + trade.TypeOrder + " /Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString() + " /Date - " + randomDateTime + " /Day_Of_Week - " + (DayOfWeek) num_day;

            if (num != 0) //защита от 0
            {


              
                Console.WriteLine("{0,-10} {1,-6} {2,-8} {3,-12} {4, -12} {5, -12} {6,-20} {7,-10} {8,-12}",
                    //"Ticker", "Type", "Volume", "Price", "StopLoss","TakeProfit", "Date", "Day" ,"Indicator"
                    ((MyConsole.Program.NameTicker.Ticker)num_ticker).ToString(),
                    trade.TypeOrder, 
                    trade.Volume, 
                    trade.Price,
                    trade.StopLoss.ToString(),
                    trade.TakeProfit.ToString(),
                    randomDateTimeStr,
                    ((MyConsole.Program.DayWeek.MineDayOfWeek)num_day).ToString(),
                    "RSI(" + random.Next(1, 14) + ")"


                    );
            }
        
        }
   
       
    }
}
