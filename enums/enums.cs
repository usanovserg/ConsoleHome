using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome.Enums
{
    public enum TradeDirections
    {
        Bye = 1,
        Neutral = 0,
        Sell = -1
    }

    public enum PositionStatuses
    {
        Open = 1,
        Empty = 0,
        Closed = -1,
    }

    public enum PositionActions
    {
        None,
        Open,
        Add,
        Reduce,
        Close
    }
}
