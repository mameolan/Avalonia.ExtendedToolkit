using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// ScrollViewer attached properties
    /// </summary>
    public static class ScrollViewerHelper
    {
        /// <summary>
        /// AttachedProperty VerticalScrollBarOnLeftSide
        /// </summary>
        public static readonly AttachedProperty<bool> VerticalScrollBarOnLeftSideProperty =
            AvaloniaProperty.RegisterAttached<IControl, bool>
            ("VerticalScrollBarOnLeftSide", typeof(ScrollViewerHelper));

        /// <summary>
        /// get VerticalScrollBarOnLeftSide
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetVerticalScrollBarOnLeftSide(IControl element)
        {
            return element.GetValue(VerticalScrollBarOnLeftSideProperty);
        }

        /// <summary>
        /// set VerticalScrollBarOnLeftSide
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetVerticalScrollBarOnLeftSide(IControl element, bool value)
        {
            element.SetValue(VerticalScrollBarOnLeftSideProperty, value);
        }
    }
}
