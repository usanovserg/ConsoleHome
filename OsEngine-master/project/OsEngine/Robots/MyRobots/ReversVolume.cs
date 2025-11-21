using OsEngine.Entity;
using OsEngine.OsTrader.Panels;
using OsEngine.OsTrader.Panels.Attributes;
using OsEngine.OsTrader.Panels.Tab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OsEngine.Robots.MyRobots
{
    [Bot("ReversVolume")] // добавляем бота в OsEngine
    public class ReversVolume : BotPanel
    {
        public ReversVolume(string name, StartProgram startProgram) : base(name, startProgram)
        {
            TabCreate(BotTabType.Simple); // получаем вкладку в боте

            _tab = TabsSimple[0]; // "вытаскиваем"  _tab

            _tab.CandleFinishedEvent += _tab_CandleFinishedEvent; //подписываемся на событие, которое завершает свечу

            _tab.PositionOpeningSuccesEvent += _tab_PositionOpeningSuccessEvent;//подписываемся на событие для роботы со стопами

            /* Параметры созданные в Filds добавляем в OsEngine */

            _risk = CreateParameter("Risk %", 1m, 0.1m, 10m, 0.1m);

            _profitKoef = CreateParameter("Profit koef", 1m, 1m, 10m, 0.5m);

            _countCandles = CreateParameter("Candles count average volume", 1, 1, 100, 1);

            _countDownCandles = CreateParameter("Count down candles", 1, 1, 7, 1);

            _koefVolume = CreateParameter("koef volumes", 1m, 1m, 10m, 0.1m);
        }

        #region Fields ================================================================

        BotTabSimple _tab;

        /// <summary>
        /// Риск на сделку
        /// </summary>
        private StrategyParameterDecimal _risk;

        /// <summary>
        /// Во сколько раз тейк больше стопа
        /// </summary>
        private StrategyParameterDecimal _profitKoef;

        /// <summary>
        /// Кол-во свечей для вычисления среднего объема
        /// </summary>
        private StrategyParameterInt _countCandles;

        /// <summary>
        /// Кол-во падающих свечей перед объемным разворотом
        /// </summary>
        private StrategyParameterInt _countDownCandles;

        /// <summary>
        /// Во сколько раз объем превышает средний объем для выполнения условия разворота
        /// </summary>
        private StrategyParameterDecimal _koefVolume;
        /// <summary>
        /// Параметр для выставления StopLoss
        /// </summary>
        private int _punktsStop = 0;

        #endregion

        #region Methods ================================================================
        /// <summary>
        /// Событие завершающее свечу
        /// </summary>
        /// <param name="candles"></param>
        private void _tab_CandleFinishedEvent(List<Candle> candles)
        {
            List<Position> positions = _tab.PositionOpenLong; //список всех позиций

            Candle candle = candles.Last(); //"выдергиваем" последнюю свечу в отдельный объект

            /* защита от ситуации, когда открывается несколько позиций подряд*/
            if (positions.Count > 0)
            {
                Position position = positions.First();

                /* Как только Close перенесется выше цены открытия + один Stop,
                 то StopLoss переносится в безубыток*/
                if (candle.Close > position.EntryPrice + _punktsStop * _tab.Security.PriceStep)
                {
                    UpdateStopLoss(position);
                }

                return;
            }
            /* проверка того, что количество свечей больше чем используется для вычисления среднего объема*/
            if (candles.Count < _countCandles.ValueInt + 1) return;

            /* проверка количества падающих свечей */
            for (int i = candles.Count - 2; i > candles.Count - 2 - _countDownCandles.ValueInt; i--)
            {
                if (candles[i].Close > candles[i].Open) return;
            }

            int count = _countCandles.ValueInt;//инициализируем переменную для упрощения формулы расчета среднего объема

            decimal averageVolume = 0; //инициализируем переменную для расчета среднего объема

            for (int i = candles.Count - count; i < candles.Count; i++)
            {
               averageVolume += candles[i].Volume;//расчетываем объем
            }                 

            averageVolume /= count;//расчетываем средний объем

            /*если закрытие свечи меньше чем половина (середина) свечи ИЛИ 
             * объем на последней свече меньше чем объем умноженный на _koefVolume
             тогда - return*/
            if (candle.Close < (candle.High + candle.Low) / 2
                || candle.Volume < averageVolume * _koefVolume.ValueDecimal) return;

            /*расчет количества лотов для открытия позиции*/
            _punktsStop = (int)((candle.Close - candle.Low) / _tab.Security.PriceStep);

            if (_punktsStop < 5) return;

            /*локальная переменная для прервода количества пунктов в деньги. StopLoss в деньгах*/
            decimal amountStop = _punktsStop * _tab.Security.PriceStepCost;

            /*определение объема риска в деньгах. 
             * _tab.Portfolio.ValueBegin - объем средств в нашем портфеле*/
            decimal amountRisk = _tab.Portfolio.ValueBegin * _risk.ValueDecimal / 100;
           
            /*определение объема необходимого для входа в позицию*/
            decimal volume = amountRisk / amountStop;

            /*проверка достаточности средств для ГО*/
            if (_tab.Security.Go > 1)
            {
                decimal maxLot = _tab.Portfolio.ValueBegin / _tab.Security.Go;
                if (maxLot < volume) return;
            }

            _tab.BuyAtMarket(volume); //!!!Отправление заявки на биржу!!!
        }
        /// <summary>
        /// Метод пререноса StopLoss в безубыток
        /// </summary>
        /// <param name="position"></param>
        private void UpdateStopLoss(Position position)
        {
            /*метод снимающий выставленный StopLoss*/
            _tab.SellAtStopCancel(); // метод снимающий выставленный StopLoss

            /*расчет stopMarketPrice. (position.EntryPrice -цена открытия) */
            decimal stopMarketPrice = position.EntryPrice - 100 * _tab.Security.PriceStep;

            /*метод выставляющий новый StopLoss*/
            _tab.CloseAtStop(position, position.EntryPrice, stopMarketPrice);// 
        }

        /// <summary>
        /// Событие для работы со стопами
        /// </summary>
        /// <param name="position"></param>
        private void _tab_PositionOpeningSuccessEvent(Position position)
        {
            /*расчет TakeProfit. (position.EntryPrice -цена открытия) */
            decimal take = position.EntryPrice + _punktsStop * _profitKoef.ValueDecimal;

            /*расчет StopLoss*/
            decimal stop = position.EntryPrice - _punktsStop * _tab.Security.PriceStep;

            /*расчет цены закрытия сделки*/
            decimal stopMarketPrice = stop - 100 * _tab.Security.PriceStep;

            /*Закрытие позици по профиту*/
            _tab.CloseAtProfit(position, take, take);

            /*Закрытие позици по стопу*/
            _tab.CloseAtStop(position, stop, stopMarketPrice);
        }

        public override string GetNameStrategyType() // мотод для обеспечения абстрактного класса ReversVolume 
        {
            return nameof(ReversVolume);
        }

        public override void ShowIndividualSettingsDialog() // мотод для обеспечения абстрактного класса ReversVolume
        {
            //throw new NotImplementedException();
        }

        #endregion

    }
}
