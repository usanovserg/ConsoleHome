using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position();
            position.PriceChangeEvents += PriceLine;//Добавляем обработчик для события PriceChangeEvents
            Console.ReadLine();
        }
        //при вызове события PriceChangeEvents будет вызываться этот метод PriceLine,
        //которому для параметра message будет передаваться строка, которая передается в PriceChangeEvents?.Invoke()

        public static void PriceLine(string message)
        {
            Console.WriteLine($"Позиция изменилась. {message}");

        }

    }
}