using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// if list count is zero and bool value is false return false
    /// else true
    /// </summary>
    public class ShowEmptyItemsConverter : IMultiValueConverter
    {
        /// <summary>
        /// if the first value is IEnumable amd second value is bool
        /// and count is zero and the bool value is false the result is false
        /// else the result is true
        /// /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<object> items = values.FirstOrDefault() as IEnumerable<object>;
            bool? showEmptyItems = values.LastOrDefault() as bool?;

            if (items != null && showEmptyItems.HasValue)
            {
                if (items.Count() == 0 && showEmptyItems == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return true;
        }
    }
}
