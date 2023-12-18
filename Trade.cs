using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    public class Trade
    {
        public int Volume;
        public int Price;
        
        public int OpenPrice;
        public int k;
      
        public enum Direction 
        {
            Long = 1,
            Short = -1,
            
        }
        private int OpenPos;

        public int OpenPosition
        {
            get 
            { 
                return OpenPos;
            }
            set 
            { 
                OpenPos = OpenPos + value;
                
            }
        }

    }
}
