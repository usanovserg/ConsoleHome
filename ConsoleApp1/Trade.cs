using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome_1_6
{
    public class Trade
    {

        #region Fields 
        public decimal Price = 0;
        public decimal Volume = 0;
        public string SecCode = "";
        public string ClassCode = "";
        public DateTime DateTime = DateTime.MinValue;
        public string Profit = "";
        public string? TypeOrder = null;
        public decimal StopLoss = 0;
        public decimal TakeProfit = 0;
        public int GUIDPos = 0;

        public decimal NewPrice = 0;
        public decimal NewVolume = 0;
        public string NewSecCode = "";
        public string NewClassCode = "";
        public DateTime NewDateTime = DateTime.MinValue;
        public string NewProfit = "";
        public string? NewTypeOrder = null;
        public decimal NewStopLoss = 0;
        public decimal NewTakeProfit = 0;
        public int NewGUIDPos = 0;


    }
    public class DayWeek
    {
        public enum MineDayOfWeek : byte // чтобы меньше места исопльзовалось числа до 255
        {
            Monday = 1,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }
    }
    public class NameTicker
    {
        public enum Ticker : byte
        {
            SI = 1,
            RTS,
            SBER
        }
    }
    #endregion

}