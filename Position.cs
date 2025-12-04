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

            timer.Interval = 5000;

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

            decimal longAndShort;

            string tapeDeal = "";

            trade.Price = random.Next(70000, 80000);

            int num = random.Next(-10, 10);



            if ((VolumeLots >= 0 && num > 0) || (VolumeLots <= 0 && num < 0))
            {
                // Вычисление средней цены для ранее открытой позиции с увеличением лотов в такой позиции (без изменения направления позиции);
                AveregePositionPrice = (Math.Abs(VolumeLots) * Math.Abs(AveregePositionPrice) + Math.Abs(num) * trade.Price) / (Math.Abs(VolumeLots) + Math.Abs(num));
            }
            // Объем текущей позиции (VolumeLots) меньше нового объема сделки (num), что позицию "переворачивает" (из Лонга в Шорт или наоборот)  - брать цену инструмента (последней сделки);
            else if (num != 0 && Math.Abs(VolumeLots) < Math.Abs(num))
            { AveregePositionPrice = trade.Price; }

            // После добавления к объему текущей позиции (VolumeLots) нового объема сделки (num), позиция закрыта "в ноль" (нет средней цены позиции);
            else if ( (VolumeLots + num) == 0 ) { AveregePositionPrice = 0; }
            // Для иных случаев средняя цена не меняется;
            

            if (num > 0)
            {                
                // Сделка в лонг;
                tapeDeal = "Deal Bay >>> ";

                longAndShort = VolumeLots + num;
            }
            else if (num < 0)
            {
                // Сделка в шорт;                
                tapeDeal = "Deal Sell <<< ";

                longAndShort = VolumeLots + num;
            }
            else
            {
                tapeDeal = "Deal Not ------ ";
                longAndShort = VolumeLots;
            }

            trade.Volume = Math.Abs(num);

            VolumeLots = longAndShort;
            

            string str1 = tapeDeal + "Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString();

            string str2 = "Current position (lot) = " + VolumeLots;

            string str3 = "Averege price position (all lots) " + Math.Round(AveregePositionPrice, 2);


            Console.WriteLine(str1);
            Console.WriteLine(str2);
            Console.WriteLine(str3);
            Console.WriteLine();
        }
    }
}
