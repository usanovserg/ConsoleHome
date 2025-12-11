using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static HomeWork156.Trade;

namespace HomeWork156
{
    public class Position
    {
        //=====================================Property========================================
        #region Property


        private Direct _direction=Direct.Short;

        public Direct Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }


        private decimal _lots=0;

        public decimal Lots
        {
            get { return _lots; }
            set { _lots = value; }
        }
        private decimal _pos=0;

        public decimal Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        private decimal _balancePrice=0;

        public decimal BalancePrice
        {
            get { return _balancePrice; }
            set { _balancePrice = value; }
        }


        private  List<Trade> _trades = new List<Trade>();

        public List<Trade> Trades
        {
            get { return _trades; }
           // set { _trades = value; }
        }
        #endregion

        //=====================================Fields========================================
        #region
            Random random = new Random();
            //static OnAddTrade _onAddTrade;
        #endregion


        //=====================================Methods========================================

        #region Methods

        public delegate void OnAddTrade(List<Trade> trades);
        public event OnAddTrade OnAddTradeEvent;


        public Position()
            {
            OnAddTradeEvent = CalculateLots;
            OnAddTradeEvent += CalculateBalanceCount;


            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += NewTrade;
            timer.Start();
            Console.WriteLine("");
            }

        
        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();
            int num = random.Next(-10, 10);
            if (num > 0) 
                { trade.Direction = Trade.Direct.Long; }
            else if (num < 0) 
                { trade.Direction = Trade.Direct.Short; }
            trade.Volume = Math.Abs(num);
            trade.Price = random.Next(70000, 80000);
            trade.SetCode = "Si";
            _trades.Add(trade);
            

            OnAddTradeEvent(_trades);

            
            //throw new NotImplementedException();
        }
        void CalculateLots(List<Trade> trades)
        {
            /* _lots = 0;
             foreach (var item in trades)
             {
                 switch (item.Direction)
                 {
                     case Trade.Direct.Long:
                         _lots += item.Volume;
                     break;
                     case Trade.Direct.Short:
                         _lots -= item.Volume;
                     break;
                 }
             }*/
           Trade _lastTrade= trades.Last();
            switch( _lastTrade.Direction)
            {
                case Trade.Direct.Long:
                    _lots += _lastTrade.Volume;
                    break;
                case Trade.Direct.Short:
                    _lots -= _lastTrade.Volume;
                    break;
            }
            if (_lots >= 0)
              { _direction = Direct.Long; }
            else if (_lots < 0) { _direction = Direct.Short; }
        }
        

        void CalculateBalanceCount(List<Trade> trades)
        {
           Trade _lastTrade = trades.Last();
            decimal _balancePriceCurrent = _lastTrade.Price* _lastTrade.Volume;

            switch (_lastTrade.Direction)
            {
                case Trade.Direct.Long:
                    _balancePrice += _balancePriceCurrent;
                    break;
                case Trade.Direct.Short:
                    _balancePrice -= _balancePriceCurrent;
                    break;
            }

            try
            {
                _pos = _balancePrice / _lots;
            }
            catch (Exception)
            {

                throw;
            }   

        }
        #endregion
    }
}
