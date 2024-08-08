using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OsEngine.Entity;
using OsEngine.Robots._MyRobots.Entity.Enums;
using OsEngine.Robots._MyRobots.PriceChannelMvvm.Models;

namespace OsEngine.Robots._MyRobots.PriceChannelMvvm.ViewModels;

public class Vm : ObservableObject
{
    public Models.PriceChannelMvvm _bot;

    public Vm(Models.PriceChannelMvvm bot)
    {
        _bot = bot;
    }

    public StrategyParameterInt PcUpLength
    {
        get => _bot.PcUpLength;
        set
        {
            _bot.PcUpLength = value;
            OnPropertyChanged(nameof(PcUpLength));
        }
    }

    public StrategyParameterInt PcDownLength
    {
        get => _bot.PcDownLength;
        set
        {
            _bot.PcDownLength = value;
            OnPropertyChanged(nameof(PcDownLength));
        }
    }

    public StrategyParameterInt Lot
    {
        get => _bot.Lot;
        set
        {
            _bot.Lot = value;
            OnPropertyChanged(nameof(Lot));
        }
    }

    public StrategyParameterDecimal Risk
    {
        get => _bot.Risk;
        set
        {
            _bot.Risk = value;
            OnPropertyChanged(nameof(Risk));
        }
    }

    public StrategyParameterDecimal PortfolioSize
    {
        get => _bot.PortfolioSize;
        set
        {
            _bot.PortfolioSize = value;
            OnPropertyChanged(nameof(PortfolioSize));
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


    public static event Action<Enum> RunBot;


    private RelayCommand _startCommand;
    public IRelayCommand StartCommand => _startCommand ??= new RelayCommand(new Action(Start));

    //[RelayCommand]
    public void Start()
    {
        ButtonName = ButtonName == StartStop.Start ? StartStop.Stop : StartStop.Start;
        RunBot?.Invoke(ButtonName);
    }

    public string ButtonContent => ButtonName == StartStop.Stop ? "Выключить робота" : "Включить робота";


}

