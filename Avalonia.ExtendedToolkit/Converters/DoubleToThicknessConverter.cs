using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Avalonia.ExtendedToolkit.Converters
{
    public class DoubleToThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double)
            {
                return new Thickness((double)value);
            }
            if(value is Thickness)
            {
                Thickness thickness = (Thickness)value;

                var sum = thickness.Top + thickness.Right + thickness.Bottom + thickness.Left;

                return thickness.IsUniform ? thickness.Top:sum;
            }

            return new Thickness();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
