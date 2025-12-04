using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static ConsoleHome.Enum;
using Timer = System.Timers.Timer;

namespace ConsoleHome
{   
    public class Position
    {
        string simb = Console.ReadLine();    
        public Position()
        {
            Timer timer = new Timer();     // Создание нового таймера и присвоение ему переменной timer

            timer.Interval = 1000;                         // Определяем время через которое данные обновляются

            timer.Elapsed += NewTrade;               // Подписка на событие. Как только таймер тикает по заданному времени происходит новый Трейд          

            timer.Start();                                // Старт таймера
        }
        Random random = new Random();



        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();                    // Новая сделка

            int num = random.Next(-10, 10);               // Генерируем случайные числа со знаком (+ -) и помещаем в переменную

            if (num > 0)
            {
                trade.Side = OrderSide.Long;  // Сделка в лонг если > 0
            }

            else if (num < 0)
            {
                trade.Side = OrderSide.Short; // Сделка в шорт если < 0
            }


            trade.Volume = Math.Abs(num);   // Количество лотов

            trade.Price = random.Next(70000, 80000);  // Цена сделки случайная

            decimal sum = trade.Volume * trade.Price;  // Переменной присваиваем результат произведение лотов и цены

            string str = "Цена " + simb + " изменилась, на " + trade.Price.ToString() + "\n"

            + "Сделка в " + trade.Side.ToString() + "\n"

            + "Лотов = " + trade.Volume.ToString() + "\n"

            + "На сумму " + sum;  // В строковую переменную присваиваем значение вычисления, "\n" - перенос строки

            Console.WriteLine(str);    // Выводим строку в консоли

            Console.WriteLine("");     // Выводим пустую строку для разделения 


            

        }
    }
}
