using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MyConsole;
using static MyConsole.Trade;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    

    public class Position
    {
        //================================================================ Fields
        #region Fields
        /// <summary>
        /// Тикер инструмента
        /// </summary>
        public string NameTicker = "SBER";

        /// <summary>
        /// Цена открытия сделки
        /// </summary>
        public decimal PriceOpenLot = 0;

        /// <summary>
        /// Количество лотов в сделке (сколько куплено, сколько продано)
        /// </summary>
        public decimal LotOfTransaction = 0;

        /// <summary>
        /// Количество лотов висит в сделке на текущий момент
        /// </summary>
        public decimal LotOfDeal = 0;

        /// <summary>
        /// Средняя цена входа в сделку
        /// </summary>
        public decimal SumPriceOfTransaction = 0;

        public decimal SumAverageSize = 0;

        #endregion

        //====================================================================Methods
        #region Methods
        /// <summary>
        /// Событие об изменении позиции 
        /// </summary>
        public void ChangeThePosition()
        {

        }
        public Position()
        {
            Timer timer = new Timer();

            timer.Interval = 1000;

            timer.Elapsed += Timer_Elapsed;

            timer.Start();
        }

        Random random = new Random();

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {

            Trade trade = new Trade(); //Создадим экземпляр класса Trade

            int num = random.Next(-10, 10);

            trade.Volume = Math.Abs(num); // число num берем по модулю, чтобы оно всегда было положительным

            trade.Price = random.Next(70000, 80000);

            if (num > 0)
            {
                //совершаем сделку в лонг
                Console.WriteLine();

                Console.WriteLine(num + " Сделка ↑ : " + NameTicker + " Объем: " + trade.Volume.ToString() + " Точка входа: " + trade.Price.ToString());

                LotOfTransaction += trade.Volume; //Общее количество лотов после совершенной сделки

            }
            else if (num < 0)
            {
                //совершаем сделку в шорт
                Console.WriteLine();
                Console.WriteLine("Значение Rundom: " + num + " Сделка ↓ : " + NameTicker + " Объем: " + trade.Volume.ToString() + " Точка входа: " + trade.Price.ToString());

                LotOfTransaction -= trade.Volume;
            }

            //Опишем общее состояние позиции после совершенной сделки
            //SumAverageSize += (LotOfTransaction * trade.Price) / LotOfTransaction;

            //SumPriceOfTransaction = ;
            Console.WriteLine(" Общее количество лотов в сделке: " + LotOfTransaction.ToString());// +" средняя сумма "+SumAverageSize);
            #endregion
        }

        static void WriteLine() => Console.WriteLine("Просто вывод строки!");

        //===============================================================Delegate 

        /// <summary>
        /// Изменение позиции
        /// </summary>
        delegate void Number(); //Объявили делегат
        Number? chPosition; //Создали переменную типа делегат
        
    }
}
