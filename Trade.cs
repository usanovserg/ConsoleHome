namespace ConsoleHome
{
    public class Trade
    {
        //============================================================= Fields ========================================
        #region Fields

        /// <summary>
        /// цена инструмента        
        /// </summary>
        public decimal Price = 0;

        public string SecCode = "";

        public string ClassCode = "";

        public string Portfolio = "";

        public DateTime DateTime = DateTime.MinValue;

        #endregion
        //============================================================= Properties ========================================
        #region Properties

        /// <summary>
        /// Объум сделки
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
        decimal _volume = 0;// приватные поля с нижнего подчеркивания и с маленькой буквы

        #endregion


    }
}