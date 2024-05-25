using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleHome.Enums;

namespace ConsoleHome
{
    public class PositionChangedEventArgs : EventArgs
    {
        public Exchange ExchengeName { get; private set; }
        public decimal PositionVolume { get; private set; }

        public PositionChangedEventArgs(Exchange exchange, decimal positionVolume) 
        {
            ExchengeName = exchange;
            PositionVolume = positionVolume;
        }
    }
}
