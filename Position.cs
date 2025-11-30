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

            timer.Interval = 5000;

            timer.Elapsed += NewTrade;

            timer.Start();                                 

        }

        public decimal AveragePrice = 0;

        public decimal FirstLevel = 0;

        public decimal HelperLevel = 0;

        public void NewTrade(object? sender, System.Timers.ElapsedEventArgs e)
        {
         
            Trade trade = new Trade();

            trade.DateTime = DateTime.Now;  

            int num =  ForTests.RandomHelper.random.Next(-30, 30);   //Генерируем объем сделки и направление сделки

            trade.DirectionOfTrade = ForTests.GetDirection();

            trade.Volume = Math.Abs(num); //Получаем объем.
          
            trade.Price = ForTests.RandomHelper.random.Next(200, 1000); // Генерируем цену инструмента.
                       
            trade.Commission = trade.GetCommission(ForTests.CalcCommission());



            if (FirstLevel == 0)
            {
                FirstLevel = trade.Price; // Сохраняем первый уровень

                HelperLevel = FirstLevel;

                AveragePrice = HelperLevel / 2 ;
            }


            else
            {
                HelperLevel = trade.Price;

                if (HelperLevel > AveragePrice)
                {
                    AveragePrice = (HelperLevel + AveragePrice) / 2;  // Если цена вверх, то средний растет
                }
                else
                {
                    AveragePrice = (AveragePrice + HelperLevel) / 2; // Если цена вниз то падает
                }

                trade.AveragePrice = AveragePrice;


                ForTests.PrintResults(trade);

            }
        }
    } 
}
