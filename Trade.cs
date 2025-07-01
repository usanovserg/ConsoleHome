using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Trade
    {
        public decimal _price = 0;
        public decimal _volume = 0;
        public string _secCode = "";
        public string _classCode = "";
        DateTime DateTime = DateTime.MinValue;
        public string Portfolio = "";

        static int Volume
        {
            get;

            set;
        }

        public void Action()
        {
            _price = 0;
            _volume = 0;
        }






        //============================





    }
}
