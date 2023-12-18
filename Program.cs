using System.Diagnostics;

using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleHome;
using ConsoleHome.Enum;

namespace ConsoleHome
{
    public class Program
    {


        static void Main(string[] args)
        {
            Position position = new Position(1000, "Position_1"); // создаем экземпляр класс Position

            //Position position2 = new Position(5000, "Pos_2222222222222222"); 

            //Trade trade = new Trade(); // создаем экземпляр класс Trade 

            int num = (int)Direction.Buy;
            num = (int)Direction.Sell;
            num = (int)Direction.None;

            Console.ReadLine();

            
        }
    }
}

