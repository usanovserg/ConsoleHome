using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlzEx.Theming;
using Grpc.Core;
using MahApps.Metro.Controls;
using OsEngine.Entity;
using OsEngine.Market;
using OsEngine.Market.Servers;
using OsEngine.Market.Servers.MoexFixFastSpot.FIX;
using OsEngine.Market.Servers.Transaq.TransaqEntity;
using OsEngine.Views;
using WebSocketSharp;
using Order = OsEngine.Entity.Order;
using Security = OsEngine.Entity.Security;
using Trade = OsEngine.Entity.Trade;

namespace OsEngine.ViewModels;

public partial class MyRobotsVM : ObservableObject
{
    public MyRobotsVM() {}

    public MyRobotsVM(MetroWindow metroWindow)
    {  
        _metroWindow = metroWindow; 
    }

    private ObservableCollection<ServerType> _serversTypes;
    public ObservableCollection<ServerType> ServersTypes
    {
        get => _serversTypes;
        set
        {
            _serversTypes = value;
            OnPropertyChanged(nameof(ServersTypes));
        }
    }

    private ObservableCollection<Robot> _robot = [];
    public ObservableCollection<Robot> Robots
    {
        get => _robot;
        set
        {
            _robot = value;
            OnPropertyChanged(nameof(Robots));
        }
    }

    private Robot _selectedRobot;
    public Robot SelectedRobot
    {
        get => _selectedRobot;
        set
        {
            _selectedRobot = value;
            OnPropertyChanged(nameof(SelectedRobot));
        }
    }



    public ReadOnlyObservableCollection<Theme> Themes { get; set; } = ThemeManager.Current.Themes;

    private readonly MetroWindow _metroWindow;

    //[ObservableProperty]
    //private Theme _selectedTheme;
    //partial void OnSelectedThemeChanging(Theme value)
    //{
    //    ThemeManager.Current.ChangeTheme(_metroWindow, SelectedTheme);
    //}

    private Theme _selectedTheme;
    public Theme SelectedTheme
    {
        get => _selectedTheme;
        set
        {
            _selectedTheme = value;
            OnPropertyChanged(nameof(SelectedTheme));

            if (_selectedTheme != null)
            {
                ThemeManager.Current.ChangeTheme(_metroWindow, SelectedTheme);
            }
        }
    }

    //private List<IServer> _servers = [];  
   

    private ServerType _serverType;  

    private ObservableCollection<ServerType> _serverTypes = [];


    //public ObservableCollection<ServerType> ServerTypes
    //{
    //    get => _serverTypes;
    //    set
    //    {
    //        _serverTypes = value;
    //        OnPropertyChanged(nameof(ServerTypes));
    //    }
    //}

    [RelayCommand]
    private void ServersToConnect()
    {
        ServersTypes = new ObservableCollection<ServerType>(ServerMaster.ServersTypes);
        ServerMaster.ShowDialog(false);
    }
    

    [RelayCommand]
    public void AddStrategy()
    {
        Robot robot = new ();
        robot.Header = "Робот " + (Robots.Count + 1).ToString();
        Robots.Add(robot);
    }    
}
