using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    public delegate void ChangePosition(decimal volume);

    public class Position
    {                     
        //=========================================================== Methods ==============================================================
        #region Methods
        public Position()
        {
            Timer timer = new Timer();

            timer.Interval = 5000;

            timer.Elapsed += NewTrade;

            timer.Start();
        }

        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);

            if (num > 0)
            {
                trade.OrderType = OrderType.Buy;
            }
            else if (num < 0)
            {
                trade.OrderType = OrderType.Sell;
            }

            trade.Volume = Math.Abs(num);

            if (trade.Volume != 0)
            {
                trade.Price = random.Next(70000, 80000);

                string str = "Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString() + " / OrderType = " + trade.OrderType.ToString();

                Console.WriteLine(str);

                switch (trade.OrderType)
                {
                    case OrderType.Buy: _positionSize += trade.Volume; break;
                    case OrderType.Sell: _positionSize -= trade.Volume; break;
                }

                if (ChangePosition != null)
                {
                    ChangePosition(_positionSize);
                }
            }
        }

        #endregion

        //=========================================================== Propeties ============================================================
        #region Properties

        public ChangePosition ChangePosition
        {
            get
            { 
                return _changePosition; 
            }
            set
            { 
                _changePosition = value; 
            }
        }

        #endregion

        //=========================================================== Fields ===============================================================
        #region Fields

        decimal _positionSize = 0;

        event ChangePosition _changePosition = null;

        Random random = new Random();

        #endregion

    }
}
