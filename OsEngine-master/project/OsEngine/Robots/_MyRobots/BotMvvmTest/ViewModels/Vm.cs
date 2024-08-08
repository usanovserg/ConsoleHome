using CommunityToolkit.Mvvm.Input;
using OsEngine.Entity;
using OsEngine.Robots._MyRobots.Entity;
using ScottPlot;
using ScottPlot.Plottables;
using ScottPlot.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using OsEngine.Robots._MyRobots.Entity.Enums;
using OsEngine.Robots._MyRobots.Services;
using Extensions = OsEngine.Robots._MyRobots.Services.Extensions;
using Side = OsEngine.Robots._MyRobots.Entity.Side;

namespace OsEngine.Robots._MyRobots.BotMvvmTest.ViewModels;

public partial class Vm : INotifyPropertyChanged
{
    private BotMvvmTest _bot;

    public Vm()
    {

    }

    public Vm(BotMvvmTest botMvvmTest)
    {
        _bot = botMvvmTest;

        Volume = _bot.Volume.ValueDecimal;

        RegmeCalcGridNames = Enum.GetValues(typeof(RegimeCalcGrid))
            .Cast<RegimeCalcGrid>()
            .Select(e => Extensions.GetEnumDescription(e))
            .ToList();

        SideNames = Enum.GetValues(typeof(Side))
            .Cast<Side>()
            .Select(e => Extensions.GetEnumDescription(e))
            .ToList();

        //_regCalcGrid = Enum.GetValues(typeof(RegimeCalcGrid)).Cast<RegimeCalcGrid>().ToList();

        Level.LoadLevels(Levels);
        LoadParams();

        _bot._tab.CandleFinishedEvent += _tab_CandleFinishedEvent;
    }

    private void _tab_CandleFinishedEvent(List<Candle> obj)
    {
       Task.Run(PlotCalc);
    }

    public List<string> SideNames { get; set; }
    public List<string> RegmeCalcGridNames { get; set; }

    

    private Side _side;
    public Side Side
    {
        get => _side;
        set
        {
            _side = value;
            OnPropertyChanged(nameof(Side));
        }
    }

    //private Side _side = Side.Buy;



    //private List<RegimeCalcGrid> _regCalcGrid;
    //public List<RegimeCalcGrid> RegCalcGrid
    //{
    //    get => _regCalcGrid;
    //    set
    //    {
    //        _regCalcGrid = value;
    //        OnPropertyChanged(nameof(RegCalcGrid));
    //    }
    //}

    


    private bool _isOn;
    public bool IsOn
    {
        get => _isOn;
        set
        {
            _isOn = value;
            OnPropertyChanged(nameof(IsOn));
        }
    }

    

    private decimal _startPrice;
    public decimal StartPrice
    {
        get => _startPrice;
        set
        {
            _startPrice = value;
            OnPropertyChanged(nameof(StartPrice));
        }
    }

    private decimal _stepLevel;
    public decimal StepLevel
    {
        get => _stepLevel;
        set
        {
            _stepLevel = value;
            OnPropertyChanged(nameof(StepLevel));
        }
    }

    private decimal _volume;  
    public decimal Volume
    {
        get => _volume;
        set
        {
            _volume = value;
            OnPropertyChanged(nameof(Volume));
            _bot.Volume.ValueDecimal = value;
        }
    }

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
            _countLevels = value; ;
            OnPropertyChanged(nameof(CountLevels));
        }
    }

    
    private decimal _profit;
    public decimal Profit
    {
        get => _profit;
        set
        {
            _profit = value;
            OnPropertyChanged(nameof(Profit));
        }
    }

    
    private List<Side> _sides;
    public List<Side> Sides
    {
        get => _sides;
        set
        {
            _sides = value;
            OnPropertyChanged(nameof(Sides));
        }
    }


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

    private RegimeCalcGrid _regmeGrid = RegimeCalcGrid.ByCountLevels;
    public RegimeCalcGrid RegmeGrid
    {
        get => _regmeGrid;
        set
        {
            _regmeGrid = value;
            OnPropertyChanged(nameof(RegmeGrid));
        }
    }

   
    private string _comboBoxCalcSelItem = "Шаг и количество уровней";
    public string ComboBoxCalcSelItem
    {
        get => _comboBoxCalcSelItem;
        set
        {
            _comboBoxCalcSelItem = value;
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

            OnPropertyChanged(nameof(ComboBoxCalcSelItem));
        }
    }

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

    private WpfPlot _wpfPlot = new WpfPlot ();
    public WpfPlot WpfPlot
    {
        get => _wpfPlot;
        set
        {
            _wpfPlot = value;
            OnPropertyChanged(nameof(WpfPlot));
        }
    }

    //public WpfPlot WpfPlot { get; set; } = new WpfPlot();
          

    private void PlotCalc()
    {

        try
        {
            List<Candle> listPlotCandles = _bot._tab.CandlesAll;
            int count = listPlotCandles.Count;
           
            TimeSpan timeSpan = TimeSpan.FromMinutes(5); 
           
            List<OHLC> prices = [];

            // DateTime dt;

            var plt = WpfPlot.Plot;        

            //Thread.Sleep(200);

            int i;

            //for (DateTime dt = timeOpen; dt <= timeClose; dt += timeSpan)
            for (i = 0; i < count-1; i++)
            {
                double open = Convert.ToDouble(listPlotCandles[i].Open);
                double close = Convert.ToDouble(listPlotCandles[i].Close);
                double high = Convert.ToDouble(listPlotCandles[i].High);
                double low = Convert.ToDouble(listPlotCandles[i].Low);
                var dt = listPlotCandles[i].TimeStart;
                prices.Add(new OHLC(open, high, low, close, dt, timeSpan));

                if (prices.Count > 11)
                {
                    plt.Axes.SetLimitsX(listPlotCandles[i-10].TimeStart.ToOADate(), prices.Last().DateTime.ToOADate());
                }
            }

            CandlestickPlot candles;
            
        
            WpfPlot.Plot.Clear();

            candles = WpfPlot.Plot.Add.Candlestick(prices);
            //candles.Axes.YAxis = plt.Axes.Right;
            //plt.Axes.Left.Max = plt.Axes.Right.Max;
            //plt.Axes.Left.Min = plt.Axes.Right.Min;


            //int tickCount = 5;
            //int tickDelta = prices.Count / tickCount;
            //DateTime[] tickDates = prices
            //    .Select(x => x.DateTime)
            //    .ToArray();

            //double[] tickPositions = Generate.Consecutive(tickDates.Length);
            //string[] tickLabels = tickDates.Select(x => x.ToString("H : mm")).ToArray();
            //ScottPlot.TickGenerators.NumericManual tickGen = new(tickPositions, tickLabels);
            //plt.Axes.Bottom.TickGenerator = tickGen;



            plt.DataBackground.AntiAlias =  false;
    

            var DtGen = plt.Axes.DateTimeTicksBottom();

            if (prices.Count > 81)
            {
                var dif = prices.Last().DateTime.ToOADate() - prices[prices.Count - 2].DateTime.ToOADate();
                var lcandle = prices.Last().DateTime.ToOADate();

                                           
                var difVert = prices.Last().High - prices.Last().Low;
       
                var t1 = prices.GetRange(prices.Count - 80, 80);

                plt.Axes.SetLimitsX(prices[prices.Count - 80].DateTime.ToOADate(), prices.Last().DateTime.ToOADate() + dif);
                //plt.Axes.SetLimitsY(Convert.ToDouble(lowCandles.Low - difVert), Convert.ToDouble(hiCandles.High + difVert));
                // plt.Axes.SetLimitsX((lcandle - dif * 70), lcandle + dif);
            }
            
            plt.DataBackground.AntiAlias = false;

       



            //if (!plt.Legend.Equals(plt.Legend.ManualItems))
            //{
            //    plt.Legend.ManualItems.Add(new ( ) { LabelText = "5 минут" });
            //}
            LegendItem legend = new () { LabelText = "5 минут" };
            //plt.Legend.LegendItems = legend;

            //plt.Legend.ManualItems;
            plt.Legend.IsVisible = true;
            plt.Legend.Alignment = Alignment.UpperRight;
            plt.Legend.ManualItems.Clear();
            plt.Legend.ManualItems.Add(legend);  


            // plt.Axes.AutoScaleX();

            WpfPlot.Refresh();

            //OnPropertyChanged(nameof(WpfPlot));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private global::CommunityToolkit.Mvvm.Input.RelayCommand? startLevelsCommand;
    public IRelayCommand StartLevelsCommand => startLevelsCommand ??= new RelayCommand(new Action(StartLevels));

    //[RelayCommand]
    public void StartLevels()
    {
        try
        {
            //----------------------------------Если Последний уровень 0----------------------------

            if (RegmeGrid == RegimeCalcGrid.ByCountLevels)
            {
                if (Side == Side.Sell && _stepLevel>0 || Side == Side.Buy && _stepLevel < 0) _stepLevel *= -1;

                Levels = Level.CalcLevels(_startPrice, _stepLevel, _countLevels, _profit, _volume, _side);

                // Выводим Последний уровень
                PriceEnd = Levels.Count;
                if (Levels.Count > 0)
                {
                    PriceEnd = Levels[Levels.Count-1].PriceEnter;
                    //PriceEnd = Side == Side.Buy ? Levels[^1].PriceEnter : Levels[0].PriceEnter;
                }

                LoadSave.SaveParams(_startPrice, _stepLevel, _countLevels, _profit, _volume, _side, _priceEnd);
            }

            //----------------------------------Если шаг цены 0------------------------------------

            else if (RegmeGrid == RegimeCalcGrid.ByCountLevelsAndLastLevel)
            {
                int n = Extensions.GetDecimalDigitsCount(_startPrice);

                // Расчитываем шаг цены

                if (CountLevels > 1)
                {
                    StepLevel = n > 0 ? Math.Round(((StartPrice - PriceEnd) / (CountLevels - 1)), n) :
                        Math.Floor(((StartPrice - PriceEnd) / (CountLevels - 1)));
                }
                else
                {
                    _stepLevel = 0;
                }

                Levels = Level.CalcLevels(_startPrice, _stepLevel, _countLevels, _profit, _volume, (Side) _side);

                // Выводим Шаг цены
                //StepLevel = Math.Abs(_stepLevel);

                PriceEnd = Levels.Count > 0 ? Levels[Levels.Count-1].PriceEnter : 0;

                LoadSave.SaveParams(_startPrice, _stepLevel, _countLevels, _profit, _volume, _side, _priceEnd);
            }

            ////----------------------------------Если Количество уровней 0---------------------------------

            else if (RegmeGrid == RegimeCalcGrid.ByStepSize)
            {
                //_countLevels = Math.Abs(Convert.ToInt32(Math.Floor((_startPrice - _priceEnd) / _stepLevel)));
                CountLevels = (int)Math.Abs((_startPrice - _priceEnd + _stepLevel) / _stepLevel);

                if (Side == Side.Sell && _stepLevel>0)
                {
                    _stepLevel *= -1;
                }

                Levels = Level.CalcLevels(_startPrice, _stepLevel, _countLevels, _profit, _volume, _side);

                LoadSave.SaveParams(_startPrice, _stepLevel, _countLevels, _profit, _volume, _side, _priceEnd);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }     
    }


    public void TradeLogic()
    {
         
    }     


    private void LoadParams()
    {
        string _pathP = "BotMvvmTest.txt";

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
        }
        catch (Exception) { }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}


// public  Vm Instance { get; } = new();
