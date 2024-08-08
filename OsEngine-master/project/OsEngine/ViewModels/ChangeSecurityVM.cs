using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OsEngine.Entity;
using OsEngine.Market;
using OsEngine.Market.Servers;

namespace OsEngine.ViewModels
{
    public partial class ChangeSecurityVM :ObservableObject
    {
        public ChangeSecurityVM(Robot robot)
        {
            _robot = robot;

            //Servers = new ObservableCollection<ServerType>(Enum.GetValues(typeof(ServerType)).Cast<ServerType>().ToList()); 
            Servers = GetServers();
        }

        private Robot _robot;

        public ObservableCollection<SecurityVm> Securities { get; set; }
        public ObservableCollection<ServerType>  Servers { get; set; }

        private ServerType _server = ServerType.None;
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

        private SecurityVm _selectedSecurityVm;
        public SecurityVm SelectedSecurityVm
        {
            get => _selectedSecurityVm;
            set
            {
                _selectedSecurityVm = value;
                OnPropertyChanged(nameof(SelectedSecurityVm));
            }
        }

        public ObservableCollection<string> CodeClasses { get; set; }

        private string _selectedCodeClass = string.Empty;
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

        private ObservableCollection<SecurityVm> GetSecurities(string codeClass)
        {
            ObservableCollection<SecurityVm> securities = [];

            if (Server == ServerType.None) return securities;

            IServer? server = GetServer(Server);
            if (server == null) return securities;

            foreach (var security in server.Securities)
            {
                if (security.NameClass == codeClass) securities.Add(new SecurityVm(security));
            }  
            return securities;
        }


        [RelayCommand]
        public void SelectedSecurity()
        {
            IServer? server = GetServer(Server);
            _robot.ServerMaster_ServerCreateEvent(server);

            _robot.SetSecurity(SelectedSecurityVm.GetSecurity());
        }   



        private ObservableCollection<ServerType> GetServers()
        {
            ObservableCollection<ServerType> newServers = [];

            List<IServer>? servers = ServerMaster.GetServers();

            if (servers == null) return newServers;

            foreach (var server in servers)
            {
                if (server != null && server.ServerStatus == ServerConnectStatus.Connect)
                {
                    newServers.Add(server.ServerType);
                }
            }

            return newServers;
        }

        private ObservableCollection<string> GetCodeClasses(ServerType serverType)
        {
            ObservableCollection<string> codeClasses = [];

            IServer? server = GetServer(serverType);

            if (server == null) return codeClasses;
           
            List<Security> securities = server.Securities;

            if (securities == null) return codeClasses;

            foreach (var security in securities)
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


        private static bool IsCodeClass(Security security, ObservableCollection<string> codeClasses)
        {   
            foreach (var codeClass in codeClasses)
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

            //if (servers == null)
            //{
            //    ServerType type = serverType;
            //    //return null;
            //    if (servers == null ||
            //        servers.Find(serv => serv.ServerType == type) == null)
            //    {
            //        // need to create a server for the first time 
            //        // нужно впервые создать сервер
            //        ServerMaster.CreateServer(type, true);

            //        servers = ServerMaster.GetServers();

            //        if (servers == null)
            //        { // something went wrong / что-то пошло не так
            //            return null;
            //        }
            //        else
            //        { // subscribe to the change status event / подписываемся на событие изменения статуса
            //            IServer myServ = servers.Find(serv => serv.ServerType == type);

            //            if (myServ != null)
            //            {
            //               // myServ.ConnectStatusChangeEvent += ServerStatusChangeEvent;
            //            }
            //        }
            //    }
            //}

            foreach (var server in servers)
            {
                if (server != null && server.ServerType == serverType)
                {
                    return server;
                }
            }

            return null;
        }
        
    }     
}
