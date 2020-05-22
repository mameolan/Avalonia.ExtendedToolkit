using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Converters
{
    /// <summary>
    /// tries to convert the thicknes to double
    /// </summary>
    public class ThicknessToDoubleConverter : IValueConverter
    {
        /// <summary>
        /// TakeThicknessSide
        /// </summary>
        public ThicknessSideType TakeThicknessSide { get; set; }

        /// <summary>
        /// tries to convert by <see cref="TakeThicknessSide"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool  isGridTarget = targetType.Name.Contains(nameof(Grid));

            if (value is Thickness)
            {
                // yes, we can override it with the parameter value
                if (parameter is ThicknessSideType)
                {
                    this.TakeThicknessSide = (ThicknessSideType)parameter;
                }
                var orgThickness = (Thickness)value;
                switch (this.TakeThicknessSide)
                {
                    case ThicknessSideType.Left:
                        if(isGridTarget)
                        {
                            return new GridLength(orgThickness.Left, GridUnitType.Pixel);
                        }
                        else
                        {
                            return orgThickness.Left;
                        }

                    case ThicknessSideType.Top:
                        if (isGridTarget)
                        {
                            return new GridLength(orgThickness.Top, GridUnitType.Pixel);
                        }
                        else
                        {
                            return orgThickness.Top;
                        }

                    case ThicknessSideType.Right:
                        if (isGridTarget)
                        {
                            return new GridLength(orgThickness.Right, GridUnitType.Pixel);
                        }
                        else
                        {
                            return orgThickness.Right;
                        }

                    case ThicknessSideType.Bottom:
                        if (isGridTarget)
                        {
                            return new GridLength(orgThickness.Bottom, GridUnitType.Pixel);
                        }
                        else
                        {
                            return orgThickness.Bottom;
                        }

                    default:
                        if (isGridTarget)
                        {
                            return new GridLength(0, GridUnitType.Pixel);
                        }
                        else
                        {
                            return 0;
                        }
                }
            }

            if (isGridTarget)
            {
                return new GridLength(0, GridUnitType.Pixel);
            }
            else
            {
                return 0;
            }
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
