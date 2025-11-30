using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    public class Trade
    {      

        //----------------------------------------------- Fields ---------------------------------------------------- 
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

        /// <summary>
        /// Направление торговли
        /// </summary>
       // public directionOfTrade DirectionOfTrade;
        public directionOfTrade DirectionOfTrade;

        /// <summary>
        /// Комиссия за сделку
        /// </summary>
        public   decimal Commission = 0;

        #endregion
        //----------------------------------------------- End Fields ---------------------------------------------------- 

        //----------------------------------------------- Properties ---------------------------------------------------- 
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
        //----------------------------------------------- End Properties ---------------------------------------------------- 

        /// <summary>
        /// Направление торговли (лонг, шорт)
        /// </summary>
       public enum directionOfTrade
        {
            Long,
            Short
        }

        public enum typeOfComission
        {
            Limit,
            Market
        }
        //----------------------------------------------- Methods ------------------------------------------------------
        public decimal GetCommission(string typeOfCommission)
        {
            var Commission = new Dictionary<string, decimal>()
                {
                    { "Limit", 0.18m },
                    { "Market", 1.05m }
                };

            try
            {
                return Commission[typeOfCommission];
            }
            catch (Exception)
            {
                throw new ArgumentException("Комиссия не известна!!! Требуется проверить!!!!!");
            }

        }
        //----------------------------------------------- End Methods ---------------------------------------------------- 
    }



}
