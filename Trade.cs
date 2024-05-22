using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleHome.Enums;

namespace ConsoleHome
{
    public class Trade
    {

        #region Fields

        /// <summary>
        /// Цена инструмента
        /// </summary>

        public decimal Price = 0;

        public string SecCode = "";

        public string ClassCode = "";

        public DateTime DateTime = DateTime.MinValue;

        public string Portfolio = "";

        public OrderType OrderType;

        public double Commision;

        public int PositionVolume;

        public string? AccountNumber;



        #endregion

        #region Properties

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

        #endregion

        #region Methods



        #endregion
    }
}
