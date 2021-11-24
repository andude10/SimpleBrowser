using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBrowser.Helpers
{
    public class CountToWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var tabCount = (int)value;
            var tabWidth = 1000 / tabCount;
            return tabWidth;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var tabWidth = (int)value;
            var tabCount = 1000 / tabWidth;
            return tabCount;
        }
    }
}
