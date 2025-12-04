using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleHome
{
    public class Position
    {

        #region Fields
        public int lot = 0;
        /// <summary>
        /// общий объем позы
        /// </summary>
        public int sumSizeLot= 0; //сумма открытых лотов текущей позиции
        public string symbol = "BTCUSDT";
        public decimal sumvolume = 0; //сумма объема для рассчета ср цены
        public decimal avrgPrcPos= 0; //ср цена
        public decimal prcVlm= 0; //цена*объем 
        public decimal totalPriceVolume = 0; //prcVlm суммируется с предыдущими для деления потом на sumvolume
        public bool newposition;
        public int sumsize;//для сохранения предыдущего sumSizeLot 
        List<Trade> list = new List<Trade>();
        #endregion
        public Position()
        {
            System.Timers.Timer timer = new System.Timers.Timer();

            timer.Interval = 50;

            timer.Elapsed += NewTrade;

            timer.Start();
           
        }

        Random random = new Random();

        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade= new Trade();
           
            
             lot = random.Next(-100, 100);
            if (lot == 0)
            {
                lot = 1; 
            }


            if (lot>0)
            {
                trade.Tradedirect = Trade.TradeDirect.Long;
                sumsize = sumSizeLot;
                sumSizeLot += lot;
               

            }
            else if(lot<0)
            {
                trade.Tradedirect = Trade.TradeDirect.Short;
                sumsize = sumSizeLot;
                sumSizeLot += lot;
                
            }
            

            trade.Volume = Math.Abs(lot);

            trade.Price=random.Next(70000, 80000);

            if ((sumsize>=0 & lot>0)|| (sumsize <= 0 & lot < 0))// рассчет ср цены в случае усреднения
            {
                list.Add(trade);
                sumvolume += trade.Volume;
                totalPriceVolume += trade.Price * trade.Volume;
                avrgPrcPos = totalPriceVolume / sumvolume;
                
            }
            else
            {
                // рассчет ср цены в случае частичного закрытия, но че-то голова уже пухнет
            }

            if (!newposition & sumSizeLot != 0)//для триггера на первую сделку 
            {
                Console.WriteLine("новая позиция");
                newposition = true;
            }
            string str = "Symbol: " + symbol + " / Volume = " + trade.Volume.ToString() + " / price= " + trade.Price.ToString()
                + " /direct= " + trade.Tradedirect + " /sumSizeLot: " + sumSizeLot + " /avrgPrcPos: " + avrgPrcPos.ToString("F2");

            Console.WriteLine(str);

            //закрытие текущей позиции и рассчет реверс позиции
            if ((sumsize < 0 & sumSizeLot>0)|| (sumsize > 0 & sumSizeLot < 0)) 
            {
                Console.WriteLine("закрытие позиции");
                Console.WriteLine();
                Console.WriteLine("новая позиция");
                sumvolume = Math.Abs(sumSizeLot);
                avrgPrcPos = trade.Price;
                totalPriceVolume = trade.Price*sumvolume;
                
                str = "Symbol: " + symbol + " / Volume = " + sumSizeLot.ToString() + " / price= " + trade.Price.ToString() + " /direct= " + 
                    trade.Tradedirect + " /sumSizeLot: " + sumSizeLot + " /avrgPrcPos: "+ avrgPrcPos.ToString("F2");
                
                Console.WriteLine(str);
            }
            //закрытие текущей позиции без переворота в противоположную
            if (sumsize + lot == 0)
            {
                Console.WriteLine("закрытие позиции");
                newposition = false;
            }



        }


    }
}
