using System.Linq;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;

namespace Avalonia.ExtendedToolkit.Extensions
{
    /// <summary>
    /// style extension class
    /// </summary>
    public static class StylesExtensions
    {
        /// <summary>
        /// gets the styleinclude from styles
        /// </summary>
        /// <param name="styles"></param>
        /// <returns></returns>
        public static StyleInclude GetThemeStyle(this Styles styles)
        {
           return styles.OfType<StyleInclude>()
                         .FirstOrDefault(styleInclude => styleInclude.
                         Source.AbsoluteUri.StartsWith("avares://Avalonia.ExtendedToolkit/Styles/Themes")
                         ||
                         styleInclude.
                         Source.AbsoluteUri.StartsWith("resm:Avalonia.ExtendedToolkit.Styles.Themes")
                         );
        }

        
        /// <summary>
        /// gets the theme index of the style
        /// </summary>
        /// <param name="styles"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int GetThemeStyleIndex(this Styles styles, IStyle item)
        {
            if (item == null)
                return -1;

            return styles.OfType<IStyle>().ToList().IndexOf(item);
        }
    }
}
