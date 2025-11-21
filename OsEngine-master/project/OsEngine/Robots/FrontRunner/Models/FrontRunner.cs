using OsEngine.Entity;
using OsEngine.OsTrader.Panels;
using OsEngine.OsTrader.Panels.Attributes;
using OsEngine.OsTrader.Panels.Tab;
using OsEngine.Robots.FrontRunner.Vievs;
using OsEngine.Robots.FrontRunner.ViewModels;
using OSEngine.Robots.MyPriceChannel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OsEngine.Robots.FrontRunner.Models
{
    [Bot("FrontRunnerBot")] // добавляем бота в OsEngine
    public class FrontRunnerBot : BotPanel
    {
        public FrontRunnerBot(string name, StartProgram startProgram) : base(name, startProgram)
        {
            TabCreate(BotTabType.Simple); // получаем вкладку в боте

            _tab = TabsSimple[0]; // "вытаскиваем"  _tab

            _tab.MarketDepthUpdateEvent += _tab_MarketDepthUpdateEvent; // подписываемся на событие изменения стакана 

            _tab.PositionOpeningSuccesEvent += _tab_PositionOpeningFailEvent; // подписываемся на событие срабатывания лимитной заявки 

        }

        #region Fields ================================================================

        public decimal BigVolume = 200;

        public int Offset = 1;

        public int Take = 5;

        public decimal Lot = 2;

        public Position Position = null;

        private BotTabSimple _tab; // вкладка в боте


        #endregion

        #region Properties ==============================================================

        public Edit Edit
        {
            get => _edit;

            set
            {
                _edit = value;

                if (_edit == Edit.Stop
                    && Position != null
                    && Position.State == PositionStateType.Opening)
                {
                    _tab.CloseAllOrderInSystem();

                    _tab.CloseAtProfit(Position, Decimal.MaxValue, Decimal.MinValue);
                }
            }
        }

        Edit _edit = Edit.Stop;

        #endregion

        #region Methods ================================================================

        private void _tab_PositionOpeningFailEvent(Position pos)
        {
            Position = null;
        }

        private void _tab_MarketDepthUpdateEvent(MarketDepth marketDepth)
        {
            if (Edit == Edit.Stop) //проверка заданного состояния бота Stop/Start
            {
                return;
            }

            if (marketDepth.SecurityNameCode != _tab.Security.Name) //проверка соответствия бумаги
            {
                return;
            }

            List<Position> positions = _tab.PositionsOpenAll;

            if (positions != null && positions.Count > 0)
            {
                foreach (Position pos in positions)
                {
                    if (pos.Direction == Side.Sell)
                    {
                        decimal takePrice = Position.EntryPrice - Take * _tab.Security.PriceStep;

                        _tab.CloseAtProfit(Position, takePrice, takePrice);
                    }
                    else if (pos.Direction == Side.Buy)
                    {
                        decimal takePrice = Position.EntryPrice + Take * _tab.Security.PriceStep;

                        _tab.CloseAtProfit(Position, takePrice, takePrice);
                    }
                }
            }

            for (int i = 0; i < marketDepth.Asks.Count; i++) // проходим по Asks-кам стакана
            {
                if (marketDepth.Asks[i].Ask >= BigVolume //если объем больше заданного "крупного объема"
                    && Position == null)
                {
                    /* цена = цена конкретного уровня стакана - шаги отступа * цена шага */
                    decimal price = marketDepth.Asks[i].Price - Offset * _tab.Security.PriceStep;

                    Position = _tab.SellAtLimit(Lot, price); //выставляем сделку на продажу в объеме Lot по цене price

                }  
                /* проверка на состояние позиции */                
                if (Position.State != PositionStateType.Open                   
                    && Position.State != PositionStateType.Opening)                    
                {
                   Position = null;                   
                }                             

                /* проверка для закрытия позиции */
                if (Position != null
                    && marketDepth.Asks[i].Price == Position.EntryPrice //цена равна цене открытой позиции
                    && marketDepth.Asks[i].Ask < BigVolume / 2) // объем по этой цене уменьшился
                {
                    if (Position.State == PositionStateType.Open)
                    {
                        _tab.CloseAtMarket(Position, Position.OpenVolume);//закрытие позиции по маркету
                    }

                    else if (Position.State == PositionStateType.Opening)
                    {
                        _tab.CloseAllOrderInSystem();
                    }
                }
                else if (Position != null
                         && Position.State == PositionStateType.Opening
                         && marketDepth.Asks[i].Ask >= BigVolume
                         && marketDepth.Asks[i].Price > Position.EntryPrice - Offset * _tab.Security.PriceStep)
                {
                    _tab.CloseAllOrderInSystem();
                    Position = null;
                    break;
                }

            }

            for (int i = 0; i < marketDepth.Bids.Count; i++) // проходим по Bids-кам стакана
            {
                if (marketDepth.Bids[i].Bid >= BigVolume //если объем больше заданного "крупного объема"
                    && Position == null)
                {
                    /* цена = цена конкретного уровня стакана + шаги отступа * цена шага */
                    decimal price = marketDepth.Bids[i].Price + Offset * _tab.Security.PriceStep;

                    Position = _tab.BuyAtLimit(Lot, price); //выставляем сделку на покупку в объеме Lot по цене price

                    /* проверка на состояние позиции */
                    if (Position.State != PositionStateType.Open
                        && Position.State != PositionStateType.Opening)
                    {
                        Position = null;
                    }
                }
                
                /* закрытие прзиции Bid*/
                if (Position != null
                    && marketDepth.Bids[i].Price == Position.EntryPrice - Offset * _tab.Security.PriceStep
                    && marketDepth.Bids[i].Bid < BigVolume / 2)
                {
                    if (Position.State == PositionStateType.Open)
                    {
                        _tab.CloseAtMarket(Position, Position.OpenVolume);
                    }

                    else if (Position.State == PositionStateType.Opening)
                    {
                        _tab.CloseAllOrderInSystem();
                    }
                }
                else if (Position != null
                         && Position.State == PositionStateType.Opening
                         && marketDepth.Bids[i].Bid >= BigVolume
                         && marketDepth.Bids[i].Price > Position.EntryPrice - Offset * _tab.Security.PriceStep)
                {
                    _tab.CloseAllOrderInSystem();
                    Position = null;
                    break;
                }
            }
        }

        public override string GetNameStrategyType() // метод реализации абстрактного класса FrontRunnerBot
        {
            return "FrontRunnerBot"; // возвращаем имя бота
        }

        public override void ShowIndividualSettingsDialog() // метод для обеспечения абстрактного класса PriceChannelFix
        {
            FrontRunnerUi ui = new FrontRunnerUi(this);

            ui.Show();
        }

        #endregion

    }

}