using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Converters
{
    public class GridEntryToIsVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PropertyItem)
            {
                PropertyItem propertyItem = value as PropertyItem;
                return propertyItem.IsBrowsable && propertyItem.MatchesFilter && propertyItem.IsReadOnly == false;
            }
            else if (value is CategoryItem)
            {
                CategoryItem categoryItem = value as CategoryItem;
                return categoryItem.IsBrowsable && categoryItem.MatchesFilter && categoryItem.HasVisibleProperties;
            }
            else
            {

            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
