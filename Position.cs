using ConsoleHome.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Transactions;
using static ConsoleHome.Position;
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

        //----------------------------------------------- Fields (поля) Begin ------------------------------
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

        public event EventHandler<PositionChangedEventArgs> PositionChanged;                // Встроенный делегат EventHandler<>;

        ///// <summary> Делегат (изменение позиции). </summary>                             // Первоначальный вариант (можно удалить);
        //public delegate void PositionChangeHandler(PositionChangeType changeType);

        ///// <summary> Событие (изменение позиции). </summary>
        //public event PositionChangeHandler PositionChanged;


        #endregion
        //----------------------------------------------- Fields (поля) End ------------------------------

        Random random = new Random();                                                       // Генерация новых сделок (случайно);

        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            DateTime dealTime = DateTime.Now;                                               // Задал возврат даты и времени генерируемой сделки;
            string dealTimeStr = dealTime.ToString("dd.MM.yyyy  HH:mm:ss");                 // формат вывода: День.Месяц.Год час.минута.секунда;

            decimal _buyOrSell;

            trade.Price = random.Next(70000, 80000);

            int number = random.Next(-10, 10);

            //----------------------------------------------- ФИН.РЕЗУЛЬТАТ ( PnL ) Begin ------------------------------
            #region PnL
                                                                                 // === Расчёт финансового результата (PnL) ===
            decimal pnl = 0;
            decimal _oldVolume = VolumeLots;                                     // сохранение объёма Позиции до новой сделки;
            bool _wasShort = _oldVolume < 0;
            bool _wasLong = _oldVolume > 0;
            bool _newDealBuy = (number > 0);
            bool _newDealSell = (number < 0);
                        
            if (_wasShort && _newDealBuy && (VolumeLots + number) < 0)              // Случай 1: частичное закрытие SHORT (была позиция Short, сделка Buy, осталась позиция Short);
            {
                decimal _closedVolume = number;                                     // Закрыто (num) лотов (при Buy сделке 0 положительное число);
                pnl = (AveregePricePosition - trade.Price) * _closedVolume;
            }
            
            else if (_wasLong && _newDealSell && (VolumeLots + number) > 0)         // Случай 2: частичное закрытие LONG (была Long, сделка Sell, осталась Long)
            {
                decimal _closedVolume = Math.Abs(number);                           // Закрыто (num) лотов (при Sell сделке - отрицательное число, берём по модулю);
                pnl = (trade.Price - AveregePricePosition) * _closedVolume;
            }

            else if (_wasShort && _newDealBuy && number >= Math.Abs(_oldVolume))    // Случай 3: Переворот позиции Short в Long или закрытие всей Позиции Short (в ноль);
            {
                decimal _closedVolume = Math.Abs(_oldVolume);                    // Закрыто (_oldVolume) лотов из Позиции Short - отрицательное число (берём по модулю);
                pnl = (AveregePricePosition - trade.Price) * _closedVolume;
            }
            else if (_wasLong && _newDealSell && Math.Abs(number) >= _oldVolume)    // Случай 4: Переворот позиции Long в Short или закрытие всей Позиции Long (в ноль);
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
            if ((VolumeLots >= 0 && number > 0) || (VolumeLots <= 0 && number < 0))               // Условие наращивания позиции Лонг или Шорт через новый объём (num);
            {
                AveregePricePosition = (Math.Abs(VolumeLots * AveregePricePosition) +       // Вычисление средней цены для ранее открытой позиции
                  Math.Abs(number * trade.Price)) / (Math.Abs(VolumeLots) + Math.Abs(number));    // с увеличением лотов в такой позиции (без изменения направления позиции);
            }
            
            else if (number != 0 && Math.Abs(VolumeLots) < Math.Abs(number))                      // Объем текущей позиции (VolumeLots) меньше нового объема сделки (num), 
            { AveregePricePosition = trade.Price; }                                         // что позицию "переворачивает"(из Лонга в Шорт или наоборот)  - брать цену последней сделки;

            else if ( (VolumeLots + number) == 0 )                                             // После добавления к объему текущей позиции (VolumeLots) нового объема сделки (num),
            { AveregePricePosition = 0; }                                                   // позиция закрыта "в ноль" (нет средней цены позиции);

            // Для иных случаев средняя цена позиции не меняется;   
            #endregion
            //----------------------------------------------- Средняя цена Позиции ( Averege Price Position ) End ------


            //----------------------------------------------- Объем и направление Позиции ( Volume, Side and Margin Position ) Begin ----
            #region Volume, Side, Margin Position

            // Присвоение направления Позиции + Вычисление общего объема позиции;
            if (number > 0)                               // Сделка покупка (Buy);
            {
                trade.Side = SideTransaction.Buy;                        
                _buyOrSell = VolumeLots + number;
            }
            else if (number < 0)                          // Сделка продажа (Sell);
            {
                trade.Side = SideTransaction.Sell;
                _buyOrSell = VolumeLots + number;
            }
            else                                       // в иных случаях, num = 0 (нет сделки) "TypeTransaction.None";
            {
                _buyOrSell = VolumeLots;
            }

            trade.Volume = Math.Abs(number);

            VolumeLots = _buyOrSell;                   // Новый размер позиции;

            
            // Присвоение направления Позиции (общая позиция);
            if (VolumeLots > 0)
            {
                trade.Position = SidePosition.Long;
            }
            else if (VolumeLots < 0)
            {
                trade.Position = SidePosition.Short;
            }
            else { trade.Position = SidePosition.None; }

            decimal currentMargin = 0;                                                            // Расчёт текущего гарантийного обеспечения для Позиции;
            if (VolumeLots > 0)       { currentMargin = VolumeLots * marginLotLong;}              // Для Позиции Long;
            else if (VolumeLots < 0)  { currentMargin = Math.Abs(VolumeLots) * marginLotShort;}   // Для Позиции Short;
                                                                                                  // Если VolumeLots = 0, размер гарантийного обеспечения = 0;

            #endregion
            //----------------------------------------------- Объем и направление Позиции ( Volume, Side and Margin Position ) End ------


            // Создание событий;
            var args = new PositionChangedEventArgs(
                changeType: (number != 0) ? PositionChangeType.Changed : PositionChangeType.NotChanged,
                newVolume: VolumeLots,
                averagePrice: AveregePricePosition,
                initialMargin: currentMargin,
                pnl: pnl,
                totalPnl: fixTotalPnL,
                tradeSide: trade.Side,
                tradeVolume: trade.Volume,
                tradePrice: trade.Price,
                dealTime: dealTimeStr
            );

            // Вызов событий;
            PositionChanged?.Invoke(this, args);











            //                                                                           // Сведения об изменении позиции (через Событие);
            //PositionChangeType changeType = (number != 0) ?
            //    PositionChangeType.Changed : PositionChangeType.NotChanged;            

            //var args = new PositionChangedEventArgs(changeType, VolumeLots);           
            //PositionChanged?.Invoke(this, args);                                       // this ссылается на текущий экземпляр, т.к. делегату EventHandle<> нужен sender,
            //                                                                           // можно игнорировать (убрать) (не рекомендуется);


            ////PositionChangeType changeType = (number != 0) ? PositionChangeType.Changed : PositionChangeType.NotChanged;  // Обращение к enum (сведения об изменении позиции);
            ////PositionChanged?.Invoke(changeType);                                                                         // Вызов события (изменение позиции или без изменения);


            //string str0 = $"Data Time: {dealTimeStr}";

            //string str1 = $"New transaction: {trade.Side}\t\t Volume transaction = {trade.Volume}\t Price transaction = {trade.Price:N0}";

            //string str2 = "Current position (lot) = " + VolumeLots + "\t Trade Side = " + trade.Position;

            //string str3 = $"Averege price position (all lots) = {AveregePricePosition:N}";

            //string str4 = $"Initial Margin (IM): {currentMargin:N0}";

            //string str5 = $"PnL from trade: {pnl:N2}";

            //string str6 = $"Fixed total PnL: {fixTotalPnL:N2}";


            //Console.WriteLine(str0);
            //Console.WriteLine(str1);
            //Console.WriteLine(str2);
            //Console.WriteLine(str3);
            //Console.WriteLine(str4);
            //Console.WriteLine(str5);
            //Console.WriteLine(str6);
            //Console.WriteLine();
        }
    }
}
