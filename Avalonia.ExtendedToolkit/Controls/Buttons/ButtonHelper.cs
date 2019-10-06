using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    public static class ButtonHelper
    {
        public static readonly AttachedProperty<CharacterCasing> ContentCharacterCasingProperty =
            AvaloniaProperty.RegisterAttached<Button, CharacterCasing>("ContentCharacterCasing", typeof(ButtonHelper));

        public static CharacterCasing GetContentCharacterCasing(Button element)
        {
            return element.GetValue(ContentCharacterCasingProperty);
        }

        public static void SetContentCharacterCasing(Button element, CharacterCasing value)
        {
            element.SetValue(ContentCharacterCasingProperty, value);
        }

        public static readonly AttachedProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.RegisterAttached<Button
                , CornerRadius>(nameof(CornerRadius), typeof(ButtonHelper));

        public static CornerRadius GetCornerRadius(Button element)
        {
            return element.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(Button element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        public static readonly AttachedProperty<IBrush> FocusBorderBrushProperty =
            AvaloniaProperty.RegisterAttached<Button, IBrush>("FocusBorderBrush", typeof(ButtonHelper));

        public static IBrush GetFocusBorderBrush(Button element)
        {
            return element.GetValue(FocusBorderBrushProperty);
        }

        public static void SetFocusBorderBrush(Button element, IBrush value)
        {
            element.SetValue(FocusBorderBrushProperty, value);
        }

        public static readonly AttachedProperty<Thickness> FocusBorderThicknessProperty =
            AvaloniaProperty.RegisterAttached<Button, Thickness>("FocusBorderThickness", typeof(ButtonHelper));

        public static Thickness GetFocusBorderThickness(Button element)
        {
            return element.GetValue(FocusBorderThicknessProperty);
        }

        public static void SetFocusBorderThickness(Button element, Thickness value)
        {
            element.SetValue(FocusBorderThicknessProperty, value);
        }
    }
}