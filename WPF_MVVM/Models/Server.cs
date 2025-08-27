using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WPF_MVVM.Entity;
using Timer = System.Timers.Timer; 

namespace WPF_MVVM.Models
{
    public class Server
    {
        public Server()
        {
            Timer timer = new Timer();
            timer.Interval = 1000; // 1 секунда
            timer.Elapsed += Timer_Elapsed; // Подписка на событие
            timer.Start();  // Запуск таймера
        }
        // Fields ====================================

        Random _random = new Random();  // Генератор случайных чисел

        // Methods ======================
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            trade.Volume = _random.Next(-10,10);
            trade.Price = _random.Next(50000, 60000);
            trade.DateTime = DateTime.Now;

            EventTradeDelegate?.Invoke(trade); // Вызов события
        }  
                
        // Event =================================
        public delegate void tradeDelegate(Trade trade); //определение делегата
        public event tradeDelegate EventTradeDelegate; //объявление события




    }
}
