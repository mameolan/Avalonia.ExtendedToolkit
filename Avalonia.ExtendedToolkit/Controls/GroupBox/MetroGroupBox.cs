using Avalonia.Controlz.Controls;
using Avalonia.Media;

using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    public class MetroGroupBox : GroupBox
    {
        public CharacterCasing ContentCharacterCasing
        {
            get { return (CharacterCasing)GetValue(ContentCharacterCasingProperty); }
            set { SetValue(ContentCharacterCasingProperty, value); }
        }

        public static readonly AvaloniaProperty ContentCharacterCasingProperty =
            AvaloniaProperty.Register<MetroGroupBox, CharacterCasing>(nameof(ContentCharacterCasing));



        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }


        public static readonly AvaloniaProperty HeaderFontSizeProperty =
            AvaloniaProperty.Register<MetroGroupBox, double>(nameof(HeaderFontSize), defaultValue: 12);



        public IBrush HeaderForeground
        {
            get { return (IBrush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }


        public static readonly AvaloniaProperty HeaderForegroundProperty =
            AvaloniaProperty.Register<MetroGroupBox, IBrush>(nameof(HeaderForeground));



        public FontFamily HeaderFontFamily
        {
            get { return (FontFamily)GetValue(HeaderFontFamilyProperty); }
            set { SetValue(HeaderFontFamilyProperty, value); }
        }


        public static readonly AvaloniaProperty HeaderFontFamilyProperty =
            AvaloniaProperty.Register<MetroGroupBox, FontFamily>(nameof(HeaderFontFamily), defaultValue: FontFamily.Default);



        public FontWeight HeaderFontWeight
        {
            get { return (FontWeight)GetValue(HeaderFontWeightProperty); }
            set { SetValue(HeaderFontWeightProperty, value); }
        }


        public static readonly AvaloniaProperty HeaderFontWeightProperty =
            AvaloniaProperty.Register<MetroGroupBox, FontWeight>(nameof(HeaderFontWeight), defaultValue: FontWeight.Normal);





    }
}
