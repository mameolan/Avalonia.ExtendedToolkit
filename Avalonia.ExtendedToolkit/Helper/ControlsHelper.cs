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
            AvaloniaProperty.RegisterAttached<IControl, CharacterCasing>("ContentCharacterCasing", typeof(ControlsHelper), defaultValue: CharacterCasing.Normal);

        public static CharacterCasing GetContentCharacterCasing(IControl element)
        {
            return element.GetValue(ContentCharacterCasingProperty);
        }

        public static void SetContentCharacterCasing(IControl element, object value)
        {
            element.SetValue(ContentCharacterCasingProperty, value);
        }

        public static readonly AttachedProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.RegisterAttached<IControl, CornerRadius>(nameof(CornerRadius), typeof(ControlsHelper));

        public static CornerRadius GetCornerRadius(IControl element)
        {
            return element.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(IControl element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        public static readonly AttachedProperty<IBrush> FocusBorderBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("FocusBorderBrush", typeof(ControlsHelper), defaultValue:(SolidColorBrush)Brushes.Transparent);

        public static IBrush GetFocusBorderBrush(IControl element)
        {
            return element.GetValue(FocusBorderBrushProperty);
        }

        public static void SetFocusBorderBrush(IControl element, IBrush value)
        {
            element.SetValue(FocusBorderBrushProperty, value);
        }

        public static readonly AttachedProperty<Thickness> FocusBorderThicknessProperty =
            AvaloniaProperty.RegisterAttached<IControl, Thickness>("FocusBorderThickness", typeof(ControlsHelper));

        public static Thickness GetFocusBorderThickness(IControl element)
        {
            return element.GetValue(FocusBorderThicknessProperty);
        }

        public static void SetFocusBorderThickness(IControl element, Thickness value)
        {
            element.SetValue(FocusBorderThicknessProperty, value);
        }

        public static readonly AttachedProperty<Thickness> HeaderMarginProperty =
            AvaloniaProperty.RegisterAttached<IControl, Thickness>("HeaderMargin", typeof(ControlsHelper));

        public static Thickness GetHeaderMargin(IControl element)
        {
            return element.GetValue(HeaderMarginProperty);
        }

        public static void SetHeaderMargin(IControl element, Thickness value)
        {
            element.SetValue(HeaderMarginProperty, value);
        }

        public static readonly AttachedProperty<FontFamily> HeaderFontFamilyProperty =
            AvaloniaProperty.RegisterAttached<IControl, FontFamily>("HeaderFontFamily", typeof(ControlsHelper), defaultValue: FontFamily.Default);

        public static FontFamily GetHeaderFontFamily(IControl element)
        {
            return element.GetValue(HeaderFontFamilyProperty);
        }

        public static void SetHeaderFontFamily(IControl element, FontFamily value)
        {
            element.SetValue(HeaderFontFamilyProperty, value);
        }

        public static readonly AttachedProperty<double> HeaderFontSizeProperty =
            AvaloniaProperty.RegisterAttached<IControl, double>("HeaderFontSize", typeof(ControlsHelper), defaultValue: 12);

        public static double GetHeaderFontSize(IControl element)
        {
            return element.GetValue(HeaderFontSizeProperty);
        }

        public static void SetHeaderFontSize(IControl element, double value)
        {
            element.SetValue(HeaderFontSizeProperty, value);
        }

        public static readonly AttachedProperty<FontWeight> HeaderFontWeightProperty =
            AvaloniaProperty.RegisterAttached<IControl, FontWeight>("HeaderFontWeight", typeof(ControlsHelper), defaultValue: FontWeight.Normal);

        public static FontWeight GetHeaderFontWeight(IControl element)
        {
            return element.GetValue(HeaderFontWeightProperty);
        }

        public static void SetHeaderFontWeight(IControl element, FontWeight value)
        {
            element.SetValue(HeaderFontWeightProperty, value);
        }
    }
}