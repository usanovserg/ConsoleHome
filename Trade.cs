using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexHolyGun
{
    public class Trade
    {
        #region Fields //------------------------------------------------------------------//
        public enum Dir
        {
            Long,
            Short
        }
        public decimal Price = 0;
        public decimal Volume = 0;

        /// <summary>
        /// код типа иструмента - фьючерс, акция и т.д.
        /// </summary>
        public string SecCode = "";
        public string ClassCode = "";
        public DateTime DateTime = DateTime.MinValue;
        public string Portfolio = "";
        public string ClientCode = "";
        public Dir Direction = Dir.Long;
        #endregion
        
        #region Properties  //------------------------------------------------------------------//

        #endregion
        
        #region Methods  //------------------------------------------------------------------//
        public Trade() {
        
        }
        #endregion

    }
}
