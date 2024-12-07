using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Setka
{
    public class Position
    {
        public decimal volumHist = 0, priceHist = 0, profitHist = 0, volumHistCost = 0;

        enum LongShort : byte
        {
            Short,
            Long
        }
        public Position()
        {
            Timer timer = new Timer(3000);
            // timer.Interval = 3000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        Random random = new Random();

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();
            LongShort posa = LongShort.Long;
            int num = random.Next(-10, 10);

            if (num > 0)
            {
                posa = LongShort.Long;
            }
            else if (num < 0)
            {
                posa = LongShort.Short;
            }
            trade.Volume = num;
            trade.Price = random.Next(70000, 80000);
            trade.SecCode = "Si";

            Sdelka(posa, trade.Volume, trade.Price, trade.SecCode);

            string str = "Volume = " + Math.Abs(num) + "/ Price = " + trade.Price.ToString();
            Console.WriteLine(str);
        }

        private void Sdelka(LongShort posa, decimal volum, decimal price, string secCode)
        {
            decimal profit = 0;

            if (volum != 0)
            {
                switch (posa)
                {
                    case LongShort.Short:
                        profit = volumHist * priceHist - Math.Abs(volum) * price;
                        Console.WriteLine("Сделка в " + posa);
                        Console.WriteLine("Результат: " + profit);
                        break;
                    case LongShort.Long:
                        profit = Math.Abs(volum) * price - volumHist * priceHist;
                        Console.WriteLine("Сделка в " + posa);
                        Console.WriteLine("Результат: " + profit);
                        break;

                }
                volumHist = volum;
                priceHist = price;
                profitHist += profit;
                volumHistCost += volum;
                Console.WriteLine("Общая прибыль с момента запуска робота: " + profitHist);
                Console.WriteLine("Текущая позиция по  " + secCode + "  : " + volumHistCost);
                Console.WriteLine("=======================================================================================");
            }

        }
    }
}