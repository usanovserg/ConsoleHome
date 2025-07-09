using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
//using Timer = System.Timers.Timer;

namespace ConsoleHome
{
    class Position
    {
        #region Fields

        public List<Trade> MyTrades = new List<Trade>(); // Сделки к позиции
        
//        public int SellBuy = 0;          // Покупка или продажа ///////
        
        public decimal Volume = 0;       // Текущий объем позиции
        
        public decimal Price { get; set; } = 0; // Текущая средняя цена позиции
        
        public string SecCode = "Si";       // Наименование актива
        
        public string ClassCode = "FUT";      // Код сектора

        public decimal Profit = 0;            // Результат Позиции

        #endregion

        public Position()
        {
            Timer timer = new Timer();

            timer.Interval = 4000;

            timer.Elapsed += NewTrade;

            timer.Start();
        }

        Random random = new Random();
        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            int num = random.Next(-10, 10);

            if (num == 0) return;

            Trade trade = new Trade();

            if (num > 0)
            {
                trade.SellBuy = (int) Direction.L_side;
            }
            else if (num<0)
            {
                trade.SellBuy = (int)Direction.S_side;
            }

        
            trade.SecCode = "Si";       

            trade.ClassCode = "FUT"; 

            trade.Price = random.Next(70000, 80000);

            trade.Volume = random.Next(10, 150);

            // Создали новую 

            string str = trade.SellBuy == (int)Direction.L_side ? "Лонг" : "Шорт";

            /*
            string str = "";

            if (trade.SellBuy == (int)Direction.L_side)
            {
                str = "Лонг(Покупка)";
            }
            else str = "Шорт(Продажа)";
            */

            str = "Сделка " + str +  "   Volume = " + (trade.Volume * Math.Sign(num)).ToString() + ";  Price = " + trade.Price.ToString() + "   ===>    ";

            Console.Write(str);

            PositionChange(trade);

        }

        private void PositionChange(Trade trade)
        {
            try
            {
                if ((trade.ClassCode != this.ClassCode) | (trade.SecCode != this.SecCode)) return;

                this.MyTrades.Add(trade);

                if (this.Volume != 0)
                {
                    if (Math.Sign(this.Volume) != Math.Sign(trade.SellBuy))             // Проверка на закрытие позиции (полное или частичное)
                    {
                        if (Math.Abs(this.Volume) <= trade.Volume)
                        {
                            this.Profit += (trade.SellBuy > 0) ? (Math.Abs(this.Volume * this.Price) - trade.Price * this.Volume) : (trade.Price * this.Volume - this.Volume * this.Price);

                            if (Math.Abs(this.Volume) != trade.Volume)                  // Произошел переворот позиции
                            {
                                this.Price = trade.Price;

                                this.Volume = (trade.Volume - Math.Abs(this.Volume)) * trade.SellBuy;
                            }
                            else
                            {
                                this.Price = 0;                                         // Позиция нулевая (закрыта)

                                this.Volume = 0;
                            }
                        }
                        else
                        {
                            this.Profit += (trade.SellBuy > 0) ? (Math.Abs(trade.Volume * this.Price) - trade.Price * trade.Volume) : (trade.Price * trade.Volume - trade.Volume * this.Price);

                            this.Volume += trade.Volume * trade.SellBuy;
                        }
                    }
                    else                                                                // Увеличение позиции
                    {
                        decimal _pos = this.Volume * this.Price;

                        this.Volume += trade.Volume * trade.SellBuy;

                        this.Price = Math.Abs((_pos + trade.Price * trade.Volume * trade.SellBuy) / this.Volume);
                    }
                }
                else
                {
                    this.Volume = trade.Volume * trade.SellBuy;

                    this.Price = trade.Price;
                }
                string _msg = "Текущая позиция:   " + this.Volume.ToString() + ";    @ Price:   " + Math.Round(this.Price, 2).ToString() +
                        "    Текущий результат:    " + Math.Round(this.Profit, 0).ToString() + "\n";

                Console.WriteLine(_msg);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
