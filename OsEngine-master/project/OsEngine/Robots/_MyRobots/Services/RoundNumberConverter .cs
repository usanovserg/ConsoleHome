using System;
using System.Globalization;
using System.Windows.Data;

namespace OsEngine.Robots._MyRobots.Services;

public class RoundNumberConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not decimal)
            throw new ArgumentException("Value must be of type decimal");

        int decimalPlaces = parameter != null ? int.Parse(parameter.ToString()) : 0;
        return Math.Round((decimal)value, decimalPlaces);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException(); // Не реализован обратный конверт
    }
}
