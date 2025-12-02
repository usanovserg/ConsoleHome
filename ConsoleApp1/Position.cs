using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection;
using System.Text;
//using System.Threading.Tasks;
using System.Timers;
using static MyConsole_1_6.Program;
namespace ConsoleHome_1_6
{
    public class Position
    {
        public Position()
        {

            System.Timers.Timer timer = new System.Timers.Timer();  //????? поч System.Timers

            timer.Interval = 1000;

            timer.Elapsed += NewTrade;

            timer.Start();
        }
        Random random = new Random();





        


        public int RowNumber = 0; // счетчик строк 

        private void NewTrade(object sender, ElapsedEventArgs e)
        {
            Trade trade = new Trade();

            int num = random.Next(-10, 10);
            byte num_day = (byte)random.Next(1, 6);
            byte num_ticker = (byte)random.Next(1, 4);



            DateTime startDate = new DateTime(2022, 10, 1, 00, 00, 00);
            DateTime endDate = new DateTime(2022, 12, 1, 00, 00, 00);

            int totalSeconds = (int)(endDate - startDate).TotalSeconds;


            int randomSeconds = (int)random.Next(totalSeconds);             // Случайные секунды для смещения по дате 
            DateTime randomDateTime = startDate.AddSeconds(randomSeconds);
            string randomDateTimeStr = randomDateTime.ToString("dd.MM.yyyy HH:mm:ss");


            DateTime newRandomDateTime = randomDateTime.AddSeconds(randomSeconds);
            string newRandomDateTimeStr = newRandomDateTime.ToString("dd.MM.yyyy HH:mm:ss");

            trade.Price = random.Next(70000, 80000);
            trade.Volume = Math.Abs(num);
            trade.GUIDPos = random.Next(100000, 300000);




            //if (num != 0) 
            //{
            //int UpRangeNewVolume = (int)(trade.Volume - 1);             //максимум сколько прибавим из объема текущей сделки
            //int LowRangeNewVolume = UpRangeNewVolume * -1;              //нижняя граница с обратным знаком чтоб уменьшать объем       
            //NewVolume = trade.Volume + random.Next(LowRangeNewVolume, UpRangeNewVolume);
            //    NewNum = random.Next(-10, 10);

            //}
            //num = 6;


            if (num > 0)
            {
                trade.TypeOrder = "LONG";
                trade.StopLoss = trade.Price - random.Next(0, 10000);
                trade.TakeProfit = trade.Price + random.Next(0, 10000);
            }
            else if (num < 0)
            {
                trade.TypeOrder = "SHORT";
                trade.StopLoss = trade.Price + random.Next(0, 10000);
                trade.TakeProfit = trade.Price - random.Next(0, 10000);
            }


            // расчет нового объема 
            int NewNum = random.Next(-10, 10);
            //NewNum = -6;
            trade.NewPrice = random.Next(70000, 80000);
            int delta = num + NewNum;           // сумма, чтоб не перевернуть отрицательный NewNum
            trade.NewVolume = Math.Abs(delta);


            switch (delta)
            {
                case > 0:
                    trade.NewTypeOrder = "LONG";
                    trade.NewPrice = Math.Round((trade.Price * trade.Volume + trade.NewPrice * Math.Abs(NewNum)) / ( trade.Volume + Math.Abs(NewNum) ), 2);
                    trade.NewStopLoss = trade.NewPrice - random.Next(0, 10000);
                    trade.NewTakeProfit = trade.NewPrice + random.Next(0, 10000);

                    break;

                case < 0:

                    trade.NewTypeOrder = "SHORT";
                    trade.NewPrice = Math.Round((trade.NewPrice * trade.Volume + trade.NewPrice * Math.Abs(NewNum)) / (trade.Volume + Math.Abs(NewNum)), 2);
                    trade.NewStopLoss = trade.NewPrice + random.Next(0, 10000);
                    trade.NewTakeProfit = trade.NewPrice - random.Next(0, 10000);

                    break;



            }
            
            

            string RSI = "RSI(" + random.Next(1, 14) + ")"; //индикатор
            string new_RSI = "RSI(" + random.Next(1, 14) + ")"; //индикатор




            //string str = "Type_Order - " + trade.TypeOrder + " /Volume = " + trade.Volume.ToString() + " / Price = " + trade.Price.ToString() + " /Date - " + randomDateTime + " /Day_Of_Week - " + (DayOfWeek) num_day;

            if (num != 0) //защита от 0
            {
               RowNumber =  RowNumber += 1;


                Console.WriteLine("{0,-10} {1,-10} {2,-10} {3, -6} {4,-8} {5,-12} {6, -12} {7, -12} {8,-20} {9,-10} {10,-12} {11, -50}",
                    
                        RowNumber.ToString(),                           //ID
                        trade.GUIDPos.ToString(),                       //GUID
                        ((NameTicker.Ticker)num_ticker).ToString(),     //"Ticker"
                        trade.TypeOrder.ToString(),                     //"Type"  
                        trade.Volume.ToString(),                        //"Volume"
                        trade.Price.ToString(),                         //"Price"
                        trade.StopLoss.ToString(),                      //"StopLoss"
                        trade.TakeProfit.ToString(),                    //"TakeProfit"
                        randomDateTimeStr.ToString(),                   //"Date"
                        ((DayWeek.MineDayOfWeek)num_day).ToString(),    //"Day"
                        RSI,                                            //"Indicator"
                        "Открыта позиция".ToString()                    //"Comment"
                    );
                //ChangePosEvent($"Позиция {trade.GUIDPos} изменилась.   New Volume = {NewVolume} New Price = {trade.Price + random.Next(-100, 100)}");
                
                RowNumber = RowNumber += 1;
             
                ChangePosEvent(
                        RowNumber.ToString(),                                                            //ID
                        trade.GUIDPos.ToString(),                                                        //GUID
                        ((NameTicker.Ticker)num_ticker).ToString(),                                      //"Ticker"
                        (trade.NewTypeOrder?? trade.TypeOrder).ToString(),                               //"Type"  
                        trade.NewVolume.ToString(),                                                      //"Volume"
                        trade.NewPrice.ToString(),                                                       //"Price"
                        trade.NewStopLoss.ToString(),                                                    //"StopLoss"
                        trade.NewTakeProfit.ToString(),                                                  //"TakeProfit"
                        newRandomDateTimeStr.ToString(),                                                 //"Date"
                        ((DayWeek.MineDayOfWeek)num_day+1).ToString(),                                   //"Day"
                        new_RSI,                                                                         //"Indicator"
                        ($"Позиция {trade.GUIDPos} {(trade.NewTypeOrder == null ? "закрылась" :"изменилась")}" +
                        $"{(trade.NewTypeOrder != trade.TypeOrder & trade.NewTypeOrder != null ? (" на "+trade.NewTypeOrder): "") } New Volume = {trade.NewVolume}").ToString() //"Comment"
                    );

            }
    }
        public delegate void ChangePos(string ID, string GUID, string Ticker, string Type, string Volume, string Price, string StopLoss, string TakeProfit, string Date, string Day, string Indicator, string Comment);
        public event ChangePos ChangePosEvent;


    }
}
