

using ConsoleHome.Enums;
using System;
using System.Globalization;
using System.Net.Http.Headers;

//Дз5-6
/*
 * Задача: создать торговый эмулятор
 * 1) Класс Exchange должен по таймеру генерировать сделки
 * 2) Класс Levels-Orders должен расставлять уровни и ордера на нём
 * 3) Класс Connector должен имитировать подключение к Exchange
 * 4) Класс Account описывает состояние счёта и его номер
 * 
 * 
 */

namespace ConsoleHome
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Создание счёта
            Console.Write("Создание счёта, введите нач.сумму (>0): ");
            uint cashAccount = uint.Parse(Console.ReadLine());                          //потенциально опасное место, нужно исключения
            cAccount account = new cAccount(cashAccount);
            Console.WriteLine("Счёт создан : #" + account.RequestID().ToString());
            Console.WriteLine("Баланс счёта: " + account.RequestBalance().ToString());

            //Пополнить счёт?
            Console.WriteLine("Желаете пополнить счёт (введите сумму >0) или N - для отмены.");
            string answer = Console.ReadLine().Trim();
            //проверка ввода
            if (!answer.Equals("N", StringComparison.OrdinalIgnoreCase))
            {
                int sum = int.Parse(answer);
                account.Add(sum);
            }

            //Получить доступные коннекты по инструментам
            Console.WriteLine("Создать коннект на бирже. Введите имя инструмента, для получения последней реф.даты (допустимо 0 - sber, 1 - gazp, 2 - gmkn)");
            cConnector connector = new cConnector();
            connector.ConnectionNotifyHandler += DisplayMessage;
            bool isOk = false;
            while(!isOk)
            { 
                string input = Console.ReadLine().Trim();
                if (Enum.TryParse(input, true, out Assets asset) && uint.Parse(input) < (uint)Enum.GetValues(typeof(Assets)).Length)
                {
                    connector.Connect(asset);
                    isOk = true;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Повторите попытку");
                }
            }

            //Создать уровни
            Console.WriteLine("Задайте уровни для сеточника в Long");
            Console.Write("Задайте верхнюю цену: ");
            decimal upPrice = decimal.Parse(Console.ReadLine().Trim());
            Console.Write("Задайте нижнюю цену: ");
            decimal downPrice = decimal.Parse(Console.ReadLine().Trim());
            Console.Write("Задайте шаг цены: ");
            decimal stepPrice = decimal.Parse(Console.ReadLine().Trim());
            List<cLevel> levels = cLevel.CalculateLevels(upPrice, downPrice,stepPrice);
            Console.WriteLine($"Создано {levels.Count()} уровней в диапазоне {downPrice} до {upPrice} с шагом {stepPrice}:");
            for (int i = 0; i < levels.Count(); i++)
            {
                Console.WriteLine($"лот {levels[i].lotLevel.ToString()} @ {levels[i].priceLevel.ToString()}");
            }


            //Начать трансляцию сделок
            Console.WriteLine("Начать трансляцию сделок для выбранного инструмента: Y/N");
            isOk = false;
            while (!isOk)
            {
                string input = Console.ReadLine().Trim();
                if (input.Equals("N", StringComparison.OrdinalIgnoreCase) || input.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    switch (input)
                    {
                        case "Y":
                        case "y":
                            connector.Start();
                            connector.NewTradeNotification += GetMyNewTrade;
                            isOk = true;
                            break;
                        case "N":
                        case "n":
                            Console.WriteLine("Введено значение N - выход из программы!");
                            return;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Повторите попытку");
                }
            }


            



            /*
            
            
            cPosition position = new cPosition();
                        */
            Console.ReadLine();

        }
        
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void GetMyNewTrade(cTrade trade)
        {
            Console.WriteLine($"Cделка по {trade.Asset.ToString()} лот {trade.Lot.ToString()} @ {trade.price.ToString()}");
            cTrade myTrade = trade;

        }



    }
}

    

    
    





