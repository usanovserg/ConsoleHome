using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GridWPF1
{
    public class Connector
    {
        List<Trade> Trades = new List<Trade>();

        public delegate void newTradeEvent(Trade trade);

        public event newTradeEvent? NewTradeEvent;

        public void NewTrade(Trade trade)
        {
            Trades.Add(trade);
            NewTradeEvent?.Invoke(trade);
        }


        Random random = new Random();
        Trade trade = new Trade();
        List<Trade> ExChageTrades = [];

        private int num;

        public void Connect(TextBox Tb_ExChange)
        {
            //Position pos = new Position();

            //Task.Run(() =>
            //{
            //    Tb_ExChange.Text = "Connecting";
            //    Thread.Sleep(200);
            //    Tb_ExChange.Text = "Connect";
            //});

            Tb_ExChange.Text = "Connecting" + "\r\n";
            Thread.Sleep(1);
            Tb_ExChange.AppendText("Connect" + "\r\n" + "\r\n");

            num = random.Next(-10, 10);

            if (num == 0)
            {
                return;
            }

            if (num > 0)
            {
                trade.Side = Side.Buy;
                NewTrade(trade);
            }

            else
            {
                trade.Side = Side.Sell;
                NewTrade(trade);
            }

            trade.Volume = GetLastTrade().Volume;
            trade.Price = GetLastTrade().Price;

            Trade newLine = new();
            newLine.Price = trade.Price;
            newLine.Volume = trade.Volume;

            ExChageTrades.Add(newLine);

            for (int i = 0; i < ExChageTrades.Count; i++)
            {
                string str = "Volume =" + ExChageTrades[i].Volume.ToString() + "  " + 
                             "Price = " + ExChageTrades[i].Price.ToString() + "\r\n";

                Tb_ExChange.AppendText(str);
                Tb_ExChange.CaretIndex = Tb_ExChange.LineCount;
                Tb_ExChange.ScrollToEnd();
            }
        }

        //================ Получение последнего сгенерированного трейда ===================

        public Trade GetLastTrade()
        {
            trade.Volume = Math.Abs(num);
            trade.Price = CountPrice();
            return trade;
        }

        //================ Генерация цены ==========================================

        decimal CountPrice()
        {
            decimal price;
            decimal lastValue;

            if (Trades.Count > 0)
            {
                lastValue = Trades[^1].Price;
            }
            else
            {
                lastValue = 0;
            }

            var t1 = random.Next(70000, 80000);

            if (lastValue == 0)
            {
                price = t1;
            }

            else
            {
                while (Math.Abs(lastValue - t1) > Volatility)
                {
                    t1 = random.Next(70000, 80000);
                }
            }

            price = t1;

            return price;
        }
        
        public int Volatility
        {
            get;
            set;
        }
    }
}
