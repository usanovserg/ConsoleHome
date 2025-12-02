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
        //=================================== Fields ===============================================
        #region Fields
        /// <summary>
        /// Цена сделки
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
                PositionChanged?.Invoke(); // вызываем событие при изменении позиции
            }
        }
        decimal _volume = 0;
        #endregion
        Random random = new Random();
        //=================================== Methods ===============================================
        #region methods
        /// <summary>
        /// обработкак нового трейда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewTrade(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Trade trade = new();
            // параметры для случайного трейда
            RandomData data = new() { MaxPrice=2, MinPrice=1, MaxVolume=4, MinVolume=1, PrecisionPrice=0, PrecisionVolume=0 };
            // создаем случайный трейд
            trade.RandomTrade(data);
            CalculateNewPosition(trade.Price, trade.Volume * (sbyte)trade.DirectionTrade);
            Console.WriteLine($"Сделка {trade.DirectionTrade} : Volume = {trade.Volume.ToString()} / Price = {trade.Price.ToString()}");

            Console.WriteLine($"Позиция {DirectionPosition}  : Volume = {Volume.ToString()} / Price = {Price.ToString()}");
            Console.WriteLine();
        }
        /// <summary>
        /// расчитывает новую позицию
        /// </summary>
        private void CalculateNewPosition(decimal TradePrice, decimal TradeVolume)
        {
            decimal SumValume = Volume * Price + TradeVolume * TradePrice;
            if ((SumValume != 0) && (TradeVolume+ Volume != 0))
            {
                Volume += TradeVolume;
                Price = SumValume / Volume;
                if (Price < 0)
                {
                    Volume *= -1;
                    Price *= -1;
                }
            }
            else if (SumValume == 0)
            {
                Volume = Price = 0;
            }
            else
            {
                Volume = Math.Sign(SumValume);
                Price = Math.Abs(SumValume);
            }
        }
        #endregion
        public delegate void PositionChangedHandler();
        public event PositionChangedHandler PositionChanged;
    }
}
