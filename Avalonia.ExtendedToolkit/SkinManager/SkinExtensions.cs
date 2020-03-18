using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using System.Linq;

namespace Avalonia.ExtendedToolkit
{
    public static class SkinExtensions
    {
        public static StyleInclude GetSkinStyle(this Styles styles)
        {
            return styles.OfType<StyleInclude>()
                          .FirstOrDefault(styleInclude => styleInclude.
                          Source.AbsoluteUri.StartsWith("avares://Avalonia.ExtendedToolkit/Styles/Skins/"));
        }
    }
}