using ConsoleHome.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
//using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer; // выбор метода при конфликте названий из разных пространств

namespace ConsoleHome
{
    public class Position
    {
        public Position()                       // Конструктор класса Position при создании
        {
            Timer timer = new Timer();          // экземпляр timer объекта Timer пространства имён System.Timers
                                                // свойства объекта timer
            timer.Interval = 3000;              // частота действия 1 сек.

            timer.Elapsed += NewTrade;          // действие 

            timer.Start();
        }


        //----------------------------------------------- Fields ---------------------------------------------------- 
        #region Fields

        public decimal openPrice, avgPrice, positionLots, PnL = 0;
        public TradeDirections positionDirection = Enums.TradeDirections.Neutral;
        public PositionStatuses positionStatus = Enums.PositionStatuses.Empty;
        public PositionActions positionAction = Enums.PositionActions.None;
        public string secCode, classCode, portfolio = "";
        //public string ClassCode = "";
        public DateTime OpenTime, LastChangeTime, CloseTime = DateTime.MinValue;

        Random random = new Random();

        string[] AssetCodes = new string[]
        {
            "Si", "RTS", "Eu", "MIX", "MXI"
        };

        #endregion
        //--------------------------------------------- Fields end --------------------------------------------------

        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();
            // Trade Fields assign

            int vol = random.Next(-10, 10);

            if (vol > 0)
            {
                trade.TradeDirection = Enums.TradeDirections.Bye;
            }
            else if (vol < 0)
            {
                trade.TradeDirection = Enums.TradeDirections.Sell;
            }
            else
            {
                trade.TradeDirection = Enums.TradeDirections.Neutral;
            }

            trade.Volume = Math.Abs(vol);
            trade.Price = random.Next(70000, 80000);
            trade.SecCode = AssetCodes[0]; //random.Next(AssetCodes.Length)

            Console.WriteLine($"\tсделка: {trade.SecCode}, {trade.TradeDirection}, {trade.Price}, {trade.Volume}");

            // Positions Fields assign
            secCode = trade.SecCode;
            if (trade.Volume == 0)              // пришёл пустой объём, позиция не меняется
            { }
            else if (positionDirection == 0)    // открытие новой позиции
            {
                positionDirection = trade.TradeDirection;
                positionAction = PositionActions.Open;
                positionStatus = PositionStatuses.Open;

                openPrice = trade.Price;
                avgPrice = trade.Price;
                positionLots = trade.Volume;
                PnL = 0;

                OpenTime = DateTime.Now;
                LastChangeTime = OpenTime;

                PositionPrint();

            }

            else if (positionDirection == trade.TradeDirection) // добавление в позицию
            {
                positionAction = PositionActions.Add;

                avgPrice = Math.Round((this.avgPrice * this.positionLots + trade.Price * trade.Volume) / (this.positionLots + trade.Volume), 0);
                positionLots = this.positionLots + trade.Volume;
                PnL = 0;

                LastChangeTime = DateTime.Now;
                PositionPrint();

            }

            else if (positionDirection != trade.TradeDirection) // сокращение позиции
                if (positionLots > trade.Volume)
                {
                    positionAction = PositionActions.Reduce;
                    positionStatus = PositionStatuses.Open;
                    positionLots = this.positionLots - trade.Volume;
                    PnL = Math.Round((trade.Price - avgPrice) * trade.Volume, 0) * PositionSign();

                    LastChangeTime = DateTime.Now;

                    PositionPrint();

                }
                else if (positionLots <= trade.Volume)
                {
                    // 1. закрытие текущей позиции
                    positionAction = PositionActions.Close;
                    positionStatus = PositionStatuses.Closed;

                    trade.Volume = trade.Volume - positionLots;
                    PnL = Math.Round((trade.Price - avgPrice) * positionLots, 0) * (int) this.positionDirection;
                    positionLots = 0;

                    LastChangeTime = DateTime.Now;

                    PositionPrint();


                    // 2. открытие новой позиции на остаток
                    positionDirection = trade.TradeDirection;
                    positionAction = PositionActions.Open;
                    positionStatus = PositionStatuses.Open;

                    openPrice = trade.Price;
                    avgPrice = trade.Price;
                    positionLots = trade.Volume;
                    PnL = 0;

                    OpenTime = DateTime.Now;
                    LastChangeTime = OpenTime;

                    PositionPrint();

                }

        }

        //----------------------------------------------- Methods ---------------------------------------------------
        #region Methods
        void PositionPrint()
        {
            Console.WriteLine(
                $"Inst. = {secCode.ToString()}, " +
                $"Dir. = {positionDirection}, " +
                $"Act. = {positionAction}, " +
                $"Price = {avgPrice.ToString()}, " +
                $"Vol. = {positionLots.ToString()}, " +
                $"PnL = {PnL.ToString()}, " +
                $"Open = {OpenTime.ToString()}, " +
                $"Changed = {LastChangeTime.ToString()}"
                );
        }

        decimal PositionSign()
        {
            decimal d = 0m;
            if (positionDirection == TradeDirections.Bye) { d = 1m; }
            else if (positionDirection == TradeDirections.Sell) { d = -1m; }
            
            return d;
        }


        //--------------------------------------------- Methods end -------------------------------------------------
        #endregion

    }
}
