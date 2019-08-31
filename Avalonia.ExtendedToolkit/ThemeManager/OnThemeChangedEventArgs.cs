using System;
using System.Collections.Generic;
using System.Text;

namespace Avalonia.ExtendedToolkit
{
    /// <summary>
    /// Class which is used as argument for an event to signal theme changes.
    /// </summary>
    public class OnThemeChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        public OnThemeChangedEventArgs(Theme theme)
        {
            this.Theme = theme;
        }

        /// <summary>
        /// The new theme.
        /// </summary>
        public Theme Theme { get; set; }
    }
}
