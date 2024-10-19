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
            AvaloniaProperty.RegisterAttached<StyledElement, IStyle>(nameof(Style), typeof(StyledElementHelper));

        /// <summary>
        /// Style
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IStyle GetStyle(StyledElement element)
        {
            return element.GetValue<IStyle>(StyleProperty);
        }

        /// <summary>
        /// Style
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetStyle(StyledElement element, IStyle value)
        {
            element.SetValue(StyleProperty, value);
            OnStyleChanged(element, value);
        }

        private static void OnStyleChanged(StyledElement styledElement, IStyle style)
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
            AvaloniaProperty.RegisterAttached<StyledElement, Classes>(nameof(Classes), typeof(StyledElementHelper));

        /// <summary>
        /// get Classes
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Classes GetClasses(StyledElement element)
        {
            return element.GetValue<Classes>(ClassesProperty);
        }

        /// <summary>
        /// set Classes
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        //.dangerous change: element.Classes.Add(value.ToString());
        public static void SetClasses(StyledElement element, Classes value)
        {
            element.SetValue(ClassesProperty, value);
            OnClassesChanged(element, value);
        }

        private static void OnClassesChanged(StyledElement element, Classes value)
        {
            if(value!=null)
            {
                element.Classes.Add(value.ToString());
            }
        }
    }
}
