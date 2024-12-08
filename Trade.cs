using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myConsole
{
    public class Trade
    {
        //============================================ Fields ==========================================

        #region Fields

        public decimal Price = 0;

        public Direction direction;  // enum для направления ордера

        public string Symbol = "";  // инструмент для торговли

        public static decimal VolumePosition = 0;  // объем полной позиции

        public string SecCode = ""; // название инструмента

        public string ClassCode = ""; // классификация биржы

        public DateTime DateTime = DateTime.MinValue;

        public string Portfolio = ""; // номер счета

        #endregion

        //=========================================== Properties ========================================

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

        public static decimal AvgPrice  // средняя цена позиции для примера (расчитывается не правильно)
        {
            get
            {
                return _avgPrice;
            }
            set
            {
                _avgPrice = value;
            }
        }
        public static decimal _avgPrice = 0;

        #endregion

        //============================================ Methods ==========================================

        #region Methods



        #endregion


    }
}

