using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Converters
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    /// <summary>
    /// convert to display an editor or not
    /// </summary>
    public class MultiBooleanToIsVisibleConverter : IMultiValueConverter
    {
        /// <summary>
        /// return true if every values are true else false
        /// if there are some invalid binding error type return true
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            //on invalid type return true
            if (values.Any(x => (x is bool)==false))
            {
                return true;
            }

            return values?.OfType<bool>().All(item => item);
        }
    }
}
