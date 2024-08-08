using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsEngine.Charts.CandleChart.Indicators;
using OsEngine.Entity;
using OsEngine.Indicators;
using OsEngine.Logging;
using OsEngine.Market;
using OsEngine.OsTrader.Panels;
using OsEngine.OsTrader.Panels.Attributes;
using OsEngine.OsTrader.Panels.Tab;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OsEngine.Robots._MyRobots.RobotCluster
{
    [Bot("RobotCluster")]
    public class RobotCluster : BotPanel
    {
        public BotTabSimple _tabSimple;
        public BotTabCluster _tabCluster;

        public StrategyParameterBool Mode;
        public StrategyParameterInt Koef;
        public StrategyParameterInt CountCandles;
        public StrategyParameterDecimal Risk;
        public StrategyParameterInt Stop;
        public StrategyParameterInt Take;
        private StrategyParameterDecimal PortfolioSize;
        public StrategyParameterInt MinVolumeLimit;

        private decimal _stopPrice;
        private decimal _takePrice;

        Aindicator _ATR;
       

        public RobotCluster(string name, StartProgram startProgram) : base(name, startProgram)
        {
            TabCreate(BotTabType.Simple);
            _tabSimple = TabsSimple[0];

            TabCreate(BotTabType.Cluster);
            _tabCluster = TabsCluster[0];

            Mode = CreateParameter("Mode", false);
            Koef = CreateParameter("Koef", 3,3,3,3);
            CountCandles = CreateParameter("CountCandles", 5, 3, 3, 3);
            PortfolioSize = CreateParameter("PortfolioSize", 1000000, 1, 1, 1m);
            Risk = CreateParameter("Risk %", 1, 1, 1, 1m);
            Stop = CreateParameter("Stop", 1, 1, 1, 1);
            Take = CreateParameter("Take", 3, 1, 1, 1);
            MinVolumeLimit = CreateParameter("MinVolumeLimit", 10000, 10000, 100000, 100);

            // Create indicator ATR
            _ATR = IndicatorsFactory.CreateIndicatorByName("ATR", name + "Atr", false);
            _ATR = (Aindicator)_tabSimple.CreateCandleIndicator(_ATR, "NewArea");
            ((IndicatorParameterInt)_ATR.Parameters[0]).ValueInt = 100;
            _ATR.Save();

            _tabSimple.CandleFinishedEvent += _tabSimple_CandleFinishedEvent;
        }

        private void _tabSimple_CandleFinishedEvent(List<Candle> candles)
        {
            if (candles.Count < CountCandles.ValueInt 
                || _tabCluster.VolumeClusters.Count < CountCandles.ValueInt)return;

            List<Position> positions = _tabSimple.PositionOpenLong;

            if (positions == null || positions.Count == 0)
            {
                decimal average = 0;
                for (int i = _tabCluster.VolumeClusters.Count - _tabCluster.VolumeClusters.Count; 
                     i < _tabCluster.VolumeClusters.Count - 1; i++)
                {
                    average += _tabCluster.VolumeClusters[i].MaxBuyVolumeLine.VolumeSumm;
                }

                average /= (CountCandles.ValueInt - 1);

                HorizontalVolumeLine hvLast = _tabCluster.VolumeClusters[_tabCluster.VolumeClusters.Count - 2].MaxSummVolumeLine;

                if (hvLast.VolumeSumm <= average * Koef.ValueInt
                    && hvLast.VolumeDelta >= 0
                    && hvLast.VolumeSumm * candles.Last().Close > MinVolumeLimit.ValueInt) return;

                decimal lastAtr = _ATR.DataSeries[0].Last;
                decimal moneyRisk = PortfolioSize.ValueDecimal * Risk.ValueDecimal / 100; 
                decimal volume = moneyRisk/(lastAtr * Stop.ValueInt) ;

                _tabSimple.BuyAtMarket(volume);

                Position pos = positions?[0];
                _stopPrice = pos!.EntryPrice - Stop.ValueInt * lastAtr;
                _takePrice = pos!.EntryPrice + Take.ValueInt * lastAtr;
            }
            else
            {
                foreach (Position pos in positions)
                {
                    if (pos.State == PositionStateType.Open)
                    {
                        _tabSimple.CloseAtStop(pos, _stopPrice, _stopPrice - 100 * _tabSimple.Securiti.PriceStep);
                        _tabSimple.CloseAtStop(pos, _takePrice, _takePrice);
                    }   
                }

            }
        }

        public override string GetNameStrategyType()
        {
            return nameof(RobotCluster);
        }

        public override void ShowIndividualSettingsDialog()
        {
            
        }
    }
}
