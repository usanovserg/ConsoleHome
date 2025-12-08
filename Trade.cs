using ConsoleHome.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Trade
    {
        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Fields
        // Поля
        public decimal Price = 0;
        public TradeDirections TradeDirection = Enums.TradeDirections.Neutral;
        public string SecCode, ClassCode, Portfolio = "";
        //public string ClassCode = "";
        public DateTime DateTime = DateTime.MinValue;
        //public string Portfolio = "";
        // Свойства
        public decimal Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }
        decimal _volume = 0;



        #endregion
        //----------------------------------------------- Fields end -------------------------------------------------
    }
}
