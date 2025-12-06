using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static MyConsole.Trade;
using Timer = System.Timers.Timer;

namespace MyConsole // Задали общее пространство имен
{
    public class Position // Создали новый класс
    {
        public Position() // Заполнили конструктор класса - добавили автомвтический запуск таймера
        {
            Timer timer = new Timer(); // создали новый таймер

            timer.Interval = 3000; // задали интревал срабатывания таймера

            timer.Elapsed += Timer_Elapsed; // Зациклили таймер

            timer.Start(); // Запустили таймер
        }

        Random random = new Random(); // вызвали системный метод - генратор случайных чисел
        private void Timer_Elapsed(object? sender, ElapsedEventArgs e) // метод 
        {
            Trade trade = new Trade(); // создали экземпляр класса трейд
            DealDirection dealDirection = DealDirection.None; // создали переменную в связке с enum

            int num = random.Next(-10, 10); // генератор случайных чисел от -10 до 10 - объем и направление сделки

            if (num > 0) //  1.направление сделки: если попалось случайное число меньше нуля - продаем, иначе покупаем.
            {
                // Сделка в лонг
                dealDirection = DealDirection.Long; //переменная enum
            }
            else if (num < 0) 
            {
                // Сделка в шорт
                dealDirection = DealDirection.Short; // переменная enum
            }

            trade.Volume = Math.Abs(num); // 2.объем сделки - это модуль от генератора случайных чисел

            trade.price = random.Next(7000, 10000); // генератор случайных чисел для цены в стакане

            string str = "Volume = " + trade.Volume.ToString() + " / Price = " + trade.price.ToString() + " / dealDirection = " + dealDirection.ToString();
            // таймер каждую секунду запускает метод Timer_Elapsed, в которм генерируется случайная сделка в шорт или в лонг со случайным объемом
            // от 0 до 10 лот по случайным ценам в диапазоне от 7000 до 10000. В результате видим строку: "кол-во лотов, цена открытия, направление.."
            Console.WriteLine(str);
            // Что далее по ДЗ:
            // 1. Нужно дописать метод в классе, который рассчитывает среднюю позицию и цену по исполненным сделкам.
            //    Далее этот метод должен принимать новую сделку и пересчитывать среднюю позицию и цену с учетом этой новой сделки.
            // 2. Продумать внимательно, что еще необходимо учитывать для позиции по инструменту.
        }
    }
}
