using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleHome.Trade;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    public class Position
    {
        public Position()
        {
            Timer timer = new Timer();
            timer.Interval = 500;
            timer.Elapsed += NewTrade;
            timer.Start();
        }
        public delegate void PositionChangedHandler(Position position); // сигнатура делегата
        public event PositionChangedHandler? PositionChanged; // делегат для события изменения позиции
        public event PositionChangedHandler ProfitChanged; // делегат для события изменения профита
        //=================================== Fields ===============================================
        #region Fields
        public decimal Price = 0;
        public decimal TotalProfit = 0;
        /// <summary>
        /// направление позиции
        /// </summary>
        public DPositon DirectionPosition = DPositon.None;
        public enum DPositon : sbyte
        {
            Long = 1 ,
            Short = -1,
            None = 0
        }
        #endregion
        //=================================== Properties ===============================================
        #region Properties
        /// <summary>
        /// Объем позиции
        /// </summary>
        public decimal Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                _volume = value;
                PositionChanged?.Invoke(this); // вызываем событие при изменении позиции
            }
        }
        decimal _volume = 0;
        public decimal Profit
        {
            get
            {
                return _profit;
            }
            set
            {
                _profit = value;
                ProfitChanged?.Invoke(this); // вызываем событие при изменении прибыли
            }
        }
        decimal _profit = 0;
        #endregion
        //=================================== Methods ===============================================
        #region methods
        public string Symbol { get; set; }           // Инструмент:
        public decimal CurrentPrice { get; set; }        // Текущая рыночная цена
        public decimal StopLoss { get; set; }           // Цена стоп-лосса
        public decimal TakeProfit { get; set; }         // Цена тейк-профита
        public DateTime OpenTime { get; set; }          // Когда открыли позицию
        public decimal Leverage { get; set; }           // Кредитное плечо
        public decimal Margin { get; set; }             // Залог под позицию
        public decimal LiquidationPrice { get; set; }   // Цена ликвидации
        /// <summary>
        /// обработкак нового трейда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewTrade(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Trade trade = new();
            // параметры для случайного трейда
            RandomData data = new() { MaxPrice=10, MinPrice=1, MaxVolume=5, MinVolume=1, PrecisionPrice=0, PrecisionVolume=0 };
            // создаем случайный трейд
            trade.RandomTrade(data);
            Console.WriteLine($"Сделка {trade.DirectionTrade} : Volume = {trade.Volume.ToString()} / Price = {trade.Price.ToString()}");
            // вычисляем новую позицию
            CalculateNewPosition(trade.Price, trade.Volume * (sbyte)trade.DirectionTrade);
            Console.WriteLine();
        }
        /// <summary>
        /// расчитывает новую позицию
        /// </summary>
        private void CalculateNewPosition(decimal TradePrice, decimal TradeVolume)
        {
            decimal tPrice = 0;
            decimal tVolume = 0;
            decimal tProfit = 0;
            if (DirectionPosition == DPositon.None) // если позиции не было, нужно ей присвоить направление при открытии
                DirectionPosition= TradeVolume >= 0? DPositon.Long : DPositon.Short;
            if ((sbyte)DirectionPosition * TradeVolume >= 0) //позиция на продолжение или вновь открытая , добавляем лоты и считаем среднюю цену
            {
                tVolume = Volume + Math.Abs(TradeVolume);
                tPrice = (Volume * Price + Math.Abs(TradeVolume) * TradePrice) / tVolume;
            }
            else //позиция сокращается, (может измениться направление позиции)
            {
                tVolume = Volume - Math.Abs(TradeVolume);
                if (tVolume == 0) // если прозиция закрывается
                {
                    tPrice = 0;
                    tProfit = (sbyte)DirectionPosition * Volume * (TradePrice - Price);
                    DirectionPosition = DPositon.None;
                }    
                else if (tVolume < 0) // если позиция перевенулась в другую сторону
                {
                    tPrice = TradePrice;
                    tVolume = -tVolume;
                    tProfit = (sbyte)DirectionPosition * Volume * (TradePrice - Price);
                    DirectionPosition = (DPositon)(-1*(sbyte)DirectionPosition);
                }
                else   // направление не поменялось
                {
                    tProfit = (sbyte)DirectionPosition * Math.Abs(TradeVolume) * (TradePrice - Price);
                    tPrice = Price;
                }
                TotalProfit += tProfit;
                Profit = tProfit;
            }
            Price = tPrice;
            Volume = tVolume;
          //  Profit= tProfit;
        }
        #endregion
    }
}
