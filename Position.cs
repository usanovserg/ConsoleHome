using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;
using Timer = System.Timers.Timer;
using System.Transactions;
using static ConsoleHome.Trade;

namespace ConsoleHome
{
    public class Position
    {
                    public Position() 
        {
            Timer timer = new Timer();

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
        public TypeTrade TypePosition = TypeTrade.None;

        #endregion
        //----------------------------------------------- Fields ------------------------------

        Random random = new Random();


        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            decimal _longAndShort;

            //string typeTrade = "";

            string tapeDeal = "";

            trade.Price = random.Next(70000, 80000);

            int num = random.Next(-10, 10);


            // Расчет средней цены позиции;
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
            // Для иных случаев средняя цена позиции не меняется;
            
            // Вычисление общего объема позиции;
            if (num > 0)
            {
                // Сделка в лонг;
                trade.Side = TypeTrade.Long;
                tapeDeal = "Deal Bay >>> ";

                _longAndShort = VolumeLots + num;
            }
            else if (num < 0)
            {
                // Сделка в шорт;
                trade.Side = TypeTrade.Short;
                tapeDeal = "Deal Sell <<< ";

                _longAndShort = VolumeLots + num;
            }
            else
            {
                tapeDeal = "Deal Not ------ ";
                // trade.Side = TypeTrade.None;
                _longAndShort = VolumeLots;
            }

            trade.Volume = Math.Abs(num);

            VolumeLots = _longAndShort;


            if (VolumeLots > 0)
            {
                trade.Side = TypeTrade.Long;
            }
            else if (VolumeLots < 0)
            {
                trade.Side = TypeTrade.Short;
            }
            else { trade.Side = TypeTrade.None; }


            //string str1 = typeTrade.ToString() + "Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString();


            string str1 = tapeDeal + "Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString();

            string str2 = "Current position (lot) = " + VolumeLots + " Trade Side = " + trade.Side;

            string str3 = "Averege price position (all lots) " + Math.Round(AveregePositionPrice, 2);


            Console.WriteLine(str1);
            Console.WriteLine(str2);
            Console.WriteLine(str3);
            Console.WriteLine();
        }
    }
}
