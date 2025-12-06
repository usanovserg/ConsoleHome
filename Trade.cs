using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    /// <summary>
    /// Класс, описывающий сделку.
    /// </summary>
    public class Trade
    {
        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Fields
        /// <summary>
        /// Цена инструмента
        /// </summary>
        public decimal price = 0;

        public decimal secCode = 0; // Код инструмента
        public string classCode = ""; // Код класса
        public DateTime dateTime = DateTime.MinValue; // Дата и время
        public string portfolio = ""; // портфель

        public enum DealDirection
        { 
            Long,
            Short,
            None
        }

        public enum DealType
        {
            Market,
            Limit
        }

        #endregion
        //-----------------------------------------------------------------------------------------------------------

        //----------------------------------------------- Properties ------------------------------------------------
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
        private decimal _volume = 0;

        #endregion
        //-----------------------------------------------------------------------------------------------------------
    }
}
