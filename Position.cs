using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;


namespace MyConsole
{
   public class Position
    {
        public Position() 
        {
            Timer timer = new Timer();

            timer.Interval = 1000;

            timer.Elapsed += NewTrade;

            timer.Start();

        }

        Random random = new Random();  

        private void NewTrade(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            trade.DateTime = DateTime.Now; 

            int num = random.Next(-10, 10);

            if (num > 0)
            {
                trade.DirectionOfTrade = Trade.directionOfTrade.Long ;         
            }

            else
            {
                trade.DirectionOfTrade = Trade.directionOfTrade.Short;
            }

            trade.Volume = Math.Abs(num);
          
            trade.Price = random.Next(70000, 80000);

            trade.Commission = trade.GetCommission(Trade.typeOfComission.Limit.ToString());

            

            decimal averagePrice = 0;



            string str =  "Время = " + trade.DateTime.ToString() +
                          " Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString() + 
                          " Средняя цена = " + averagePrice.ToString() +
                          " / Direction = " + trade.DirectionOfTrade.ToString() + 
                          " / Commission = " + trade.Commission.ToString(); 
            
            Console.WriteLine(str);
        }
    } 
}
