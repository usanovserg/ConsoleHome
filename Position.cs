// Ignore Spelling: Сlass

using ConsoleHome.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    public class Position
    {
        #region ========================================================Constructor=======================================================
        public Position() //конструктор, инициирующий появление событий - сделок в позиции
        {
            Timer timer = new Timer();

            timer.Interval = 1000;

            timer.Elapsed += NewTrade; //подписка на событие

            timer.Start();
        }
        #endregion

        #region ========================================================Delegate and Event=======================================================
        public delegate void PositionChangeHandler(Position position);

        public event PositionChangeHandler? PositionChangeEvent;
        #endregion

        #region ========================================================Fields=======================================================

        /// <summary>
        /// накопительный объём сделок в Long в позиции (сделок * лотов)
        /// </summary>
        decimal volumeLong = 0;

        /// <summary>
        /// накопительное объем сделок в Short в позиции (сделок * лотов)
        /// </summary>
        decimal volumeShort = 0;

        /// <summary>
        /// Средняя стоимость позиции
        /// </summary>
        decimal averagePrice = 0;

        #endregion

        #region ========================================================Properties=====================================================

        /// <summary>
        /// Время входа в позицию
        /// </summary>
        public DateTime EnterTime { get; } = DateTime.Now;

        /// <summary>
        /// Код торгового счета (портфель)
        /// </summary>
        public string PortFolio { get; } = Program.RandomString("PF");

        /// <summary>
        /// Код торговой площадки
        /// </summary>
        public string PlatformCode { get; } = Program.RandomString("PC");

        /// <summary>
        /// Код бумаги (инструмент)
        /// </summary>
        public string SecCode { get; } = Program.RandomString("SC");

        /// <summary>
        /// Классификатор на бирже (акции, фьючи, валюта ...)
        /// </summary>
        public string СlassCode { get; } = Program.RandomString("CC");

        /// <summary>
        /// Время последней сделки в позиции
        /// </summary>
        public DateTime LastTradeTime
        {
            get
            {
                return _lastTradeTime;
            }
        }
        DateTime _lastTradeTime = DateTime.Now;

        /// <summary>
        /// Текущее направление позиции (Long, Short)
        /// </summary>
        public Way Way
        {
            get
            {
                return _way;
            }

        }
        Way _way = Way.Long;



        /// <summary>
        /// Позиция - количество открытых контрактов в позиции
        /// </summary>
        public decimal Volume
        {
            get
            {
                return _volume;
            }

        }
        decimal _volume = 0;
        
        /// <summary>
        /// Стоимость - текущая стоимость открытых контрактов в позиции - цена позиции по действующей стоимости 
        /// </summary>
        public decimal Price
        {
            get
            {
                return _price;
            }

        }
        decimal _price = 0;

        /// <summary>
        /// Накопленная прибыль/убыток по позиции
        /// </summary>
        public decimal Margin
        {
            get
            {
                return _margin;
            }

        }
        decimal _margin = 0;

        /// <summary>
        /// Накопленная комиссия по позиции
        /// </summary>
        public decimal Commission
        {
            get
            {
                return _commission;
            }

        }
        decimal _commission = 0;

        #endregion

        #region ========================================================Methods========================================================
        Random random = new Random();

        /// <summary>
        /// Метод, создающий новую сделку в позиции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            decimal wayVolume = trade.Volume;//для сделки в Long хранит положительное значение trade.Volume

            if (trade.Way == Way.Short)
            {
                volumeShort += trade.Volume;

                wayVolume = -wayVolume; //для сделки в Short хранит отрицательное значение trade.Volume
            }
            else
            {
                volumeLong += trade.Volume;
            }

            Console.Write("Сделка: ");

            Program.PrintProperties(trade);

            CalculatingPosition(trade, wayVolume);

            PositionChangeEvent?.Invoke(this); //через событие отдаем очередную позицию на обрадотку в основную программу

            if (volumeLong == volumeShort)
            {
                Console.WriteLine("Все контракты в позиции закрыты!\n");
            }
        }

        /// <summary>
        /// Прерасчет позиции по данным очередной сделки
        /// </summary>
        /// <param name="trade"></param>
        /// <param name="num"></param>
        private void CalculatingPosition(Trade trade, decimal wayVolume)
        {
            CalculatingLastTradeTime(trade);

            CalculatingWay();

            CalculatingVolume();

            CalculatingPrice(trade);

            CalculatingMargin(trade, wayVolume);

            CalculatingCommission(trade);
        }

        /// <summary>
        /// Пересчет времени последней сделки в позиции
        /// </summary>
        /// <param name="trade"></param>
        private void CalculatingLastTradeTime(Trade trade)
        {
            _lastTradeTime = trade.DateTime;
        }

        /// <summary>
        /// Пересчет текущего направлния позиции
        /// </summary>
        private void CalculatingWay()
        {
            if (volumeLong > volumeShort)
            {
                _way = Way.Long;
            }
            else if (volumeLong < volumeShort)
            {
                _way = Way.Short;
            }
        }

        /// <summary>
        /// Пересчет текущего количества открытых контрактов в позиции
        /// </summary>
        private void CalculatingVolume()
        {
            _volume = Math.Abs(volumeLong - volumeShort);
        }

        /// <summary>
        /// Пересчет текущей стоимости открытых контрактов в позиции
        /// </summary>
        /// <param name="trade"></param>
        private void CalculatingPrice(Trade trade)
        {
            _price = _volume * trade.Price;
        }

        /// <summary>
        /// Пересчет текущей прибыли/убытка в позиции
        /// </summary>
        /// <param name="trade"></param>
        /// <param name="num"></param>
        private void CalculatingMargin(Trade trade, decimal wayVolume)
        {
            averagePrice += -trade.Price * wayVolume;

            _margin = averagePrice + _price;

            if (_way == Way.Long)
            {
                _margin = averagePrice + _price;
            }
            else
            {
                _margin = averagePrice - _price;
            }
        }

        /// <summary>
        /// Пересчет текущей накопленной комиссии в позиции
        /// </summary>
        /// <param name="trade"></param>
        private void CalculatingCommission(Trade trade)
        {
            _commission += trade.Commission;
        }

       #endregion
    }
}
