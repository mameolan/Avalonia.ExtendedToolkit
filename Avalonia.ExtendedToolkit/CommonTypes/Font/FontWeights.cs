using System;
using System.Globalization;
using System.Linq;
using Avalonia.Media;

namespace Avalonia.ExtendedToolkit.Font
{
    //from ms

    /// <summary>
    /// FontWeights contains predefined font weight structures for common font weights.
    /// </summary>
    public static class FontWeights
    {
        /// <summary>
        /// Predefined font weight : Thin.
        /// </summary>
        public static FontWeight Thin { get { return FontWeight.Thin; } }

        /// <summary>
        /// Predefined font weight : Extra-light.
        /// </summary>
        public static FontWeight ExtraLight { get { return FontWeight.ExtraLight; } }

        /// <summary>
        /// Predefined font weight : Ultra-light.
        /// </summary>
        public static FontWeight UltraLight { get { return FontWeight.UltraLight; } }

        /// <summary>
        /// Predefined font weight : Light.
        /// </summary>
        public static FontWeight Light { get { return FontWeight.Light; } }

        /// <summary>
        /// Predefined font weight : Normal.
        /// </summary>
        public static FontWeight Normal { get { return FontWeight.Normal; } }

        /// <summary>
        /// Predefined font weight : Regular.
        /// </summary>
        public static FontWeight Regular { get { return FontWeight.Regular; } }

        /// <summary>
        /// Predefined font weight : Medium.
        /// </summary>
        public static FontWeight Medium { get { return FontWeight.Medium; } }

        /// <summary>
        /// Predefined font weight : Demi-bold.
        /// </summary>
        public static FontWeight DemiBold { get { return FontWeight.DemiBold; } }

        /// <summary>
        /// Predefined font weight : Semi-bold.
        /// </summary>
        public static FontWeight SemiBold { get { return FontWeight.SemiBold; } }

        /// <summary>
        /// Predefined font weight : Bold.
        /// </summary>
        public static FontWeight Bold { get { return FontWeight.Bold; } }

        /// <summary>
        /// Predefined font weight : Extra-bold.
        /// </summary>
        public static FontWeight ExtraBold { get { return FontWeight.ExtraBold; } }

        /// <summary>
        /// Predefined font weight : Ultra-bold.
        /// </summary>
        public static FontWeight UltraBold { get { return FontWeight.UltraBold; } }

        /// <summary>
        /// Predefined font weight : Black.
        /// </summary>
        public static FontWeight Black { get { return FontWeight.Black; } }

        /// <summary>
        /// Predefined font weight : Heavy.
        /// </summary>
        public static FontWeight Heavy { get { return FontWeight.Heavy; } }

        /// <summary>
        /// Predefined font weight : ExtraBlack.
        /// </summary>
        public static FontWeight ExtraBlack { get { return FontWeight.ExtraBlack; } }

        /// <summary>
        /// Predefined font weight : UltraBlack.
        /// </summary>
        public static FontWeight UltraBlack { get { return FontWeight.UltraBlack; } }

        internal static bool FontWeightStringToKnownWeight(string s, IFormatProvider provider, ref FontWeight fontWeight)
        {
            switch (s.Length)
            {
                case 4:
                    if (s.Equals("Bold", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.Bold;
                        return true;
                    }
                    if (s.Equals("Thin", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.Thin;
                        return true;
                    }
                    break;

                case 5:
                    if (s.Equals("Black", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.Black;
                        return true;
                    }
                    if (s.Equals("Light", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.Light;
                        return true;
                    }
                    if (s.Equals("Heavy", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.Heavy;
                        return true;
                    }
                    break;

                case 6:
                    if (s.Equals("Normal", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.Normal;
                        return true;
                    }
                    if (s.Equals("Medium", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.Medium;
                        return true;
                    }
                    break;

                case 7:
                    if (s.Equals("Regular", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.Regular;
                        return true;
                    }
                    break;

                case 8:
                    if (s.Equals("SemiBold", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.SemiBold;
                        return true;
                    }
                    if (s.Equals("DemiBold", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.DemiBold;
                        return true;
                    }
                    break;

                case 9:
                    if (s.Equals("ExtraBold", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.ExtraBold;
                        return true;
                    }
                    if (s.Equals("UltraBold", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.UltraBold;
                        return true;
                    }
                    break;

                case 10:
                    if (s.Equals("ExtraLight", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.ExtraLight;
                        return true;
                    }
                    if (s.Equals("UltraLight", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.UltraLight;
                        return true;
                    }
                    if (s.Equals("ExtraBlack", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.ExtraBlack;
                        return true;
                    }
                    if (s.Equals("UltraBlack", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = FontWeights.UltraBlack;
                        return true;
                    }
                    break;
            }
            int weightValue;
            if (int.TryParse(s, NumberStyles.Integer, provider, out weightValue))
            {
                fontWeight = Enum.GetValues(typeof(FontWeight))
                    .OfType<FontWeight>().First(x => (int)x == weightValue);
                return true;
            }
            return false;
        }

        internal static bool FontWeightToString(int weight, out string convertedValue)
        {
            switch (weight)
            {
                case 100:
                    convertedValue = "Thin";
                    return true;

                case 200:
                    convertedValue = "ExtraLight";
                    return true;

                case 300:
                    convertedValue = "Light";
                    return true;

                case 400:
                    convertedValue = "Normal";
                    return true;

                case 500:
                    convertedValue = "Medium";
                    return true;

                case 600:
                    convertedValue = "SemiBold";
                    return true;

                case 700:
                    convertedValue = "Bold";
                    return true;

                case 800:
                    convertedValue = "ExtraBold";
                    return true;

                case 900:
                    convertedValue = "Black";
                    return true;

                case 950:
                    convertedValue = "ExtraBlack";
                    return true;
            }
            convertedValue = null;
            return false;
        }
    }
}
