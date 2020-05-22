using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Converters
{
    /// <summary>
    /// tries to convert a double into a <see cref="GridLength"/>
    /// </summary>
    public class DoubleToGridLengthConverter : IValueConverter
    {
        /// <summary>
        /// if value is double return gridlenth with double value
        /// else return empty gridlength
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
                return new GridLength((double)value);
            }

            return new GridLength();
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
