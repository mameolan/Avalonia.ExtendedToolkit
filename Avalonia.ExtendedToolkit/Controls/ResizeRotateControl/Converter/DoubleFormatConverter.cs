using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://www.codeproject.com/Articles/22952/WPF-Diagram-Designer-Part-1

    /// <summary>
    /// rounds the double value with Math.Round
    /// </summary>
    public class DoubleFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double d = (double)value;
            return Math.Round(d);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
