using OsEngine.Commands;
using OsEngine.Entity;
using OsEngine.Market;
using OsEngine.Market.Servers;
using OsEngine.Market.Servers.MoexAlgopack.Entity;
using OsEngine.OsTrader.Gui;
using OsEngine.RobotEntity;
using OsEngine.RobotEnums;
using OsEngine.Robots;
using OsEngine.Robots.FrontRunner.ViewModels;
using OsEngine.Views;
using OSEngine.RobotEntity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace OsEngine.ViewModels
{
    public class Robot : BaseVM
    {
        public Robot()
        {
            ServerMaster.ServerCreateEvent += ServerMaster_ServerCreateEvent1;
            _messenger = Messenger.Instance;
            _messenger.Message += _messenger_Message;
            _dispatcher = Dispatcher.CurrentDispatcher; //изменение 3-29 ===============
        }

        #region Properties ================================================================

        public string Header
        {
            get => _header;
            set
            {
                _header = value;
                OnPropertyChanged(nameof(Header));
            }
        }

        string _header = string.Empty;

        public Security? Security => _security;

        public IServer? Server => _server;

        public ConfigRobot? ConfigRobot
        {
            get
            { 
                if (_configRobot == null) {
                    _configRobot = new ConfigRobot()
                    {
                        Header = this.Header,
                        SecurityName = this.Security?.Name ?? "",
                        SecurityClass = this.Security?.NameClass ?? "",
                        ServerType = this.Server?.ServerType ?? ServerType.None,
                        SelectedPortfolio = this.SelectedPortfolio ?? null, //DZ 3-30 номер счета
                        LimitOrders = this.LimitOrders != null ? this.LimitOrders.ToList() : null //DZ 3-31 номер счета
                    };
                }

                return _configRobot;
            }
            set
            {
                _configRobot = value;
                if (ConfigRobot != null) InitServer(ConfigRobot);
            }
        }

        ConfigRobot? _configRobot;

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

        /* изменение от 3-29 =>*/
        public decimal VolumeOrder
        {
            get => _volumeOrder;
            set
            {
                _volumeOrder = value;
                OnPropertyChanged(nameof(VolumeOrder));
            }
        }
        decimal _volumeOrder;

        public decimal PriceOrder
        {
            get => _priceOrder;
            set
            {
                _priceOrder = value;
                OnPropertyChanged(nameof(PriceOrder));
            }
        }
        decimal _priceOrder;

        /* изменение от 3-30 =>*/
        public decimal OpenPrice
        {
            get
            {
                if (Position == null) return 0;
                return Position.OpenPrice;
            }
        }

        public decimal PositionVolume
        {
            get
            {
                if (Position == null) return 0;
                return Position.Volume;
            }
        }
        /*<= изменение от 3-30 */

        public ObservableCollection<LimitOrder> LimitOrders
        {
            get => _limitOrders;
            set
            {
                _limitOrders = value;
                _configRobot.LimitOrders = value.ToList(); //DZ 3-31 номер счета
                OnPropertyChanged(nameof(LimitOrders));
            }
        }

        ObservableCollection<LimitOrder> _limitOrders = new ObservableCollection<LimitOrder>();

        public ObservableCollection<Portfolio> Portfolios
        {
            get => _portfolios;
            set
            {
                _portfolios = value;
                OnPropertyChanged(nameof(Portfolios));
            }
        }
        ObservableCollection<Portfolio> _portfolios = new ObservableCollection<Portfolio>();

        public Portfolio? SelectedPortfolio
        {
            get => _selectedPortfolio;
            set
            {
                _selectedPortfolio = value;
                ConfigRobot.SelectedPortfolio = value; //DZ 3-30 номер счета
                OnPropertyChanged(nameof(SelectedPortfolio));
            }
        }
        Portfolio? _selectedPortfolio;
        /*<= изменение от 3-29*/

        #endregion

        #region Fields ================================

        Messenger _messenger;

        IServer? _server;

        Security? _security;

        List<MyCandle> _candles = new List<MyCandle>();

        int _lastTradeIndex = 0; //последний номер отработанной сделки

        TimeFrame _timeFrame = TimeFrame.Min1;

        Dispatcher _dispatcher; //изменение 3-29 ===============

        public MyPosition? Position;//изменение 3-30 ===============

        #endregion

        #region Commands ================================

        DelegateCommand? _commandChangeSecurity;

        public DelegateCommand? CommandChangeSecurity
        {
            get
            {
                if (_commandChangeSecurity == null) _commandChangeSecurity
                    = new DelegateCommand(ChangeSecurity);
                return _commandChangeSecurity;
            }
        }

        /* изменение от 3-29 =>*/
        DelegateCommand? _commandBuy;
        public DelegateCommand? CommandBuy
        {
            get
            {
                if (_commandBuy == null) _commandBuy = new DelegateCommand((object? o) => SendOrder(Side.Buy));
                return _commandBuy;
            }
        }

        DelegateCommand? _commandSell;
        public DelegateCommand? CommandSell
        {
            get
            {
                if (_commandSell == null) _commandSell = new DelegateCommand((object? o) => SendOrder(Side.Sell));
                return _commandSell;
            }
        }
        /*<= изменение от 3-29*/

        #endregion

        #region Methods ================================================================

        /* изменение от 3-29 =>*/
        private void SendOrder(Side side)
        {
            if (_server != null
                && _server.ServerStatus == ServerConnectStatus.Connect
                && PriceOrder > 0
                && VolumeOrder > 0
                && Security != null
                && SelectedPortfolio != null
                && Position.GetPermission()) //изменение от 3-30 ====================
            {
                Order order = new Order()
                {
                    SecurityClassCode = Security.NameClass,
                    SecurityNameCode = Security.Name,
                    PortfolioNumber = SelectedPortfolio.Number,
                    Side = side,
                    Price = PriceOrder,
                    Volume = VolumeOrder,
                    TypeOrder = OrderPriceType.Limit,
                    NumberUser = NumberGen.GetNumberOrder(StartProgram.IsOsTrader)
                };

                Position.AddNewOrder(order); //изменение от 3-30 ====================

                _server.ExecuteOrder(order);
            }
        }
        /*<= изменение от 3-29*/


        private void _messenger_Message(MessageType type, object message)
        {

        }

        private void ServerMaster_ServerCreateEvent1(IServer server)
        {
           if (_configRobot != null
             && _configRobot.ServerType == server.ServerType)
            {
                ServerMaster_ServerCreateEvent(server);
            }
        }

        private void InitServer(ConfigRobot configRobot)
        {
            if (configRobot.ServerType == ServerType.None) return;

            List<IServer> servers = ServerMaster.GetServers();

            if (servers == null) return;

            foreach (IServer server in servers)
            {
                if (server.ServerType == configRobot.ServerType)
                {
                    ServerMaster_ServerCreateEvent(server);
                }
            }
        }

        private void ChangeSecurity(object? o)
        {
            _messenger.SendMessage(MessageType.ChangeSecurity, this);
            _messenger.SendMessage(MessageType.SaveParameters);
            if (_security != null)
            {
                StartSecurity(_security);
            }
        }

        public void SetSecurity(Security security)
        {
             _security = security;
        }

        public void ServerMaster_ServerCreateEvent(IServer newServer)
        {
            if (_server == newServer)
            {
                return;
            }
            if (_server != null)
            {
                _server.PortfoliosChangeEvent -= NewServer_PortfoliosChangeEvent;
                _server.SecuritiesChangeEvent -= NewServer_SecuritiesChangeEvent;
                _server.NeedToReconnectEvent -= NewServer_NeadToReconnectEvent;
                _server.NewMarketDepthEvent -= NewServer_NewMarketDepthEvent;
                _server.NewTradeEvent -= NewServer_NewTradeEvent;
                _server.NewOrderIncomeEvent -= NewServer_NewOrderIncomeEvent;
                _server.NewMyTradeEvent -= NewServer_NewMyTradeEvent;
                _server.ConnectStatusChangeEvent -= NewServer_ConnectStatusChangeEvent;
            }
            _server = newServer;
            _server.PortfoliosChangeEvent += NewServer_PortfoliosChangeEvent;
            _server.SecuritiesChangeEvent += NewServer_SecuritiesChangeEvent;
            _server.NeedToReconnectEvent += NewServer_NeadToReconnectEvent;
            _server.NewMarketDepthEvent += NewServer_NewMarketDepthEvent;
            _server.NewTradeEvent += NewServer_NewTradeEvent;
            _server.NewOrderIncomeEvent += NewServer_NewOrderIncomeEvent;
            _server.NewMyTradeEvent += NewServer_NewMyTradeEvent;
            _server.ConnectStatusChangeEvent += NewServer_ConnectStatusChangeEvent;
        }

        private void NewServer_ConnectStatusChangeEvent(string state)
        {
        }

        private void AddLimitOrder(LimitOrder limitOrder) {
            if (ConfigRobot.LimitOrders == null) {
                ConfigRobot.LimitOrders = new List<LimitOrder>();
            }
            ConfigRobot.LimitOrders.Add(limitOrder);
        }
        /* изменение от 3-30 =>*/
        private void NewServer_NewMyTradeEvent(MyTrade myTrade)
        {
            Position.AddTrade(myTrade);
            OnPropertyChanged(nameof(PositionVolume));
            OnPropertyChanged(nameof(OpenPrice));
        }
        /*<= изменение от 3-30*/
        /* изменение от 3-29 =>*/
        private void NewServer_NewOrderIncomeEvent(Order order)
        {
            LimitOrder LimitOrder = new LimitOrder()
            {
                PriceOrder = order.Price,
                Volume = order.Volume,
                Status = order.State,
                NUmberUser = order.NumberUser
            };

            _dispatcher.Invoke(() =>
            {
                List<LimitOrder> existingLimitOrder = LimitOrders
                    .Where(item => item.NUmberUser == order.NumberUser && item.Status == order.State)
                    .ToList();

                if (existingLimitOrder.Count == 0) { 
                    LimitOrders.Add(LimitOrder);
                    AddLimitOrder(LimitOrder);
                }
            });
        }
        /*<= изменение от 3-29*/

        private void NewServer_NewTradeEvent(List<Trade> trades)
        {
            Trade trade = trades.Last();

            if (_security != null && trade != null && trade.SecurityNameCode == _security.Name)
            {
                Price = trade.Price;
                for (int i = _lastTradeIndex; i < trades.Count; i++)
                {
                    if (_candles.Count == 0)
                    {
                        _candles.Add(new MyCandle(trade, _timeFrame));
                    }
                    else
                    {
                        MyCandle lastCandle = _candles.Last();

                        if (lastCandle.TimeStart <= trade.Time && trade.Time < lastCandle.TimeStart.AddSeconds((int)_timeFrame))
                        {
                            lastCandle.AddTick(trade);
                        }
                        else
                        {
                            _candles.Add(new MyCandle(trade, _timeFrame));
                        }
                    }
                    
                }

                _lastTradeIndex = trades.Count - 1;
            }
        }

        private void NewServer_NewMarketDepthEvent(MarketDepth marketDepth) 
        {

        }

        private void NewServer_NeadToReconnectEvent() 
        { 

        }

        private void NewServer_SecuritiesChangeEvent(List<Security> securities) 
        { 
            if (_configRobot != null
            && !string.IsNullOrEmpty(_configRobot.SecurityName)
            && !string.IsNullOrEmpty(_configRobot.SecurityClass))
            {
                foreach (Security security in securities)
                {
                    if (security.NameClass == _configRobot.SecurityClass
                    && security.Name == _configRobot.SecurityName)
                    {
                        _security = security;
                        StartSecurity(security);
                    }
                }
            }
        }

        /* изменение от 3-29 =>*/
        private void NewServer_PortfoliosChangeEvent(List<Portfolio> newPortfolios)
        {
            ObservableCollection<Portfolio> portfolios = new ObservableCollection<Portfolio>();

            foreach (Portfolio portfolio in newPortfolios)
            {
                portfolios.Add(portfolio);
            }

            Portfolios = portfolios;

            /*DZ 3-31 номер счета =>*/
            if (_configRobot != null && _configRobot.SelectedPortfolio != null) //DZ 3-31 номер счета
            {                
                foreach (Portfolio portfolio in portfolios)
                {
                    if (portfolio.Number == _configRobot.SelectedPortfolio.Number) {
                        SelectedPortfolio = portfolio;
                    }
                }
            }
            /*<= DZ 3-31 номер счета*/
        }
        /*<= изменение от 3-29*/

        private void StartSecurity(Security security) 
        { 
        if (security == null)
            {
                return;
            }

            Task.Run(() =>
            {
                while (true)
                {
                    var series = _server.StartThisSecurity(security.Name,
                    new TimeFrameBuilder(StartProgram.IsOsTrader), security.NameClass);

                    if (series != null)
                    {
                        /* изменение от 3-30 =>*/

                        Position = new MyPosition(Security);

                        if (_configRobot != null)
                        {
                            if (_configRobot.Orders != null)
                            {
                                foreach (Order order in _configRobot.Orders)
                                {
                                    Position.AddNewOrder(order);
                                }
                            }

                            if (_configRobot.MyTrades != null)
                            {
                                foreach (MyTrade myTrade in _configRobot.MyTrades)
                                {
                                    Position.AddTrade(myTrade);
                                }
                                OnPropertyChanged(nameof(PositionVolume));
                                OnPropertyChanged(nameof(OpenPrice));
                            }
                        }

                        break;
                    }
                    /*<= изменение от 3-30*/

                    Thread.Sleep(1000);
                }
            });
        }

        public void Restore(ConfigRobot config) {
            this.Header = config.Header;
            this.ConfigRobot = config;

            // Восстанавливаем LimitOrders
            if (config.LimitOrders != null) {
                ObservableCollection<LimitOrder> restoringLimitOrders = new ObservableCollection<LimitOrder>();
                foreach (LimitOrder item in config.LimitOrders)
                {
                    restoringLimitOrders.Add(item);
                }
               
                _limitOrders = restoringLimitOrders;
                OnPropertyChanged(nameof(LimitOrders));
            }
        }

        #endregion
    }
}
