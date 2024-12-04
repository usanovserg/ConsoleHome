using ConsoleHome.Model;
using ConsoleHome.Service;
using System;
using System.Collections.Generic;

namespace ConsoleHome.Strategy
{
    public class GridStrategy : IStrategy
    {
        #region Fields

        public decimal priceUp;
        public decimal priceDown;
        public decimal priceStep;

        private List<Level> levels = null;
        Random rnd = null;

        #endregion

        public GridStrategy()
        {
            rnd = new Random();
        }

        //================================================ IStrategy ================================================//

        #region IStrategy

        public void Init()
        {
            RequestParams();
            CalcLevels();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Стратегия содержит {levels.Count} уровней.");
            foreach (Level item in levels)
            {
                Console.WriteLine(item.price);
            }
        }

        public Order Trade(decimal oldPrice, decimal newPrice)
        {
            int next = rnd.Next(-1, 2);
            if (next < 0)
            {
                Order trade = new Order();
                trade.price = newPrice;
                trade.secCode = "SBERP";
                trade.volume = Level.DEFAULT_LOT;
                return trade;
            } else if (next > 0) {
                Order trade = new Order();
                trade.price = newPrice;
                trade.secCode = "SBERP";
                trade.volume = -Level.DEFAULT_LOT;
                return trade;
            }
            return null;
        }

        #endregion

        //================================================ Private Methods ================================================//

        #region Private Methods

        private void RequestParams()
        {
            priceUp = ReadDecimal("Задайте верхнюю цену: ");
            priceDown = ReadDecimal("Задайте нижнюю цену: ");
            priceStep = ReadDecimal("Задайте шаг цены: ");
        }

        private decimal ReadDecimal(string message)
        {
            decimal value = 0;
            Console.Write(message);
            try
            {
                value = decimal.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка ввода числа: " + e.Message);
                return ReadDecimal(message);
            }
            return value;
        }

        private void CalcLevels()
        {
            levels = new List<Level>();

            decimal price = priceUp;
            while (price > priceDown)
            {
                levels.Add(new Level(price));
                price -= priceStep;
            }
            levels.Add(new Level(priceDown));

        }

        #endregion
    }
}
