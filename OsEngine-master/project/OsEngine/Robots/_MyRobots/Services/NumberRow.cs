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
    public class NumberRow : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            DataGridRow row = value as DataGridRow;
            return row.GetIndex() + 1;
        }
        public object ConvertBack(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
