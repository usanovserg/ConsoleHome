using System;
using System.Collections.Generic;
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
            timer.Interval = 1000;
            timer.Elapsed += NewTrade;
           // timer.AutoReset = true;
          //  timer.Enabled = true;
            timer.Start();
        }
        public delegate void PositionChangedHandler(Position position); // сигнатура делегата
        public event PositionChangedHandler? PositionChanged; // делегат для события
        //=================================== Fields ===============================================
        #region Fields
        /// <summary>
        /// Цена позиции
        /// </summary>
        public decimal Price = 0;
        /// <summary>
        /// направление позиции
        /// </summary>
        public DPositon DirectionPosition = DPositon.No;
        public enum DPositon 
        {
            Long ,
            Short ,
            No
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
                if (value < 0) DirectionPosition = DPositon.Short;
                else if (value > 0) DirectionPosition = DPositon.Long;
                else DirectionPosition = DPositon.No;
                PositionChanged?.Invoke(this); // вызываем событие при изменении позиции
            }
        }
        decimal _volume = 0;
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
            decimal SumValume = Volume * Price + TradeVolume * TradePrice;
            decimal tPrice = 0;
            decimal tVolume = 0;

            if ((SumValume != 0) && (TradeVolume+ Volume != 0))
            {
                tPrice = SumValume / (Volume + TradeVolume);
                tVolume = Volume + TradeVolume;
                if (tPrice < 0)
                {
                    tPrice *= -1;
                    tVolume *= -1;
                }
            }
            else if (SumValume == 0)
            {
                tVolume = tPrice = 0;
            }
            else
            {
                tPrice = Math.Abs(SumValume);
                tVolume = Math.Sign(SumValume);
            }
            Price = tPrice;
            Volume = tVolume;
        }
        #endregion
    }
}
