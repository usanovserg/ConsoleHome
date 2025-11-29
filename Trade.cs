

namespace ConsoleHome
{

    public class Trade
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime TradeTime { get; set; }
        public Direction Direction { get; set; }

        public Trade(string symbol, decimal price, int quantity, Direction direction)
        {
            Symbol = symbol;
            Price = price;
            Quantity = quantity;
            Direction = direction;
            TradeTime = DateTime.Now;
        }

        public override string ToString()
        {
            string directionText = Direction == Direction.Long ? "ЛОНГ" : "ШОРТ";
            return $"{Symbol} | {directionText} | Цена: {Price:C} | Количество: {Quantity} | Время: {TradeTime:HH:mm:ss}";
        }
    }


    public enum Direction
    {
        /// <summary>
        /// Лонг (покупка)
        /// </summary>
        Long,

        /// <summary>
        /// Шорт (продажа)
        /// </summary>
        Short
    }


}
