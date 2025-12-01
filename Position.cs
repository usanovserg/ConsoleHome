using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;


namespace MyConsole
{
   public class Position
    {
        public Position() 
        {
            Timer timer = new Timer();

            timer.Interval = 1000;

            timer.Elapsed += NewTrade;

            timer.Start();                                 

        }

        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Fields

        /// <summary>
        /// Цена инструмента
        /// </summary>
        public decimal Price = 0;

        /// <summary>
        /// Цена открытия позиции
        /// </summary>
        public decimal PriceOpen = 0;

        /// <summary>
        /// Цена закрытия инструмента по SL
        /// </summary>
        public decimal PriceSL = 0;

        /// <summary>
        /// Цена закрытия инструмента по TP
        /// </summary>
        public decimal PriceTP = 0;


        /// <summary>
        /// Шаг трейлинг стопа
        /// </summary>
        public decimal StepTS = 0;

        /// <summary>
        /// Наименование инструмента
        /// </summary>
        public string SecCode = "";

        /// <summary>
        /// Классификация
        /// </summary>
        public string ClassCode = "";

        /// <summary>
        /// Время
        /// </summary>
        public DateTime DateTime = DateTime.MinValue;

        /// <summary>
        /// Портфель (номер счета)
        /// </summary>
        public string Portfolio = "";

        /// <summary>
        /// Направление торговли
        /// </summary>
        public string DirectionOfTrade = "";
        //  public directionOfTrade DirectionOfTrade;

        /// <summary>
        /// Комиссия за сделку
        /// </summary>
        public decimal Commission = 0;








        public decimal AveragePrice = 0;


      //  public decimal AveragePrice = 0;

        public bool IsFirstPrice = true;

        public decimal HelperLevel = 0;

        
        
        #endregion Fields
        //----------------------------------------------- End Fields ------------------------------------------------


        //----------------------------------------------- Methods ---------------------------------------------------- 

        #region Methods
        public void NewTrade(object? sender, System.Timers.ElapsedEventArgs e)
        {
         
            Trade trade = new Trade();

            trade.DateTime = DateTime.Now;  

            int num =  ForTests.RandomHelper.random.Next(-30, 30);   //Генерируем объем сделки и направление сделки

            trade.DirectionOfTrade = ForTests.GetDirection();

            trade.Volume = Math.Abs(num); //Получаем объем.
          
            trade.Price = ForTests.RandomHelper.random.Next(200, 1000); // Генерируем цену инструмента.
                       
            trade.Commission = trade.GetCommission(ForTests.CalcCommission());            


            if (IsFirstPrice)
            {
                AveragePrice = trade.Price; // Сохраняем первый уровень

                HelperLevel = AveragePrice;

                IsFirstPrice = false;
            }

            else
            {
                 HelperLevel = trade.Price;

                AveragePrice = (AveragePrice + HelperLevel) / 2;
                
            }

            trade.AveragePrice = AveragePrice;
            ForTests.PrintResults(trade);
        }

        #endregion
        //----------------------------------------------- End Methods ------------------------------------------------ 
    }
}
