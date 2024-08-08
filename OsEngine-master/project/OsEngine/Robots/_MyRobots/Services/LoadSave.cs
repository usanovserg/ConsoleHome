using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using OsEngine.Entity;
using Side = OsEngine.Robots._MyRobots.Entity.Side;

namespace OsEngine.Robots._MyRobots.Services;
 
public static class LoadSave
{
    private static string _pathS = "BotMvvmTest.txt";

    public static void SaveParams(decimal startPrice, decimal step, int count, decimal profit, decimal volume, Side side, decimal priceEnd)
    {
        try
        {
            using StreamWriter writer = new(_pathS, false);

            writer.WriteLine(startPrice);
            writer.WriteLine(step);
            writer.WriteLine(count);
            writer.WriteLine(profit);
            writer.WriteLine(volume);
            writer.WriteLine(side);
            writer.WriteLine(priceEnd);

            writer.Close();
        }
        catch (Exception) { }
    }
}
