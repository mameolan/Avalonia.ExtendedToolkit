using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    public static class ControlsHelper
    {
        public static readonly AttachedProperty<CharacterCasing> ContentCharacterCasingProperty =
            AvaloniaProperty.RegisterAttached<Control, CharacterCasing>("ContentCharacterCasing", typeof(ControlsHelper));

        public static CharacterCasing GetContentCharacterCasing(Control element)
        {
            return element.GetValue(ContentCharacterCasingProperty);
        }

        public static void SetContentCharacterCasing(Control element, object value)
        {
            element.SetValue(ContentCharacterCasingProperty, value);
        }

        public static readonly AttachedProperty<object> CornerRadiusProperty =
            AvaloniaProperty.RegisterAttached<Control, object>(nameof(CornerRadius), typeof(ControlsHelper));

        public static object GetCornerRadius(Control element)
        {
            return element.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(Control element, object value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        public static readonly AttachedProperty<SolidColorBrush> FocusBorderBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, SolidColorBrush>("FocusBorderBrush", typeof(ControlsHelper), defaultValue:(SolidColorBrush)Brushes.Transparent);

        public static SolidColorBrush GetFocusBorderBrush(Control element)
        {
            return element.GetValue(FocusBorderBrushProperty);
        }

        public static void SetFocusBorderBrush(Control element, SolidColorBrush value)
        {
            element.SetValue(FocusBorderBrushProperty, value);
        }

        public static readonly AttachedProperty<Thickness> FocusBorderThicknessProperty =
            AvaloniaProperty.RegisterAttached<Control, Thickness>("FocusBorderThickness", typeof(ControlsHelper));

        public static Thickness GetFocusBorderThickness(Control element)
        {
            return element.GetValue(FocusBorderThicknessProperty);
        }

        public static void SetFocusBorderThickness(Control element, Thickness value)
        {
            element.SetValue(FocusBorderThicknessProperty, value);
        }
    }
}