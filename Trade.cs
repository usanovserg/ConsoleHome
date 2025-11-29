using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    public class Trade
    {
        #region Fields
        //  Параметры сделки

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
        /// Портфель (номер счета)
        /// </summary>
        public string Portfolio = ""; 

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

    }
}
