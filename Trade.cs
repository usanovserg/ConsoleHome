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
        
        public string _secCode = "";
        public string _classCode = "";
        DateTime DateTime = DateTime.MinValue;
        public string Portfolio = "";




        private decimal _volume = 0;
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


        public void Action()
        {
            _price = 0;
            _volume = 0;
        }






        //============================





    }
}
