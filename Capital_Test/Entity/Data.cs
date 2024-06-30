using Capital_Test.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capital_Test.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

// ReSharper disable InconsistentNaming

namespace Capital_Test.Entity;

    public partial class Data: ObservableObject
{
    public Data()
    {
    }

    public Data(decimal DepoStart, StrategyType strategyType)
    {
        StrategyType = strategyType;
        Depo = DepoStart;
    }

    public StrategyType StrategyType { get; set; }

    [ObservableProperty]
    private decimal _depo;
    partial void OnDepoChanged(decimal value) => ResultDepo = value;
    
    /// <summary>
    /// Результат Эквити (Депо)
    /// </summary>
    [ObservableProperty]
    private decimal _resultDepo;
    partial void OnResultDepoChanged(decimal value)
    {
        _resultDepo = Math.Round(value, 2);
        Profit = ResultDepo - Depo;
        PercentProfit = Math.Round((Profit * 100 / Depo), 2);
        ListEquity.Add(ResultDepo);
        CalcDrawDown();
    }
    
    [ObservableProperty]
    private decimal _profit;

    /// <summary>
    /// Относительный профит в процентах
    /// </summary>
    [ObservableProperty]
    private decimal _percentProfit;

    /// <summary>
    /// Максимальная абсолютная просадка в деньгах
    /// </summary>
  [ObservableProperty]
    private decimal _drawDown;
    partial void OnDrawDownChanged(decimal value)
    {
        CalcPercentDrawDawn();
    }

    /// <summary>
    /// Максимальная относительная просадка в процентах
    /// </summary>

    [ObservableProperty]
    private decimal _percentDrawDown;

    public decimal CountTake { get; set; }
    public decimal CountStop { get; set; }

    private List<decimal> ListEquity = [];
    public List<decimal> GetListEquity()
    {
        return ListEquity;
    }

    private decimal _max = 0;
    private decimal _min = 0;

    private void CalcDrawDown()
    {
        if (ResultDepo > _max)
        {
            _max = ResultDepo;
            _min = ResultDepo;
            return;
        }
        _min = ResultDepo;
        DrawDown = _max - _min;
    }

    private void CalcPercentDrawDawn()
    {
        if (ResultDepo == 0) return;
        decimal percent = DrawDown * 100 / ResultDepo;
        if (percent > PercentDrawDown) PercentDrawDown = Math.Round(percent, 2);
    }
}
