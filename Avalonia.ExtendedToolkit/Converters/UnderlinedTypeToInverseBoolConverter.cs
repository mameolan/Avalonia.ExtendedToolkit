using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.ExtendedToolkit.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Avalonia.ExtendedToolkit.Converters
{
    public class UnderlinedTypeToInverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IControl control = value as IControl;
            if (control != null && parameter is UnderlinedType)
            {
                UnderlinedType underlinedTypeParam = (UnderlinedType)parameter;


                UnderlinedType underlinedType = TabControlHelper.GetUnderlined(control);
                if (underlinedType == underlinedTypeParam)
                    return false;
            }



            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
