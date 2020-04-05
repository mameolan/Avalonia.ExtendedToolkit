using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    internal static class PropertyGridUtils
    {
        public static IEnumerable<T> GetAttributes<T>(object target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return GetAttributes<T>(target.GetType());
        }

        public static IEnumerable<T> GetAttributes<T>(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            var attributes =
               from T attribute
                 in type.GetCustomAttributes(typeof(T), true)
               select attribute;

            return attributes;
        }
    }
}
