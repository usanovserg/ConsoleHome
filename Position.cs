using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleHome
{
    public class Position
    {
        private readonly Dictionary<string, PositionBySec> _position = new Dictionary<string, PositionBySec>();

        private readonly IDateProvider _dateProvider;
        
        private readonly Queue<string> _lastTrades = new Queue<string>();
        
        public Position(IDateProvider dateProvider)
        {
            _dateProvider = dateProvider;
            _dateProvider.TradeReceived += DateProviderOnTradeReceived;
        }

        public decimal Profit { get; private set; }

        private void DateProviderOnTradeReceived(object sender, TradeEventArgs e)
        {
            _lastTrades.Enqueue($"{e.Trade.Symbol} time: {e.Trade.Time} price: {e.Trade.Price} operation" +
                                $" {(e.Trade.Operation == Operation.Buy ? "Buy" : "Sell" )} volume : {e.Trade.Volume}");
            
            if (_position.TryGetValue(e.Trade.Symbol, out var positionBySec))
            {
                var tradeVolumeWithSign = e.Trade.Operation == Operation.Buy ? (int)e.Trade.Volume : -(int)e.Trade.Volume;
                if (positionBySec.Position < 0 && e.Trade.Operation == Operation.Sell || 
                    positionBySec.Position > 0 && e.Trade.Operation == Operation.Buy || 
                    Math.Abs(positionBySec.Position) >= e.Trade.Volume)
                {
                    var newPrice = (positionBySec.Position * positionBySec.AveragePrice +
                                    tradeVolumeWithSign * e.Trade.Price) /
                                   (positionBySec.Position + tradeVolumeWithSign);
                    var newPosition = positionBySec.Position + tradeVolumeWithSign;
                    positionBySec.Position  = newPosition;
                    positionBySec.AveragePrice = newPrice;
                }
                else 
                {
                    Profit += positionBySec.Position * (e.Trade.Price - positionBySec.AveragePrice);
                    positionBySec.Position = tradeVolumeWithSign + positionBySec.Position;
                    positionBySec.AveragePrice = e.Trade.Price;
                }

                _position[e.Trade.Symbol] = positionBySec;
            }
            else
            {
                _position[e.Trade.Symbol] = new PositionBySec
                {
                    AveragePrice = e.Trade.Price,
                    Position = e.Trade.Operation == Operation.Buy ? (int)e.Trade.Volume : -(int)e.Trade.Volume,
                };
            }
            Display();
        }

        private void Display()
        {
            Console.Clear();
            _position.Select(pair => $"{pair.Key} {pair.Value.AveragePrice} {pair.Value.Position}").ToList().ForEach(Console.WriteLine);
            Console.WriteLine($"Total Profit: {Profit}");
            _lastTrades.Select(s => s).ToList().ForEach(Console.WriteLine);
        }
    }
}