
using System.Diagnostics;

namespace ConsoleHome
{
    public class Position
    {
        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Fields
        public string SecCode = "";

        public string ClassCode = "";

        public string Portfolio = "";
        public string Status = "";


        public Direction Direction;
        public decimal Lots = 0;
        public DateTime OpenTime = DateTime.MinValue;
        public decimal OpenPrice = 0;
        public DateTime CloseTime = DateTime.MinValue;
        public decimal ClosePrice = 0;
        public decimal AveragePrice = 0;
        public DateTime LastUpdateTime = DateTime.MinValue;

        public decimal TotalCost = 0;
        public decimal TotalResult = 0;
        public decimal comission = 0.05m/100m;
        public bool IsLong => Lots > 0;
        public bool IsShort => Lots < 0;
        #endregion

        public delegate void PositionHandler(string message);
        public event PositionHandler? ChangePositionEvent;
        public event PositionHandler? OpenPositionEvent;
        public event PositionHandler? ClosePositionEvent;

        // Конструктор
        public Position()
        {

        }

        //----------------------------------------------- Methods ---------------------------------------------------
        #region Methods

        /// <summary>
        /// Открытие новой позициии 
        /// </summary>
        public void Open(Trade trade)
        {
            this.SecCode = trade.SecCode;
            this.Status = "Open";
            this.Direction = trade.Direction;
            this.Lots = trade.Direction == Direction.Long ? trade.Volume : -trade.Volume;
            this.OpenPrice = trade.Price;
            this.OpenTime = trade.DateTime;
            this.AveragePrice = trade.Price;
            this.TotalCost = trade.Price * Math.Abs(trade.Volume)*comission;
            this.OpenTime = trade.DateTime;
            this.LastUpdateTime = this.OpenTime;

            string directionText = Direction == Direction.Long ? "ЛОНГ" : "ШОРТ";

            OpenPositionEvent?.Invoke($"Позиция открыта: {directionText} по инструменту {SecCode} открыта по цене {OpenPrice} кол-во лотов {Lots} ");
        }

        /// <summary>
        /// Обработка новой сделки и пересчет позиции
        /// </summary>
        public void Change(Trade trade)
        {
            if (trade.SecCode != this.SecCode)
            {
                Console.WriteLine($"Ошибка: символ сделки ({trade.SecCode}) не соответствует символу позиции ({SecCode})");
                return;
            }

            string directionText = Direction == Direction.Long ? "ЛОНГ" : "ШОРТ";
            ChangePositionEvent?.Invoke($"Позиция изменена: {directionText} по инструменту {trade.SecCode} по цене {trade.Price} кол-во лотов {trade.Volume} ");


            // Определяем знак количества лотов в зависимости от направления
            var tradeLots = trade.Direction == Direction.Long ? trade.Volume : -trade.Volume;

            // Пересчет позиции
            Update(tradeLots, trade.Price);
            LastUpdateTime = trade.DateTime;

        }
        public void Close(Trade trade)
        {
            var tradeLots = trade.Direction == Direction.Long ? trade.Volume : -trade.Volume;
            if (Lots * tradeLots < 0 && Math.Abs(Lots) == Math.Abs(tradeLots))
            {
                Status = "Close";

            }

            // Пересчет позиции
            Change(trade);
            ClosePositionEvent?.Invoke($"Позиция закрыта");

        }
        /// <summary>
        /// Пересчет позиции на основе новой сделки
        /// </summary>
        private void Update(decimal tradeLots, decimal tradePrice)
        {
            if (IsLong)
            {
                if (tradeLots > 0)
                {
                    TotalCost += Math.Abs(tradeLots) * tradePrice*comission;
                    var newLots = tradeLots + Lots;
                    AveragePrice = (AveragePrice * Lots + tradePrice * tradeLots) / (newLots);
                    Lots = newLots;
                }
                else if (tradeLots < 0)
                {
                    if (Math.Abs(tradeLots) <= Math.Abs(Lots))
                    {
                        TotalCost += Math.Abs(tradeLots) * tradePrice* comission;
                        TotalResult += (tradePrice - AveragePrice) * Math.Abs(tradeLots);
                        Lots = Lots + tradeLots;


                    }
                    else
                    {
                        TotalCost += Math.Abs(tradeLots) * tradePrice* comission;
                        TotalResult += (tradePrice - AveragePrice) * Math.Abs(tradeLots);
                        Lots = tradeLots + Lots;
                        AveragePrice = tradePrice;


                    }

                }
            }
            else if (IsShort)
            {
                if (tradeLots < 0)
                {
                    TotalCost += Math.Abs(tradeLots) * tradePrice* comission;
                    var newLots = tradeLots + Lots;
                    AveragePrice = (AveragePrice * Lots + tradePrice * tradeLots) / (newLots);
                    Lots = newLots;

                }
                else if (tradeLots > 0)
                {
                    if (Math.Abs(tradeLots) <= Math.Abs(Lots))
                    {
                        TotalCost += Math.Abs(tradeLots) * tradePrice * comission;
                        TotalResult += (AveragePrice-tradePrice) * Math.Abs(tradeLots);
                        Lots = Lots + tradeLots;

                    }
                    else
                    {
                        
                        TotalCost += Math.Abs(tradeLots) * tradePrice * comission;
                        TotalResult += (tradePrice - AveragePrice) * Math.Abs(tradeLots);
                        AveragePrice = tradePrice;
                        Lots = tradeLots + Lots;
                    }

                }

            }

        }

        /// <summary>
        /// Вывод текущей позиции на консоль
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"\nТЕКУЩАЯ ПОЗИЦИЯ:");
            Console.WriteLine($"Инструмент: {SecCode}");
            Console.WriteLine($"Количество лотов: {Lots}");

            if (Lots != 0)
            {
                string positionType = IsLong ? "ЛОНГ" : "ШОРТ";
                Console.WriteLine($"Тип позиции: {positionType}");
                Console.WriteLine($"Статус позиции: {Status}");

                Console.WriteLine($"Средняя цена: {AveragePrice:F2}");
                Console.WriteLine($"Общая комиссия: {TotalCost:F2}");
                Console.WriteLine($"Общий результат: {TotalResult:F2}");

                Console.WriteLine($"Время открытия: {OpenTime:HH:mm:ss}");
            }
            else
            {
                Console.WriteLine($"Статус позиции: {Status}");
                Console.WriteLine($"Позиция: (нет открытых позиций)");
            }

            Console.WriteLine($"Последнее обновление: {LastUpdateTime:HH:mm:ss}");
            Console.WriteLine("---");
        }

        public static Position OpenPosition(Trade trade)
        {

            Position position = new Position();
            position.Open(trade);

            return position;
        }
        #endregion
    }




}
