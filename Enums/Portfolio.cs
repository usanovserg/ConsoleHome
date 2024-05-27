using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome.Enums
{
    public enum Portfolio
    {
        ExChangeName, ClassCode, SecCode, Ticker, PnL, Volume
    }

    public class PortfolioEvent : EventArgs
    {
        public Portfolio ExChangeName {  get; private set; }

        public Portfolio ClassCodeName { get; private set; }

        public Portfolio SecCodeName { get; private set; }

        public Portfolio TickerName { get; private set; }

        public Portfolio PortfolioPnL { get; private set; }

        public Portfolio TickerVolume { get; private set; }

        public PortfolioEvent (Portfolio exChangeName, Portfolio classCodeName, Portfolio secCodeName, 
            Portfolio tickerName, Portfolio portfolioPnl, Portfolio tickerVolume)
        {
            ExChangeName = exChangeName;

            ClassCodeName = classCodeName;

            SecCodeName = secCodeName;

            TickerName = tickerName;

            PortfolioPnL = portfolioPnl;

            TickerVolume = tickerVolume;

        }

    }
}
