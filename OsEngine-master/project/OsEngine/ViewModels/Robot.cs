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
using OsEngine.Entity;
using OsEngine.Market;
using OsEngine.Market.Servers;
using OsEngine.Views;

namespace OsEngine.ViewModels
{
    public partial class Robot: ObservableObject
    {  
        public Robot()
        {
            ServerMaster.ServerCreateEvent += ServerMaster_ServerCreateEvent;
        }

        private IServer? _server;
        private Security? _security;

        private string _header = string.Empty;
		public string Header
		{
			get => _header;
			set
			{
				_header = value;
				OnPropertyChanged(nameof(Header));
			}
		}

        private List<Portfolio> _portfolios = [];

        private CandleSeries _series = null;

        public ObservableCollection<string> ListSecurities { get; set; } = [];
        private List<Security> _securities;
        

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

        [RelayCommand]
        public void ChangeSecurity()
        {
            ChangeSecurityWindow changeSecurityWindow = new(this);
            changeSecurityWindow.ShowDialog();
        }   


        public List<IServer> Servers { get; set; } = [];

        public void SetSecurity(Security security)
        {
            _security = security;
        }


        public void ServerMaster_ServerCreateEvent(IServer? newServer)
        {
            if (_server != null)
            {
                _server.PortfoliosChangeEvent -= PortfoliosChangeEvent;
                _server.SecuritiesChangeEvent -= SecuritiesChangeEvent;
                _server.NeadToReconnectEvent -= NeedToReconnectEvent;
                _server.NewTradeEvent -= NewTradeEvent;
                _server.NewOrderIncomeEvent -= NewOrderIncomeEvent;
                _server.NewMyTradeEvent -= NewMyTradeEvent;
                _server.ConnectStatusChangeEvent -= ConnectStatusChangeEvent;
                _server.NewMarketDepthEvent -= NewMarketDepthEvent;
            }

            _server = newServer;   

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
                ListSecuritiesView.Refresh();
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
}
