using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public static class ExpanderHelper
    {
        public static readonly AttachedProperty<string> HeaderUpClassProperty =
                    AvaloniaProperty.RegisterAttached<IControl, string>("HeaderUpClass", typeof(ExpanderHelper));

        public static string GetHeaderUpClass(IControl element)
        {
            return element.GetValue(HeaderUpClassProperty);
        }

        public static void SetHeaderUpClass(IControl element, string value)
        {
            element.SetValue(HeaderUpClassProperty, value);
        }

        public static readonly AttachedProperty<string> HeaderDownClassProperty =
                    AvaloniaProperty.RegisterAttached<IControl, string>("HeaderDownClass", typeof(ExpanderHelper));

        public static string GetHeaderDownClass(IControl element)
        {
            return element.GetValue(HeaderDownClassProperty);
        }

        public static void SetHeaderDownClass(IControl element, string value)
        {
            element.SetValue(HeaderDownClassProperty, value);
        }

        public static readonly AttachedProperty<string> HeaderLeftClassProperty =
                    AvaloniaProperty.RegisterAttached<IControl, string>("HeaderLeftClass", typeof(ExpanderHelper));

        public static string GetHeaderLeftClass(IControl element)
        {
            return element.GetValue(HeaderLeftClassProperty);
        }

        public static void SetHeaderLeftClass(IControl element, string value)
        {
            element.SetValue(HeaderLeftClassProperty, value);
        }

        public static readonly AttachedProperty<string> HeaderRightClassProperty =
                    AvaloniaProperty.RegisterAttached<IControl, string>("HeaderRightClass", typeof(ExpanderHelper));

        public static string GetHeaderRightClass(IControl element)
        {
            return element.GetValue(HeaderRightClassProperty);
        }

        public static void SetHeaderRightClass(IControl element, string value)
        {
            element.SetValue(HeaderRightClassProperty, value);
        }


    }
}
