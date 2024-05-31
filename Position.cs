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
            timer.Elapsed += NewTrade;

            timer.Start();
        }

        Random random = new Random();

        private void NewTrade(object? sender, ElapsedEventArgs e) 
        {
            Trade trade = new Trade();

            Int32 num = random.Next(-10, 10);

            // Биржа, аккаунт
            if (num % 2 == 0 && num != 0)
            {
                trade.ExchangeName = Exchange.MOEX;
                trade.TradingAccount = 24891;
            }
            else if (num % 2 != 0 && num != 0)
            {
                trade.ExchangeName = Exchange.NYSE;
                trade.TradingAccount = 51658;
            }
            else
            {
                trade.ExchangeName = Exchange.Bybit;
                trade.TradingAccount = 6152;
            }

            // Инструмент
            if (num % 2 == 0 && num >= 0)
            {
                trade.SecurityIndex = Security.BR;
            }
            else if (num % 2 != 0 && num >= 0)
            {
                trade.SecurityIndex = Security.NG;
            }
            else if (num % 2 == 0 && num < 0)
            {
                trade.SecurityIndex = Security.GC;
            }
            else
            {
                trade.SecurityIndex = Security.SI;
            }

            // Направление - Лонг или шорт
            if (num >= 0)
            {
                trade.TypeOrderTrade = TypeOrder.Long;
            }
            else
            {
                trade.TypeOrderTrade = TypeOrder.Short;
            }

            // Объём сделки
            trade.Volume = Math.Abs(num);

            // Чтобы не было сделок с нулевым объёмом
            if (trade.Volume == 0)
                trade.Volume = 1;

            // Цена сделки - диапазон в зависимости от инструмента
            if (trade.SecurityIndex == Security.BR)
            {
                trade.Price = random.Next(55, 75);
            }

            if (trade.SecurityIndex == Security.NG)
            {
                trade.Price = random.Next(2, 4);
            }

            if (trade.SecurityIndex == Security.GC)
            {
                trade.Price = random.Next(1800, 2700);
            }

            if (trade.SecurityIndex == Security.SI)
            {
                trade.Price = random.Next(1400, 2400);
            }

            // Строка вывода
            String str = "Exchange = " + trade.ExchangeName
                + " | Account = " + trade.TradingAccount
                + " | Security = " + trade.SecurityIndex
                + " | TypeOrder = " + trade.TypeOrderTrade
                + " | Volume = " + trade.Volume.ToString()
                + " | Price = " + trade.Price.ToString();

            Console.WriteLine(str);
        }

        // Задание
        // Добавить поля или свойства, которые описывают позицию: кол-во лотов, цена сделки, инструмент, .... что-то еще
        // Добавить метод, который получает новую сделку. По сути он уже создан - Timer_Elapsed. Переименовать в NewTrade.
    }
}
