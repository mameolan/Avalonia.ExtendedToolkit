using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// contentcontrol with special properties
    /// </summary>
    public class ContentControlEx : ContentControl
    {
        public CharacterCasing ContentCharacterCasing
        {
            get { return (CharacterCasing)GetValue(ContentCharacterCasingProperty); }
            set { SetValue(ContentCharacterCasingProperty, value); }
        }

        public static readonly AvaloniaProperty<CharacterCasing> ContentCharacterCasingProperty =
            AvaloniaProperty.Register<ContentControlEx, CharacterCasing>(nameof(ContentCharacterCasing), defaultValue:CharacterCasing.Normal);

        public bool RecognizesAccessKey
        {
            get { return (bool)GetValue(RecognizesAccessKeyProperty); }
            set { SetValue(RecognizesAccessKeyProperty, value); }
        }

        public static readonly AvaloniaProperty<bool> RecognizesAccessKeyProperty =
            AvaloniaProperty.Register<ContentControlEx, bool>(nameof(RecognizesAccessKey));
    }
}