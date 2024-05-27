using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome.Enums
{
    public enum Direction
    {
        Long,
        Short
    }

    public class PositionDerectionEvent : EventArgs
    {
        public Direction PositionDirection { get; private set; }


        public PositionDerectionEvent(Direction direction)
        {

            PositionDirection = direction;
        }

    }
}
