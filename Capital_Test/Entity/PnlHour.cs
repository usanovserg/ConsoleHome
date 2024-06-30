using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Media;
using Capital_Test.ViewModels;
using DevExpress.Mvvm.POCO;

//using static Capital_Test.Entity.PnlHour;
// ReSharper disable ArrangeAccessorOwnerBody

namespace Capital_Test.Entity;

public partial class PnlHour : ObservableRecipient
{
    public PnlHour()
    {
    }

    Binding bind = new Binding();
    private decimal _pnl = 0;

    [ObservableProperty]
    private bool _isActiv = true;
    partial void OnIsActivChanged(bool value)
    {
        EventCheckChanged?.Invoke();
    }

    //private bool _isActiv = true;
    //public bool IsActiv
    //{
    //    get => _isActiv;

    //    set
    //    {
    //        _isActiv = value;
    //        EventCheckChanged?.Invoke();
    //        OnPropertyChanged(nameof(IsActiv));
    //    }
    //}



    [ObservableProperty] private int _hour = 0;
    [ObservableProperty] private decimal _value = 0;
    [ObservableProperty] private decimal _maximum = 0;
    [ObservableProperty] private SolidColorBrush _color = Brushes.Transparent;

    public decimal Pnl
    {
        get => _pnl;
        set
        {
            _pnl = value;
            Value = Math.Abs(_pnl);

            Color = Pnl > 0 ? Brushes.Green : Brushes.Red;
        }
    }

    //[RelayCommand]
    //public bool Act(bool x, Func<bool, bool> t)
    //{
    //   return t(x);
    //}

    public delegate void eventCheckChanged();
    public event eventCheckChanged? EventCheckChanged;
}
