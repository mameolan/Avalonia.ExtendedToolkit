using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// AttachedProperty for buttons
    /// </summary>
    public static class ButtonHelper
    {
        /// <summary>
        /// ContentCharacterCasing attached property
        /// </summary>
        public static readonly AttachedProperty<CharacterCasing> ContentCharacterCasingProperty =
            AvaloniaProperty.RegisterAttached<Button, CharacterCasing>("ContentCharacterCasing", typeof(ButtonHelper));

        /// <summary>
        /// get ContentCharacterCasing attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static CharacterCasing GetContentCharacterCasing(Button element)
        {
            return element.GetValue(ContentCharacterCasingProperty);
        }

        /// <summary>
        /// set ContentCharacterCasing attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetContentCharacterCasing(Button element, CharacterCasing value)
        {
            element.SetValue(ContentCharacterCasingProperty, value);
        }

        /// <summary>
        /// CornerRadius attached property
        /// </summary>
        public static readonly AttachedProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.RegisterAttached<Button
                , CornerRadius>(nameof(CornerRadius), typeof(ButtonHelper));

        /// <summary>
        /// get CornerRadius attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static CornerRadius GetCornerRadius(Button element)
        {
            return element.GetValue(CornerRadiusProperty);
        }

        /// <summary>
        /// set CornerRadius attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetCornerRadius(Button element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// FocusBorderBrush attached property
        /// </summary>
        public static readonly AttachedProperty<IBrush> FocusBorderBrushProperty =
            AvaloniaProperty.RegisterAttached<Button, IBrush>("FocusBorderBrush", typeof(ButtonHelper));

        /// <summary>
        /// get FocusBorderBrush attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush GetFocusBorderBrush(Button element)
        {
            return element.GetValue(FocusBorderBrushProperty);
        }

        /// <summary>
        /// set FocusBorderBrush attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetFocusBorderBrush(Button element, IBrush value)
        {
            element.SetValue(FocusBorderBrushProperty, value);
        }

        /// <summary>
        /// FocusBorderThickness attached property
        /// </summary>
        public static readonly AttachedProperty<Thickness> FocusBorderThicknessProperty =
            AvaloniaProperty.RegisterAttached<Button, Thickness>("FocusBorderThickness", typeof(ButtonHelper));

        /// <summary>
        /// get FocusBorderThickness attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Thickness GetFocusBorderThickness(Button element)
        {
            return element.GetValue(FocusBorderThicknessProperty);
        }

        /// <summary>
        /// set FocusBorderThickness attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetFocusBorderThickness(Button element, Thickness value)
        {
            element.SetValue(FocusBorderThicknessProperty, value);
        }
    }
}
