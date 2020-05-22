using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Font
{
    /// <summary>
    /// font wheight extension
    /// </summary>
    public static class FontWeightExtensions
    {
        /// <summary>
        /// adds 400 (?) to the font weight enum
        /// </summary>
        /// <param name="fontWeight"></param>
        /// <returns></returns>
        public static int ToOpenTypeWeight(this FontWeight fontWeight)
        {
            //realfont weight in wpf (don't know where 400 comes from not documented. )
            return (int)fontWeight + 400;
        }
    }
}
