using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace WPF_MVVM.ViewModels;

public class BaseVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged /*= delegate { }*/;

    protected virtual void OnPropertyChanged(string prop)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    protected virtual bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}