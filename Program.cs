using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleHome.Entities;

namespace ConsoleHome
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Position position = new Position();
            position.NewTradeEvent += Position_NewTradeEvent;


         
            Console.ReadKey();

        }

        private static void Position_NewTradeEvent(object sender, Trade trade, decimal position)
        {
            Console.WriteLine("new event!");
            Console.WriteLine($"New trade info: DateTime = {trade.DateTime}, Volume = {trade.Volume}, " +
                $"Price = {trade.Price}, Direction = {trade.tradeDirection}, Position = {position}");
        }
    }
}

