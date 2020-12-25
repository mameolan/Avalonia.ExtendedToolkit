using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Controls.ChildWindowConverter
{
    //ported from: https://github.com/punker76/MahApps.Metro.SimpleChildWindow/ 

    /// <summary>
    /// returns the correct TrueValue or FalseValue
    /// </summary>
    public class EnumToIsVisibileConverter : IValueConverter
    {
        public bool TrueValue { get; set; } = true;
        public bool FalseValue { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                if (value == null)
                {
                    return this.FalseValue;
                }
                else
                {
                    var equals = Equals(value, parameter);
                    return equals ? this.TrueValue : this.FalseValue;
                }
            }
            return AvaloniaProperty.UnsetValue;
        }

        /// <summary>
        /// does nothing
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
