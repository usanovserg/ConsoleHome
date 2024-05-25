using System.Security.Cryptography.X509Certificates;
using System.Timers;
using ConsoleHome.Enums;
using Timer = System.Timers.Timer;


namespace ConsoleHome
{
    public class Position
    {
        public event Action<string>? PositionChanged;

        public Position()
        {
            Timer timer = new Timer();

            timer.Elapsed += NewTrade;

            timer.Start();

            PositionChanged += ShowExchengePosition;

            timer.Interval = 4000;
        }

        Random random = new Random();

        decimal binancePositionVoluem = 0;
        decimal bybitPositionVoluem = 0;

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
                trade.ExchangeName = Exchange.Bybit;
            }

            trade.Volume = Math.Abs(num);
            if (trade.ExchangeName == Exchange.Binanse)
            {
                binancePositionVoluem += trade.Volume;
            }
            else if (trade.ExchangeName == Exchange.Bybit)
            {
                bybitPositionVoluem += trade.Volume;
            }

            decimal positionVoluemForOut = 0;
            if (trade.ExchangeName == Exchange.Binanse)
            {
                positionVoluemForOut = binancePositionVoluem;
            }
            else if(trade.ExchangeName == Exchange.Bybit)
            {
                positionVoluemForOut = bybitPositionVoluem;
            }

            if (trade.ExchangeName == Exchange.Binanse)
            {
                trade.AccountNumber = 13574357;
                trade.Commision = 0.05;
            }
            else if (trade.ExchangeName == Exchange.Bybit)
            {
                trade.AccountNumber = 54685468;
                trade.Commision = 0.1;
            }

            Console.Write($"Направление сделки: {trade.TypeOrderTrade}, Объём сделки: {trade.Volume}, " +
                $"Биржа {trade.ExchangeName}, Комиссия {trade.Commision}, Номер счёта {trade.AccountNumber} \n");

            if (PositionChanged != null)
            {
                PositionChanged($"Открыта новая сделка на {trade.ExchangeName}, новый объём позиции {positionVoluemForOut}");
            }
        }

        private void ShowExchengePosition(string messege)
        {
            Console.WriteLine(messege);
        }

    }
}