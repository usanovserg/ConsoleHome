using OsEngine.Entity;
using OsEngine.Indicators;
using OsEngine.OsTrader.Panels;
using OsEngine.OsTrader.Panels.Attributes;
using OsEngine.OsTrader.Panels.Tab;
using System;
using System.Collections.Generic;
using System.Windows.Documents;


namespace OsEngine.Robots.MyRobotClaster
{
    [Bot("RobotCluster")] // добавляем бота в OsEngine
    public class RobotCluster : BotPanel
    {
        public RobotCluster(string name, StartProgram startProgram) : base(name, startProgram)
        {
            TabCreate(BotTabType.Cluster);
            _tabCluster = TabsCluster[0];
            TabCreate(BotTabType.Simple);
            _tabSimple = TabsSimple[0];

            Mode = CreateParameter("Mode", false);
            Koef = CreateParameter("Koef", 3, 3, 9, 1);
            CountCandles = CreateParameter("CountCandles", 5, 3, 9, 1);
            Risk = CreateParameter("Risk %", 1m, 0.1m, 2m, 0.1m);
            Stop = CreateParameter("Stop ATR", 1, 1, 1, 1);
            Take = CreateParameter("Take ATR", 3, 1, 10, 1);
            Depo = CreateParameter("Depo", 1000, 1000, 1000, 1);
            MinVolumeDollar = CreateParameter("MinVolumeDollar", 100000, 100000, 100000, 50000);

            _atr = IndicatorsFactory.CreateIndicatorByName("ATR", name + "ATR", false);

            _atr.ParametersDigit[0].Value = 100;

            _atr = (Aindicator)_tabSimple.CreateCandleIndicator(_atr, "Second");
            _atr.PaintOn = true;
            _atr.Save();

            _tabSimple.CandleFinishedEvent += _tabSimple_CandleFinishedEvent;

            //_tabSimple.PositionOpenLong +=
        }

        #region Fields ================================

        private BotTabSimple _tabSimple;
        private BotTabCluster _tabCluster;

        public StrategyParameterBool Mode;
        public StrategyParameterInt Koef;
        public StrategyParameterInt CountCandles;
        public StrategyParameterDecimal Risk;
        public StrategyParameterInt Stop;
        public StrategyParameterInt Take;
        public StrategyParameterInt Depo;
        public StrategyParameterInt MinVolumeDollar;

        private Aindicator _atr;
        private decimal _stopPrice = 0;
        private decimal _takePrice = 0;

        #endregion

        #region Methods ================================================================

        private void _tabSimple_CandleFinishedEvent(List<Candle> candles)
        {
            if (candles.Count < CountCandles.ValueInt
            || _tabCluster.VolumeClusters.Count < CountCandles.ValueInt)
            {
                return;
            }

            List<Position> positions = _tabSimple.PositionOpenLong;

            if (positions == null || positions.Count == 0)
            {
                decimal average = 0;

                for (int i = _tabCluster.VolumeClusters.Count - CountCandles.ValueInt - 1;
                i < _tabCluster.VolumeClusters.Count - 2;
                i++)
                {
                    average += _tabCluster.VolumeClusters[i].MaxSummVolumeLine.VolumeSumm;
                }

                average /= (CountCandles.ValueInt - 1);

                HorizontalVolumeLine last = 
                    _tabCluster.VolumeClusters[_tabCluster.VolumeClusters.Count - 2]
                    .MaxSummVolumeLine;

                if (last.VolumeSumm > average * Koef.ValueInt 
                    && last.VolumeDelta < 0 
                    && last.VolumeSumm * last.Price > MinVolumeDollar.ValueInt)
                {
                    decimal lastAtr = _atr.DataSeries[0].Last;
                    decimal manyRisk = Depo.ValueInt * Risk.ValueDecimal / 100;
                    decimal volume = manyRisk / (lastAtr * Stop.ValueInt);
                    _tabSimple.BuyAtMarket(volume);
                    _stopPrice = candles[candles.Count - 1].Close - lastAtr;
                    _takePrice = candles[candles.Count - 1].Close + lastAtr * Take.ValueInt;
                }
            }
        }

        public override string GetNameStrategyType()
        {
            return nameof(RobotCluster);
        }

        public override void ShowIndividualSettingsDialog()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

