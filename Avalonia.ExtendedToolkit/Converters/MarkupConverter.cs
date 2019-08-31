using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Avalonia.ExtendedToolkit.Converters
{
    public abstract class MarkupConverter : MarkupExtension, IValueConverter
    {
        protected abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        protected abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);



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

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
