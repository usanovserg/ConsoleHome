
namespace ConsoleHome
{
    public class Position
    {
        // Свойства позиции
        public string SecCode = "";

        public string ClassCode = "";

        public string Portfolio = "";
        public string Status = "";


        public Direction Direction;
        public decimal Lots = 0;
        public DateTime OpenTime = DateTime.MinValue;
        public decimal OpenPrice= 0;
        public DateTime CloseTime = DateTime.MinValue;
        public decimal ClosePrice = 0;
        public decimal AveragePrice= 0;
        public DateTime LastUpdateTime= DateTime.MinValue;

        public decimal TotalCost = 0;
        public bool IsLong => Lots > 0;
        public bool IsShort => Lots < 0;

        public delegate void PositionHandler(string message);
        public event PositionHandler? ChangePositionEvent;
        public event PositionHandler? OpenPositionEvent;
        public event PositionHandler? ClosePositionEvent;

        // Конструктор
        public Position()
        {

        }

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
            this.TotalCost = trade.Price * trade.Volume;
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
            if (Lots * tradeLots < 0 && Math.Abs(Lots) == Math.Abs(tradeLots)) {
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
            //decimal prevCost = TotalCost;
            decimal prevLots = Lots;

            // Если закрываем или разворачиваем позицию
            if (prevLots * tradeLots < 0) // Разные знаки
            {
                var absPrevious = Math.Abs(prevLots);
                var absTrade = Math.Abs(tradeLots);
                var minLots = Math.Min(absPrevious, absTrade);

                // Закрываем часть позиции
                TotalCost -= minLots * Math.Sign(prevLots) * AveragePrice;
                Lots -= minLots * Math.Sign(prevLots);

                // Если остались лоты для открытия в другом направлении
                if (absTrade > minLots)
                {
                    var remainingLots = (absTrade - minLots) * Math.Sign(tradeLots);
                    TotalCost += remainingLots * tradePrice;
                    Lots += remainingLots;
                }
            }
            else // Увеличиваем позицию в том же направлении
            {
                TotalCost += tradeLots * tradePrice;
                Lots += tradeLots;
            }

            // Расчет средней цены
            if (Lots != 0)
            {
                AveragePrice = Math.Abs(TotalCost / Lots);
            }
            else
            {
                AveragePrice = 0;
                TotalCost = 0;
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
                Console.WriteLine($"Общая стоимость: {TotalCost:F2}");
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

    public static Position OpenPosition(Trade trade) { 
       
        Position position = new Position();
        position.Open(trade);
        
        return position;
    }
    
    }




}
