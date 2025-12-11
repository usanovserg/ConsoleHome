using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleHome
{
     public class Trade
    {
        
        public enum TransactionDirection 
        {
            Buy,
            Sell
        };

        /// <summary>
        /// Направление сделки
        /// </summary>
        public TransactionDirection direction;

        // =============================== Fiels ================================
        #region Fields

        /// <summary>
        /// Цена инструмента
        /// </summary>
        public decimal Price = 0;

        /// <summary>
        /// Наименование инструмента
        /// </summary>
        public string SecCode = "";

        /// <summary>
        /// Классификация
        /// </summary>        
        public string ClassCode = "";

        /// <summary>
        /// Время
        /// </summary>
        public DateTime DateTime = DateTime.MinValue;

        /// <summary>
        /// Номер счета (название портфеля)
        /// </summary>
        public string Portfolio = "";

        #endregion

        // =============================== Properties ================================
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

        // =============================== Methods ================================
        #region Methods



        #endregion
    }
}