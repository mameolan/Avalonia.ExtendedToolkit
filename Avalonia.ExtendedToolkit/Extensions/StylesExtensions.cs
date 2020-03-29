using System.Linq;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;

namespace Avalonia.ExtendedToolkit.Extensions
{
    public static class StylesExtensions
    {
        public static StyleInclude GetThemeStyle(this Styles styles)
        {
           return styles.OfType<StyleInclude>()
                         .FirstOrDefault(styleInclude => styleInclude.
                         Source.AbsoluteUri.StartsWith("avares://Avalonia.ExtendedToolkit/Styles/Themes"));
        }

        public static int GetThemeStyleIndex(this Styles styles, IStyle item)
        {
            if (item == null)
                return -1;

            return styles.OfType<IStyle>().ToList().IndexOf(item);
        }
    }
}
