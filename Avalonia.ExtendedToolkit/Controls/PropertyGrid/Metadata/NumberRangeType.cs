using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    /// <summary>
    /// Type for the number range attribute 
    /// which is used in the 
    /// <see cref="Converters.NumberRangeConverter"/>
    /// </summary>
    public enum NumberRangeType
    {
        /// <summary>
        /// minimum
        /// </summary>
        Minimum,
        /// <summary>
        /// maximum
        /// </summary>
        Maximum,
        /// <summary>
        /// tick
        /// </summary>
        Tick,
        /// <summary>
        /// precision
        /// </summary>
        Precision
    }
}
