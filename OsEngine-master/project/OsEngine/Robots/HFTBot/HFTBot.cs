using OsEngine.Entity;
using OsEngine.Market;
using OsEngine.Market.Servers;
using OsEngine.OsTrader.Panels;
using OsEngine.OsTrader.Panels.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSEngine.Robots.HFT

{
    [Bot("HFTBot")] // добавляем бота в OsEngine
    public class HFTBot : BotPanel
    {
        
        public HFTBot(string name, StartProgram startProgram) : base(name, startProgram) //реализуем конструктор
        {
            ServerMaster.ServerCreateEvent += ServerMaster_ServerCreateEvent; //подписка на событие создания нового сервера
        }

        #region Fields ================================

        private List<IServer> _servers = new List<IServer>(); // List где храняться IServer-а

        private List<Portfolio> _portfolios = new List<Portfolio>(); // List где храняться номера счетов

        private string _nameSecurity = "BTCUSDT"; // название необходимого инструмента

        private ServerType _serverType = ServerType.Binance; //определение подключаемого сервера (в данном случае Binance)

        /// <summary>
        /// переменная, в которой храниться необходимый инструмент
        /// </summary>
        private Security _security = null; 

        /// <summary>
        /// переменная, в которой храниться рабочий сервер
        /// </summary>
        private IServer _server;  

        /// <summary>
        /// переменная для проверки поступления данных с сервера (наличие обрыва связи с сервером).
        /// CandleSeries последовательность свечей. Объект, в котором собираются входящие данные, 
        /// - это свечи.
        /// </summary>
        private CandleSeries _series = null; 


        #endregion


        #region Methods ================================

        private void ServerMaster_ServerCreateEvent(IServer newServer)
        {
            foreach (IServer server in _servers) // Цикл проверки наличия сервера в списке серверов
                                                 // и добавления новых в List где храняться IServer-а
            {
                if (newServer == server)
                {
                    return;
                }
            }

            if (newServer.ServerType == _serverType)
            {
                _server = newServer;
            }

            _servers.Add(newServer);

            newServer.PortfoliosChangeEvent += NewServer_PortfoliosChangeEvent; //событие изменения счетов портфеля
            newServer.SecuritiesChangeEvent += NewServer_SecuritiesChangeEvent; //событие изменения инструмента
            newServer.NeedToReconnectEvent += NewServer_NeadToReconnectEvent; //событие необходимо перезаказать данные у сервера (обрыв связи)
            newServer.NewMarketDepthEvent += NewServer_NewMarketDepthEvent; //событие изменения стакана
            newServer.NewTradeEvent += NewServer_NewTradeEvent;//событие изменения обезличенных сделок
            newServer.NewOrderIncomeEvent += NewServer_NewOrderIncomeEvent;//событие изменения ордера
            newServer.NewMyTradeEvent += NewServer_NewMyTradeEvent;//событие изменения моей сделки
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

        private void NewServer_SecuritiesChangeEvent(List<Security> securities) //метод создания листа инструментов
                                                                                //List<Security> securities 
        {
            if (_security != null)
            {
                return;
            }

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

        private void StartSecurity(Security security) // метод для начала работы с нужным инструментом
        {
            if (security == null)
            {
                Debug.WriteLine("StartSecurity security = null");
                return;
            }

            Task.Run(() => // запуск отдельного потока
            {
                while (true)
                {                    
                    _series = _server.StartThisSecurity(security.Name, new TimeFrameBuilder(StartProgram), security.NameClass); // заказ нужного инструмента с рабочего сервера

                    if (_series != null) // проверка поступления данных с сервера (наличие связи)
                    {
                        break;
                    }

                    Thread.Sleep(1000);
                }
            });

        }

        private void NewServer_PortfoliosChangeEvent(List<Portfolio> newPortfolios) //метод создания листа счетов портфеля
                                                                                    //List<Portfolio> newPortfolios 
        {
            for (int x = 0; x < newPortfolios.Count; x++)
            {
                bool flag = true;

                for (int i = 0; i < _portfolios.Count; i++)
                {
                    if (newPortfolios[x].Number == _portfolios[i].Number)
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    _portfolios.Add(newPortfolios[x]);
                }
            }
        }

        public override string GetNameStrategyType() // метод реализации абстрактного класса PriceChannelFix
        {
            return nameof(HFTBot);
        }

        public override void ShowIndividualSettingsDialog() // метод для обеспечения абстрактного класса PriceChannelFix
        {

        }

        #endregion
    }
}