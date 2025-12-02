using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole

{
        public class Trade
        {
        //============================================ Enum (Перечисление)
        #region Enum
        /// <summary>
        /// Перечисление направлений сделки
        /// </summary>
        public enum DirectionOfTransaction
        {
            Long,
            Short,
        }
        #endregion

        //===========================================Fields (поля)
        #region Fields
        public decimal Price = 0;

        public string SecCode = "";

        public string ClassCode = "";

        public DateTime DateTime = DateTime.MinValue;

        string Portfolio = "";

        #endregion

        //=============================================Properties (свойства)
        #region Properties 
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
        //Внутренние поля рекомендуется начинать с нижнего подчеркивания и с маленькой буквы!
        //Это приватные поля
        //Публичные поля рекомендуется писать с большой буквы
        decimal _volume = 0;

        #endregion


    }
}
