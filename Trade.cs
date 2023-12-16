using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    /// <summary>
    /// Направление сделки (long, short)
    /// </summary>
    public enum Way : byte //сделал общим для всего пространства имен
    {
        Long, Short
    }

    /// <summary>
    /// Сделка
    /// </summary>
    public class Trade
    {
        #region  =========================================================Constructor=========================================================
        public Trade()
        {
            Random random = new Random();

            //инициализация свойства Volume
            _volume = random.Next(-10, +10);

            while (_volume == 0) //исключаем нулевые значения
            {
                _volume = random.Next(-10, +10);
            }

            //инициализация свойства Way, коррекция Volume по ABS
            if (_volume < 0) //сделка в Short (по умолчанию - в Long)
            {
                _way = Way.Short;

                _volume = -_volume;
            }

            //инициализация свойства Commission
            _commission = random.Next(1, 20) * _volume;

            //инициализация свойства Price
            _price = random.Next(89000, 90000);

            //инициализация свойства AmountPrice
            _amountPrice = _price * _volume;
        }
        #endregion

        #region =========================================================Fields=========================================================

        /// <summary>
        /// Код бумаги (инструмент)
        /// </summary>
        public string SecCode = "";

        /// <summary>
        /// Классификатор на бирже (акции, фьючи, валюта ...)
        /// </summary>
        public string ClassCode = "";

        /// <summary>
        /// Время сделки
        /// </summary>
        public DateTime DateTime = DateTime.Now;

        /// <summary>
        /// Код торгового счета (портфель)
        /// </summary>
        public string Portfolio = "";

        #endregion

        #region =========================================================Properties======================================================

        /// <summary>
        /// Направление сделки (по умолчанию Long)
        /// </summary>         
        public Way Way
        {
            get
            {
                return _way;
            }
            set
            {
                _way = value;
            }
        }
        Way _way = Way.Long;

        /// <summary>
        /// Объем сделки
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
            }
        }
        decimal _volume = 0;

        /// <summary>
        /// Цена сделки (за один контракт)
        /// </summary>
        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {

                _price = value;
            }
        }
        decimal _price = 0;

        /// <summary>
        /// Сумма сделки (Price*Volume)
        /// </summary>
        public decimal AmountPrice
        {
            get
            {
                return _amountPrice;
            }
            set
            {

                _amountPrice = value;
            }
        }
        decimal _amountPrice = 0;

        /// <summary>
        /// Комиссия по сделке
        /// </summary>
        public decimal Commission
        {
            get
            {
                return _commission;
            }
            set
            {

                _commission = value;
            }
        }
        decimal _commission = 0;


        #endregion

        #region =========================================================Methods=========================================================



        #endregion

    }
}