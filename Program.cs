using System;

namespace ConsoleHome
{
    internal class Program
    {
        public static void Main()
        {
            using (var marketDataProvider = new FakeMarketDateProvider())
            {
                var position = new Position(marketDataProvider);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(true);
                Console.WriteLine($"Profit = {position?.Profit}");
            }
        }
    }
}