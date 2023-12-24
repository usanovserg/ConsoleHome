using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Lesson_5 
{/// <summary>
/// Класс для рандомной генерации значений для класса позиция и запуска метода вывода на экран данных класса позиция 
/// </summary>
    public class StarTrading
    {
        public StarTrading()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 4000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        Random random = new Random();

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {

            CurrentPosition myCurrentPosition = new CurrentPosition();
            
            myCurrentPosition.Volume = random.Next(-20, +20);

            if (myCurrentPosition.Volume > 0)
            {
                EnumTypePosition.MyEnumOfTrades positiontype = EnumTypePosition.MyEnumOfTrades.Long;
                myCurrentPosition.PositionType = Convert.ToString(positiontype);
            }
            else 
            {
                EnumTypePosition.MyEnumOfTrades positiontype = EnumTypePosition.MyEnumOfTrades.Short;
                myCurrentPosition.PositionType = Convert.ToString(positiontype);
            }
            
           
            // Приводим текущий объём лотов в положительному значению
            myCurrentPosition.Volume = Math.Abs(myCurrentPosition.Volume);

            // Определяем стоимость случайным образом
            myCurrentPosition.PriceOfLot = random.Next(100, 200);

            // Определяем общуую стоимость позции
            myCurrentPosition.Price = myCurrentPosition.PriceOfLot*myCurrentPosition.Volume;

            // Определяем остаток депо 
            myCurrentPosition.Deposit = myCurrentPosition.Deposit - myCurrentPosition.Price;

            //Определяем тип  актива
            
            if (myCurrentPosition.PriceOfLot > 170 )            
                myCurrentPosition.SecCode = Convert.ToString(EnumSecCode.SecCode.Sber);
            if (myCurrentPosition.PriceOfLot > 135 && myCurrentPosition.PriceOfLot < 170)
                myCurrentPosition.SecCode = Convert.ToString(EnumSecCode.SecCode.Lukoil);
            if (myCurrentPosition.PriceOfLot<135)
                myCurrentPosition.SecCode = Convert.ToString(EnumSecCode.SecCode.Gazp);

            myCurrentPosition.ConsoleWrite();

        }


    }
}
