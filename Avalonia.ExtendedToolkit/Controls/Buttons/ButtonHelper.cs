using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls.Buttons
{
    public class ButtonHelper
    {

        public static readonly AttachedProperty<CharacterCasing> ContentCharacterCasingProperty =
            AvaloniaProperty.RegisterAttached<ButtonHelper, Button, CharacterCasing>("ContentCharacterCasing");

        public static CharacterCasing GetContentCharacterCasing(Button element)
        {
            return element.GetValue(ContentCharacterCasingProperty);
        }

        public static void SetContentCharacterCasing(Button element, CharacterCasing value)
        {
            element.SetValue(ContentCharacterCasingProperty, value);
        }



        public static readonly AttachedProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.RegisterAttached<ButtonHelper, Button
                , CornerRadius>(nameof(CornerRadius));

        public static CornerRadius GetCornerRadius(Button element)
        {
            return element.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(Button element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }



        public static readonly AttachedProperty<SolidColorBrush> FocusBorderBrushProperty =
            AvaloniaProperty.RegisterAttached<ButtonHelper, Button, SolidColorBrush>("FocusBorderBrush");

        public static SolidColorBrush GetFocusBorderBrush(Button element)
        {
            return element.GetValue(FocusBorderBrushProperty);
        }

        public static void SetFocusBorderBrush(Button element, SolidColorBrush value)
        {
            element.SetValue(FocusBorderBrushProperty, value);
        }



        public static readonly AttachedProperty<Thickness> FocusBorderThicknessProperty =
            AvaloniaProperty.RegisterAttached<ButtonHelper, Button, Thickness>("FocusBorderThickness");

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
