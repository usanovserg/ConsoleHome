using System;
using System.Collections.Generic;

namespace Video_1._3
{
    internal class Program
    {
        static List<decimal> levels;
        static decimal steplevel;
        static decimal pricelow;
        static decimal priceup;
        static decimal countlevels;
        static decimal pricelevel;
        static void Main(string[] args)
        {
            levels = new List<decimal>();

            Writеline();
          
            string str = Readline("Задайте верхнюю цену");
            priceup = decimal.Parse(str);
            
            str = Readline("Задайте нижнюю цену");
            pricelow = Convert.ToDecimal(str);
           
            if (pricelow>=priceup)
            {
                Console.WriteLine("Нижняя цена должна быть меньше верхней. Введите значение заново:");
                str = Console.ReadLine();
                pricelow = Convert.ToDecimal(str);
            }

            str = Readline("Введите шаг цены");
            Steplevel = decimal.Parse(str);

            //Считаем количество уровней и выводим их
            pricelevel = priceup;
            countlevels = ((priceup - pricelow) / steplevel) + 1;
            Console.WriteLine($"Число уровней - {countlevels}");
            for (int i = 0; i < countlevels; i++)
            {
                levels.Add(pricelevel);
                pricelevel -= steplevel;
            }
            Writеline();
        }
        static decimal Steplevel
        // Все последующее коротко: s..d.. Steplevel (get; set;)
        //        
        // По сути, поле get - свойство работает в режиме "чтения", а поле set - "запись"
        {
            get
            {
                return steplevel;
            }
            set
            // set используется, когда заносим значение в формате Steplevel=что-то там. Что-то там пишется в value
            {
                steplevel = value;
            }
        }
        static string Readline(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
        static void Writеline()
        {
            Console.WriteLine("Количество элементов в списке: " + countlevels.ToString());
            for (int i = 0; i < countlevels; i++)
            {
                Console.WriteLine(levels[i]);
            }
        }
    }

}
