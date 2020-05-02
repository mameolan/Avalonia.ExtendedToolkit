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
        Minimum,
        Maximum,
        Tick,
        Precision
    }
}
