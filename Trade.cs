using ConsoleHome.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    public class Trade
    {
        //======================================Fields=======================/
        #region Fields
        /// <summary>
        /// Цена сделки
        /// </summary>
        public decimal Price = 0;

        /// <summary>
        /// Кол-во лотов в сделке
        /// </summary>
        public decimal Volium = 0;

        /// <summary>
        /// Всего лотов в портфеле
        /// </summary>
        public decimal TotalVolium = 0;

        /// <summary>
        /// Направление сделки
        /// </summary>
        public Direction TradeDirection = Direction.None;

        /// <summary>
        /// Направление позиции
        /// </summary>
        public TotalPosition TradeTotalPosition = TotalPosition.No_Pozition;

        /// <summary>
        ///  Время сделки
        /// </summary>
        public DateTime dateTime = DateTime.MinValue;

        #endregion
    }
}