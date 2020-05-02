using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Converters
{
    public class NumberRangeConverter : IValueConverter
    {
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
