using ConsoleHome.Enams;
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

        //public string name;
        public decimal Price = 0;
        public string SecCode = "SRZ3";
        public string ClassCode = "";
        public DateTime DateTime = DateTime.MinValue;
        public string Portfolio = "";
        public Operation Napr = Operation.None;
        



        #endregion

        #region Propertis

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
    }

}
