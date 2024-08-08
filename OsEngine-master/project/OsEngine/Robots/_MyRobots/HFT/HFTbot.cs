using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OsEngine.Entity;
using OsEngine.Market;
using OsEngine.Market.Servers;
using OsEngine.OsTrader.Panels;
using OsEngine.OsTrader.Panels.Attributes;

namespace OsEngine.Robots._MyRobots.HFT
{
    [Bot("HFTbot")]
    public class HFTbot : BotPanel
    {
        public HFTbot(string name, StartProgram startProgram) : base(name, startProgram)
        {
            Mode = CreateParameter("Mode", false);

            ServerMaster.ServerCreateEvent += ServerMaster_ServerCreateEvent;
        }



        private List<IServer> _servers = [];
        private List<Portfolio> _portfolios = [];

        private ServerType _serverType = ServerType.Transaq;
        private IServer _server;
        private CandleSeries _series = null;

        private string _nameSecurity = "CRU4";
        private Security _security = null;
        private StrategyParameterBool Mode;


        private void ServerMaster_ServerCreateEvent(IServer newServer)
        {
            if (Mode.ValueBool == false)
            {
                return;
            }


            for (int i = 0; i < _servers.Count; i++)
            {
                if (_servers[i] == newServer)
                {
                   return;
                }
            }

            if (newServer.ServerType == _serverType)
            {
                _server = newServer;
            }
            _servers.Add(newServer);

            newServer.PortfoliosChangeEvent += NewServer_PortfoliosChangeEvent;
            newServer.SecuritiesChangeEvent += NewServer_SecuritiesChangeEvent;
            newServer.NeadToReconnectEvent += NewServer_NeadToReconnectEvent;
            newServer.NewMarketDepthEvent += NewServer_NewMarketDepthEvent;
            newServer.NewTradeEvent += NewServer_NewTradeEvent;
            newServer.NewOrderIncomeEvent += NewServer_NewOrderIncomeEvent;
            newServer.NewMyTradeEvent += NewServer_NewMyTradeEvent;

        }

        private void NewServer_NewMyTradeEvent(MyTrade myTrade)
        {
            
        }

        private void NewServer_NewOrderIncomeEvent(Order order)
        {
            
        }

        private void NewServer_NewTradeEvent(List<Trade> trades)
        {
            
        }

        private void NewServer_NewMarketDepthEvent(MarketDepth marketDepth)
        {
            
        }

        private void NewServer_NeadToReconnectEvent()
        {
            StartSecurity(_security);
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
                   _series = _server.StartThisSecurity(security.Name, new TimeFrameBuilder(StartProgram.IsOsTrader), _security.NameClass);

                   if (_series != null)
                   {
                       break;
                   }    
                   Thread.Sleep(1000);
                }
            });

             
        }


        private void NewServer_SecuritiesChangeEvent(List<Security> securities)
        {
            if (_security != null ) return;

            for (int i = 0; i < securities.Count; i++)
            {
                if (_nameSecurity == securities[i].Name)
                {
                    _security = securities[i];
                    StartSecurity(_security);
                    break;
                }
            }
        }

        private void NewServer_PortfoliosChangeEvent(List<Portfolio> newPortfolios)
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

        public override string GetNameStrategyType()
        {
            return nameof(HFTbot);
        }

        public override void ShowIndividualSettingsDialog()
        {
            
        }
    }
}
