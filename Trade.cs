using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Trade
    {
        //================================================= Fields ======================================
        #region Fields

        /// <summary>
        /// цена актива
        /// </summary>
        public decimal Price = 0;       
        /// <summary>
        /// название инструмента
        /// </summary>
        public string? SecCode = "";
        /// <summary>
        /// класиффикация биржи
        /// </summary>
        public string? ClassCode = "";
        DateTime DateTime = DateTime.MinValue;
        /// <summary>
        /// название портфеля
        /// </summary>
        public string? Portfolie = "";
        /// <summary>
        /// напровление сделки
        /// </summary>
        public string DirectionTrade = "";
        /// <summary>
        /// имя актива
        /// </summary>
        public string AssetName = "WLD";
        /// <summary>
        /// цена входа
        /// </summary>
        public decimal PriceEnter = 0;
        /// <summary>
        /// цена выхода
        /// </summary>
        public decimal PriceExit = 0;

        public Direction direction;
        #endregion


        //====================================================== Properties ================================
        #region Properties
        /// <summary>
        /// объём сделки
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

        public enum Direction 
        {
            Long,
            Short
        }
        
        #endregion

    }
}
