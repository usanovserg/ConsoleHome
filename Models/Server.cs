using ConsoleHome.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ConsoleHome.Models
{
    public class Server // имитируем Модель (конектор подключенный к бирже)
    {
        public Server()
        {
            System.Timers.Timer timer = new Timer(); // сделка приходит по таймеру
            timer.Interval = 5000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

        }

        #region==========================Fields================================

        Random _random = new Random(); // имитирум обезличенные сделки сoвершенные на бирже

        #endregion

        #region==========================Methods================================
        private void Timer_Elapsed(object sender, ElapsedEventArgs e) // событие генерируется, когда приходит новая сделка
        {
            Trade trade = new Trade();  
            decimal _vol = _random.Next(-10, 10);
            if (_vol > 0)
            {
                trade.Side = Side.Buy; // Сделка в Buy
                
            }
            else if (_vol < 0)
            {
                trade.Side = Side.Sell; // Сделка в Sell
                 
            }
            else if (_vol == 0)
            {
                trade.Side = Side.None; // Нет сделки
                 
            }
            trade.Volume = Math.Abs(_vol);
            trade.Price = _random.Next(50000, 60000);
            trade.DateTime = DateTime.Now;

            EventTradeDelegate?.Invoke(trade); // вызываем событие при появлении новой обезличенной сделки, передаем класс Trade во ВьюМодель
        }
        #endregion

        #region==========================Events================================

        public delegate void tradeDelegate(Trade trade); // передаем сделку во ВьюМодель
        public event tradeDelegate EventTradeDelegate;

        #endregion

    }
}
