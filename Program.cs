// See https://aka.ms/new-console-template for more information

using System;
using System.Globalization;
//Дз сеточник

namespace MyConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            while (true)        //пока программа работает
            {
                Console.Clear();
                //объявление нужных переменных
                decimal priceUp = AskForPrice("Введите вверхнюю цену: ");
                decimal priceDown = AskForPrice("Введите нижнюю цену: ");
                while (priceDown > priceUp)
                {
                    Console.WriteLine("Неправильный ввод. Нижняя цена должна быть строго меньше верхней!");
                    priceDown = AskForPrice("Введите нижнюю цену: ");
                }
                decimal priceStep = AskForPrice("Введите шаг уровней: "); ;

                //расчёт уровней
                int countLevels = (int)((priceUp - priceDown) / priceStep) + 1;

                List<decimal> levels = new List<decimal>();
                decimal levelPrice = priceUp;
                for (int i = 0; i < countLevels; i++)
                {
                    levels.Add(levelPrice);
                    levelPrice -= priceStep;
                }

                //вывод результата
                Console.WriteLine($"Кол-во уровней в диапазоне цен от {priceDown} до {priceUp} c шагом {priceStep} = " + levels.Count.ToString());

                for (int i = 0; i < levels.Count; i++)
                {
                    Console.WriteLine("Level " + (i + 1) + " : " + levels[i]);
                }

                Console.ReadLine();

            }

        }

        static decimal AskForPrice(string message) 
        {
            decimal value = 0;
            Console.Write(message);
            bool isOk = false;
            do
            {
                try
                {
                    value = decimal.Parse(Console.ReadLine());
                    if(value>0)
                    {
                        isOk = true;
                    }
                    else { Console.Write("Значение должно быть строго >0. " + message); }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Возникла ошибка ввода (повторите ввод значения >0 и состоящего только из цифр): ");
                }
            }
            while (!isOk);
            return value;
        }
    }
}

    

    
    





