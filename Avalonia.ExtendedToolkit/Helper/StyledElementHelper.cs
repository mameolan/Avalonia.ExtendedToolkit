using Avalonia.Controls;
using Avalonia.Styling;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// StyledElement attached properties
    /// </summary>
    public static class StyledElementHelper
    {
        /// <summary>
        /// Style AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IStyle> StyleProperty =
            AvaloniaProperty.RegisterAttached<IStyledElement, IStyle>(nameof(Style), typeof(StyledElementHelper));

        /// <summary>
        /// Style
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IStyle GetStyle(IStyledElement element)
        {
            return element.GetValue(StyleProperty);
        }

        /// <summary>
        /// Style
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetStyle(IStyledElement element, IStyle value)
        {
            element.SetValue(StyleProperty, value);
            OnStyleChanged(element, value);
        }

        private static void OnStyleChanged(IStyledElement styledElement, IStyle style)
        {
            if (style != null)
            {
                styledElement.Styles.Add(style);
            }
        }

        /// <summary>
        /// Classes AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<Classes> ClassesProperty =
            AvaloniaProperty.RegisterAttached<IStyledElement, Classes>(nameof(Classes), typeof(StyledElementHelper));

        /// <summary>
        /// get Classes
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Classes GetClasses(IStyledElement element)
        {
            return element.GetValue(ClassesProperty);
        }

        /// <summary>
        /// set Classes
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetClasses(IStyledElement element, Classes value)
        {
            element.SetValue(ClassesProperty, value);
            OnClassesChanged(element, value);
        }

        private static void OnClassesChanged(IStyledElement element, Classes value)
        {
            if(value!=null)
            {
                element.Classes = value;
            }
        }
    }
}
