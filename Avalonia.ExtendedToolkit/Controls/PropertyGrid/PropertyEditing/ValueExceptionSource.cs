using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit.Controls.PropertyGrid
{
    /// <summary>
    /// Specifies the source of value exception
    /// </summary>
    public enum ValueExceptionSource
    {
        /// <summary>
        /// Exception occurred during a Get operation
        /// </summary>
        Get,
        /// <summary>
        /// Exception occurred during a Set operation
        /// </summary>
        Set
    }
}
