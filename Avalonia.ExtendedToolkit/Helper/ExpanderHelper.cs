using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// Expander attached properties
    /// </summary>
    public static class ExpanderHelper
    {
        /// <summary>
        /// HeaderUpClass AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<string> HeaderUpClassProperty =
                    AvaloniaProperty.RegisterAttached<Control, string>("HeaderUpClass", typeof(ExpanderHelper));

        /// <summary>
        /// get HeaderUpClass
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetHeaderUpClass(Control element)
        {
            return element.GetValue<string>(HeaderUpClassProperty);
        }

        /// <summary>
        /// set HeaderUpClass
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderUpClass(Control element, string value)
        {
            element.SetValue(HeaderUpClassProperty, value);
        }

        /// <summary>
        /// HeaderDownClass AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<string> HeaderDownClassProperty =
                    AvaloniaProperty.RegisterAttached<Control, string>("HeaderDownClass", typeof(ExpanderHelper));

        /// <summary>
        /// get HeaderDownClass
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetHeaderDownClass(Control element)
        {
            return element.GetValue<string>(HeaderDownClassProperty);
        }

        /// <summary>
        /// set HeaderDownClass
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderDownClass(Control element, string value)
        {
            element.SetValue(HeaderDownClassProperty, value);
        }

        /// <summary>
        /// HeaderLeftClass AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<string> HeaderLeftClassProperty =
                    AvaloniaProperty.RegisterAttached<Control, string>("HeaderLeftClass", typeof(ExpanderHelper));

        /// <summary>
        /// get HeaderLeftClass
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetHeaderLeftClass(Control element)
        {
            return element.GetValue<string>(HeaderLeftClassProperty);
        }

        /// <summary>
        /// set HeaderLeftClass
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderLeftClass(Control element, string value)
        {
            element.SetValue(HeaderLeftClassProperty, value);
        }

        /// <summary>
        /// HeaderRightClass AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<string> HeaderRightClassProperty =
                    AvaloniaProperty.RegisterAttached<Control, string>("HeaderRightClass", typeof(ExpanderHelper));

        /// <summary>
        /// get HeaderRightClass
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetHeaderRightClass(Control element)
        {
            return element.GetValue<string>(HeaderRightClassProperty);
        }

        /// <summary>
        /// set HeaderRightClass
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderRightClass(Control element, string value)
        {
            element.SetValue(HeaderRightClassProperty, value);
        }
    }
}
