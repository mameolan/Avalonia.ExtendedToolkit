using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Media;

namespace Avalonia.Controlz.Font
{
    public static class FontWeightExtensions
    {
        public static int ToOpenTypeWeight(this FontWeight fontWeight)
        {
            //realfont weight in wpf (don't know where 400 comes from not documented. )
            return (int)fontWeight + 400;
        }

    }
}
