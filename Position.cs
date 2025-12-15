using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static MyConsole.Trade;

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
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// Цена открытия позиции
        /// </summary>
        public decimal PriceOpen { get; set; } = 0;

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
        /// Общий объем позиции
        /// </summary>
        public decimal SumOfVolume { get; set; } = 0;

        public decimal PnL { get; set; } = 0;        

        /// <summary>
        /// Наименование инструмента
        /// </summary>
        public string SecCode { get; set; } = "";

        /// <summary>
        /// Классификация
        /// </summary>
        public string ClassCode { get; set; } = "";

        /// <summary>
        /// Время
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Портфель (номер счета)
        /// </summary>
        public string Portfolio { get; set; } = "";

        /// <summary>
        /// Направление торговли
        /// </summary>
        public string DirectionOfTrade { get; set; } = "";

        /// <summary>
        /// Предыдущее направление торговли
        /// </summary>
        public string OldDirectionOfTrade { get; set; } = "";
        
        /// <summary>
        ///Средняя цена 
        /// </summary>
        public decimal AveragePrice { get; set; } = 0;

        /// <summary>
        /// Предыдущая средняя цена
        /// </summary>
        public decimal OldAveragePrice { get; set; } = 0;

        /// <summary>
        /// Предыдущая цена
        /// </summary>
        public decimal OldPrice { get; set; } = 1;

        public bool IsFirstPrice { get; set; } = true;

        public decimal AllVolume { get; set; } = 0;             
             

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
          
            Volume = Math.Abs(num); //Получаем объем.
                       
            DirectionOfTrade = GetDirection(num); //Генерируем направление сделки

            SecCode = "BTCMMM"; //Задаем инструмент                      
                     

            // Тут определяем первый проход
            if (IsFirstPrice)
            {               
                OldAveragePrice = Price; // Сохраняем первый уровень
                OldDirectionOfTrade = DirectionOfTrade;
                SumOfVolume = Volume;             

                IsFirstPrice = false;
            }
           
            // Последующие проходы                        
            else
            {
                PnL = 0;

                //Если направление сделки НЕ поменялось
                if (OldDirectionOfTrade == DirectionOfTrade)
                {
                    AveragePrice = (OldAveragePrice * SumOfVolume + Price * Volume) / (Volume + SumOfVolume);

                    OldAveragePrice = AveragePrice;
                    SumOfVolume += Volume;
                }

                //Если направление сделки поменялось
                else
                {
                    if (SumOfVolume > Volume) //Накопленного объема хватает для закрытия сделки
                    {
                        if (OldDirectionOfTrade == directionOfTrade.Long.ToString())
                        {
                            PnL =  (Price - OldAveragePrice) * Volume;                           
                        }
                        else
                        {
                            PnL = - (Price - OldAveragePrice) * Volume;
                        }

                        SumOfVolume -= Volume;                                          

                      //  OldAveragePrice = AveragePrice;
                        //Тут поставим метку что новая сделка закрыта
                    }
                    else //Накопленного объема не хватает на закрытие сделки
                    {
                        if (OldDirectionOfTrade == directionOfTrade.Long.ToString())
                        {
                            PnL = (Price - OldAveragePrice) * SumOfVolume;

                            OldDirectionOfTrade = directionOfTrade.Short.ToString();

                            DirectionOfTrade = directionOfTrade.Short.ToString();
                        }
                        else
                        {
                            PnL = - (Price - OldAveragePrice) * SumOfVolume;

                            OldDirectionOfTrade = directionOfTrade.Long.ToString();

                            DirectionOfTrade = directionOfTrade.Long.ToString();
                        }

                        if (SumOfVolume < Volume) 
                        {
                            SumOfVolume = Volume - SumOfVolume;
                            AveragePrice = Price;
                            OldAveragePrice = AveragePrice;

                            //Тут поставим метку что старая сделка закрыта
                        }

                        else  // Сюда попадем если объемы противоположных сделок одинаковы
                        {
                            if (OldDirectionOfTrade == directionOfTrade.Long.ToString()) //тут знаки наоборот! Потому что направление уже поменено
                            {
                                PnL = - (Price - OldPrice) * SumOfVolume;                                                               
                            }
                            else
                            {
                                PnL = (Price - OldPrice) * SumOfVolume;                                                               
                            }

                            IsFirstPrice = true;
                            Price = 0;
                            AveragePrice = 0;
                            OldPrice = 0;
                            Volume = 0;
                            SumOfVolume = 0;
                            DirectionOfTrade = "";
                            OldDirectionOfTrade = "";

                            //Тут поставим метку что обе сделки закрыты
                        }
                    }

                }                       

            }
              NewTradeEvent(Price);  // Вызов события

            PrintPosition();
        }
       
        public string GetDirection(int num)
        {
            if (num > 0)
            {               
                return directionOfTrade.Long.ToString();                 
            }
            else
            {              
                return directionOfTrade.Short.ToString();
            }
        }        

        public void PrintPosition()
        {

            string str =  "Время = " + DateTime.ToString() +
                          " / Инструмент " + SecCode.ToString() +
                          " / Volume = " + Volume.ToString() +
                          " / SumOfVolume = " + SumOfVolume.ToString() +
                          " / Price = " + Price.ToString() +                         
                          " / Средняя цена = " + Math.Round(OldAveragePrice, 2).ToString() +
                          " / Direction = " + DirectionOfTrade.ToString() +
                           " / PnL = " + Math.Round(PnL, 2).ToString(); 

                     
                    

            Console.WriteLine(str);
            Console.WriteLine("");

        }

        #endregion
        //----------------------------------------------- End Methods ------------------------------------------------ 

       delegate void MessageOfChange(decimal price);

        
    }   
       
}
