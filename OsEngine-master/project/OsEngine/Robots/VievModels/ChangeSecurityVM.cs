using OsEngine.Commands;
using OsEngine.Entity;
using OsEngine.Market;
using OsEngine.Market.Servers;
using OsEngine.Robots;
using OsEngine.Robots.FrontRunner.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OsEngine.ViewModels
{
    public class ChangeSecurityVM : BaseVM
    {
        public ChangeSecurityVM(Robot robot)
        {
            _robot = robot;
            Servers = GetServers();
        }

        #region Properties ================================

        public ObservableCollection<ServerType> Servers { get; set; }

        public ServerType Server
        {
            get => _server;
            set
            {
                _server = value;
                OnPropertyChanged(nameof(Server));
                CodeClasses = GetCodeClasses(_server);
                OnPropertyChanged(nameof(CodeClasses));
            }
        }

        ServerType _server = ServerType.None;

        public ObservableCollection<string> CodeClasses { get; set; }

        public string SelectedCodeClass
        {
            get => _selectedCodeClass;
            set
            {
                _selectedCodeClass = value;
                OnPropertyChanged(nameof(SelectedCodeClass));

                Securities = GetSecurities(SelectedCodeClass);
                OnPropertyChanged(nameof(Securities));
            }
        }

        string _selectedCodeClass = string.Empty;

        public ObservableCollection<SecurityVM> Securities { get; set; }

        public SecurityVM SelectedSecurityVM
        {
            get => _selectedSecurityVM;
            set
            {
                _selectedSecurityVM = value;
            }
        }

        SecurityVM _selectedSecurityVM;

        #endregion

        #region Fields ================================

        Robot _robot;

        #endregion

        #region Commands ================================

        private DelegateCommand? _commandSelectedSecurity;

        public DelegateCommand? CommandSelectedSecurity
        {
            get
            {
                if (_commandSelectedSecurity == null) _commandSelectedSecurity
                    = new DelegateCommand(SelectedSecurity);
                return _commandSelectedSecurity;
            }
        }

        #endregion

        #region Methods ================================

        private void SelectedSecurity(object? o)
        {
            IServer server = GetServer(Server);
            _robot.ServerMaster_ServerCreateEvent(server);
            _robot.SetSecurity(SelectedSecurityVM.GetSecurity());
        }

        private ObservableCollection<ServerType> GetServers()
        {
            ObservableCollection<ServerType> newServers = new ObservableCollection<ServerType>();
            List<IServer>? servers = ServerMaster.GetServers();
            if (servers == null) return newServers;
            foreach (IServer server in servers)
            {
                if (server != null
                    && server.ServerStatus == ServerConnectStatus.Connect)
                {
                    newServers.Add(server.ServerType);
                }
            }
            return newServers;
        }

        private ObservableCollection<string> GetCodeClasses(ServerType serverType)
        {
            ObservableCollection<string> codeClasses = new ObservableCollection<string>();

            IServer? server = GetServer(serverType);

            if (server == null) return codeClasses;

            List<Security> securities = server.Securities;

            foreach (Security security in securities)
            {
                if (codeClasses.Count == 0) codeClasses.Add(security.NameClass);
                else
                {
                    if (!IsCodeClass(security, codeClasses))
                    {
                        codeClasses.Add(security.NameClass);
                    }
                }
            }

            return codeClasses;
        }

        private ObservableCollection<SecurityVM> GetSecurities(string codeClass)
        {
            ObservableCollection<SecurityVM> securities = new ObservableCollection<SecurityVM>();

            if (Server == ServerType.None) return securities;

            IServer? server = GetServer(Server);

            if (server == null) return securities;

            foreach (Security security in server.Securities)
            {
                if (security.NameClass == codeClass) securities.Add(new SecurityVM(security));
            }

            return securities;
        }

        private bool IsCodeClass(Security security, ObservableCollection<string> codeClasses)
        {
            foreach (string codeClass in codeClasses)
            {
                if (codeClass == security.NameClass)
                {
                    return true;
                }
            }
            return false;
        }

        private IServer? GetServer(ServerType serverType)
        {
            List<IServer>? servers = ServerMaster.GetServers();
            if (servers == null) return null;
            foreach (IServer server in servers)
            {
                if (server != null
                    && server.ServerType == serverType)
                {
                    return server;
                }
            }

            return null;
        }

        #endregion
    }
}