using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    /// <summary>
    /// event args for closing a windo
    /// </summary>
    public class ClosingWindowEventArgs : EventArgs
    {
        /// <summary>
        /// flag if the opertation is cancelled
        /// </summary>
        public bool Cancelled { get; set; }
    }
}
