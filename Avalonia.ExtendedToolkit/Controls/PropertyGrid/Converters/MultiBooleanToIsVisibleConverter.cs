using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Converters
{
    public class MultiBooleanToIsVisibleConverter : IMultiValueConverter
    {
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.OfType<bool>().All(item => item);
        }
    }
}
