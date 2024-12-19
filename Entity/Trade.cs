using ConsoleHome.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome.Entity
{
    public class Trade
    {

        public Direction Direction;

        private string trade_id = "";
        public string sec_code;
        public decimal vol;
        public decimal price;
        public Direction dir;
        public Trade(string sec_code, decimal price, decimal vol, Direction dir)
        {
            this.sec_code = sec_code;
            this.price = price;
            if (vol > 0 && dir == Direction.SELL || vol < 0 && dir == Direction.BUY || vol == 0)
            {
                throw new Exception("vol и dir не соответствуют друг другу");
            }
            this.vol = vol;
            this.dir = dir;
        }
        public override string ToString()
        {
            return $"Trade {sec_code} {price}@{vol} {dir}";
        }
    }


}
