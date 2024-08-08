using System;
using System.Globalization;
using System.Windows.Data;
using OsEngine.Entity;

namespace OsEngine.Robots._MyRobots.Services;

public class VolumeSelectorConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 3) return 0.ToString();
        var state = values[0]?.ToString();

        switch (state)
        {
            case "Open":
                // Открываемая позиция
                return values[1].ToString();
            case "Opening":
                // Закрытая позиция
                return values[2].ToString();
            default:
                // По умолчанию возвращаем OpeningVolume
                return 0.ToString();
        }

        //var position = values[1] as Position; // Предполагается, что первый аргумент - это позиция
        //if (position is {State: PositionStateType.Open}) // Предполагается, что у позиции есть свойство IsOrderCompleted
        //{
        //    return position.MaxVolume;
        //}
        //else if (position is {State: PositionStateType.Opening})
        //{
        //    return position.WaitVolume;
        //}

        //return null;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}