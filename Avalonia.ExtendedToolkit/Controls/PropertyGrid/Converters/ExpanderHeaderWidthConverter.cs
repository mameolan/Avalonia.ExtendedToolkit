using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Converters
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// Expander header width converter
    /// </summary>
    // For more details see http://joshsmithonwpf.wordpress.com/2007/02/24/stretching-content-in-an-expander-header/
    public sealed class ExpanderHeaderWidthConverter : IValueConverter
    {
        /// <summary>
        /// Specifies the default offset that should be used in case no parameter is provided. By default is -25.0
        /// </summary>
        public const double DefaultOffset = -25.0;

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double width = (double)value;
            double diff = DefaultOffset;
            if (parameter != null && double.TryParse(parameter.ToString(), out diff))
                return width + diff;

            return width + DefaultOffset;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double width = (double)value;
            double diff = DefaultOffset;
            if (parameter != null && double.TryParse(parameter.ToString(), out diff))
                return width - diff;

            return width - DefaultOffset;
        }
    }
}
