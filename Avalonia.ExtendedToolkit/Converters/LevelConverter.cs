using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Converters
{
    /// <summary>
    /// Return the width multiplied by the level
    /// </summary>
    public class LevelConverter : IValueConverter
    {
        /// <summary>
        /// gridlength as levelwidth
        /// </summary>
        public GridLength LevelWidth { get; set; }

        /// <summary>
        /// Return the width multiplied by the level
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result= ((int)value * LevelWidth.Value);

            // Return the width multiplied by the level
            return new GridLength(result);
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
