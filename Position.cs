using System.Timers;
using Timer = System.Timers.Timer;


namespace ConsoleHome
{
    public class Position
    {
        public Position()
        {
            Timer timer = new Timer();

            timer.Elapsed += NewTrade;

            timer.Start();

            timer.Interval = 2000;
        }

        Random random = new Random();

        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);

            if (num >= 0)
            {
                trade.TypeOrderTrade = TypeOrder.Long;
                trade.ExchangeName = Exchange.Binanse;
            }
            else
            {
                trade.TypeOrderTrade = TypeOrder.Short;
                trade.ExchangeName = Exchange.Bibit;
            }

            trade.Volume = Math.Abs(num);

            trade.Commision = 0.02;


            if (trade.ExchangeName == Exchange.Binanse)
            {
                trade.AccountNumber = 13574357;
                trade.Commision = 0.05;
            }
            else if (trade.ExchangeName == Exchange.Bibit)
            {
                trade.AccountNumber = 54685468;
                trade.Commision = 0.1;
            }

            Console.Write($"Направление сделки: {trade.TypeOrderTrade}, Объём сделки: {trade.Volume}, " +
                $"Биржа {trade.ExchangeName}, Комиссия {trade.Commision}, Номер счёта {trade.AccountNumber} \n");

        }

    }
}