using ConsoleHome.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using Timer = System.Timers.Timer;


namespace ConsoleHome
{
    public class Position
    {

        public Position(int interval,
                            string name) // создаем конструктор класса Position  
        {
            _name = name + " ===== ";

            Timer timer = new Timer(); // создаем объект Timer, из пространства имен (добавляем пространство имен using System.Timers;)

            timer.Interval = interval; // берем объект класса и задаем ему свойство Interval

            timer.Elapsed += NewTrade; // подписываемся на событие += NewTrade, который производит действия каждые 5 сек

            timer.Start(); // запускаем таймер с помощью метода Start()           
        }

        Random random = new Random();

        int _totalVolium = 0; // локальная переменаая для расчета общей позиции

        string _name = string.Empty;


        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade(); // создаем экземпляр класса Trade

            int num = random.Next(-10, 10); // в переменную num присваиваем значения из random от -10 до +10

            if (num > 0)
            {
                trade.TradeDirection = Direction.Buy; // Сделка в Buy
            }
            else if (num < 0)
            {
                trade.TradeDirection = Direction.Sell; // Сделка в Sell
            }
            else if (num == 0)
            {
                trade.TradeDirection = Direction.None; // Нет сделки
            }

            _totalVolium += num; // расчет общей позиции

            if (_totalVolium > 0)
            {
                trade.TradeTotalPosition = TotalPosition.Long; // Total Long
            }
            else if (_totalVolium < 0)
            {
                trade.TradeTotalPosition = TotalPosition.Short; // Total Short
            }
            else if (_totalVolium == 0)
            {
                trade.TradeTotalPosition = TotalPosition.No_Pozition; // Нет позиции
            }

            trade.Volium = Math.Abs(num);

            trade.TotalVolium = Math.Abs(_totalVolium);

            trade.Price = random.Next(70000, 80000);

            string str = _name + DateTime.Now.ToString() + " / Лот =" + trade.Volium.ToString() + " / Цена открытие = " + trade.Price.ToString()
                + " / Направление = " + trade.TradeDirection.ToString() + " / Общая позиция = "
                + trade.TradeTotalPosition.ToString() + " = " + trade.TotalVolium.ToString();

            Console.WriteLine(str);
        }
    }
}

