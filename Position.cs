using Lesson_6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Lesson_6
{
    public class Position
    {
       
        public event EventHandler<PosEventArgs> Moving;

        public int OpenPos = 0;
        public int OpenPrice;
        public int PL;
        public int PL_Cum = 0;
        public int LastPos = 0;
        public Position()
        {

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += NewTrade;
            timer.Start();
        }
        public void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();
            int num = (new Random()).Next(-10, 10);
            while (num == 0) num = (new Random()).Next(-10, 10);
            Trade.Direction LS; // Переменная для определения Long/Short
            int value = 0;

            if (num > 0)
            {
                value = 1;
            }
            else if (num < 0)
            {
                value = -1;
            }
            LS = (Trade.Direction)value;

            trade.Volume = Math.Abs(num);
            trade.Price = (new Random()).Next(100, 300);
            if (OpenPos == 0)
            {
                OpenPrice = trade.Price;
            }

            #region PL calculation
            if (num < 0) // сделка на продажу
            {
                if (OpenPos > 0) // лонг закрывается
                {
                    if (trade.Volume < OpenPos) // лонг закрывается частично
                    {
                        PL = (trade.Price - OpenPrice) * trade.Volume;
                    }
                    else if (trade.Volume == OpenPos) // лонг закрывается полностью
                    {
                        PL = (trade.Price - OpenPrice) * OpenPos;
                    }
                    else if (trade.Volume > OpenPos) // лонг переворачивается в шорт
                    {
                        PL = (trade.Price - OpenPrice) * OpenPos;
                        OpenPrice = trade.Price;
                    }
                }
                else if (OpenPos < 0) // добор шорта
                {
                    OpenPrice = (trade.Volume * trade.Price + Math.Abs(OpenPos) * OpenPrice) / (trade.Volume + Math.Abs(OpenPos));
                }
            }
            else if (num > 0) // сделка на покупку
            {
                if (OpenPos < 0) // шорт закрывается
                {
                    if (trade.Volume < Math.Abs(OpenPos)) // шорт закрывается частично
                    {
                        PL = (OpenPrice - trade.Price) * trade.Volume;
                    }
                    else if (trade.Volume == Math.Abs(OpenPos)) // шорт закрывается полностью
                    {
                        PL = (trade.Price - OpenPrice) * OpenPos;
                    }
                    else if (trade.Volume > Math.Abs(OpenPos)) // шорт переворачивается в лонг
                    {
                        PL = (trade.Price - OpenPrice) * OpenPos;
                        OpenPrice = trade.Price;
                    }
                }
                else if (OpenPos > 0) // добор лонга
                {
                    OpenPrice = (trade.Volume * trade.Price + Math.Abs(OpenPos) * OpenPrice) / (trade.Volume + Math.Abs(OpenPos));
                }
            }
#endregion    

            PL_Cum += PL;

            OpenPos += num;

            string str = "Время " + DateTime.Now + " / Направление " + LS + " / Price = " + trade.Price.ToString() + " / Volume = " + trade.Volume.ToString() + " / Position " + OpenPos + " / AvrPrice " + OpenPrice + " / PL_Cum = " + PL_Cum;
            Console.WriteLine(str);

            if (OpenPos != LastPos)
            {
                Moving?.Invoke(this, new PosEventArgs(OpenPos));
                LastPos = OpenPos;
            }
        }
    }
}
