using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Trade
    {
        #region Fields

        /// <summary>
        /// Цена инструмента
        /// </summary>

        public decimal Price = 0;

        public int SellBuy = 0;

        public string SecCode = "";

        public string ClassCode = "";

        public DateTime DateTime = DateTime.MinValue;

        public string Portfolio = "";

        #endregion

        #region Properties

        public decimal Volume { get; set; }
        #endregion

        

    }
    public enum Direction
    {
        S_side = -1,
        L_side = 1
    }
}
