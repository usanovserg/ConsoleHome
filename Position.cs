using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace ConsoleHome {
    public class Position {
        public string sec_code;
        List<Trade> trades;
        public decimal total_vol;
        public decimal avg_price;
        public decimal live_value;
        public decimal fixed_value;
        public decimal last_price;
        public decimal closed_vol;

        public delegate void OnPositionChangeDelegate(decimal PositionSize);
        public event OnPositionChangeDelegate onPositionChangeDelegate;
        public Position(string sec_code) {
            this.sec_code = sec_code;
            total_vol = 0;
            fixed_value = 0;
            live_value = 0;
            last_price = 0;
            avg_price = 0;
            trades = new List<Trade>();
        }

        public void OnTrade(Trade trade) {
            if (trade == null) return;
            trades.Add(trade);

            decimal new_total_vol = total_vol + trade.vol;

            // фиксируем часть позиции
            if(Math.Sign(trade.vol) != Math.Sign(total_vol) && total_vol!=0) {
                if(Math.Abs(total_vol) > Math.Abs(trade.vol)) {
                    closed_vol += Math.Abs(trade.vol);
                    fixed_value += trade.vol * (avg_price - trade.price);
                }
            } else {
                if(new_total_vol != 0) {
                    decimal total = total_vol * avg_price + trade.price * trade.vol;
                    avg_price = total / new_total_vol;
                } else {
                    avg_price = 0;
                }
            }
            total_vol = new_total_vol;
        
            last_price = trade.price;

            live_value = (last_price-avg_price) * total_vol;
            onPositionChangeDelegate(total_vol);
        }
        public override string ToString() {
            return $"{sec_code} avg_price={avg_price:f2} vol={total_vol} last_price={last_price:f2} value={live_value+fixed_value:f2} live={live_value:f2} fixed={fixed_value:f2}";
        }
    }
}
