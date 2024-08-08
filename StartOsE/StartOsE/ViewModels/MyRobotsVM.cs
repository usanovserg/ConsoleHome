using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using StartOsE.Entity;
using StartOsE.Market.Servers;
using StartOsE.Candles;
using StartOsE.Entity;
using StartOsE.Market.Servers;
using StartOsE.Entity;
using StartOsE.Market;

namespace StartOsE.ViewModels;

public partial class MyRobotsVM : ObservableObject
{
    public MyRobotsVM(MetroWindow metroWindow)
    {
        ServerMaster.ServerCreateEvent += ServerMaster_ServerCreateEvent;
        _metroWindow = metroWindow;
    }

    public ReadOnlyObservableCollection<Theme> Themes { get; set; } = ThemeManager.Current.Themes;

    private readonly MetroWindow _metroWindow;

    [ObservableProperty]
    private Theme _selectedTheme; 
    partial void OnSelectedThemeChanged(Theme value)
    {
        ThemeManager.Current.ChangeTheme(_metroWindow, SelectedTheme);
    }

    //private Theme _selectedTheme;
    //public Theme SelectedTheme
    //{
    //    get => _selectedTheme;
    //    set
    //    {
    //        _selectedTheme = value;
    //        OnPropertyChanged(nameof(SelectedTheme));

    //        if (_selectedTheme != null)
    //        {
    //            ThemeManager.Current.ChangeTheme(_metroWindow, SelectedTheme);
    //        }
    //    }
    //}

    public ObservableCollection<string> ListSecurities { get; set; } = [];

    //private List<IServer> _servers = [];
    private List<Portfolio> _portfolios = [];
    private List<Security> _securities;

    private ServerType _serverType = ServerType.Transaq;
    private IServer _server;

    private CandleSeries _series = null;

    //private string _nameSecurity = "CRU4";
    private Security _security;

   // private const StartProgram StartProgram = Entity.StartProgram.IsOsTrader;

    private string _selectedSecurity;

    public string SelectedSecurity
    {
        get => _selectedSecurity;
        set
        {
            _selectedSecurity = value;
            OnPropertyChanged(nameof(SelectedSecurity));
            _security = GetSecurityForName(_selectedSecurity);
            StartSecurity(_security);
        }
    }

    private void StartSecurity(Security security)
    {
        if (security == null)
        {
            Debug.WriteLine("Security = null");
            return;
        }

        Task.Run(() =>
        {
            while (true)
            {
                _series = _server.StartThisSecurity(security?.Name, new TimeFrameBuilder(), _security?.NameClass);

                if (_series != null)
                {
                    break;
                }
                Thread.Sleep(1000);
            }
        });
    }


    [RelayCommand]
    private void ServersToConnect()
    {
        ServerMaster.ShowDialog(false);
    }



    private Security GetSecurityForName(string name)
    {
        for (int i = 0; i < _securities.Count; i++)
        {
            if (_securities[i].Name == name)
            {
                return _securities[i];
            }
        }
        return null;
    }

    private void ServerMaster_ServerCreateEvent(IServer newServer)
    {
        //for (int i = 0; i < _servers.Count; i++)
        //{
        //    if (_servers[i] == newServer)
        //    {
        //        return;
        //    }
        //}

        //if (newServer == _server)
        //{
        //    _server = newServer;
        //}

        _server = newServer;

        //_servers.Add(newServer);

        _server.PortfoliosChangeEvent += PortfoliosChangeEvent;
        _server.SecuritiesChangeEvent += SecuritiesChangeEvent;
        _server.NeadToReconnectEvent += NeedToReconnectEvent;
        _server.NewTradeEvent += NewTradeEvent;
        _server.NewOrderIncomeEvent += NewOrderIncomeEvent;
        _server.NewMyTradeEvent += NewMyTradeEvent;
        _server.ConnectStatusChangeEvent += ConnectStatusChangeEvent;
        _server.NewMarketDepthEvent += NewMarketDepthEvent;
    }

    private void NewMarketDepthEvent(MarketDepth depth)
    {

    }

    private void ConnectStatusChangeEvent(string obj)
    {

    }

    private void NewMyTradeEvent(MyTrade myTrade)
    {

    }

    private void NewOrderIncomeEvent(Order order)
    {

    }

    private void NewTradeEvent(List<Trade> trades)
    {

    }


    private void NeedToReconnectEvent()
    {
        StartSecurity(_security);
    }


    private ICollectionView _listSecuritiesView;
    public ICollectionView ListSecuritiesView
    {
        get => _listSecuritiesView;
        set
        {
            _listSecuritiesView = value;
            OnPropertyChanged(nameof(ListSecuritiesView));
        }
    }

    private string _securitiesFilter = string.Empty;
    public string SecuritiesFilter
    {
        get => _securitiesFilter;
        set
        {
            _securitiesFilter = value;
            OnPropertyChanged(nameof(SecuritiesFilter));
            ListSecuritiesView?.Refresh();
        }
    }

    private void SecuritiesChangeEvent(List<Security> securities)
    {
        ObservableCollection<string> listSecurities = [];

        for (int i = 0; i < securities.Count; i++)
        {
            listSecurities.Add(securities[i].Name);
        }

        ListSecurities = listSecurities;

        ListSecuritiesView = CollectionViewSource.GetDefaultView(ListSecurities);

        ListSecuritiesView.Filter = FilterSecurities;

        OnPropertyChanged(nameof(ListSecurities));

        _securities = securities;
    }






    private bool FilterSecurities(object obj)
    {
        if (string.IsNullOrEmpty(SecuritiesFilter))
        {
            return true; // Если фильтр пустой, отображаем все элементы
        }

        if (obj is string item)
        {
            // Фильтруем элементы по содержимому текста фильтра (case insensitive)
            return item.IndexOf(SecuritiesFilter.ToUpperInvariant(), StringComparison.OrdinalIgnoreCase) >= 0;
        }

        return false;

        //if (obj is string item)
        //{
        //    return string.IsNullOrEmpty(SecuritiesFilter.ToUpperInvariant()) 
        //           || item.IndexOf(SecuritiesFilter.ToUpperInvariant(), 
        //                        StringComparison.OrdinalIgnoreCase) >= 0;
        //}   
        //return false;   

        //if (obj is not string securityView || string.IsNullOrEmpty(securityView)) return false;

        //return securityView.Contains(SecuritiesFilter.ToUpperInvariant());
    }

    private void PortfoliosChangeEvent(List<Portfolio> newPortfolios)
    {
        for (int j = 0; j < newPortfolios.Count; j++)
        {
            bool flag = true;
            for (int i = 0; i < _portfolios.Count; i++)
            {
                if (newPortfolios[j].Number == _portfolios[i].Number)
                {
                    flag = false;
                    break;
                }
            }

            if (flag)
            {
                _portfolios.Add(newPortfolios[j]);
            }
        }
    }
}
