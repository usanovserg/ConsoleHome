using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTrainingBot
{
    


    internal class Program
    {
        static void Main(string[] args)
        {
            //PriceBoard price_board = new PriceBoard();
            //price_board.SetLowPrice().SetHighPrice().SetPriceStep().FillPriceList().PrintPriceList(System.Console.Out);
            
            Portfolio portfolio = new Portfolio();

            //в этот файл будет выводиться информация о составе портфеле после каждой сделки
            portfolio.FilePathToOutput = "PortfolioChanges.txt";

            Position rtsPosition = new Position("RTS-12.24", "RTS", "SPBFUT123", "SPBFUT123", 1);
            portfolio.SubscribeNewTrade(rtsPosition);
            rtsPosition.Connect();
            Console.ReadLine();
            //подписываемся на событие
            //portfolio.SubscribeNewTrade(rtsPosition);

        }
    }

   
}


