using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Converters
{
    /// <summary>
    /// Converts a Thickness to a new Thickness. It's possible to ignore a side With the IgnoreThicknessSide property.
    /// </summary>
    /// </summary>
    public class ThicknessBindingConverter : IValueConverter
    {
        /// <summary>
        /// get/sets IgnoreThicknessSide
        /// </summary>
        public ThicknessSideType IgnoreThicknessSide { get; set; }

        /// <summary>
        /// returns the thickness by <see cref="IgnoreThicknessSide"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Thickness)
            {
                // yes, we can override it with the parameter value
                if (parameter is ThicknessSideType)
                {
                    this.IgnoreThicknessSide = (ThicknessSideType)parameter;
                }
                var orgThickness = (Thickness)value;
                switch (this.IgnoreThicknessSide)
                {
                    case ThicknessSideType.Left:
                        return new Thickness(0, orgThickness.Top, orgThickness.Right, orgThickness.Bottom);

                    case ThicknessSideType.Top:
                        return new Thickness(orgThickness.Left, 0, orgThickness.Right, orgThickness.Bottom);

                    case ThicknessSideType.Right:
                        return new Thickness(orgThickness.Left, orgThickness.Top, 0, orgThickness.Bottom);

                    case ThicknessSideType.Bottom:
                        return new Thickness(orgThickness.Left, orgThickness.Top, orgThickness.Right, 0);

                    default:
                        return orgThickness;
                }
            }
            return default(Thickness);
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
