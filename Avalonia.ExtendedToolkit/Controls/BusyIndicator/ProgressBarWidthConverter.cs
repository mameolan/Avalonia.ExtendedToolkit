using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class ProgressBarWidthConverter : IMultiValueConverter
    {
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            double contentWidth=0;
            double parentMinWidth = 0;
            double.TryParse(values[0].ToString(), out contentWidth);
            double.TryParse(values[1].ToString(), out parentMinWidth);

            return Math.Max(contentWidth, parentMinWidth);
        }
    }
}