namespace Avalonia.ExtendedToolkit.Controls
{
    public class HamburgerMenuGlyphItem: HamburgerMenuItem
    {
        public string Glyph
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        public static readonly AvaloniaProperty GlyphProperty =
            AvaloniaProperty.Register<HamburgerMenuGlyphItem, string>(nameof(Glyph));
    }
}