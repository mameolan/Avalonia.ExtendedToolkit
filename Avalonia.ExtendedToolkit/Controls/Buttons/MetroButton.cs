using Avalonia.Controls;
using Avalonia.Media;
using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// button with special properties
    /// </summary>
    public class MetroButton : Button
    {
        public Type StyleKey => typeof(Button);

        public CharacterCasing ContentCharacterCasing
        {
            get { return (CharacterCasing)GetValue(ContentCharacterCasingProperty); }
            set { SetValue(ContentCharacterCasingProperty, value); }
        }

        public static readonly StyledProperty<CharacterCasing> ContentCharacterCasingProperty =
            AvaloniaProperty.Register<MetroButton, CharacterCasing>(nameof(ContentCharacterCasing));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<MetroButton, CornerRadius>(nameof(CornerRadius));

        public IBrush FocusBorderBrush
        {
            get { return (IBrush)GetValue(FocusBorderBrushProperty); }
            set { SetValue(FocusBorderBrushProperty, value); }
        }

        public static readonly StyledProperty<IBrush> FocusBorderBrushProperty =
            AvaloniaProperty.Register<MetroButton, IBrush>(nameof(FocusBorderBrush));

        public Thickness FocusBorderThickness
        {
            get { return (Thickness)GetValue(FocusBorderThicknessProperty); }
            set {  SetValue(FocusBorderThicknessProperty, value); }
        }

        public static readonly StyledProperty<Thickness> FocusBorderThicknessProperty =
            AvaloniaProperty.Register<MetroButton, Thickness>(nameof(FocusBorderThickness));

        

    }
}