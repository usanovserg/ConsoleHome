using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyConsole;


namespace MyConsole
{
    public enum Direction
    {
        Long,
        Short
    }
    public class Trade
    {
        #region Fields //------------------------------------------------------------------//

        public decimal price = 0;
        public decimal volume = 0;

        /// <summary>
        /// код типа иструмента - фьючерс, акция и т.д.
        /// </summary>
        public string SecCode = "";
        public string ClassCode = "";
        public DateTime DateTime = DateTime.MinValue;
        public string Portfolio = "";
        public string ClientCode = "";
        public Direction Direction = Direction.Long;
        #endregion

        #region Properties  //------------------------------------------------------------------//

        #endregion

        #region Methods  //------------------------------------------------------------------//
        public Trade()
        {

        }
        #endregion

    }
}
