using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace ConsoleHome
{
    /// <summary>
    ///  Класс, описывающий наши активы
    /// </summary>
    public class Position
    {

        public enum PositionDirection
        {
            None, Long, Short
        }

        #region Properties

        /// <summary>
        /// Название инстумента
        /// </summary>
        public string toolName;

        /// <summary>
        /// Список сделок по позиции
        /// </summary>
        public List<Trade> tradeList = new List<Trade>();


        /// <summary>
        /// Торговля - лонг/шорт
        /// </summary>
        public PositionDirection direction = PositionDirection.None;

        /// <summary>
        /// Текущая цена
        /// </summary>
        public decimal currentPrice;

        /// <summary>
        /// Кол-во лотов
        /// </summary>
        public decimal numPos = 0;

        /// <summary>
        /// Средняя цена позиции
        /// </summary>
        public decimal averagePrice  = 0;

        /// <summary>
        /// Реализованная прибыль/убыток
        /// </summary>
        public decimal realizedPnL = 0;

        #endregion

        public Position(string name)
        {
            toolName = name;

            System.Timers.Timer timer = new System.Timers.Timer
            {
                Interval = 5000
            };

            timer.Elapsed += Timer_Elapsed;

            timer.Start();
        }

        Random random = new Random();

        #region Methods
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Trade trade = CreateNewTrade();

            if (trade != null)
            {
                UpdatePosition(trade);
            }           
        }

        public Trade CreateNewTrade()
        {
            Trade trade = new Trade();

            // количество лотов в сделке
            int num = random.Next(-10, 10);

            if (num > 0)
            {
                trade.direction = Trade.TransactionDirection.Buy;
            }
            else if (num < 0)
            {
                trade.direction = Trade.TransactionDirection.Sell;
            }
            else
            {
                return null;
            }

            trade.SecCode = toolName;
            trade.DateTime = DateTime.Now;
            trade.Volume = Math.Abs(num);
            //trade.Price = random.Next(70000, 80000);
            trade.Price = random.Next(100, 200);
            currentPrice = trade.Price;

            Console.WriteLine(
                "New Trade: SecCode = " + trade.SecCode + 
                " Направление сделки = " +trade.direction + 
                " DateTime = "  + trade.DateTime.ToString() + 
                " Volume = " + trade.Volume.ToString() + 
                " Price = " + trade.Price.ToString());

            return trade;
        }

        public void UpdatePosition(Trade trade)
        {
            if (direction == PositionDirection.None)
            {
                if (trade.direction == Trade.TransactionDirection.Buy)
                {
                    direction = Position.PositionDirection.Long;
                } else
                {
                    direction = Position.PositionDirection.Short;
                }
                
                numPos = trade.Volume;
                averagePrice = trade.Price;
            } else if (direction == PositionDirection.Long)
            {   
                // Позиция лонговая
                if (trade.direction == Trade.TransactionDirection.Buy)
                {
                    // Добираем (Докупаем)
                    Console.WriteLine("Добираем (докупаем) позицию");
                    BalancingPosotion(trade);
                } 
                else
                {
                    if (numPos > trade.Volume)
                    {
                        // Закрываем (продаем) частично
                        Console.WriteLine("Закрываем (продаем) позицию частично");
                        PartiallyClosePosition(trade);
                    } else if (numPos == trade.Volume)
                    {
                        // Закрываем (продаем) полностью
                        Console.WriteLine("Полностью закрываем (продаем) позицию");
                        ClosePosition(trade);  
                    }
                    else 
                    {
                        // Разворот
                        RevertPosition(trade);
                        direction = PositionDirection.Short;
                    }
                }
            } else
            {
                // Позиция шортовая
                if (trade.direction == Trade.TransactionDirection.Sell)
                {
                    // Добираем (Допродаем)
                    Console.WriteLine("Добираем (допродаем) позицию");
                    BalancingPosotion(trade);
                } 
                else
                {
                    if (numPos > trade.Volume)
                    {
                        // Закрываем (откупаем) частично
                        Console.WriteLine("Закрываем (откупаем) частично позицию");
                        PartiallyClosePosition(trade);
                    } else if (numPos == trade.Volume)
                    {
                        // Закрываем (откупаем) полность
                        Console.WriteLine("Полностью закрываем (откупаем) позицию");
                        ClosePosition(trade);
                    }
                    else 
                    {
                        // Разворот
                        RevertPosition(trade);
                        direction = PositionDirection.Long;
                    }
                }
            }

            tradeList.Add(trade); 
            PrintUpdatedPosition();
        }


        public decimal CalculatePnL(decimal tradePrice, decimal closedVolume)
        {
            if (direction == PositionDirection.Long)
            {
                // Для лонга: прибыль = (цена закрытия - средняя цена) × объем
                return (tradePrice - averagePrice) * closedVolume;
            }
            else // PositionType.Short
            {
                // Для шорта: прибыль = (средняя цена - цена закрытия) × объем
                return (averagePrice - tradePrice) * closedVolume;
            }
        }


        public void BalancingPosotion(Trade trade)
        {
            decimal total = (numPos * averagePrice) + (trade.Volume * trade.Price);
            numPos += trade.Volume;
            averagePrice = total / numPos;
        }


        public void ClosePosition(Trade trade)
        {
            // Фиксируем PnL за всю позицию
            decimal pnl = CalculatePnL(trade.Price, numPos);
            realizedPnL += pnl;

            // Обнуляем позицию
            direction = PositionDirection.None;
            numPos = 0;
            averagePrice = 0;

            Console.WriteLine($"Фиксируем PnL: {pnl:F2}");
            Console.WriteLine("Позиция закрыта");
        }

        public void PartiallyClosePosition(Trade trade)
        {
            // Фиксируем PnL за закрытую часть
            decimal pnl = CalculatePnL(trade.Price, trade.Volume);
            realizedPnL += pnl;

            // Уменьшаем объем
            numPos -= trade.Volume;

            Console.WriteLine($"Фиксируем PnL: {pnl:F2}");
        }

        public void RevertPosition(Trade trade)
        {
            Console.WriteLine("РАЗВОРАЧИВАЕМ позицию!");

            // Объем для разворота = объем сделки - текущий объем
            decimal reverseVolume = trade.Volume - numPos;

            Console.WriteLine($"Разворот на {reverseVolume} лотов");

            // Фиксируем PnL за закрытие текущей позиции
            decimal closePnL = CalculatePnL(trade.Price, numPos);
            realizedPnL += closePnL;

            Console.WriteLine($"Фиксируем PnL за закрытие: {closePnL:F2}");

            numPos = reverseVolume;
            averagePrice = trade.Price;
        }

        public void PrintUpdatedPosition()
        {
            switch (direction)
            {
                case PositionDirection.Short:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case PositionDirection.Long:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case PositionDirection.None:
                    return;
            }
            
            Console.WriteLine($"Позиции: {toolName}");
            Console.WriteLine($"Направление: {direction}");
            Console.WriteLine($"Объем: {numPos}");
            Console.WriteLine($"Средняя цена: {averagePrice:F2}");
            Console.WriteLine($"Текущая цена: {currentPrice}");
            Console.WriteLine($"Реализованная прибыль/убыток: {realizedPnL:F2}");
            Console.WriteLine($"********************************************************************");

            Console.ResetColor();
        }

        #endregion
    }
}
