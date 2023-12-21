using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleHome
{
    /// <summary>
    /// Информация о позиции
    /// </summary>
    public class Position
    {
        //===================== КОНСТРУКТОР =====================
        #region
        public Position()
        {
            System.Timers.Timer timer = new System.Timers.Timer();

            timer.Interval = 3000;              // каждые 3000 мс будет производится действие

            Console.WriteLine("Ждем " + timer.Interval / 1000 + " сек.");

            timer.Elapsed += NewTrade;          // вызов действия
            
            timer.Start();                      // запуск таймера в параллельном потоке

            ChangePosition += PrintChangePosition;  // подписка на событие
        }
        #endregion

        //===================== ПОЛЯ И СВОЙСТВА =====================
        #region
        /// <summary>
        /// Код клиента
        /// </summary>
        private uint clientCode = TradingAccount.ClientCode; // код клиента

        /// <summary>
        /// Код инструмента
        /// </summary>
        private string secCode = ""; // код инструмента

        /// <summary>
        /// Варианты позиции
        /// </summary>
        private enum Direction
        {
            None,
            Long,
            Short
        }

        /// <summary>
        /// Направление позиции
        /// </summary>
        private Direction direction = Direction.None; // направление позиции

        /// <summary>
        /// Предыдущее кол-во лотов в позиции
        /// </summary>
        private decimal prevQty = 0;    // предыдущее кол-во лотов в позиции

        /// <summary>
        /// Текущее кол-во лотов в позиции
        /// </summary>
        private decimal qty = 0;    // кол-во лотов в позиции

        /// <summary>
        /// Кол-во в 1 лоте
        /// </summary>
        private uint lot = 1;    // кол-во в 1 лоте

        /// <summary>
        /// Цена позиции
        /// </summary>
        private decimal price = 0;   // цена позиции

        /// <summary>
        /// Шаг цены
        /// </summary>
        private decimal secPrise = 0.01m; // шаг цены

        /// <summary>
        /// Стоимость позиции
        /// </summary>
        private decimal value = 0;   // стоимость позиции

        /// <summary>
        /// Цена стоп-лосса
        /// </summary>
        private decimal priceStop;  // цена стоп-лосса
        public decimal PriceStop
        {
            get { return priceStop; }
            set 
            {
                if (direction == Direction.Long)
                {
                    priceStop = price - secPrise * 20;
                }
                else if (direction == Direction.Short)
                {
                    priceStop = price + secPrise * 20;
                }
                else
                {
                    priceStop = 0;
                }
            }
        }

        /// <summary>
        /// Цена тейк-профита
        /// </summary>
        private decimal priceProfit;  // цена тейк-профита
        public decimal PriceProfit
        {
            get { return priceProfit; }
            set
            {
                if (direction == Direction.Long)
                {
                    priceProfit = price + secPrise * 60;
                }
                else if (direction == Direction.Short)
                {
                    priceProfit = price - secPrise * 60;
                }
                else
                {
                    priceProfit = 0;
                }
            }
        }
        #endregion

        //===================== МЕТОДЫ =====================
        #region
        Random random = new Random();

        /// <summary>
        /// Генерация новой сделки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int num = random.Next(-4, 4);     // генерация количества в сделке

            trade.qty = Math.Abs(num);

            trade.price = random.Next(1, 9);

            if (num > 0)
            {
                // сделка в лонг
                trade.direction = Trade.Direction.Buy;

                if (direction == Direction.None || direction == Direction.Long) // если лонговая позиция или ее нет
                {
                    direction = Direction.Long;

                    PlusPosition(trade.qty, trade.price);
                }
                else
                {
                    if (qty < trade.qty)   // если шортовая позиция <= кол-ва в сделке на покупку
                    {
                        direction = Direction.Long;

                        MinusPosition(trade.qty, trade.price);
                    }
                    else if (qty == trade.qty)   // если шортовая позиция = кол-ва в сделке на покупку
                    {
                        direction = Direction.None;

                        ZeroPosition();
                    }
                    else                    // иначе шортовая позиция > кол-ва в сделке на покупку
                    {
                        direction = Direction.Short;

                        MinusPosition(trade.qty, trade.price);
                    }
                }
            }
            else if (num < 0)
            {
                // сделка в шорт
                trade.direction = Trade.Direction.Sell;

                if (direction == Direction.None || direction == Direction.Short) // если шортовая позиция или ее нет
                {
                    direction = Direction.Short;

                    PlusPosition(trade.qty, trade.price);
                }
                else
                {
                    if (qty <= trade.qty)   // если лонговая позиция <= кол-ва в сделке на продажу
                    {
                        direction = Direction.Short;

                        PlusPosition(trade.qty, trade.price);
                    }
                    else if (qty == trade.qty)   // если лонговая позиция = кол-ва в сделке на продажу
                    {
                        direction = Direction.None;

                        ZeroPosition();
                    }
                    else                    // иначе лонговая позиция > кол-ва в сделке на продажу
                    {
                        direction = Direction.Long;

                        MinusPosition(trade.qty, trade.price);
                    }
                }
            }

            if (num != 0)       // кол-во в сделке не может быть равно 0
            {
                string str = "Сделка:\t\t" + trade.direction.ToString() + "\tкол-во: " + trade.qty.ToString() + "\tцена: " + trade.price.ToString();

                Console.WriteLine(str);

                ChangePosition(prevQty, qty);   // вызов события

                PrintPosition();

                Console.WriteLine("****************");
            }
        }

        /// <summary>
        /// Увеличение позиции
        /// </summary>
        /// <param name="tradeQty"></param>
        /// <param name="tradePrice"></param>
        private void PlusPosition(decimal tradeQty, decimal tradePrice)
        {
            if (qty + tradeQty != 0)
            {
                prevQty = qty;      // запомнили предыдущее кол-во лот в позиции

                qty += tradeQty;    // кол-во лот в покупке

                value += tradeQty * lot * tradePrice;    // стоимость позиции

                price = Math.Round(value / qty, 2, MidpointRounding.ToZero);    // цена позиции

                PriceStop = tradePrice;

                PriceProfit = tradePrice;
            }
            else ZeroPosition();
        }

        /// <summary>
        /// Уменьшение позиции
        /// </summary>
        /// <param name="tradeQty"></param>
        /// <param name="tradePrice"></param>
        private void MinusPosition(decimal tradeQty, decimal tradePrice)
        {
            if (qty - tradeQty != 0)
            {
                prevQty = qty;      // запомнили предыдущее кол-во лот в позиции

                qty -= tradeQty;    // кол-во лот в продаже

                value -= tradeQty * lot * tradePrice;    // стоимость позиции

                price = Math.Round(value / qty, 2, MidpointRounding.ToZero);    // цена позиции

                PriceStop = tradePrice;

                PriceProfit = tradePrice;
            }
            else ZeroPosition();
        }

        /// <summary>
        /// Закрытие позиции
        /// </summary>
        private void ZeroPosition()
        {
            prevQty = qty;      // запомнили предыдущее кол-во лот в позиции

            qty = 0;      // кол-во лот в продаже

            value = 0;    // стоимость позиции

            price = 0;    // цена позиции

            PriceStop = 0;

            PriceProfit = 0;
        }

        /// <summary>
        /// Вывод позиции на консоль
        /// </summary>
        public void PrintPosition()
        {
            string dir = "";

            if (direction == Direction.None)
            {
                dir = "None";
            }
            else if (direction == Direction.Short)
            {
                dir = "Short";
            }
            else
            {
                dir = "Long";
            }

            string str = "Позиция:\t" + dir + "\tкол-во: " + qty.ToString() + "\tцена: " + price.ToString() + " \tсумма: " + value.ToString();

            Console.WriteLine(str);

            Console.WriteLine("\tСтоп-лосс: " + priceStop + " ; Тейк-профит: " + priceProfit);
        }

        /// <summary>
        /// Вывод изменения позиции (по событию)
        /// </summary>
        /// <param name="oldQty"> предыдущая позиция </param>
        /// <param name="newQty"> текущая позиция </param>
        public void PrintChangePosition(decimal oldQty, decimal newQty)
        {
            Console.WriteLine($"Позиция изменилась: была {oldQty}, стала: {newQty}");
        }

        #endregion

        //===================== ДЕЛЕГАТЫ И СОБЫТИЯ =====================
        #region
        public delegate void Handler(decimal x1, decimal x2);

        /// <summary>
        /// Событие: изменение позиции
        /// </summary>
        public event Handler ChangePosition;
        #endregion
    }


}
