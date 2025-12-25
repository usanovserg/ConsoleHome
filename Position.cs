using MyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace MyConsole
{
    public class Position
    {
        public Position()
        {
            Timer timer = new Timer();

            timer.Interval = 3000;

            timer.Elapsed += NewTrade;

            timer.Start();

        }

        string instrument = "";
        decimal openPrice = 0;
        int volume = 0;
        string trend = "";
        decimal result = 0;


        Random random = new Random();

        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);
            string str;
            trade.Volume = Math.Abs(num);

            if (num > 0)
            {
                //Сделка в лонг
                trade.Price = random.Next(500, 1000);
                CheckPosition(Math.Abs(num), trade.Price, Trend.Long);
                str = "Новая сделка \n Volume = " + trade.Volume.ToString() + " / Prise = " + trade.Price.ToString() + " " + Trend.Long.ToString();
                Console.WriteLine(str);
            }
            else if (num < 0) 
            {
                // Сделка в шорт
                trade.Price = random.Next(500, 1000); 
                CheckPosition(Math.Abs(num), trade.Price, Trend.Short);
                str = "Новая сделка \n Volume = " + trade.Volume.ToString() + " / Prise = " + trade.Price.ToString() + " " + Trend.Short.ToString();
                Console.WriteLine(str);
            }


            str =  "Результат торговли после сделки: " + result.ToString("N3");

            Console.WriteLine(str);

            str = "Новая позиция: \n Напрвление " + trend.ToString() + 
                " Размер позиции: " + volume.ToString() + 
                " лота(ов)  Цена открытия: " + openPrice.ToString(); 

            Console.WriteLine(str);
        }

        public enum Trend
        {
            Short = -1,
            Long = 1
        }

        public void CheckPosition(int newTrade, decimal priceTrade, Trend newTrend)
        {
            if ((trend == "short" && newTrend == Trend.Long) || (trend == "long" && newTrend == Trend.Short))
            {
                if (volume < newTrade)
                {
                    result += volume * (openPrice - priceTrade) * (int)newTrend;
                    volume = newTrade - volume;
                    openPrice = priceTrade;
                }

                else if (volume > newTrade)
                {
                    result += newTrade * (openPrice - priceTrade) * (int)newTrend;
                    volume -= newTrade;
                }
                else if (volume == newTrade)
                {
                    openPrice = 0;
                    trend = "";
                }
            }
            else if ((trend == "long" && newTrend == Trend.Long) || (trend == "short" && newTrend == Trend.Short)) 
            {
                openPrice = ((openPrice * volume) + (priceTrade * newTrade))/(volume + newTrade);
                volume += newTrade;
            }
            else if (trend == "")
            {
                openPrice = priceTrade;
                volume = newTrade;
                if (newTrend == Trend.Long) trend = "long";
                else if (newTrend == Trend.Short) trend = "short";
            }
        }

    }
}
