using System;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// represents a theme entry
    /// </summary>
    public class Theme
    {
        /// <summary>
        /// Gets the key for the theme name.
        /// </summary>
        public const string ThemeNameKey = "Theme.Name";

        /// <summary>
        /// Gets the key for the theme display name.
        /// </summary>
        public const string ThemeDisplayNameKey = "Theme.DisplayName";

        /// <summary>
        /// Gets the key for the theme base color scheme.
        /// </summary>
        public const string ThemeBaseColorSchemeKey = "Theme.BaseColorScheme";

        /// <summary>
        /// Gets the key for the theme color scheme.
        /// </summary>
        public const string ThemeColorSchemeKey = "Theme.ColorScheme";

        /// <summary>
        /// Gets the key for the theme showcase brush.
        /// </summary>
        public const string ThemeShowcaseBrushKey = "Theme.ShowcaseBrush";

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="style">The ResourceDictionary of the theme.</param>
        public Theme(IStyle style)
        {
            this.ThemeStyle = style ?? throw new ArgumentNullException(nameof(style));

            object result;
            //.edited, ThemeVariant.Default
            (style as StyleInclude).TryGetResource(ThemeNameKey, ThemeVariant.Default, out result);
            this.Name = (string)result;
            (style as StyleInclude).TryGetResource(ThemeDisplayNameKey, ThemeVariant.Default, out result);
            this.DisplayName = (string)result;
            (style as StyleInclude).TryGetResource(ThemeBaseColorSchemeKey, ThemeVariant.Default, out result);
            this.BaseColorScheme = (string)result;
            (style as StyleInclude).TryGetResource(ThemeColorSchemeKey, ThemeVariant.Default, out result);
            this.ColorScheme = (string)result;
            (style as StyleInclude).TryGetResource(ThemeShowcaseBrushKey, ThemeVariant.Default, out result);
            this.ShowcaseBrush = (SolidColorBrush)result;
        }

        /// <summary>
        /// ctor with uri
        /// </summary>
        /// <param name="uri"></param>
        public Theme(Uri uri)
            :this(new StyleInclude(new Uri("resm:Styles?assembly=Avalonia.ExtendedToolkit"))
            {
                Source = uri
            })
        {
        }

        /// <summary>
        /// The Style that represents this application theme.
        /// </summary>
        public IStyle ThemeStyle { get; }

        /// <summary>
        /// Gets the name of the theme.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the display name of the theme.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Get the base color scheme for this theme.
        /// </summary>
        public string BaseColorScheme { get; }

        /// <summary>
        /// Gets the color scheme for this theme.
        /// </summary>
        public string ColorScheme { get; }

        /// <summary>
        /// Gets a brush which can be used to showcase this theme.
        /// </summary>
        public IBrush ShowcaseBrush { get; }

        //public override string ToString()
        //{
        //    return DisplayName;
        //}
    }
}
