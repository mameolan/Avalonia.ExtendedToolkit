using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// transformed the items to a <see cref="IndexItemModel"/> list
    /// </summary>
    public class DataContextToIndexItemConverter : IValueConverter
    {
        /// <summary>
        /// if the value is IEnumerable of objects
        /// and the parameter is a string and the collection contains
        /// a property with the parameter name, the collection
        /// is transformed into <see cref="IndexItemModel"/> list
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<object> items
                && parameter is string propertyName)
            {
                List<IndexItemModel> result = new List<IndexItemModel>();

                foreach (var item in items)
                {
                    string val = item.GetType().GetProperty(propertyName)?.GetValue(item) as string;
                    if (string.IsNullOrEmpty(val) == false)
                    {
                        result.Add(new IndexItemModel { Text = val, ItemData = item });
                    }
                }
                return result;
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