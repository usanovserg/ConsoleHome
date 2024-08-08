using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OsEngine.Entity;
using OsEngine.Robots._MyRobots.Entity.Enums;
using OsEngine.Robots._MyRobots.FrontRunner.Models;
using ru.micexrts.cgate;
using ScottPlot;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OsEngine.Robots._MyRobots.FrontRunner.ViewModels
{
    public class Vm :ObservableObject
    {
        public FrontRunnerBot _bot;

        public Vm(FrontRunnerBot bot)
        {
            _bot = bot;
            _bot._tab.NewTickEvent += NewTickEvent;
            PositionClosed = new ObservableCollection<Position>(_bot._tab.PositionsCloseAll);
        }

        //private void RunBot(Enum value)
        //{
        //    if (_bot._tab.NameStrategy != _bot._tab.TabName)
        //    {
        //        return;
        //    }

        //    if (value.Equals(StartStop.Stop))
        //    {
        //        _bot._tab.MarketDepthUpdateEvent -= _tab_MarketDepthUpdateEvent;
        //        _bot._tab.MarketDepthUpdateEvent += _tab_MarketDepthUpdateEvent;
        //    }
        //    else /*if(value.Equals(StartStop.Start))*/
        //    {
        //        _bot._tab.CloseAllOrderInSystem();
        //        //_tab.CloseAllOrderToPosition(_position);
        //        _bot._tab.MarketDepthUpdateEvent -= _tab_MarketDepthUpdateEvent;
        //    }
        //}

        private void NewTickEvent(Trade trade)
        {
            //var positionsOpeningFail = _bot._tab.PositionsAll.FindAll(p => p.State == PositionStateType.OpeningFail).ToList();
            var positionClosedList = _bot._tab.PositionsCloseAll.OrderByDescending(p => p.TimeOpen).ToList();
            PositionClosed = new ObservableCollection<Position>(positionClosedList); 
            Position = new ObservableCollection<Position>(_bot._tab.PositionsOpenAll); 
        }
       
        public ObservableCollection<Position> Position
        {
            get => _position;
            set
            {   
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        } 
        private ObservableCollection<Position> _position;

       
        public ObservableCollection<Position> PositionClosed
        {
            get => _positionClosed;
            set
            {
                _positionClosed = value;
                OnPropertyChanged(nameof(PositionClosed));
            }
        }
        private ObservableCollection<Position> _positionClosed;


        public decimal BigVolume
        {
            get =>  _bot.BigVolume;
            set
            {
                _bot.BigVolume = value;
                OnPropertyChanged(nameof(BigVolume));
            }
        }

        public int Offset
        {
            get => _bot.Offset;
            set
            {
                _bot.Offset = value;
                OnPropertyChanged(nameof(Offset));
            }
        }

        public int Profit
        {
            get => _bot.Profit;
            set
            {
                _bot.Profit = value;
                OnPropertyChanged(nameof(Profit));
            }
        }

        public decimal Lot
        {
            get => _bot.Lot;
            set
            {
                _bot.Lot = value;
                OnPropertyChanged(nameof(Lot));
            }
        }

        public StartStop ButtonName
        {
            get => _buttonName;
            set
            {
                _buttonName = value;
                OnPropertyChanged(nameof(ButtonName));
                OnPropertyChanged(nameof(ButtonContent));
            }
        }
        private StartStop _buttonName;


       //public static event Action<Enum> RunBot;


        private RelayCommand _startCommand;
        public IRelayCommand StartCommand => _startCommand ??= new RelayCommand(new Action(Start));

        //[RelayCommand]
        private void Start()
        {
            ButtonName = ButtonName == StartStop.Start ? StartStop.Stop : StartStop.Start;

            _bot.RunBot(ButtonName);
            //RunBot?.Invoke(ButtonName);

            //if (ButtonName == StartStop.Stop)
            //{   
            //    _bot._tab.MarketDepthUpdateEvent += _bot.MarketDepthUpdateEvent;
            //}
            //else /*if(value.Equals(StartStop.Start))*/
            //{
            //    _bot._tab.CloseAllOrderInSystem();
            //    _bot._tab.MarketDepthUpdateEvent -= _bot.MarketDepthUpdateEvent;
            //}
        }

        public string ButtonContent => ButtonName == StartStop.Stop ? "Выключить робота" : "Включить робота"; 
 
      
        private string _regimeSelectedItem = "В обе стороны";
      
        public string RegimeSelectedItem
        {
            get => _regimeSelectedItem;
            set
            {
                _regimeSelectedItem = value;
                    if (RegimeSelectedItem.Contains("В обе стороны"))
                {
                   _bot.RegimeSelect = Regime.Both;
                }
                else if(RegimeSelectedItem.Contains("Только лонг"))
                {
                    _bot.RegimeSelect = Regime.OnlyLong; ;
                }
                else
                {
                    _bot.RegimeSelect = Regime.OnlyShort; 
                }
                OnPropertyChanged(nameof(RegimeSelectedItem));
            }
        } 
        public string[] RegimeSelect { get; set; } = ["В обе стороны", "Только лонг", "Только шорт"]; 
    }  
}
