using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class cTrade
    {
        // ====== FIELDS ===== \\
        #region 
        public DateTime dateTime = DateTime.MinValue;
        public decimal price = 0;
        public decimal lot = 0;
        public string assetName = "";
        public string codeClass = "";
        public string portfolio = "";
        public Direction direction = Direction.none;

        public enum Direction
        { 
            Long,
            Short,
            none
        }


        #endregion
        //\\==================//\\

        #region Properties
        
        public decimal Lot
        {
            get { return _lot; }
            set { _lot = value; }
        }   decimal _lot = 0;

        #endregion



        public string toString(Direction dir) 
        {
            switch(dir)
            {
                case Direction.Long: return "Long"; 
                case Direction.Short: return "Short";
                default:
                    return "none";

            }
        }
    }
}
