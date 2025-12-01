using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
  class ForTests
    {
        public static decimal CountAverage = 0;

        public static void PrintResults(Trade trade)
        {

            string str = "Время = " + trade.DateTime.ToString() +
                          " Volume = " + trade.Volume.ToString() +
                          " / Price = " + trade.Price.ToString() +
                          " Средняя цена = " + trade.AveragePrice.ToString() +
                          " / Direction = " + trade.DirectionOfTrade.ToString() +
                          " / Commission = " + trade.Commission.ToString();                     


            Console.WriteLine(str);

        }
        /// <summary>
        /// Выдает направление сделки случайным образом
        /// </summary>
        /// <returns></returns>
        public static string GetDirection()
        {
          //  Random random = new Random();

            int num = RandomHelper.random.Next(-100, 100);

            if (num > 0)
            {
                return Trade.directionOfTrade.Long.ToString();
            }
            else            
            {
                return Trade.directionOfTrade.Short.ToString();
            }
        }
    
        /// <summary>
        /// Определяет тип комиссии. Основывается на случайном значении направления сделки.  
        /// </summary>
        /// <returns></returns>
        public static string CalcCommission()
        {
            if ( GetDirection() == Trade.directionOfTrade.Short.ToString())
            {
                return Trade.typeOfComission.Limit.ToString();
            }
            else
            {
                return Trade.typeOfComission.Market.ToString();
            }
                           
        } 

        public static class RandomHelper
        {
            public static readonly Random random = new Random();
        }

      

    }
}
