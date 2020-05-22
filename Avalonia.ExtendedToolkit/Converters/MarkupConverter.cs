using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;

namespace Avalonia.ExtendedToolkit.Converters
{
    /// <summary>
    /// markup extension for <see cref="IValueConverter"/>
    /// </summary>
    public abstract class MarkupConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// abstract convert function from <see cref="IValueConverter"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        protected abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// abstract convertback function from <see cref="IValueConverter"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        protected abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// tries to call convert function
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return Convert(value, targetType, parameter, culture);
            }
            catch
            {
                return AvaloniaProperty.UnsetValue;
            }
        }

        /// <summary>
        /// tries to call convertback function
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ConvertBack(value, targetType, parameter, culture);
            }
            catch
            {
                return AvaloniaProperty.UnsetValue;
            }
        }

        /// <summary>
        /// return this
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
