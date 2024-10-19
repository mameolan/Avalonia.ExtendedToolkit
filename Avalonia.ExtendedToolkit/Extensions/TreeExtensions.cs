using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using Avalonia.Visuals;

namespace Avalonia.ExtendedToolkit.Extensions
{
    /// <summary>
    /// visual/logical tree extensions
    /// </summary>
    public static class TreeExtensions
    {
        /// <summary>
        /// (from avalonia master)
        /// Gets a transform from an ancestor to a descendent.
        /// </summary>
        /// <param name="ancestor">The ancestor visual.</param>
        /// <param name="visual">The visual.</param>
        /// <returns>The transform.</returns>
        public static Matrix GetOffsetFrom(Visual ancestor, Visual visual)
        {
            var result = Matrix.Identity;

            while (visual != ancestor)
            {
                if (visual.RenderTransform?.Value != null)
                {
                    var origin = visual.RenderTransformOrigin.ToPixels(visual.Bounds.Size);
                    var offset = Matrix.CreateTranslation(origin);
                    var renderTransform = (-offset) * visual.RenderTransform.Value * (offset);

                    result *= renderTransform;
                }

                var topLeft = visual.Bounds.TopLeft;

                if (topLeft != default)
                {
                    result *= Matrix.CreateTranslation(topLeft);
                }
                //.edited
                visual = (Visual)visual.Parent;

                if (visual == null)
                {
                    throw new ArgumentException("'visual' is not a descendant of 'ancestor'.");
                }
            }

            return result;
        }

        /// <summary>
        /// Transforms the specified point and 
        /// the give matrix
        /// to the resulting point
        /// </summary>
        public static Point Transform(this Matrix matrix, Point point)
        {
            //calculation taken from MonoGame.Extended/Math/Matrix2.cs
            double x = point.X * matrix.M11 + point.Y * matrix.M21 + matrix.M31;
            double y = point.X * matrix.M12 + point.Y * matrix.M22 + matrix.M32;
            return new Point(x, y);
        }

        /// <summary>
        /// tries to find the parent by type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static T TryFindParent<T>(this Control control) where T : Control
        {
            if (control is T)
                return (T)control;

            if (control.TemplatedParent is T)
            {
                return (T)control.TemplatedParent;
            }




            if (control is ContentControl
                && ((ContentControl)control).Content is T)
            {
                return (T)((ContentControl)control).Content;
            }

            if (control.Parent is T)
                return (T)control.Parent;

            Control parent = (Control)control.Parent?.Parent;

            while (parent != null)
            {
                if (parent is T)
                {
                    return (T)parent;
                }

                T result = TryFindParent<T>(parent);

                if (result is T)
                {
                    return result;
                }
                //.edited
                parent = (Control)parent.Parent;
            }
            return default(T);
        }

        /// <summary>
        /// tries to find the child by type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static T FindChildren<T>(this Control control) where T : Control
        {
            //if (control.Parent is T)
            //    return (T)control.Parent;

            //Control parent = control.Parent.Parent;

            //while (parent != null)
            //{
            //    if (parent is T)
            //        return (T)parent;

            //    parent = parent.Parent;
            //}
            return control.GetChildObjects().OfType<T>().FirstOrDefault();

            //return default(T);
        }


        /// <summary>
        /// returns the children by control
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="forceUsingTheVisualTreeHelper"></param>
        /// <returns></returns>
        public static IEnumerable<Control> GetChildObjects(this Control parent, bool forceUsingTheVisualTreeHelper = false)
        {
            if (parent == null)
                yield break;

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
        /// try to find children by T
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
        /// tries to find the visual parent
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
    }
}
