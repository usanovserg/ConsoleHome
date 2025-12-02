namespace MyConsole
{
    public class Trade //Создан класс Trade в котором есть несколько полей и одной свойство
    {
        #region Fields

        /// <summary>
        /// Цена инструмента
        /// </summary>
        public decimal Price = 0;

        //public decimal Volume = 0; Позже из этого поля было создано свойство

        public string SecCode = ""; //Строковое название инструмента

        string ClassCode = ""; // Код класса

        public DateTime DataTime = DateTime.MinValue;

        public string Portfolio = ""; // Нащвание портфеля

        #endregion

        #region Properties
        /// <summary>
        /// Объем сделки
        /// </summary>
        public decimal Volume //Создано свойство
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
        decimal _volume = 0; // создали поле
        #endregion
    }
}