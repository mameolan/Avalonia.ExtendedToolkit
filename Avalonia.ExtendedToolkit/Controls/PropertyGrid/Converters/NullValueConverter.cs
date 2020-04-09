using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Converters
{
    /// <summary>
    /// Converts a null value into a string representation.
    /// </summary>
    public class NullValueConverter : IValueConverter
    {
        /// <summary>
        /// Default Null presentation. The default value is "(null)".
        /// </summary>
        public const string NullString = "(null)";

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
            if (value == null)
                return parameter ?? NullString;
            else
                return value;
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
            if (value == null)
                return null;

            string stringValue = value.ToString();
            if (string.IsNullOrEmpty(stringValue))
                return null;

            bool isNull = (parameter != null) ? stringValue == (string)parameter : stringValue == NullString;
            return (isNull) ? null : value;
        }
    }
}
