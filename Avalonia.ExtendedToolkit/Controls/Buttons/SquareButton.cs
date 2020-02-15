using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// button with special properties
    /// </summary>
    public class SquareButton : Button
    {
        public CharacterCasing ContentCharacterCasing
        {
            get { return (CharacterCasing)GetValue(ContentCharacterCasingProperty); }
            set {  SetValue(ContentCharacterCasingProperty, value); }
        }

        public static readonly StyledProperty<CharacterCasing> ContentCharacterCasingProperty =
            AvaloniaProperty.Register<SquareButton, CharacterCasing>(nameof(ContentCharacterCasing));
    }
}