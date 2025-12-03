using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using static MyConsole.Connector;
//using static MyConsole.Position;
using Timer = System.Timers.Timer;


namespace MyConsole
{
        
   public class Position
    {            
       
        public Position() 
        {               
            NewTradeEvent += Program.MessageOfChange; //Подписываемся на событие

            Timer timer = new Timer();                //Настраиваем таймер

            timer.Interval = 1000;

            timer.Elapsed += NewTrade;
           
            timer.Start();                                 

        }


        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Fields
        /// <summary>
        /// Делегат для создания события
        /// </summary>
        /// <returns></returns>
        public delegate void newTradeEvent(decimal price);

        /// <summary>
        /// Создаем событие
        /// </summary>
        public static event newTradeEvent NewTradeEvent;


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

            NewTradeEvent(trade.Price);

            ForTests.PrintResults(trade);
    }


    #endregion
    //----------------------------------------------- End Methods ------------------------------------------------ 

    delegate void MessageOfChange(decimal price);
      //  MessageOfChange ChangedPrice;
        
    }

    //public class Connector
    //{
    //    //  public newTradeEvent NewTradeEvent;

    //    public delegate void newTradeEvent();
     
    //    public static event newTradeEvent NewTradeEvent;

    //    public List<Trade> Trades = new List<Trade>();

    //    public void NewTrade (Trade trade)
    //    {
    //        Trades.Add(trade);

    //        NewTradeEvent();
    //    }
          
    //    public void Connect()
    //    {
    //        Console.WriteLine("Connect is Exchange");
    //    }

    //    //public void AddDelegate(newTradeEvent method)
    //    //{
    //    //    NewTradeEvent = method;
    //    //}


    //}
}
