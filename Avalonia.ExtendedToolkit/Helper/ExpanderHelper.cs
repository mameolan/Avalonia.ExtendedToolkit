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
                    AvaloniaProperty.RegisterAttached<IControl, string>("HeaderUpClass", typeof(ExpanderHelper));

        /// <summary>
        /// get HeaderUpClass
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetHeaderUpClass(IControl element)
        {
            return element.GetValue(HeaderUpClassProperty);
        }

        /// <summary>
        /// set HeaderUpClass
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderUpClass(IControl element, string value)
        {
            element.SetValue(HeaderUpClassProperty, value);
        }

        /// <summary>
        /// HeaderDownClass AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<string> HeaderDownClassProperty =
                    AvaloniaProperty.RegisterAttached<IControl, string>("HeaderDownClass", typeof(ExpanderHelper));

        /// <summary>
        /// get HeaderDownClass
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetHeaderDownClass(IControl element)
        {
            return element.GetValue(HeaderDownClassProperty);
        }

        /// <summary>
        /// set HeaderDownClass
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderDownClass(IControl element, string value)
        {
            element.SetValue(HeaderDownClassProperty, value);
        }

        /// <summary>
        /// HeaderLeftClass AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<string> HeaderLeftClassProperty =
                    AvaloniaProperty.RegisterAttached<IControl, string>("HeaderLeftClass", typeof(ExpanderHelper));

        /// <summary>
        /// get HeaderLeftClass
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetHeaderLeftClass(IControl element)
        {
            return element.GetValue(HeaderLeftClassProperty);
        }

        /// <summary>
        /// set HeaderLeftClass
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderLeftClass(IControl element, string value)
        {
            element.SetValue(HeaderLeftClassProperty, value);
        }

        /// <summary>
        /// HeaderRightClass AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<string> HeaderRightClassProperty =
                    AvaloniaProperty.RegisterAttached<IControl, string>("HeaderRightClass", typeof(ExpanderHelper));

        /// <summary>
        /// get HeaderRightClass
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetHeaderRightClass(IControl element)
        {
            return element.GetValue(HeaderRightClassProperty);
        }

        /// <summary>
        /// set HeaderRightClass
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderRightClass(IControl element, string value)
        {
            element.SetValue(HeaderRightClassProperty, value);
        }
    }
}
