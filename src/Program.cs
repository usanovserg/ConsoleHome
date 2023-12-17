using ConsoleHome;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

#if DEBUG
Environment.SetEnvironmentVariable("DOTNET_Environment", "Development");
#endif

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(configuration =>
    {
        configuration.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.Development.json", true);
    }).UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext())
    .ConfigureServices(services => { services.AddSingleton<IPositionFactory, PositionFactory>(); })
    .Build();


var logger = host.Services.GetRequiredService<ILogger<Program>>();

var positionFactory = host.Services.GetRequiredService<IPositionFactory>();


var accountInfo = new AccountInfo("410G0W5");

var siTicketInfo = new TicketInfo("Si-12.23", 1, 1);;

var siPosition = positionFactory.Create(siTicketInfo, accountInfo);
var siPositionOtherAccount = positionFactory.Create(siTicketInfo, new AccountInfo("410G0W6"));


var ngTicketInfo = new TicketInfo("NG-12.23", 0.1m, 9.01158m);
var ngPosition = positionFactory.Create(ngTicketInfo, accountInfo);


siPosition.AddTrade(new Trade { Price = 90400, Volume = 2, TradeType = TradeType.Sell });
siPosition.AddTrade(new Trade { Price = 90300, Volume = 4, TradeType = TradeType.Sell });

siPosition.AddTrade(new Trade { Price = 90100, Volume = 1, TradeType = TradeType.Buy });
siPosition.AddTrade(new Trade { Price = 90500, Volume = 10, TradeType = TradeType.Buy });


var positions = new List<Position> { ngPosition, siPosition, siPositionOtherAccount };

var tradeTasks = positions.Select(p =>
    CreateTradeEmulationLoop(p,
        p.TicketName == "Si-12.23"
            ? () => Random.Shared.Next(85000, 95000) // генерация цены трейда Si
            : () => (decimal)GetRandomNumber(1.921, 2.834) // генерация цены трейда Ng
    ));


await Task.WhenAll(tradeTasks);

return;


async Task CreateTradeEmulationLoop(Position position, Func<decimal> generatePriceFn)
{
    while (true)
    {
        await Task.Delay(TimeSpan.FromSeconds(5));

        var volume = Random.Shared.Next(-10, 10);

        if (volume == 0)
            continue;

        var trade = new Trade
        {
            Volume = Math.Abs(volume),
            Price = generatePriceFn(),
            DateTime = DateTime.Now,
            TradeType = volume > 0 ? TradeType.Buy : TradeType.Sell
        };

        position.AddTrade(trade);

        logger.LogInformation(
            "TradeAccount: {TradeAccountName}, ticket: {TicketName}, trade: {@Trade}, direction: {Direction}, lots: {LotQuantity}",
            position.TradeAccount,
            position.TicketName,
            trade,
            position.Direction,
            position.LotQuantity);

        if (position.State == PositionState.Close)
            return;
    }
}


double GetRandomNumber(double minimum, double maximum)
{
    var next = Random.Shared.NextDouble();

    return minimum + next * (maximum - minimum);
}