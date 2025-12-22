using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    /// <summary>
    /// Класс управления отдельной сделкой (Шорт или Лонг);
    /// </summary>
    public class Trade
    {
        public Trade()
        {
            
        }
        //----------------------------------------------- Fields ------------------------------
        #region Fields
        /// <summary>
        /// Цена инструмента
        /// </summary>
        public decimal Price = 0;

        public string SecCode = "";

        public string ClassCode = "";

        public DateTime dateTime = DateTime.MinValue;

        public string Portfolio = "";

        public TypeTransaction Side { get; set; }

        public TypeTrade Position { get; set; }

        #endregion
        //----------------------------------------------- Fields ------------------------------

        //----------------------------------------------- Properties ------------------------------
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
        //----------------------------------------------- Properties ------------------------------

    }

}
