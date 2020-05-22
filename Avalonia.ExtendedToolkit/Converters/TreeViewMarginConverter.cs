using System;
using System.Globalization;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.ExtendedToolkit.Extensions;

namespace Avalonia.ExtendedToolkit.Converters
{
    /// <summary>
    /// tries to convert treeview item to a thicknes value
    /// </summary>
    public class TreeViewMarginConverter : IValueConverter
    {
        /// <summary>
        /// get/sets the length
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// if value is not a <see cref="TreeViewItem"/> zero thickness is returned
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as TreeViewItem;
            if (item == null)
                return new Thickness(0);

            return new Thickness(Length * item.GetDepth(), 0, 0, 0);
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

    /// <summary>
    /// treeview item extension class
    /// </summary>
    public static class TreeViewItemExtensions
    {
        /// <summary>
        /// gets the depth from the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int GetDepth(this TreeViewItem item)
        {
            return item.GetAncestors().TakeWhile(e => !(e is TreeView)).OfType<TreeViewItem>().Count();
        }
    }
}
