using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Transactions;
using Timer = System.Timers.Timer;


namespace ConsoleHome
{
    /// <summary>
    /// Класс управления совокупной позицией;
    /// </summary>
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
        /// <summary> Код инструмента (тикер). </summary>
        public string TickerCode = "";

        /// <summary> Общий объем открытой позиции (лот). </summary>
        public decimal VolumeLots = 0;

        /// <summary> Средняя цена открытия всей позиции. </summary>
        public decimal AveregePricePosition = 0;

        /// <summary> Фиксированный общий финансовый результат (сумма PnL всех сделок). </summary>
        public decimal fixTotalPnL = 0;

        /// <summary> Гарантийное обеспечение на 1 лот для длинной позиции (Long). </summary>
        public decimal marginLotLong = 12600;

        /// <summary> Гарантийное обеспечение на 1 лот для короткой позиции (Short). </summary>
        public decimal marginLotShort = 13000;


        #endregion
        //----------------------------------------------- Fields ------------------------------

        Random random = new Random();                                                       // Генерация новых сделок (случайно);

        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            DateTime dealTime = DateTime.Now;                                               // Задал возврат даты и времени генерируемой сделки;
            string dealTimeStr = dealTime.ToString("dd.MM.yyyy  HH:mm:ss");                 // формат вывода: День.Месяц.Год час.минута.секунда;

            decimal _buyOrSell;

            trade.Price = random.Next(70000, 80000);

            int num = random.Next(-10, 10);

            //----------------------------------------------- ФИН.РЕЗУЛЬТАТ ( PnL ) Begin ------------------------------
            #region PnL
                                                                                 // === Расчёт финансового результата (PnL) ===
            decimal pnl = 0;
            decimal _oldVolume = VolumeLots;                                     // сохранение объёма Позиции до новой сделки;
            bool _wasShort = _oldVolume < 0;
            bool _wasLong = _oldVolume > 0;
            bool _newDealBuy = (num > 0);
            bool _newDealSell = (num < 0);
                        
            if (_wasShort && _newDealBuy && (VolumeLots + num) < 0)              // Случай 1: частичное закрытие SHORT (была позиция Short, сделка Buy, осталась позиция Short);
            {
                decimal _closedVolume = num;                                     // Закрыто (num) лотов (при Buy сделке 0 положительное число);
                pnl = (AveregePricePosition - trade.Price) * _closedVolume;
            }
            
            else if (_wasLong && _newDealSell && (VolumeLots + num) > 0)         // Случай 2: частичное закрытие LONG (была Long, сделка Sell, осталась Long)
            {
                decimal _closedVolume = Math.Abs(num);                           // Закрыто (num) лотов (при Sell сделке - отрицательное число, берём по модулю);
                pnl = (trade.Price - AveregePricePosition) * _closedVolume;
            }

            else if (_wasShort && _newDealBuy && num >= Math.Abs(_oldVolume))    // Случай 3: Переворот позиции Short в Long или закрытие всей Позиции Short (в ноль);
            {
                decimal _closedVolume = Math.Abs(_oldVolume);                    // Закрыто (_oldVolume) лотов из Позиции Short - отрицательное число (берём по модулю);
                pnl = (AveregePricePosition - trade.Price) * _closedVolume;
            }
            else if (_wasLong && _newDealSell && Math.Abs(num) >= _oldVolume)    // Случай 4: Переворот позиции Long в Short или закрытие всей Позиции Long (в ноль);
            {
                decimal _closedVolume = _oldVolume;                              // Закрыто (_oldVolume) лотов Long (положительное число - не нужен модуль);
                pnl = (trade.Price - AveregePricePosition) * _closedVolume;
            }
            // Иначе — открытие новой позиции - PnL не рассчитываем;

            fixTotalPnL += pnl;                                                     // добавляем PnL текущей сделки к общему;
            #endregion
            //----------------------------------------------- ФИН.РЕЗУЛЬТАТ ( PnL ) End --------------------------------


            //----------------------------------------------- Средняя цена Позиции ( Averege Price Position ) Begin ----
            #region Averege Price Position
            // Расчет средней цены позиции;                                                                            
            if ((VolumeLots >= 0 && num > 0) || (VolumeLots <= 0 && num < 0))               // Условие наращивания позиции Лонг или Шорт через новый объём (num);
            {
                AveregePricePosition = (Math.Abs(VolumeLots * AveregePricePosition) +       // Вычисление средней цены для ранее открытой позиции
                  Math.Abs(num * trade.Price)) / (Math.Abs(VolumeLots) + Math.Abs(num));    // с увеличением лотов в такой позиции (без изменения направления позиции);
            }
            
            else if (num != 0 && Math.Abs(VolumeLots) < Math.Abs(num))                      // Объем текущей позиции (VolumeLots) меньше нового объема сделки (num), 
            { AveregePricePosition = trade.Price; }                                         // что позицию "переворачивает"(из Лонга в Шорт или наоборот)  - брать цену последней сделки;

            else if ( (VolumeLots + num) == 0 )                                             // После добавления к объему текущей позиции (VolumeLots) нового объема сделки (num),
            { AveregePricePosition = 0; }                                                   // позиция закрыта "в ноль" (нет средней цены позиции);

            // Для иных случаев средняя цена позиции не меняется;   
            #endregion
            //----------------------------------------------- Средняя цена Позиции ( Averege Price Position ) End ------


            // Присвоение направления Сделке + Вычисление общего объема позиции;
            if (num > 0)                               // Сделка покупка (Buy);
            {
                trade.Side = TypeTransaction.Buy;                        
                _buyOrSell = VolumeLots + num;
            }
            else if (num < 0)                          // Сделка продажа (Sell);
            {
                trade.Side = TypeTransaction.Sell;
                _buyOrSell = VolumeLots + num;
            }
            else                                       // в иных случаях, num = 0 (нет сделки) "TypeTransaction.None";
            {
                _buyOrSell = VolumeLots;
            }

            trade.Volume = Math.Abs(num);

            VolumeLots = _buyOrSell;


            if (VolumeLots > 0)
            {
                trade.Position = TypeTrade.Long;
            }
            else if (VolumeLots < 0)
            {
                trade.Position = TypeTrade.Short;
            }
            else { trade.Position = TypeTrade.None; }

            
            decimal currentMargin = 0;                                                            // Расчёт текущего гарантийного обеспечения для Позиции;
            if (VolumeLots > 0)       { currentMargin = VolumeLots * marginLotLong;}              // Для Позиции Long;
            else if (VolumeLots < 0)  { currentMargin = Math.Abs(VolumeLots) * marginLotShort;}   // Для Позиции Short;
            // Если VolumeLots = 0, размер гарантийного обеспечения = 0;


            string str0 = $"Data Time: {dealTimeStr}";

            string str1 = $"New transaction: {trade.Side}\t Volume = {trade.Volume}\t Price = {trade.Price:N0}";

            string str2 = "Current position (lot) = " + VolumeLots + "\t Trade Side = " + trade.Position;

            string str3 = $"Averege price position (all lots) = {AveregePricePosition:N}";

            string str4 = $"Initial Margin (IM): {currentMargin:N0}";

            string str5 = $"PnL from trade: {pnl:N2}";

            string str6 = $"Fixed total PnL: {fixTotalPnL:N2}";


            Console.WriteLine(str0);
            Console.WriteLine(str1);
            Console.WriteLine(str2);
            Console.WriteLine(str3);
            Console.WriteLine(str4);
            Console.WriteLine(str5);
            Console.WriteLine(str6);
            Console.WriteLine();
        }
    }
}
