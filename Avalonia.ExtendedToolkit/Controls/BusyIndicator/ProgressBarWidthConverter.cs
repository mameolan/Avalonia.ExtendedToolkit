using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/xceedsoftware/wpftoolkit

    /// <summary>
    /// Multiconverter
    /// - First value contentwidth
    /// - Second value parent minwidth
    /// </summary>
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
