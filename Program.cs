using ConsoleHome;
using System.Timers;
using Timer = System.Timers.Timer;


Random rnd = new Random();
decimal currentPrice = 1000;
Position p = new Position("ZZZ");
void T_Elapsed(object? sender, ElapsedEventArgs e) {
    Trade.Direction dir = (Trade.Direction)rnd.Next(1, 3);
    currentPrice += rnd.Next(-20, 20);
    decimal vol = rnd.Next(1, 10)*(dir==Trade.Direction.BUY?1:-1);
    Trade t = new Trade(p.sec_code, currentPrice, vol, dir);
    Console.WriteLine(t.ToString() );
    p.OnTrade(t);
    Console.WriteLine(p);
}

p.onPositionChangeDelegate += P_onPositionChangeDelegate;

void P_onPositionChangeDelegate(decimal size) {
    Console.WriteLine($"delegateCall: новый размер позиции: {size}");
}

Timer t = new Timer();
t.Interval = 1000;
t.AutoReset = true;
t.Elapsed += T_Elapsed;

t.Start();
//Console.WriteLine(p);
//p.OnTrade(new Trade(p.sec_code, 10, 5, Trade.Direction.BUY));
//Console.WriteLine(p);
//p.OnTrade(new Trade(p.sec_code, 11, -3, Trade.Direction.SELL));
//Console.WriteLine(p);
Console.ReadLine();