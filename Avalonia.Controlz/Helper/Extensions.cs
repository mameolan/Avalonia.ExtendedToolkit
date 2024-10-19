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
        public static T FindParent<T>(this Control control) where T : Control
        {
            if (control.Parent is T)
                return (T)control.Parent;

            Control parent = (Control)control.Parent.Parent;

            while (parent != null)
            {
                if (parent is T)
                    return (T)parent;

                parent = (Control)parent.Parent;
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
        public static T FindChild<T>(this Control control,bool forceUsingTheVisualTreeHelper = false) where T : Control
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
        public static IEnumerable<Control> GetChildObjects(this Control parent, bool forceUsingTheVisualTreeHelper = false)
        {
            if (parent == null) yield break;

            ILogical parentLogical = parent as ILogical;

            if (!forceUsingTheVisualTreeHelper && (parentLogical != null))
            {
                foreach (var item in LogicalExtensions.GetLogicalChildren(parentLogical))
                {
                    Control avalonia = item as Control;
                    if (avalonia != null)
                        yield return item as Control;
                }
            }
            else
            {
                Visual visual = parent as Visual;

                foreach (var item in VisualTree.VisualExtensions.GetVisualChildren(visual))
                {
                    Control avalonia = item as Control;
                    if (avalonia != null)
                        yield return item as Control;
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
        public static IEnumerable<T> FindChildren<T>(this Control source, bool forceUsingTheVisualTreeHelper = false) where T : Control
        {
            if (source != null)
            {
                var childs = GetChildObjects(source, forceUsingTheVisualTreeHelper);
                foreach (Control child in childs)
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
        public static IEnumerable<Visual> GetAncestors(this Visual child)
        {
            Visual parent = VisualTree.VisualExtensions.GetVisualParent(child);
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
            AvaloniaProperty.RegisterAttached<StyledElement, Classes>(nameof(Classes), typeof(Extensions));

        /// <summary>
        /// Get classes
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Classes GetClasses(StyledElement element)
        {
            return element.GetValue<Classes>(ClassesProperty);
        }

        /// <summary>
        /// set classes
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetClasses(StyledElement element, Classes value)
        {
            element.SetValue(ClassesProperty, value);
            OnClassesChanged(element, value);
        }

        private static void OnClassesChanged(StyledElement element, Classes value)
        {
            if (value != null)
            {
                element.Classes.Add(value.ToString());
            }
        }
    }
}
