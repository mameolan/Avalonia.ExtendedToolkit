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
        AvaloniaProperty.RegisterAttached<Control, bool>("IsAttached", typeof(InvalidateThemeChangedExtension));

        /// <summary>
        /// Gets the attach property IsAttached.
        /// </summary>
        public static bool GetIsAttached(Control element)
        {
            return element.GetValue<bool>(IsAttachedProperty);
        }

        /// <summary>
        /// Sets the attach property IsAttached.
        /// </summary>

        public static void SetIsAttached(Control element, bool value)
        {
            element.SetValue(IsAttachedProperty, value);
            ThemeManager.Instance.IsThemeChanged += (o, e) =>
              {
                  element?.InvalidateArrange();
                  element?.InvalidateMeasure();
                  //.orig
                  //element?.InvalidateStyles();
                  element?.InvalidateVisual();
              };

        }

    }
}
