using ConsoleHome.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome
{
    /// <summary>
    /// Класс управления отдельной сделкой (Шорт или Лонг);
    /// </summary>
    public class Trade
    {
        public Trade()
        {
            
        }
        //----------------------------------------------- Fields (поля) Begin ------------------------------
        #region Fields
        /// <summary>
        /// Цена совершения новой сделки;
        /// </summary>
        public decimal Price = 0;

        public string SecCode = "";

        public string ClassCode = "";

        public DateTime dateTime = DateTime.MinValue;

        public string Portfolio = "";

        public SideTransaction Side { get; set; }

        public SidePosition Position { get; set; }

        #endregion
        //----------------------------------------------- Fields (поля) End ------------------------------

        //----------------------------------------------- Properties Begin ------------------------------
        #region Properties

        /// <summary> Объем сделки. </summary>
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
        //----------------------------------------------- Properties End ------------------------------

    }

}
