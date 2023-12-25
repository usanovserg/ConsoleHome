using ConsoleHome.Enums;
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
        
        public decimal lot = 0;
        public decimal price = 0;


        public string codeClass = "";
        public string portfolio = "";

        #endregion
        //\\==================//\\

        #region Properties
        public Assets Asset 
        {
            get { return _assetName; }
            set { _assetName = value; }
        } public Assets _assetName;

        public decimal Lot
        {
            get { return _lot; }
            set { _lot = value; }
        }   decimal _lot = 0;

        #endregion

        #region Methods

        //Constructor
        public cTrade(DateTime now, Assets curAsset) 
        {
            dateTime = now;
            Asset = curAsset;
        }


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
        #endregion
    }
}
