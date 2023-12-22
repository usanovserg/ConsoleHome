using ConsoleHome.Enams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleHome
{
    public class Position
    {
        public Position()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += NewTrade;
            timer.Start();
        }

        public delegate void Handler();
        public event Handler Stuk = ShowMessage;


        Random random = new Random();
        /// <summary>
        /// Общее количество лот
        /// </summary>
        decimal TotalVolume = 0;
        //string name = Trade.;  
        





        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();
            int num = random.Next(-10, 10);
            trade.Volume = Math.Abs(num);

            if (num != 0)
            {
                Stuk();
            }

            if (num > 0)
            {
                //сделка в лонг
                trade.Napr = Operation.Buy;
                TotalVolume += trade.Volume;


            }
            else if (num <0)
            {
                // сделка в шорт
                trade.Napr = Operation.Sell;
                TotalVolume -= trade.Volume;
            }

            
            

            trade.Price = random.Next(70000, 80000);

            string str = trade.SecCode + " === " + DateTime.Now.ToString() + " Volume = " + trade.Volume.ToString() + " / Prise = " + trade.Price.ToString() + " направление = " + trade.Napr.ToString() + " итого в портфеле = " + TotalVolume.ToString() + " лот.";


            Console.WriteLine(str);

        }

        public static void ShowMessage()
        {
            Console.WriteLine("ПОЗИЦИЯ ИЗМЕНИЛАСЬ!");
        }

    }
}
