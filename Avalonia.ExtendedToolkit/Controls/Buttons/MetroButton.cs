using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class MetroButton : Button
    {
        public CharacterCasing ContentCharacterCasing
        {
            get { return (CharacterCasing)GetValue(ContentCharacterCasingProperty); }
            set { SetValue(ContentCharacterCasingProperty, value); }
        }

        public static readonly AvaloniaProperty ContentCharacterCasingProperty =
            AvaloniaProperty.Register<MetroButton, CharacterCasing>(nameof(ContentCharacterCasing));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly AvaloniaProperty CornerRadiusProperty =
            AvaloniaProperty.Register<MetroButton, CornerRadius>(nameof(CornerRadius));

        public SolidColorBrush FocusBorderBrush
        {
            get { return (SolidColorBrush)GetValue(FocusBorderBrushProperty); }
            set { SetValue(FocusBorderBrushProperty, value); }
        }

        public static readonly AvaloniaProperty FocusBorderBrushProperty =
            AvaloniaProperty.Register<MetroButton, SolidColorBrush>(nameof(FocusBorderBrush));

        public Thickness FocusBorderThickness
        {
            get { return (Thickness)GetValue(FocusBorderThicknessProperty); }
            set {  SetValue(FocusBorderThicknessProperty, value); }
        }

        public static readonly AvaloniaProperty FocusBorderThicknessProperty =
            AvaloniaProperty.Register<MetroButton, Thickness>(nameof(FocusBorderThickness));

    }
}