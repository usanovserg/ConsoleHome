using OsEngine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSEngine.RobotEntity
{
    public class MyPosition
    {
        public MyPosition(Security security)
        {
            Security = security;
        }
        
        public Security Security { get; set; }
        
        public decimal Volume { get; set; }
        
        public decimal OpenPrice
        {
            get => _openPrice;
            set
            {
                _openPrice = ((int)(value / Security.PriceStep)) * Security.PriceStep;
            }
        }
        decimal _openPrice;
        
        public decimal Accum { get; set; }
        
        public decimal Margine { get; set; }
        
        /// <summary>
        /// Все сделки позиции
        /// </summary>
        public List<MyTrade> MyTrades { get; private set; } = new List<MyTrade>();
        
        /// <summary>
        /// Ордера, относящиеся к этой позиции
        /// </summary>
        public List<Order> Orders { get; private set; } = new List<Order>();

        #region Methods ================================================================

        public void Clear()
        {
            MyTrades.Clear();
            Orders.Clear();
        }

        public bool GetPermission()
        {
            if (Orders.Count == 0) return true;

            Order order = Orders.Last();

            if (order.State == OrderStateType.Pending
            || order.State == OrderStateType.None) return false;

            return true;
        }

        public void AddNewOrder(Order newOrder)
        {
            Orders.Add(newOrder);
        }

        public void AddOrderFromServer(Order newOrder)
        {
            for (int i = 0; i < Orders.Count; i++)
            {
                if (newOrder.NumberUser == Orders[i].NumberUser
                || (newOrder.NumberMarket != ""
                && newOrder.NumberMarket == Orders[i].NumberMarket))
                {
                    Orders[i] = newOrder;
                    return;
                }
            }
        }

        public void AddTrade(MyTrade newMyTrade)
        {
            if (!IsMyTrade(newMyTrade)) return;

            foreach (MyTrade myTrade in MyTrades)
            {
                if (myTrade.NumberTrade == newMyTrade.NumberTrade) return;
            }

            MyTrades.Add(newMyTrade);

            CalculatePosition();
        }

        private void CalculatePosition()
        {
            decimal openPrice = 0;
            decimal volume = 0;
            decimal accum = 0;

            foreach (MyTrade myTrade in MyTrades)
            {
                if (myTrade.Side == Side.Buy)
                {
                    if (volume == 0)
                    {
                        openPrice = myTrade.Price;
                    }
                    else if (volume > 0)
                    {
                        openPrice = (openPrice * volume + myTrade.Price * myTrade.Volume) / (volume + myTrade.Volume);
                    }
                    else if (volume < 0)
                    {
                        if (Math.Abs(volume) >= myTrade.Volume)
                        {
                            accum = (openPrice - myTrade.Price) * myTrade.Volume;
                        }
                        else
                        {
                            accum = (myTrade.Price - openPrice) * volume;
                            openPrice = myTrade.Price;
                        }
                    }

                    volume += myTrade.Volume;
                }

                else
                {
                    if (volume == 0)
                    {
                        openPrice = myTrade.Price;
                    }
                    else if (volume > 0)
                    {
                        if (volume >= myTrade.Volume)
                        {
                            accum = (myTrade.Price - openPrice) * myTrade.Volume;
                        }
                        else
                        {
                            accum = (myTrade.Price - openPrice) * volume;
                            openPrice = myTrade.Price;
                        }
                    }
                    else if (volume < 0)
                    {
                        openPrice = (openPrice * Math.Abs(volume)
                            + myTrade.Price * myTrade.Volume)
                            / (Math.Abs(volume) + myTrade.Volume);
                    }

                    volume -= myTrade.Volume;
                }
            }

            Accum = accum;
            OpenPrice = openPrice;
            Volume = volume;
        }

        private bool IsMyTrade(MyTrade trade)
        {
            foreach (var order in Orders)
            {
                if (order.NumberMarket == trade.NumberOrderParent) return true;
            }
            return false;
        }

        #endregion
    }
}