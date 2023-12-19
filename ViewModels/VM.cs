 
using ConsoleHome.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using System.Threading;
using System.Diagnostics;

namespace ConsoleHome.Models
{
    public class VM // имитируем Вью Модель (работу робота, который расчитывает нашу общую позицию и направление)

    {
        public VM()
        {
            _server = new Server(); 

            _server.EventTradeDelegate += _server_EventTradeDelegate; // подписываемся на событие которая есть внутри Server

        }

        


        #region==========================Fiels=============================

        Server _server;
        public string str;
        public decimal TotVol = 0m;
        public decimal LastTotVol = 0m;
        public Side Direction = Side.None;

        #endregion



        #region==========================Properties=============================

        public decimal Volume { get; set; }
         
        public decimal Price { get; set; }
         
        public DateTime DateTimeTrade { get; set; }

        public Side SideTrade { get; set; }

        #endregion

        #region==========================Methods=============================
        private void _server_EventTradeDelegate(Trade trade) // вызываем метод, на который подписан  делегат Сервера и в нем получаем данные из конектора
        {
             
            Volume = trade.Volume;  
            Price = trade.Price; 
            DateTimeTrade = trade.DateTime;  
            SideTrade = trade.Side;     
             
            decimal _num = Volume;

            if ((int)SideTrade < 0)  
            {
                _num *= (int)SideTrade; // если сделка Sell, то в _num присваиваем отрицатительное значение Volume
            }

            TotVol += _num;

            if (TotVol > 0)
            {
                Direction = Side.Long; // Позиция в Long
                 
            }
            else if (TotVol < 0)
            {
                Direction = Side.Short; // Позиция в Short
                 
            }
            else if (TotVol == 0)
            {
                Direction = Side.None; // Нет позиции
                
            }

            if (LastTotVol != TotVol)
            {
                string newTrade = "ПОЗИЦИЯ ИЗМЕНИЛАСЬ";
                EventMessageDelegate?.Invoke(newTrade);

            }
            else if (LastTotVol == TotVol)
            {
                string noTrade = "===========ПОЗИЦИЯ НЕ ИЗМЕНИЛАСЬ=========";
                EventMessageDelegate?.Invoke(noTrade);
            }

            LastTotVol = TotVol;

            str = ( DateTimeTrade.ToString() + " / Направление сделки =" + SideTrade.ToString()
                + " / Лот =" + Volume.ToString() + " / Цена =" + Price.ToString() 
                + " / Направление позиции =" + Direction.ToString()
                + " / Позиция =" + TotVol.ToString());

            EventMessageDelegate?.Invoke(str);
        }       

        #endregion

        #region==========================Events================================

        public delegate void MessageDelegate(string message); // передаем сообщение во Вьюшку
        public event MessageDelegate EventMessageDelegate;

        #endregion


    }
}
