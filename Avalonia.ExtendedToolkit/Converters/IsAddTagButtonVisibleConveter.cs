using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Avalonia.Data.Converters;
using Avalonia.ExtendedToolkit.Controls;

namespace Avalonia.ExtendedToolkit.Converters
{
    /// <summary>
    /// converter for showing the AddTagbutton in the <see cref="TagControl"/>
    /// </summary>
    public class IsAddTagButtonVisibleConveter : IMultiValueConverter
    {
        /// <summary>
        /// return true if showaddtagbutton is true and control is enable
        /// </summary>
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            bool? showAddTagButton = values.FirstOrDefault() as bool?;
            bool? isEnabled = values.LastOrDefault() as bool?;

            if(showAddTagButton.HasValue&& showAddTagButton==true
                && isEnabled.HasValue&& isEnabled==true)
            {
                return true;
            }
            return false;
        }
    }
}
