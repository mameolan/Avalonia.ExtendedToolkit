using System;
using System.Globalization;

namespace Avalonia.ExtendedToolkit.Converters
{
    /// <summary>
    /// tries to convert the string value to lower
    /// </summary>
    public class ToLowerConverter : MarkupConverter
    {
        private static ToLowerConverter _instance;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static ToLowerConverter()
        {
        }

        /// <summary>
        /// returns the instance
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new ToLowerConverter());
        }

        /// <summary>
        /// tries to make the string to lower
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as string;
            return val != null ? val.ToLower() : value;
        }

        /// <summary>
        /// does nothing
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
