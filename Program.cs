

using ConsoleHome.Enums;

namespace ConsoleHome
{
    public class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position();
            position.PositionChanged += ShowExchengePosition;
            Console.ReadKey();
        }

        public static void ShowExchengePosition(object? sender, PositionChangedEventArgs e)
        {
            Console.WriteLine($"Открыта новая сделка на бирже {e.ExchengeName}. Новый объём позиции {e.PositionVolume}. \n");
        }

    }
}
