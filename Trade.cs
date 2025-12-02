using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHome_1_5
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
            public string TypeOrder = "";
            public decimal StopLoss = 0;
            public decimal TakeProfit = 0;

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