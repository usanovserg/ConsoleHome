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
        public TradeDirect tradeDirect;
        public decimal totalPriceVolume = 0; //prcVlm суммируется с предыдущими для деления потом на sumvolume
        public bool newposition;
        //public int previousSumSizeLot;//для сохранения предыдущего sumSizeLot 
        public delegate void PositionChanged();
        public event PositionChanged PosCh;
        #endregion
        public Position()
        {
            System.Timers.Timer timer = new System.Timers.Timer();

            timer.Interval = 50;

            timer.Elapsed += NewTrade;

            timer.Start();
            //while (true)
            //{
            //    NewTrade();
            //    Thread.Sleep(60);
            //}

        }

        Random random = new Random();
        //private void NewTrade()
        private void NewTrade(object? sender, ElapsedEventArgs e)
        {
            Trade trade= new Trade();
           
            
             lot = random.Next(-5, 6);
            


            if (lot>0)
            {
                tradeDirect = TradeDirect.Long;
                
            }
            else if(lot<0)
            {
                tradeDirect = TradeDirect.Short;
               
            }

            if (lot != 0)
            {
               
                int previousSumSizeLot = sumSizeLot;
                sumSizeLot += lot;

                trade.Volume = Math.Abs(lot);

                trade.Price = random.Next(4, 11);

                if ((previousSumSizeLot >= 0 & lot > 0) || (previousSumSizeLot <= 0 & lot < 0))// рассчет ср цены в случае усреднения
                {
                    
                    sumvolume += trade.Volume;
                    totalPriceVolume += trade.Price * trade.Volume;
                    avrgPrcPos = totalPriceVolume / sumvolume;

                }
                

                if (!newposition & sumSizeLot != 0)//для триггера на первую сделку 
                {
                    Console.WriteLine("новая позиция");
                    newposition = true;
                }
                string str = "Symbol: " + symbol + " / Volume = " + trade.Volume.ToString() + " / price= " + trade.Price.ToString()
                    + " /direct= " + tradeDirect + " /sumSizeLot: " + sumSizeLot + " /avrgPrcPos: " + avrgPrcPos.ToString("F2");

                Console.WriteLine(str);

                //закрытие текущей позиции и рассчет реверс позиции
                if ((previousSumSizeLot < 0 & sumSizeLot > 0) || (previousSumSizeLot > 0 & sumSizeLot < 0))
                {
                    Console.WriteLine("закрытие позиции");
                    PosCh();
                    Console.WriteLine();
                    
                    Console.WriteLine("новая позиция");
                    sumvolume = Math.Abs(sumSizeLot);
                    avrgPrcPos = trade.Price;
                    totalPriceVolume = trade.Price * sumvolume;

                    str = "Symbol: " + symbol + " / Volume = " + sumvolume.ToString() + " / price= " + trade.Price.ToString() + " /direct= " +
                        tradeDirect + " /sumSizeLot: " + sumSizeLot + " /avrgPrcPos: " + avrgPrcPos.ToString("F2");

                    Console.WriteLine(str);

                }
                //закрытие текущей позиции без переворота в противоположную
                if (previousSumSizeLot + lot == 0)
                {
                    Console.WriteLine("закрытие позиции");
                    newposition = false;
                    sumvolume = 0;
                    totalPriceVolume = 0;
                    PosCh();
                    Console.WriteLine();
                }

            }

        }


    }
}
