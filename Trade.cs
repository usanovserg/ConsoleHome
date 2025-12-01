namespace MyConsole
{
    public class Trade
    {
        #region Fields
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

        #region Properties
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

    }
}