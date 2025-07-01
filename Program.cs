// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


//Console.WriteLine("Hello, World!");

namespace ConsoleHome
{
    class Programm
    {        
        static List<Level> PriceLevels = new List<Level>();
        static int CountLevels = 0;
        static decimal priceUp=0;
        static decimal priceLow=0;
        static decimal Price = 0;
        static decimal Dif = 0;
        static decimal Step = 0;

        static void Main(string[] args)
        {
            Position position = new Position(); 


            //WriteLine();

            /*
            string str = ReadLine("Задайте количество уровней: ");
            CountLevels = Convert.ToInt32(str);

            str = ReadLine("Задайте верхний уровень цены: ");
            PriceUp = decimal.Parse(str);

            str = ReadLine("Задайте нижний уровень цены: ");
            PriceLow = decimal.Parse(str);
            */

            WriteLine();
            Console.ReadLine();            
        }

        #region fields
        public static decimal PriceUp
        {
            get
            {
                return priceUp;
            }
            set
            {
                if (priceUp < 0)
                {
                    Console.WriteLine("Начальная цена больше конечной, введите другую начальную цену..");
                    return;
                }
                else
                {
                    priceUp = value;
                    Price = priceUp;
                }
            }
        }

        public static decimal PriceLow
        {
            get
            {
                return priceLow;
            }
            set
            {                
                if (priceLow < PriceUp)
                {
                    priceLow = value;

                    Dif = PriceUp - priceLow;

                    if (Dif % CountLevels != 0)
                    {
                        Console.WriteLine("Некорректно задана конечная цена или шаг цены, задайте другие значения..");
                        return;
                    }
                    else
                    {
                        Step = Convert.ToInt32(Dif / CountLevels);
                    }
                }
                else
                {
                    Console.WriteLine("Некорректно задан шаг.. выход из программы...");
                    return;
                }
            }
        }
        #endregion

        static void WriteLine()
        {
            //Console.WriteLine("Количество элементов в списке: " + Pricelevels.Count.ToString());

            for (int i = 0; i < CountLevels; i++)
            {
                Level level = new Level();
                level.PriceLevel = Price;
                //level.LotLevel = Lot;

                PriceLevels.Add(level);
                Price = Price - Step;

                Console.WriteLine(Price);
            }

            Console.WriteLine("Количество элементов в списке: " + PriceLevels.Count.ToString());
        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }    
    }    
}

//propfull  























