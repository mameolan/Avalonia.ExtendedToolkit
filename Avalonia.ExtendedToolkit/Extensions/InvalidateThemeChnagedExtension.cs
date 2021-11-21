using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Extensions
{
    public static class InvalidateThemeChangedExtension
    {
        /// <summary>
        /// Defines the IsAttached attach property.
        /// </summary>
        public static readonly AttachedProperty<bool> IsAttachedProperty =
        AvaloniaProperty.RegisterAttached<IControl, bool>("IsAttached", typeof(InvalidateThemeChangedExtension));

        /// <summary>
        /// Gets the attach property IsAttached.
        /// </summary>
        public static bool GetIsAttached(IControl element)
        {
            return element.GetValue(IsAttachedProperty);
        }

        /// <summary>
        /// Sets the attach property IsAttached.
        /// </summary>

        public static void SetIsAttached(IControl element, bool value)
        {
            element.SetValue(IsAttachedProperty, value);
            ThemeManager.Instance.IsThemeChanged += (o, e) =>
              {
                  element?.InvalidateArrange();
                  element?.InvalidateMeasure();
                  element?.InvalidateStyles();
                  element?.InvalidateVisual();
              };

        }

    }
}