using System;
using Avalonia.Controlz.Controls;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Controls
{
    /// <summary>
    /// GroupBox with CharacterCasing
    /// </summary>
    public class MetroGroupBox : GroupBox
    {
        /// <summary>
        /// style key of this control
        /// </summary>
        public Type StyleKey => typeof(MetroGroupBox);

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
            AvaloniaProperty.Register<MetroGroupBox, CharacterCasing>(nameof(ContentCharacterCasing));

        /// <summary>
        /// get/sets HeaderFontSize
        /// </summary>
        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        /// <summary>
        /// <see cref="HeaderFontSize"/>
        /// </summary>
        public static readonly StyledProperty<double> HeaderFontSizeProperty =
            AvaloniaProperty.Register<MetroGroupBox, double>(nameof(HeaderFontSize), defaultValue: 12);

        /// <summary>
        /// get/sets HeaderForeground
        /// </summary>
        public IBrush HeaderForeground
        {
            get { return (IBrush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        /// <summary>
        /// <see cref="HeaderForeground"/>
        /// </summary>
        public static readonly StyledProperty<IBrush> HeaderForegroundProperty =
            AvaloniaProperty.Register<MetroGroupBox, IBrush>(nameof(HeaderForeground));

        /// <summary>
        /// get/sets HeaderFontFamily
        /// </summary>
        public FontFamily HeaderFontFamily
        {
            get { return (FontFamily)GetValue(HeaderFontFamilyProperty); }
            set { SetValue(HeaderFontFamilyProperty, value); }
        }

        /// <summary>
        /// <see cref="HeaderFontFamily"/>
        /// </summary>
        public static readonly StyledProperty<FontFamily> HeaderFontFamilyProperty =
            AvaloniaProperty.Register<MetroGroupBox, FontFamily>(nameof(HeaderFontFamily), defaultValue: FontFamily.Default);

        /// <summary>
        /// get/sets HeaderFontWeight
        /// </summary>
        public FontWeight HeaderFontWeight
        {
            get { return (FontWeight)GetValue(HeaderFontWeightProperty); }
            set { SetValue(HeaderFontWeightProperty, value); }
        }

        /// <summary>
        /// <see cref="HeaderFontWeight"/>
        /// </summary>
        public static readonly StyledProperty<FontWeight> HeaderFontWeightProperty =
            AvaloniaProperty.Register<MetroGroupBox, FontWeight>(nameof(HeaderFontWeight), defaultValue: FontWeight.Normal);
    }
}
