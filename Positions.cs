using ConsoleHome;
using ConsoleHome.Enums;
using System;
using System.Collections.Generic;
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

            timer.Interval = 1000;

            timer.Elapsed += NewTrade;

            timer.Start();

        }

        public string Nomercheta = "SPBFUT001";       // Торговый счет
        public string ClassCode = "SPBFUT";           // Класс инструмента 
        public string SecCode = "SRZ3";               // Код инструмента  

        public decimal PlanDS = 1000000;              // Планируемые (свободные) денежные средства
        public decimal PriceOpenPoz = 0;              // Цена открытых позиций
        public decimal SumOpenPoz = 0;                // Сумма открытых позиций  

        public decimal OpenPoz = 0;                   // Открытые позиции - (количество лот)
        public decimal LotSell = 0;                   // Количество лотов в продаже
        public decimal LotBuy = 0;                    // Количество лотов в покупке

        public decimal VarMarga = 0;                  // Вариационная маржа   
        public decimal NakDohod = 0;                  // Накопленный доход

        public Operation LS = Operation.Long;


        Random random = new Random();

        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);

            trade.Volume = Math.Abs(num);

            trade.Price = random.Next(70000, 80000);

            trade.SummaSd = trade.Volume * trade.Price;

            if (num > 0) // сделка в лонг
            {
                OpenPoz = OpenPoz + num;

                LS = Operation.Long;

                SumOpenPoz = SumOpenPoz + trade.SummaSd;

            }

            else if (num < 0) // сделка в шорт
            {
                OpenPoz = OpenPoz + num;

                LS = Operation.Short;

                SumOpenPoz = SumOpenPoz - trade.SummaSd;

            }

            if (OpenPoz != 0)
            {
                PriceOpenPoz = Math.Abs(SumOpenPoz) / Math.Abs(OpenPoz);
            }
            else { PriceOpenPoz = 0; SumOpenPoz = 0; }


            string str = "|" + Nomercheta + "|" + ClassCode + "|" + SecCode + "|" + "|" + LS + "\t|" + trade.Volume.ToString() + "\t|" + trade.Price.ToString() + "\t|" + trade.SummaSd.ToString() + "\t|" + OpenPoz + "\t|" + Math.Abs(SumOpenPoz) + "\t|" + PriceOpenPoz;


            Console.WriteLine(str);



        }
    }
}
