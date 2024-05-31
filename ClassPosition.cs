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
                trade.TraderAccount = 248915817;
            }
            else if (num % 2 != 0 && num != 0)
            {
                trade.ExchangeName = ClassExchange.NYSE;
                trade.TraderAccount = 516589157;
            }
            else
            {
                trade.ExchangeName = ClassExchange.Bybit;
                trade.TraderAccount = 615375912;
            }

            // Текущие дата и время
            trade.DateTime = DateTime.Now;

            // Класс инструмента
            trade.SecurityClass = ClassSecurityClass.Futures;

            // Инструмент
            if (num % 2 == 0 && num >= 0)
            {
                trade.SecurityCode = ClassSecurity.BR;
            }
            else if (num % 2 != 0 && num >= 0)
            {
                trade.SecurityCode = ClassSecurity.NG;
            }
            else if (num % 2 == 0 && num < 0)
            {
                trade.SecurityCode = ClassSecurity.GC;
            }
            else
            {
                trade.SecurityCode = ClassSecurity.SI;
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
                trade.Volume = random.Next(1, 10);

            // Цена сделки - диапазон в зависимости от инструмента
            if (trade.SecurityCode == ClassSecurity.BR)
            {
                trade.Price = random.Next(55, 75);
            }

            if (trade.SecurityCode == ClassSecurity.NG)
            {
                trade.Price = random.Next(2, 4);
            }

            if (trade.SecurityCode == ClassSecurity.GC)
            {
                trade.Price = random.Next(1800, 2700);
            }

            if (trade.SecurityCode == ClassSecurity.SI)
            {
                trade.Price = random.Next(1400, 2400);
            }

            // Строка вывода
            String str = "Exchange = " + trade.ExchangeName
                + " | Account = " + trade.TraderAccount
                + " | DateTime = " + trade.DateTime.ToString()
                + " | SecurityClass = " + trade.SecurityClass
                + " | Security = " + trade.SecurityCode
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
