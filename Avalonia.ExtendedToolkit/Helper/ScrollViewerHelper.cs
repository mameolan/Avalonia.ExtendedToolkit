using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public static class ScrollViewerHelper
    {


        public static readonly AttachedProperty<bool> VerticalScrollBarOnLeftSideProperty =
            AvaloniaProperty.RegisterAttached<IControl, bool>
            ("VerticalScrollBarOnLeftSide", typeof(ScrollViewerHelper));

        public static bool GetVerticalScrollBarOnLeftSide(IControl element)
        {
            return element.GetValue(VerticalScrollBarOnLeftSideProperty);
        }

        public static void SetVerticalScrollBarOnLeftSide(IControl element, bool value)
        {
            element.SetValue(VerticalScrollBarOnLeftSideProperty, value);
        }


    }
}
