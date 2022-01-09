using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;

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
        public static Matrix GetOffsetFrom(IVisual ancestor, IVisual visual)
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

                visual = visual.VisualParent;

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
        public static T TryFindParent<T>(this IControl control) where T : IControl
        {
            if (control is T)
                return (T)control;

            if (control.TemplatedParent is T)
            {
                return (T)control.TemplatedParent;
            }




            if (control is IContentControl
                && ((IContentControl)control).Content is T)
            {
                return (T)((IContentControl)control).Content;
            }

            if (control.Parent is T)
                return (T)control.Parent;

            IControl parent = control.Parent?.Parent;

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

                parent = parent.Parent;
            }
            return default(T);
        }

        /// <summary>
        /// tries to find the child by type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
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


        /// <summary>
        /// returns the children by control
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="forceUsingTheVisualTreeHelper"></param>
        /// <returns></returns>
        public static IEnumerable<IControl> GetChildObjects(this IControl parent, bool forceUsingTheVisualTreeHelper = false)
        {
            if (parent == null)
                yield break;

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
        /// try to find children by T
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
        /// tries to find the visual parent
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
    }
}
