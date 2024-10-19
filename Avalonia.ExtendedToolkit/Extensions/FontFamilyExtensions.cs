using System.Collections.Generic;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Extensions
{
    /// <summary>
    /// fontfamily extension class
    /// </summary>
    public static class FontFamilyExtensions
    {
        private static List<FontFamily> _fontFamilies = new List<FontFamily>();
        
        /// <summary>
        /// returns the current installed fonts
        /// </summary>
        public static IEnumerable<FontFamily> InstalledFontFamilies
        {
            get
            {
                if (_fontFamilies.Count == 0)
                {
                    //.fixed?
                    foreach (var item in FontManager.Current.SystemFonts)
                    {
                        _fontFamilies.Add(new FontFamily(item.Name));
                    }
                }
                return FontManager.Current.SystemFonts;
            }
        }
    }
}
