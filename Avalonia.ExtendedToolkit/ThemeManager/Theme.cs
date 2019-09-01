using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using System;

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
        /// <param name="resourceDictionary">The ResourceDictionary of the theme.</param>
        public Theme(IStyle resourceDictionary)
        {
            this.Resources = resourceDictionary ?? throw new ArgumentNullException(nameof(resourceDictionary));

            object result;
            resourceDictionary.TryGetResource(ThemeNameKey, out result);
            this.Name = (string)result;
            resourceDictionary.TryGetResource(ThemeDisplayNameKey, out result);
            this.DisplayName = (string)result;
            resourceDictionary.TryGetResource(ThemeBaseColorSchemeKey, out result);
            this.BaseColorScheme = (string)result;
            resourceDictionary.TryGetResource(ThemeColorSchemeKey, out result);
            this.ColorScheme = (string)result;
            resourceDictionary.TryGetResource(ThemeShowcaseBrushKey, out result);
            this.ShowcaseBrush = (SolidColorBrush)result;
        }

        public Theme(Uri uri)
            :this(new StyleInclude(new Uri("resm:Styles?assembly=Avalonia.ExtendedToolkit"))
            {
                Source = uri
            })
        {
        }

        /// <summary>
        /// The ResourceDictionary that represents this application theme.
        /// </summary>
        public IStyle Resources { get; }

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
        public SolidColorBrush ShowcaseBrush { get; }

        //public override string ToString()
        //{
        //    return DisplayName;
        //}
    }
}