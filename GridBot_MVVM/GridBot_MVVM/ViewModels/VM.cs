using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GridBotMVVM.Entity;
using GridBotMVVM.Entity.Enums;
using Microsoft.Win32;
using GridBotMVVM.Utils;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using static GridBotMVVM.Services.Extensions;
using static GridBotMVVM.Services.LoadSave;
using ScottPlot.WPF;
using ScottPlot;
using ScottPlot.Plottable;
using ScottPlot.Renderable;
using ScottPlot.Statistics;
using SkiaSharp;
using ScottPlot.Drawing.Colormaps;
using Axis = ScottPlot.Renderable.Axis;
using LiveChartsCore.Kernel;
using Coordinate = ScottPlot.Coordinate;
using static System.Runtime.InteropServices.JavaScript.JSType;

// ReSharper disable All
#pragma warning disable CS0168 // Variable is declared but never used

namespace GridBotMVVM.ViewModels;

public partial class Vm : ObservableObject
{
    public Vm()
    {
       //sides = Enum.GetValues(typeof(Side)).Cast<Side>().ToList();
       //Sides = [Side.Buy, Side.Sell];
       //_regCalcGrid = Enum.GetValues(typeof(RegimeCalcGrid)).Cast<RegimeCalcGrid>().ToList();
       SideNames = Enum.GetValues(typeof(Side))
                        .Cast<Side>()
                        .Select(e => GetEnumDescription(e))
                        .ToList();

       RegmeCalcGridNames = Enum.GetValues(typeof(RegimeCalcGrid))
           .Cast<RegimeCalcGrid>()
           .Select(e => GetEnumDescription(e))
           .ToList();

        //Ls = new();
       Level.LoadLevels(Levels);
       LoadParams();
       LoadFileParams(FileNameDisplay);

       Emulator.NewTradeEvent += Emulator_NewTradeEvent; 
    }

    public List<string> SideNames { get; set; } 
    public List<string> RegmeCalcGridNames { get; set; }

    //public LoadSave Ls;

    //[ObservableProperty] 
    //public List<Side> sides;

    //[ObservableProperty] private List<RegimeCalcGrid> _regCalcGrid;

    [ObservableProperty] private bool _isOn;
    [ObservableProperty] private decimal _startPrice;
    //[ObservableProperty] private decimal _stepLevel;
    private decimal _stepLevel;
    public decimal StepLevel
    {
        get => _stepLevel;
        set
        {
            _stepLevel = Math.Abs(value);
            OnPropertyChanged(nameof(StepLevel));
        }
    }

    //[ObservableProperty] private decimal _priceEnd;
    private decimal _priceEnd;
    public decimal PriceEnd
    {
        get => _priceEnd;
        set
        {
            _priceEnd = value;  
            OnPropertyChanged(nameof(PriceEnd));
        }
    }

    private int _countLevels;
    public int CountLevels
    {
        get => _countLevels;
        set
        {
            _countLevels = Math.Abs(value);
            OnPropertyChanged(nameof(CountLevels));
        }
    }


    [ObservableProperty] decimal _volume;
    [ObservableProperty] private decimal _profit;

    [ObservableProperty] private Side _side;

    [ObservableProperty] private RegimeCalcGrid _regmeGrid;

    //[ObservableProperty] 
    //private string _comboBoxSideSelItem;
    private string _comboBoxSideSelItem = "Покупка";
    public string ComboBoxSideSelItem
    {
        get => _comboBoxSideSelItem;
        set
        {
            _comboBoxSideSelItem = value;
            OnPropertyChanged(nameof(ComboBoxSideSelItem));
            if (_comboBoxSideSelItem.Contains("Покупка"))
            {
                Side = Side.Buy;
            }
            else
            {
                Side = Side.Sell;
            }
        }
    }


    [ObservableProperty] 
    private string _comboBoxCalcSelItem = "Шаг и количество уровней";
    partial void OnComboBoxCalcSelItemChanged(string value)
    {  
        if (_comboBoxCalcSelItem.Contains("Шаг и количество уровней"))
        {
            RegmeGrid = RegimeCalcGrid.ByCountLevels;
        }

        else if (_comboBoxCalcSelItem.Contains("Шаг и последний уровень"))
        {
            RegmeGrid = RegimeCalcGrid.ByStepSize;
        }

        else
        {
            RegmeGrid = RegimeCalcGrid.ByCountLevelsAndLastLevel;
        }
    }


    //[ObservableProperty]
    private List<Level> _levels = [];
    public List<Level> Levels
    {
        get => _levels;
        set
        {
            _levels = value;            
            OnPropertyChanged(nameof(Levels));
        }
    }

    [RelayCommand]
    public void StartLevels()
    {   
        try
        {
            //----------------------------------Если Последний уровень 0----------------------------

            if (RegmeGrid == RegimeCalcGrid.ByCountLevels)
            {   
               // if (Side == Side.Sell && _stepLevel > 0 || Side == Side.Buy && _stepLevel < 0) _stepLevel *= -1;

                Levels = Level.CalcLevels(_startPrice, _stepLevel, _countLevels, _profit, _volume, _side);

                // Выводим Последний уровень
                PriceEnd = Levels.Count;
                if (Levels.Count > 0)
                {
                    PriceEnd = Levels[^1].PriceEnter;
                    //PriceEnd = Side == Side.Buy ? Levels[^1].PriceEnter : Levels[0].PriceEnter;
                }

                SaveParams(_startPrice, _stepLevel, _countLevels, _profit, _volume, _side, _priceEnd);
            }

            //----------------------------------Если шаг цены 0------------------------------------

            else if (RegmeGrid == RegimeCalcGrid.ByCountLevelsAndLastLevel)
            {
                int n = GetDecimalDigitsCount(_startPrice);

                // Расчитываем шаг цены

                if (CountLevels > 1)
                {
                    StepLevel = n > 0 ? Math.Round(((StartPrice - PriceEnd) / (CountLevels-1)), n) :
                        Math.Floor(((StartPrice - PriceEnd) / (CountLevels-1)));

                    StepLevel = Math.Abs(StepLevel);
                }
                else
                {
                    _stepLevel = 0;
                }

                Levels = Level.CalcLevels(_startPrice, _stepLevel, _countLevels, _profit, _volume, _side);

                // Выводим Шаг цены
                //StepLevel = Math.Abs(_stepLevel);

                PriceEnd = Levels.Count > 0 ? Levels[^1].PriceEnter : 0;

                SaveParams(_startPrice, _stepLevel, _countLevels, _profit, _volume, _side, _priceEnd);
            }

            ////----------------------------------Если Количество уровней 0---------------------------------

            else if (RegmeGrid == RegimeCalcGrid.ByStepSize)
            {
                //_countLevels = Math.Abs(Convert.ToInt32(Math.Floor((_startPrice - _priceEnd) / _stepLevel)));

                if (Side == Side.Buy)
                {
                    CountLevels = (int)Math.Abs((_startPrice - _priceEnd + _stepLevel) / _stepLevel);
                }
                else
                { 
                    CountLevels = (int) Math.Abs((_startPrice - _priceEnd - _stepLevel) / _stepLevel);
                }  
                //if (Side == Side.Sell && _stepLevel > 0 || Side == Side.Buy && _stepLevel < 0) _stepLevel *= -1;
                
                Levels = Level.CalcLevels(_startPrice, _stepLevel, _countLevels, _profit, _volume, _side); 
                SaveParams(_startPrice, _stepLevel, _countLevels, _profit, _volume, _side, _priceEnd);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        //Запись параметров в файл
       
    }
    

    private void LoadParams()
    {
        string _pathP = "GridBotMVVM_params.txt";

        if (!File.Exists(_pathP))
        {
            return;
        }

        try
        {
            using StreamReader reader = new(_pathP);

            _startPrice = Convert.ToDecimal(reader.ReadLine());
            _stepLevel = Convert.ToDecimal(reader.ReadLine());
            _countLevels = Convert.ToInt32(reader.ReadLine());
            _profit = Convert.ToDecimal(reader.ReadLine());
            _volume = Convert.ToDecimal(reader.ReadLine());
            Enum.TryParse(reader.ReadLine(), out _side);
            _priceEnd = Convert.ToDecimal(reader.ReadLine()); 

            reader.Close();

            //if (File.Exists("GridBotLoadSaveTestData.txt"))
            //{
            //   string fileName = (File.ReadAllText("GridBotLoadSaveTestData.txt"));
            //   _fileName = fileName.Remove(fileName.Length - 2);
            //    string[] split = _fileName!.Split('\\');
            //    string tempF = split[^1];
            //    _fileNameDisplay = tempF.Remove(tempF.Length - 6);
            //    Candle.SetCandleFromString(_fileName);
            //}
        }
        catch (Exception ) {  } 
    }

    private string? _fileName;

    [ObservableProperty]
    private string? _fileNameDisplay;
    partial void OnFileNameDisplayChanged(string? value)
    {
        string[] split = _fileName!.Split('\\');
        string tempF = split[^1];
        _fileNameDisplay = tempF.Remove(tempF.Length - 4);
    }

    [RelayCommand]
    public void LoadFile(object? obj)
    {
        OpenFileDialog fileDialog = new OpenFileDialog();

        fileDialog.Filter = "Txt Files| *.txt";

        if (fileDialog.ShowDialog() != true) return;
        _fileName = fileDialog.FileName;

        if (!File.Exists(_fileName)) return;
        SaveParams(_fileName);

        FileNameDisplay = _fileName;

        //Candle candle = new Candle();
        Candle.SetCandleFromString(_fileName);
        
    }

    List<Candle> LoadCandles = Candle.loadCandles;

    private static int _emTimerInterval = 1;
    public static int EmTimerInterval
    {
        get => _emTimerInterval;
        set
        {
            _emTimerInterval = value;
            //OnPropertyChanged(nameof(EmTimerInterval)); 
        }
    }
    string status = "off";
    private Emulator em = new Emulator();

    [RelayCommand]
    private void EmulatorStart(object obj)
    {  
        if (status.Contains("off"))
        {
            _emTimerInterval = 1;
            em.TimerStart(LoadCandles, EmTimerInterval);
            status = "on";
            return;
        }   
        EmTimerInterval = 1000000;
        em.TimerStart(LoadCandles, EmTimerInterval);
        status = "off";
    }

    //[ObservableProperty] 
    //public ObservableCollection<Candle> candlesEm = []; 
    //partial void OnCandlesEmChanged(ObservableCollection<Candle> value)
    //{
    //    if (CandlesEm.Count > 2)
    //    {
    //        SelectedIndex = CandlesEm.Count - 1;
    //    }
    //}

    private ObservableCollection<Candle> _candlesEm = [];
    public ObservableCollection<Candle> CandlesEm
    {
        get => _candlesEm;
        set
        {  
            _candlesEm = value;
            OnPropertyChanged(nameof(CandlesEm));
        }
    }

    private void Emulator_NewTradeEvent(Candle candle)
    {
        CandlesEm.Add(candle);

        PlotCalc();
    }
   
    private int _selectedIndex;
    public int SelectedIndex
    {
        get { return _selectedIndex; }
        set 
        {
            _selectedIndex = value; 
            OnPropertyChanged(nameof(SelectedIndex));
        }
    }

    


    private WpfPlot _wpfPlot = new WpfPlot();
    public WpfPlot WpfPlot
    {
        get => _wpfPlot;
        set
        {
            _wpfPlot = value;
            OnPropertyChanged(nameof(WpfPlot));
        }
    }

    public WpfPlot WpfPlotV { get; set; } = new WpfPlot();




    private void PlotCalc()
    {   
        try
        {
            ObservableCollection<Candle> listPlotCandles = CandlesEm;

            TimeSpan timeSpan = TimeSpan.FromMinutes(5);
           
            List<double> voumePlotList = [];

            List<OHLC> prices = [];
            
            var plt = WpfPlot.Plot;
            var pltV = WpfPlotV.Plot;

            int i;


            // Лимиты /////////////////////////////////////////////////////////////////////

            AxisLimits limits = plt.GetAxisLimits();
            double minY;
            var difX = limits.XMax - limits.XMin;

            // Лимиты /////////////////////////////////////////////////////////////////////

            int delta = 12;

            for (i = 0; i < listPlotCandles.Count; i++)
            {
                double open = Convert.ToDouble(listPlotCandles[i].Open);
                double close = Convert.ToDouble(listPlotCandles[i].Close);
                double high = Convert.ToDouble(listPlotCandles[i].High);
                double low = Convert.ToDouble(listPlotCandles[i].Low);
                double volume = Convert.ToDouble(listPlotCandles[i].Volume);

                DateTime dt = listPlotCandles[i].TimeStart;

                minY = limits.YMin; 

                //voumePlot = voumePlot.Append(volume/90).ToArray();
                //yOffsetsV = yOffsetsV.Append(minY).ToArray();

                voumePlotList.Add(volume/500);

                prices.Add(new OHLC(open, high, low, close, dt, timeSpan));
            }                                                              
            

            WpfPlot.Plot.Clear();
            WpfPlotV.Plot.Clear();

            FinancePlot candles = WpfPlot.Plot.AddCandlesticks([.. prices]);
   
            candles.Sequential = true;    


            //DateTime[] tickDates = prices.Select(x => x.DateTime).ToArray();


            DateTime[] tickDates = [];

            if (prices.Count > 0)
            {
                for (int j = 0; j < prices.Count; j++)
                {
                    if (difX > 20 && j>0)
                    {    
                        if (prices[j].DateTime.Minute == 00 
                            && prices[j-1].DateTime.Minute == 55 )
                        {
                            delta = 12;
                            tickDates = tickDates.Append(prices[j].DateTime).ToArray();
                        }

                        else if(prices[j].DateTime.Minute == 00 
                                && prices[j - 1].DateTime.Minute == 45 
                                    && prices[j].DateTime.Day != prices[j-1].DateTime.Day)
                        {
                            delta = 12;
                            tickDates = tickDates.Append(prices[j].DateTime).ToArray();
                        }

                        else if (prices[j].DateTime.Minute == 05 
                                 && prices[j - 1].DateTime.Minute == 45)
                        {
                            delta = 11;
                            var dtTemp1 = prices[j].DateTime;
                            DateTime dtTemp = dtTemp1.AddMinutes(-05);

                            tickDates = tickDates.Append(dtTemp).ToArray();
                        }

                        else
                        {
                            continue;
                        }
                    }

                    else
                    {
                        delta = 1;

                        tickDates = tickDates.Append(prices[j].DateTime).ToArray();
                    }
                }

                double[] tickPositions = Generate.Consecutive(tickDates.Length, delta);
                string[] tickLabels = tickDates.Select(x => x.ToString("H.mm")).ToArray();
                plt.XTicks(tickPositions, tickLabels);
            }

                   

            double candleLimitMin = Double.MaxValue;
            double candleLimitMax = 0;

            if (prices.Count > 0)
            {
                int k = 0;
                if (prices.Count > 50)
                {
                    k = prices.Count - 50;
                }
                candleLimitMin = prices[k].Low;
                candleLimitMax = prices[k].High;

                for (int j = k; j < prices.Count; j++)
                {
                    if (Convert.ToDouble(prices[j].Low) < candleLimitMin)
                    {
                        candleLimitMin = Convert.ToDouble(prices[j].Low);
                    }

                    if (Convert.ToDouble(prices[j].High) > candleLimitMax)
                    {
                        candleLimitMax = Convert.ToDouble(prices[j].High);
                    }
                }
            }

           

            candles.YAxisIndex = plt.RightAxis.AxisIndex;

            plt.YAxis.Ticks(false);
            plt.YAxis.Grid(false);
            plt.RightAxis.Ticks(true);
            plt.RightAxis.Grid(true);

            plt.LeftAxis.Ticks(true);


            // plt.RightAxis.AutomaticTickPositions();


            var t22 = candles.GetAxisLimits();
            //plt.RightAxis.AxisTicks.;

            //var t32 = plt.LeftAxis.GetTicks();  
            //var t2 = plt.RightAxis.GetTicks();
            //plt.RightAxis.SetTicks(t32);
            //var t21 = plt.RightAxis.GetTicks();
            var t11 = plt.LeftAxis.GetSettings();
            var t12 = plt.LeftAxis.GetTicks();


            int pixelX = 100;
            int pixelY = 200;

            (double coordX, double coordY) = plt.GetCoordinate(pixelX, pixelY);


            limits = plt.GetAxisLimits();

            // Самая нижняя видимая координата X и Y
            double minX = limits.XMin;
            minY = limits.YMin;

            double pltVValue = 0;

            int limitMultVert = 150;

            //WpfPlot.Configuration.AddLinkedControl(WpfPlotV);

            WpfPlot.AxesChanged += WpfPlot_AxesChanged;
            //WpfPlotV.Configuration.AddLinkedControl(WpfPlot);
            WpfPlotV.Plot.AxisAutoY();
            WpfPlotV.Plot.SetAxisLimits(yMin: 0);

            if (prices.Count>0)
            {
                plt.SetAxisLimitsY(t22.YMin- t22.YMin/100*0.1, t22.YMax+ t22.YMin / 100 * 0.1, yAxisIndex: 0);
                plt.SetAxisLimitsY(t22.YMin - t22.YMin / 100 * 0.1, t22.YMax+ t22.YMin / 100 * 0.1,  yAxisIndex:1);
                //plt.SetAxisLimitsY(candleLimitMin -= candleLimitMin / limitMultVert, candleLimitMax += candleLimitMin / limitMultVert, yAxisIndex: 0);
                plt.SetAxisLimitsX(prices.Count-70, prices.Count+2);

                var hline = plt.AddHorizontalLine(prices.Last().Close);
                hline.YAxisIndex = 1;
                hline.PositionLabelOppositeAxis = true;
                hline.LineWidth = 1;
                hline.PositionLabel = true;
                hline.PositionLabelBackground = hline.Color;
                hline.DragEnabled = true;



                if (voumePlotList.Count > 0)
                {
                    double[] voumePlot = [];
                    double[] yOffsetsV = [];

                    var pltV_ = pltV.AddBar(voumePlotList.ToArray());
                    //pltV_.ValueOffsets = yOffsetsV;
                    pltVValue = pltV_.Values.Last();

                    //double[] offsets = Enumerable.Range(0, voumePlotList.Count).Select(x => voumePlotList.Take(x).Sum()).ToArray(); ;
                    //pltV_.ValueOffsets = offsets;

                    pltV_.FillColorNegative = Color.Red;
                    pltV_.FillColor = Color.Green;
                    pltV_.BarWidth = 0.8;
                    WpfPlotV.Plot.AxisAutoY();
                    WpfPlotV.Plot.BottomAxis.Ticks(false);
                    
                    //WpfPlotV.Plot.Margins();
                   // plt.Margins(x: .25, y: .4);
                    //plt.Layout(bottom: 100);


                    //WpfPlotV.Plot.Layout(bottom: 50);


                }
                //pltV.ValueOffsets = offsetsV;

                //if (pltV.Values.Length > 0)
                //{
                //    for (int j = 0; j < pltV.Values.Length; j++)
                //    {

                //        offsetsV = offsetsV.Append(pltV.Values[j]).ToArray();
                //        //if (pltVValue > pltV.Values[^2])
                //        //{
                //        //    pltV.FillColor = Color.Green;
                //        //}

                //        //else
                //        //{
                //        //    pltV.FillColor = Color.Red;
                //        //}
                //    }
                //}
            }
             

            var annotation = plt.AddAnnotation(" 5 минут ", Alignment.UpperRight);
            annotation.BackgroundColor = Color.White;
            annotation.Font.Size = 16;
                                     

            //// получение координат из пиксельных значений
            //Pixel px = new Pixel(, yPixel);
            //Coordinates coordinates = formsPlot1.GetCoordinates(px);

            //// получение пиксельных значений из координат
            //Coordinates coordinates = new Coordinates(xCoord, yCoord);
            //Pixel px = formsPlot1.Plot.GetPixel(coordinates);


            plt.XAxis.DateTimeFormat(true);

            WpfPlot.Refresh();
            WpfPlotV.Refresh();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void WpfPlot_AxesChanged(object sender, RoutedEventArgs e)
    {
        WpfPlotV.Plot.MatchAxis(WpfPlot.Plot, horizontal: true, vertical: false);
        WpfPlotV.Plot.MatchLayout(WpfPlot.Plot, horizontal: true, vertical: false);
        WpfPlotV.Refresh();
    }
}      