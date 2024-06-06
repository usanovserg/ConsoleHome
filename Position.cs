using System;

using System.Timers;

namespace ConsoleHome
{
    public class Position
    {
        // Делегат, через который будет сообщаться об изменении позиции
        public delegate void ChangePosition(String exchange, String securityCode, String typeOrder, Decimal price, UInt32 volume);

        // Свойство, через которое можно зарегистрировать ссылку на метод
        // Через него внешний код сможет добавлять методы для оповещения
        public ChangePosition PositionChangeHandler { get; set; }

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
                trade.TraderAccount = 248915817;
            }
            else if (num % 2 != 0 && num != 0)
            {
                trade.ExchangeName = Exchange.NYSE;
                trade.TraderAccount = 516589157;
            }
            else
            {
                trade.ExchangeName = Exchange.Bybit;
                trade.TraderAccount = 615375912;
            }

            // Текущие дата и время
            trade.DateTime = DateTime.Now;

            // Класс инструмента
            trade.SecurityClass = SecurityClass.Futures;

            // Инструмент
            if (num % 2 == 0 && num >= 0)
            {
                trade.SecurityCode = Security.BR;
            }
            else if (num % 2 != 0 && num >= 0)
            {
                trade.SecurityCode = Security.NG;
            }
            else if (num % 2 == 0 && num < 0)
            {
                trade.SecurityCode = Security.GC;
            }
            else
            {
                trade.SecurityCode = Security.SI;
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
                trade.Volume = random.Next(1, 10);

            // Цена сделки - диапазон в зависимости от инструмента
            if (trade.SecurityCode == Security.BR)
            {
                trade.Price = random.Next(57, 64);
            }

            if (trade.SecurityCode == Security.NG)
            {
                trade.Price = random.Next(2, 4);
            }

            if (trade.SecurityCode == Security.GC)
            {
                trade.Price = random.Next(1880, 2120);
            }

            if (trade.SecurityCode == Security.SI)
            {
                trade.Price = random.Next(1450, 1600);
            }

            // Передача данных об изменённой позиции
            PositionChangeHandler(trade.ExchangeName.ToString(), trade.SecurityCode.ToString(),
                trade.TypeOrderTrade.ToString(), trade.Price, ((UInt32)trade.Volume));

            // Строка вывода
            //String str = "Exchange = " + trade.ExchangeName
            //    + " | Account = " + trade.TraderAccount
            //    + " | DateTime = " + trade.DateTime.ToString()
            //    + " | SecurityClass = " + trade.SecurityClass
            //    + " | Security = " + trade.SecurityCode
            //    + " | TypeOrder = " + trade.TypeOrderTrade
            //    + " | Volume = " + trade.Volume.ToString()
            //    + " | Price = " + trade.Price.ToString();

            //Console.WriteLine(str);
        }
    }
}
