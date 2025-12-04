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
        //=================================== Fields ===============================================
        #region Fields
        /// <summary>
        /// Цена инструмента
        /// </summary>
        public decimal Price = 0;
        public string SecCode = "";
        public string ClassCode = "";
        public DateTime DateTime = DateTime.MinValue;
        public string Portfolio = "";
        public decimal Volume;
        /// <summary>
        /// Структура входных данных для генерации случайного тейда. Максимальные и минимальный цены и лоты, количество знаков после запятой.
        /// </summary>
        public struct RandomData
        {
            public decimal MaxPrice;
            public decimal MinPrice;
            public byte PrecisionPrice;
            public decimal MaxVolume;
            public decimal MinVolume;
            public byte PrecisionVolume;
        }
        public enum DTrade : sbyte
        {
            Buy = 1,
            Sell = -1
        }
        public DTrade DirectionTrade = DTrade.Buy;
        #endregion
        //=================================== Properties ===============================================
        #region Properties
        #endregion
        //=================================== Methods ===============================================
        #region methods
        public void RandomTrade(RandomData data)
        {
            Random random = new Random();
            Price = Math.Round(data.MinPrice + (decimal)random.NextDouble() * (data.MaxPrice - data.MinPrice), data.PrecisionPrice);
            Volume = Math.Round(data.MinVolume + (decimal)random.NextDouble() * (data.MaxVolume - data.MinVolume), data.PrecisionVolume);
            DirectionTrade = (DTrade)(random.Next(0, 2)*2-1);
        }
        #endregion
    }
}
