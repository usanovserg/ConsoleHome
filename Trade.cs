using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
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

        /// <summary>
        /// Направление сделок (Long; None; Short)
        /// </summary>
        public enum TypeTrade 
        {
            Short =-1,
            None = 0,
            Long = 1
        }

        public TypeTrade Side = TypeTrade.None;

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

        //----------------------------------------------- MyTransaction ------------------------------
        #region MyTransaction

        /// <summary>
        /// Направление сделки: Bay (1) ; Sell (-1)
        /// </summary>
        public enum MyTransaction
        {
            Sell = -1,
            Bay = 1            
        }
        
        #endregion
        //----------------------------------------------- MyTransaction ------------------------------
    }

}
