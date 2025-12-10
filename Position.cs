using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
        
   public class Position    {            
       
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

        public decimal OldVolume =0;

        public decimal PnL = 0;

        decimal _volume = 0;

        /// <summary>
        /// Цена закрытия инструмента по SL
        /// </summary>
        public decimal PercentSL = 0.3m;

        /// <summary>
        /// Цена закрытия инструмента по TP
        /// </summary>
        public decimal PercentTP = 1.5m;

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

        public string OldDirectionOfTrade = "";

        /// <summary>
        /// Комиссия за сделку
        /// </summary>
        public decimal Commission = 0;

        public decimal AveragePrice = 0;
        public decimal OldAveragePrice = 0;

        public decimal OldPrice = 1;

        public bool IsFirstPrice = true;

        public decimal AllVolume = 0;
              
             

        #endregion Fields
        //----------------------------------------------- End Fields ------------------------------------------------


        //----------------------------------------------- Methods ---------------------------------------------------- 


        #region Methods

        public void NewTrade(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Random random = new Random();

            Trade trade = new Trade();

            DateTime = DateTime.Now;

            Price = random.Next(100, 300); // Генерируем цену инструмента.
                                           // 
            int num =  random.Next(-200, 200);   //Генерируем объем сделки
                        
          //  Get_TP_SL(); //Вычисляеи TP SL

            Volume = Math.Abs(num); //Получаем объем.
                       
            DirectionOfTrade = GetDirection(num); //Генерируем направление сделки

            SecCode = "BTCMMM"; //Задаем инструмент
                        

          //  Commission = trade.GetCommission(CalcCommission());   //Определяем комиссию         

            // Тут определяем первый проход
            if (IsFirstPrice)
            {               
                OldAveragePrice = Price; // Сохраняем первый уровень
                OldDirectionOfTrade = DirectionOfTrade;
                OldVolume = Volume;             

                IsFirstPrice = false;
            }
           
            // Последующие проходы                        
            else
            {
                PnL = 0;

                //Если направление сделки НЕ поменялось
                if (OldDirectionOfTrade == DirectionOfTrade)
                {
                    AveragePrice = (OldAveragePrice * OldVolume + Price * Volume) / (Volume + OldVolume);

                    OldAveragePrice = AveragePrice;
                    OldVolume += Volume;
                }

                //Если направление сделки поменялось
                else
                {
                    if (OldVolume > Volume) //Накопленного объема хватает для закрытия сделки
                    {
                        if (OldDirectionOfTrade == directionOfTrade.Long.ToString())
                        {
                            PnL =  - (Price - OldAveragePrice) * Volume;                           
                        }
                        else
                        {
                            PnL = (Price - OldAveragePrice) * Volume;
                        }

                        OldVolume -= Volume;                                          

                        OldAveragePrice = AveragePrice;
                        //Тут поставим метку что новая сделка закрыта
                    }
                    else //Накопленного объема не хватает на закрытие сделки
                    {
                        if (OldDirectionOfTrade == directionOfTrade.Long.ToString())
                        {
                            PnL = (Price - OldAveragePrice) * OldVolume;

                            OldDirectionOfTrade = directionOfTrade.Short.ToString();

                            DirectionOfTrade = directionOfTrade.Short.ToString();
                        }
                        else
                        {
                            PnL = - (Price - OldAveragePrice) * OldVolume;

                            OldDirectionOfTrade = directionOfTrade.Long.ToString();

                            DirectionOfTrade = directionOfTrade.Long.ToString();
                        }

                        if (OldVolume < Volume) 
                        {
                            OldVolume = Volume - OldVolume;
                            AveragePrice = Price;
                            OldAveragePrice = AveragePrice;

                            //  AveragePrice = (OldAveragePrice * OldVolume + Price * Volume) / (Volume + OldVolume);

                            // OldAveragePrice = AveragePrice;

                            //Тут поставим метку что старая сделка закрыта
                        }

                        else  // Сюда попадем если объемы противоположных сделок одинаковы
                        {
                            if (OldDirectionOfTrade == directionOfTrade.Long.ToString()) //тут знаки наоборот! Потому что направление уже поменено
                            {
                                PnL = - (Price - OldPrice) * OldVolume;                                                               
                            }
                            else
                            {
                                PnL = (Price - OldPrice) * OldVolume;                                                               
                            }

                            IsFirstPrice = true;
                            Price = 0;
                            AveragePrice = 0;
                            OldPrice = 0;
                            Volume = 0;
                            OldVolume = 0;
                            DirectionOfTrade = "";
                            OldDirectionOfTrade = "";

                            //Тут поставим метку что обе сделки закрыты
                        }
                    }

                }                       

            }
            //  NewTradeEvent(Price);  // Вызов события

            PrintPosition();
        }

        public void Get_TP_SL()
        {
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

        }
        public string GetDirection(int num)
        {
            if (num > 0)
            {
               // OldPrice = Price;
                return directionOfTrade.Long.ToString();
                 
            }
            else
            {
               // OldPrice = Price;
                return directionOfTrade.Short.ToString();
            }
        }
        public string CalcCommission()
        {  Random random = new Random();
          
            if (random.Next(-100, 100) >= 0)
            {
                return Trade.typeOfComission.Limit.ToString();
            }
            else
            {
                return Trade.typeOfComission.Market.ToString();
            }
          
        }

        public void PrintPosition()
        {

            string str = //"Время = " + DateTime.ToString() +
                          //" / Инструмент " + SecCode.ToString() +
                          " / Volume = " + Volume.ToString() +
                          " / OldVolume = " + OldVolume.ToString() +
                          " / Price = " + Price.ToString() +
                          //     " / PriceTP = " + PriceTP.ToString() +
                          //     " / PriceSL = " + PriceSL.ToString() +
                          " / Средняя цена = " + Math.Round(OldAveragePrice, 2).ToString() +
                          " / Direction = " + DirectionOfTrade.ToString() +
                           " / PnL = " + Math.Round(PnL, 2).ToString(); 

                     //     " / Commission = " + Commission.ToString();
                    

            Console.WriteLine(str);

        }

        #endregion
        //----------------------------------------------- End Methods ------------------------------------------------ 

       // delegate void MessageOfChange(decimal price);

        
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
