using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Controls;
using Avalonia.VisualTree;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid.Utils
{
    //
    // ported from https://github.com/DenisVuyka/WPG
    //

    internal static class PropertyGridUtils
    {
        public static IEnumerable<T> GetAttributes<T>(object target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            return GetAttributes<T>(target.GetType());
        }

        public static IEnumerable<T> GetAttributes<T>(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var attributes =
               from T attribute
                 in type.GetCustomAttributes(typeof(T), true)
               select attribute;

            return attributes;
        }

        public static T FindVisualParent<T>(this IControl element) where T : class
        {
            if (element == null)
                return default(T);
            IControl parent = element.Parent;

            if (parent is T)
                return parent as T;
            if(parent is IControl)
                return FindVisualParent<T>(parent);

            return null;
        }

        public static IEnumerable<T> FindVisualChildren<T>(this IVisual element) where T : IVisual
        {
            List<T> result = new List<T>();
            foreach(var item in VisualTree.VisualExtensions.GetVisualChildren(element))
            {
                var child= item.FindVisualChild<T>();
                if(child!=null)
                {
                    result.Add(child);
                }

                if(child?.VisualChildren.Any()==true)
                {
                    result.AddRange(FindVisualChildren<T>(child));
                }
            }
            return result;
        }

        public static T FindVisualChild<T>(this IVisual element) where T : IVisual
        {
            if (element == null)
                return default(T);
            if (element is T)
                return (T)element;

            var children = VisualTree.VisualExtensions.GetVisualChildren(element);

            if (children.Any())
            {
                for (var i = 0; i < children.Count(); i++)
                {
                    //object child = VisualTreeHelper.GetChild(element, i);
                    IVisual child = children.ElementAt(i);

                    if (child is SearchTextBox)
                        continue;//speeds up things a bit
                    if (child is T)
                        return (T)child;
                    if (child is IVisual)
                    {
                        var res = FindVisualChild<T>(child as IVisual);
                        if (res == null)
                            continue;
                        return res;
                    }
                }
            }
            return default(T);
        }
    }
}
