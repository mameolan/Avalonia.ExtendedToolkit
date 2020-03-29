using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;

namespace Avalonia.ExtendedToolkit.Extensions
{
    public static class TreeExtensions
    {
        public static T TryFindParent<T>(this IControl control) where T : IControl
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

        public static T FindChildren<T>(this IControl control) where T : IControl
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
            return control.GetChildObjects().OfType<T>().FirstOrDefault();

            //return default(T);
        }

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

                foreach(var item in VisualTree.VisualExtensions.GetVisualChildren(visual))
                {
                    IControl avalonia = item as IControl;
                    if (avalonia != null)
                        yield return item as IControl;
                }
            }
        }

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

        public static IEnumerable<IVisual> GetAncestors(this IVisual child)
        {
            IVisual parent= VisualTree.VisualExtensions.GetVisualParent(child);
            while (parent != null)
            {
                yield return parent;
                parent = VisualTree.VisualExtensions.GetVisualParent(parent);
            }
        }
    }
}
