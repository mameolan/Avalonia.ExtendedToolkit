using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.ExampleApp.Converters
{
    /// <summary>
    /// Specialized converter for keeping bindings alive.
    /// </summary>
    public class EmptyValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
