using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;


namespace ConsoleHome
{
    public class Position
    {

        public Position()
        {
            System.Timers.Timer timer = new System.Timers.Timer();

            timer.Interval = 1000;

            timer.Elapsed += NewTrade;

            timer.Start();
        }
        Random random = new Random();

        private void NewTrade(object sender, EventArgs a)
        {
            Trade trade = new Trade();

            string instrument = trade.Instrument;

            decimal price = trade.Price;

            int portfolio = trade.Portfolio;

            int num = random.Next(-10, 10);

            string direction = " ";

            if (num > 0)
            {
                direction = " Лонг";
                trade.Price = random.Next();
            }
            else if (num < 0)
            {
                direction = " Шорт";
                trade.Price = random.Next();
            }
            else if (num == 0)
            {
                direction = "Без сделки";
                price = 0;
            }

            trade.Volume = Math.Abs(num);

            trade.Price = price;   //не выяснил как переписать что б не дублировать (цена в консоли слишком большие )   

            //string str = "Cчет: " + trade.Portfolio.ToString() + " тикер = " trade.Instrumet.ToString() + " Кол-во лотов = " + trade.Volume.ToString() + "-- Цена = " + trade.Price.ToString();

            string str = $"Счет: {trade.Portfolio} -- Тикер: {trade.Instrument}  -- Кол-во лотов: {trade.Volume}  -- Цена: {trade.Price} --  Сделка в: {direction}    -- Дата: {trade.DateTime}";

            Console.WriteLine(str);

        }
    }
}
