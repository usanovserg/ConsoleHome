// See https://aka.ms/new-console-template for more information
using System;
using System.Net.NetworkInformation;
using System.Security.Cryptography;


namespace ConsoleHome
{
    public class Program
    {
         static string ReadLine( string Comment)
        {
         Console.WriteLine(Comment);         
         return Console.ReadLine();
        }
        static void WriteLine()
        {
            Console.WriteLine("Кол-во элементов в списке: " + levels.Count.ToString());

            for(int i=0; i < levels.Count; i++ )
            {
                Console.WriteLine(levels[i].PriceLevel);
            }
        }

        static decimal stopLevel;
        static decimal priceUp;
        static List<Level> levels;
        static int contLevels;
        static decimal lotLevel;
        static void Main(string[] args)
        {
            levels = new List<Level>();
            Console.WriteLine("Hello Main");
            string str = ReadLine("Введите количество уровней: ");
            contLevels = Convert.ToInt32(str);
            str = ReadLine("Задайте верхнюю границу");
            priceUp= decimal.Parse(str);
            str = ReadLine("Введите шаг уровня: ");
            StopLevel = decimal.Parse(str);
            str = ReadLine("Введите лот на  уровень: ");
            lotLevel= decimal.Parse(str);


            WriteLine();
            /*
            double PriceUp=9;
            Trade trade = new Trade();
            trade.Volume = 83838;

            str = PriceUp.ToString();            
            */
        }
          
            public static decimal StopLevel
            {
            get
                {
                    return stopLevel;
                }
             set
             { 
                 if(value <= 100 )
                 {
                    stopLevel = value;
                    levels= Level.CalculateLevels(priceUp, stopLevel, contLevels);
                 }
             }


             }
           
    }
}




