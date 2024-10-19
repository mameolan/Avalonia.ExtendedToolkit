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
            AvaloniaProperty.RegisterAttached<Control, CharacterCasing>("ContentCharacterCasing", typeof(ControlsHelper), defaultValue: CharacterCasing.Normal);

        /// <summary>
        /// get ContentCharacterCasing
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static CharacterCasing GetContentCharacterCasing(Control element)
        {
            return element.GetValue<CharacterCasing>(ContentCharacterCasingProperty);
        }

        /// <summary>
        /// set ContentCharacterCasing
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetContentCharacterCasing(Control element, CharacterCasing value)
        {
            element.SetValue(ContentCharacterCasingProperty, value);
        }

        /// <summary>
        /// attached property CornerRadius
        /// </summary>
        public static readonly AttachedProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.RegisterAttached<Control, CornerRadius>(nameof(CornerRadius), typeof(ControlsHelper));

        /// <summary>
        /// get CornerRadius
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static CornerRadius GetCornerRadius(Control element)
        {
            return element.GetValue<CornerRadius>(CornerRadiusProperty);
        }

        /// <summary>
        /// set CornerRadius
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetCornerRadius(Control element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// FocusBorderBrush AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<IBrush> FocusBorderBrushProperty =
            AvaloniaProperty.RegisterAttached<Control, IBrush>("FocusBorderBrush", typeof(ControlsHelper), defaultValue: (IBrush)Brushes.Transparent);

        /// <summary>
        /// get FocusBorderBrush
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetFocusBorderBrush(Control element)
        {
            return element.GetValue<IBrush>(FocusBorderBrushProperty);
        }

        /// <summary>
        /// set FocusBorderBrush
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetFocusBorderBrush(Control element, IBrush value)
        {
            element.SetValue(FocusBorderBrushProperty, value);
        }

        /// <summary>
        /// FocusBorderThickness AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<Thickness> FocusBorderThicknessProperty =
                    AvaloniaProperty.RegisterAttached<Control, Thickness>("FocusBorderThickness", typeof(ControlsHelper));

        /// <summary>
        /// get FocusBorderThickness
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Thickness GetFocusBorderThickness(Control element)
        {
            return element.GetValue<Thickness>(FocusBorderThicknessProperty);
        }

        /// <summary>
        /// set FocusBorderThickness
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetFocusBorderThickness(Control element, Thickness value)
        {
            element.SetValue(FocusBorderThicknessProperty, value);
        }

        /// <summary>
        /// HeaderMargin AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<Thickness> HeaderMarginProperty =
            AvaloniaProperty.RegisterAttached<Control, Thickness>("HeaderMargin", typeof(ControlsHelper));

        /// <summary>
        /// get HeaderMargin
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Thickness GetHeaderMargin(Control element)
        {
            return element.GetValue<Thickness>(HeaderMarginProperty);
        }

        /// <summary>
        /// set HeaderMargin
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderMargin(Control element, Thickness value)
        {
            element.SetValue(HeaderMarginProperty, value);
        }

        /// <summary>
        /// HeaderFontFamily AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<FontFamily> HeaderFontFamilyProperty =
            AvaloniaProperty.RegisterAttached<Control, FontFamily>("HeaderFontFamily", typeof(ControlsHelper), defaultValue: FontFamily.Default);

        /// <summary>
        /// get HeaderFontFamily
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static FontFamily GetHeaderFontFamily(Control element)
        {
            return element.GetValue<FontFamily>(HeaderFontFamilyProperty);
        }

        /// <summary>
        /// set HeaderFontFamily
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderFontFamily(Control element, FontFamily value)
        {
            element.SetValue(HeaderFontFamilyProperty, value);
        }

        /// <summary>
        /// HeaderFontSize AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<double> HeaderFontSizeProperty =
            AvaloniaProperty.RegisterAttached<Control, double>("HeaderFontSize", typeof(ControlsHelper), defaultValue: 12);

        /// <summary>
        /// get HeaderFontSize
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static double GetHeaderFontSize(Control element)
        {
            return element.GetValue<double>(HeaderFontSizeProperty);
        }

        /// <summary>
        /// set HeaderFontSize
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderFontSize(Control element, double value)
        {
            element.SetValue(HeaderFontSizeProperty, value);
        }

        /// <summary>
        /// HeaderFontWeight AttachedProperty
        /// </summary>
        public static readonly AttachedProperty<FontWeight> HeaderFontWeightProperty =
            AvaloniaProperty.RegisterAttached<Control, FontWeight>("HeaderFontWeight", typeof(ControlsHelper), defaultValue: FontWeight.Normal);

        /// <summary>
        /// get HeaderFontWeight
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static FontWeight GetHeaderFontWeight(Control element)
        {
            return element.GetValue<FontWeight>(HeaderFontWeightProperty);
        }

        /// <summary>
        /// set HeaderFontWeight
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetHeaderFontWeight(Control element, FontWeight value)
        {
            element.SetValue(HeaderFontWeightProperty, value);
        }
    }
}
