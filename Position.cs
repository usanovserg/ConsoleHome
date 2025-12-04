using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static MyConsole.ForTests;
using static MyConsole.Trade;

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

            timer.Interval = 5000;

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
        /// Текущая цена инструмента
        /// </summary>
        public decimal Price = 0;

        /// <summary>
        /// Цена открытия позиции
        /// </summary>
        public decimal PriceOpen = 0;

        /// <summary>
        /// Объем сделки
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
        decimal _volume = 0;

        /// <summary>
        /// Цена закрытия инструмента по SL
        /// </summary>
        public decimal PercentSL = 0.03m;

        /// <summary>
        /// Цена закрытия инструмента по TP
        /// </summary>
        public decimal PercentTP = 1;

        public decimal PriceSL = 0;

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

        public decimal OldPrice = 1;

      //  public decimal AveragePrice = 0;

        public bool IsFirstPrice = true;

        public decimal HelperLevel = 0;
        
             

        #endregion Fields
        //----------------------------------------------- End Fields ------------------------------------------------


        //----------------------------------------------- Methods ---------------------------------------------------- 


        #region Methods

        public void NewTrade(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Random random = new Random();

            Trade trade = new Trade();

            DateTime = DateTime.Now;

            Price = random.Next(200, 1000); // Генерируем цену инструмента.

            DirectionOfTrade = GetDirection(); //Генерируем направление сделки

            int num =  random.Next(-30, 30);   //Генерируем объем сделки             

            Volume = Math.Abs(num); //Получаем объем.

            SecCode = "BTCMMM";

            if (DirectionOfTrade == directionOfTrade.Long.ToString())
            {
                PriceTP = Math.Round(Price * (1 + PercentTP / 100), 2);
                PriceSL = Math.Round(Price * (1 - PercentSL / 100), 2);
            }
            else 
            {
                PriceTP = Math.Round(Price * (1 - PercentTP / 100), 2);
                PriceSL = Math.Round(Price * (1 + PercentSL / 100), 2);
            }



            Commission = trade.GetCommission(ForTests.CalcCommission());            


            if (IsFirstPrice)
            {
                AveragePrice = Price; // Сохраняем первый уровень

                HelperLevel = AveragePrice;

                IsFirstPrice = false;
            }

            else
            {
                 HelperLevel = Price;

                AveragePrice =Math.Round( (AveragePrice + HelperLevel) / 2 , 2);
                
            }

          //  trade.AveragePrice = AveragePrice;

            NewTradeEvent(Price);  // Вызов события

            PrintPosition();
        }

        public string GetDirection()
        {

            if (Price > OldPrice)
            {
                OldPrice = Price;
                return Trade.directionOfTrade.Long.ToString();
            }
            else
            {
                OldPrice = Price;
                return Trade.directionOfTrade.Short.ToString();
            }

        }


        public void PrintPosition()
        {

            string str = "Время = " + DateTime.ToString() +
                          " / Инструмент " + SecCode.ToString() +
                          " / Volume = " + Volume.ToString() +
                          " / Price = " + Price.ToString() +
                          " / PriceTP = " + PriceTP.ToString() +
                          " / PriceSL = " + PriceSL.ToString() +
                          " / Средняя цена = " + AveragePrice.ToString() +
                          " / Direction = " + DirectionOfTrade.ToString() +
                          " / Commission = " + Commission.ToString();


            Console.WriteLine(str);


        }

        #endregion
        //----------------------------------------------- End Methods ------------------------------------------------ 

        delegate void MessageOfChange(decimal price);

        
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
