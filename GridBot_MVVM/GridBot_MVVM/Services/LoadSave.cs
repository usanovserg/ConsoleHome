using GridBotMVVM.Entity.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GridBotMVVM.ViewModels;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System.Data.SqlTypes;
using System.Globalization;
using System.Windows;
using GridBotMVVM.Entity;

namespace GridBotMVVM.Services;


public static class LoadSave
{
    private static string _pathS = "GridBotMVVM_params.txt";
    private static string? _fileName;

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


    //public static void ParsingFile()
    //{
    //    try
    //    {
    //        List<Candle> loaCandles = [];

    //        if (string.IsNullOrEmpty(_fileName)) return;

    //        TextFieldParser parser = new (_fileName);
    //        parser.SetDelimiters(",");

    //        string[] listString = File.ReadAllLines(_fileName!);

    //        foreach (var str in listString)
    //        {
    //            if (string.IsNullOrEmpty(str)) continue;;

    //            if (!decimal.TryParse(split[numberRowTrade], out decimal pnl)) continue;

    //            PnlDateTime PnlDT = new PnlDateTime();
    //            PnlDT.Pnl = pnl;

    //            PnlDT.DateTime = DateTime.ParseExact(split[numberRowDt], "dd.MM.yyyy H:mm:ss", new CultureInfo("ru-Ru"));

    //            if (PnlDT.DateTime > CancelSelectedDate && _isDateChecked)
    //            {
    //                continue;
    //            }

    //            pnls.Add(PnlDT);

    //            if (_max < pnl) _max = pnl;

    //            if (_isDateChecked) continue;

    //            if (PnlDT.DateTime.Date > CancelSelectedDate || pnls.Count == 1)
    //            {
    //                CancelSelectedDate = PnlDT.DateTime.Date;
    //            }
    //        }
    //        _pnls = pnls;
    //    }
    //    catch (Exception e) { MessageBox.Show(e.Message); }
    //}

    public static void SaveParams(string? fileName)
    {
        try
        {
            using StreamWriter writer = new("GridBotLoadSaveTestData.txt", false);
            writer.WriteLine(fileName);
            writer.Close();
        }
        catch (Exception e) { }
    }

    public static void LoadFileParams(string? fileNameDisplay)
    {
        string path = "GridBotLoadSaveTestData.txt";

        if (!File.Exists(path)) return;

        try
        {
            //using StreamReader reader = new(path);
            //fileNameDisplay = reader.ReadLine();

            //reader.Close();

            //Candle candle = new Candle();
            Candle.SetCandleFromString(fileNameDisplay);
            //string[] split = _fileName!.Split('\\');
            //_fileNameDisplay = split[^1];
        }
        catch (Exception e) { }
    }


}

