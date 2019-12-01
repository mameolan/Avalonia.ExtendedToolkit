using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.ExtendedToolkit.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Avalonia.ExtendedToolkit.Converters
{
    public class UnderlinedTypeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IControl control = value as IControl;
            if(control!=null)
            {
               UnderlinedType underlinedType= TabControlHelper.GetUnderlined(control);
                if (underlinedType == UnderlinedType.TabPanel)
                    return true;
            }
            


            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
