using System;

using System.Timers;

namespace ConsoleHome
{
    public class Position
    {
        // Метод - Конструктор класса
        public Position()
        {
            Timer timer = new Timer();

            timer.Interval = 1500;

            // Срабатывает по истечении заданного интервала
            timer.Elapsed += Timer_Elapsed;

            timer.Start();
        }

        Random random = new Random();

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e) 
        {
            Trade trade = new Trade();

            Int32 num = random.Next(-10, 10);

            if (num > 0)
            {
                // Сделка в лонг

            }
            else if (num < 0)
            {
                // Сделка в шорт
                
            }

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(70000, 80000);

            String str = "Volume = " + trade.Volume.ToString() + " | Price = " + trade.Price.ToString();

            Console.WriteLine(str);
        }

        // Добавить поля или свойства, которые описывают позицию: кол-во лотов, цена сделки, инструмент, .... что-то еще

        // Добавить метод, который получает новую сделку. По сути он уже создан - Timer_Elapsed
        // Переименовать в NewTrade
    }
}
