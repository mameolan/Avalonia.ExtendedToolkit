using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// right now this does not work with avalonia
    /// </summary>
    public static class ControlsHelper
    {
        public static readonly AttachedProperty<CharacterCasing> ContentCharacterCasingProperty =
            AvaloniaProperty.RegisterAttached<Control, CharacterCasing>("ContentCharacterCasing", typeof(ControlsHelper), defaultValue: CharacterCasing.Normal);

        public static CharacterCasing GetContentCharacterCasing(Control element)
        {
            return element.GetValue(ContentCharacterCasingProperty);
        }

        public static void SetContentCharacterCasing(Control element, object value)
        {
            element.SetValue(ContentCharacterCasingProperty, value);
        }

        public static readonly AttachedProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.RegisterAttached<Control, CornerRadius>(nameof(CornerRadius), typeof(ControlsHelper));

        public static CornerRadius GetCornerRadius(Control element)
        {
            return element.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(Control element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        public static readonly AttachedProperty<IBrush> FocusBorderBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("FocusBorderBrush", typeof(ControlsHelper), defaultValue:(SolidColorBrush)Brushes.Transparent);

        public static IBrush GetFocusBorderBrush(Control element)
        {
            return element.GetValue(FocusBorderBrushProperty);
        }

        public static void SetFocusBorderBrush(Control element, IBrush value)
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

        public static readonly AttachedProperty<Thickness> HeaderMarginProperty =
            AvaloniaProperty.RegisterAttached<Control, Thickness>("HeaderMargin", typeof(ControlsHelper));

        public static Thickness GetHeaderMargin(Control element)
        {
            return element.GetValue(HeaderMarginProperty);
        }

        public static void SetHeaderMargin(Control element, Thickness value)
        {
            element.SetValue(HeaderMarginProperty, value);
        }

        public static readonly AttachedProperty<FontFamily> HeaderFontFamilyProperty =
            AvaloniaProperty.RegisterAttached<Control, FontFamily>("HeaderFontFamily", typeof(ControlsHelper), defaultValue: FontFamily.Default);

        public static FontFamily GetHeaderFontFamily(Control element)
        {
            return element.GetValue(HeaderFontFamilyProperty);
        }

        public static void SetHeaderFontFamily(Control element, FontFamily value)
        {
            element.SetValue(HeaderFontFamilyProperty, value);
        }

        public static readonly AttachedProperty<double> HeaderFontSizeProperty =
            AvaloniaProperty.RegisterAttached<Control, double>("HeaderFontSize", typeof(ControlsHelper), defaultValue: 12);

        public static double GetHeaderFontSize(Control element)
        {
            return element.GetValue(HeaderFontSizeProperty);
        }

        public static void SetHeaderFontSize(Control element, double value)
        {
            element.SetValue(HeaderFontSizeProperty, value);
        }

        public static readonly AttachedProperty<FontWeight> HeaderFontWeightProperty =
            AvaloniaProperty.RegisterAttached<Control, FontWeight>("HeaderFontWeight", typeof(ControlsHelper), defaultValue: FontWeight.Normal);

        public static FontWeight GetHeaderFontWeight(Control element)
        {
            return element.GetValue(HeaderFontWeightProperty);
        }

        public static void SetHeaderFontWeight(Control element, FontWeight value)
        {
            element.SetValue(HeaderFontWeightProperty, value);
        }
    }
}