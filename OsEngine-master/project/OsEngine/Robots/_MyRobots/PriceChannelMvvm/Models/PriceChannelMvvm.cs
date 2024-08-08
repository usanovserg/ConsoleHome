using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsEngine.Entity;
using OsEngine.Indicators;
using OsEngine.OsTrader.Panels;
using OsEngine.OsTrader.Panels.Attributes;
using OsEngine.OsTrader.Panels.Tab;
using OsEngine.Robots._MyRobots.Entity.Enums;
using OsEngine.Robots._MyRobots.PriceChannelMvvm.Models;
using OsEngine.Robots._MyRobots.PriceChannelMvvm.ViewModels;
using OsEngine.Robots._MyRobots.PriceChannelMvvm.Views;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OsEngine.Robots._MyRobots.PriceChannelMvvm.Models;

[Bot(nameof(PriceChannelMvvm))]
public class PriceChannelMvvm : BotPanel
{
    public BotTabSimple _tab;

    // Indicator setting 
    public StrategyParameterInt PcUpLength;
    public StrategyParameterInt PcDownLength;

    public StrategyParameterString Mode;
    public StrategyParameterInt Lot;
    public StrategyParameterDecimal Risk;
    public StrategyParameterDecimal PortfolioSize;

    // Indicator
    public Aindicator _Pc;

    // The prev value of the indicator
    public decimal PrevUpPc;
    public decimal PrevDownPc;



    public PriceChannelMvvm(string name, StartProgram startProgram) : base(name, startProgram)
    {
        TabCreate(BotTabType.Simple);
        _tab = TabsSimple[0];

        // Indicator setting
        PcUpLength = CreateParameter("Up Line Length", 21, 7, 48, 7, "Indicator");
        PcDownLength = CreateParameter("Down Line Length", 21, 7, 48, 7, "Indicator");

        Mode = CreateParameter("Mode", "Off", new [] {"Off", "On"});
        Lot = CreateParameter("Lot", 10, 5,20,1);
        Risk = CreateParameter("Risk", 1 , 0.25m, 3, 0.1m);
        PortfolioSize = CreateParameter("PortfolioSize", 1000000, 5, 20, 1m);

        // Create indicator PC
        _Pc = IndicatorsFactory.CreateIndicatorByName("PriceChannel", name + "PC", false);
        _Pc = (Aindicator)_tab.CreateCandleIndicator(_Pc, "Prime");
        ((IndicatorParameterInt)_Pc.Parameters[0]).ValueInt = PcUpLength.ValueInt;
        ((IndicatorParameterInt)_Pc.Parameters[1]).ValueInt = PcDownLength.ValueInt;
        _Pc.Save();

        // Subscribe to the indicator update event
        ParametrsChangeByUser += PriceChannelMvvm_ParametrsChangeByUser; ; ;

        // Subscribe to the candle finished event
        _tab.CandleFinishedEvent += _tab_CandleFinishedEvent;

        Vm.RunBot += RunBot;
    }

    


    private void PriceChannelMvvm_ParametrsChangeByUser()
    {
        ((IndicatorParameterInt)_Pc.Parameters[0]).ValueInt = PcUpLength.ValueInt;
        ((IndicatorParameterInt)_Pc.Parameters[1]).ValueInt = PcDownLength.ValueInt;
        _Pc.Save();
        _Pc.Reload();
    }

      
    private void RunBot(Enum value)
    {
        if (value.Equals(StartStop.Stop))
        {
            //_tab.MarketDepthUpdateEvent -= _tab_MarketDepthUpdateEvent;
            //_tab.MarketDepthUpdateEvent += _tab_MarketDepthUpdateEvent;
        }
        else if (value.Equals(StartStop.Start))
        {
            _tab.CloseAllOrderInSystem();
           // _tab.MarketDepthUpdateEvent -= _tab_MarketDepthUpdateEvent;
        }
    }


    private void _tab_CandleFinishedEvent(List<Candle> candles)
    {
        if (Mode.ValueString == "Off") return;
        if (_Pc.DataSeries[0].Values == null || _Pc.DataSeries[1].Values == null
                                             || _Pc.DataSeries[0].Values.Count < PcUpLength.ValueInt + 1
                                             || _Pc.DataSeries[1].Values.Count < PcDownLength.ValueInt + 1) return;

        PrevUpPc = _Pc.DataSeries[0].Values[_Pc.DataSeries[0].Values.Count - 2];
        PrevDownPc = _Pc.DataSeries[1].Values[_Pc.DataSeries[1].Values.Count - 2];

        List<Position> openPositions = _tab.PositionsOpenAll;

        if (candles.Last().Close > PrevUpPc
            && candles.Last().Open < PrevUpPc
            && openPositions.Count == 0)
        {
            decimal riskMany = PortfolioSize.ValueDecimal + Risk.ValueDecimal / 100;
            decimal costPriceStep = _tab.Securiti.PriceStep;

            if (StartProgram == StartProgram.IsTester)
            {
                costPriceStep = 1;
            }

            decimal steps = (PrevUpPc - PrevDownPc) / costPriceStep;
            decimal lot = riskMany / (steps * costPriceStep);

            _tab.BuyAtMarket(/*(int)lot*/Lot.ValueInt);
        }

        if (openPositions.Count > 0)
        {
            Trailing(openPositions);
        }
    }


    private void Trailing(List<Position> openPositions)
    {
        foreach (var pos in openPositions)
        {
            decimal lastDown = _Pc.DataSeries[1].Last;

            if (pos.State == PositionStateType.Open)
            {
                if (pos.Direction == Side.Buy)
                {    
                    _tab.CloseAtTrailingStop(pos, lastDown, lastDown  - _tab.Securiti.PriceStep * 25);
                }
            }
        }
    }



    public override void ShowIndividualSettingsDialog()
    {
        PriceChannelMvvmUi botPriceChannelMvvmUiUi = new(this);
        //botPriceChannelMvvmUiUi.Title = nameof(PriceChannelMvvm);
        botPriceChannelMvvmUiUi.Show();  
    }

    public override string GetNameStrategyType()
    {
        return nameof(PriceChannelMvvm);
    }
}