using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace myConsole
{
    public class Position
    {
        public Position() // при создании экземпляра класса запускается конструктор класса
        {
            Timer timer = new Timer();  // создаем объект класса Timer

            timer.Interval = 3000;   // ставим в свойствах класса интервал (какого то действия)

            timer.Elapsed += NewTrade;  // после каждого интервала будет вызываться метод Timer_Elapsed

            timer.Start();   // запускаем метод Start
        }

        Random random = new Random();

        private void NewTrade(object? sender, ElapsedEventArgs e)  // метод работает в паралельном потоке
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);

            trade.Symbol = "Si";

            if (num > 0)
            {
                trade.direction = Direction.Buy;
            }
            else if (num < 0) 
            {
                trade.direction = Direction.Sell;
            }

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(70000, 80000);

            Trade.VolumePosition += num;

            Trade.AvgPrice = trade.Price;

            string str = $"Volume = {trade.Volume.ToString()} / Price = {trade.Price.ToString()} / Direction = {trade.direction} " +
                $" / Symbol = {trade.Symbol} / VolumePosition = {Trade.VolumePosition} / AvgPrice = {Trade.AvgPrice}";

            Console.WriteLine(str);
        }
    }
}
