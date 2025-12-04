using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleHome.Enum;

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


        #endregion

        #region Properties 

        /// <summary>
        /// Объём сделки
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
        //        =================================================== Новое растолковать===================================================
        public OrderSide Side { get; set; }   // Публичный класс со свойством Side куда можно внести или извлечь данные


    }

        #endregion
    
}


       
   

