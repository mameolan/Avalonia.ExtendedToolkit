using System;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Font
{
    //from MS

    /// <summary>
    /// FontStyles contains predefined font style structures for common font styles.
    /// </summary>
    public static class FontStyles
    {
        /// <summary>
        /// Predefined font style : Normal.
        /// </summary>
        public static FontStyle Normal { get { return FontStyle.Normal; } }

        /// <summary>
        /// Predefined font style : Oblique.
        /// </summary>
        public static FontStyle Oblique { get { return FontStyle.Oblique; } }

        /// <summary>
        /// Predefined font style : Italic.
        /// </summary>
        public static FontStyle Italic { get { return FontStyle.Italic; } }

        internal static bool FontStyleStringToKnownStyle(string s, IFormatProvider provider, ref FontStyle fontStyle)
        {
            if (s.Equals("Normal", StringComparison.OrdinalIgnoreCase))
            {
                fontStyle = FontStyles.Normal;
                return true;
            }
            if (s.Equals("Italic", StringComparison.OrdinalIgnoreCase))
            {
                fontStyle = FontStyles.Italic;
                return true;
            }
            if (s.Equals("Oblique", StringComparison.OrdinalIgnoreCase))
            {
                fontStyle = FontStyles.Oblique;
                return true;
            }
            return false;
        }
    }
}
