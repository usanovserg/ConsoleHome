using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MyConsole;
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
        #endregion

        //====================================================================Methods
        #region Methods
        public Position()
        {
            Timer timer = new Timer();

            timer.Interval = 5000;

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

            if (num > 0 )
            {
                //совершаем сделку в лонг
                Console.WriteLine();

                Console.WriteLine(" Сделка ↑ : "+NameTicker+" Объем: "+ trade.Volume.ToString()+" Точка входа: "+trade.Price.ToString());

                //Опишем общее состояние позиции после совершенной сделки

            }
            else if (num < 0)
            {
                //совершаем сделку в шорт
                Console.WriteLine();
                Console.WriteLine(" Сделка ↓ : " + NameTicker + " Объем: " + trade.Volume.ToString() + " Точка входа: " + trade.Price.ToString());
            }
            #endregion
        }
    }
}
