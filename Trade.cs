using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    public class Trade
    {

        //----------------------------------------------- Properties ---------------------------------------------------- #region Fields
        #region Properties
        //  Параметры сделки

        /// <summary>
        /// Цена инструмента
        /// </summary>
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// Наименование инструмента
        /// </summary>
        public string SecCode { get; set; } = ""; 

        /// <summary>
        /// Классификация
        /// </summary>
        public string ClassCode { get; set; } = "";

        /// <summary>
        /// Время
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Портфель (номер счета)
        /// </summary>
        public string Portfolio { get; set; } = "";

        /// <summary>
        /// Направление торговли
        /// </summary>
        public string DirectionOfTrade { get; set; } = "";                  

        /// <summary>
        /// Средняя цена
        /// </summary>
        public decimal AveragePrice { get; set; } = 0;
            
              
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
        //----------------------------------------------- End Properties ---------------------------------------------------- 
       
    }
}
