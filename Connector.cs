using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Connector
    {
        public delegate void newTradeEvent();
        public event newTradeEvent NewTradeEvent;    //без event это делегат, с ним стает событием

        public List<Trade> Trades = new List<Trade>();
        private object? priceUp;
        private object? stepLevel;
        private object? priceDown;
        public object? direction;


        //============================================================= Methods ========================================
        #region Methods

        private void NewTrade(Trade trade)
        {
            Trades.Add(trade);
            NewTradeEvent();
        }

        public void Connect()
        {
            Console.WriteLine("Connect is ExChange");
        }

        /*public void AddDelegate(newTradeEvent method)     //это для делегата, для события(event) это не надо
        {
            NewTradeEvent = method;
        }
        */      

       

        #endregion
    }
}

