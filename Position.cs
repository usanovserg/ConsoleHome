//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Timers;

//namespace ConsoleHome
//{
//    public class Position
//    {
//        public Position()
//        {
//            System.Timers.Timer timer = new System.Timers.Timer();
//            timer.Interval = 1000;
//            timer.Elapsed += NewTrade;
//            timer.Start();
//        }
//        Random random = new Random();

//        private void NewTrade(object sender, ElapsedEventArgs e)
//        {
//            Trade trade = new Trade();
//            int num = random.Next(-10, 19);

//            if (num > 0)
//            {
//                //deal long
//            }
//            else if (num < 0)
//            {
//                //deal short
//            }
//            trade.Volume = Math.Abs(num);
//            trade.Price = random.Next(70000, 80000);
//            string str = "Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString();

//            Console.WriteLine(str);
//        }


//    }
//}
