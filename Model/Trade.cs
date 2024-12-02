using System;

namespace ConsoleHome.Model
{
    public class Trade
    {
        #region Fields

        public decimal price = 0;
        public decimal volume = 0;
        public string secCode = null;
        public string classCode = null;
        public string portfolio = null;
        public DateTime createTime = DateTime.MinValue;

        #endregion

    }
}
