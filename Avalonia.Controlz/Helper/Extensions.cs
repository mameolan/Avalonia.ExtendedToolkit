using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using System.Collections.Generic;
using System.Linq;

namespace Avalonia.Controlz.Helper
{
    /// <summary>
    /// extensions for logical / visual tree
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// returns the parent of T or default(T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static T FindParent<T>(this IControl control) where T : IControl
        {
            if (control.Parent is T)
                return (T)control.Parent;

            IControl parent = control.Parent.Parent;

            while (parent != null)
            {
                if (parent is T)
                    return (T)parent;

                parent = parent.Parent;
            }
            return default(T);
        }

        /// <summary>
        /// searches for child control of T
        /// if forceUsingTheVisualTreeHelper is set to true
        /// the visual tree is used
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="forceUsingTheVisualTreeHelper"></param>
        /// <returns></returns>
        public static T FindChild<T>(this IControl control,bool forceUsingTheVisualTreeHelper = false) where T : IControl
        {
            //if (control.Parent is T)
            //    return (T)control.Parent;

            //IControl parent = control.Parent.Parent;

            //while (parent != null)
            //{
            //    if (parent is T)
            //        return (T)parent;

            //    parent = parent.Parent;
            //}
            return control.GetChildObjects(forceUsingTheVisualTreeHelper).OfType<T>().FirstOrDefault();

            //return default(T);
        }

        /// <summary>
        /// returns an IEnumerable of child controls
        /// if forceUsingTheVisualTreeHelper is set to true
        /// the visual tree is used
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="forceUsingTheVisualTreeHelper"></param>
        /// <returns></returns>
        public static IEnumerable<IControl> GetChildObjects(this IControl parent, bool forceUsingTheVisualTreeHelper = false)
        {
            if (parent == null) yield break;

            ILogical parentLogical = parent as ILogical;

            if (!forceUsingTheVisualTreeHelper && (parentLogical != null))
            {
                foreach (var item in LogicalExtensions.GetLogicalChildren(parentLogical))
                {
                    IControl avalonia = item as IControl;
                    if (avalonia != null)
                        yield return item as IControl;
                }
            }
            else
            {
                IVisual visual = parent as IVisual;

                foreach (var item in VisualTree.VisualExtensions.GetVisualChildren(visual))
                {
                    IControl avalonia = item as IControl;
                    if (avalonia != null)
                        yield return item as IControl;
                }
            }
        }

        /// <summary>
        /// returns an IEnumerable of children by T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="forceUsingTheVisualTreeHelper"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindChildren<T>(this IControl source, bool forceUsingTheVisualTreeHelper = false) where T : IControl
        {
            if (source != null)
            {
                var childs = GetChildObjects(source, forceUsingTheVisualTreeHelper);
                foreach (IControl child in childs)
                {
                    //analyze if children match the requested type
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    //recurse tree
                    foreach (T descendant in FindChildren<T>(child, forceUsingTheVisualTreeHelper))
                    {
                        yield return descendant;
                    }
                }
            }
        }

        /// <summary>
        /// gets an IEnumerable of visual parents
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public static IEnumerable<IVisual> GetAncestors(this IVisual child)
        {
            IVisual parent = VisualTree.VisualExtensions.GetVisualParent(child);
            while (parent != null)
            {
                yield return parent;
                parent = VisualTree.VisualExtensions.GetVisualParent(parent);
            }
        }


#warning this does not work right now, avalonia throws an exception while using this attacheproperty in a style
        /// <summary>
        /// attache property of classes
        /// </summary>
        public static readonly AttachedProperty<Classes> ClassesProperty =
            AvaloniaProperty.RegisterAttached<IStyledElement, Classes>(nameof(Classes), typeof(Extensions));

        /// <summary>
        /// Get classes
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Classes GetClasses(IStyledElement element)
        {
            return element.GetValue(ClassesProperty);
        }

        /// <summary>
        /// set classes
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetClasses(IStyledElement element, Classes value)
        {
            element.SetValue(ClassesProperty, value);
            OnClassesChanged(element, value);
        }

        private static void OnClassesChanged(IStyledElement element, Classes value)
        {
            if (value != null)
            {
                element.Classes = value;
            }
        }
    }
}
