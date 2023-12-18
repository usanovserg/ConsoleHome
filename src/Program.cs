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

siPosition.OnPositionChanged += (sender, eventArgs) =>
{
    logger.LogInformation("OnPositionChanged {@PositionChangedEventArgs}", eventArgs);
};

siPosition.AddTrade(new Trade { Price = 90400, Volume = 2, TradeType = TradeType.Sell });
siPosition.AddTrade(new Trade { Price = 90300, Volume = 4, TradeType = TradeType.Sell });

siPosition.AddTrade(new Trade { Price = 90100, Volume = 1, TradeType = TradeType.Buy });
siPosition.AddTrade(new Trade { Price = 90500, Volume = 10, TradeType = TradeType.Buy });


