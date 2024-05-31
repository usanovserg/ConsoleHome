using System;

using System.Timers;

namespace ConsoleHome
{
    public class ClassPosition
    {
        // Метод - Конструктор класса
        public ClassPosition()
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
            ClassTrade trade = new ClassTrade();

            Int32 num = random.Next(-10, 10);

            // Биржа, аккаунт
            if (num % 2 == 0 && num != 0)
            {
                trade.ExchangeName = ClassExchange.MOEX;
                trade.TradingAccount = 24891;
            }
            else if (num % 2 != 0 && num != 0)
            {
                trade.ExchangeName = ClassExchange.NYSE;
                trade.TradingAccount = 51658;
            }
            else
            {
                trade.ExchangeName = ClassExchange.Bybit;
                trade.TradingAccount = 6152;
            }

            // Инструмент
            if (num % 2 == 0 && num >= 0)
            {
                trade.SecurityIndex = ClassSecurity.BR;
            }
            else if (num % 2 != 0 && num >= 0)
            {
                trade.SecurityIndex = ClassSecurity.NG;
            }
            else if (num % 2 == 0 && num < 0)
            {
                trade.SecurityIndex = ClassSecurity.GC;
            }
            else
            {
                trade.SecurityIndex = ClassSecurity.SI;
            }

            // Направление - Лонг или шорт
            if (num >= 0)
            {
                trade.TypeOrderTrade = ClassTypeOrder.Long;
            }
            else
            {
                trade.TypeOrderTrade = ClassTypeOrder.Short;
            }

            // Объём сделки
            trade.Volume = Math.Abs(num);

            // Чтобы не было сделок с нулевым объёмом
            if (trade.Volume == 0)
                trade.Volume = 1;

            // Цена сделки - диапазон в зависимости от инструмента
            if (trade.SecurityIndex == ClassSecurity.BR)
            {
                trade.Price = random.Next(55, 75);
            }

            if (trade.SecurityIndex == ClassSecurity.NG)
            {
                trade.Price = random.Next(2, 4);
            }

            if (trade.SecurityIndex == ClassSecurity.GC)
            {
                trade.Price = random.Next(1800, 2700);
            }

            if (trade.SecurityIndex == ClassSecurity.SI)
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
