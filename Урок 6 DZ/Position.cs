using MyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static MyConsole.Trade;
using Timer = System.Timers.Timer;

namespace MyConsole
{
    public class Position
    {
        public delegate void PositionChangeCallback(decimal volume, decimal price);
        public event PositionChangeCallback positionChange;

        readonly Random rand = new Random();
        public static decimal volume = 0;
        public static decimal averagePrice = 0;


        public Position()
        {
            System.Timers.Timer timer = new System.Timers.Timer(2000);
            timer.Elapsed += NewTrade;
            timer.Start();
        }

        private decimal calculateAverage(Trade trade)
        {
            if (trade.Direction == Direction.Short)
            {
                if (volume - trade.volume == 0)
                {
                    return trade.price;
                }

                return (volume * averagePrice - trade.volume * trade.price) / (volume - trade.volume);
            }
            else
            {
                return (volume * averagePrice + trade.volume * trade.price) / (volume + trade.volume);
            }
        }

        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            int num = rand.Next(-10, 10);
            Trade trade = new Trade();
            trade.price = rand.Next(4000, 5000);
            trade.volume = Math.Abs(num);

            if (num > 0) //сделка в лонг
            {
                trade.Direction = Direction.Long;
                averagePrice = this.calculateAverage(trade);
                volume += trade.volume;
            }
            else if (num < 0) //сделка в шорт
            {
                trade.Direction = Direction.Short;
                averagePrice = this.calculateAverage(trade);
                volume -= trade.volume;
            }
            else //нет сделки
                return;
            Console.WriteLine($"TRADE Volume:\t{trade.volume.ToString()}\t  Price:\t{trade.price.ToString()}\tDirection:{trade.Direction.ToString()} ");
            
            positionChange.Invoke(volume, averagePrice);
        }

    }
}
