using Capital_Test.Entity;
using Capital_Test.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using ScottPlot.WPF;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using Microsoft.VisualBasic.FileIO;
using OpenTK.Graphics.OpenGL;
using Color = System.Drawing.Color;
using ScottPlot.TickGenerators.TimeUnits;
// ReSharper disable All
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Capital_Test.ViewModels;

public partial class VM : ObservableRecipient
{
    public VM()
    {
        WeekHours = Init();

        Strategies = Enum.GetValues(typeof(StrategyType)).Cast<StrategyType>().ToList();

        LoadParams();

        CheckBoxesCollection = new Dictionary<string, bool>()
        {
            ["IsCheckedFix"] = true,
            ["IsCheckedCap"] = false,
            ["IsCheckedProg"] = false,
            ["IsCheckedDown"] = false
        };

        //CheckBoxes = [true, false, false, false];
    }

    public Dictionary<string, bool> CheckBoxesCollection = [];

    public ObservableCollection<bool> CheckBoxes { get; set; }
        = new ObservableCollection<bool> { true, false, false, false };

    public ObservableCollection<Data> Datas { get; set; } = [];
    public List<List<PnlHour>> WeekHours { get; set; }
    public List<StrategyType> Strategies { get; set; }


    public WpfPlot WpfPlot { get; init; } = new WpfPlot();


    private decimal ResultDepo;
    private static decimal _max = 0;
    private static List<PnlDateTime> _pnls;
    private string? _fileName;

    [ObservableProperty] public bool checkChanged = false;
    [ObservableProperty] private decimal _depo = 20000;
    [ObservableProperty] private decimal _sumEq = 0;
    [ObservableProperty] private decimal _sumEqFilter;
    [ObservableProperty] DateTime _cancelSelectedDate;
    [ObservableProperty] private bool _isDateChecked;

    [ObservableProperty]
    private bool _isCheckedFix = true;
    partial void OnIsCheckedFixChanged(bool newValue)
    { CheckBoxes[0] = _isCheckedFix;}

    [ObservableProperty] private bool _isCheckedCap;
    partial void OnIsCheckedCapChanged(bool newValue)
    { CheckBoxes[1] = _isCheckedCap;}

    [ObservableProperty] private bool _isCheckedProg;
    partial void OnIsCheckedProgChanged(bool newValue)
    {CheckBoxes[2] = _isCheckedProg;}

    [ObservableProperty] private bool _isCheckedDown;
    partial void OnIsCheckedDownChanged(bool newValue)
    {CheckBoxes[3] = _isCheckedDown;}

    [ObservableProperty]
    private string? _fileNameDisplay;
    partial void OnFileNameDisplayChanged(string? value)
    {
        string[] split = _fileName!.Split('\\');
        string tempF = split[^1];
        _fileNameDisplay = tempF.Remove(tempF.Length - 4);
    }

    [ObservableProperty]
    private int _comboBoxSelectedIndex;

    partial void OnComboBoxSelectedIndexChanged(int value)
    {
        PlotCalc(ComboBoxSelectedIndex, true);

        for (int i = 0; i < 4; i++)
        {
            if (i != ComboBoxSelectedIndex)
            {
                CheckBoxes[i] = false;
            }
            else
            {
                CheckBoxes[i] = true;
            }
        }
        IsCheckedFix = CheckBoxes[0];
        IsCheckedCap = CheckBoxes[1];
        IsCheckedProg = CheckBoxes[2];
        IsCheckedDown = CheckBoxes[3];
    }
    
    [RelayCommand]
    public void Checked()
    {
        CalcFilter();
    }

    [RelayCommand]
    public void CheckedChanged()
    { 
        WpfPlot.Plot.Clear();

        for (int i = 0; i < CheckBoxes.Count; i++)
        {
            if (CheckBoxes[i] == true)
            {
                PlotCalc(i, false);
            }  
        }
    }
    

    public List<List<PnlHour>> Init()
    {
        List <List<PnlHour>> pnlWeek = [];

        for (int i = 0; i < 7; i++)
        {
            List < PnlHour > pnlDay = [];

            for (int x = 0; x < 24; x++)
            {
                PnlHour pnlHour = new PnlHour();
                {
                    pnlHour.Hour = x;
                } ;

                pnlHour.EventCheckChanged += PnlHour_EventCheckChanged;
               
                pnlDay.Add(pnlHour);
            }
            pnlWeek.Add(pnlDay);
        }
        return pnlWeek;
    }

    private void PnlHour_EventCheckChanged()
    {
    }

    [RelayCommand]
    public void Calc()
    {
        ParsingFile();

        decimal sumEq = 0;

        foreach (var pnlDT in _pnls)
        {
            sumEq += pnlDT.Pnl;
        }
        SumEq = sumEq;

        CalcWeekHours();
        CalcFilter();
    }


    private void CalcWeekHours()
    {
        WeekHours = Init();

        foreach (var pnlDT in _pnls)
        {
            if (pnlDT.DateTime > CancelSelectedDate.AddDays(1))
            {
                break;
            }

            int ind = (int)pnlDT.DateTime.DayOfWeek;

            WeekHours[ind][pnlDT.DateTime.Hour].Pnl += pnlDT.Pnl;
            WeekHours[ind][pnlDT.DateTime.Hour].Maximum = _max;
        }
        OnPropertyChanged(nameof(WeekHours));
    }


    [RelayCommand]
    public void CalcFilter()
    {
        if (_pnls == null) return;

        decimal sumEq = 0;
        Datas.Clear();

        List<decimal> depos = [];
        int countTake = 0;
        int countStop = 0;
        decimal koefPercent = 1;
        int multiplay = 1;
        int div = 1;

        foreach (StrategyType type in Strategies)
        {
            Datas.Add(new Data(Depo, type));
            depos.Add(ResultDepo);
        }

        foreach (var pnlDT in _pnls)
        {
            int ind = (int)pnlDT.DateTime.DayOfWeek;
            if (WeekHours[ind][pnlDT.DateTime.Hour].IsActiv)
            {
                sumEq += pnlDT.Pnl;

                //============ 1 стратегия ================
                depos[0] += pnlDT.Pnl;

                //============ 2 стратегия ================
                depos[1] += pnlDT.Pnl * koefPercent;
                decimal newKoef = (depos[1] + Depo) / Depo;
                if (koefPercent < newKoef) koefPercent = newKoef;

                //============ 3 стратегия ================
                depos[2] += pnlDT.Pnl * multiplay;

                //============ 4 стратегия ================
                depos[3] += pnlDT.Pnl / div;

                if (pnlDT.Pnl > 0)
                {
                    countTake++;
                    multiplay = 3;
                    div = 1;
                }
                else
                {
                    countStop++;
                    multiplay = 1;
                    div *= 2;
                    if (div > 4) {div = 4;}
                }
                for (int i = 0; i < Strategies.Count; i++)
                {
                    Datas[i].ResultDepo = depos[i] + Depo;
                    Datas[i].CountTake = countTake;
                    Datas[i].CountStop = countStop;
                }
            }
            SumEqFilter = sumEq;
        }
        CheckedChanged();
    }


    [RelayCommand]
    private void LoadFile(object obj)
    {
        OpenFileDialog fileDialog = new OpenFileDialog();

        fileDialog.Filter = "Txt Files| *.txt";

        if (fileDialog.ShowDialog() != true) return;
        _fileName = fileDialog.FileName;

        if (!File.Exists(_fileName)) return;

        SaveParams(_fileName);

        FileNameDisplay = _fileName;

        ParsingFile();
    }

    private void ParsingFile()
    {
        try
        {
            List<PnlDateTime> pnls = [];

            if (string.IsNullOrEmpty(_fileName)) return;

            TextFieldParser parser = new TextFieldParser(_fileName);
            parser.SetDelimiters(";");

            string[] listString = File.ReadAllLines(_fileName!);
            const int numberRowTrade = 12;
            const int numberRowDt = 1;

            foreach (var str in listString)
            {
                if (string.IsNullOrEmpty(str)) continue;

                string[] split = str.Split(';');
                if (split.Length <= numberRowTrade) continue;

                if (!decimal.TryParse(split[numberRowTrade], out decimal pnl)) continue;

                PnlDateTime PnlDT = new PnlDateTime();
                PnlDT.Pnl = pnl;

                PnlDT.DateTime = DateTime.ParseExact(split[numberRowDt], "dd.MM.yyyy H:mm:ss", new CultureInfo("ru-Ru"));

                if (PnlDT.DateTime > CancelSelectedDate && _isDateChecked)
                {
                    continue;
                }

                pnls.Add(PnlDT);

                if (_max < pnl) _max = pnl;

                if (_isDateChecked) continue;

                if (PnlDT.DateTime.Date > CancelSelectedDate || pnls.Count == 1)
                {
                    CancelSelectedDate = PnlDT.DateTime.Date;
                }
            }
            _pnls = pnls;
        }
        catch (Exception e) { MessageBox.Show(e.Message);}
    }

    private void SaveParams(string? fileName)
    {
        try
        {
            using StreamWriter writer = new("Cap_T_params.txt", false);
            writer.WriteLine(fileName);
            writer.Close();
        }
        catch (Exception e) { }
    }

    private void LoadParams()
    {
        string path = "Cap_T_params.txt";

        if (!File.Exists(path)) return;

        try
        {
            using StreamReader reader = new(path);
            _fileName = reader.ReadLine();

            reader.Close();

            ParsingFile();

            FileNameDisplay = _fileName;
            //string[] split = _fileName!.Split('\\');
            //_fileNameDisplay = split[^1];
        }
        catch (Exception e) { }
    }


    private void PlotCalc(int index, bool clear)
    {
        if (Datas.Count == 0) return;

        List<decimal> listEquity = Datas[index].GetListEquity();
        int count = listEquity.Count;

        double[] dataX1 = new double[listEquity.Count];
        double[] dataY1 = new double[listEquity.Count];

        for (int i = 0; i < count; i++)
        {
            dataX1[i] = i;
            dataY1[i] = (double)(listEquity[i]);
        }

        ScottPlot.Color color = default;

        switch (index)
        {
            case 0:
                color = ScottPlot.Color.FromColor(Color.CornflowerBlue);
                break;
            case 1:
                color = ScottPlot.Color.FromColor(Color.DarkRed);
                break;
            case 2:
                color = ScottPlot.Color.FromColor(Color.DarkGreen);
                break;
            case 3:
                color = ScottPlot.Color.FromColor(Color.Orange);
                break;
        }

        if (clear)
        {
            WpfPlot.Plot.Clear();
        }

        WpfPlot.Plot.Add.ScatterLine(dataX1, dataY1, color);
        WpfPlot.Plot.Axes.AutoScale();
        WpfPlot.Refresh();
        OnPropertyChanged(nameof(WpfPlot));
    }
}

