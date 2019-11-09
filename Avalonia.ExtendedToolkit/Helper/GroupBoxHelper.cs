using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public static class GroupBoxHelper
    {
        public static readonly AttachedProperty<IBrush> HeaderForegroundProperty =
        AvaloniaProperty.RegisterAttached<IControl, IBrush>("HeaderForeground", typeof(GroupBoxHelper));

        public static IBrush GetHeaderForeground(IControl element)
        {
            return element.GetValue(HeaderForegroundProperty);
        }

        public static void SetHeaderForeground(IControl element, IBrush value)
        {
            element.SetValue(HeaderForegroundProperty, value);
        }


    }
}
