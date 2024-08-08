using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using GridBotMVVM.Entity;

namespace GridBotMVVM.Services;

public class NumberRow : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DataGridRow row = value as DataGridRow;
        if (row == null)
            throw new InvalidOperationException("This converter class can only be used with DataGridRow elements.");
        //var t1 = row.Item;
        //var t2 = t1.GetType


        return row.GetIndex() + 1; // Добавляем 1, потому что индексация начинается с 0
       // return row.Item;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}


//public class NumberRow : IValueConverter
//{
//    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
//    {
//        DataGridRow? row = value as DataGridRow;
//        return row!.GetIndex() + 1;
//    }
//    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
//    {
//        return null!;
//    }
//}

//public class NumberRow : IValueConverter
//{
//    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//    {
//        DataGridRow row = value as DataGridRow;
//        if (row == null)
//            throw new InvalidOperationException("This converter class can only be used with DataGridRow elements.");

//        return row.GetIndex() + 1; // Добавляем 1, потому что индексация начинается с 0
//    }

//    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//    {
//        throw new NotSupportedException();
//    }
//}