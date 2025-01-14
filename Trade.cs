using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleHome.Trade;

namespace ConsoleHome
{
    public class Trade
    {

        /*enum instr
        {
            SBER,
            VTBR,
            RTS,
            Si,
            LKOH,
            GAZP
        }
        */

        public enum Order
        {
            Long,
            Short
        }

        //public int instrCount = Enum.GetNames(typeof(instr)).Length;

        #region Fields

        /// <summary>
        /// Цена инструмента
        /// </summary>
        public decimal Price = 0;

        public string SecCode = "";

        public string ClassCode = "";

        /// <summary>
        /// Название инструмента
        /// </summary>
        public string InstrName = "";

        /// <summary>
        /// Время сделки
        /// </summary>
        public DateTime DateTimes = DateTime.MinValue;

        /// <summary>
        /// торовый счет
        /// </summary>
        public string order_account = "";

        public string Portfolio = "";

        /// <summary>
        /// Тип сделки (Лонг, Шорт)
        /// </summary>
        public Order type_order;

        #endregion

        #region Propertice

        /// <summary>
        /// Объем сделки
        /// </summary>
        public decimal Volume
        {
            get
            {
                return _volume;
            }

            set
            {
                _volume = value;
            }

        }
        decimal _volume = 0;

        #endregion

    }

}
