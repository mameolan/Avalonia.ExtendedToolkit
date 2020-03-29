using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Converters
{
    public class ThicknessToDoubleConverter : IValueConverter
    {
        public ThicknessSideType TakeThicknessSide { get; set; }

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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
