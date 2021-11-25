using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleBrowser.Helpers
{
    public class IsAdditionColumnVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isLast = (bool)value;
            double result;
            if(isLast)
            {
                result = 30;
            }
            else
            {
                result = 0;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var tabWidth = (int)value;
            var tabCount = 1000 / tabWidth;
            return tabCount;
        }
    }
}
