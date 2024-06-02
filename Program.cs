// See https://aka.ms/new-console-template for more information
using System;
using System.Net.NetworkInformation;
using System.Security.Cryptography;


namespace ConsoleHome
{
    public class Program
    {
        static decimal stopLevel;
        static decimal priceUp;
        static List<Level> levels;
        static int contLevels;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Main");
            levels = new List<Level>();          
            double PriceUp=9;
            Trade trade = new Trade();
            trade.Volume = 83838;

              string str = PriceUp.ToString();            
            
        }
          
            public static decimal StepLevel
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
                    decimal priceLevel = priceUp;
                    for( int i=0; i < contLevels; i++ )
                    {
                        Level level = new Level();
                        level.PriceLevel = priceLevel;
                        level.Add(priceLevel);
                        priceLevel -= StepLevel;
                    }


                 }
             }


             }
           
    }
}




