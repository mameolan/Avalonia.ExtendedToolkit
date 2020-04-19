using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Avalonia.ExampleApp.Converters
{
    public class FontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fw = (FontWeight)value;
            return fw == FontWeight.Bold;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                bool isSet = (bool)value;

                if (isSet)
                {
                    return FontWeight.Bold;
                }
            }

            return FontWeight.Normal;
        }
    }
}
