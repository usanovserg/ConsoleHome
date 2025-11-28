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
            int num_day = random.Next(1,7);

            

            DateTime startDate = new DateTime(2022, 10, 1, 00, 00, 00);
            DateTime endDate = new DateTime(2022, 12, 1, 00, 00, 00);
            // int days_between = (int)(endDate - startDate).Days;
            int totalSeconds = (int)(endDate - startDate).TotalSeconds;


            int randomSeconds = (int)random.Next(totalSeconds);             // Случайные секунды для смещения по дате 
            DateTime randomDateTime = startDate.AddSeconds(randomSeconds);


           
            if (num > 0)
            {
                trade.TypeOrder = "LONG Position";
            }
            else if (num<0)
            {
                trade.TypeOrder = "SHORT Position"; 
            }
            trade.Volume = Math.Abs(num);   

            trade.Price = random.Next( 70000, 80000);

            string str = "Type_Order - " + trade.TypeOrder + " /Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString() + " /Date - " + randomDateTime + " /Day_Of_Week - " + (DayOfWeek) num_day;

            Console.WriteLine(str);
        
        }
   
       
    }
}
