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

    /// <summary>
    /// property grid utils
    /// </summary>
    internal static class PropertyGridUtils
    {
        /// <summary>
        /// gets the attributes from target
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetAttributes<T>(object target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            return GetAttributes<T>(target.GetType());
        }

        /// <summary>
        /// gets the attributes from type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
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

        /// <summary>
        /// tries to find the visual parent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T FindVisualParent<T>(this Control element) where T : class
        {
            if (element == null)
                return default(T);
            Control parent = (Control)element.Parent;

            if (parent is T)
                return parent as T;
            if(parent is Control)
                return FindVisualParent<T>(parent);

            return null;
        }

        /// <summary>
        /// tries to find visual children
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindVisualChildren<T>(this Visual element) where T : Visual
        {
            List<T> result = new List<T>();
            foreach(var item in VisualTree.VisualExtensions.GetVisualChildren(element))
            {
                var child= item.FindVisualChild<T>();
                if(child!=null)
                {
                    result.Add(child);
                }
                //.edited
                if(child?.GetVisualChildren().Any()==true)
                {
                    result.AddRange(FindVisualChildren<T>(child));
                }
            }
            return result;
        }

        /// <summary>
        /// tries to find visual child
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T FindVisualChild<T>(this Visual element) where T : Visual
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
                    Visual child = children.ElementAt(i);

                    if (child is SearchTextBox)
                        continue;//speeds up things a bit
                    if (child is T)
                        return (T)child;
                    if (child is Visual)
                    {
                        var res = FindVisualChild<T>(child as Visual);
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
