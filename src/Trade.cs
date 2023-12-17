namespace ConsoleHome;

public class Trade
{
    public DateTime DateTime { get; set; }
    public decimal Volume { get; set; }
    public TradeType TradeType { get; set; }
    public decimal Price { get; set; }
}

public enum TradeType
{
    Sell,
    Buy
}