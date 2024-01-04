using ConsoleHome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MyConsole
{
    public class Positions
    {
        public Positions()
        {
            System.Timers.Timer timer = new System.Timers.Timer();

            timer.Interval = 3000;

            timer.Elapsed += NewTrade;

            timer.Start();

        }

        public string Nomercheta = "SPBFUT001";       // Торговый счет
        public string ClassCode = "SPBFUT";           // Класс инструмента 
        public string SecCode = "SRZ3";               // Код инструмента  

        public decimal PlanDS = 1000000;              // Планируемые (свободные) денежные средства
        public decimal PriceOpenPoz = 0;              // Цена открытых позиций
        public decimal SumOpenPoz = 0;                // Сумма открытых позиций  
        public decimal SummaSdelki = 0;
        
        public decimal OpenPoz = 0;                   // Открытые позиции - (количество лот)
        public decimal LotSell = 0;                   // Количество лотов в продаже
        public decimal LotBuy = 0;                    // Количество лотов в покупке
        
        
        public decimal VarMarga = 0;                  // Вариационная маржа   
        public decimal NakDohod = 0;                  // Накопленный доход
        public decimal izm = 0;                       // Изменение отрытой позиции

        public Trade.NaprSdelki LS = Trade.NaprSdelki.Long;


        Random random = new Random();

        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(100, 500);

            SummaSdelki = SummaSD(trade.Volume, trade.Price);

            SumOpenPoz = Sdelka(OpenPoz, num);

            IzmOpenPoz();
            
            if (OpenPoz != 0)
            {
                PriceOpenPoz = Math.Abs(SumOpenPoz) / Math.Abs(OpenPoz);
            }
            else { PriceOpenPoz = 0; SumOpenPoz = 0; }


            string str = "|" + Nomercheta + "|" + ClassCode + "|" + SecCode + "|" + "|" + LS + "\t|" + trade.Volume.ToString() + "\t|" + trade.Price.ToString() + "\t|" + SummaSdelki.ToString() + "\t|" + OpenPoz + "\t|" + Math.Abs(SumOpenPoz) + "\t|" + PriceOpenPoz;


            Console.WriteLine(str);

        }

        //===============================================Metod====================================================================================
        #region Metod

        public decimal SummaSD (decimal Volume, decimal Price)
        {
            decimal SummaSd = Volume * Price;

            return SummaSd;
        }

        public decimal Sdelka (decimal openpoz, int num)
        {
            if (num > 0) // сделка в лонг
            {
                OpenPoz = openpoz + num;

                LS = Trade.NaprSdelki.Long;

                SumOpenPoz = SumOpenPoz + SummaSdelki;
                                
            }

            else if (num< 0) // сделка в шорт

            {
                OpenPoz = openpoz + num;

                LS = Trade.NaprSdelki.Short;

                SumOpenPoz = SumOpenPoz - SummaSdelki;

            }
            
            return SumOpenPoz;
        }
        public void IzmOpenPoz()
        {
            if (OpenPoz != izm)
            {
                izm = OpenPoz;

                IzmPoz?.Invoke();
            }
        }       

        #endregion

        //====================================================delegate==================================================================================

        public delegate void izmpoz ();
        
        public event izmpoz? IzmPoz;

    }
}
           /*if (num > 0) // сделка в лонг
            {
                sdelka = SdelkaLong;
                
                SumOpenPoz=sdelka(OpenPoz,num);
                
            }

            else if (num < 0) // сделка в шорт
            {
                sdelka=SdelkaShort;

                SumOpenPoz = sdelka(OpenPoz, num);               
            }*/