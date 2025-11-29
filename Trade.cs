

namespace ConsoleHome
{

    public class Trade
    {

        public decimal Price = 0;

        public string SecCode = "";

        public string ClassCode = "";

        public DateTime DateTime = DateTime.MinValue;

        public string Portfolio = "";

        public Direction Direction;
        public decimal Volume { get; set; }



        public Trade(string symbol, decimal price, decimal volume, DateTime datetime, Direction direction)
        {
            SecCode = symbol;
            Price = price;
            Volume = volume;
            Direction = direction;
            DateTime = datetime;
        }

        public override string ToString()
        {
            string directionText = Direction == Direction.Long ? "ЛОНГ" : "ШОРТ";
            return $"{SecCode} | {directionText} | Цена: {Price:F2} | Количество: {Volume} | Время: {DateTime:HH:mm:ss}";
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
