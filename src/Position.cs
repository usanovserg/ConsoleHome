using System.Timers;
using Microsoft.Extensions.Logging;
using Timer = System.Timers.Timer;

namespace ConsoleHome;

public class Position
{
    private readonly ILogger<Position> _logger;
    private readonly Timer _timer;


    public Position(ILogger<Position> logger)
    {
        _logger = logger;

        _timer = new Timer();

        _timer.Interval = 5000;

        _timer.Elapsed += NewTrade;

        _timer.Start();
    }

    private void NewTrade(object sender, ElapsedEventArgs e)
    {
        var volume = Random.Shared.Next(-10, 10);

        if (volume == 0)
            return;


        var trade = new Trade
        {
            Volume = Math.Abs(volume),
            Price = Random.Shared.Next(70000, 80000),
            ClassCode = "SPBFUT",
            SecCode = "SiZ3",
            DateTime = DateTime.Now,
            TradeType = volume > 0 ? TradeType.Long : TradeType.Short
        };

        _logger.LogInformation("Created trade: {@Trade}", trade);
    }
}