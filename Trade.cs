namespace MyConsole
{
    public class Trade
    {
        //---------------------------------------- Fields -----------------------------------------
        #region Fields
        // --------------  Fields (Поля)
        // это переменные, хранящие данные внутри класса, структуры или записи, определяющие состояние объекта

        /// <summary>
        /// Цена инструмента
        /// </summary>
        public decimal Price = 0;

        //public decimal Volume = 0;

        public string SecCode = "";

        public string ClassCode = "";

        public DateTime DateTime = DateTime.MinValue;

        public string Portfolio = "";



        #endregion

        //----------------------------------------  Properties  -----------------------------------------
        #region Properties
        //------------- Properties (Свойства)
        // это специальные члены класса, обеспечивающие управляемый доступ к данным(полям) объекта через методы getter(получение) и setter(установка)

        /// <summary>
        /// Объем сделки
        /// </summary>
        public decimal Volume //Мы создали свойство. Его имя с большой буквы для красоты кода
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
        decimal _volume = 0; // внутренние (приватные) поля с маленькой буквы и нижнего подчеркивания
        #endregion

        //----------------------------------------  Enum  -----------------------------------------
        #region Enum
        //------------- Enum (Перечисление)
        // это специальный тип данных в программировании, представляющий собой набор (!) именованных констант. (не список!)

        enum TradeDir // Направление сделки
        {
            Long,
            Short

        }




        #endregion

    }
}