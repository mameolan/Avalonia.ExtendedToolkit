using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Converters
{
    /// <summary>
    /// tries to convert a double into a thickness value
    /// </summary>
    public class DoubleToThicknessConverter : IValueConverter
    {
        /// <summary>
        /// if value is double thicknes with double value is returned
        /// else if value is thicknes and is uniform the top is returned
        ///      if not unimormed a sum is returned
        /// else empty thicknes is returned
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
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

        /// <summary>
        /// does nothing
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
