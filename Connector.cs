using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Connector
    {

        public delegate void newTradeEvent();

        public event newTradeEvent NewTradeEvent;

        public List<Trade> Trades = new List<Trade>();

        private void NewTrade(Trade trade)
        {
            Trades.Add(trade);

            NewTradeEvent();
        }

        public void Connect()
        {
            Console.WriteLine("Connection is OK");
        }
        /*
        public void AddDelegate(newTradeEvent method)
        {
            NewTradeEvent = method;
        }
        */
    }
}
