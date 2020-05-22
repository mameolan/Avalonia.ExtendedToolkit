using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Converters
{
    /// <summary>
    /// number range converter
    /// </summary>
    public class NumberRangeConverter : IValueConverter
    {
        /// <summary>
        /// if value is PropertyItem and parameter is NumberRangeType
        /// return the values by NumberRangeType
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PropertyItem propertyItem = value as PropertyItem;

            if (propertyItem != null && parameter is NumberRangeType)
            {
                NumberRangeType rangeType = (NumberRangeType)parameter;

                //just "NumberRange"
                string metadataName = nameof(NumberRangeAttribute).Replace(nameof(Attribute), string.Empty);

                NumberRangeAttribute rangeAttribute = propertyItem.Metadata[metadataName] as NumberRangeAttribute;
                if (rangeAttribute != null)
                {
                    switch (rangeType)
                    {
                        case NumberRangeType.Minimum:
                            return rangeAttribute.Minimum;

                        case NumberRangeType.Maximum:
                            return rangeAttribute.Maximum;

                        case NumberRangeType.Tick:
                            return rangeAttribute.Tick;

                        case NumberRangeType.Precision:
                            return rangeAttribute.Precision;
                    }
                }
            }

            return value;
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
