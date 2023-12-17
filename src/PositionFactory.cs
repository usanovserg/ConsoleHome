using Microsoft.Extensions.Logging;

namespace ConsoleHome;

public class PositionFactory : IPositionFactory
{
    private readonly ILogger<PositionFactory> _logger;
    private readonly ILoggerFactory _loggerFactory;

    public PositionFactory(ILogger<PositionFactory> logger, ILoggerFactory loggerFactory)
    {
        _logger = logger;
        _loggerFactory = loggerFactory;
    }

    public Position Create(TicketInfo ticketInfo, AccountInfo accountInfo) =>
        new(ticketInfo, accountInfo, _loggerFactory.CreateLogger<Position>());
}