using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyConsole;


namespace MyConsole
{

    public class Trade
    {
        #region Fields

        /// <summary>
        /// Цена инструмента
        /// </summary>
        public decimal Price = 0;
        /// <summary>
        /// Название инструмента
        /// </summary>
        public string Seccode = "";
        /// <summary>
        /// код инструмента
        /// </summary>
        public string ClassCode = "";
        /// <summary>
        /// время сделки
        /// </summary>
        public DateTime DateTime = DateTime.MinValue;
        /// <summary>
        /// номер счета
        /// </summary>
        public string Portfolio = "";
        /// <summary>
        /// Код клиента
        /// </summary>
        public int ClientsCode = 0;
        /// <summary>
        /// Тип ордера
        /// </summary>
        public TypeOrder typeOrder;

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
        
        private decimal _volume = 0;

        #endregion

        
    }
}
