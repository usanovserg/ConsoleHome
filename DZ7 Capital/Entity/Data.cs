using Capital.Enums;
using Capital_comments.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Capital.Entity
{
    internal class Data
    {
        public Data(decimal depoStart, StrategyType strategyType)
        {
            StrategyType = strategyType;
            Depo = depoStart;
            // Добавляем начальное значение депозита в историю эквити
            ListEquity.Add(depoStart);

            // Устанавливаем цвет в зависимости от типа стратегии
            LineColor = ColorConstants.GetGraphColor(strategyType);
        }
        #region Properties ==============================================

        /// <summary>
        /// Свойство цвет кисти
        /// </summary>
        public Brush LineColor { get; private set; }

        public StrategyType StrategyType { get; set; }

        /// <summary>
        /// Начальный депозит
        /// </summary>
        public decimal Depo
        {
            get => _depo;
            set
            {
                _depo = value;
                ResultDepo = value;
            }
        }
        decimal _depo;

        /// <summary>
        /// Результат эквити (депо) 
        /// </summary>
        public decimal ResultDepo
        {
            get => _resultDepo;
            set
            {
                _resultDepo = value;

                Profit = ResultDepo - Depo;

                PercentProfit = Profit * 100 / Depo;

                ListEquity.Add(ResultDepo);

                CalcDrawDown();
            }
        }
        decimal _resultDepo;

        /// <summary>
        /// Максимальная абсолютная просадка в деньгах 
        /// </summary>
        public decimal MaxDrawDown
        {
            get => _maxDrawDown;

            set
            {
                _maxDrawDown = value;

                CalcPercentDrawDown();
            }
        }
        decimal _maxDrawDown;


        /// <summary>
        /// Прибыль 
        /// </summary>
        public decimal Profit { get; set; }

        /// <summary>
        /// Процент прибыли 
        /// </summary>
        public decimal PercentProfit { get; set; }

        /// <summary>
        /// Максимальная относительная просадка в процентах
        /// </summary>
        public decimal PercentDrawDown { get; set; }

        #endregion Properties ==============================================

        #region Fields ==============================================
        /// <summary>
        /// Список ResultDepo результатов эквити (депо)
        /// </summary>
        List<decimal> ListEquity = new List<decimal>();

        private decimal _max = 0;

        private decimal _min = 0;

        #endregion

        #region Methods ==============================================

        /// <summary>
        /// Добавляет текущее значение депозита в историю эквити
        /// </summary>
        /// <param name="currentDepo">Текущее значение депозита</param>
        public void AddEquityPoint(decimal currentDepo)
        {
            ListEquity.Add(currentDepo);
        }

        public List<decimal> GetListEquity()
        {
            return ListEquity;
        }

        /// <summary>
        /// Расчет просадки
        /// </summary>
        private void CalcDrawDown()
        {
            if (_max < ResultDepo)
            {
                _max = ResultDepo;
                _min = ResultDepo;
            }

            if (_min > ResultDepo)
            {
                _min = ResultDepo;

                if(MaxDrawDown < _max - _min)
                {
                    MaxDrawDown = _max - _min;
                }
            }
        }

        /// <summary>
        /// Расчет относительной просадки
        /// </summary>
        /// <returns></returns>
        private void CalcPercentDrawDown() 
        {
            decimal percent = MaxDrawDown * 100 / ResultDepo;

            if (percent > PercentDrawDown) PercentDrawDown = Math.Round(percent, 2);
        }
               
        #endregion
    }
}