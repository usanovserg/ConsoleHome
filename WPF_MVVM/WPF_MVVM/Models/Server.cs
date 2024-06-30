using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using WPF_MVVM.Entity;
using WPF_MVVM.ViewModels;

namespace WPF_MVVM.Models;

public class Server
{
    public Server()
    {
        Timer timer = new Timer();
        timer.Interval = 1000;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    }

    Random rnd = new Random();

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        Trade trade = new Trade
        {
            Volume = rnd.Next(-10, 10),
            Price = rnd.Next(50000, 60000),
            DateTime = DateTime.Now
        };

        EventTradeDelegate?.Invoke(trade);
    }
    
    public delegate void TradeDelegate(Trade trade);

    public event TradeDelegate EventTradeDelegate;
}


