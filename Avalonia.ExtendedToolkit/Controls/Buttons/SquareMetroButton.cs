using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class SquareMetroButton: Button
    {
        public CharacterCasing ContentCharacterCasing
        {
            get { return (CharacterCasing)GetValue(ContentCharacterCasingProperty); }
            set { SetValue(ContentCharacterCasingProperty, value); }
        }

        public static readonly AvaloniaProperty ContentCharacterCasingProperty =
            AvaloniaProperty.Register<SquareMetroButton, CharacterCasing>(nameof(ContentCharacterCasing));
    }
}