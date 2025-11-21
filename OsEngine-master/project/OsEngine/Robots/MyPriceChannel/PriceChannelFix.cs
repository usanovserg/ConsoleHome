using OsEngine.Entity;
using OsEngine.Indicators;
using OsEngine.Market.Servers.TraderNet.Entity;
using OsEngine.OsTrader.Panels;
using OsEngine.OsTrader.Panels.Attributes;
using OsEngine.OsTrader.Panels.Tab;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OsEngine.Market.Servers.Deribit.Entity.ResponseChannelUserChanges;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OSEngine.Robots.MyPriceChannel
{
    [Bot("PriceChannelFix")] // добавляем бота в OsEngine
    public class PriceChannelFix : BotPanel
    {
        public PriceChannelFix(string name, StartProgram startProgram) : base(name, startProgram) //реализуем конструктор
        {
            TabCreate(BotTabType.Simple); // получаем вкладку в боте

            _tab = TabsSimple[0]; // "вытаскиваем"  _tab

            LengthUp = CreateParameter("Length Up", 12, 5, 80, 2); //параметры верхней линии

            LengthDown = CreateParameter("Length Down", 12, 5, 80, 2); //параметры нижней линии

            Mode = CreateParameter("Mode", "Off", new[] { "Off", "On" }); //параметр "вкл/выкл" бот

            Lot = CreateParameter("Lot", 10, 5, 20, 1); // параметр количество лотов

            Risk = CreateParameter("Risk", 1m, 0.25m, 3m, 0.1m); // параметр процент риска

            _pc = IndicatorsFactory.CreateIndicatorByName("PriceChannel", name + "PriceChannel", false);// создаем индикатор 

            _pc.ParametersDigit[0].Value = LengthUp.ValueInt;

            _pc.ParametersDigit[1].Value = LengthDown.ValueInt;

            _pc = (Aindicator)_tab.CreateCandleIndicator(_pc, "Prime");/* переносим созданный индикатор 
            на вкладку _tab. Параметр "Prime" обеспечивает размещение индикатора вместе со свечами*/

            _pc.Save(); //сохраняем готовые параметры

            _tab.CandleFinishedEvent += _tab_CandleFinishedEvent; //подписываемся на событие
        }

        #region  Filds ===================================

        private BotTabSimple _tab;

        private Aindicator _pc;

        private StrategyParameterInt LengthUp; //высота верхней линии

        private StrategyParameterInt LengthDown; //высота нижней линии

        private StrategyParameterString Mode;

        private StrategyParameterInt Lot;

        private StrategyParameterDecimal Risk;

        #endregion

        #region  Methods  ===================================

        private void _tab_CandleFinishedEvent(List<Candle> candles)
        {

            if (Mode.ValueString == "Off") // эащита по режиму
            {
                return;
            }

            /* защита при пустой серии и при серии меньшей заданной в параметрах*/
            if (_pc.DataSeries[0].Values == null || _pc.DataSeries[1].Values == null
            || _pc.DataSeries[0].Values.Count < LengthUp.ValueInt + 1
            || _pc.DataSeries[1].Values.Count < LengthDown.ValueInt + 1)
            {
                return;
            }

            Candle candle = candles[candles.Count - 1];// последняя свеча

            decimal lastUp = _pc.DataSeries[0].Values[_pc.DataSeries[0].Values.Count - 2];// последнее значение для индикатора (по предпоследней свече)

            decimal lastDown = _pc.DataSeries[1].Values[_pc.DataSeries[1].Values.Count - 2];

            List<Position> positions = _tab.PositionsOpenAll; // список открытых позиций

            if (candle.Close > lastUp // свеча закрылась выше верхней линии 
                && candle.Open < lastUp // свеча открылась ниже верхней линии
                && positions.Count == 0 // открытых позиций нет
                && (Mode.ValueString == "On")) // бот подключен
            {
                decimal riskMoney = _tab.Portfolio.ValueBegin * Risk.ValueDecimal / 100;

                decimal costPriceStep = _tab.Security.PriceStepCost;

                costPriceStep = 1;

                decimal steps = (lastUp - lastDown) / _tab.Security.PriceStep;

                decimal lot = riskMoney / (steps * costPriceStep);

                _tab.BuyAtMarket((int)lot);
            }

            if (positions.Count > 0)
            {
                Trailing(positions); 
            }
        }
        private void Trailing(List<Position> positions) // трейлинг позиции
        {

            decimal lastDown = _pc.DataSeries[1].Values.Last(); // последнее значение lastDown

            decimal lastUp = _pc.DataSeries[0].Values.Last(); // последнее значение lastUp

            foreach (Position pos in positions)
            {

                if (pos.State == PositionStateType.Open) //если позиция открыта
                {
                    if (pos.Direction == Side.Buy) // если позиция открыта в Buy
                    {
                        /*выставляется TrailingStop ордер для позиции*/
                        _tab.CloseAtTrailingStop(pos, lastDown, lastDown - 100 * _tab.Security.PriceStep);
                    }
                }

            }
        }

        public override string GetNameStrategyType() // метод реализации абстрактного класса PriceChannelFix
        {
            return nameof(PriceChannelFix); // возвращаем имя бота
        }


        public override void ShowIndividualSettingsDialog() // метод для обеспечения абстрактного класса PriceChannelFix
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}