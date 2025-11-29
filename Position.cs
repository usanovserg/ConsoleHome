
namespace ConsoleHome
{
    public class Position
    {
        // Свойства позиции
        public string Symbol { get; private set; }
        public Direction Direction { get; set; }
        public int Lots { get; private set; }
        public DateTime OpenTime { get; private set; }
        public decimal OpenPrice { get; private set; }
        public DateTime CloseTime { get; private set; }
        public decimal ClosePrice { get; private set; }
        public decimal AveragePrice { get; private set; }
        public DateTime LastUpdateTime { get; private set; }

        public decimal TotalCost { get; private set; }
        public bool IsLong => Lots > 0;
        public bool IsShort => Lots < 0;

        // Конструктор
        public Position()
        {
            Symbol = "";
            Lots = 0;
            OpenTime = DateTime.MinValue;
            OpenPrice = 0;
            CloseTime = DateTime.MinValue;
            ClosePrice = 0;
            TotalCost = 0;
            AveragePrice = 0;
        }

        /// <summary>
        /// Открытие новой позициии 
        /// </summary>
        public void Open(Trade trade)
        { 
            this.Symbol = trade.Symbol;
            this.Direction = trade.Direction;
            this.Lots = trade.Quantity;
            this.OpenPrice = trade.Price;
            this.OpenTime = trade.TradeTime;
            this.AveragePrice = trade.Price;
            this.TotalCost = trade.Price * trade.Quantity;
            this.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Обработка новой сделки и пересчет позиции
        /// </summary>
        public void Change(Trade trade)
        {
            if (trade.Symbol != this.Symbol)
            {
                Console.WriteLine($"Ошибка: символ сделки ({trade.Symbol}) не соответствует символу позиции ({Symbol})");
                return;
            }

            Console.WriteLine($"\n--- Обработка новой сделки ---");
            Console.WriteLine(trade.ToString());
            Console.WriteLine($"Позиция ДО: Лотов = {Lots}, Средняя цена = {AveragePrice:F2}");

            // Определяем знак количества лотов в зависимости от направления
            int tradeLots = trade.Direction == Direction.Long ? trade.Quantity : -trade.Quantity;

            // Пересчет позиции
            Update(tradeLots, trade.Price);

            // Обновление времени
            LastUpdateTime = DateTime.Now;
            if (Lots != 0 && OpenTime == DateTime.MinValue)
            {
                OpenTime = LastUpdateTime;
            }
            else if (Lots == 0)
            {
                OpenTime = DateTime.MinValue;
            }

            // Вывод новой позиции
            Print();
        }

        /// <summary>
        /// Пересчет позиции на основе новой сделки
        /// </summary>
        private void Update(int tradeLots, decimal tradePrice)
        {

        }

        /// <summary>
        /// Вывод текущей позиции на консоль
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"\nТЕКУЩАЯ ПОЗИЦИЯ:");
            Console.WriteLine($"Инструмент: {Symbol}");
            Console.WriteLine($"Количество лотов: {Lots}");

            if (Lots != 0)
            {
                string positionType = IsLong ? "ЛОНГ" : "ШОРТ";
                Console.WriteLine($"Тип позиции: {positionType}");
                Console.WriteLine($"Средняя цена: {AveragePrice:F2}");
                Console.WriteLine($"Общая стоимость: {TotalCost:F2}");
                Console.WriteLine($"Время открытия: {OpenTime:HH:mm:ss}");
            }
            else
            {
                Console.WriteLine($"Позиция: (нет открытых позиций)");
            }

            Console.WriteLine($"Последнее обновление: {LastUpdateTime:HH:mm:ss}");
            Console.WriteLine("---");
        }


    }
}
