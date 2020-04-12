using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit.Controls.PropertyGrid.Controls;
using Avalonia.VisualTree;

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

        public static T FindVisualChild<T>(this IVisual element) where T : class
        {
            if (element == null)
                return default(T);
            if (element is T)
                return element as T;

            var children = VisualTree.VisualExtensions.GetVisualChildren(element);

            if (children.Any())
            {
                for (var i = 0; i < children.Count(); i++)
                {
                    //object child = VisualTreeHelper.GetChild(element, i);
                    object child = children.ElementAt(i);

                    if (child is SearchTextBox)
                        continue;//speeds up things a bit
                    if (child is T)
                        return child as T;
                    if (child is IVisual)
                    {
                        var res = FindVisualChild<T>(child as IVisual);
                        if (res == null)
                            continue;
                        return res;
                    }
                }
            }
            return null;
        }

    }
}
