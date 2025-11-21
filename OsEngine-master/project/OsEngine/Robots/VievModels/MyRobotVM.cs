using ControlzEx.Theming;
using MahApps.Metro.Controls;
using OsEngine.Commands;
using OsEngine.Entity;
using OsEngine.Market;
using OsEngine.Market.Servers;
using OsEngine.RobotEntity;
using OsEngine.RobotEnums;
using OsEngine.Robots.FrontRunner.ViewModels;
using OsEngine.Views;
using OSEngine.RobotEntity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace OsEngine.ViewModels
{
    public class MyRobotVM : BaseVM
    {
        public MyRobotVM(MetroWindow metroWindow)
        {
            _metroWindow = metroWindow;
            Init();
        }

        #region Properties ================================================================

        public ReadOnlyObservableCollection<Theme> Themes { get; set; }
            = ThemeManager.Current.Themes;

        public Theme SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                _selectedTheme = value;
                OnPropertyChanged(nameof(SelectedTheme));
                if (SelectedTheme != null)
                {
                    ThemeManager.Current.ChangeTheme(_metroWindow, SelectedTheme);
                    _config.SaveConfig(_metroWindow, SelectedTheme);
                }
            }
        }

        Theme _selectedTheme;

        public ObservableCollection<Robot> Robots
        {
            get => _robot;
            set
            {
                _robot = value;
                OnPropertyChanged(nameof(Robots));
            }
        }

        ObservableCollection<Robot> _robot = new ObservableCollection<Robot>();

        public Robot SelectedRobot
        {
            get => _selectedRobot;
            set
            {
                _selectedRobot = value;
                OnPropertyChanged(nameof(SelectedRobot));
            }
        }
         
        Robot _selectedRobot = new Robot();

        #endregion

        #region Fields ================================

        Messenger _messenger;
        IServer _server;
        List<IServer> _servers = new List<IServer>();
        List<Security> _securities = new List<Security>();
        Security _security;
        MetroWindow _metroWindow;
        Config _config;

        #endregion

        #region Commands ================================

        private DelegateCommand _commandServersToConnect;

        public DelegateCommand CommandServersToConnect
        {
            get
            {
                if (_commandServersToConnect == null)
                    _commandServersToConnect = new DelegateCommand(ServersToConnect);
                return _commandServersToConnect;
            }
        }

        private DelegateCommand _commandAddStrategy;

        public DelegateCommand CommandAddStrategy
        {
            get
            {
                if (_commandAddStrategy == null) _commandAddStrategy
                    = new DelegateCommand(AddStrategy);
                return _commandAddStrategy; 
            }
        }
        
        /* отличие от 3-29 =>*/
        private DelegateCommand? _commandRemoveTab;

        public DelegateCommand CommandRemoveTab
        {
            get 
            {
                if (_commandRemoveTab == null) _commandRemoveTab = new DelegateCommand(RemoveTab);
                return _commandRemoveTab;
            }
        }
        /*<= отличие от 3-29*/  

        #endregion

        #region Methods ================================================================

        /* отличие от 3-29 =>*/
        private void RemoveTab(object? obj) 
        {
            if (obj is string header
                && header != "")
            {
                Robot? robot = GetRobot(header);

                if (robot != null)
                {
                    MessageBoxResult result = MessageBox.Show("Удалить вкладку " + SelectedRobot.Header + "?", "", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        Robots.Remove(robot);
                    }
                }
            }            
        }

        private Robot? GetRobot(string header)
        {
            foreach (var robot in Robots)
            {
                if (robot.Header == header)
                {
                    return robot;
                }
            } 

            return null;
        }
        /*<= отличие от 3-39*/ 

        private void Init()
        {
            _messenger = Messenger.Instance;
            _messenger.Message += _messenger_Message;
            _config = Config.LoadConfig();
            Recovery(_config);
            _metroWindow.Closing += _metroWindow_Closing;
        }

        private void _messenger_Message(MessageType type, object message)
        {
            switch (type)
            {
                case MessageType.SaveParameters:
                    ReadConfigs();
                    _config.SaveConfig(_metroWindow, SelectedTheme);
                    break;

                case MessageType.ChangeSecurity:
                    if (message is Robot robot) ChangeSecurity(robot);
                    break;
            }
        }

        private void ChangeSecurity(Robot robot)
        {
            ChangeSecurityWindow changeSecurityWindow = new ChangeSecurityWindow(robot);

            ThemeManager.Current.ChangeTheme(changeSecurityWindow, SelectedTheme);

            changeSecurityWindow.ShowDialog();
        }

        private void _metroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sender is MetroWindow window)
            {
                ReadConfigs();
                _config.SaveConfig(window, SelectedTheme);
            }
        }

        private void ReadConfigs()
        {
            if (Robots.Count == 0) return;

            List<ConfigRobot> list = new List<ConfigRobot>();
            foreach (Robot robot in Robots)
            {
                ConfigRobot configRobot = new ConfigRobot()
                {
                    Header = robot.Header,
                    SecurityName = robot.Security?.Name ?? "",
                    SecurityClass = robot.Security?.NameClass ?? "",
                    ServerType = robot.Server?.ServerType ?? ServerType.None,
                    SelectedPortfolio = robot.SelectedPortfolio ?? null, //DZ 3-30 номер счета
                    LimitOrders = robot.LimitOrders != null ? robot.LimitOrders.ToList() : null //DZ 3-31 номер счета
                };

                if (robot.Position != null)
                {
                    configRobot.Orders = robot.Position.Orders;
                    configRobot.MyTrades = robot.Position.MyTrades;
                }

                list.Add(configRobot);
            }

            _config.ConfigRobots = list;
        }

        /*метод для применения восстановленных данных из config к окну*/
        private void Recovery(Config config)
        {
            _metroWindow.Left = config.Left;
            _metroWindow.Top = config.Top;
            _metroWindow.Height = config.Height;
            _metroWindow.Width = config.Width;

            if (!string.IsNullOrEmpty(config.Theme))
            {
                SelectedTheme = GetThemeFromString(config.Theme);
            }

            if (config.ConfigRobots.Count > 0)
            {
                foreach (ConfigRobot configRobot in config.ConfigRobots)
                {
                    AddStrategy(configRobot);
                }
            }
        }

          /*метод получения темы из строки*/
        private Theme? GetThemeFromString(string theme)
        {
            foreach (Theme item in ThemeManager.Current.Themes)
            {
                if (item.Name == theme) return item;
            }

            return null;
        }

        private void AddStrategy(object? o)
        {
            AddStrategy(null);
        }

        private void AddStrategy(ConfigRobot? configRobot)
        {
            Robot robot = new Robot();
            if (configRobot != null)
            {
                robot.Restore(configRobot);
            }
            else
            {
                robot.Header = "Tab " + (Robots.Count + 1).ToString();
            }

            Robots.Add(robot);

            SelectedRobot = robot;
        }

        private void ServersToConnect(object o)
        {
            ServerMaster.ShowDialog(false);
        }

        #endregion
    }
}