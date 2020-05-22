using System;
using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// button with special properties
    /// </summary>
    public class SquareButton : Button
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(SquareButton);

        /// <summary>
        /// get/sets ContentCharacterCasing
        /// </summary>
        public CharacterCasing ContentCharacterCasing
        {
            get { return (CharacterCasing)GetValue(ContentCharacterCasingProperty); }
            set {  SetValue(ContentCharacterCasingProperty, value); }
        }

        /// <summary>
        /// <see cref="ContentCharacterCasing"/>
        /// </summary>
        public static readonly StyledProperty<CharacterCasing> ContentCharacterCasingProperty =
            AvaloniaProperty.Register<SquareButton, CharacterCasing>(nameof(ContentCharacterCasing));
    }
}
