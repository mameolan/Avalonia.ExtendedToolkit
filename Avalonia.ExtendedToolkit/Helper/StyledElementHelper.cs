using Avalonia.Controls;
using Avalonia.Styling;

namespace Avalonia.ExtendedToolkit.Controls
{
    public static class StyledElementHelper
    {
        public static readonly AttachedProperty<IStyle> StyleProperty =
            AvaloniaProperty.RegisterAttached<IStyledElement, IStyle>(nameof(Style), typeof(StyledElementHelper));

        public static IStyle GetStyle(IStyledElement element)
        {
            return element.GetValue(StyleProperty);
        }

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

        public static readonly AttachedProperty<Classes> ClassesProperty =
            AvaloniaProperty.RegisterAttached<IStyledElement, Classes>(nameof(Classes), typeof(StyledElementHelper));

        public static Classes GetClasses(IStyledElement element)
        {
            return element.GetValue(ClassesProperty);
        }

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
