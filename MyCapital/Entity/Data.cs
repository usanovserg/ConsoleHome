using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCapital.Enums;

namespace MyCapital.Entity
{
    internal class Data
    {
        public Data(decimal DepoStart, StrategyType strategyType)
        {
            StrategyType = strategyType;

            Depo = DepoStart;
        }

        public StrategyType StrategyType { get; set; }

        public decimal Depo
        {
            get => _depo;
            set => _depo = value;
        }

        private decimal _depo;

        public decimal ResultDepo
        {
            get => _resultDepo;
            set => _resultDepo = value;
        }

        private decimal _resultDepo { get; set; }

        public decimal Profit;

        /// <summary>
        /// Относительный профит в процентах
        /// </summary>
        public decimal PercentProfit { get; set; }

        /// <summary>
        /// Максимальная абсолютная просадка в деньгах
        /// </summary>
        public decimal MaxDrawDown
        {
            get => _maxDrawDown;
            set => _maxDrawDown = value;
        }

        private decimal _maxDrawDown;
        /// <summary>
        /// Максимальная относительная просадка в процентах
        /// </summary>
        public decimal PercentDrawDown { get; set; }

    }
}
