namespace ConsoleHome.Model
{
    public class Level
    {
        public const decimal DEFAULT_LOT = 10;

        #region Feilds

        /// <summary>
        /// Цена уровня
        /// </summary>
        public decimal price = 0;
        /// <summary>
        /// Количество лотов
        /// </summary>
        public decimal lot = DEFAULT_LOT;

        #endregion

        public Level(decimal price)
        {
            this.price = price;
        }
    }
}
