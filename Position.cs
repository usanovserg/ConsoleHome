using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Transactions;
using static ConsoleHome.Trade;

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

        //----------------------------------------------- Fields ------------------------------
        #region Fields
        /// <summary> Код инструмента (тикер) </summary>
        public string TickerCode = "";

        /// <summary> Общий объем открытой позиции (лот) </summary>
        public decimal VolumeLots = 0;

        /// <summary> Средняя цена открытия всей позиции </summary>
        public decimal AveregePositionPrice = 0;

        /// <summary> Направление общей позиции (Long; Short) </summary>
        public string TapePosition = "";

        #endregion
        //----------------------------------------------- Fields ------------------------------

        Random random = new Random();


        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            string tapeDeal = "";

            int num = random.Next(-10, 10);

            if (num > 0)
            {
                // Сделка в лонг;
                MyTransaction _deal = MyTransaction.Bay;
                tapeDeal = "Deal Bay >>> ";

            }
            else if (num < 0)
            {
                // Сделка в шорт;
                MyTransaction _deal = MyTransaction.Sell;
                tapeDeal = "Deal Sell <<< ";
            }
            else { tapeDeal = "Deal Not ------ "; }

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(70000, 80000);


            string str = tapeDeal + "Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString();


            Console.WriteLine(str);
        }
    }
}
