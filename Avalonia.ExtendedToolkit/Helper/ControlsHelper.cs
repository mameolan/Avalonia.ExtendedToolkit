using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// Control extensions
    /// </summary>
    public static class ControlsHelper
    {
        /// <summary>
        /// attached property ContentCharacterCasing
        /// </summary>
        public static readonly AttachedProperty<CharacterCasing> ContentCharacterCasingProperty =
            AvaloniaProperty.RegisterAttached<IControl, CharacterCasing>("ContentCharacterCasing", typeof(ControlsHelper), defaultValue: CharacterCasing.Normal);

        /// <summary>
        /// get ContentCharacterCasing
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static CharacterCasing GetContentCharacterCasing(IControl element)
        {
            return element.GetValue(ContentCharacterCasingProperty);
        }

        /// <summary>
        /// set ContentCharacterCasing
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetContentCharacterCasing(IControl element, CharacterCasing value)
        {
            element.SetValue(ContentCharacterCasingProperty, value);
        }

        /// <summary>
        /// attached property CornerRadius
        /// </summary>
        public static readonly AttachedProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.RegisterAttached<IControl, CornerRadius>(nameof(CornerRadius), typeof(ControlsHelper));

        /// <summary>
        /// get CornerRadius
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static CornerRadius GetCornerRadius(IControl element)
        {
            return element.GetValue(CornerRadiusProperty);
        }

        /// <summary>
        /// set CornerRadius
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetCornerRadius(IControl element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// FocusBorderBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> FocusBorderBrushProperty =
            AvaloniaProperty.RegisterAttached<IControl, IBrush>("FocusBorderBrush", typeof(ControlsHelper), defaultValue: (IBrush)Brushes.Transparent);

        /// <summary>
        /// get FocusBorderBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetFocusBorderBrush(IControl element)
        {
            return element.GetValue(FocusBorderBrushProperty);
        }

        /// <summary>
        /// set FocusBorderBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetFocusBorderBrush(IControl element, IBrush value)
        {
            element.SetValue(FocusBorderBrushProperty, value);
        }

        /// <summary>
        /// FocusBorderThickness AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<Thickness> FocusBorderThicknessProperty =
                    AvaloniaProperty.RegisterAttached<IControl, Thickness>("FocusBorderThickness", typeof(ControlsHelper));

        /// <summary>
        /// get FocusBorderThickness
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Thickness GetFocusBorderThickness(IControl element)
        {
            return element.GetValue(FocusBorderThicknessProperty);
        }

        /// <summary>
        /// set FocusBorderThickness
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetFocusBorderThickness(IControl element, Thickness value)
        {
            element.SetValue(FocusBorderThicknessProperty, value);
        }

        /// <summary>
        /// HeaderMargin AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<Thickness> HeaderMarginProperty =
            AvaloniaProperty.RegisterAttached<IControl, Thickness>("HeaderMargin", typeof(ControlsHelper));

        /// <summary>
        /// get HeaderMargin
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Thickness GetHeaderMargin(IControl element)
        {
            return element.GetValue(HeaderMarginProperty);
        }

        /// <summary>
        /// set HeaderMargin
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderMargin(IControl element, Thickness value)
        {
            element.SetValue(HeaderMarginProperty, value);
        }

        /// <summary>
        /// HeaderFontFamily AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<FontFamily> HeaderFontFamilyProperty =
            AvaloniaProperty.RegisterAttached<IControl, FontFamily>("HeaderFontFamily", typeof(ControlsHelper), defaultValue: FontFamily.Default);

        /// <summary>
        /// get HeaderFontFamily
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static FontFamily GetHeaderFontFamily(IControl element)
        {
            return element.GetValue(HeaderFontFamilyProperty);
        }

        /// <summary>
        /// set HeaderFontFamily
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderFontFamily(IControl element, FontFamily value)
        {
            element.SetValue(HeaderFontFamilyProperty, value);
        }

        /// <summary>
        /// HeaderFontSize AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<double> HeaderFontSizeProperty =
            AvaloniaProperty.RegisterAttached<IControl, double>("HeaderFontSize", typeof(ControlsHelper), defaultValue: 12);

        /// <summary>
        /// get HeaderFontSize
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static double GetHeaderFontSize(IControl element)
        {
            return element.GetValue(HeaderFontSizeProperty);
        }

        /// <summary>
        /// set HeaderFontSize
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderFontSize(IControl element, double value)
        {
            element.SetValue(HeaderFontSizeProperty, value);
        }

        /// <summary>
        /// HeaderFontWeight AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<FontWeight> HeaderFontWeightProperty =
            AvaloniaProperty.RegisterAttached<IControl, FontWeight>("HeaderFontWeight", typeof(ControlsHelper), defaultValue: FontWeight.Normal);

        /// <summary>
        /// get HeaderFontWeight
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static FontWeight GetHeaderFontWeight(IControl element)
        {
            return element.GetValue(HeaderFontWeightProperty);
        }

        /// <summary>
        /// set HeaderFontWeight
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderFontWeight(IControl element, FontWeight value)
        {
            element.SetValue(HeaderFontWeightProperty, value);
        }
    }
}
