using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.ComponentModel;
using OpenTK.Graphics.ES11;
using OsEngine.Entity;
using OsEngine.Market.Servers.GateIo.GateIoFutures.Entities.Response;
using OsEngine.OsTrader.Panels;
using OsEngine.OsTrader.Panels.Attributes;
using OsEngine.OsTrader.Panels.Tab;
using OsEngine.Robots._MyRobots.BotMvvmTest;
using OsEngine.Robots._MyRobots.BotMvvmTest.ViewModels;
using OsEngine.Robots._MyRobots.Entity.Enums;
using OsEngine.Robots._MyRobots.FrontRunner.ViewModels;
using OsEngine.Robots._MyRobots.FrontRunner.Views;
using OsEngine.Robots._MyRobots.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static OsEngine.Robots._MyRobots.FrontRunner.ViewModels.Vm;
using static OsEngine.Robots._MyRobots.Services.Extensions;
using Vm = OsEngine.Robots._MyRobots.FrontRunner.ViewModels.Vm;
using Window = System.Windows.Window;

namespace OsEngine.Robots._MyRobots.FrontRunner.Models  
{   
    [Bot("FrontRunnerBot")]
    public class FrontRunnerBot : BotPanel 
    {   
        public FrontRunnerBot(string name, StartProgram startProgram) : base(name, startProgram)
        {
            TabCreate(BotTabType.Simple);
            
            _tab = TabsSimple[0];

           // Regime = CreateParameter("Regime", "Off", new[] { "Off", "On", "OnlyLong", "OnlyShort" }); 
            //_tab.MarketDepthUpdateEvent += _tab_MarketDepthUpdateEvent;

            _tab.ManualPositionSupport.DisableManualSupport();

            //Vm.RunBot += RunBot;

            Positions = _tab.PositionsOpenAll;
           // positionsClosed = _tab.PositionsCloseAll;
        }


        public BotTabSimple _tab;

        public decimal BigVolume = 2000;
        public int Offset = 1;
        public int Profit = 25;
        public decimal Lot = 1;

        private bool _isCloseOrderSet = false;
        private bool _inPosition = true;

        //public StartStop ButtonName { get; set; }

        public Regime RegimeSelect { get; set; }
        private Position _position = null;
        public List<Position> Positions { get; set; }
        //public List<Position> positionsClosed { get; set; } 
        //public List<Trade> MyTrades { get; set; }
        //


        public void RunBot(Enum value)
        {
            if (value.Equals(StartStop.Stop))
            {
                _tab.MarketDepthUpdateEvent += MarketDepthUpdateEvent;
            }
            else /*if(value.Equals(StartStop.Start))*/
            {
                _tab.CloseAllOrderInSystem();
                _tab.MarketDepthUpdateEvent -= MarketDepthUpdateEvent;
            }
        }


        private void MarketDepthUpdateEvent(MarketDepth marketDepth)
        {
            if (marketDepth.SecurityNameCode != _tab.Securiti.Name) return;   
            
            _inPosition = Positions != null && Positions.Count != 0;

            if (!_inPosition)
            {
                LogicOpen(marketDepth); 
            }
            else if (_inPosition)
            {
                _position = Positions![0];
                LogicClose(_position, marketDepth);
            }
        }


        private void LogicOpen(MarketDepth marketDepth)
        {
            if (RegimeSelect != Regime.OnlyLong)
            {
                for (int i = 0; i < marketDepth.Asks.Count; i++)
                {
                    if (marketDepth.Asks[i].Ask <= BigVolume) continue;

                    decimal priceSell = marketDepth.Asks[i].Price - Offset * _tab.Securiti.PriceStep;

                    decimal exitPriceTemp = priceSell - Profit * _tab.Securiti.PriceStep;

                    string exitPrice = RoundPrice(exitPriceTemp, _tab.Securiti, Side.Sell);

                    _inPosition = true;
                    _tab.SellAtLimit(Lot, priceSell, exitPrice);
                    break;
                }
            }

            if (_inPosition == true)  return;

            if (RegimeSelect == Regime.OnlyShort) return;

            for (int i = 0; i < marketDepth.Bids.Count; i++)
            {
                if (marketDepth.Bids[i].Bid <= BigVolume) continue;
                decimal priceBuy = marketDepth.Bids[i].Price + Offset * _tab.Securiti.PriceStep;

                decimal exitPriceTemp = priceBuy + Profit * _tab.Securiti.PriceStep;

                string exitPrice = RoundPrice(exitPriceTemp, _tab.Securiti, Side.Buy);
                _inPosition = true;
                _tab.BuyAtLimit(Lot, priceBuy, exitPrice);
                break;
            }
        }     

        private void LogicClose(Position position, MarketDepth marketDepth)
        {
            if (position.Direction == Side.Buy)
            {
                for (int i = 0; i < marketDepth.Bids.Count; i++)
                {
                    if (marketDepth.Bids[i].Bid < BigVolume / 3
                        && marketDepth.Bids[i].Price
                        == position.EntryPrice - Offset * _tab.Securiti.PriceStep)
                    {
                        if (position.State == PositionStateType.Open)
                        {
                            _tab.CloseAtMarket(position, position.OpenVolume);
                            _isCloseOrderSet = false;
                        }

                        if (position.State == PositionStateType.Opening)
                        {
                            _tab.CloseAllOrderInSystem();
                            _isCloseOrderSet = false;
                        }
                    }

                    else if (position.State == PositionStateType.Opening
                             && marketDepth.Bids[i].Bid > BigVolume
                             && marketDepth.Bids[i].Price >
                             position.EntryPrice /*- Offset * _tab.Securiti.PriceStep*/
                             || RegimeSelect == Regime.OnlyShort)
                    {
                        _tab.CloseAllOrderInSystem();
                        _isCloseOrderSet = false;
                        break;
                    }
                }

                if (_tab.PriceBestAsk - _tab.Securiti.PriceStep*2 < position.EntryPrice)
                {
                    _tab.CloseAllOrderInSystem();
                    _isCloseOrderSet = false;
                }

                if (_isCloseOrderSet) return;
                decimal takePrice = position.EntryPrice + Profit * _tab.Securiti.PriceStep;
                _tab.CloseAtProfit(position, takePrice, takePrice);
                _isCloseOrderSet = true;
            }


            else
            {
                for (int i = 0; i < marketDepth.Asks.Count; i++)
                {
                    if (marketDepth.Asks[i].Ask < BigVolume / 3 &&
                        marketDepth.Asks[i].Price
                        == position.EntryPrice + Offset * _tab.Securiti.PriceStep)
                    {
                        if (position.State == PositionStateType.Open)
                        {
                            _tab.CloseAtMarket(position, position.OpenVolume);
                            _isCloseOrderSet = false;

                        }
                    }

                    if (marketDepth.Asks[i].Ask < BigVolume &&
                        marketDepth.Asks[i].Price
                        == position.EntryPrice + Offset * _tab.Securiti.PriceStep
                        && position.State == PositionStateType.Opening
                        || RegimeSelect == Regime.OnlyLong)
                    {
                        _tab.CloseAllOrderInSystem();
                        _isCloseOrderSet = false;
                    }

                    else if (position.State == PositionStateType.Opening
                             && marketDepth.Asks[i].Ask > BigVolume
                             && marketDepth.Asks[i].Price < position.EntryPrice /*+ Offset * _tab.Securiti.PriceStep*/)
                    {
                        _tab.CloseAllOrderInSystem();
                        _isCloseOrderSet = false;
                        break;
                    }
                }

                if (_tab.PriceBestBid + _tab.Securiti.PriceStep*2 > position.EntryPrice)
                {
                    _tab.CloseAllOrderInSystem();
                    _isCloseOrderSet = false;
                }

                decimal takePrice = position.EntryPrice - Profit * _tab.Securiti.PriceStep;

                if (_isCloseOrderSet) return;

                _tab.CloseAtProfit(position, takePrice, takePrice);
                _isCloseOrderSet = true;
            }

            //foreach (Position pos in position)
            //{
            //    if (pos.Direction == Side.Sell)
            //    {
            //        decimal takePrice = pos.EntryPrice - Profit * _tab.Securiti.PriceStep;

            //        _tab.CloseAtProfit(pos, takePrice, takePrice);
            //    }
            //    else if (pos.Direction == Side.Buy)
            //    {
            //        decimal takePrice = pos.EntryPrice + Profit * _tab.Securiti.PriceStep;

            //        _tab.CloseAtProfit(pos, takePrice, takePrice);
            //    }
            //}  
        }     
        

        public override string GetNameStrategyType()
        {
            return nameof(FrontRunnerBot);
        }


        private bool _canUiWindowOpen = true;

        public override void ShowIndividualSettingsDialog()
        {
            if (!_canUiWindowOpen) return;

            FrontRunnerUi botFrontRunnerUiWindow = new(this);
            _canUiWindowOpen = false;

            botFrontRunnerUiWindow.ShowActivated = true;
            botFrontRunnerUiWindow.Title = NameStrategyUniq + " UI";
            botFrontRunnerUiWindow.Show();  
        }
    }
}
