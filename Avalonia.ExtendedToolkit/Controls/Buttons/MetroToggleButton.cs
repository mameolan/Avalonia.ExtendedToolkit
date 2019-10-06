using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// toggle button with special properties
    /// </summary>
    public class MetroToggleButton : ToggleButton
    {
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly AvaloniaProperty CornerRadiusProperty =
            AvaloniaProperty.Register<MetroToggleButton, CornerRadius>(nameof(CornerRadius));

        public Thickness FocusBorderThickness
        {
            get { return (Thickness)GetValue(FocusBorderThicknessProperty); }
            set { SetValue(FocusBorderThicknessProperty, value); }
        }

        public static readonly AvaloniaProperty FocusBorderThicknessProperty =
            AvaloniaProperty.Register<MetroToggleButton, Thickness>(nameof(FocusBorderThickness));

        public SolidColorBrush FocusBorderBrush
        {
            get { return (SolidColorBrush)GetValue(FocusBorderBrushProperty); }
            set { SetValue(FocusBorderBrushProperty, value); }
        }

        public static readonly AvaloniaProperty FocusBorderBrushProperty =
            AvaloniaProperty.Register<MetroToggleButton, SolidColorBrush>(nameof(FocusBorderBrush));

        public CharacterCasing ContentCharacterCasing
        {
            get { return (CharacterCasing)GetValue(ContentCharacterCasingProperty); }
            set { SetValue(ContentCharacterCasingProperty, value); }
        }

        public static readonly AvaloniaProperty ContentCharacterCasingProperty =
            AvaloniaProperty.Register<MetroToggleButton, CharacterCasing>(nameof(ContentCharacterCasing));
    }
}