using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from: 
    //https://www.codeproject.com/Articles/42849/Making-a-Drop-Down-Style-Custom-Color-Picker-in-WP

    /// <summary>
    /// class which holds the frmework defined colors
    /// </summary>
    internal class CustomColors
    {
        /// <summary>
        /// returns the system colors
        /// </summary>
        public IReadOnlyList<Color> SelectableColors { get;}

        /// <summary>
        /// fills the SelectableColors
        /// </summary>
        public CustomColors()
        {
            Type ColorsType = typeof(Colors);

            PropertyInfo[] ColorsProperty = ColorsType.GetProperties();

            var colors =  new List<Color>();

            foreach (var entry in ColorsProperty)
            {
                colors.Add((Color)entry.GetValue(entry));
            }

            SelectableColors = colors;

        }
    }
}
