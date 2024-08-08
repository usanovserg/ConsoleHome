using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsEngine.Entity;

namespace OsEngine.Robots._MyRobots.Entity; 

public class Level
{
    public int Number { get; set; }
    public bool IsOn { get; set; } = true;
    public decimal PriceEnter { get; set; } = 10;
    public decimal PriceExit { get; set; } = 0;
    public decimal Volume { get; set; } = 0;
    public Side Side { get; set; }
    public bool IsSelected { get; set; } = false;
    public string Delete { get; set; }


    //====================== Цикл расчета сетки =========================================
    #region Цикл расчета сетки

    public static List<Level> CalcLevels(decimal startPrice, decimal step, int count,
         decimal profit, decimal volume, Side side)
    {
        List<Level> levels = [];


        for (var i = 0; i < count; i++)
        {
            Level level = new()
            {
                IsOn = true,
                PriceEnter = startPrice,
                PriceExit = startPrice + profit,
                Side = side,
                Volume = volume,
                IsSelected = false
            };
            //level.Number -= 1 ;                   
            levels.Add(level);
            startPrice -= step;
        }

        //levels = [.. levels.OrderByDescending(x => x.PriceEnter)];

        SaveLevels(levels);

        return levels;
    }

    #endregion

    // ========================================================================================

    public string GetSaveStr()
    {
        string result = "";

        result += IsOn + "|";
        result += PriceEnter + "|";
        result += Volume + "|";
        result += Side + "|";
        result += PriceExit + "|";
        result += IsSelected + "|";

        return result;
    }

    private static string _pathL = "GridBotMVVM_Levels.txt";

    public static void SaveLevels(List<Level> levels)
    {
        //Levels.OrderByDescending(x => x.PriceEnter);



        try
        {
            using StreamWriter writer = new(_pathL, false);
            for (int i = 0; i < levels.Count; i++)
            {
                writer.WriteLine(levels[i].GetSaveStr());
            }

            writer.Close();
        }
        catch (Exception) { }
    }


    public static void LoadLevels(List<Level> levels)
    {
        if (!File.Exists(_pathL))
        {
            return;
        }

        try
        {
            using StreamReader reader = new(_pathL);
            while (reader.EndOfStream == false)
            {
                Level level = new();
                level.ParsingLevels(reader.ReadLine());
                levels.Add(level);
            }
            reader.Close();

        }
        catch (Exception)
        {
            // отправить в лог
        }
    }


    public void ParsingLevels(string? str)
    {
        string[]? saveArray = str?.Split('|');

        IsOn = saveArray![0].Contains("True");
        PriceEnter = saveArray[1].ToDecimal();
        Volume = saveArray[2].ToDecimal();
        Enum.TryParse(saveArray[3], out Side _);
        PriceExit = saveArray[4].ToDecimal();
        IsSelected = saveArray[5].Contains("True");
    }
}

