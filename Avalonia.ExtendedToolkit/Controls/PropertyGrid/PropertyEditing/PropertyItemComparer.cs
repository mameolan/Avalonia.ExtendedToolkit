using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    /// <summary>
    /// Default <see cref="PropertyItem"/> comparer.
    /// </summary>
    public class PropertyItemComparer : IComparer<PropertyItem>
    {
        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// Value
        /// Condition
        /// Less than zero
        /// <paramref name="x"/> is less than <paramref name="y"/>.
        /// Zero
        /// <paramref name="x"/> equals <paramref name="y"/>.
        /// Greater than zero
        /// <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </returns>
        public virtual int Compare(PropertyItem x, PropertyItem y)
        {
            if (x == y)
                return 0;
            if (x == null)
                return -1;
            if (y == null)
                return 1;

            int leftOrder = 0;

            var attribute = x.GetAttribute<PropertyOrderAttribute>();
            if (attribute != null)
                leftOrder = attribute.Order;

            int rightOrder = 0;

            attribute = y.GetAttribute<PropertyOrderAttribute>();
            if (attribute != null)
                rightOrder = attribute.Order;

            int num = (leftOrder == rightOrder) ? 0 : leftOrder.CompareTo(rightOrder);
            if (num != 0)
                return num;

            return string.Compare(x.Name, y.Name, true);
        }
    }
}
