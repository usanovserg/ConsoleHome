using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WPF_MVVM.Models;
using WPF_MVVM.Entity;


namespace WPF_MVVM.ViewModels;

public class VM : BaseVM
{
    public VM()
    {
        _server = new Server();
        _server.EventTradeDelegate += _server_EventTradeDelegate;
    }
    

    private Server _server;


    public decimal Volume
    {
        get => _volume;
        //set
        //{
        //    _volume = value;
        //    OnPropertyChanged(nameof(Volume));
        //}
        set => SetField(ref _volume, value);
    }

    decimal _volume;

    public decimal Price
    {
        get => _price;
        set
        {
            _price = value;
            OnPropertyChanged(nameof(Price));
        }
    }
    decimal _price;

    public  DateTime DateTimeTrade
    {
        get => _dateTimeTrade;
        set
        {
            _dateTimeTrade = value;
            OnPropertyChanged(nameof(DateTimeTrade));
        }
    }
    DateTime _dateTimeTrade;

    public Side Side
    {
        get => _side;
        set
        {
            _side = value;
            OnPropertyChanged(nameof(Side));
        }
    }
    Side _side;

    public decimal Sum
    {
        get => _sum;
        set
        {
            _sum = value;
            OnPropertyChanged(nameof(Sum));
        }
    }
    decimal _sum;


    private void _server_EventTradeDelegate(Trade trade)
    {
        Volume = trade.Volume;
        Price = trade.Price;
        Side = trade.Side;
        DateTimeTrade = trade.DateTime;
        Sum += Math.Abs(Volume);
    }
}

