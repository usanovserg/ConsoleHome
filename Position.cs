using System.Security.AccessControl;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ConsoleHome;

public class Position
{

    public Position()
    {
        Timer timer = new Timer();

        timer.Interval = 5000;

        timer.Elapsed += NewTrade;
        
        timer.Start();

    }

    Random random = new Random();

    private void NewTrade(object sender, ElapsedEventArgs e)
    {

        Trade trade = new Trade();

        int num = random.Next(-10, 10);

        if (num > 0)
        {
            trade.Operation = Operation.Long;
        }
        else if (num < 0) 
        {
            trade.Operation = Operation.Short;
        }

        Trade.Quantity += num;

        trade.Volume = Math.Abs(num);

        trade.Price = random.Next(70000, 90000);

        trade.ClassCode = "SPBFUT";

        string TypeTrade = (trade.Price < 80000) ? trade.SecCode = "SiZ3" : trade.SecCode = "SiH4";

        trade.Comment = "Комментария нет";
  
        trade.NumTrade = random.Next(1000, 80000);

        trade.ID_Transaction = random.Next(1, 80000);

        trade.DateTime = DateTime.Now;

        string str = "Number: " + trade.NumTrade.ToString() + " ID_trans: " + trade.ID_Transaction.ToString() +  " Depo: " + trade.Depo.ToString() + " Ticker: " 
            + trade.SecCode.ToString() + " Class: " + trade.ClassCode.ToString() + " Дата: " + trade.DateTime.Day.ToString() + "." + trade.DateTime.Month.ToString() 
            + " Время: " + trade.DateTime.Hour.ToString() + ":" + trade.DateTime.Minute.ToString() + ":" + trade.DateTime.Second.ToString() + " Volume: " 
            + trade.Volume.ToString() + "Quantity: " + Trade.Quantity + " Price: " + trade.Price.ToString() + " Operation: " + trade.Operation.ToString() 
            + " Comment: " + trade.Comment.ToString();

        Console.WriteLine(str);

    }

}
