using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace ConsoleHome;

public class Position
{
    private readonly TicketInfo _ticketInfo;
    private readonly AccountInfo _accountInfo;
    private readonly ILogger<Position> _logger;

    private readonly List<Trade> _trades;

    public Position(TicketInfo ticketInfo,
        AccountInfo accountInfo,
        ILogger<Position> logger)
    {
        _ticketInfo = ticketInfo;
        _accountInfo = accountInfo;
        _logger = logger;
        _trades = new List<Trade>();
        State = PositionState.Created;
        Direction = PositionDirection.None;
    }

    public PositionDirection Direction { get; private set; }
    public PositionState State { get; private set; }
    public decimal Fee => throw new NotImplementedException();
    public decimal Margin => throw new NotImplementedException();
    public decimal AveragePrice => throw new NotImplementedException();
    public string TradeAccount => _accountInfo.Name;
    public string TicketName => _ticketInfo.Name;
    public decimal LotQuantity { get; private set; }

    public void AddTrade(Trade trade)
    {
        _trades.Add(trade);

        if (State == PositionState.Created)
        {
            CreateFirstTrade(trade);

            return;
        }

        Trace.Assert(Direction != PositionDirection.None, "Direction != PositionDirection.None");
        Trace.Assert(State != PositionState.Close, "State != PositionState.Close");

        switch (Direction)
        {
            case PositionDirection.Long:
                ChangePosition(trade.TradeType == TradeType.Buy ? trade.Volume : -trade.Volume);
                return;
            case PositionDirection.Short:
                ChangePosition(trade.TradeType == TradeType.Sell ? -trade.Volume : trade.Volume);
                return;
        }
    }

    private void CreateFirstTrade(Trade trade)
    {
        State = PositionState.Open;
        Direction = trade.TradeType == TradeType.Buy ? PositionDirection.Long : PositionDirection.Short;

        LotQuantity = trade.TradeType == TradeType.Buy ? trade.Volume : -trade.Volume;
    }

    private void ChangePosition(decimal tradeVolume)
    {
        LotQuantity += tradeVolume;

        switch (LotQuantity)
        {
            case 0:
                State = PositionState.Close;
                Direction = PositionDirection.None;
                break;
            case > 0:
                Direction = PositionDirection.Long;
                break;
            case < 0:
                Direction = PositionDirection.Short;
                break;
        }
    }
}

public enum PositionState
{
    Created,
    Open,
    Close
}

public enum PositionDirection
{
    None,
    Long,
    Short
}