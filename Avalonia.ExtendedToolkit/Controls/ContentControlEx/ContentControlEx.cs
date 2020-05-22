using System;
using Avalonia.Controls;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// contentcontrol with special properties
    /// </summary>
    public class ContentControlEx : ContentControl
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(ContentControlEx);

        /// <summary>
        /// get/sets ContentCharacterCasing
        /// </summary>
        public CharacterCasing ContentCharacterCasing
        {
            get { return (CharacterCasing)GetValue(ContentCharacterCasingProperty); }
            set { SetValue(ContentCharacterCasingProperty, value); }
        }

        /// <summary>
        /// <see cref="ContentCharacterCasing"/>
        /// </summary>
        public static readonly StyledProperty<CharacterCasing> ContentCharacterCasingProperty =
            AvaloniaProperty.Register<ContentControlEx, CharacterCasing>(nameof(ContentCharacterCasing), defaultValue:CharacterCasing.Normal);

        /// <summary>
        /// get/ sets RecognizesAccessKey
        /// </summary>
        public bool RecognizesAccessKey
        {
            get { return (bool)GetValue(RecognizesAccessKeyProperty); }
            set { SetValue(RecognizesAccessKeyProperty, value); }
        }

        /// <summary>
        /// <see cref="RecognizesAccessKey"/>
        /// </summary>
        public static readonly StyledProperty<bool> RecognizesAccessKeyProperty =
            AvaloniaProperty.Register<ContentControlEx, bool>(nameof(RecognizesAccessKey));
    }
}
