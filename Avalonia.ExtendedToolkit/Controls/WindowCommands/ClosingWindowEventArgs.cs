using System;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/MahApps/MahApps.Metro

    public class ClosingWindowEventArgs : EventArgs
    {
        public bool Cancelled { get; set; }
    }
}
