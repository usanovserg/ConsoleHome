using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WPF_MVVM.Entity;
using WPF_MVVM.Models;
using Timer = System.Timers.Timer;

namespace WPF_MVVM.ViewModels
{
    public class VM: BaseVM
    {
        public VM()
        {
            _server = new Server();
            _server.EventTradeDelegate += _server_EventTradeDelegate;
        }

       
        // Filds ==============================
        Server _server;

        // Properties ==============================

        public decimal Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                OnPropertyChanged(nameof(Volume));
            }
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

        public decimal Summ
        {
            get => _summ;
            set
            {
                _summ = value;                
            }
        }
        decimal _summ;

        public DateTime DateTimeTrade
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


        //Method ==============================
       
        private void _server_EventTradeDelegate(Trade trade)
        {                      
            Volume = trade.Volume;
            Price = trade.Price;
            DateTimeTrade = trade.DateTime;
            Side = trade.Side;

            Summ += Volume;
            OnPropertyChanged(nameof(Summ));  
        }         
    }
}
