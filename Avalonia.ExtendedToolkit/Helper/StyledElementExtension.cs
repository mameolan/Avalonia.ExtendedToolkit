using Avalonia.Styling;

namespace Avalonia.ExtendedToolkit.Helper
{
    public static class StyledElementExtension
    {
        public static readonly AttachedProperty<IStyle> StyleProperty =
            AvaloniaProperty.RegisterAttached<StyledElement, IStyle>(nameof(Style), typeof(StyledElementExtension));

        public static IStyle GetStyle(StyledElement element)
        {
            return element.GetValue(StyleProperty);
        }

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
    }
}