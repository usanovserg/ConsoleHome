using ConsoleHome.Service;
using ConsoleHome.Strategy;
using System;

namespace ConsoleHome
{
    public class Application
    {
        #region Fields

        IStrategy strategy = null;
        Market market = null;

        #endregion

        public Application()
        {
            strategy = new GridStrategy();
            market = new Market();
        }

        public void run()
        {
            strategy.Init();
            strategy.ShowInfo();

            market.Start(1000, strategy);
        }
    }
}
