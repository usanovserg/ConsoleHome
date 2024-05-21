using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using Timer = System.Timers.Timer;

namespace GridWPF1
{
    public class Position
    {
        public Position()
        {
          
        }

        public decimal EntryPrice;

        public decimal ClosePrice;

        public List<Trade> OpenTrades
        {
            get
            {
                return _openTrades;
            }
        }
        private List<Trade> _openTrades;

        public Side Direction;

        private PositionStateType _state;

        /// <summary>
        /// Transaction status Open / Close / Opening
        /// </summary>
        public PositionStateType State
        {
            get { return _state; }
            set
            {
                _state = value;
            }
        }

        /// <summary>
        /// Position number
        /// </summary>
        public int Number;



        public enum PositionStateType
        {
            /// <summary>
            /// None
            /// </summary>
            None,

            /// <summary>
            /// Opening
            /// </summary>
            Opening,

            /// <summary>
            /// Closed
            /// </summary>
            Done,

            /// <summary>
            /// Error
            /// </summary>
            OpeningFail,

            /// <summary>
            /// Opened
            /// </summary>
            Open,

            /// <summary>
            /// Closing
            /// </summary>
            Closing,

            /// <summary>
            /// Closing fail
            /// </summary>
            ClosingFail,

            /// <summary>
            /// Brute force during closing
            /// </summary>
            ClosingSurplus,

            /// <summary>
            /// Deleted
            /// </summary>
            Deleted
        }
    }
}
