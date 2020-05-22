using System.Linq;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// skin extensions
    /// </summary>
    public static class SkinExtensions
    {
        /// <summary>
        /// search the styles of type StyleInclude
        /// which have the source url of the skin
        /// </summary>
        /// <param name="styles"></param>
        /// <returns></returns>
        public static StyleInclude GetSkinStyle(this Styles styles)
        {
            return styles.OfType<StyleInclude>()
                          .FirstOrDefault(styleInclude => styleInclude.
                          Source.AbsoluteUri.StartsWith("avares://Avalonia.ExtendedToolkit/Styles/Skins/"));
        }
    }
}
