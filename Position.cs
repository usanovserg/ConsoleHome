using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace ConsoleHome
{
    public class Position
    {
        readonly Random rand = new();
        public static decimal Volume = 0;
        public static decimal AvPrice = 0;

        public Position()
        {
            System.Timers.Timer timer = new(2000);
            timer.Elapsed += NewTrade;
            timer.Start();
        }


        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            int num = rand.Next(-10, 10);
            Trade trade = new();
            trade.Price = rand.Next(70000, 80000);
            trade.Volume = Math.Abs(num);

            if (num > 0) //сделка в лонг
            {
                trade.Direction = Direction.Long;
                Volume += trade.Volume;
                //AvPrice = (AvPrice == 0) ? trade.Price : (Volume * AvPrice + trade.Volume * trade.Price) / (Volume + trade.Volume);
            }
            else if (num < 0) //сделка в шорт
            {
                trade.Direction = Direction.Short;
                Volume -= trade.Volume;
                //AvPrice = (AvPrice == 0)? trade.Price : (Volume * AvPrice - trade.Volume * trade.Price) / (Volume - trade.Volume);
            }
            else //нет сделки
                return;

            Console.WriteLine($"TRADE Volume:\t{trade.Volume.ToString()}\t  Price:\t{trade.Price.ToString()}\tDirection:{trade.Direction.ToString()} ");
            Console.WriteLine($"POSITION Vol:\t{Volume}\tAvPrice:\t{AvPrice}\n");

        }
    }    
            
}
