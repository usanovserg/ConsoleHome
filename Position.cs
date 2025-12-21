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
                    public Position()                                                       // Выполнение метода генерации новых сделок по таймеру (мс);
        {
            Timer timer = new Timer();
            timer.Interval = 3000;
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

        /// <summary> Направление общей позиции (Short; None; Long) </summary>
        public TypeTrade TypePosition = TypeTrade.None;

        #endregion
        //----------------------------------------------- Fields ------------------------------

        Random random = new Random();                                                       // Генерация новых сделок (случайно);

        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            DateTime dealTime = DateTime.Now;                                               // Задал возврат даты и времени генерируемой сделки;
            string dealTimeStr = dealTime.ToString("dd.MM.yyyy  HH:mm:ss");                 // формат вывода: День.Месяц.Год час.минута.секунда;

            decimal _bayOrSell;

            trade.Price = random.Next(70000, 80000);

            int num = random.Next(-10, 10);

            // Расчет средней цены позиции;                                                                            
            if ((VolumeLots >= 0 && num > 0) || (VolumeLots <= 0 && num < 0))               // Условие наращивания позиции Лонг или Шорт через новый объём (num);
            {
                AveregePositionPrice = (Math.Abs(VolumeLots * AveregePositionPrice) +       // Вычисление средней цены для ранее открытой позиции
                  Math.Abs(num) * trade.Price) / (Math.Abs(VolumeLots) + Math.Abs(num));    // с увеличением лотов в такой позиции (без изменения направления позиции);
            }
            
            else if (num != 0 && Math.Abs(VolumeLots) < Math.Abs(num))                      // Объем текущей позиции (VolumeLots) меньше нового объема сделки (num), 
            { AveregePositionPrice = trade.Price; }                                         // что позицию "переворачивает"(из Лонга в Шорт или наоборот)  - брать цену последней сделки;

            else if ( (VolumeLots + num) == 0 )                                             // После добавления к объему текущей позиции (VolumeLots) нового объема сделки (num),
            { AveregePositionPrice = 0; }                                                   // позиция закрыта "в ноль" (нет средней цены позиции);

                                                // Для иных случаев средняя цена позиции не меняется;   


            // Присвоение направления Сделке + Вычисление общего объема позиции;
            if (num > 0)                               // Сделка покупка (Bay);
            {
                trade.Side = TypeTransaction.Bay;                        
                _bayOrSell = VolumeLots + num;
            }
            else if (num < 0)                          // Сделка продажа (Sell);
            {
                trade.Side = TypeTransaction.Sell;
                _bayOrSell = VolumeLots + num;
            }
            else                                       // в иных случаях, num = 0 (нет сделки) "TypeTransaction.None";
            {
                _bayOrSell = VolumeLots;
            }

            trade.Volume = Math.Abs(num);

            VolumeLots = _bayOrSell;


            if (VolumeLots > 0)
            {
                trade.Position = TypeTrade.Long;
            }
            else if (VolumeLots < 0)
            {
                trade.Position = TypeTrade.Short;
            }
            else { trade.Position = TypeTrade.None; }


            string str0 = $"Time: {dealTimeStr}";

            string str1 = $"New transaction: {trade.Side}  / Volume = {trade.Volume.ToString()} / Price = {trade.Price.ToString()}";

            string str2 = "Current position (lot) = " + VolumeLots + " / Trade Side = " + trade.Position;

            string str3 = "Averege price position (all lots) = " + Math.Round(AveregePositionPrice, 2);


            Console.WriteLine(str0);
            Console.WriteLine(str1);
            Console.WriteLine(str2);
            Console.WriteLine(str3);
            Console.WriteLine();
        }
    }
}
