using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace OsEngine.Robots._MyRobots.Services
{
    public class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is DateTime))
                throw new ArgumentException("Value must be of type DateTime.");

            DateTime dateTime = (DateTime) value;
            string format = parameter as string ?? "dd.MM.yyyy"; // Используем точку в качестве разделителя
            return dateTime.ToString(format, new CultureInfo("ru-RU"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
                throw new ArgumentException("Value must be of type string.");

            string dateString = (string) value;
            string format = parameter as string ?? "dd.MM.yyyy"; // Используем тот же формат для преобразования обратно
            return DateTime.ParseExact(dateString, format, new CultureInfo("ru-RU"), DateTimeStyles.None);
        }
    }
}
